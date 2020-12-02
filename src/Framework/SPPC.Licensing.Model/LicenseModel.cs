using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Model
{
    public class LicenseModel
    {
        public LicenseModel()
        {
            var now = DateTime.Now.Date;
            UserCount = 10;
            ContractStart = now;
            ContractEnd = new DateTime(now.Year + 1, now.Month, now.Day);
        }

        public InstanceModel InstanceKey { get; set; }

        public string HardwareKey { get; set; }

        public string ClientKey { get; set; }

        public string Secret { get; set; }

        public int UserCount { get; set; }

        public string Edition { get; set; }

        public DateTime ContractStart { get; set; }

        public DateTime ContractEnd { get; set; }

        public int ActiveModules { get; set; }

        public LicenseModel GetCopy()
        {
            return (LicenseModel)MemberwiseClone();
        }
    }
}
