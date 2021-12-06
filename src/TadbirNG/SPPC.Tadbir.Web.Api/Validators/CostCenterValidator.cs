using System;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Web.Api.Extensions;

namespace SPPC.Tadbir.Web.Api.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class CostCenterValidator : IModelValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strings"></param>
        public CostCenterValidator(IStringLocalizer<AppStrings> strings)
        {
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
            if (config.MaxCostCenterDepth > 0)
            {
                var costCenter = model as ITreeEntityView;
                if (costCenter.Level >= config.MaxCostCenterDepth)
                {
                    result = _strings.Format(AppStrings.Edition_DepthLimit,
                        config.Name, AppStrings.CostCenter, config.MaxCostCenterDepth.ToString());
                }
            }

            return result;
        }

        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
