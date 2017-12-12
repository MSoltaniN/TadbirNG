using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Procurement;
using SPPC.Tadbir.ViewModel.Workflow;

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

        /// <summary>
        /// اطلاعات کامل یک درخواست کالا (شامل اطلاعات آرتیکل ها) را برمی گرداند
        /// </summary>
        /// <param name="id">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <returns>اطلاعات کامل درخواست کالا</returns>
        public RequisitionFullViewModel GetDetailRequisitionInfo(int id)
        {
            var requisition = _apiClient.Get<RequisitionFullViewModel>(RequisitionApi.RequisitionDetails, id);
            return requisition;
        }

        /// <summary>
        /// اطلاعات کامل یک سطر درخواست کالا را برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <param name="lineId">شناسه دیتابیسی سطر مورد نظر در درخواست کالا</param>
        /// <returns>اطلاعات کامل سطر درخواست کالا</returns>
        public RequisitionVoucherLineViewModel GetDetailRequisitionLineInfo(int voucherId, int lineId)
        {
            var line = _apiClient.Get<RequisitionVoucherLineViewModel>(
                RequisitionApi.RequisitionLine, voucherId, lineId);
            return line;
        }

        /// <summary>
        /// یک درخواست کالا را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucher">اطلاعات درخواست کالا</param>
        public void SaveRequisition(RequisitionVoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            if (voucher.Id == 0)
            {
                _apiClient.Insert(voucher, RequisitionApi.Requisitions);
            }
            else
            {
                _apiClient.Update(voucher, RequisitionApi.Requisition, voucher.Id);
            }
        }

        /// <summary>
        /// یک سطر درخواست کالا را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="line">اطلاعات سطر درخواست کالا</param>
        public void SaveRequisitionLine(RequisitionVoucherLineViewModel line)
        {
            Verify.ArgumentNotNull(line, "line");
            if (line.Id == 0)
            {
                _apiClient.Insert(line, RequisitionApi.RequisitionLines, line.VoucherId);
            }
            else
            {
                _apiClient.Update(line, RequisitionApi.RequisitionLine, line.VoucherId, line.Id);
            }
        }

        /// <summary>
        /// اطلاعات یک درخواست کالای موجود را حذف می کند.
        /// </summary>
        /// <param name="id">شناسه دیتابیسی یک درخواست کالای موجود</param>
        public void DeleteRequisition(int id)
        {
            _apiClient.Delete(RequisitionApi.Requisition, id);
        }

        /// <summary>
        /// اطلاعات یک سطر درخواست کالای موجود را حذف می کند.
        /// </summary>
        /// <param name="id">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <param name="lineId">شناسه دیتابیسی یک سطر درخواست کالای موجود</param>
        public void DeleteRequisitionLine(int id, int lineId)
        {
            _apiClient.Delete(RequisitionApi.RequisitionLine, id, lineId);
        }

        /// <summary>
        /// یک درخواست کالای موجود را به وضعیت تنظیم شده می برد
        /// </summary>
        /// <param name="id">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <param name="paraph">پاراف متنی اختیاری برای گیرنده کار</param>
        /// <returns>اطلاعات پاسخ دریافت شده از سرویس</returns>
        public ServiceResponse Prepare(int id, string paraph = null)
        {
            var detail = new ActionDetailViewModel() { Paraph = paraph };
            var response = _apiClient.Update(detail, RequisitionApi.Prepare, id);
            return response;
        }

        private IApiClient _apiClient;
    }
}
