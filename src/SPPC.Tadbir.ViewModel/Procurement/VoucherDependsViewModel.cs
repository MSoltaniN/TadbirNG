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
        public List<KeyValue> VoucherTypes { get; protected set; }

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
        /// شرکای تجاری به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> Partners { get; protected set; }

        /// <summary>
        /// واحدهای سازمانی به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> Units { get; protected set; }

        /// <summary>
        /// انبارها به صورت مجموعه ای از کد و نام
        /// </summary>
        public List<KeyValue> Warehouses { get; protected set; }
    }
}
