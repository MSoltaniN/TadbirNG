using System;
using System.Collections.Generic;
using System.IO;
using SPPC.Framework.Common;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class GenerateFileCommand : RepositoryCommand, IRepositoryCommand
    {
        public GenerateFileCommand() : base()
        {
        }

        public override void Execute()
        {
            base.Execute();
            string path = String.Empty;
            object param = null;
            string templateFileName = (string)Parameters["fileName"];
            if (Parameters.ContainsKey("item"))
            {
                param = Parameters["item"];
                var entity = Parameters["item"] as Entity;
                var fileName = String.Format(templateFileName, entity.Name);
                path = !String.IsNullOrWhiteSpace(entity.Repository.GenerationOutputPath)
                    ? Path.Combine(GetTemplatePath(entity, templateFileName), fileName)
                    : fileName;
            }
            else if (Parameters.ContainsKey("object"))
            {
                param = Parameters["object"];
                var repository = Parameters["object"] as Repository;
                path = !String.IsNullOrWhiteSpace(repository.GenerationOutputPath)
                    ? Path.Combine(repository.GenerationOutputPath, templateFileName)
                    : templateFileName;
            }

            var templateType = Type.GetType((string)Parameters["template"]);
            var template = Reflector.Instantiate(templateType, param) as ITextTemplate;
            File.WriteAllText(path, template.TransformText());
        }

        protected override IDictionary<string, string> GetRequiredParameters()
        {
            var requiredParams = new Dictionary<string, string>
            {
                { "fileName", typeof(string).FullName },
                { "template", typeof(string).FullName }
            };
            return requiredParams;
        }

        private string GetTemplatePath(Entity entity, string templateFileName)
        {
            string root = entity.Repository.GenerationOutputPath;
            string templatePath = root;
            if (templateFileName.IndexOf("ViewModel") != -1)
            {
                templatePath = GetGenerationPath(root, "ViewModel", entity.Area);
            }
            else if (templateFileName.IndexOf("Map") != -1)
            {
                templatePath = GetGenerationPath(root, "Mapping", entity.Area);
            }
            else if (templateFileName.IndexOf("_CreateTable") != -1)
            {
                templatePath = GetGenerationPath(root, "Script", entity.Area);
            }
            else if (templateFileName == "{0}.Generated.cs")
            {
                templatePath = GetGenerationPath(root, "Model", entity.Area);
            }

            return templatePath;
        }

        private string GetGenerationPath(string root, string typeName, string area)
        {
            string folder = Path.Combine(root, typeName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!String.IsNullOrWhiteSpace(area))
            {
                folder = Path.Combine(folder, area);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

            return folder;
        }
    }
}
