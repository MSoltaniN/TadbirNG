using System.IO;
using System.Text;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;

namespace SPPC.Tools.Persistence
{
    public class JsonRepositoryStorage : IRepositoryStorage
    {
        public Storage Storage { get; set; }

        public Repository Load()
        {
            var path = Path.Combine(Storage.Connection, Storage.Name);
            var repository = JsonHelper.To<Repository>(
                File.ReadAllText(path, Encoding.UTF8));
            return repository;
        }

        public void Save(Repository repository)
        {
            var storage = repository.Store;
            var path = Path.Combine(storage.Connection, storage.Name);
            File.WriteAllText(path, JsonHelper.From(repository));
        }
    }
}
