using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Model
{
    public class ActivationModel
    {
        public InstanceModel InstanceKey { get; set; }

        public string HardwareKey { get; set; }

        public string ClientKey { get; set; }
    }
}
