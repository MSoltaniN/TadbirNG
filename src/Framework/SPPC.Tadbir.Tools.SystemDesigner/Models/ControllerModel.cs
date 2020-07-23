using System;
using System.Collections.Generic;
using System.Configuration;

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
        public string EntityPersianName { get; set; }
        public string EntityPluralPersianName { get; set; }

        public string EntityArea { get; set; }

        public bool HasCrudMethods { get; set; }

        public bool HasCrudImpl { get; set; }

        public bool IsFiscalEntity { get; set; }
    }
}
