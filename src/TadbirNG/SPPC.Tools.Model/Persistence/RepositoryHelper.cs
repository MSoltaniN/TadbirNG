using System;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.Persistence
{
    public class RepositoryHelper
    {
        public static Repository LoadFromFile(string path)
        {
            var repositoryStorage = RepositoryStorageFactory.GetStorage(
                StorageFactory.CreateFromFile(path));
            return repositoryStorage.Load();
        }

        public static void SortEntitiesByName(Repository repository)
        {
            Verify.ArgumentNotNull(repository, nameof(repository));
            var entities = new Entity[repository.Entities.Count];
            repository.Entities.CopyTo(entities, 0);
            repository.Entities.Clear();
            Array.ForEach(entities
                .OrderBy(ent => ent.Name)
                .ToArray(), entity => repository.Entities.Add(entity));
        }
    }
}
