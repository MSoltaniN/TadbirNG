using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPPC.CodeChallenge.Api;
using SPPC.CodeChallenge.Persistence;

namespace SPPC.CodeChallenge.WebApi.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public partial class LookupController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public LookupController(ILookupRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/provinces
        [HttpGet]
        [Route(LookupApi.ProvincesUrl)]
        public async Task<IActionResult> GetProvincesAsync()
        {
            var lookup = await _repository.GetProvincesAsync();
            return Json(lookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        // GET: api/lookup/cities/{provinceId:min(1)}
        [HttpGet]
        [Route(LookupApi.CitiesUrl)]
        public async Task<IActionResult> GetCitiesAsync(int provinceId)
        {
            var lookup = await _repository.GetCitiesAsync(provinceId);
            return Json(lookup);
        }

        private readonly ILookupRepository _repository;
    }
}
