using System;
using System.Threading.Tasks;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence
{
    public interface ILoggingRepository<TEntity, TEntityView>
        where TEntity : class, IFiscalEntity
        where TEntityView : class, new()
    {
        Task InsertAsync(IRepository<TEntity> repository, TEntity entity);

        Task UpdateAsync(IRepository<TEntity> repository, TEntity entity, TEntityView entityView);

        Task DeleteAsync(IRepository<TEntity> repository, TEntity entity);
    }
}
