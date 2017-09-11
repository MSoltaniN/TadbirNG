using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// عملیات دیتابیسی مربوط به مدیریت درخواست های کار را در زیرسیستم تدارکات تعریف می کند.
    /// </summary>
    public interface IRequisitionRepository
    {
        /// <summary>
        /// کلیه درخواست های کالا را در دوره مالی و شعبه مشخص شده از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">کد دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد دیتابیسی یکی از شعبه های موجود</param>
        /// <returns>مجموعه ای از درخواست های کالا در یک دوره مالی و شعبه خاص</returns>
        IList<VoucherSummaryViewModel> GetRequisitions(int fpId, int branchId);

        /// <summary>
        /// اطلاعات کامل یک درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
        RequisitionFullViewModel GetRequisitionDetails(int voucherId);

        /// <summary>
        /// اطلاعات مستند مرتبط با یک درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
        DocumentViewModel GetRequisitionDocument(int voucherId);

        /// <summary>
        /// اطلاعات یک سطر درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        RequisitionVoucherLineViewModel GetRequisitionLine(int lineId);

        /// <summary>
        /// آخرین اطلاعات یک درخواست کالا را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucher">اطلاعات وارد شده برای درخواست کالا</param>
        void SaveRequisition(RequisitionVoucherViewModel voucher);

        /// <summary>
        /// آخرین اطلاعات یک سطر درخواست کالا را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="line">اطلاعات وارد شده برای سطر درخواست کالا</param>
        void SaveRequisitionLine(RequisitionVoucherLineViewModel line);

        /// <summary>
        /// اطلاعات یک درخواست کالای موجود را از محل ذخیره حذف می کند.
        /// </summary>
        /// <param name="voucherId">شناسه یک درخواست کالای موجود</param>
        void DeleteRequisition(int voucherId);

        /// <summary>
        /// اطلاعات یک سطر درخواست کالای موجود را از محل ذخیره حذف می کند.
        /// </summary>
        /// <param name="lineId">شناسه یک سطر درخواست کالای موجود</param>
        void DeleteRequisitionLine(int lineId);
    }
}
