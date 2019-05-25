﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with financial vouchers and articles.
    /// </summary>
    public sealed class VoucherApi
    {
        private VoucherApi()
        {
        }

        /// <summary>
        /// API client URL for all vouchers defined in current environment
        /// </summary>
        public const string EnvironmentVouchers = "vouchers";

        /// <summary>
        /// API server route URL for all vouchers defined in current environment
        /// </summary>
        public const string EnvironmentVouchersUrl = "vouchers";

        /// <summary>
        /// API client URL for count of all vouchers defined in current environment
        /// </summary>
        public const string EnvironmentItemCount = "vouchers/count";

        /// <summary>
        /// API server route URL for count of all vouchers defined in current environment
        /// </summary>
        public const string EnvironmentItemCountUrl = "vouchers/count";

        /// <summary>
        /// API client URL for a single voucher specified by identifier
        /// </summary>
        public const string Voucher = "vouchers/{0}";

        /// <summary>
        /// API server route URL for a single voucher specified by identifier
        /// </summary>
        public const string VoucherUrl = "vouchers/{voucherId:min(1)}";

        /// <summary>
        /// API client URL for a newly initialized and saved voucher
        /// </summary>
        public const string NewVoucher = "vouchers/new";

        /// <summary>
        /// API server route URL for a newly initialized and saved voucher
        /// </summary>
        public const string NewVoucherUrl = "vouchers/new";

        /// <summary>
        /// API client URL for a single voucher specified by number
        /// </summary>
        public const string VoucherByNo = "vouchers/by-no/{0}";

        /// <summary>
        /// API server route URL for a single voucher specified by number
        /// </summary>
        public const string VoucherByNoUrl = "vouchers/by-no/{voucherNo:min(1)}";

        /// <summary>
        /// API client URL for the first voucher in current environment
        /// </summary>
        public const string FirstVoucher = "vouchers/first";

        /// <summary>
        /// API server route URL for the first voucher in current environment
        /// </summary>
        public const string FirstVoucherUrl = "vouchers/first";

        /// <summary>
        /// API client URL for previous voucher in current environment
        /// </summary>
        public const string PreviousVoucher = "vouchers/{0}/previous";

        /// <summary>
        /// API server route URL for previous voucher in current environment
        /// </summary>
        public const string PreviousVoucherUrl = "vouchers/{voucherNo:min(1)}/previous";

        /// <summary>
        /// API client URL for next voucher in current environment
        /// </summary>
        public const string NextVoucher = "vouchers/{0}/next";

        /// <summary>
        /// API server route URL for next voucher in current environment
        /// </summary>
        public const string NextVoucherUrl = "vouchers/{voucherNo:min(1)}/next";

        /// <summary>
        /// API client URL for the last voucher in current environment
        /// </summary>
        public const string LastVoucher = "vouchers/last";

        /// <summary>
        /// API server route URL for the last voucher in current environment
        /// </summary>
        public const string LastVoucherUrl = "vouchers/last";

        /// <summary>
        /// API client URL for preparing a single voucher specified by identifier
        /// </summary>
        public const string PrepareVoucher = "vouchers/{0}/prepare";

        /// <summary>
        /// API server route URL for preparing a single voucher specified by identifier
        /// </summary>
        public const string PrepareVoucherUrl = "vouchers/{voucherId:int}/prepare";

        /// <summary>
        /// API client URL for preparing multiple vouchers
        /// </summary>
        public const string PrepareVouchers = "vouchers/prepare";

        /// <summary>
        /// API server route URL for preparing multiple vouchers
        /// </summary>
        public const string PrepareVouchersUrl = "vouchers/prepare";

        /// <summary>
        /// API client URL for reviewing multiple vouchers
        /// </summary>
        public const string ReviewVouchers = "vouchers/review";

        /// <summary>
        /// API server route URL for reviewing multiple vouchers
        /// </summary>
        public const string ReviewVouchersUrl = "vouchers/review";

        /// <summary>
        /// API client URL for rejecting multiple vouchers
        /// </summary>
        public const string RejectVouchers = "vouchers/reject";

        /// <summary>
        /// API server route URL for rejecting multiple vouchers
        /// </summary>
        public const string RejectVouchersUrl = "vouchers/reject";

        /// <summary>
        /// API client URL for confirming multiple vouchers
        /// </summary>
        public const string ConfirmVouchers = "vouchers/confirm";

        /// <summary>
        /// API server route URL for confirming multiple vouchers
        /// </summary>
        public const string ConfirmVouchersUrl = "vouchers/confirm";

        /// <summary>
        /// API client URL for approving multiple vouchers
        /// </summary>
        public const string ApproveVouchers = "vouchers/approve";

        /// <summary>
        /// API server route URL for approving multiple vouchers
        /// </summary>
        public const string ApproveVouchersUrl = "vouchers/approve";

        /// <summary>
        /// API client URL for reviewing a single voucher specified by identifier
        /// </summary>
        public const string ReviewVoucher = "vouchers/{0}/review";

        /// <summary>
        /// API server route URL for reviewing a single voucher specified by identifier
        /// </summary>
        public const string ReviewVoucherUrl = "vouchers/{voucherId:min(1)}/review";

        /// <summary>
        /// API client URL for rejecting a reviewed voucher specified by identifier
        /// </summary>
        public const string RejectVoucher = "vouchers/{0}/reject";

        /// <summary>
        /// API server route URL for rejecting a reviewed voucher specified by identifier
        /// </summary>
        public const string RejectVoucherUrl = "vouchers/{voucherId:min(1)}/reject";

        /// <summary>
        /// API client URL for confirming a single voucher specified by identifier
        /// </summary>
        public const string ConfirmVoucher = "vouchers/{0}/confirm";

        /// <summary>
        /// API server route URL for confirming a single voucher specified by identifier
        /// </summary>
        public const string ConfirmVoucherUrl = "vouchers/{voucherId:min(1)}/confirm";

        /// <summary>
        /// API client URL for approving a single voucher specified by identifier
        /// </summary>
        public const string ApproveVoucher = "vouchers/{0}/approve";

        /// <summary>
        /// API server route URL for approving a single voucher specified by identifier
        /// </summary>
        public const string ApproveVoucherUrl = "vouchers/{voucherId:min(1)}/approve";

        /// <summary>
        /// API client URL for checking (changing document status to Checked) a single voucher specified by identifier
        /// </summary>
        public const string CheckVoucher = "vouchers/{0}/check";

        /// <summary>
        /// API server route URL for checking (changing document status to Checked) a single voucher specified by identifier
        /// </summary>
        public const string CheckVoucherUrl = "vouchers/{voucherId:min(1)}/check";

        /// <summary>
        /// API client URL for checking (changing document status to Checked) a single voucher specified by identifier
        /// </summary>
        public const string UncheckVoucher = "vouchers/{0}/uncheck";

        /// <summary>
        /// API server route URL for checking (changing document status to Checked) a single voucher specified by identifier
        /// </summary>
        public const string UncheckVoucherUrl = "vouchers/{voucherId:min(1)}/uncheck";

        /// <summary>
        /// API client URL for all articles in a single voucher specified by identifier
        /// </summary>
        public const string VoucherArticles = "vouchers/{0}/articles";

        /// <summary>
        /// API server route URL for all articles in a single voucher specified by identifier
        /// </summary>
        public const string VoucherArticlesUrl = "vouchers/{voucherId:min(1)}/articles";

        /// <summary>
        /// API client URL for count of all articles in a voucher specified by identifier
        /// </summary>
        public const string VoucherArticleCount = "vouchers/{0}/articles/count";

        /// <summary>
        /// API server route URL for count of all articles in a voucher specified by identifier
        /// </summary>
        public const string VoucherArticleCountUrl = "vouchers/{voucherId:min(1)}/articles/count";

        /// <summary>
        /// API client URL for a single voucher article specified by identifier
        /// </summary>
        public const string VoucherArticle = "vouchers/articles/{0}";

        /// <summary>
        /// API server route URL for a single voucher article specified by identifier
        /// </summary>
        public const string VoucherArticleUrl = "vouchers/articles/{articleId:min(1)}";

        /// <summary>
        /// API client URL for details of a single voucher article specified by identifier
        /// </summary>
        public const string VoucherArticleDetails = "vouchers/articles/{0}/details";

        /// <summary>
        /// API server route URL for details of a single voucher article specified by identifier
        /// </summary>
        public const string VoucherArticleDetailsUrl = "vouchers/articles/{articleId:min(1)}/details";

        /// <summary>
        /// API client URL for voucher article metadata
        /// </summary>
        public const string VoucherArticleMetadata = "vouchers/articles/metadata";

        /// <summary>
        /// API server route URL for voucher article metadata
        /// </summary>
        public const string VoucherArticleMetadataUrl = "vouchers/articles/metadata";
    }
}
