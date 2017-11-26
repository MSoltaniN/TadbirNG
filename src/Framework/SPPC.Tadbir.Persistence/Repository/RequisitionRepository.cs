using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
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

namespace SPPC.Tadbir.Persistence
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

        /// <summary>
        /// اطلاعات خلاصه یک درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی یک درخواست کالای موجود</param>
        /// <returns>اطلاعات نمایشی خلاصه برای درخواست کالا</returns>
        public VoucherSummaryViewModel GetRequisitionSummary(int voucherId)
        {
            var summary = default(VoucherSummaryViewModel);
            var repository = _unitOfWork.GetRepository<RequisitionVoucher>();
            var voucher = repository.GetByID(voucherId);
            if (voucher != null)
            {
                summary = _mapper.Map<VoucherSummaryViewModel>(voucher);
            }

            return summary;
        }

        /// <summary>
        /// اطلاعات کامل یک درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// اطلاعات مستند مرتبط با یک درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// اطلاعات یک سطر درخواست کالا را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// آخرین اطلاعات یک درخواست کالا را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="voucher">اطلاعات وارد شده برای درخواست کالا</param>
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

        /// <summary>
        /// آخرین اطلاعات یک سطر درخواست کالا را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="line">اطلاعات وارد شده برای سطر درخواست کالا</param>
        public void SaveRequisitionLine(RequisitionVoucherLineViewModel line)
        {
            Verify.ArgumentNotNull(line, "line");
            var repository = _unitOfWork.GetRepository<RequisitionVoucherLine>();
            var actionRepository = _unitOfWork.GetRepository<DocumentAction>();
            if (line.Id == 0)
            {
                var newLine = _mapper.Map<RequisitionVoucherLine>(line);
                UpdateRequisitionLineAction(newLine);
                newLine.Action.Document = new Document() { Id = line.DocumentId };
                actionRepository.Insert(newLine.Action);
                repository.Insert(newLine);
                newLine.Action.LineId = newLine.Id;
                actionRepository.Update(newLine.Action);
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

        /// <summary>
        /// اطلاعات یک درخواست کالای موجود را از دیتابیس حذف می کند.
        /// </summary>
        /// <param name="voucherId">شناسه یک درخواست کالای موجود</param>
        public void DeleteRequisition(int voucherId)
        {
            var repository = _unitOfWork.GetRepository<RequisitionVoucher>();
            var documentRepository = _unitOfWork.GetRepository<Document>();
            var voucher = repository.GetByID(voucherId);
            if (voucher != null)
            {
                voucher.Document.Actions.Clear();
                documentRepository.Update(voucher.Document);
                voucher.Lines.Clear();
                repository.Update(voucher);
                repository.Delete(voucher);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// اطلاعات یک سطر درخواست کالای موجود را از محل ذخیره حذف می کند.
        /// </summary>
        /// <param name="lineId">شناسه یک سطر درخواست کالای موجود</param>
        public void DeleteRequisitionLine(int lineId)
        {
            var repository = _unitOfWork.GetRepository<RequisitionVoucherLine>();
            var actionRepository = _unitOfWork.GetRepository<DocumentAction>();
            var line = repository.GetByID(lineId);
            if (line != null)
            {
                var action = line.Action;
                repository.Delete(line);
                actionRepository.Delete(action);
                _unitOfWork.Commit();
            }
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

        private static void UpdateExistingVoucherLine(RequisitionVoucherLine existing, RequisitionVoucherLineViewModel line)
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
            existing.Action.ModifiedDate = DateTime.Now;
            existing.Action.ModifiedBy = new User()
            {
                Id = line.DocumentAction.ModifiedById
            };
        }

        private static void UpdateRequisitionAction(RequisitionVoucher voucher)
        {
            if (voucher.Id == 0)
            {
                var mainAction = voucher.Document.Actions.First();
                mainAction.Document = voucher.Document;
                mainAction.CreatedDate = DateTime.Now;
                mainAction.ModifiedDate = DateTime.Now;
            }
        }

        private static void UpdateRequisitionLineAction(RequisitionVoucherLine line)
        {
            if (line.Id == 0)
            {
                line.Action.CreatedDate = DateTime.Now;
                line.Action.ModifiedDate = DateTime.Now;
            }
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
