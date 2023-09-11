using System;
using System.Configuration;
using System.IO;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Persistence;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesignerCli
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
            string codeGenPath = ConfigurationManager.AppSettings["CodeGenPath"];
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

                Console.WriteLine("Generating model layer classes for entity '{0}' ...", entity.Name);
                GeneratePoco(repository, entity, csModelPath);
                GenerateViewModel(repository, entity, csViewModelPath);
                GenerateEFCoreMapping(repository, entity, csPersistPath);
                Console.WriteLine();
            }

            GenerateSqlScript(repository, _entities, codeGenPath);
        }

        private Repository LoadXmlMetadataRepository(string path)
        {
            var serializer = new BasicXmlSerializer();
            var repository = serializer.Deserialize(path, typeof(Repository)) as Repository;
            //Array.ForEach(repository.Entities.ToArray(), entity => entity.Repository = repository);
            return repository;
        }

        private void GeneratePoco(Repository repository, Entity entity, string directory)
        {
            string fileName = String.Format("{0}.Generated.cs", entity.Name);
            string path = !String.IsNullOrEmpty(entity.Area)
                ? Path.Combine(directory, entity.Area, fileName)
                : Path.Combine(directory, fileName);

            Console.WriteLine("    => POCO class in Model project...");
            var template = new CsPocoFromXmlMetadata(repository, entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private void GenerateViewModel(Repository repository, Entity entity, string directory)
        {
            string fileName = String.Format("{0}ViewModel.Generated.cs", entity.Name);
            string path = !String.IsNullOrEmpty(entity.Area)
                ? Path.Combine(directory, entity.Area, fileName)
                : Path.Combine(directory, fileName);

            Console.WriteLine("    => View model class in ViewModel project...");
            var template = new CsViewModelFromMetadata(repository, entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private void GenerateEFCoreMapping(Repository repository, Entity entity, string directory)
        {
            string fileName = String.Format("{0}Map.Generated.cs", entity.Name);
            string path = !String.IsNullOrEmpty(entity.Area)
                ? Path.Combine(directory, "Mapping", entity.Area, fileName)
                : Path.Combine(directory, "Mapping", fileName);

            Console.WriteLine("    => EF Core mapping class in Persistence project...");
            var template = new CsFluentMappingFromMetadata(repository, entity);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private void GenerateSqlScript(Repository repository, string[] entityNames, string directory)
        {
            string fileName = "CreateDbObjects.Generated.sql";
            string path = Path.Combine(directory, fileName);

            Console.WriteLine("Generating SQL CREATE TABLE scripts in path '{0}' ...", directory);
            var entities = repository.Entities.Where(entity => entityNames.Contains(entity.Name)).ToArray();
            var template = new SqlCreateTableFromMetadata(repository, entities);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private readonly string[] _entities;
    }
}
