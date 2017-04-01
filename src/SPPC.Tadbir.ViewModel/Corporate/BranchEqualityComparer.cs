using System;
using System.Collections.Generic;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.ViewModel.Corporate
{
    /// <summary>
    /// Defines methods to support the comparison of <see cref="BranchViewModel"/> objects for equality.
    /// </summary>
    public class BranchEqualityComparer : IEqualityComparer<BranchViewModel>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type BranchViewModel to compare.</param>
        /// <param name="y">The second object of type BranchViewModel to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(BranchViewModel x, BranchViewModel y)
        {
            bool areEqual = true;
            if (x == null || y == null)
            {
                areEqual = false;
            }
            else
            {
                areEqual = (x.Id == y.Id);
            }

            return areEqual;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The BranchViewModel for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        public int GetHashCode(BranchViewModel obj)
        {
            Verify.ArgumentNotNull(obj, "obj");
            return obj.Id.GetHashCode();
        }
    }
}
