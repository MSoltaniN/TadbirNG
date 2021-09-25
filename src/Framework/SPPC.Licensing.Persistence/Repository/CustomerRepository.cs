using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<IList<CustomerModel>> GetCustomersAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<CustomerModel>();
            return await repository.GetAllAsync();
        }

        public async Task<CustomerModel> GetCustomerAsync(int customerId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CustomerModel>();
            return await repository.GetByIDAsync(customerId);
        }

        public async Task<IList<KeyValue>> GetCustomerLookupAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<CustomerModel>();
            return await repository
                .GetEntityQuery()
                .Select(cust => new KeyValue(cust.Id.ToString(), cust.CompanyName))
                .ToListAsync();
        }

        public async Task SaveCustomerAsync(CustomerModel customer)
        {
            Verify.ArgumentNotNull(customer, nameof(customer));
            var repository = UnitOfWork.GetAsyncRepository<CustomerModel>();
            if (customer.Id == 0)
            {
                repository.Insert(customer);
                await UnitOfWork.CommitAsync();
            }
            else
            {
                var existing = await repository.GetByIDAsync(customer.Id);
                if (existing != null)
                {
                    UpdateValues(customer, existing);
                    repository.Update(customer);
                    await UnitOfWork.CommitAsync();
                }
            }
        }

        private IUnitOfWork UnitOfWork { get; }

        private static void UpdateValues(CustomerModel modified, CustomerModel existing)
        {
            existing.CompanyName = modified.CompanyName;
            existing.Industry = modified.Industry;
            existing.EmployeeCount = modified.EmployeeCount;
            existing.HeadquartersAddress = modified.HeadquartersAddress;
            existing.ContactFirstName = modified.ContactFirstName;
            existing.ContactLastName = modified.ContactLastName;
            existing.WorkPhone = modified.WorkPhone;
            existing.WorkFax = modified.WorkFax;
            existing.CellPhone = modified.CellPhone;
        }
    }
}
