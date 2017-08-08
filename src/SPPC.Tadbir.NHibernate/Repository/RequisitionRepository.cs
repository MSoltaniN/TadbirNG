using System;
using System.Collections.Generic;
using System.Linq;
using BabakSoft.Platform.Common;
using BabakSoft.Platform.Persistence;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Inventory;
using SPPC.Tadbir.Model.Procurement;
using SPPC.Tadbir.ViewModel.Core;
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

        public DocumentViewModel GetRequisitionDocument(int voucherId)
        {
            var document = default(DocumentViewModel);
            var repository = _unitOfWork.GetRepository<RequisitionVoucher>();
            var voucher = repository.GetByID(voucherId);
            if (voucher != null && voucher.Document != null)
            {
                document = _mapper.Map<DocumentViewModel>(voucher.Document);
            }

            return document;
        }

        public void SaveRequisition(RequisitionVoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            var repository = _unitOfWork.GetRepository<RequisitionVoucher>();
            if (voucher.Id == 0)
            {
                var newVoucher = _mapper.Map<RequisitionVoucher>(voucher);
                UpdateRequisitionAction(newVoucher);
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

        public RequisitionVoucherLineViewModel GetRequisitionLine(int lineId)
        {
            var line = default(RequisitionVoucherLineViewModel);
            var repository = _unitOfWork.GetRepository<RequisitionVoucherLine>();
            var existing = repository.GetByID(lineId);
            if (existing != null)
            {
                line = _mapper.Map<RequisitionVoucherLineViewModel>(existing);
            }

            return line;
        }

        public void SaveRequisitionLine(RequisitionVoucherLineViewModel line)
        {
            Verify.ArgumentNotNull(line, "line");
            var repository = _unitOfWork.GetRepository<RequisitionVoucherLine>();
            var documentRepository = _unitOfWork.GetRepository<Document>();
            if (line.Id == 0)
            {
                var newLine = _mapper.Map<RequisitionVoucherLine>(line);
                UpdateRequisitionLineAction(newLine);
                var document = documentRepository.GetByID(line.Document.Id);
                var lineAction = newLine.Document.Actions
                    .Where(act => act.LineId == line.No)
                    .Single();
                lineAction.Document = document;
                document.Actions.Add(lineAction);
                repository.Insert(newLine);
                documentRepository.Update(document);
            }
            else
            {
                var existing = repository.GetByID(line.Id);
                if (existing != null)
                {
                    UpdateExistingVoucherLine(existing, line);
                    repository.Update(existing);
                }
            }

            _unitOfWork.Commit();
        }

        private static void UpdateExistingVoucher(RequisitionVoucherViewModel voucher, RequisitionVoucher existing)
        {
            existing.No = voucher.No;
            existing.OrderedDate = JalaliDateTime.Parse(voucher.OrderedDate).ToGregorian();
            existing.PromisedDate = !String.IsNullOrWhiteSpace(voucher.PromisedDate)
                ? JalaliDateTime.Parse(voucher.PromisedDate).ToGregorian()
                : (DateTime?)null;
            existing.RequiredDate = !String.IsNullOrWhiteSpace(voucher.RequiredDate)
                ? JalaliDateTime.Parse(voucher.RequiredDate).ToGregorian()
                : (DateTime?)null;
            existing.Reason = voucher.Reason;
            existing.Reference = voucher.Reference;
            existing.WarehouseComment = voucher.WarehouseComment;
            existing.Description = voucher.Description;
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

        private void UpdateExistingVoucherLine(RequisitionVoucherLine existing, RequisitionVoucherLineViewModel line)
        {
            existing.No = line.No;
            existing.OrderedQuantity = line.OrderedQuantity;
            existing.DeliveredQuantity = line.DeliveredQuantity;
            existing.ReservedQuantity = line.ReservedQuantity;
            existing.LastOrderedQuantity = line.LastOrderedQuantity;
            existing.RequiredDate = JalaliDateTime.Parse(line.RequiredDate).ToGregorian();
            existing.PromisedDate = !String.IsNullOrWhiteSpace(line.PromisedDate)
                ? JalaliDateTime.Parse(line.PromisedDate).ToGregorian()
                : (DateTime?)null;
            existing.DeliveredDate = !String.IsNullOrWhiteSpace(line.DeliveredDate)
                ? JalaliDateTime.Parse(line.DeliveredDate).ToGregorian()
                : (DateTime?)null;
            existing.LastOrderedDate = !String.IsNullOrWhiteSpace(line.LastOrderedDate)
                ? JalaliDateTime.Parse(line.LastOrderedDate).ToGregorian()
                : (DateTime?)null;
            existing.Description = line.Description;
            existing.Warehouse = new Warehouse() { Id = line.WarehouseId };
            existing.Product = new Product() { Id = line.ProductId };
            existing.Uom = new UnitOfMeasurement() { Id = line.UomId };
            existing.FullAccount.Account = new Account() { Id = line.FullAccount.AccountId };
            existing.FullAccount.Detail = new DetailAccount() { Id = line.FullAccount.DetailId };
            existing.FullAccount.CostCenter = new CostCenter() { Id = line.FullAccount.CostCenterId };
            existing.FullAccount.Project = new Project() { Id = line.FullAccount.ProjectId };
            var lineAction = existing.Document.Actions
                .Where(act => act.LineId == line.No)
                .Single();
            lineAction.ModifiedBy = new User()
            {
                Id = line.Document.Actions
                        .Where(act => act.LineId == line.No)
                        .Single().ModifiedById
            };
        }

        private void UpdateRequisitionAction(RequisitionVoucher voucher)
        {
            if (voucher.Id == 0)
            {
                var mainAction = voucher.Document.Actions.First();
                mainAction.Document = voucher.Document;
                mainAction.CreatedDate = DateTime.Now;
            }
        }

        private void UpdateRequisitionLineAction(RequisitionVoucherLine line)
        {
            if (line.Id == 0)
            {
                var lineAction = line.Document.Actions
                    .Where(act => act.LineId == line.No)
                    .Single();
                lineAction.Document = line.Document;
                lineAction.CreatedDate = DateTime.Now;
                lineAction.ModifiedDate = DateTime.Now;
            }
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
