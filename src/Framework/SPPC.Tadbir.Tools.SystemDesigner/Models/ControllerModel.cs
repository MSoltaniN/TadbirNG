using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class ControllerModel
    {
        public ControllerModel()
        {
            OutputPath = ConfigurationManager.AppSettings["ControllerFolder"];
            HasCrudMethods = true;
            HasCrudImpl = true;
            IsFiscalEntity = true;
        }

        public string OutputPath { get; set; }

        public string EntityName { get; set; }

        public string EntityArea { get; set; }

        public bool HasCrudMethods { get; set; }

        public bool HasCrudImpl { get; set; }

        public bool IsFiscalEntity { get; set; }
    }
}
