using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides localized names for entity fields (in Persian).
    /// </summary>
    public sealed class FieldNames
    {
        private FieldNames()
        {
        }

        /// <summary>
        /// Localized name of Id field.
        /// </summary>
        public const string IdField = "شناسه";

        /// <summary>
        /// Localized name of FirstName field.
        /// </summary>
        public const string FirstNameField = "نام";

        /// <summary>
        /// Localized name of LastName field.
        /// </summary>
        public const string LastNameField = "نام خانوادگی";

        /// <summary>
        /// Localized name of Name field.
        /// </summary>
        public const string NameField = "نام";

        /// <summary>
        /// Localized name of Description field.
        /// </summary>
        public const string DescriptionField = "شرح";

        /// <summary>
        /// Localized name of Code field.
        /// </summary>
        public const string CodeField = "کد";

        /// <summary>
        /// Localized name of StartDate field.
        /// </summary>
        public const string StartDateField = "تاریخ شروع";

        /// <summary>
        /// Localized name of EndDate field.
        /// </summary>
        public const string EndDateField = "تاریخ پایان";

        /// <summary>
        /// Localized name of Debit field.
        /// </summary>
        public const string DebitField = "بدهکار";

        /// <summary>
        /// Localized name of Credit field.
        /// </summary>
        public const string CreditField = "بستانکار";

        /// <summary>
        /// Localized name of No (Number) field.
        /// </summary>
        public const string NumberField = "شماره";

        /// <summary>
        /// Localized name of Date field.
        /// </summary>
        public const string DateField = "تاریخ";

        /// <summary>
        /// Localized name of Status field.
        /// </summary>
        public const string StatusField = "وضعیت";

        /// <summary>
        /// Localized name of IsVerified field.
        /// </summary>
        public const string IsVerifiedField = "تایید شده";

        /// <summary>
        /// Localized name of IsApproved field.
        /// </summary>
        public const string IsApprovedField = "تصویب شده";

        /// <summary>
        /// Localized name of Code field in Account entity.
        /// </summary>
        public const string AccountCodeField = "کد حساب";

        /// <summary>
        /// Localized name of Name field in Account entity.
        /// </summary>
        public const string AccountNameField = "نام حساب";

        /// <summary>
        /// Localized name of DebitSum field
        /// </summary>
        public const string DebitSumField = "جمع بدهکار";

        /// <summary>
        /// Localized name of CreditSum field
        /// </summary>
        public const string CreditSumField = "جمع بستانکار";

        /// <summary>
        /// Localized name of Currency Type field.
        /// </summary>
        public const string CurrencyTypeField = "نوع ارز";

        /// <summary>
        /// Localized name of Account field.
        /// </summary>
        public const string AccountField = "حساب";

        /// <summary>
        /// Localized name of Permissions field.
        /// </summary>
        public const string PermissionsField = "دسترسی ها";

        /// <summary>
        /// Localized name of UserName field
        /// </summary>
        public const string UserName = "نام کاربری";

        /// <summary>
        /// Localized name of Password field
        /// </summary>
        public const string Password = "رمز ورود";

        /// <summary>
        /// Localized name of LastLoginDate field.
        /// </summary>
        public const string LastLoginDate = "تاریخ آخرین ورود";

        /// <summary>
        /// Localized name of Security Code field (used in forms that require Captcha validation)
        /// </summary>
        public static readonly string SecurityCode = "کد امنیتی";
    }
}
