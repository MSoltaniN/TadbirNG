using System;
using System.IO;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Persistence
{
    public class XmlRepositoryStorage : IRepositoryStorage
    {
        public Storage Storage { get; set; }

        public Repository Load()
        {
            var serializer = new BasicXmlSerializer();
            var repository = serializer.Deserialize(GetFileStoragePath(Storage), typeof(Repository)) as Repository;
            PrepareEntities(repository);
            return repository;
        }

        public void Save(Repository repository)
        {
            var serializer = new BasicXmlSerializer();
            serializer.Serialize(GetFileStoragePath(repository.Store), repository);
        }

        private static void PrepareEntities(Repository repository)
        {
            var entities = new Entity[repository.Entities.Count];
            repository.Entities.CopyTo(entities, 0);
            repository.Entities.Clear();
            Array.ForEach(entities
                .OrderBy(ent => ent.Name)
                .ToArray(), entity =>
                {
                    //entity.Repository = repository;
                    repository.Entities.Add(entity);
                });
        }

        private static string GetFileStoragePath(Storage storage)
        {
            Verify.ArgumentNotNull(storage, "storage");
            return Path.Combine(storage.Connection, storage.Name);
        }
    }
}
