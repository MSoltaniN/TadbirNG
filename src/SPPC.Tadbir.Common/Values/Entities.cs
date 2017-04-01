using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides localized names for entities (in Persian).
    /// </summary>
    public sealed class Entities
    {
        private Entities()
        {
        }

        /// <summary>
        /// Localized name of Fiscal Period entity.
        /// </summary>
        public const string FiscalPeriod = "دوره مالی";

        /// <summary>
        /// Localized name of Branch entity (singular form).
        /// </summary>
        public const string Branch = "شعبه";

        /// <summary>
        /// Localized name of Branch entity (plural form).
        /// </summary>
        public const string Branches = "شعب";

        /// <summary>
        /// Localized name of Company entity.
        /// </summary>
        public const string Company = "شرکت";

        /// <summary>
        /// Localized name of Account entity (long form).
        /// </summary>
        public const string AccountLongName = "سرفصل حسابداری";

        /// <summary>
        /// Localized name of Account entity.
        /// </summary>
        public const string Account = "حساب";

        /// <summary>
        /// Localized name of Role entity (singular form).
        /// </summary>
        public const string Role = "نقش";

        /// <summary>
        /// Localized name of Role entity (plural form).
        /// </summary>
        public const string Roles = "نقش ها";

        /// <summary>
        /// Localized name of Role entity (unknown).
        /// </summary>
        public const string ARole = "نقشی";
    }
}
