using System;
using System.Configuration;
using System.IO;
using System.Linq;
using BabakSoft.Platform.Metadata;
using BabakSoft.Platform.Persistence;
using SPPC.Framework.Common;
using SPPC.Framework.Tools.ProjectCLI.Templates;

namespace SPPC.Framework.Tools.ProjectCLI
{
    public class GenerateCsModelCommand : ICliCommand
    {
        public GenerateCsModelCommand(params string[] args)
        {
            if (args.Length < 1)
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Insufficient arguments were provided (needed 1).", "args");
            }

            _entities = args[0].Split(',');
        }

        public void Execute()
        {
            string csModelPath = ConfigurationManager.AppSettings["CsModelPath"];
            string csViewModelPath = ConfigurationManager.AppSettings["CsViewModelPath"];
            string csPersistPath = ConfigurationManager.AppSettings["CsPersistPath"];
            string xmlRepoPath = ConfigurationManager.AppSettings["XmlRepoPath"];
            var repository = LoadXmlMetadataRepository(xmlRepoPath);
            foreach (var entityName in _entities)
            {
                var entity = repository.Entities
                    .Where(ent => ent.Name == entityName)
                    .FirstOrDefault();
                if (entity == null)
                {
                    Console.WriteLine(
                        "WARNING: Skipped entity '{0}' becuase it does not exist in XML repository.", entityName);
                    continue;
                }

                GeneratePoco(entity, csModelPath);
                GenerateViewModel(entity, csViewModelPath);
                GenerateEFCoreMapping(entity, csPersistPath);
            }
        }

        private Repository LoadXmlMetadataRepository(string path)
        {
            var serializer = new BasicXmlSerializer();
            var repository = serializer.Deserialize(path, typeof(Repository)) as Repository;
            Array.ForEach(repository.Entities.ToArray(), entity => entity.Repository = repository);
            return repository;
        }

        private void GeneratePoco(Entity entity, string directory)
        {
            string fileName = String.Format("{0}.Generated.cs", entity.Name);
            string path = !String.IsNullOrEmpty(entity.Area)
                ? Path.Combine(directory, entity.Area, fileName)
                : Path.Combine(directory, fileName);

            Console.WriteLine("Generating POCO class for entity '{0}' ...", entity.Name);
            var template = new CsPocoFromXmlMetadata(entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private void GenerateViewModel(Entity entity, string directory)
        {
            string fileName = String.Format("{0}ViewModel.Generated.cs", entity.Name);
            string path = !String.IsNullOrEmpty(entity.Area)
                ? Path.Combine(directory, entity.Area, fileName)
                : Path.Combine(directory, fileName);

            Console.WriteLine("Generating view model class for entity '{0}' ...", entity.Name);
            var template = new CsViewModelFromMetadata(entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private void GenerateEFCoreMapping(Entity entity, string directory)
        {
            string fileName = String.Format("{0}Map.Generated.cs", entity.Name);
            string path = !String.IsNullOrEmpty(entity.Area)
                ? Path.Combine(directory, "Mapping", entity.Area, fileName)
                : Path.Combine(directory, "Mapping", fileName);

            Console.WriteLine("Generating EF Core mapping class for entity '{0}' ...", entity.Name);
            var template = new CsFluentMappingFromMetadata(entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private string[] _entities;
    }
}
