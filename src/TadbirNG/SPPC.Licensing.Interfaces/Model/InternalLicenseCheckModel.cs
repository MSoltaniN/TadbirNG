using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Model
{
    public class InternalLicenseCheckModel
    {
        public InstanceModel Instance { get; set; }

        public string HardwardKey { get; set; }

        public string Certificate { get; set; }
    }
}
