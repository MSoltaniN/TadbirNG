using System;
using System.Collections.Generic;

namespace SPPC.Framework.Common
{
    /// <summary>
    /// Defines generic data type for a property.
    /// </summary>
    public enum BuiltinType
    {
        /// <summary>
        /// The System.String data type
        /// </summary>
#pragma warning disable CA1720 // Identifier contains type name
        String,

        /// <summary>
        /// The System.Char data type
        /// </summary>
        Char,

        /// <summary>
        /// The System.Byte data type
        /// </summary>
        Byte,

        /// <summary>
        /// The System.SByte data type
        /// </summary>
        SByte,

        /// <summary>
        /// The System.Int16 data type
        /// </summary>
        Int16,

        /// <summary>
        /// The System.UInt16 data type
        /// </summary>
        UInt16,

        /// <summary>
        /// The System.Int32 data type
        /// </summary>
        Int32,

        /// <summary>
        /// The System.UInt32 data type
        /// </summary>
        UInt32,

        /// <summary>
        /// The System.Int64 data type
        /// </summary>
        Int64,

        /// <summary>
        /// The System.UInt64 data type
        /// </summary>
        UInt64,

        /// <summary>
        /// The System.Decimal data type
        /// </summary>
        Decimal,

        /// <summary>
        /// The System.Single data type
        /// </summary>
        Single,

        /// <summary>
        /// The System.Double data type
        /// </summary>
        Double,

        /// <summary>
        /// The System.DateTime data type
        /// </summary>
        DateTime,

        /// <summary>
        /// The System.TimeSpan data type
        /// </summary>
        TimeSpan,

        /// <summary>
        /// The System.Boolean data type
        /// </summary>
        Boolean,

        /// <summary>
        /// The System.Guid data type
        /// </summary>
        Guid
#pragma warning restore CA1720 // Identifier contains type name
    }
}
