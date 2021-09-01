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
        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/customers
        [HttpGet]
        [Route(CustomerApi.CustomersUrl)]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var allCustomers = await _repository.GetCustomersAsync();
            return Json(allCustomers);
        }

        // GET: api/customers/{customrId:int(1)}
        [HttpGet]
        [Route(CustomerApi.CustomerUrl)]
        public async Task<IActionResult> GetCustomerAsync(int customerId)
        {
            var customer = await _repository.GetCustomerAsync(customerId);
            return Json(customer);
        }

        // GET: api/customers/lookup
        [HttpGet]
        [Route(CustomerApi.CustomerLookupUrl)]
        public async Task<IActionResult> GetCustomersLoopkupAsync()
        {
            var lookup = await _repository.GetCustomerLookupAsync();
            return Json(lookup);
        }

        // POST: api/customers
        [HttpPost]
        [Route(CustomerApi.CustomersUrl)]
        public async Task<IActionResult> PostNewCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
            {
                return BadRequest("Request failed because customer data is missing or malformed.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.SaveCustomerAsync(customer);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/customers/{customerId:min(1)}
        [HttpPut]
        [Route(CustomerApi.CustomerUrl)]
        public async Task<IActionResult> PutModifiedCustomer(int customerId, [FromBody] CustomerModel customer)
        {
            if (customer == null)
            {
                return BadRequest("Request failed because customer data is missing or malformed.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (customer.Id != customerId)
            {
                return BadRequest("Request failed due to conflicting request data.");
            }

            await _repository.SaveCustomerAsync(customer);
            return Ok();
        }

        private readonly ICustomerRepository _repository;
    }
}