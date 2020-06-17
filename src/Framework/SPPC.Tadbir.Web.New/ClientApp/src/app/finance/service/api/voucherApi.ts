// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.857
//     Template Version: 1.0
//     Generation Date: 2020-04-07 7:26:30 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class VoucherApi {

    // vouchers
    public static EnvironmentVouchers = environment.BaseUrl + "/vouchers";

    // vouchers/count
    public static EnvironmentItemCount = environment.BaseUrl + "/vouchers/count";

    // vouchers/range
    public static EnvironmentItemRange = environment.BaseUrl + "/vouchers/range";

    // vouchers/{voucherId:min(1)}
    public static Voucher = environment.BaseUrl + "/vouchers/{0}";

    // vouchers/new
    public static NewVoucher = environment.BaseUrl + "/vouchers/new";

    // vouchers/by-no/{voucherNo:min(1)}
    public static VoucherByNo = environment.BaseUrl + "/vouchers/by-no/{0}";

    // vouchers/first
    public static FirstVoucher = environment.BaseUrl + "/vouchers/first";

    // vouchers/{voucherNo:min(1)}/previous
    public static PreviousVoucher = environment.BaseUrl + "/vouchers/{0}/previous";

    // vouchers/{voucherNo:min(1)}/next
    public static NextVoucher = environment.BaseUrl + "/vouchers/{0}/next";

    // vouchers/last
    public static LastVoucher = environment.BaseUrl + "/vouchers/last";

    // vouchers/confirm
    public static ConfirmVouchers = environment.BaseUrl + "/vouchers/confirm";

    // vouchers/approve
    public static ApproveVouchers = environment.BaseUrl + "/vouchers/approve";

    // vouchers/{voucherId:min(1)}/confirm
    public static ConfirmVoucher = environment.BaseUrl + "/vouchers/{0}/confirm";

    // vouchers/{voucherId:min(1)}/approve
    public static ApproveVoucher = environment.BaseUrl + "/vouchers/{0}/approve";

    // vouchers/{voucherId:min(1)}/confirm/undo
    public static UndoConfirmVoucher = environment.BaseUrl + "/vouchers/{0}/confirm/undo";

    // vouchers/{voucherId:min(1)}/approve/undo
    public static UndoApproveVoucher = environment.BaseUrl + "/vouchers/{0}/approve/undo";

    // vouchers/{voucherId:min(1)}/check
    public static CheckVoucher = environment.BaseUrl + "/vouchers/{0}/check";

    // vouchers/{voucherId:min(1)}/check/undo
    public static UndoCheckVoucher = environment.BaseUrl + "/vouchers/{0}/check/undo";

    // vouchers/{voucherId:min(1)}/finalize
    public static FinalizeVoucher = environment.BaseUrl + "/vouchers/{0}/finalize";

    // vouchers/{voucherId:min(1)}/finalize/undo
    public static UndoFinalizeVoucher = environment.BaseUrl + "/vouchers/{0}/finalize/undo";

    // vouchers/count/by-status
    public static VoucherCountByStatus = environment.BaseUrl + "/vouchers/count/by-status";

    // vouchers/articles
    public static AllVoucherArticles = environment.BaseUrl + "/vouchers/articles";

    // vouchers/{voucherId:min(1)}/articles
    public static VoucherArticles = environment.BaseUrl + "/vouchers/{0}/articles";

    // vouchers/{voucherId:min(1)}/articles/count
    public static VoucherArticleCount = environment.BaseUrl + "/vouchers/{0}/articles/count";

    // vouchers/articles/{articleId:min(1)}
    public static VoucherArticle = environment.BaseUrl + "/vouchers/articles/{0}";

    // vouchers/articles/{articleId:min(1)}/mark
    public static VoucherArticleMark = environment.BaseUrl + "/vouchers/articles/{0}/mark";

    // vouchers/articles/{articleId:min(1)}/details
    public static VoucherArticleDetails = environment.BaseUrl + "/vouchers/articles/{0}/details";

    // vouchers/articles/metadata
    public static VoucherArticleMetadata = environment.BaseUrl + "/vouchers/articles/metadata";

    // vouchers/articles/count
    public static VoucherArticlesCount = environment.BaseUrl + "/vouchers/articles/count";

    // vouchers/no-article
    public static VoucherWithNoArticle = environment.BaseUrl + "/vouchers/no-article";

    // vouchers/unbalanced
    public static UnbalancedVouchers = environment.BaseUrl + "/vouchers/unbalanced";

    // vouchers/miss-number
    public static MissingVoucherNumber = environment.BaseUrl + "/vouchers/miss-number";

    // vouchers/articles/sys-issue/{issueType}
    public static SystemIssueArticles = environment.BaseUrl + "/vouchers/articles/sys-issue/{0}";

    // vouchers/opening
    public static OpeningVoucherQuery = environment.BaseUrl + "/vouchers/opening/query";

    // vouchers/opening
    public static OpeningVoucher = environment.BaseUrl + "/vouchers/opening";

    // vouchers/closing-tmp
    public static ClosingAccountsVoucher = environment.BaseUrl + "/vouchers/closing-tmp";

    // vouchers/closing
  public static ClosingVoucher = environment.BaseUrl + "/vouchers/closing";
  
  // vouchers/check
  public static CheckVouchers = environment.BaseUrl + "/vouchers/check";

  // vouchers/undocheck
  public static UnDoCheckVouchers = environment.BaseUrl + "/vouchers/undocheck";

  // vouchers/finalize
  public static FinalizeVouchers = environment.BaseUrl + "/vouchers/finalize";

  // vouchers/undofinalize
  public static UndoFinalizeVouchers = environment.BaseUrl + "/vouchers/undofinalize";




  
  
}
