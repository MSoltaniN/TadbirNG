﻿using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Model
{
    public class LicenseCheckModel
    {
        public string InstanceKey { get; set; }

        public string HardwardKey { get; set; }

        public string Certificate { get; set; }
    }
}
