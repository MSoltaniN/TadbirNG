using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Model
{
    public class LicenseFileModel
    {
        public string CustomerName { get; set; }

        public string ContactName { get; set; }

        public string CustomerKey { get; set; }

        public string LicenseKey { get; set; }

        public string HardwareKey { get; set; }

        public string ClientKey { get; set; }

        public string Secret { get; set; }

        public int UserCount { get; set; }

        public string Edition { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ActiveModules { get; set; }

        public bool IsActivated { get; set; }
    }
}
