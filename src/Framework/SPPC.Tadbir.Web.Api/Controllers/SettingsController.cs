using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
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
        public async Task<IActionResult> GetAllSettings()
        {
            var allSettings = await _repository.GetAllConfigAsync();
            Array.ForEach(allSettings.ToArray(), cfg =>
                {
                    cfg.Title = _strings[cfg.Title];
                    cfg.Description = _strings[cfg.Description];
                });
            return Json(allSettings);
        }

        // GET: api/settings/list/users/{userId:min(1)}
        [Route(SettingsApi.ListSettingsByUserUrl)]
        public async Task<IActionResult> GetListSettingsByUser(int userId)
        {
            var listSettings = await _repository.GetListViewConfigByUserAsync(userId);
            return Json(listSettings);
        }

        private readonly IConfigRepository _repository;
        private readonly IStringLocalizer<AppStrings> _strings;
    }
}