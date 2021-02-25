using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal class FullCodeBranch
    {
        internal string FullCode { get; set; }

        internal string BranchName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is FullCodeBranch codeBranch)
            {
                return codeBranch.FullCode == FullCode
                    && codeBranch.BranchName == BranchName;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + (!String.IsNullOrEmpty(FullCode) ? FullCode.GetHashCode() : 1);
                hash = (hash * 23) + (!String.IsNullOrEmpty(BranchName) ? BranchName.GetHashCode() : 1);
                return hash;
            }
        }
    }
}
