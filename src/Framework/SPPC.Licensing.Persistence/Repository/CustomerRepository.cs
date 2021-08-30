using System;
using System.Threading.Tasks;
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

        public async Task SaveCustomerAsync(CustomerModel customer)
        {
            if (customer.Id == 0)
            {
                var repository = UnitOfWork.GetAsyncRepository<CustomerModel>();
                repository.Insert(customer);
                await UnitOfWork.CommitAsync();
            }
        }

        private IUnitOfWork UnitOfWork { get; }
    }
}
