using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// Defines methods to support the comparison of <see cref="IEntity"/> objects for equality.
    /// </summary>
    /// <remarks>The Id property is used as the main basis for comparison.</remarks>
    public class EntityEqualityComparer : IEqualityComparer<IEntity>
    {
        /// <summary>
        /// Determines whether the specified entities are equal.
        /// </summary>
        /// <param name="x">The first entity to compare.</param>
        /// <param name="y">The second entity to compare.</param>
        /// <returns>true if the specified entities are equal; otherwise, false.</returns>
        public bool Equals(IEntity x, IEntity y)
        {
            bool areEqual = false;
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
        /// Returns a hash code for the specified entity.
        /// </summary>
        /// <param name="obj">The entity for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified entity.</returns>
        public int GetHashCode(IEntity obj)
        {
            Verify.ArgumentNotNull(obj, "obj");
            return obj.Id.GetHashCode();
        }
    }
}
