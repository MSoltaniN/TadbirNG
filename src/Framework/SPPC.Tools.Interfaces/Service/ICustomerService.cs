using System;
using System.Collections.Generic;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Service
{
    public interface ICustomerService
    {
        IList<CustomerModel> GetCustomers();

        IList<KeyValue> GetCustomerLookup();

        CustomerModel GetCustomer(int customerId);

        string InsertCustomer(CustomerModel customer);

        string UpdateCustomer(CustomerModel customer);

        string DeleteCustomer(int customerId);
    }
}
