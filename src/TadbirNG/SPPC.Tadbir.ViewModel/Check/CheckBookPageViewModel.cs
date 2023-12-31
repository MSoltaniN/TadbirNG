﻿using System;

namespace SPPC.Tadbir.ViewModel.Check
{
    public partial class CheckBookPageViewModel
    {
        /// <summary>
        ///  شناسه یکتای دسته چک که برگه چک در آن تعریف می شود
        /// </summary>
        public int CheckBookId { get; set; }

        /// <summary>
        /// عنوان محلی شده برای وضعیت برگه
        /// </summary>
        public string StatusName { get; set; }
    }
}
