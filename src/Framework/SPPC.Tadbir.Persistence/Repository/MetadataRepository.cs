using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;
using SPPC.Framework.Mapper;
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
            _unitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای نوع موجودیت مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که فراداده آن مورد نیاز است</typeparam>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<ViewViewModel> GetViewMetadataAsync<TEntity>()
            where TEntity : IEntity
        {
            return await GetViewMetadataAsync(typeof(TEntity).Name);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewName">نام (شناسه متنی) موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<ViewViewModel> GetViewMetadataAsync(string viewName)
        {
            var repository = _unitOfWork.GetAsyncRepository<View>();
            var viewMetadata = await repository
                .GetByCriteriaAsync(vu => vu.Name == viewName, vu => vu.Columns);
            return viewMetadata
                .Select(ent => _mapper.Map<ViewViewModel>(ent))
                .FirstOrDefault();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه عددی موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        public async Task<ViewViewModel> GetViewMetadataByIdAsync(int viewId)
        {
            var repository = _unitOfWork.GetAsyncRepository<View>();
            var viewMetadata = await repository
                .GetByCriteriaAsync(vu => vu.Id == viewId, vu => vu.Columns);
            return viewMetadata
                .Select(vu => _mapper.Map<ViewViewModel>(vu))
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
            var topCommands = await repository
                .GetEntityQuery()
                .Include(cmd => cmd.Children)
                    .ThenInclude(cmd => cmd.Children)
                        .ThenInclude(cmd => cmd.Children)
                .Where(cmd => cmd.Parent == null && cmd.TitleKey != "Profile")
                .ToListAsync();
            return topCommands
                .Select(cmd => _mapper.Map<CommandViewModel>(cmd))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی دستورات پیش فرض کاربران را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه دستورات در منوی پیش فرض کاربران</returns>
        public async Task<IList<CommandViewModel>> GetDefaultCommandsAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Command>();
            var profileCommands = await repository.GetSingleByCriteriaAsync(
                cmd => cmd.TitleKey == "Profile", cmd => cmd.Children);
            return profileCommands.Children
                .Select(cmd => _mapper.Map<CommandViewModel>(cmd))
                .ToList();
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
