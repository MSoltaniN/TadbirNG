using System;
using System.Configuration;
using System.IO;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesigner.Commands
{
    public class GenerateCsModelCommand : ICommand
    {
        public GenerateCsModelCommand(CrudWizardModel model)
        {
            _model = model;
        }

        public void Execute()
        {
            var repository = _model.EntityInfo.Repository;
            var entity = _model.EntityInfo.Entity;
            if (_model.Options.HasModel)
            {
                GeneratePoco(repository, entity);
            }
            if (_model.Options.HasViewModel)
            {
                GenerateViewModel(repository, entity);
            }
            if (_model.Options.HasDbMapping)
            {
                GenerateEFCoreMapping(repository, entity);
            }
            if (_model.Options.HasDbScript)
            {
                GenerateSqlScript(repository, entity);
            }
        }

        private static void GeneratePoco(Repository repository, Entity entity)
        {
            string csModelPath = ConfigurationManager.AppSettings["CsModelPath"];
            string fileName = String.Format("{0}.Generated.cs", entity.Name);
            string path = GetInitializedFilePath(csModelPath, entity.Area, fileName);

            var template = new CsPocoFromXmlMetadata(repository, entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private static void GenerateViewModel(Repository repository, Entity entity)
        {
            string csViewModelPath = ConfigurationManager.AppSettings["CsViewModelPath"];
            string fileName = String.Format("{0}ViewModel.Generated.cs", entity.Name);
            string path = GetInitializedFilePath(csViewModelPath, entity.Area, fileName);

            var template = new CsViewModelFromMetadata(repository, entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private static void GenerateEFCoreMapping(Repository repository, Entity entity)
        {
            string csPersistPath = Path.Combine(ConfigurationManager.AppSettings["CsPersistPath"], "Mapping");
            string fileName = String.Format("{0}Map.Generated.cs", entity.Name);
            string path = GetInitializedFilePath(csPersistPath, entity.Area, fileName);

            var template = new CsFluentMappingFromMetadata(repository, entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private static void GenerateSqlScript(Repository repository, Entity entity)
        {
            string codeGenPath = ConfigurationManager.AppSettings["CodeGenPath"];
            string fileName = "CreateDbObjects.Generated.sql";
            string path = Path.Combine(codeGenPath, fileName);

            var entities = new Entity[] { entity };
            var template = new SqlCreateTableFromMetadata(repository, entities);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private static string GetInitializedFilePath(string root, string area, string fileName)
        {
            if (String.IsNullOrEmpty(area))
            {
                return Path.Combine(root, fileName);
            }
            else
            {
                string directory = Path.Combine(root, area);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                return Path.Combine(directory, fileName);
            }
        }

        private readonly CrudWizardModel _model;
    }
}
