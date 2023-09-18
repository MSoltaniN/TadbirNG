using System;
using System.IO;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Tools.Model;

namespace SPPC.Tools.Persistence
{
    public class XmlRepositoryStorage : IRepositoryStorage
    {
        public Storage Storage { get; set; }

        public Repository Load()
        {
            var serializer = new BasicXmlSerializer();
            var repository = serializer.Deserialize(GetFileStoragePath(Storage), typeof(Repository)) as Repository;
            RepositoryHelper.SortEntitiesByName(repository);
            return repository;
        }

        public void Save(Repository repository)
        {
            var serializer = new BasicXmlSerializer();
            serializer.Serialize(GetFileStoragePath(repository.Store), repository);
        }

        private static string GetFileStoragePath(Storage storage)
        {
            Verify.ArgumentNotNull(storage, "storage");
            return Path.Combine(storage.Connection, storage.Name);
        }
    }
}
