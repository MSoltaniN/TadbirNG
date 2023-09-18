using SPPC.Tools.Model;

namespace SPPC.Tools.Persistence
{
    public interface IRepositoryStorage
    {
        Storage Storage { get; set; }

        Repository Load();

        void Save(Repository repository);
    }
}
