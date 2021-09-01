using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Service;
using SPPC.Licensing.Api;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Service
{
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class
        /// </summary>
        /// <param name="apiClient">Object that wraps common operations for calling a Web API service</param>
        public CustomerService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IList<CustomerModel> GetCustomers()
        {
            var customers = _apiClient.Get<IList<CustomerModel>>(CustomerApi.Customers);
            return customers;
        }

        public IList<KeyValue> GetCustomerLookup()
        {
            var lookup = _apiClient.Get<IList<KeyValue>>(CustomerApi.CustomerLookup);
            return lookup;
        }

        public CustomerModel GetCustomer(int customerId)
        {
            var customer = _apiClient.Get<CustomerModel>(CustomerApi.Customer, customerId);
            return customer;
        }

        public string InsertCustomer(CustomerModel customer)
        {
            string error = String.Empty;
            Verify.ArgumentNotNull(customer, nameof(customer));
            var response = _apiClient.Insert(customer, CustomerApi.Customers);
            if (!response.Succeeded)
            {
                error = response.Message;
            }

            return error;
        }

        public string UpdateCustomer(CustomerModel customer)
        {
            string error = String.Empty;
            Verify.ArgumentNotNull(customer, nameof(customer));
            var response = _apiClient.Update(customer, CustomerApi.Customer, customer.Id);
            if (!response.Succeeded)
            {
                error = response.Message;
            }

            return error;
        }

        public string DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        private readonly IApiClient _apiClient;
    }
}
