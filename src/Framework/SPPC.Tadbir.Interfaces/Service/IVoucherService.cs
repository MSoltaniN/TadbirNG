using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines operations required for working with financial vouchers in the application.
    /// </summary>
    public interface IVoucherService
    {
        #region Voucher CRUD Operations

        /// <summary>
        /// Retrieves all voucher items that are currently defined in the specified fiscal period and branch.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>Collection of all vouchers in the specified fiscal period</returns>
        IEnumerable<VoucherViewModel> GetVouchers(int fpId, int branchId);

        /// <summary>
        /// Inserts or updates a financial voucher.
        /// </summary>
        /// <param name="voucher">Financial voucher to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of service operation</returns>
        ServiceResponse SaveVoucher(VoucherViewModel voucher);

        /// <summary>
        /// Deletes a financial voucher specified by unique identifier.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to delete</param>
        void DeleteVoucher(int voucherId);

        #endregion

        #region Article CRUD Operations

        /// <summary>
        /// Inserts or updates a financial voucher article.
        /// </summary>
        /// <param name="article">Article to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of service operation</returns>
        ServiceResponse SaveArticle(VoucherLineViewModel article);

        /// <summary>
        /// Retrieves a single voucher article specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the voucher article to retrieve</param>
        /// <returns>Voucher article as a <see cref="VoucherLineViewModel"/> object</returns>
        VoucherLineViewModel GetArticle(int articleId);

        /// <summary>
        /// Deletes a financial voucher line (article) specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the article to delete</param>
        void DeleteArticle(int articleId);

        #endregion

        #region Voucher Workflow Operations

        /// <summary>
        /// Updates operational status of a financial voucher to Prepared.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to prepare</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse PrepareVoucher(int voucherId, string paraph = null);

        /// <summary>
        /// Updates operational status of a financial voucher to Reviewed.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to review</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ReviewVoucher(int voucherId, string paraph = null);

        /// <summary>
        /// Updates operational status of a reviewed financial voucher to Prepared,
        /// meaning it needs to be reviewed again.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to reject</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse RejectVoucher(int voucherId, string paraph = null);

        /// <summary>
        /// Updates operational status of a financial voucher to Confirmed.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to confirm</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ConfirmVoucher(int voucherId, string paraph = null);

        /// <summary>
        /// Updates operational status of a financial voucher to Approved.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to approve</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ApproveVoucher(int voucherId, string paraph = null);

        /// <summary>
        /// Updates operational status of multiple financial vouchers to Prepared.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to prepare</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse PrepareVouchers(IEnumerable<int> vouchers, string paraph = null);

        /// <summary>
        /// Updates operational status of multiple financial vouchers to Reviewed.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to review</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ReviewVouchers(IEnumerable<int> vouchers, string paraph = null);

        /// <summary>
        /// Updates operational status of multiple reviewed financial voucher to Prepared,
        /// meaning they need to be reviewed again.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to reject</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse RejectVouchers(IEnumerable<int> vouchers, string paraph = null);

        /// <summary>
        /// Updates operational status of multiple financial vouchers to Confirmed.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to confirm</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ConfirmVouchers(IEnumerable<int> vouchers, string paraph = null);

        /// <summary>
        /// Updates operational status of multiple financial vouchers to Approved.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to approve</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        ServiceResponse ApproveVouchers(IEnumerable<int> vouchers, string paraph = null);

        #endregion
    }
}
