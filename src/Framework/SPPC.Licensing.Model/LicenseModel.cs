using System;
using SPPC.Framework.Domain;

namespace SPPC.Licensing.Model
{
    public class LicenseModel : IEntity
    {
        public LicenseModel()
        {
            var now = DateTime.Now.Date;
            UserCount = 10;
            StartDate = now;
            EndDate = new DateTime(now.Year + 1, now.Month, now.Day);
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public InstanceModel InstanceKey { get; set; }

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

        public CustomerModel Customer { get; set; }

        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public LicenseModel GetCopy()
        {
            return (LicenseModel)MemberwiseClone();
        }
    }
}
