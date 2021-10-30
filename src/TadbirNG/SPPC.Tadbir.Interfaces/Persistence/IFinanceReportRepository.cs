using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه و محاسبه اطلاعات گزارش های زیرسیستم حسابداری را تعریف می کند
    /// </summary>
    public interface IFinanceReportRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش خلاصه اسناد حسابداری</returns>
        Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByDateReportAsync(
            GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، نعداد سطرهای اطلاعاتی در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>تعداد سطرهای گزارش خلاصه اسناد حسابداری</returns>
        Task<int> GetVoucherSummaryByDateCountAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش فرم مرسوم سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند مالی مورد نظر برای چاپ</param>
        /// <param name="withDetail">مشخص می کند که آیا جزییات سطوح شناور نیز مورد نیاز است یا نه</param>
        /// <returns>اطلاعات گزارش فرم مرسوم سند</returns>
        Task<StandardVoucherViewModel> GetStandardVoucherFormAsync(int voucherNo, bool withDetail = false);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش چاپی "ساده - در سطح تفصیلی" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند حسابداری مورد نظر</param>
        /// <returns>اطلاعات سند حسابداری</returns>
        Task<StandardVoucherViewModel> GetVoucherByDetailAsync(int voucherNo);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش چاپی "مرکب - در سطح کل" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند حسابداری مورد نظر</param>
        /// <returns>اطلاعات سند حسابداری در سطح کل</returns>
        Task<StandardVoucherViewModel> GetVoucherByLedgerAsync(int voucherNo);

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش چاپی "مرکب - در سطح معین" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند حسابداری مورد نظر</param>
        /// <returns>اطلاعات سند حسابداری در سطح معین</returns>
        Task<StandardVoucherViewModel> GetVoucherBySubsidiaryAsync(int voucherNo);
    }
}
