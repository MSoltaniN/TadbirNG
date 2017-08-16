using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    /// <summary>
    /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک سطر از درخواست کالا را نشان می دهد
    /// </summary>
    public class VoucherLineDependsViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند
        /// </summary>
        public VoucherLineDependsViewModel()
        {
            Accounts = new List<KeyValue>();
            DetailAccounts = new List<KeyValue>();
            CostCenters = new List<KeyValue>();
            Projects = new List<KeyValue>();
            Products = new List<KeyValue>();
            Units = new List<KeyValue>();
            Warehouses = new List<KeyValue>();
        }

        /// <summary>
        /// سرفصل های حسابداری به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> Accounts { get; protected set; }

        /// <summary>
        /// تفصیلی های شناور به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> DetailAccounts { get; protected set; }

        /// <summary>
        /// مراکز هزینه به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> CostCenters { get; protected set; }

        /// <summary>
        /// پروژه ها به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> Projects { get; protected set; }

        /// <summary>
        /// کالاها به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> Products { get; protected set; }

        /// <summary>
        /// واحدهای اندازه گیری به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> Units { get; protected set; }

        /// <summary>
        /// انبار ها به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> Warehouses { get; protected set; }
    }
}
