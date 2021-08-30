using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Model
{
    public class InternalActivationModel
    {
        public InstanceModel Instance { get; set; }

        public string HardwareKey { get; set; }

        public string ClientKey { get; set; }
    }
}
