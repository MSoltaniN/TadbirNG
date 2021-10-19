using System;
using System.Threading.Tasks;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class SystemErrorRepository : RepositoryBase, ISystemErrorRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public SystemErrorRepository(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="error"></param>
        public async Task SaveAsync(SystemErrorViewModel error)
        {
            var repository = UnitOfWork.GetAsyncRepository<SystemError>();
            repository.Insert(Mapper.Map<SystemError>(error));
            await UnitOfWork.CommitAsync();
        }
    }
}
