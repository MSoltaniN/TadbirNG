using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with cash registers.
    /// </summary>
    public sealed class CashRegisterApi
    {
        /// <summary>
        /// API client URL for all cash register items
        /// </summary>
        public const string CashRegisters = "cash-registers";

        /// <summary>
        /// API server route URL for all cash register items
        /// </summary>
        public const string CashRegistersUrl = "cash-registers";

        /// <summary>
        /// API client URL for a cash register item specified by unique identifier
        /// </summary>
        public const string CashRegister = "cash-registers/{0}";

        /// <summary>
        /// API server route URL for a cash register item specified by unique identifier
        /// </summary>
        public const string CashRegisterUrl = "cash-registers/{cashRegisterId:min(1)}";

        /// <summary>
        /// API client URL for all users assigned to the cash register item specified by unique identifier
        /// </summary>
        public const string UserCashRegisters = "cash-registers/{0}/users";

        /// <summary>
        /// API server route URL for all Users assigned to the cash register item specified by unique identifier
        /// </summary>
        public const string UserCashRegistersUrl = "cash-registers/{cashregisterId:min(1)}/users";
    }
}
