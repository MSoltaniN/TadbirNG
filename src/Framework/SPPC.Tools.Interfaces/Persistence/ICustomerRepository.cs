using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public interface ICustomerRepository
    {
        Task<IList<CustomerModel>> GetCustomersAsync();

        Task<CustomerModel> GetCustomerAsync(int customerId);

        Task<IList<KeyValue>> GetCustomerLookupAsync();

        Task SaveCustomerAsync(CustomerModel customer);
    }
}
