using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class LogSettingModel
    {
        public LogSettingModel()
        {
            Operations = new List<int>();
        }

        public int Id { get; set; }

        public int SubsystemId { get; set; }

        public int SourceTypeId { get; set; }

        public int? SourceId { get; set; }

        public int? EntityTypeId { get; set; }

        public string Name { get; set; }

        public int OperationId { get; set; }

        public List<int> Operations { get; }

        public bool IsEnabled { get; set; }
    }
}
