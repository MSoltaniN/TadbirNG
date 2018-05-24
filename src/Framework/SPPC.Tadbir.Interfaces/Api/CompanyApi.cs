using System;
using System.Collections.Generic;
using System.Text;

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
        /// API client URL for all branche items
        /// </summary>
        public const string Companies = "Companies";

        /// <summary>
        /// API server route URL for all branche items
        /// </summary>
        public const string CompaniesUrl = "Companies";

        /// <summary>
        /// API client URL for a branche item specified by unique identifier
        /// </summary>
        public const string Company = "companies/{0}";

        /// <summary>
        /// API server route URL for a branche item specified by unique identifier
        /// </summary>
        public const string CompanyUrl = "companies/{companyId:min(1)}";

        /// <summary>
        /// API client URL for company metadata
        /// </summary>
        public const string CompanyMetadata = "companies/metadata";

        /// <summary>
        /// API server route URL for company metadata
        /// </summary>
        public const string CompanyMetadataUrl = "companies/metadata";
    }
}
