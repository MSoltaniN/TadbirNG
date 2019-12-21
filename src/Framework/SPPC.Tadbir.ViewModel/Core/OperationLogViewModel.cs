using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    public partial class OperationLogViewModel
    {
        /// <summary>
        /// نام شرکتی که عملیات مورد نظر روی دیتابیس آن انجام شده
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// نام و نام خانوادگی کاربری که عملیات برنامه توسط او انجام شده
        /// </summary>
        public string UserName { get; set; }
    }
}
