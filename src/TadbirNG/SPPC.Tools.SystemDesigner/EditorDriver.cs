using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model.Project;

namespace SPPC.Tools.SystemDesigner
{
    public class EditorDriver
    {
        static EditorDriver()
        {
            _metadata = LoadMetadata();
        }

        public static void SetupBindings<TForm>(TForm form, object entity)
            where TForm : Control
        {
            var metadata = GetEntityMetadata(entity);
            if (metadata != null)
            {
                Array.ForEach(metadata.Bindings.ToArray(), binding =>
                {
                    var control = form.Controls
                        .Cast<Control>()
                        .Where(ctl => ctl.Name == binding.ControlName)
                        .FirstOrDefault();
                    if (control != null)
                    {
                        control.DataBindings.Add(binding.PropertyName, entity, binding.DataMember);
                    }
                });
            }
        }

        public static string GetValidationResult(object entity)
        {
            var resultBuilder = new StringBuilder();
            var metadata = GetEntityMetadata(entity);
            if (metadata != null)
            {
                var errors = new List<string>();
                Array.ForEach(metadata.Rules.ToArray(), rule =>
                {
                    var value = Reflector.GetProperty(entity, rule.FieldName);
                    if (rule.Required && String.IsNullOrEmpty(value?.ToString()?.Trim()))
                    {
                        errors.Add($"{rule.FieldName} is required and cannot be empty or whitespace.");
                    }
                    else
                    {
                        var length = value?.ToString()?.Length;
                        if (length < rule.MinLength || length > rule.MaxLength)
                        {
                            errors.Add(
                                $"{rule.FieldName} must have between {rule.MinLength} and {rule.MaxLength} characters.");
                        }
                    }
                });

                if (errors.Any())
                {
                    resultBuilder.AppendLine("Please correct the following error(s) :");
                    resultBuilder.AppendLine();
                    resultBuilder.Append(
                        String.Join(Environment.NewLine, errors.Select(error => $"* {error}")));
                }
            }

            return resultBuilder.ToString();
        }

        private static List<EntityMetadata> LoadMetadata()
        {
            var metadata = new List<EntityMetadata>();
            var assembly = Assembly.GetAssembly(typeof(EditorDriver));
            if (assembly != null)
            {
                using var reader = new StreamReader(
                    assembly.GetManifestResourceStream(_matadataUri));
                string jsonConfig = reader.ReadToEnd();
                metadata = JsonHelper.To<List<EntityMetadata>>(jsonConfig);
            }

            return metadata;
        }

        private static EntityMetadata GetEntityMetadata(object entity)
        {
            return _metadata
                .Where(meta => meta.Type == entity.GetType().Name)
                .FirstOrDefault();
        }

        private const string _matadataUri = "SPPC.Tools.SystemDesigner.project-metadata.json";
        private static readonly List<EntityMetadata> _metadata;
    }
}
