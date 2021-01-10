using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Licensing.Api;
using SPPC.Licensing.Model;
using SPPC.Licensing.Persistence;

namespace SPPC.Licensing.Web.Controllers
{
    [Produces("application/json")]
    public class CustomerController : Controller
    {
        public CustomerController(ILicenseRepository repository)
        {
            _repository = repository;
        }

        // POST: api/customers
        [HttpPost]
        [Route(CustomerApi.CustomersUrl)]
        public IActionResult PostNewCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
            {
                return BadRequest("Request failed because customer data is missing or malformed.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.InsertCustomer(customer);
            return StatusCode(StatusCodes.Status201Created);
        }

        private readonly ILicenseRepository _repository;
    }
}