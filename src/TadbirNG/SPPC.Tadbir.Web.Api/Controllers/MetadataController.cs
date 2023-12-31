﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class MetadataController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public MetadataController(IMetadataRepository repository,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/metadata/views
        [HttpGet]
        [Route(MetadataApi.ViewsMetadataUrl)]
        public async Task<IActionResult> GetViewsMetadata()
        {
            var allViews = await _repository.GetViewsMetadataAsync();
            foreach (var view in allViews)
            {
                Localize(view);
            }

            return JsonReadResult(allViews);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        // GET: api/metadata/views/{viewName}
        [HttpGet]
        [Route(MetadataApi.ViewMetadataUrl)]
        public async Task<IActionResult> GetViewMetadata(string viewName)
        {
            var metadata = await _repository.GetViewMetadataAsync(viewName);
            Localize(metadata);
            return JsonReadResult(metadata);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/metadata/views/{viewId:min(1)}
        [HttpGet]
        [Route(MetadataApi.ViewMetadataByIdUrl)]
        public async Task<IActionResult> GetViewMetadataById(int viewId)
        {
            var metadata = await _repository.GetViewMetadataByIdAsync(viewId);
            Localize(metadata);
            return JsonReadResult(metadata);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/metadata/permissions
        [HttpGet]
        [Route(MetadataApi.PermissionMetadataUrl)]
        public async Task<IActionResult> GetPermissionMetadataAsync()
        {
            var permissions = await _repository.GetPermissionGroupsAsync();
            return Json(permissions);
        }

        private void Localize(ViewViewModel metadata)
        {
            if (metadata != null)
            {
                metadata.Name = _strings[metadata.Name];
                foreach (var column in metadata.Columns)
                {
                    var config = JsonHelper.To<ColumnViewConfig>(column.Settings);
                    config.ExtraLarge.Title =
                        config.ExtraSmall.Title =
                        config.Large.Title =
                        config.Medium.Title =
                        config.Small.Title = _strings[column.Name];
                    column.GroupName = _strings[column.GroupName ?? string.Empty];
                    column.Settings = JsonHelper.From(config, false);
                }
            }
        }

        private readonly IMetadataRepository _repository;
    }
}