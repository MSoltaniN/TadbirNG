using System;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات نمایشی خلاصه برای یک بخش از بردار حساب را نگهداری می کند
    /// </summary>
    public class AccountItemBriefViewModel : ITreeEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی این بخش
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام این بخش
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد شناسایی جزئی این رکورد در ساختار درختی
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// کد کامل این بخش
        /// </summary>
        public string FullCode { get; set; }

        /// <summary>
        /// شماره سطح این رکورد در ساختار درختی
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// تعداد زیرشاخه های این بخش
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// شناسه دیتابیسی والد این بخش
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// مشخص می کند که آیا وضعیت این بخش انتخاب شده است یا نه؟
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// شناسه دیتابیسی گروه حساب این بخش
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// اطلاعات این نمونه را به صورت متنی ساخته و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات این نمونه به صورت متنی</returns>
        public override string ToString()
        {
            return String.Format("{0} ({1})", Name, FullCode);
        }
    }
}
