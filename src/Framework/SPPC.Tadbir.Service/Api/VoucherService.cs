using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines operations required for working with financial vouchers in the application.
    /// </summary>
    public class VoucherService : IVoucherService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoucherService"/> class
        /// </summary>
        /// <param name="apiClient">Object that wraps common operations for calling a Web API service</param>
        public VoucherService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Retrieves all voucher items that are currently defined in the specified fiscal period and branch.
        /// </summary>
        /// <param name="fpId">Identifier of an existing fiscal period</param>
        /// <param name="branchId">Identifier of an existing corporate branch</param>
        /// <returns>Collection of all vouchers in the specified fiscal period</returns>
        public IEnumerable<VoucherViewModel> GetVouchers(int fpId, int branchId)
        {
            var vouchers = _apiClient.Get<IEnumerable<VoucherViewModel>>(
                VoucherApi.FiscalPeriodBranchVouchers, fpId, branchId);
            return vouchers;
        }

        /// <summary>
        /// Inserts or updates a financial voucher.
        /// </summary>
        /// <param name="voucher">Financial voucher to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of service operation</returns>
        public ServiceResponse SaveVoucher(VoucherViewModel voucher)
        {
            Verify.ArgumentNotNull(voucher, "voucher");
            ServiceResponse response = null;
            if (voucher.Id == 0)
            {
                response = _apiClient.Insert(voucher, VoucherApi.Vouchers);
            }
            else
            {
                response = _apiClient.Update(voucher, VoucherApi.Voucher, voucher.Id);
            }

            return response;
        }

        /// <summary>
        /// Deletes a financial voucher specified by unique identifier.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to delete</param>
        public void DeleteVoucher(int voucherId)
        {
            _apiClient.Delete(VoucherApi.Voucher, voucherId);
        }

        /// <summary>
        /// Updates operational status of a financial voucher to Prepared.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to prepare</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse PrepareVoucher(int voucherId, string paraph = null)
        {
            var detail = new ActionDetailViewModel() { Paraph = paraph };
            var response = _apiClient.Update(detail, VoucherApi.PrepareVoucher, voucherId);
            return response;
        }

        /// <summary>
        /// Updates operational status of a financial voucher to Reviewed.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to review</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse ReviewVoucher(int voucherId, string paraph = null)
        {
            var detail = new ActionDetailViewModel() { Paraph = paraph };
            var response = _apiClient.Update(detail, VoucherApi.ReviewVoucher, voucherId);
            return response;
        }

        /// <summary>
        /// Updates operational status of a reviewed financial voucher to Prepred,
        /// meaning it needs to be reviewed again.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to reject</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse RejectVoucher(int voucherId, string paraph = null)
        {
            var detail = new ActionDetailViewModel() { Paraph = paraph };
            var response = _apiClient.Update(detail, VoucherApi.RejectVoucher, voucherId);
            return response;
        }

        /// <summary>
        /// Updates operational status of a financial voucher to Confirmed.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to confirm</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse ConfirmVoucher(int voucherId, string paraph = null)
        {
            var detail = new ActionDetailViewModel() { Paraph = paraph };
            var response = _apiClient.Update(detail, VoucherApi.ConfirmVoucher, voucherId);
            return response;
        }

        /// <summary>
        /// Updates operational status of a financial voucher to Approved.
        /// </summary>
        /// <param name="voucherId">Unique identifier of the voucher to approve</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse ApproveVoucher(int voucherId, string paraph = null)
        {
            var detail = new ActionDetailViewModel() { Paraph = paraph };
            var response = _apiClient.Update(detail, VoucherApi.ApproveVoucher, voucherId);
            return response;
        }

        /// <summary>
        /// Updates operational status of multiple financial vouchers to Prepared.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to prepare</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse PrepareVouchers(IEnumerable<int> vouchers, string paraph = null)
        {
            var detail = GetGroupOperationDetail(vouchers, paraph);
            var response = _apiClient.Update(detail, VoucherApi.PrepareVouchers);
            return response;
        }

        /// <summary>
        /// Updates operational status of multiple financial vouchers to Reviewed.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to review</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse ReviewVouchers(IEnumerable<int> vouchers, string paraph = null)
        {
            var detail = GetGroupOperationDetail(vouchers, paraph);
            var response = _apiClient.Update(detail, VoucherApi.ReviewVouchers);
            return response;
        }

        /// <summary>
        /// Updates operational status of multiple reviewed financial voucher to Prepared,
        /// meaning they need to be reviewed again.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to reject</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse RejectVouchers(IEnumerable<int> vouchers, string paraph = null)
        {
            var detail = GetGroupOperationDetail(vouchers, paraph);
            var response = _apiClient.Update(detail, VoucherApi.RejectVouchers);
            return response;
        }

        /// <summary>
        /// Updates operational status of multiple financial vouchers to Confirmed.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to confirm</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse ConfirmVouchers(IEnumerable<int> vouchers, string paraph = null)
        {
            var detail = GetGroupOperationDetail(vouchers, paraph);
            var response = _apiClient.Update(detail, VoucherApi.ConfirmVouchers);
            return response;
        }

        /// <summary>
        /// Updates operational status of multiple financial vouchers to Approved.
        /// </summary>
        /// <param name="vouchers">Unique identifiers of vouchers to approve</param>
        /// <param name="paraph">Optional remarks that user can enter before completing the action</param>
        public ServiceResponse ApproveVouchers(IEnumerable<int> vouchers, string paraph = null)
        {
            var detail = GetGroupOperationDetail(vouchers, paraph);
            var response = _apiClient.Update(detail, VoucherApi.ApproveVouchers);
            return response;
        }

        /// <summary>
        /// Inserts or updates a financial voucher article.
        /// </summary>
        /// <param name="article">Article to insert or update</param>
        /// <returns>A <see cref="ServiceResponse"/> object that contains details about the result of service operation</returns>
        public ServiceResponse SaveArticle(VoucherLineViewModel article)
        {
            Verify.ArgumentNotNull(article, "article");
            var response = new ServiceResponse();
            if (article.Id == 0)
            {
                response = _apiClient.Insert(article, VoucherApi.VoucherArticles, article.VoucherId);
            }
            else
            {
                response = _apiClient.Update(article, VoucherApi.VoucherArticle, article.Id);
            }

            return response;
        }

        /// <summary>
        /// Retrieves a single voucher article specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the voucher article to retrieve</param>
        /// <returns>Voucher article as a <see cref="VoucherLineViewModel"/> object</returns>
        public VoucherLineViewModel GetArticle(int articleId)
        {
            var article = _apiClient.Get<VoucherLineViewModel>(
                VoucherApi.VoucherArticle, articleId);
            return article;
        }

        /// <summary>
        /// Deletes a financial voucher line (article) specified by unique identifier.
        /// </summary>
        /// <param name="articleId">Unique identifier of the article to delete</param>
        public void DeleteArticle(int articleId)
        {
            _apiClient.Delete(VoucherApi.VoucherArticle, articleId);
        }

        private static ActionDetailViewModel GetGroupOperationDetail(IEnumerable<int> vouchers, string paraph)
        {
            Verify.ArgumentNotNull(vouchers, "vouchers");
            var detail = new ActionDetailViewModel() { Paraph = paraph };
            Array.ForEach(vouchers.ToArray(), id => detail.Items.Add(id));
            return detail;
        }

        private IApiClient _apiClient;
    }
}
