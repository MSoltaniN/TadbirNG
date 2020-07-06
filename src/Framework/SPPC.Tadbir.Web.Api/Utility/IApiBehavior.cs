using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel;

namespace SPPC.Tadbir.Web.Api.Utility
{
    public interface IApiBehavior
    {
        IStringLocalizer<AppStrings> Strings { get; }

        SecurityContext SecurityContext { get; }

        GridOptions GridOptions { get; }

        void SetCurrentContext(HttpRequest request, HttpResponse response);

        void SetItemCount(int count);

        void SetRowNumbers<TModel>(IEnumerable<TModel> items)
            where TModel : ViewModelBase;

        IActionResult JsonListResult<TModel>(PagedList<TModel> pagedList)
            where TModel : ViewModelBase;

        IActionResult JsonReadResult(object data);

        IActionResult OkReadResult(object data);

        SecurityContext SecurityContextFromTicket(string ticket);
    }
}
