using System;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.Web.Api.Extensions;

namespace SPPC.Tadbir.Web.Api.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class BranchValidator : IModelValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        public BranchValidator(IEditionRepository repository, IStringLocalizer<AppStrings> strings)
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
            if (config.MaxBranches > 0 && config.MaxBranchDepth > 0)
            {
                var branch = model as BranchViewModel;
                bool validated = _repository.CanCreteBranchAsync(config.MaxBranches).Result;
                if (!validated)
                {
                    result = _strings.Format(AppStrings.Edition_MaxBranchLimit,
                        config.Name, config.MaxBranches.ToString());
                }
                else if (branch.Level >= config.MaxBranchDepth)
                {
                    result = _strings.Format(AppStrings.Edition_DepthLimit,
                        config.Name, AppStrings.Branch, config.MaxBranchDepth.ToString());
                }
            }

            return result;
        }

        private readonly IEditionRepository _repository;
        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
