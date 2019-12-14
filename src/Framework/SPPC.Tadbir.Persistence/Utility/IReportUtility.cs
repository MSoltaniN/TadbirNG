using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبات گزارشی را تعریف می کند
    /// </summary>
    public interface IReportUtility
    {
        /// <summary>
        /// آرتیکل های فیلتر شده و تجمیع نشده را برای یک گزارش خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نیاز برای نگاشت اطلاعات آرتیکل</typeparam>
        /// <param name="query">پرس و جوی آماده شده برای اعمال فیلترهای اضافه</param>
        /// <param name="from">تاریخ شروع در دوره گزارشگیری</param>
        /// <param name="to">تاریخ پایان در دوره گزارشگیری</param>
        /// <param name="gridOptions">فیلترهای سریع مورد نیاز که لازم است پیش از تجمیع اطلاعات گزارش اعمال شوند</param>
        /// <returns>آرتیکل های اولیه برای استفاده در گزارش</returns>
        Task<List<TModel>> GetRawReportByDateLinesAsync<TModel>(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions);

        /// <summary>
        /// آرتیکل های فیلتر شده و تجمیع نشده و به تفکیک شعبه را برای یک گزارش خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نیاز برای نگاشت اطلاعات آرتیکل</typeparam>
        /// <param name="query">پرس و جوی آماده شده برای اعمال فیلترهای اضافه</param>
        /// <param name="from">تاریخ شروع در دوره گزارشگیری</param>
        /// <param name="to">تاریخ پایان در دوره گزارشگیری</param>
        /// <param name="gridOptions">فیلترهای سریع مورد نیاز که لازم است پیش از تجمیع اطلاعات گزارش اعمال شوند</param>
        /// <returns>آرتیکل های اولیه برای استفاده در گزارش</returns>
        Task<List<TModel>> GetRawReportByDateByBranchLinesAsync<TModel>(IQueryable<VoucherLine> query,
            DateTime from, DateTime to, GridOptions gridOptions);

        /// <summary>
        /// آرتیکل های فیلتر شده و تجمیع نشده را برای یک گزارش خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نیاز برای نگاشت اطلاعات آرتیکل</typeparam>
        /// <param name="query">پرس و جوی آماده شده برای اعمال فیلترهای اضافه</param>
        /// <param name="from">شماره اولین سند در دوره گزارشگیری</param>
        /// <param name="to">شماره آخرین سند در دوره گزارشگیری</param>
        /// <param name="gridOptions">فیلترهای سریع مورد نیاز که لازم است پیش از تجمیع اطلاعات گزارش اعمال شوند</param>
        /// <returns>آرتیکل های اولیه برای استفاده در گزارش</returns>
        Task<List<TModel>> GetRawReportByNumberLinesAsync<TModel>(IQueryable<VoucherLine> query,
            int from, int to, GridOptions gridOptions);

        /// <summary>
        /// آرتیکل های فیلتر شده و تجمیع نشده و به تفکیک شعبه را برای یک گزارش خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نیاز برای نگاشت اطلاعات آرتیکل</typeparam>
        /// <param name="query">پرس و جوی آماده شده برای اعمال فیلترهای اضافه</param>
        /// <param name="from">تاریخ شروع در دوره گزارشگیری</param>
        /// <param name="to">تاریخ پایان در دوره گزارشگیری</param>
        /// <param name="gridOptions">فیلترهای سریع مورد نیاز که لازم است پیش از تجمیع اطلاعات گزارش اعمال شوند</param>
        /// <returns>آرتیکل های اولیه برای استفاده در گزارش</returns>
        Task<List<TModel>> GetRawReportByNumberByBranchLinesAsync<TModel>(IQueryable<VoucherLine> query,
            int from, int to, GridOptions gridOptions);

        /// <summary>
        /// آرتیکل های داده شده را بر حسب یکی از سطوح درختی مولفه حساب گروه بندی می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی ردیف های گزارش در نمای لیستی</typeparam>
        /// <param name="lines">فهرست آرتیکل های داده شده برای گروه بندی</param>
        /// <param name="groupLevel">سطح مورد نظر در ساختار درختی برای گروه بندی</param>
        /// <param name="lineFilter">فیلتر تکمیلی مورد نیاز برای اعمال پیش از گروه بندی</param>
        /// <returns>مجموعه گروه های به دست آمده با توجه به سایر پارامترهای متد</returns>
        IEnumerable<IGrouping<string, TModel>> GetTurnoverGroups<TModel>(
            IEnumerable<TModel> lines, int groupLevel, Func<TModel, bool> lineFilter)
            where TModel : class, IAccountView;

        /// <summary>
        /// طول کد یکی از مولفه های حساب را در سطح داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="level">شماره سطح مورد نظر در ساختار درختی</param>
        /// <returns>طول کد مولفه حساب در سطح مورد نظر</returns>
        int GetLevelCodeLength(int viewId, int level);

        /// <summary>
        /// مجموعه آرتیکل های داده شده را بر حسب یک فیلد گروه بندی می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی ردیف های گزارش در نمای لیستی</typeparam>
        /// <typeparam name="TKey1">نوع فیلد مورد نظر برای گروه بندی</typeparam>
        /// <param name="items">مجموعه آرتیکل های داده شده برای گروه بندی</param>
        /// <param name="selector1">عبارت لامدای مورد نظر برای گروه بندی</param>
        /// <returns>مجموعه ای از گروه های به دست آمده</returns>
        IEnumerable<IGrouping<TKey1, TModel>> GetGroupByItems<TModel, TKey1>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1);

        /// <summary>
        /// مجموعه آرتیکل های داده شده را بر حسب دو فیلد گروه بندی می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی ردیف های گزارش در نمای لیستی</typeparam>
        /// <typeparam name="TKey1">نوع فیلد اول مورد نظر برای گروه بندی</typeparam>
        /// <typeparam name="TKey2">نوع فیلد دوم مورد نظر برای گروه بندی</typeparam>
        /// <param name="items">مجموعه آرتیکل های داده شده برای گروه بندی</param>
        /// <param name="selector1">عبارت لامدای اول برای گروه بندی</param>
        /// <param name="selector2">عبارت لامدای دوم برای گروه بندی</param>
        /// <returns>مجموعه ای از گروه های به دست آمده</returns>
        IEnumerable<IGrouping<TKey2, TModel>> GetGroupByItems<TModel, TKey1, TKey2>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1, Func<TModel, TKey2> selector2);

        /// <summary>
        /// مجموعه آرتیکل های داده شده را بر حسب سه فیلد گروه بندی می کند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی ردیف های گزارش در نمای لیستی</typeparam>
        /// <typeparam name="TKey1">نوع فیلد اول مورد نظر برای گروه بندی</typeparam>
        /// <typeparam name="TKey2">نوع فیلد دوم مورد نظر برای گروه بندی</typeparam>
        /// <typeparam name="TKey3">نوع فیلد سوم مورد نظر برای گروه بندی</typeparam>
        /// <param name="items">مجموعه آرتیکل های داده شده برای گروه بندی</param>
        /// <param name="selector1">عبارت لامدای اول برای گروه بندی</param>
        /// <param name="selector2">عبارت لامدای دوم برای گروه بندی</param>
        /// <param name="selector3">عبارت لامدای سوم برای گروه بندی</param>
        /// <returns>مجموعه ای از گروه های به دست آمده</returns>
        IEnumerable<IGrouping<TKey3, TModel>> GetGroupByItems<TModel, TKey1, TKey2, TKey3>(
            IEnumerable<TModel> items, Func<TModel, TKey1> selector1, Func<TModel, TKey2> selector2,
            Func<TModel, TKey3> selector3);
    }
}
