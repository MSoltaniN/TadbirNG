using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ذخیره و بازیابی تنظیمات برنامه را پیاده سازی می کند
    /// </summary>
    public class ConfigRepository : IConfigRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public ConfigRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// تمام تنظیمات موجود برای برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از تمام تنظیمات موجود برای برنامه</returns>
        public async Task<IList<SettingBriefViewModel>> GetAllConfigAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Setting>();
            var allConfig = await repository
                .GetAllAsync();
            return allConfig
                .Select(cfg => _mapper.Map<SettingBriefViewModel>(cfg))
                .ToList();
        }

        /// <summary>
        /// تنظیمات موجود برای کلاس تنظیمات مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TConfig">نوع تنظیمات مورد نیاز</typeparam>
        /// <returns>تنظیمات موجود برای کلاس تنظیمات مشخص شده</returns>
        public async Task<TConfig> GetConfigByTypeAsync<TConfig>()
        {
            var configByType = default(TConfig);
            var repository = _unitOfWork.GetAsyncRepository<Setting>();
            var configItems = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(TConfig).Name);
            var config = configItems.SingleOrDefault();
            if (config != null)
            {
                configByType = _mapper.Map<TConfig>(config);
            }

            return configByType;
        }

        /// <summary>
        /// تمام تنظیمات کاربری موجود برای فرم های لیستی را برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>تنظیمات کاربری موجود برای فرم های لیستی</returns>
        public async Task<IList<ListFormViewConfig>> GetListViewConfigByUserAsync(int userId)
        {
            IList<ListFormViewConfig> configList = new List<ListFormViewConfig>();
            var repository = _unitOfWork.GetAsyncRepository<UserSetting>();
            var viewRepository = _unitOfWork.GetAsyncRepository<Entity>();
            var configItems = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(ListFormViewConfig).Name
                    && cfg.User.Id == userId);
            var userViewIds = configItems.Select(cfg => cfg.EntityViewId.Value);
            var entityViews = await viewRepository.GetByCriteriaAsync(
                ev => !userViewIds.Contains(ev.Id),
                ev => ev.Properties);
            foreach (var view in entityViews)
            {
                var userConfig = new ListFormViewConfig() { ViewId = view.Id };
                foreach (var column in view.Properties)
                {
                    userConfig.ColumnViews.Add(_mapper.Map<ColumnViewConfig>(column));
                }

                configList.Add(userConfig);
            }

            foreach (var config in configItems)
            {
                configList.Add(_mapper.Map<ListFormViewConfig>(config));
            }

            return configList;
        }

        /// <summary>
        /// تنظیمات کاربری موجود برای یکی از فرم های لیستی را برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات کاربری موجود برای یکی از فرم های لیستی</returns>
        public async Task<ListFormViewConfig> GetListViewConfigByUserAsync(int userId, int viewId)
        {
            var userConfig = default(ListFormViewConfig);
            var repository = _unitOfWork.GetAsyncRepository<UserSetting>();
            var items = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(ListFormViewConfig).Name
                    && cfg.User.Id == userId
                    && cfg.EntityView.Id == viewId);
            var config = items.SingleOrDefault();
            if (config == null)
            {
                var viewRepository = _unitOfWork.GetAsyncRepository<Entity>();
                var entityView = await viewRepository.GetByIDAsync(viewId, ev => ev.Properties);
                if (entityView != null)
                {
                    userConfig = new ListFormViewConfig() { ViewId = entityView.Id };
                    foreach (var column in entityView.Properties)
                    {
                        userConfig.ColumnViews.Add(_mapper.Map<ColumnViewConfig>(column));
                    }
                }
            }
            else
            {
                userConfig = _mapper.Map<ListFormViewConfig>(config);
            }

            return userConfig;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
