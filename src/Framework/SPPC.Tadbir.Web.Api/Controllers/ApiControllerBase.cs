using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// امکانات مشترک مورد نیاز در سرویس وب برنامه را پیاده سازی می کند
    /// </summary>
    public abstract class ApiControllerBase : Controller
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        protected ApiControllerBase(IStringLocalizer<AppStrings> strings)
        {
            _strings = strings;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی مورد نیاز برای کاربر جاری برنامه
        /// </summary>
        protected SecurityContext SecurityContext
        {
            get { return GetSecurityContext(); }
        }

        /// <summary>
        /// گزینه های فیلتر، مرتب سازی و صفحه بندی اطلاعات در فهرست اطلاعاتی جاری
        /// </summary>
        protected GridOptions GridOptions
        {
            get { return GetGridOptions(); }
        }

        /// <summary>
        /// توکن امنیتی کدگذاری شده داده شده را به صورت اطلاعات محیطی و امنیتی تبدیل کرده و برمی گرداند
        /// </summary>
        /// <param name="ticket">توکن امنیتی کدگذاری شده به صورت متنی</param>
        /// <returns>اطلاعات محیطی و امنیتی به دست آمده از توکن</returns>
        protected static SecurityContext SecurityContextFromTicket(string ticket)
        {
            var json = Encoding.UTF8.GetString(Transform.FromBase64String(ticket));
            return JsonHelper.To<SecurityContext>(json);
        }

        /// <summary>
        /// تعداد کل سطرهای اطلاعاتی فهرست جاری را به صورت هدر خاص برنامه به درخواست اضافه می کند
        /// </summary>
        /// <param name="count">تعداد کل سطرها با در نظر گرفتن فیلترهای فعال</param>
        protected void SetItemCount(int count)
        {
            Response.Headers.Add(AppConstants.TotalCountHeaderName, count.ToString());
        }

        /// <summary>
        /// شماره ردیف سطرهای اطلاعاتی را در صفحه جاری اطلاعات تنظیم می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی به کار رفته در اطلاعات</typeparam>
        /// <param name="items">مجموعه سطرهای اطلاعاتی در صفحه جاری</param>
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

        /// <summary>
        /// لیست اطلاعاتی صفحه بندی شده را برای نمایش در برنامه آماده کرده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی به کار رفته در اطلاعات</typeparam>
        /// <param name="pagedList">لیست اطلاعاتی صفحه بندی شده</param>
        /// <returns>اطلاعات لیست اطلاعاتی با قالب برنامه</returns>
        protected IActionResult JsonListResult<TModel>(PagedList<TModel> pagedList)
            where TModel : ViewModelBase
        {
            SetItemCount(pagedList.TotalCount);
            SetRowNumbers(pagedList.Items);
            return Json(pagedList.Items);
        }

        /// <summary>
        /// پاسخ مناسب به برنامه را با توجه به آبجکت اطلاعاتی داده شده برمی گرداند
        /// </summary>
        /// <param name="data">آبجکت اطلاعاتی داده شده</param>
        /// <returns>آبجکت اطلاعاتی با قالب برنامه یا کد وضعیتی 404 در صورت مقدار نداشتن آبجکت</returns>
        protected IActionResult JsonReadResult(object data)
        {
            var result = (data != null)
                ? Json(data)
                : NotFound() as IActionResult;

            return result;
        }

        /// <summary>
        /// پاسخ مناسب به برنامه را با توجه به آبجکت اطلاعاتی داده شده برمی گرداند
        /// </summary>
        /// <param name="data">آبجکت اطلاعاتی داده شده</param>
        /// <returns>آبجکت اطلاعاتی با قالب برنامه یا کد وضعیتی 404 در صورت مقدار نداشتن آبجکت</returns>
        protected IActionResult OkReadResult(object data)
        {
            var result = (data != null)
                ? Ok(data)
                : NotFound() as IActionResult;

            return result;
        }

        /// <summary>
        /// پاسخ مورد نیاز برای یک درخواست نامعتبر را با توجه به مقادیر داده شده برمی گرداند
        /// </summary>
        /// <param name="message">متن محلی شده پیغام خطا</param>
        /// <param name="type">نوع خطای ایجاد شده</param>
        /// <returns>پاسخ مورد نیاز برای درخواست نامعتبر</returns>
        protected IActionResult BadRequestResult(string message, ErrorType type = ErrorType.ValidationError)
        {
            var error = new ErrorViewModel(message, type);
            return BadRequest(error);
        }

        /// <summary>
        /// پاسخ مورد نیاز برای یک درخواست نامعتبر را با توجه به مقادیر داده شده برمی گرداند
        /// </summary>
        /// <param name="modelState">ساختار اطلاعاتی شامل ریز خطاهای اعتبارسنجی</param>
        /// <returns>پاسخ مورد نیاز برای درخواست نامعتبر</returns>
        protected IActionResult BadRequestResult(ModelStateDictionary modelState)
        {
            var error = new ErrorViewModel() { Type = ErrorType.ValidationError };
            foreach (string field in modelState.Keys)
            {
                foreach (var modelError in modelState[field].Errors)
                {
                    error.Messages.Add(modelError.ErrorMessage);
                }
            }

            return BadRequest(error);
        }

        /// <summary>
        /// کد دو حرفی زبان اصلی مورد نیاز درخواست را خوانده و برمی گرداند
        /// </summary>
        /// <returns>زبان اصلی مورد نیاز درخواست به شکل کد دو حرفی</returns>
        protected string GetPrimaryRequestLanguage()
        {
            string languages = GetAcceptLanguages();
            return languages.Substring(0, 2);
        }

        /// <summary>
        /// زبان های مورد نیاز درخواست را از هدر استاندارد مربوطه خوانده و برمی گرداند
        /// </summary>
        /// <returns>زبان های مورد نیاز درخواست با قالب متنی استاندارد وب</returns>
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

        /// <summary>
        /// هدر ویژه برنامه را برای به دست آوردن ورودی های مفصل و پیچیده از درخواست جاری می خواند
        /// </summary>
        /// <typeparam name="T">نوع مدل اطلاعاتی پارامترها</typeparam>
        /// <returns>پارامترهای خوانده شده از درخواست</returns>
        protected T GetParameters<T>()
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

        /// <summary>
        /// امکان ترجمه متن های چندزبانه را فراهم می کند
        /// </summary>
        protected IStringLocalizer<AppStrings> _strings;
    }
}