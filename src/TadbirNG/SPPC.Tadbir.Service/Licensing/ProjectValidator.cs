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
    public class ProjectValidator : IModelValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strings"></param>
        public ProjectValidator(IStringLocalizer<AppStrings> strings)
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
            var account = model as ITreeEntityView;
            if (config.MaxProjectDepth > 0 && account.Level >= config.MaxProjectDepth)
            {
                result = _strings.Format(AppStrings.Edition_DepthLimit,
                    config.Name, AppStrings.Project, config.MaxProjectDepth.ToString());
            }
            else if (config.MaxProjectDepth == 0 && account.Level >= AppConstants.MaxAccountTreeLevel)
            {
                result = _strings.Format(
                    AppStrings.UnsupportedDepth, AppConstants.MaxAccountTreeLevel.ToString());
            }

            return result;
        }

        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
