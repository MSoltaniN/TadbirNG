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
    }
}
