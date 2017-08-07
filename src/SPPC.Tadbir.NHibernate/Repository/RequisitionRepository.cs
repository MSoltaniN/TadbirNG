using System;
using System.Collections.Generic;
using System.Linq;
using BabakSoft.Platform.Common;
using BabakSoft.Platform.Persistence;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Inventory;
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

        public RequisitionFullViewModel GetRequisitionDetails(int voucherId)
        {
            var voucherDetails = default(RequisitionFullViewModel);
            var repository = _unitOfWork.GetRepository<RequisitionVoucher>();
            var voucher = repository.GetByID(voucherId);
            if (voucher != null)
            {
                voucherDetails = _mapper.Map<RequisitionFullViewModel>(voucher);
            }

            return voucherDetails;
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
            }
            else
            {
                var existing = repository.GetByID(voucher.Id);
                if (existing != null)
                {
                    UpdateExistingVoucher(voucher, existing);
                    repository.Update(existing);
                }
            }

            _unitOfWork.Commit();
        }

        private static void UpdateExistingVoucher(RequisitionVoucherViewModel voucher, RequisitionVoucher existing)
        {
            existing.Type = new RequisitionVoucherType() { Id = voucher.TypeId };
            existing.Requester = new BusinessPartner() { Id = voucher.RequesterId };
            existing.Receiver = new BusinessPartner() { Id = voucher.ReceiverId };
            existing.RequesterUnit = new BusinessUnit() { Id = voucher.RequesterUnitId };
            existing.ReceiverUnit = new BusinessUnit() { Id = voucher.ReceiverUnitId };
            existing.Warehouse = new Warehouse() { Id = voucher.WarehouseId };
            existing.FullAccount.Account = new Account() { Id = voucher.FullAccount.AccountId };
            existing.FullAccount.Detail = new DetailAccount() { Id = voucher.FullAccount.DetailId };
            existing.FullAccount.CostCenter = new CostCenter() { Id = voucher.FullAccount.CostCenterId };
            existing.FullAccount.Project = new Project() { Id = voucher.FullAccount.ProjectId };
            var mainAction = existing.Document.Actions.First();
            mainAction.ModifiedBy = new User() { Id = voucher.Document.Actions.First().ModifiedById };
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
