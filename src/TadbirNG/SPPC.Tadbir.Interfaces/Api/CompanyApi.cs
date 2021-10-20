using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with branches.
    /// </summary>
    public sealed class CompanyApi
    {
        /// <summary>
        /// API client URL for all companies
        /// </summary>
        public const string Companies = "companies";

        /// <summary>
        /// API server route URL for all companies
        /// </summary>
        public const string CompaniesUrl = "companies";

        /// <summary>
        /// API client URL for a company specified by unique identifier
        /// </summary>
        public const string Company = "companies/{0}";

        /// <summary>
        /// API server route URL for a company specified by unique identifier
        /// </summary>
        public const string CompanyUrl = "companies/{companyId:min(1)}";

        /// <summary>
        /// API client URL for roles that can access a company specified by unique identifier
        /// </summary>
        public const string CompanyRoles = "companies/{0}/roles";

        /// <summary>
        /// API server route URL for roles that can access a company specified by unique identifier
        /// </summary>
        public const string CompanyRolesUrl = "companies/{companyId:min(1)}/roles";
    }
}
