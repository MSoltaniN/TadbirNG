using System;
using System.Collections.Generic;
using BabakSoft.Platform.Common;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Defines methods to support the comparison of <see cref="PermissionViewModel"/> objects for equality.
    /// </summary>
    public class PermissionEqualityComparer : IEqualityComparer<PermissionBriefViewModel>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type PermissionBriefViewModel to compare.</param>
        /// <param name="y">The second object of type PermissionBriefViewModel to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(PermissionBriefViewModel x, PermissionBriefViewModel y)
        {
            bool areEqual = true;
            if (x == null || y == null)
            {
                areEqual = false;
            }
            else
            {
                areEqual = ((x.EntityName == y.EntityName) && (x.Flags == y.Flags));
            }

            return areEqual;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The PermissionBriefViewModel for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        public int GetHashCode(PermissionBriefViewModel obj)
        {
            Verify.ArgumentNotNull(obj, "obj");
            return (obj.EntityName.GetHashCode() ^ obj.Flags.GetHashCode());
        }
    }
}
