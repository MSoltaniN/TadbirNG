using System;
using System.Collections.Generic;
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
    }
}
