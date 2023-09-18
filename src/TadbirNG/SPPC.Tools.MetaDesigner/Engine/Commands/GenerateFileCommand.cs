using System;
using System.Collections.Generic;
using System.IO;
using SPPC.Framework.Common;
using SPPC.Framework.Utility;
using SPPC.Tools.Model;

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
            var repository = Parameters["object"] as Repository;
            var entity = Parameters["item"] as Entity;
            string fileNameTemplate = (string)Parameters["fileName"];
            var fileName = String.Format(fileNameTemplate, entity.Name);
            var path = !String.IsNullOrWhiteSpace(repository.GenerationOutputPath)
                ? Path.Combine(GetTemplatePath(repository, entity, fileNameTemplate), fileName)
                : fileName;

            var templateType = Type.GetType((string)Parameters["template"]);
            var template = Reflector.Instantiate(templateType, repository, entity) as ITextTemplate;
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

        private string GetTemplatePath(Repository repository, Entity entity, string templateFileName)
        {
            string root = repository.GenerationOutputPath;
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
