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
    public class DetailAccountValidator : IModelValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strings"></param>
        public DetailAccountValidator(IStringLocalizer<AppStrings> strings)
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
            var detailAccount = model as ITreeEntityView;
            if (config.MaxDetailAccountDepth > 0 && detailAccount.Level >= config.MaxDetailAccountDepth)
            {
                result = _strings.Format(AppStrings.Edition_DepthLimit,
                    config.Name, AppStrings.DetailAccount, config.MaxDetailAccountDepth.ToString());
            }
            else if (config.MaxDetailAccountDepth == 0 && detailAccount.Level >= AppConstants.MaxAccountTreeLevel)
            {
                result = _strings.Format(
                    AppStrings.UnsupportedDepth, AppConstants.MaxAccountTreeLevel.ToString());
            }

            return result;
        }

        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
