using System;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Web.Api.Extensions;

namespace SPPC.Tadbir.Web.Api.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyValidator : IModelValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        public CompanyValidator(IEditionRepository repository, IStringLocalizer<AppStrings> strings)
        {
            _repository = repository;
            _strings = strings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public string Validate(object model, EditionConfig config)
        {
            string result = String.Empty;
            if (config.MaxCompanies > 0)
            {
                bool validated = _repository.CanCreteCompanyAsync(config.MaxCompanies).Result;
                if (!validated)
                {
                    result = _strings.Format(AppStrings.Edition_MaxCompanyLimit,
                        config.Name, config.MaxCompanies.ToString());
                }
            }

            return result;
        }

        private readonly IEditionRepository _repository;
        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
