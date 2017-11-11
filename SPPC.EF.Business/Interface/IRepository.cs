namespace SPPC.Tadbir.Business
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SPPC.Tadbir.DataAccess;

    public interface IRepository<EntityType>
    {
        //Task<List<EntityType>> Get(int fpId, int branchId, GridOption gridOption);

        Task<List<EntityType>> Get(GridOption gridOption);

        Task<EntityType> Get(int id);

        Task<bool> Delete(int id);

        Task<bool> Edit(EntityType entity);

        Task<bool> Insert(EntityType entity);

        Task<int> GetCount();

        Task<int> GetCount(GridOption gridOption);
    }
}
