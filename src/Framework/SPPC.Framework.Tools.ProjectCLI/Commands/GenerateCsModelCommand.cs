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

                string fileName = String.Format("{0}.Generated.cs", entity.Name);
                string path = !String.IsNullOrEmpty(entity.Area)
                    ? Path.Combine(csModelPath, entity.Area, fileName)
                    : Path.Combine(csModelPath, fileName);

                Console.WriteLine("Generating POCO class for entity '{0}' ...", entity.Name);
                var template = new CsPocoFromXmlMetadata(entity);
                string transformed = template.TransformText();
                File.WriteAllText(path, transformed);
            }
        }

        private Repository LoadXmlMetadataRepository(string path)
        {
            var serializer = new BasicXmlSerializer();
            var repository = serializer.Deserialize(path, typeof(Repository)) as Repository;
            Array.ForEach(repository.Entities.ToArray(), entity => entity.Repository = repository);
            return repository;
        }

        private string[] _entities;
    }
}
