using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Model
{
    public class InstanceModel : IEquatable<InstanceModel>
    {
        /// <summary>
        /// Unique key of a customer (default Guid format)
        /// </summary>
        public string CustomerKey { get; set; }

        /// <summary>
        /// Unique key of a specific program installation for a customer (default Guid format)
        /// </summary>
        public string LicenseKey { get; set; }

        public bool Equals(InstanceModel other)
        {
            bool sameCustomer = (String.Compare(other.CustomerKey, CustomerKey) == 0);
            bool sameLicense = (String.Compare(other.LicenseKey, LicenseKey) == 0);
            return sameCustomer && sameLicense;
        }
    }
}
