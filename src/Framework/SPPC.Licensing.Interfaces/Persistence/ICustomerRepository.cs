using System;
using System.Collections.Generic;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public interface ICustomerRepository
    {
        void SaveCustomer(CustomerModel customer);
    }
}
