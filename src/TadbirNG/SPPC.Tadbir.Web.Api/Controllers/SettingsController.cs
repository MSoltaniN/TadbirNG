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
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class SettingsController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="log"></param>
        /// <param name="system"></param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public SettingsController(IConfigRepository repository, ILogConfigRepository log,
            ISystemConfigRepository system, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _logRepository = log;
            _systemRepository = system;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/settings
        [HttpGet]
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        // PUT: api/settings
        [HttpPut]
        [Route(SettingsApi.AllSettingsUrl)]
        [AuthorizeRequest(SecureEntity.Setting, (int)SettingPermissions.SaveSettings)]
        public async Task<IActionResult> PutModifiedSettingsAsync([FromBody] List<SettingBriefViewModel> settings)
        {
            if (settings == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Settings));
            }

            await _repository.SaveConfigAsync(settings);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        // GET: api/settings/{settingId:min(1)}
        [HttpGet]
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/settings/list/users/{userId:min(1)}
        [HttpGet]
        [Route(SettingsApi.ListSettingsByUserUrl)]
        public async Task<IActionResult> GetListSettingsByUserAsync(int userId)
        {
            var listSettings = await _systemRepository.GetListViewConfigByUserAsync(userId);
            Array.ForEach(listSettings.ToArray(), item => Localize(item));
            return Json(listSettings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/settings/list/users/{userId:min(1)}/views/{viewId:min(1)}
        [HttpGet]
        [Route(SettingsApi.ListSettingsByUserAndViewUrl)]
        public async Task<IActionResult> GetListSettingsByUserAndViewAsync(int userId, int viewId)
        {
            var listSettings = await _systemRepository.GetListViewConfigByUserAsync(userId, viewId);
            Localize(listSettings);
            return Json(listSettings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        // PUT: api/settings/sysconfig
        [HttpPut]
        [Route(SettingsApi.SystemConfigUrl)]
        public async Task<IActionResult> PutSystemConfigAsync([FromBody] SettingBriefViewModel setting)
        {
            if (setting == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Settings));
            }
            if (await _repository.ValidateInventoryModeChangeAsync())
            {
                await _repository.SaveSystemConfigAsync(setting);
                return Ok();
            }
            else
            {
                return BadRequestResult(_strings.Format(AppStrings.InventoryModeChangeNotAllowed, AppStrings.Settings));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        // PUT: api/settings/list/users/{userId:min(1)}
        [HttpPut]
        [Route(SettingsApi.ListSettingsByUserUrl)]
        public async Task<IActionResult> PutModifiedListSettingsByUserAsync(
            int userId, [FromBody] ListFormViewConfig settings)
        {
            if (settings == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Settings));
            }

            await _systemRepository.SaveUserListConfigAsync(userId, settings);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/settings/qsearch/users/{userId:min(1)}/views/{viewId:min(1)}
        [HttpGet]
        [Route(SettingsApi.QuickSearchSettingsByUserAndViewUrl)]
        public async Task<IActionResult> GetQSearchSettingsByUserAndViewAsync(int userId, int viewId)
        {
            var searchSettings = await _repository.GetQuickSearchConfigAsync(userId, viewId);
            Localize(searchSettings);
            return Json(searchSettings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        // PUT: api/settings/qsearch/users/{userId:min(1)}
        [HttpPut]
        [Route(SettingsApi.QuickSearchSettingsByUserUrl)]
        public async Task<IActionResult> PutModifiedQSearchSettingsByUserAsync(
            int userId, [FromBody] QuickSearchConfig settings)
        {
            if (settings == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Settings));
            }

            await _repository.SaveQuickSearchConfigAsync(userId, settings);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/settings/qreport/users/{userId:min(1)}/views/{viewId:min(1)}
        [HttpGet]
        [Route(SettingsApi.QuickReportSettingsByUserAndViewUrl)]
        public async Task<IActionResult> GetQReportSettingsByUserAndViewAsync(int userId, int viewId)
        {
            var reportSettings = await _systemRepository.GetQuickReportConfigAsync(userId, viewId);
            Localize(reportSettings);
            return Json(reportSettings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        // PUT: api/settings/qreport/users/{userId:min(1)}
        [HttpPut]
        [Route(SettingsApi.QuickReportSettingsByUserUrl)]
        public async Task<IActionResult> PutModifiedQReportSettingsByUserAsync(
            int userId, [FromBody] QuickReportConfig settings)
        {
            if (settings == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Settings));
            }

            await _systemRepository.SaveQuickReportConfigAsync(userId, settings);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/settings/views/{viewId:min(1)}/tree
        [HttpGet]
        [Route(SettingsApi.ViewTreeSettingsByViewUrl)]
        [AuthorizeRequest(SecureEntity.Setting, (int)SettingPermissions.ViewSettings)]
        public async Task<IActionResult> GetViewTreeSettingsByViewAsync(int viewId)
        {
            var viewSettings = await _repository.GetViewTreeConfigByViewAsync(viewId);
            Localize(viewSettings, viewId);
            return Json(viewSettings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        // PUT: api/settings/views/tree
        [HttpPut]
        [Route(SettingsApi.ViewTreeSettingsUrl)]
        [AuthorizeRequest(SecureEntity.Setting, (int)SettingPermissions.SaveSettings)]
        public async Task<IActionResult> PutModifiedViewTreeSettingsAsync([FromBody] List<ViewTreeFullConfig> settings)
        {
            if (settings == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Settings));
            }

            await _repository.SaveViewTreeConfigAsync(settings);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/settings/log
        [HttpGet]
        [Route(SettingsApi.LogSettingsUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> GetLogSettingsAsync()
        {
            var result = await _logRepository.GetAllConfigAsync();
            SetItemCount(result.Count);
            Localize(result);
            return Json(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/settings/sys/log
        [HttpGet]
        [Route(SettingsApi.SystemLogSettingsUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> GetSystemLogSettingsAsync()
        {
            var result = await _logRepository.GetAllSystemConfigAsync();
            SetItemCount(result.Count);
            Localize(result);
            return Json(result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modifiedItems"></param>
        /// <returns></returns>
        // PUT: api/settings/log
        [HttpPut]
        [Route(SettingsApi.LogSettingsUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutModifiedLogSettingsAsync(
            [FromBody] List<LogSettingItemViewModel> modifiedItems)
        {
            await _logRepository.SaveModifiedConfigAsync(modifiedItems);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modifiedItems"></param>
        /// <returns></returns>
        // PUT: api/settings/sys/log
        [HttpPut]
        [Route(SettingsApi.SystemLogSettingsUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutModifiedSystemLogSettingsAsync(
            [FromBody] List<LogSettingItemViewModel> modifiedItems)
        {
            await _logRepository.SaveModifiedSystemConfigAsync(modifiedItems);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        // GET: api/settings/labels/forms/{formId:min(1)}
        [HttpGet]
        [Route(SettingsApi.FormLabelsConfigUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> GetFormLabelSettingsAsync(int formId)
        {
            var locale = GetPrimaryRequestLanguage();
            var localeId = await _systemRepository.GetLocaleIdAsync(locale);
            var fullConfig = await _repository.GetFormLabelConfigAsync(formId, localeId);
            return JsonReadResult(fullConfig);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="labelConfig"></param>
        /// <returns></returns>
        // PUT: api/settings/labels/forms/{formId:min(1)}
        [HttpPut]
        [Route(SettingsApi.FormLabelsConfigUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutModifiedFormLabelSettingsAsync(
            int formId, [FromBody] FormLabelConfig labelConfig)
        {
            if (labelConfig == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Settings));
            }

            if (labelConfig.FormId != formId)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.Settings));
            }

            await _repository.SaveFormLabelConfigAsync(labelConfig);
            return Ok();
        }

        private void Localize(ViewTreeFullConfig viewSettings, int viewId)
        {
            if (viewId == ViewId.Account)
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
                var localCode = GetPrimaryRequestLanguage();
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

        private void Localize(IEnumerable<LogSettingNodeViewModel> settings)
        {
            foreach (var node in settings)
            {
                node.Name = _strings[node.Name];
                foreach (var item in node.Items)
                {
                    item.OperationName = _strings[item.OperationName];
                }
            }
        }

        private readonly IConfigRepository _repository;
        private readonly ILogConfigRepository _logRepository;
        private readonly ISystemConfigRepository _systemRepository;
    }
}