using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات گزارش دفتر حساب را نگهداری می کند
    /// لازم به یادآوری است که مفهوم حساب در اینجا عمومی تر از سرفصل حسابداری بوده
    /// و شامل همه مولفه های بردار حساب می شود.
    /// </summary>
    public class AccountBookViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountBookViewModel()
        {
            Items = new List<AccountBookItemViewModel>();
        }

        /// <summary>
        /// نام محلی شده موضوع مورد نظر برای دفترگیری
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// کد کامل موضوع انتخاب شده برای دفترگیری
        /// </summary>
        public string FullCode { get; set; }

        /// <summary>
        /// نام موضوع انتخاب شده برای دفترگیری
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// جمع مبالغ بدهکار در تمام سطرهای گزارش
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// جمع مبالغ بستانکار در تمام سطرهای گزارش
        /// </summary>
        public decimal CreditSum { get; set; }

        /// <summary>
        /// مانده نهایی موضوع مورد نظر در انتهای دوره گزارشگیری
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// مجموعه سطرهای اطلاعاتی گزارش
        /// </summary>
        public List<AccountBookItemViewModel> Items { get; }
    }
}
