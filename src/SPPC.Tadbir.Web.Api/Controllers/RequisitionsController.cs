using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BabakSoft.Platform.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.NHibernate;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Procurement;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public class RequisitionsController : ApiController
    {
        public RequisitionsController(IRequisitionRepository repository, ISecurityContextManager contextManager)
        {
            Verify.ArgumentNotNull(contextManager, "contextManager");
            _repository = repository;
            _userContext = contextManager.CurrentContext;
        }

        // GET: api/requisitions/fp/{fpId:int}/branch/{branchId:int}
        [Route(RequisitionApi.FiscalPeriodBranchRequisitionsUrl)]
        public IHttpActionResult GetRequisitions(int fpId, int branchId)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var requisitions = _repository.GetRequisitions(fpId, branchId);
            return Json(requisitions);
        }

        // POST: api/requisitions
        [Route(RequisitionApi.RequisitionsUrl)]
        public IHttpActionResult PostNewRequisitionVoucher([FromBody] RequisitionVoucherViewModel voucher)
        {
            if (voucher == null)
            {
                return BadRequest("Could not post new requisition because a 'null' value was provided.");
            }

            SetVoucherDocument(voucher);
            _repository.SaveRequisition(voucher);
            return StatusCode(HttpStatusCode.Created);
        }

        private static string GenerateNumber()
        {
            return Guid.NewGuid()
                .ToString()
                .Replace("{", String.Empty)
                .Replace("}", String.Empty)
                .Replace("-", String.Empty)
                .Substring(0, 8);
        }

        private void SetVoucherDocument(RequisitionVoucherViewModel voucher)
        {
            var document = new DocumentViewModel()
            {
                No = GenerateNumber(),
                OperationalStatus = DocumentStatus.Created,
                StatusId = (int)DocumentStatuses.Draft,
                TypeId = (int)DocumentTypes.RequisitionVoucher
            };
            document.Actions.Add(new DocumentActionViewModel()
            {
                CreatedById = _userContext.User.Id,
                ModifiedById = _userContext.User.Id
            });
            voucher.Document = document;
        }

        private IRequisitionRepository _repository;
        private ISecurityContext _userContext;
    }
}
