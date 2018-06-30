using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class SettingsController : Controller
    {
        public SettingsController(IConfigRepository repository, IStringLocalizer<AppStrings> strings)
        {
            _repository = repository;
            _strings = strings;
        }

        // GET: api/settings
        [Route(SettingsApi.AllSettingsUrl)]
        public async Task<IActionResult> GetAllSettingsAsync()
        {
            var allSettings = await _repository.GetAllConfigAsync();
            Array.ForEach(allSettings.ToArray(), cfg =>
                {
                    cfg.Title = _strings[cfg.Title];
                    cfg.Description = _strings[cfg.Description];
                });
            return Json(allSettings);
        }

        // PUT: api/settings
        [HttpPut]
        [Route(SettingsApi.AllSettingsUrl)]
        public async Task<IActionResult> PutModifiedSettingsAsync([FromBody] List<SettingBriefViewModel> settings)
        {
            if (settings == null)
            {
                return BadRequest();        // TODO: Add error message
            }

            await _repository.SaveConfigAsync(settings);
            return Ok();
        }

        // GET: api/settings/list/users/{userId:min(1)}
        [Route(SettingsApi.ListSettingsByUserUrl)]
        public async Task<IActionResult> GetListSettingsByUserAsync(int userId)
        {
            var listSettings = await _repository.GetListViewConfigByUserAsync(userId);
            return Json(listSettings);
        }

        // GET: api/settings/list/users/{userId:min(1)}/views/{viewId:min(1)}
        [Route(SettingsApi.ListSettingsByUserAndViewUrl)]
        public async Task<IActionResult> GetListSettingsByUserAndViewAsync(int userId, int viewId)
        {
            var listSettings = await _repository.GetListViewConfigByUserAsync(userId, viewId);
            return Json(listSettings);
        }

        // PUT: api/settings/list/users/{userId:min(1)}
        [HttpPut]
        [Route(SettingsApi.ListSettingsByUserUrl)]
        public async Task<IActionResult> PutModifiedListSettingsByUserAsync(
            int userId, [FromBody] ListFormViewConfig settings)
        {
            if (settings == null)
            {
                return BadRequest();        // TODO: Add error message
            }

            await _repository.SaveUserListConfigAsync(userId, settings);
            return Ok();
        }

        private readonly IConfigRepository _repository;
        private readonly IStringLocalizer<AppStrings> _strings;
    }
}