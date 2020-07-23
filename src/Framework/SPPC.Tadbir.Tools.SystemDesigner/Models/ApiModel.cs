using System;
using System.Collections.Generic;
using System.Configuration;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class ApiModel
    {
        public ApiModel()
        {
            OutputPath = ConfigurationManager.AppSettings["ControllerFolder"];
        }
        
        public string EntityName { get; set; }

        public string OutputPath { get; set; }
    }
}
