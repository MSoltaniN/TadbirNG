using System;
using System.Collections.Generic;
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
        /// مشخص می کند که آیا وضعیت این بخش انتخاب شده است یا نه؟
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
