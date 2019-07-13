using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class SettingsController : ApiControllerBase
    {
        public SettingsController(IConfigRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/settings
        [Route(SettingsApi.AllSettingsUrl)]
        [AuthorizeRequest(SecureEntity.Setting, (int)SettingPermissions.ViewSettings)]
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
        [AuthorizeRequest(SecureEntity.Setting, (int)SettingPermissions.ManageSettings)]
        public async Task<IActionResult> PutModifiedSettingsAsync([FromBody] List<SettingBriefViewModel> settings)
        {
            if (settings == null)
            {
                return BadRequest();        // TODO: Add error message
            }

            await _repository.SaveConfigAsync(settings);
            return Ok();
        }

        // GET: api/settings/{settingId:min(1)}
        [Route(SettingsApi.SettingUrl)]
        public async Task<IActionResult> GetSettingByIdAsync(int settingId)
        {
            var setting = await _repository.GetConfigByIdAsync(settingId);
            if (setting != null)
            {
                setting.Title = _strings[setting.Title];
                setting.Description = _strings[setting.Description];
            }

            return JsonReadResult(setting);
        }

        // GET: api/settings/list/users/{userId:min(1)}
        [Route(SettingsApi.ListSettingsByUserUrl)]
        public async Task<IActionResult> GetListSettingsByUserAsync(int userId)
        {
            var listSettings = await _repository.GetListViewConfigByUserAsync(userId);
            Array.ForEach(listSettings.ToArray(), item => Localize(item));
            return Json(listSettings);
        }

        // GET: api/settings/list/users/{userId:min(1)}/views/{viewId:min(1)}
        [Route(SettingsApi.ListSettingsByUserAndViewUrl)]
        public async Task<IActionResult> GetListSettingsByUserAndViewAsync(int userId, int viewId)
        {
            var listSettings = await _repository.GetListViewConfigByUserAsync(userId, viewId);
            Localize(listSettings);
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

        // GET: api/settings/qsearch/users/{userId:min(1)}/views/{viewId:min(1)}
        [Route(SettingsApi.QuickSearchSettingsByUserAndViewUrl)]
        public async Task<IActionResult> GetQSearchSettingsByUserAndViewAsync(int userId, int viewId)
        {
            var searchSettings = await _repository.GetQuickSearchConfigAsync(userId, viewId);
            Localize(searchSettings);
            return Json(searchSettings);
        }

        // PUT: api/settings/qsearch/users/{userId:min(1)}
        [HttpPut]
        [Route(SettingsApi.QuickSearchSettingsByUserUrl)]
        public async Task<IActionResult> PutModifiedQSearchSettingsByUserAsync(
            int userId, [FromBody] QuickSearchConfig settings)
        {
            if (settings == null)
            {
                return BadRequest();        // TODO: Add error message
            }

            await _repository.SaveQuickSearchConfigAsync(userId, settings);
            return Ok();
        }

        // GET: api/settings/qreport/users/{userId:min(1)}/views/{viewId:min(1)}
        [Route(SettingsApi.QuickReportSettingsByUserAndViewUrl)]
        public async Task<IActionResult> GetQReportSettingsByUserAndViewAsync(int userId, int viewId)
        {
            var reportSettings = await _repository.GetQuickReportConfigAsync(userId, viewId);
            Localize(reportSettings);
            return Json(reportSettings);
        }

        // PUT: api/settings/qreport/users/{userId:min(1)}
        [HttpPut]
        [Route(SettingsApi.QuickReportSettingsByUserUrl)]
        public async Task<IActionResult> PutModifiedQReportSettingsByUserAsync(
            int userId, [FromBody] QuickReportConfig settings)
        {
            if (settings == null)
            {
                return BadRequest();        // TODO: Add error message
            }

            await _repository.SaveQuickReportConfigAsync(userId, settings);
            return Ok();
        }

        // GET: api/settings/views/{viewId:min(1)}/tree
        [Route(SettingsApi.ViewTreeSettingsByViewUrl)]
        [AuthorizeRequest(SecureEntity.Setting, (int)SettingPermissions.ViewSettings)]
        public async Task<IActionResult> GetViewTreeSettingsByViewAsync(int viewId)
        {
            var viewSettings = await _repository.GetViewTreeConfigByViewAsync(viewId);
            Localize(viewSettings, viewId);
            return Json(viewSettings);
        }

        // PUT: api/settings/views/tree
        [HttpPut]
        [Route(SettingsApi.ViewTreeSettingsUrl)]
        [AuthorizeRequest(SecureEntity.Setting, (int)SettingPermissions.ManageSettings)]
        public async Task<IActionResult> PutModifiedViewTreeSettingsAsync([FromBody] List<ViewTreeFullConfig> settings)
        {
            if (settings == null)
            {
                return BadRequest();        // TODO: Add error message
            }

            await _repository.SaveViewTreeConfigAsync(settings);
            return Ok();
        }

        private void Localize(ViewTreeFullConfig viewSettings, int viewId)
        {
            if (viewId == ViewName.Account)
            {
                Array.ForEach(
                    viewSettings.Default.Levels.Where(level => level.No <= viewSettings.Default.MaxDepth).ToArray(),
                    level => level.Name = _strings.Format(level.Name));
                Array.ForEach(
                    viewSettings.Default.Levels.Where(level => level.No > viewSettings.Default.MaxDepth).ToArray(),
                    level => level.Name = String.Format(_strings[AppStrings.LevelX], level.No));
                Array.ForEach(
                    viewSettings.Current.Levels.Where(level => level.No <= viewSettings.Default.MaxDepth).ToArray(),
                    level => level.Name = _strings.Format(level.Name));
                Array.ForEach(
                    viewSettings.Current.Levels.Where(level => level.No > viewSettings.Default.MaxDepth).ToArray(),
                    level => level.Name = level.Name == "LevelX"
                        ? String.Format(_strings[AppStrings.LevelX], level.No)
                        : level.Name);
            }
            else
            {
                Array.ForEach(
                    viewSettings.Default.Levels.ToArray(),
                    level => level.Name = String.Format(_strings[AppStrings.LevelX], level.No));
                Array.ForEach(
                    viewSettings.Current.Levels.ToArray(),
                    level => level.Name = level.Name == "LevelX"
                        ? String.Format(_strings[AppStrings.LevelX], level.No)
                        : level.Name);
            }
        }

        private void Localize(ListFormViewConfig metadata)
        {
            if (metadata != null)
            {
                foreach (var column in metadata.ColumnViews)
                {
                    column.ExtraLarge.Title =
                        column.ExtraSmall.Title =
                        column.Large.Title =
                        column.Medium.Title =
                        column.Small.Title = _strings[column.Name];
                }
            }
        }

        private void Localize(QuickSearchConfig searchSettings)
        {
            if (searchSettings != null)
            {
                foreach (var column in searchSettings.Columns)
                {
                    column.Title = _strings[column.Title];
                }
            }
        }

        private void Localize(QuickReportConfig reportSettings)
        {
            if (reportSettings != null)
            {
                var localCode = GetAcceptLanguages().Substring(0, 2);
                foreach (var column in reportSettings.Columns)
                {
                    var userTitle = column.UserTitleMap
                        .Where(item => item.Key == localCode)
                        .SingleOrDefault();
                    column.UserTitle = !String.IsNullOrEmpty(userTitle.Value)
                        ? userTitle.Value
                        : _strings[column.Title];
                    var userWidth = column.WidthMap
                        .Where(item => item.Key == localCode)
                        .SingleOrDefault();
                    if (!String.IsNullOrEmpty(userWidth.Key))
                    {
                        column.Width = userWidth.Value;
                    }
                }
            }
        }

        private readonly IConfigRepository _repository;
    }
}