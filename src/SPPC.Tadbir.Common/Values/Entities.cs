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
        /// Localized name of Account entity (plural form).
        /// </summary>
        public const string Accounts = "حساب ها";

        /// <summary>
        /// Localized name of Account entity (unknown).
        /// </summary>
        public const string AnAccount = "حسابی";

        /// <summary>
        /// Localized name of Transaction entity (long form).
        /// </summary>
        public const string TransactionLongName = "سند مالی";

        /// <summary>
        /// Localized name of Transaction entity.
        /// </summary>
        public const string Transaction = "سند";

        /// <summary>
        /// Localized name of Transaction entity (unknown).
        /// </summary>
        public const string ATransaction = "سندی";

        /// <summary>
        /// Localized name of Transaction entity (plural form).
        /// </summary>
        public const string Transactions = "اسناد";

        /// <summary>
        /// Localized text for the caption for transaction lines (articles)
        /// </summary>
        public const string TransactionLines = "آرتیکل های سند";

        /// <summary>
        /// Localized name of Article.
        /// </summary>
        public const string Article = "آرتیکل";

        /// <summary>
        /// Localized name of Article (plural form).
        /// </summary>
        public const string Articles = "آرتیکل ها";

        /// <summary>
        /// Localized name of Article (unknown).
        /// </summary>
        public const string AnArticle = "آرتیکلی";

        /// <summary>
        /// Localized name of User entity (singular form).
        /// </summary>
        public const string User = "کاربر";

        /// <summary>
        /// Localized name of User entity (plural form).
        /// </summary>
        public const string Users = "کاربران";

        /// <summary>
        /// Localized name of User entity (unknown).
        /// </summary>
        public const string AUser = "کاربری";

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

        /// <summary>
        /// Localized name of WorkItem entity (singular form).
        /// </summary>
        public const string WorkItem = "کار";

        /// <summary>
        /// Localized name of WorkItem entity (unknown).
        /// </summary>
        public const string AWorkItem = "کاری";
    }
}
