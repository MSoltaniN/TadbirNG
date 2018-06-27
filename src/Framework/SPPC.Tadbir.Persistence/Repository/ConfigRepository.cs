using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Model.Config;
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
        /// تنظیمات موجود برای مدیریت ارتباطات بین مولفه های بردار حساب را خوانده و برمی گرداند
        /// </summary>
        /// <returns></returns>
        public async Task<RelationsConfig> GetRelationsConfigAsync()
        {
            var relationsConfig = default(RelationsConfig);
            var repository = _unitOfWork.GetAsyncRepository<Setting>();
            var configItems = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(RelationsConfig).Name);
            var config = configItems.SingleOrDefault();
            if (config != null)
            {
                relationsConfig = _mapper.Map<RelationsConfig>(config);
            }

            return relationsConfig;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
    }
}
