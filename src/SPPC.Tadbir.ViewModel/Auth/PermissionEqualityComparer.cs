using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Defines methods to support the comparison of <see cref="PermissionViewModel"/> objects for equality.
    /// </summary>
    public class PermissionEqualityComparer : IEqualityComparer<PermissionViewModel>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type PermissionViewModel to compare.</param>
        /// <param name="y">The second object of type PermissionViewModel to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(PermissionViewModel x, PermissionViewModel y)
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
        /// <param name="obj">The PermissionViewModel for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        public int GetHashCode(PermissionViewModel obj)
        {
            Verify.ArgumentNotNull(obj, "obj");
            return obj.Id.GetHashCode();
        }
    }
}
