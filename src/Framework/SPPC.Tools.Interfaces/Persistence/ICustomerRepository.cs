using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public interface ICustomerRepository
    {
        Task SaveCustomerAsync(CustomerModel customer);
    }
}
