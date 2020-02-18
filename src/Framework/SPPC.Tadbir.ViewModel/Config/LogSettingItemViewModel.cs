using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Config
{
    public class LogSettingItemViewModel
    {
        public int Id { get; set; }

        public int OperationId { get; set; }

        public string OperationName { get; set; }

        public bool IsEnabled { get; set; }
    }
}
