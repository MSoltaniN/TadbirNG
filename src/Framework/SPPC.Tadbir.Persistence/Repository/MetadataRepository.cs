using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Domain;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات فراداده ای از دیتابیس را پیاده سازی می کند
    /// </summary>
    public class MetadataRepository : IMetadataRepository
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public MetadataRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای نوع موجودیت مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که فراداده آن مورد نیاز است</typeparam>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<EntityViewModel> GetEntityMetadataAsync<TEntity>()
            where TEntity : IEntity
        {
            return await GetEntityMetadataAsync(typeof(TEntity).Name);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="entityName">نام (شناسه متنی) موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<EntityViewModel> GetEntityMetadataAsync(string entityName)
        {
            var repository = _unitOfWork.GetAsyncRepository<Entity>();
            var entityMetadata = await repository
                .GetByCriteriaAsync(ent => ent.Name == entityName, ent => ent.Properties);
            return entityMetadata
                .Select(ent => _mapper.Map<EntityViewModel>(ent))
                .FirstOrDefault();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="entityId">شناسه عددی موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<EntityViewModel> GetEntityMetadataByIdAsync(int entityId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Entity>();
            var entityMetadata = await repository
                .GetByCriteriaAsync(ent => ent.Id == entityId, ent => ent.Properties);
            return entityMetadata
                .Select(ent => _mapper.Map<EntityViewModel>(ent))
                .FirstOrDefault();
        }

        /// <summary>
        /// اطلاعات نمایشی تمام دستوراتی که در بالاترین سطح ساختار درختی قرار دارند را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از دستورات در بالاترین سطح</returns>
        public async Task<IList<CommandViewModel>> GetTopLevelCommandsAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Command>();
            var topCommands = await repository.GetByCriteriaAsync(
                cmd => cmd.Parent == null, cmd => cmd.Children);
            return topCommands
                .Select(cmd => _mapper.Map<CommandViewModel>(cmd))
                .ToList();
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
