using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    /// <summary>
    /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک درخواست کالا را نشان می دهد
    /// </summary>
    public class VoucherDependsViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند
        /// </summary>
        public VoucherDependsViewModel()
        {
            VoucherTypes = new List<KeyValue>();
            Accounts = new List<KeyValue>();
            DetailAccounts = new List<KeyValue>();
            CostCenters = new List<KeyValue>();
            Projects = new List<KeyValue>();
            Partners = new List<KeyValue>();
            Units = new List<KeyValue>();
            Warehouses = new List<KeyValue>();
        }

        /// <summary>
        /// انواع تعریف شده برای درخواست کالا به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> VoucherTypes { get; private set; }

        /// <summary>
        /// سرفصل های حسابداری به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> Accounts { get; private set; }

        /// <summary>
        /// تفصیلی های شناور به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> DetailAccounts { get; private set; }

        /// <summary>
        /// مراکز هزینه به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> CostCenters { get; private set; }

        /// <summary>
        /// پروژه ها به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> Projects { get; private set; }

        /// <summary>
        /// شرکای تجاری به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> Partners { get; private set; }

        /// <summary>
        /// واحدهای سازمانی به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> Units { get; private set; }

        /// <summary>
        /// انبارها به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> Warehouses { get; private set; }
    }
}
