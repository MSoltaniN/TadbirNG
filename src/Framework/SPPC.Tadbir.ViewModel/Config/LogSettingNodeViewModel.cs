using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Config
{
    public class LogSettingNodeViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public LogSettingNodeViewModel()
        {
            Items = new List<LogSettingItemViewModel>();
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public List<LogSettingItemViewModel> Items { get; }
    }
}
