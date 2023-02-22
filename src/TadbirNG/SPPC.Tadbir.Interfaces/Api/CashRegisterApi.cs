using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with cashregisters.
    /// </summary>
    public sealed class CashRegisterApi
    {
        /// <summary>
        /// API client URL for all cashregister items
        /// </summary>
        public const string CashRegisters = "cashRegisters";

        /// <summary>
        /// API server route URL for all cashregister items
        /// </summary>
        public const string CashRegistersUrl = "cashregisters";

        /// <summary>
        /// API client URL for a cashregister item specified by unique identifier
        /// </summary>
        public const string CashRegister = "cashregisters/{0}";

        /// <summary>
        /// API server route URL for a cashregister item specified by unique identifier
        /// </summary>
        public const string CashRegisterUrl = "cashRegisters/{cashRegisterId:min(1)}";
    }
}
