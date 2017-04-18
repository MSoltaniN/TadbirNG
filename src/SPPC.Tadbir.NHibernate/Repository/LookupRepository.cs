using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SwForAll.Platform.Persistence;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Provides repository operations for getting different types of key/value collections (lookups) from
    /// the underlying database.
    /// </summary>
    public class LookupRepository : ILookupRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookupRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public LookupRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all financial account items in the specified fiscal period as a collection of
        /// <see cref="KeyValue"/> objects. The key for each entry is the unique identifier of corresponding
        /// account in database.
        /// </summary>
        /// <param name="fpId">Unique identifier of an existing fiscal period</param>
        /// <param name="branchId">Unique identifier of the branch to look for accounts</param>
        /// <returns>Collection of all account items in the specified fiscal period.</returns>
        public IEnumerable<KeyValue> GetAccounts(int fpId, int branchId)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var accounts = repository
                .GetByCriteria(acc => acc.FiscalPeriod.Id == fpId
                    && acc.Branch.Id == branchId)
                .OrderBy(acc => acc.Code)
                .Select(acc => _mapper.Map<KeyValue>(acc));
            return accounts;
        }

        /// <summary>
        /// Retrieves all currency objects as a collection of <see cref="KeyValue"/> objects. The key for each
        /// entry is the unique identifier of corresponding currency in database.
        /// </summary>
        /// <returns>Collection of all currency items.</returns>
        public IEnumerable<KeyValue> GetCurrencies()
        {
            var repository = _unitOfWork.GetRepository<Currency>();
            var currencies = repository
                .GetAll()
                .OrderBy(curr => curr.Name)
                .Select(curr => _mapper.Map<KeyValue>(curr));
            return currencies;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
