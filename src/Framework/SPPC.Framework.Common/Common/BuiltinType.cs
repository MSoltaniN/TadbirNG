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
        Text,

        /// <summary>
        /// The System.Char data type
        /// </summary>
        Character,

        /// <summary>
        /// The System.Byte data type
        /// </summary>
        TinyNumber,

        /// <summary>
        /// The System.SByte data type
        /// </summary>
        SignedTinyNumber,

        /// <summary>
        /// The System.Int16 data type
        /// </summary>
        SmallNumber,

        /// <summary>
        /// The System.UInt16 data type
        /// </summary>
        UnsignedSmallNumber,

        /// <summary>
        /// The System.Int32 data type
        /// </summary>
        Number,

        /// <summary>
        /// The System.UInt32 data type
        /// </summary>
        UnsignedNumber,

        /// <summary>
        /// The System.Int64 data type
        /// </summary>
        BigNumber,

        /// <summary>
        /// The System.UInt64 data type
        /// </summary>
        UnsignedBigNumber,

        /// <summary>
        /// The System.Decimal data type
        /// </summary>
        DecimalNumber,

        /// <summary>
        /// The System.Single data type
        /// </summary>
        SinglePrecision,

        /// <summary>
        /// The System.Double data type
        /// </summary>
        DoublePrecision,

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
        SystemGuid
    }
}
