using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات درخواست های کالا را تعریف می کند.
    /// </summary>
    public interface IRequisitionService
    {
        /// <summary>
        /// مجموعه ای از اطلاعات درخواست های کالا در یک دوره مالی و یک شعبه خاص را برمی گرداند
        /// </summary>
        /// <param name="fpId">کد دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد دیتابیسی یکی از شعبه های موجود</param>
        /// <returns>مجموعه ای از درخواست های کالا در یک دوره مالی و شعبه خاص</returns>
        IEnumerable<VoucherSummaryViewModel> GetRequisitions(int fpId, int branchId);

        /// <summary>
        /// اطلاعات کامل یک درخواست کالا (شامل اطلاعات آرتیکل ها) را برمی گرداند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <returns>اطلاعات کامل درخواست کالا</returns>
        RequisitionFullViewModel GetDetailRequisitionInfo(int id);

        /// <summary>
        /// اطلاعات کامل یک سطر درخواست کالا را برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <param name="lineId">شناسه دیتابیسی سطر مورد نظر در درخواست کالا</param>
        /// <returns>اطلاعات کامل سطر درخواست کالا</returns>
        RequisitionVoucherLineViewModel GetDetailRequisitionLineInfo(int voucherId, int lineId);

        /// <summary>
        /// یک درخواست کالا را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucher">اطلاعات درخواست کالا</param>
        void SaveRequisition(RequisitionVoucherViewModel voucher);

        /// <summary>
        /// یک سطر درخواست کالا را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="line">اطلاعات سطر درخواست کالا</param>
        void SaveRequisitionLine(RequisitionVoucherLineViewModel line);

        /// <summary>
        /// اطلاعات یک درخواست کالای موجود را حذف می کند.
        /// </summary>
        /// <param name="id">شناسه دیتابیسی یک درخواست کالای موجود</param>
        void DeleteRequisition(int id);

        /// <summary>
        /// اطلاعات یک سطر درخواست کالای موجود را حذف می کند.
        /// </summary>
        /// <param name="id">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <param name="lineId">شناسه دیتابیسی یک سطر درخواست کالای موجود</param>
        void DeleteRequisitionLine(int id, int lineId);

        /// <summary>
        /// یک درخواست کالای موجود را به وضعیت تنظیم شده می برد
        /// </summary>
        /// <param name="id">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <param name="paraph">پاراف متنی اختیاری برای گیرنده کار</param>
        /// <returns>اطلاعات پاسخ دریافت شده از سرویس</returns>
        ServiceResponse Prepare(int id, string paraph = null);
    }
}
