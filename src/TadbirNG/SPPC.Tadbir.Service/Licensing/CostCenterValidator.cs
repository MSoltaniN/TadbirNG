using System;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Resources;

namespace SPPC.Tadbir.Licensing
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
            Verify.ArgumentNotNull(model, nameof(model));
            Verify.TypeIsAssignableFromType(typeof(ITreeEntityView), model.GetType());
            string result = String.Empty;
            var costCenter = model as ITreeEntityView;
            if (config.MaxCostCenterDepth > 0 && costCenter.Level >= config.MaxCostCenterDepth)
            {
                result = _strings.Format(AppStrings.Edition_DepthLimit,
                    config.Name, AppStrings.CostCenter, config.MaxCostCenterDepth.ToString());
            }
            else if (config.MaxCostCenterDepth == 0 && costCenter.Level >= AppConstants.MaxAccountTreeLevel)
            {
                result = _strings.Format(
                    AppStrings.UnsupportedDepth, AppConstants.MaxAccountTreeLevel.ToString());
            }

            return result;
        }

        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
