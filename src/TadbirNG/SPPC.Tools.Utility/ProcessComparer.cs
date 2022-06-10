using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SPPC.Tools.Utility
{
    public class ProcessComparer : IEqualityComparer<Process>
    {
        public bool Equals(Process x, Process y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Id.Equals(y.Id);
        }

        public int GetHashCode([DisallowNull] Process process)
        {
            return process.Id.GetHashCode();
        }
    }
}
