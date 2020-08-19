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
        /// API client URL for companies defined in a company
        /// </summary>
        public const string CompanyChildren = "companies/company/{0}";

        /// <summary>
        /// API server route URL for companies defined in a company
        /// </summary>
        public const string CompanyChildrenUrl = "companies/company/{companyId:min(1)}";

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
    }
}
