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
            string result = String.Empty;
            if (config.MaxProjectDepth > 0)
            {
                var project = model as ITreeEntityView;
                if (project.Level >= config.MaxProjectDepth)
                {
                    result = _strings.Format(AppStrings.Edition_DepthLimit,
                        config.Name, AppStrings.Project, config.MaxProjectDepth.ToString());
                }
            }

            return result;
        }

        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
