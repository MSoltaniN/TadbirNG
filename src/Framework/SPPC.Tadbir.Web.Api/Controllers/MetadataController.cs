using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class MetadataController : Controller
    {
        public MetadataController(IMetadataRepository repository)
        {
            _repository = repository;
        }

        // GET: api/metadata/entity/{entityName}
        [Route(MetadataApi.EntityMetadataUrl)]
        public async Task<IActionResult> GetEntityMetadata(string entityName)
        {
            var metadata = await _repository.GetViewMetadataAsync(entityName);
            return JsonReadResult(metadata);
        }

        // GET: api/metadata/entity/{entityId:min(1)}
        [Route(MetadataApi.EntityMetadataByIdUrl)]
        public async Task<IActionResult> GetEntityMetadataById(int entityId)
        {
            var metadata = await _repository.GetViewMetadataByIdAsync(entityId);
            return JsonReadResult(metadata);
        }

        // GET: api/metadata/permissions
        [Route(MetadataApi.PermissionMetadataUrl)]
        public async Task<IActionResult> GetPermissionMetadataAsync()
        {
            var permissions = await _repository.GetPermissionGroupsAsync();
            return Json(permissions);
        }

        private IActionResult JsonReadResult<TData>(TData metadata)
        {
            var result = (metadata != null)
                ? Json(metadata)
                : NotFound() as IActionResult;

            return result;
        }

        private IMetadataRepository _repository;
    }
}
