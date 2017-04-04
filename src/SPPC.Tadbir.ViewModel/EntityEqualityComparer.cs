using System;
using System.Collections.Generic;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// Defines methods to support the comparison of view model objects for equality.
    /// </summary>
    /// <remarks>This class only works with view model classes that define an Id property having the type Int32 (int).
    /// This Id property is used as the main basis for comparison.
    /// </remarks>
    public class EntityEqualityComparer<TEntity> : IEqualityComparer<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// Determines whether the specified entities are equal.
        /// </summary>
        /// <param name="x">The first entity to compare.</param>
        /// <param name="y">The second entity to compare.</param>
        /// <returns>true if the specified entities are equal; otherwise, false.</returns>
        public bool Equals(TEntity x, TEntity y)
        {
            bool areEqual = true;
            if (x == null || y == null)
            {
                areEqual = false;
            }
            else
            {
                object xIdObject = Reflector.GetProperty(x, "Id");
                object yIdObject = Reflector.GetProperty(y, "Id");
                if (xIdObject != null && yIdObject != null)
                {
                    areEqual = (Convert.ToInt32(xIdObject) == Convert.ToInt32(yIdObject));
                }
            }

            return areEqual;
        }

        /// <summary>
        /// Returns a hash code for the specified entity.
        /// </summary>
        /// <param name="obj">The entity for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified entity.</returns>
        public int GetHashCode(TEntity obj)
        {
            Verify.ArgumentNotNull(obj, "obj");
            object id = Reflector.GetProperty(obj, "Id");
            int hashCode = (id != null)
                ? Convert.ToInt32(id).GetHashCode()
                : 0;
            return hashCode;
        }
    }
}
