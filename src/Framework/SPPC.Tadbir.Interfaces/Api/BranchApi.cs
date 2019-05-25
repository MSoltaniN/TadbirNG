namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with branches.
    /// </summary>
    public sealed class BranchApi
    {
        /// <summary>
        /// API client URL for branches defined in a company
        /// </summary>
        public const string CompanyBranches = "branches/company/{0}";

        /// <summary>
        /// API server route URL for branches defined in a company
        /// </summary>
        public const string CompanyBranchesUrl = "branches/company/{companyId:min(1)}";

        /// <summary>
        /// API client URL for all branch items
        /// </summary>
        public const string Branches = "branches";

        /// <summary>
        /// API server route URL for all branch items
        /// </summary>
        public const string BranchesUrl = "branches";

        /// <summary>
        /// API client URL for a branch item specified by unique identifier
        /// </summary>
        public const string Branch = "branches/{0}";

        /// <summary>
        /// API server route URL for a branch item specified by unique identifier
        /// </summary>
        public const string BranchUrl = "branches/{branchId:min(1)}";

        /// <summary>
        /// API client URL for all roles who can access a branch item specified by unique identifier
        /// </summary>
        public const string BranchRoles = "branches/{0}/roles";

        /// <summary>
        /// API server route URL for all roles who can access a branch item specified by unique identifier
        /// </summary>
        public const string BranchRolesUrl = "branches/{branchId:min(1)}/roles";
    }
}
