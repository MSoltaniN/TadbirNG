using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Report;

namespace SPPC.Tadbir.Persistence
{
    public class ReportRepository : IReportRepository
    {
        public ReportRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecureRepository repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _repository.SetCurrentContext(userContext);
        }

        public async Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByDateReportAsync(
            GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var vouchers = await _repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher, voucher => voucher.Lines)
                .Select(voucher => _mapper.Map<VoucherSummaryViewModel>(voucher))
                .Apply(gridOptions)
                .ToListAsync();
            return vouchers;
        }

        public async Task<int> GetVoucherSummaryByDateCountAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            int count = await _repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher, voucher => voucher.Lines)
                .Select(voucher => _mapper.Map<VoucherSummaryViewModel>(voucher))
                .Apply(gridOptions)
                .CountAsync();
            return count;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly ISecureRepository _repository;
    }
}
