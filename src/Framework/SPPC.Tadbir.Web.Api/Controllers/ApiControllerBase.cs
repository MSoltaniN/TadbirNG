﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        protected ApiControllerBase(IStringLocalizer<AppStrings> strings = null)
        {
            _strings = strings;
        }

        protected SecurityContext SecurityContext
        {
            get { return GetSecurityContext(); }
        }

        protected GridOptions GridOptions
        {
            get { return GetGridOptions(); }
        }

        protected static SecurityContext SecurityContextFromTicket(string ticket)
        {
            var json = Encoding.UTF8.GetString(Transform.FromBase64String(ticket));
            return JsonHelper.To<SecurityContext>(json);
        }

        protected void SetItemCount(int count)
        {
            Response.Headers.Add(AppConstants.TotalCountHeaderName, count.ToString());
        }

        protected void SetRowNumbers<TModel>(IEnumerable<TModel> items)
            where TModel : ViewModelBase
        {
            var gridOptions = GridOptions ?? new GridOptions();
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var item in items)
            {
                item.RowNo = rowNo++;
            }
        }

        protected IActionResult JsonListResult<TModel>(PagedList<TModel> pagedList)
            where TModel : ViewModelBase
        {
            SetItemCount(pagedList.TotalCount);
            SetRowNumbers(pagedList.Items);
            return Json(pagedList.Items);
        }

        protected IActionResult JsonReadResult(object data)
        {
            var result = (data != null)
                ? Json(data)
                : NotFound() as IActionResult;

            return result;
        }

        protected IActionResult OkReadResult(object data)
        {
            var result = (data != null)
                ? Ok(data)
                : NotFound() as IActionResult;

            return result;
        }

        protected string GetPrimaryRequestLanguage()
        {
            string languages = GetAcceptLanguages();
            return languages.Substring(0, 2);
        }

        protected string GetAcceptLanguages()
        {
            var acceptLanguages = "fa-IR,fa";
            var header = Request.Headers["Accept-Language"];
            if (!String.IsNullOrEmpty(header))
            {
                acceptLanguages = header;
            }

            return acceptLanguages;
        }

        protected T GetHeaderParameters<T>()
        {
            var parameters = Request.Headers[AppConstants.ParametersHeaderName];
            if (String.IsNullOrEmpty(parameters))
            {
                return default(T);
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(parameters));
            var json = WebUtility.UrlDecode(urlEncoded);
            return JsonHelper.To<T>(json);
        }

        private SecurityContext GetSecurityContext()
        {
            var context = Request.Headers[AppConstants.ContextHeaderName];
            if (String.IsNullOrEmpty(context))
            {
                return null;
            }

            return SecurityContextFromTicket(context);
        }

        private GridOptions GetGridOptions()
        {
            var options = Request.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return null;
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return JsonHelper.To<GridOptions>(json);
        }

        protected IStringLocalizer<AppStrings> _strings;
    }
}