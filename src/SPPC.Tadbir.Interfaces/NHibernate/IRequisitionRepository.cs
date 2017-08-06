using System;
using System.Collections.Generic;
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

        void SaveRequisition(RequisitionVoucherViewModel voucher);
    }
}
