using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    ///
    /// </summary>
    public class FullAccountCodeBranch
    {
        /// <summary>
        ///
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string DetailAccountFullCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CostCenterFullCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ProjectFullCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is FullAccountCodeBranch codeBranch)
            {
                return codeBranch.AccountFullCode == AccountFullCode
                    && codeBranch.DetailAccountFullCode == DetailAccountFullCode
                    && codeBranch.CostCenterFullCode == CostCenterFullCode
                    && codeBranch.ProjectFullCode == ProjectFullCode
                    && codeBranch.BranchName == BranchName;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + (!String.IsNullOrEmpty(AccountFullCode) ? AccountFullCode.GetHashCode() : 1);
                hash = (hash * 23) + (!String.IsNullOrEmpty(DetailAccountFullCode) ? DetailAccountFullCode.GetHashCode() : 1);
                hash = (hash * 23) + (!String.IsNullOrEmpty(CostCenterFullCode) ? CostCenterFullCode.GetHashCode() : 1);
                hash = (hash * 23) + (!String.IsNullOrEmpty(ProjectFullCode) ? ProjectFullCode.GetHashCode() : 1);
                hash = (hash * 23) + (!String.IsNullOrEmpty(BranchName) ? BranchName.GetHashCode() : 1);
                return hash;
            }
        }
    }
}
