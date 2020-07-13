using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// 
    /// </summary>
public class GroupActionResultViewModel
    {
        /// <summary>
        /// شناسه موجودیت
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام موجودیت
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد کامل مورد استفاده برای موجودیت های پایه ای که حالت درختی دارند و نامشان ممکن است تکراری باشد
        /// </summary>
        public string FullCode { get; set; }

        /// <summary>
        /// پیام خطا رادر خود نگه می دارد
        /// </summary>
        public string ErrorMesagge { get; set; }

        /// <summary>
        /// تاریخ ایجاد سند
        /// </summary>
        public DateTime Date { get; set; }
    }
}
