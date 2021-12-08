using System;
using Microsoft.Extensions.Localization;
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
            string result = String.Empty;
            if (config.MaxDetailAccountDepth > 0)
            {
                var detailAccount = model as ITreeEntityView;
                if (detailAccount.Level >= config.MaxDetailAccountDepth)
                {
                    result = _strings.Format(AppStrings.Edition_DepthLimit,
                        config.Name, AppStrings.DetailAccount, config.MaxDetailAccountDepth.ToString());
                }
            }

            return result;
        }

        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
