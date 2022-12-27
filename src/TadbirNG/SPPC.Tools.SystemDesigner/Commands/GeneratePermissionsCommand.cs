using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesigner.Commands
{
    public class GeneratePermissionsCommand : ICommand
    {
        public GeneratePermissionsCommand(CrudWizardModel model)
        {
            _entity = model.EntityInfo;
            _model = model.EntityActions;
            _implementsRepo = model.Options.HasRepoImplementation;
        }

        public void Execute()
        {
            GeneratePermissionsEnum();
            RegisterSecureEntity();
            if (_implementsRepo)
            {
                RegisterEntityType();
            }
        }

        private void GeneratePermissionsEnum()
        {
            var template = new CsPermissionsEnum(_entity, _model);
            var transformed = template.TransformText();
            var allPermissions = File.ReadAllText(PathConfig.PermissionsPath, Encoding.UTF8);
            var endMarker = $"}}{Environment.NewLine}";
            int index = allPermissions.LastIndexOf(endMarker);
            if (index != -1)
            {
                allPermissions = allPermissions.Insert(index, transformed);
                File.WriteAllText(PathConfig.PermissionsPath, allPermissions, Encoding.UTF8);
            }
        }

        private void RegisterSecureEntity()
        {
            var template = @"
        /// <summary>
        /// نام موجودیت {0}
        /// </summary>
        public const string {1} = ""{1}"";
";
            var sourcePath = Path.Combine(Path.GetDirectoryName(PathConfig.PermissionsPath), "SecureEntity.cs");
            var contents = File.ReadAllText(sourcePath, Encoding.UTF8);
            var endMarker = $"    }}{Environment.NewLine}}}{Environment.NewLine}";
            int index = contents.IndexOf(endMarker);
            if (index != -1)
            {
                contents = contents.Insert(
                    index, String.Format(template, _entity.SingularName, _entity.Entity.Name));
                File.WriteAllText(sourcePath, contents, Encoding.UTF8);
            }
        }

        private void RegisterEntityType()
        {
            var regex = new Regex(@"        (\w+) = (\d+)");
            var sourceFile = _entity.IsSystemEntity ? "SysEntityTypeId.cs" : "EntityTypeId.cs";
            var sourcePath = Path.Combine(PathConfig.SolutionRoot, "SPPC.Tadbir.Persistence", "Metadata", sourceFile);
            var contents = File.ReadAllText(sourcePath, Encoding.UTF8);
            var match = regex
                .Matches(contents)
                .LastOrDefault();
            if (match != null)
            {
                int lastId = Int32.Parse(match.Groups
                    .Cast<Group>()
                    .Last()
                    .Value);
                var template = @",
        {0} = {1}";
                contents = contents.Insert(
                    match.Index + match.Length, String.Format(template, _entity.Entity.Name, lastId + 1));
                File.WriteAllText(sourcePath, contents, Encoding.UTF8);
            }
        }

        private readonly EntityInfoModel _entity;
        private readonly EntityActionsModel _model;
        private readonly bool _implementsRepo;
    }
}
