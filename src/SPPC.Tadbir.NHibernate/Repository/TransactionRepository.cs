using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;
using SwForAll.Platform.Persistence;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Provides operations required for managing a single transaction and its child items in the underlying database.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class using specified
        /// unit of work implementation and domain mapper.
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> implementation to use for all database operations
        /// in this repository.</param>
        /// <param name="mapper">Domain mapper to use for mapping between entitiy and view model classes</param>
        /// </summary>
        public TransactionRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all transactions in specified fiscal period from repository.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <returns>A collection of <see cref="TransactionViewModel"/> objects retrieved from repository</returns>
        public IList<TransactionViewModel> GetTransactions(int fpId)
        {
            var repository = _unitOfWork.GetRepository<Transaction>();
            var transactions = repository
                .GetByCriteria(txn => txn.FiscalPeriod.Id == fpId)
                .OrderBy(txn => txn.Date)
                .Select(item => _mapper.Map<TransactionViewModel>(item))
                .ToList();
            return transactions;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
