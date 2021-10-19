using System;
using System.Collections.Generic;

namespace SPPC.Framework.Mapper
{
    /// <summary>
    /// Defines a simple contract for mappings between classes.
    /// </summary>
    public interface IDomainMapper
    {
        /// <summary>
        /// Gets an object used for mapping configuration.
        /// </summary>
        object Configuration { get; }

        /// <summary>
        /// Maps source object to another object having a potentially different type.
        /// </summary>
        /// <typeparam name="T">Type of target object</typeparam>
        /// <param name="source">Source object that should be mapped</param>
        /// <returns>The target object mapped from the source object</returns>
        T Map<T>(object source);
    }
}
