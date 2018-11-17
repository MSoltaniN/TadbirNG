using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// کلید متن های چند زبانه مورد استفاده در اعتبارسنجی یک بردار حساب را تعریف می کند
    /// </summary>
    public sealed class FullAccountError
    {
        private FullAccountError()
        {
        }

        /// <summary>
        /// خطای استفاده از یک حساب ناموجود یا غیر قابل دسترسی در محیط جاری برنامه
        /// </summary>
        public const string InvalidAccountInFullAccount = "InvalidAccountInFullAccount";

        /// <summary>
        /// خطای استفاده از حساب دارای زیرمجموعه در بردار حساب
        /// </summary>
        public const string NonLeafAccountInFullAccount = "NonLeafAccountInFullAccount";

        /// <summary>
        /// خطای استفاده از یک تفصیلی شناور ناموجود یا غیر قابل دسترسی در محیط جاری برنامه
        /// </summary>
        public const string InvalidDetailAccountInFullAccount = "InvalidDetailAccountInFullAccount";

        /// <summary>
        /// خطای استفاده از تفصیلی شناور دارای زیرمجموعه در بردار حساب
        /// </summary>
        public const string NonLeafDetailAccountInFullAccount = "NonLeafDetailAccountInFullAccount";

        /// <summary>
        /// خطای مشخص نکردن یک تفصیلی شناور اجباری مرتبط با حساب انتخاب شده در بردار حساب
        /// </summary>
        public const string MissingDetailAccountInFullAccount = "MissingDetailAccountInFullAccount";

        /// <summary>
        /// خطای استفاده از تفصیلی شناور نامرتبط با حساب انتخاب شده در بردار حساب
        /// </summary>
        public const string UnrelatedDetailAccountInFullAccount = "UnrelatedDetailAccountInFullAccount";

        /// <summary>
        /// خطای استفاده از یک مرکز هزینه ناموجود یا غیر قابل دسترسی در محیط جاری برنامه
        /// </summary>
        public const string InvalidCostCenterInFullAccount = "InvalidCostCenterInFullAccount";

        /// <summary>
        /// خطای استفاده از مرکز هزینه دارای زیرمجموعه در بردار حساب
        /// </summary>
        public const string NonLeafCostCenterInFullAccount = "NonLeafCostCenterInFullAccount";

        /// <summary>
        /// خطای مشخص نکردن یک مرکز هزینه اجباری مرتبط با حساب انتخاب شده در بردار حساب
        /// </summary>
        public const string MissingCostCenterInFullAccount = "MissingCostCenterInFullAccount";

        /// <summary>
        /// خطای استفاده از مرکز هزینه نامرتبط با حساب انتخاب شده در بردار حساب
        /// </summary>
        public const string UnrelatedCostCenterInFullAccount = "UnrelatedCostCenterInFullAccount";

        /// <summary>
        /// خطای استفاده از یک پروژه ناموجود یا غیر قابل دسترسی در محیط جاری برنامه
        /// </summary>
        public const string InvalidProjectInFullAccount = "InvalidProjectInFullAccount";

        /// <summary>
        /// خطای استفاده از پروژه دارای زیرمجموعه در بردار حساب
        /// </summary>
        public const string NonLeafProjectInFullAccount = "NonLeafProjectInFullAccount";

        /// <summary>
        /// خطای مشخص نکردن یک پروژه اجباری مرتبط با حساب انتخاب شده در بردار حساب
        /// </summary>
        public const string MissingProjectInFullAccount = "MissingProjectInFullAccount";

        /// <summary>
        /// خطای استفاده از پروژه نامرتبط با حساب انتخاب شده در بردار حساب
        /// </summary>
        public const string UnrelatedProjectInFullAccount = "UnrelatedProjectInFullAccount";
    }
}
