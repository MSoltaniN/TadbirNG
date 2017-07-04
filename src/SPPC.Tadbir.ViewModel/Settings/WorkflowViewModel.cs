using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Settings
{
    /// <summary>
    /// این مدل نمایشی تنظیمات مربوط به یکی از گردش های کاری را نمایش می دهد
    /// </summary>
    public class WorkflowViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public WorkflowViewModel()
        {
            Editions = new List<WorkflowEditionViewModel>();
        }

        /// <summary>
        /// نام سیستمی گردش کار
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام محلی شده (فارسی) گردش کار
        /// </summary>
        public string LocalName { get; set; }

        /// <summary>
        /// نام سیستمی ویرایش پیش فرض گردش کار
        /// </summary>
        public string DefaultEdition { get; set; }

        /// <summary>
        /// مجموعه ای از ویرایش ها یا پیاده سازی های موجود برای گردش کار
        /// </summary>
        public IList<WorkflowEditionViewModel> Editions { get; private set; }
    }
}
