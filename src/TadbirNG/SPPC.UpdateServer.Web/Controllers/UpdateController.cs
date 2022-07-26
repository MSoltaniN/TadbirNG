using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SPPC.Framework.Service;
using SPPC.Licensing.Api;
using SPPC.Licensing.Model;
using SPPC.Tools.Api;
using SPPC.Tools.Utility;

namespace SPPC.UpdateServer.Web.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class UpdateController : ControllerBase
    {
        public UpdateController(IApiClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _utility = new UpdateUtility(configuration["ImageRoot"]);
        }

        // GET: api/versions/latest
        [Route(UpdateApi.LatestVersionInfoUrl)]
        public IActionResult GetLatestVersionInfo()
        {
            var instance = GetInstanceData();
            if (instance == null)
            {
                return BadRequest("Instance key cannot be null.");
            }

            var license = _apiClient.Get<LicenseViewModel>(LicenseApi.LicenseByKey, instance.LicenseKey);
            var versionInfo = _utility.GetCurrentVersionInfo(license.Edition);
            return Ok(versionInfo);
        }

        // GET: api/services/license-server
        [Route(UpdateApi.LicenseServerImageUrl)]
        public IActionResult GetLicenseServerAsync()
        {
            var imageFileName = $"{DockerService.LicenseServerImage}.tar.gz";
            var imageFileData = _utility.GetImageData(DockerService.LicenseServerImage);
            return File(imageFileData, ImageMimeType, imageFileName);
        }

        // GET: api/services/api-server
        [Route(UpdateApi.ApiServerImageUrl)]
        public IActionResult GetApiServerAsync()
        {
            var instance = GetInstanceData();
            if (instance == null)
            {
                return BadRequest("Instance key cannot be null.");
            }

            var license = _apiClient.Get<LicenseViewModel>(LicenseApi.LicenseByKey, instance.LicenseKey);
            var imageFileName = $"{DockerService.ApiServerImage}.tar.gz";
            var imageFileData = _utility.GetImageData(DockerService.ApiServerImage, license.Edition);
            return File(imageFileData, ImageMimeType, imageFileName);
        }

        // GET: api/services/db-server
        [Route(UpdateApi.DbServerImageUrl)]
        public IActionResult GetDbServerAsync()
        {
            var imageFileName = $"{DockerService.DbServerImage}.tar.gz";
            var imageFileData = _utility.GetImageData(DockerService.DbServerImage);
            return File(imageFileData, ImageMimeType, imageFileName);
        }

        // GET: api/services/web-app
        [Route(UpdateApi.WebAppImageUrl)]
        public IActionResult GetWebAppAsync()
        {
            var imageFileName = $"{DockerService.WebAppImage}.tar.gz";
            var imageFileData = _utility.GetImageData(DockerService.WebAppImage);
            return File(imageFileData, ImageMimeType, imageFileName);
        }

        private InstanceModel GetInstanceData()
        {
            var instance = default(InstanceModel);
            var header = Request.Headers[Constants.InstanceHeaderName];
            if (!String.IsNullOrEmpty(header))
            {
                instance = InstanceFactory.FromCrypto(header);
            }

            return instance;
        }

        private const string ImageMimeType = "application/x-gtar";
        private readonly IApiClient _apiClient;
        private readonly UpdateUtility _utility;
    }
}
