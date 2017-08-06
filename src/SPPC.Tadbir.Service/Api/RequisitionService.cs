using System;
using System.Collections.Generic;
using System.Linq;
using BabakSoft.Platform.Common;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات درخواست های کالا را پیاده سازی می کند.
    /// </summary>
    public class RequisitionService : IRequisitionService
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="apiClient">پیاده سازی اینترفیس مربوط به کار با سرویس</param>
        public RequisitionService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// مجموعه ای از اطلاعات درخواست های کالا در یک دوره مالی و یک شعبه خاص را برمی گرداند
        /// </summary>
        /// <param name="fpId">کد دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد دیتابیسی یکی از شعبه های موجود</param>
        /// <returns>مجموعه ای از درخواست های کالا در یک دوره مالی و شعبه خاص</returns>
        public IEnumerable<VoucherSummaryViewModel> GetRequisitions(int fpId, int branchId)
        {
            var requisitions = _apiClient.Get<IEnumerable<VoucherSummaryViewModel>>(
                RequisitionApi.FiscalPeriodBranchRequisitions, fpId, branchId);
            return requisitions;
        }

        public void SaveRequisition(RequisitionVoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            if (voucher.Id == 0)
            {
                _apiClient.Insert(voucher, RequisitionApi.Requisitions);
            }
        }

        private IApiClient _apiClient;
    }
}
