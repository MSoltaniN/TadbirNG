using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SPPC.Framework.Service;
using SPPC.Licensing.Api;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Configuration;
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
            var imageFileName = $"{SysParameterUtility.LicenseServer.ImageName}.tar.gz";
            var imageFileData = _utility.GetImageData(SysParameterUtility.LicenseServer.ImageName);
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
            var imageFileName = $"{SysParameterUtility.ApiServer.ImageName}.tar.gz";
            var imageFileData = _utility.GetImageData(SysParameterUtility.ApiServer.ImageName, license.Edition);
            return File(imageFileData, ImageMimeType, imageFileName);
        }

        // GET: api/services/db-server
        [Route(UpdateApi.DbServerImageUrl)]
        public IActionResult GetDbServerAsync()
        {
            var imageFileName = $"{SysParameterUtility.DbServer.ImageName}.tar.gz";
            var imageFileData = _utility.GetImageData(SysParameterUtility.DbServer.ImageName);
            return File(imageFileData, ImageMimeType, imageFileName);
        }

        // GET: api/services/web-app
        [Route(UpdateApi.WebAppImageUrl)]
        public IActionResult GetWebAppAsync()
        {
            var imageFileName = $"{SysParameterUtility.WebApp.ImageName}.tar.gz";
            var imageFileData = _utility.GetImageData(SysParameterUtility.WebApp.ImageName);
            return File(imageFileData, ImageMimeType, imageFileName);
        }

        private InstanceModel GetInstanceData()
        {
            var instance = default(InstanceModel);
            var header = Request.Headers[LicenseConstants.InstanceHeaderName];
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
