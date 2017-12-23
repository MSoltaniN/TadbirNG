using System;
using System.Collections.Generic;

namespace SPPC.Framework.Common
{
    /// <summary>
    /// Defines a method for comparing two interface types.
    /// </summary>
    public class InterfaceComparer : IComparer<Type>
    {
        #region IComparer<Type> Members

        /// <summary>
        /// Compares two types and determines the inheritance order of them.
        /// </summary>
        /// <param name="x">The left type of the comparison operator.</param>
        /// <param name="y">The right type of the comparison operator.</param>
        /// <returns>-1 if an object having the type y can be assigned to an object having the type x,
        /// 1 if an object having the type x can be assigned to an object having the type y, otherwise
        /// returns 0.</returns>
        /// <remarks> This comparison follows the same semantic convention as integral types;
        /// i.e. if x is less than y returns -1 and if x is greater than y returns 1. A type
        /// higher in the inheritance hierarchy is supposed to be greater than a type lower
        /// in the inheritance hierarchy. If two types are unrelated (don't belong to the same
        /// inheritance hierarchy), or if two types are equal, then this method returns 0;
        /// With this logic, the clients cannot rely on this method for type equality.</remarks>
        public int Compare(Type x, Type y)
        {
            Verify.ArgumentNotNull(x, "x");
            Verify.ArgumentNotNull(y, "y");

            int result;
            if (x == y)
            {
                result = 0;
            }
            else if (x.IsAssignableFrom(y))
            {
                result = -1;
            }
            else if (y.IsAssignableFrom(x))
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        #endregion
    }
}
