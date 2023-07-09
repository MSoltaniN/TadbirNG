using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
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
        /// Localized name of FirstName and LastName field.
        /// </summary>
        public const string FullNameField = "نام و نام خانوادگی";

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
        /// Localized name of Full Code field.
        /// </summary>
        public const string FullCodeField = "کد کامل";

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
        /// Localized name of Operational Status field.
        /// </summary>
        public const string OperationalStatusField = "وضعیت عملیاتی";

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
        /// Localized name of DetailAccount field.
        /// </summary>
        public const string DetailAccountField = "تفصیلی شناور";

        /// <summary>
        /// Localized name of CostCenter field.
        /// </summary>
        public const string CostCenterField = "مرکز هزینه";

        /// <summary>
        /// Localized name of Project field.
        /// </summary>
        public const string ProjectField = "پروژه";

        /// <summary>
        /// Localized name of FullAccount field.
        /// </summary>
        public const string FullAccountField = "بردار حساب";

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
        /// Localized name of Old Password field
        /// </summary>
        public const string OldPassword = "رمز ورود قدیم";

        /// <summary>
        /// Localized name of New Password field
        /// </summary>
        public const string NewPassword = "رمز ورود جدید";

        /// <summary>
        /// Localized name of Repeat Password field
        /// </summary>
        public const string RepeatPassword = "تکرار رمز ورود";

        /// <summary>
        /// Localized name of LastLoginDate field.
        /// </summary>
        public const string LastLoginDate = "تاریخ آخرین ورود";

        /// <summary>
        /// Localized name of EntityName field
        /// </summary>
        public const string EntityNameField = "نام موجودیت";

        /// <summary>
        /// Localized name of Permission Group field
        /// </summary>
        public const string PermissionGroupField = "گروه دسترسی";

        /// <summary>
        /// Localized name of CreatedBy field
        /// </summary>
        public const string CreatedBy = "ایجاد کننده";

        /// <summary>
        /// Localized name of Time field
        /// </summary>
        public const string Time = "زمان";

        /// <summary>
        /// Localized name of Subject field
        /// </summary>
        public const string Subject = "موضوع";

        /// <summary>
        /// Localized name of DocumentType field
        /// </summary>
        public const string DocumentType = "نوع مستند";

        /// <summary>
        /// Localized name of DocumentId field
        /// </summary>
        public const string DocumentId = "شناسه مستند";

        /// <summary>
        /// Localized name of DocumentNo field
        /// </summary>
        public const string DocumentNo = "شماره مستند";

        /// <summary>
        /// Localized name of Full Name field
        /// </summary>
        public const string UserFullName = "نام کاربر";

        /// <summary>
        /// Localized name of Target Role field
        /// </summary>
        public const string TargetRole = "گیرنده کار";

        /// <summary>
        /// Localized name of Action field
        /// </summary>
        public const string ActionType = "نوع اقدام";

        /// <summary>
        /// Localized name of Remarks field
        /// </summary>
        public const string CartableRemarks = "پاراف";

        /// <summary>
        /// Localized name of Workflow field
        /// </summary>
        public const string Workflow = "گردش کار";

        /// <summary>
        /// Localized name of Edition field
        /// </summary>
        public const string Edition = "ویرایش";

        /// <summary>
        /// Localized name of LastActor field
        /// </summary>
        public const string LastActor = "آخرین اقدام کننده";

        /// <summary>
        /// Localized name of LastActionDate field
        /// </summary>
        public const string LastActionDate = "تاریخ آخرین اقدام";

        /// <summary>
        /// Localized name of Reference field
        /// </summary>
        public const string ReferenceField = "ارجاع";

        /// <summary>
        /// Localized name of Requester Voucher Type field
        /// </summary>
        public const string RequisitionVoucherTypeField = "نوع درخواست کالا";

        /// <summary>
        /// Localized name of Requester field
        /// </summary>
        public const string RequesterField = "درخواست کننده";

        /// <summary>
        /// Localized name of RequesterUnit field
        /// </summary>
        public const string RequesterUnitField = "واحد درخواست کننده";

        /// <summary>
        /// Localized name of Receiver field
        /// </summary>
        public const string ReceiverField = "دریافت کننده";

        /// <summary>
        /// Localized name of ReceiverUnit field
        /// </summary>
        public const string ReceiverUnitField = "واحد دریافت کننده";

        /// <summary>
        /// Localized name of Required Date field
        /// </summary>
        public const string RequiredDateField = "تاریخ نیاز";

        /// <summary>
        /// Localized name of Promised Date field
        /// </summary>
        public const string PromisedDateField = "تاریخ تحویل توافق شده";

        /// <summary>
        /// Localized name of Warehouse field
        /// </summary>
        public const string WarehouseField = "انبار";

        /// <summary>
        /// Localized name of Reason field in RequisitionVoucher entity
        /// </summary>
        public const string RequestReasonField = "دلیل درخواست";

        /// <summary>
        /// Localized name of Description field in RequisitionVoucher entity
        /// </summary>
        public const string RequestDescriptionField = "شرح درخواست";

        /// <summary>
        /// Localized name of WarehouseComment field
        /// </summary>
        public const string WarehouseCommentField = "نظریه انبار";

        /// <summary>
        /// Localized name of Product field
        /// </summary>
        public const string ProductField = "کالا";

        /// <summary>
        /// Localized name of Product Code field
        /// </summary>
        public const string ProductCodeField = "کد کالا";

        /// <summary>
        /// Localized name of Product Name field
        /// </summary>
        public const string ProductNameField = "نام کالا";

        /// <summary>
        /// Localized name of Initial Inventory field
        /// </summary>
        public const string InitialInventory = "موجودی اولیه";

        /// <summary>
        /// Localized name of UOM (Unit of Measurement) field
        /// </summary>
        public const string UomField = "واحد اندازه گیری";

        /// <summary>
        /// Localized name of Ordered Quantity field
        /// </summary>
        public const string OrderedQuantityField = "مقدار درخواستی";

        /// <summary>
        /// Alternate localized name of Description field
        /// </summary>
        public const string RemarksField = "ملاحظات";

        /// <summary>
        /// Localized name of DeliveredQuantity field
        /// </summary>
        public const string DeliveredQuantityField = "مقدار تحویلی";

        /// <summary>
        /// Localized name of Reserved Quantity field
        /// </summary>
        public const string ReservedQuantityField = "مقدار رزرو";

        /// <summary>
        /// Localized name of Last Ordered Quantity field
        /// </summary>
        public const string LastOrderedQuantityField = "مقدار درخواست قبلی";

        /// <summary>
        /// Localized name of Delivered Date field
        /// </summary>
        public const string DeliveredDateField = "تاریخ تحویل";

        /// <summary>
        /// Localized name of Last Ordered Date field
        /// </summary>
        public const string LastOrderedDateField = "تاریخ درخواست قبلی";

        /// <summary>
        /// Localized name of IsActive field
        /// </summary>
        public const string IsActiveField = "فعال";

        /// <summary>
        /// Localized name of Company field
        /// </summary>
        public const string Company = "شرکت";

        /// <summary>
        /// Localized name of Branch field
        /// </summary>
        public const string Branch = "شعبه";

        /// <summary>
        /// Localized name of FiscalPeriod field
        /// </summary>
        public const string FiscalPeriod = "دوره مالی";

        /// <summary>
        /// Localized name of Security Code field (used in forms that require Captcha validation)
        /// </summary>
        public static readonly string SecurityCode = "کد امنیتی";
    }
}
