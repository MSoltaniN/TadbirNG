using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Model
{
    public class ActivationModel
    {
        public string InstanceKey { get; set; }

        public string HardwareKey { get; set; }

        public string ClientKey { get; set; }

        public string ServerUser { get; set; }

        public string ServerPassword { get; set; }
    }
}
