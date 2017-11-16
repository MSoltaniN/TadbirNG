using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.ViewModel.Inventory
{
    /// <summary>
    /// اطلاعات پایه مورد نیاز برای ورود اطلاعات یک سطر از موجودی کالا را نشان می دهد
    /// </summary>
    public class InventoryDependsViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند
        /// </summary>
        public InventoryDependsViewModel()
        {
            Units = new List<KeyValue>();
            Products = new List<KeyValue>();
            Warehouses = new List<KeyValue>();
        }

        /// <summary>
        /// واحدهای اندازه گیری به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> Units { get; private set; }

        /// <summary>
        /// کالاها به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> Products { get; private set; }

        /// <summary>
        /// انبارها به صورت مجموعه ای از کد و نام
        /// </summary>
        public IList<KeyValue> Warehouses { get; private set; }
    }
}
