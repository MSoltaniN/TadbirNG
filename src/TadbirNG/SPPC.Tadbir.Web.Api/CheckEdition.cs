using System.IO;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Web.Api
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
        public CheckEdition(IModelValidatorFactory factory)
        {
            _factory = factory;
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

        private static EditionConfig GetEditionConfig()
        {
            using var reader = new StreamReader(
                typeof(Program).Assembly.GetManifestResourceStream(_configUri));
            string jsonConfig = reader.ReadToEnd();
            return JsonHelper.To<EditionConfig>(jsonConfig);
        }

#if DEBUG
        private static readonly string _configUri = "SPPC.Tadbir.Web.Api.wwwroot.edition.Development.json";
#else
        private static readonly string _configUri = "SPPC.Tadbir.Web.Api.wwwroot.edition";
#endif
        private readonly IModelValidatorFactory _factory;
    }
}
