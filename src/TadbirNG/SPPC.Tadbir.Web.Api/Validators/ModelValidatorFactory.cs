using System;
using Microsoft.Extensions.DependencyInjection;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Web.Api.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class ModelValidatorFactory : IModelValidatorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public ModelValidatorFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IModelValidator Create(EditionLimit limit)
        {
            IModelValidator validator = default;
            switch (limit)
            {
                case EditionLimit.CompanyCount:
                    validator = _provider.GetService<CompanyValidator>();
                    break;
                case EditionLimit.BranchCountAndDepth:
                    validator = _provider.GetService<BranchValidator>();
                    break;
                case EditionLimit.AccountDepth:
                    validator = _provider.GetService<AccountValidator>();
                    break;
                case EditionLimit.DetailAccountDepth:
                    validator = _provider.GetService<DetailAccountValidator>();
                    break;
                case EditionLimit.CostCenterDepth:
                    validator = _provider.GetService<CostCenterValidator>();
                    break;
                case EditionLimit.ProjectDepth:
                    validator = _provider.GetService<ProjectValidator>();
                    break;
            }

            return validator;
        }

        private readonly IServiceProvider _provider;
    }
}
