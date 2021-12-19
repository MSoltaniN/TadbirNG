using System.IO;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Web.Api;

namespace SPPC.Tadbir.Licensing
{
    /// <summary>
    /// عملیات مورد نیاز برای کنترل ویرایش برنامه را پیاده سازی می کند
    /// </summary>
    public class CheckEdition : ICheckEdition
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="pathProvider">مسیرهای فایل های کاربردی مورد نیاز سرویس وب را فراهم می کند</param>
        public CheckEdition(IModelValidatorFactory factory, IApiPathProvider pathProvider)
        {
            _factory = factory;
            _pathProvider = pathProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public string ValidateNewModel(object model, EditionLimit limit)
        {
            var validator = _factory.Create(limit);
            var config = GetEditionConfig();
            return validator.Validate(model, config);
        }

        private EditionConfig GetEditionConfig()
        {
            using var reader = new StreamReader(
                typeof(Program).Assembly.GetManifestResourceStream(_pathProvider.Edition));
            string jsonConfig = reader.ReadToEnd();
            return JsonHelper.To<EditionConfig>(jsonConfig);
        }

        private readonly IModelValidatorFactory _factory;
        private readonly IApiPathProvider _pathProvider;
    }
}
