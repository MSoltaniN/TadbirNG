using System;
using System.Collections.Generic;

namespace SPPC.Licensing.Api
{
    public sealed class CustomerApi
    {
        private CustomerApi()
        {
        }

        public const string Customers = "customers";

        public const string CustomersUrl = "customers";

        public const string Customer = "customers/{0}";

        public const string CustomerUrl = "customers/{customerId:min(1)}";

        public const string CustomerLookup = "customers/lookup";

        public const string CustomerLookupUrl = "customers/lookup";
    }
}
