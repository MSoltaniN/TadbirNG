using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;

namespace SPPC.Tadbir.Web.Api.Utility
{
    public class ApiValidation : IApiValidation
    {
        public ApiValidation(IStringLocalizer<AppStrings> strings, IApiResultFactory factory)
        {
            Strings = strings;
            _results = factory;
        }

        #region IApiBehavior Implementation

        public IStringLocalizer<AppStrings> Strings { get; }

        public SecurityContext SecurityContext
        {
            get { return GetSecurityContext(); }
        }

        public GridOptions GridOptions
        {
            get { return GetGridOptions(); }
        }

        public void SetCurrentContext(HttpRequest request, HttpResponse response)
        {
            _request = request;
            _response = response;
        }

        public SecurityContext SecurityContextFromTicket(string ticket)
        {
            var json = Encoding.UTF8.GetString(Transform.FromBase64String(ticket));
            return JsonHelper.To<SecurityContext>(json);
        }

        public void SetItemCount(int count)
        {
            _response.Headers.Add(AppConstants.TotalCountHeaderName, count.ToString());
        }

        public void SetRowNumbers<TModel>(IEnumerable<TModel> items)
            where TModel : ViewModelBase
        {
            var gridOptions = GridOptions ?? new GridOptions();
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var item in items)
            {
                item.RowNo = rowNo++;
            }
        }

        public IActionResult JsonListResult<TModel>(PagedList<TModel> pagedList)
            where TModel : ViewModelBase
        {
            SetItemCount(pagedList.TotalCount);
            SetRowNumbers(pagedList.Items);
            return _results.Json(pagedList.Items);
        }

        public IActionResult JsonReadResult(object data)
        {
            var result = (data != null)
                ? _results.Json(data)
                : _results.NotFound() as IActionResult;

            return result;
        }

        public IActionResult OkReadResult(object data)
        {
            var result = (data != null)
                ? _results.Ok(data)
                : _results.NotFound() as IActionResult;

            return result;
        }

        #endregion

        #region IApiValidation Implementation

        public string EntityNameKey { get; set; }

        public IActionResult BasicValidationResult<TViewModel>(
            TViewModel item, ModelStateDictionary modelState, int itemId = 0)
            where TViewModel : class, new()
        {
            return GetBasicValidationResult(item, modelState, itemId);
        }

        public IActionResult BranchValidationResult<TFiscalView>(TFiscalView item)
            where TFiscalView : class, IFiscalEntityView
        {
            var currentContext = SecurityContext.User;
            if (item.BranchId != currentContext.BranchId)
            {
                return _results.BadRequest(Strings.Format(AppStrings.OtherBranchEditNotAllowed));
            }

            return _results.Ok();
        }

        public IActionResult ConfigValidationResult<TTreeView>(TTreeView item, ViewTreeConfig treeConfig)
            where TTreeView : class, ITreeEntityView
        {
            Verify.ArgumentNotNull(treeConfig, nameof(treeConfig));
            if (item.Level == treeConfig.MaxDepth)
            {
                string message = String.Format(Strings[AppStrings.TreeLevelsAreTooDeep],
                    treeConfig.MaxDepth, (string)Strings[EntityNameKey]);
                return _results.BadRequest(message);
            }

            var levelConfig = treeConfig.Levels[item.Level];
            int codeLen = levelConfig.CodeLength;
            if (item.Code.Length != codeLen)
            {
                string message = String.Format(Strings[AppStrings.LevelCodeLengthIsIncorrect],
                    (string)Strings[EntityNameKey], levelConfig.Name, levelConfig.CodeLength);
                return _results.BadRequest(message);
            }

            return _results.Ok();
        }

        public async Task<IActionResult> FullAccountValidationResult(
            FullAccountViewModel fullAccount, IRelationRepository repository)
        {
            Verify.ArgumentNotNull(repository, nameof(repository));
            var lookupResult = await repository.LookupFullAccountAsync(fullAccount);
            if (!String.IsNullOrEmpty(lookupResult))
            {
                return _results.BadRequest(Strings.Format(lookupResult));
            }

            return _results.Ok();
        }

        public async Task<IEnumerable<string>> ValidateGroupDeleteAsync(
            IEnumerable<int> items, DeleteValidatorDelegate deleteValidator)
        {
            var messages = new List<string>();
            foreach (int item in items)
            {
                messages.Add(await deleteValidator(item));
            }

            return messages
                .Where(msg => !String.IsNullOrEmpty(msg));
        }

        #endregion

        private SecurityContext GetSecurityContext()
        {
            var context = _request.Headers[AppConstants.ContextHeaderName];
            if (String.IsNullOrEmpty(context))
            {
                return null;
            }

            return SecurityContextFromTicket(context);
        }

        private GridOptions GetGridOptions()
        {
            var options = _request.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return null;
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return JsonHelper.To<GridOptions>(json);
        }

        private IActionResult GetBasicValidationResult(object item, ModelStateDictionary modelState, int itemId)
        {
            if (item == null)
            {
                return _results.BadRequest(Strings.Format(AppStrings.RequestFailedNoData, EntityNameKey));
            }

            if (!modelState.IsValid)
            {
                return _results.BadRequest(modelState);
            }

            int id = Int32.Parse(Reflector.GetProperty(item, "Id").ToString());
            if (itemId != id)
            {
                return _results.BadRequest(Strings.Format(AppStrings.RequestFailedConflict, EntityNameKey));
            }

            return _results.Ok();
        }

        private readonly IApiResultFactory _results;
        private HttpRequest _request;
        private HttpResponse _response;
    }
}
