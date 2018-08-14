using System;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    public abstract class LoggingRepository<TEntity, TEntityView> :
        SecureRepository, ILoggingRepository<TEntity, TEntityView>
        where TEntity : FiscalEntity
        where TEntityView : class, new()
    {
        public LoggingRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, IOperationLogRepository logRepository)
            : base(unitOfWork, mapper)
        {
            _logRepository = logRepository;
        }

        public async Task InsertAsync(IRepository<TEntity> repository, TEntity entity)
        {
            OnAction("Create", null, entity);
            repository.Insert(entity);
            await FinalizeActionAsync();
        }

        public async Task UpdateAsync(IRepository<TEntity> repository, TEntity entity, TEntityView entityView)
        {
            var clone = Mapper.Map<TEntity>(entity);
            OnAction("Edit", clone, null);
            UpdateExisting(entityView, entity);
            Log.AfterState = GetState(entity);
            repository.Update(entity);
            await FinalizeActionAsync();
        }

        public async Task DeleteAsync(IRepository<TEntity> repository, TEntity entity)
        {
            var clone = Mapper.Map<TEntity>(entity);
            OnAction("Delete", clone, null);
            DisconnectEntity(entity);
            repository.Delete(entity);
            await FinalizeActionAsync();
        }

        protected OperationLogViewModel Log { get; private set; }

        protected abstract string GetState(TEntity entity);

        protected abstract void UpdateExisting(TEntityView viewModel, TEntity entity);

        private static OperationLogViewModel GetOperationLog(string action, TEntity entity)
        {
            var log = new OperationLogViewModel()
            {
                Action = action,
                FiscalPeriodId = entity.FiscalPeriodId,
                BranchId = entity.BranchId,
                CompanyId = entity.Branch.Company.Id,
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                Succeeded = true,
                View = typeof(TEntity).Name,
                UserId = 1
            };
            return log;
        }

        private static void DisconnectEntity(TEntity entity)
        {
            var relations = Reflector
                .GetPropertyNames(entity)
                .Where(prop => Reflector.GetPropertyType(entity, prop)
                    .Namespace.StartsWith(ModelNamespace))
                .ToArray();
            Array.ForEach(relations, prop => Reflector.SetProperty(entity, prop, null));
        }

        private void OnAction(string action, TEntity before, TEntity after)
        {
            var entity = before ?? after;
            Log = GetOperationLog(action, entity);
            Log.BeforeState = GetState(before);
            Log.AfterState = GetState(after);
        }

        private async Task FinalizeActionAsync()
        {
            try
            {
                await UnitOfWork.CommitAsync();
                await _logRepository.SaveLogAsync(Log);
            }
            catch (Exception ex)
            {
                Log.Succeeded = false;
                Log.FailReason = ex.Message;
                await _logRepository.SaveLogAsync(Log);
                throw;
            }
        }

        private const string ModelNamespace = "SPPC.Tadbir.Model";
        private readonly IOperationLogRepository _logRepository;
    }
}
