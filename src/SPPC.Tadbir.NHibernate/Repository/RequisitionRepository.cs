using System;
using System.Collections.Generic;
using System.Linq;
using BabakSoft.Platform.Common;
using BabakSoft.Platform.Persistence;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Procurement;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// عملیات دیتابیسی مربوط به مدیریت درخواست های کار را در زیرسیستم تدارکات پیاده سازی می کند.
    /// </summary>
    public class RequisitionRepository : IRequisitionRepository
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند.
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public RequisitionRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// کلیه درخواست های کالا را در دوره مالی و شعبه مشخص شده از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">کد دیتابیسی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد دیتابیسی یکی از شعبه های موجود</param>
        /// <returns>مجموعه ای از درخواست های کالا در یک دوره مالی و شعبه خاص</returns>
        public IList<VoucherSummaryViewModel> GetRequisitions(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<RequisitionVoucher>();
            var requisitions = repository
                .GetByCriteria(req => req.FiscalPeriod.Id == fpId
                    && req.Branch.Id == branchId)
                .Select(item => _mapper.Map<VoucherSummaryViewModel>(item))
                .ToList();
            return requisitions;
        }

        public void SaveRequisition(RequisitionVoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            var repository = _unitOfWork.GetRepository<RequisitionVoucher>();
            if (voucher.Id == 0)
            {
                var newVoucher = _mapper.Map<RequisitionVoucher>(voucher);
                PrepareRequisitionActions(newVoucher);
                repository.Insert(newVoucher);
                _unitOfWork.Commit();
            }
        }

        private void PrepareRequisitionActions(RequisitionVoucher voucher)
        {
            Array.ForEach(
                voucher.Document.Actions.ToArray(),
                act =>
                {
                    act.Document = voucher.Document;
                    act.CreatedDate = DateTime.Now;
                    act.ModifiedDate = DateTime.Now;
                });
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
