using System;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with cash registers.
    /// </summary>
    public sealed class CashRegisterApi
    {
        /// <summary>
        /// API client URL for all active cash register items
        /// </summary>
        public const string CashRegisters = "cash-registers";

        /// <summary>
        /// API server route URL for all active cash register items
        /// </summary>
        public const string CashRegistersUrl = "cash-registers";

        /// <summary>
        /// API client URL for all inactive cash register items
        /// </summary>
        public const string InactiveCashRegisters = "cash-registers/inactive";

        /// <summary>
        /// API server route URL for all inactive cash register items
        /// </summary>
        public const string InactiveCashRegistersUrl = "cash-registers/inactive";

        /// <summary>
        /// API client URL for all cash register items
        /// </summary>
        public const string AllCashRegisters = "cash-registers/all";

        /// <summary>
        /// API server route URL for all cash register items
        /// </summary>
        public const string AllCashRegistersUrl = "cash-registers/all";

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
        public const string CashRegisterUsers = "cash-registers/{0}/users";

        /// <summary>
        /// API server route URL for all Users assigned to the cash register item specified by unique identifier
        /// </summary>
        public const string CashRegisterUsersUrl = "cash-registers/{cashRegisterId:min(1)}/users";

        /// <summary>
        /// API client URL for marking an active cash register as inactive
        /// </summary>
        public const string DeactivateCashRegister = "cash-registers/{0}/deactivate";

        /// <summary>
        /// API server route URL for marking an active cash register as inactive
        /// </summary>
        public const string DeactivateCashRegisterUrl = "cash-registers/{cashRegisterId:int}/deactivate";

        /// <summary>
        /// API client URL for marking an inactive cash register as active
        /// </summary>
        public const string ReactivateCashRegister = "cash-registers/{0}/reactivate";

        /// <summary>
        /// API server route URL for marking an inactive cash register as active
        /// </summary>
        public const string ReactivateCashRegisterUrl = "cash-registers/{cashRegisterId:int}/reactivate";
    }
}
