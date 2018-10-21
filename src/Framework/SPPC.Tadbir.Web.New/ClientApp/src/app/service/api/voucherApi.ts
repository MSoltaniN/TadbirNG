// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.429
//     Template Version: 1.0
//     Generation Date: 10/20/2018 12:16:12 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "../../../environments/environment";

export class VoucherApi {

  // vouchers
  public static EnvironmentVouchers = environment.BaseUrl + "/vouchers";

  // vouchers/count
  public static EnvironmentItemCount = environment.BaseUrl + "/vouchers/count";

  // vouchers/{voucherId:min(1)}
  public static Voucher = environment.BaseUrl + "/vouchers/{0}";

  // vouchers/metadata
  public static VoucherMetadata = environment.BaseUrl + "/vouchers/metadata";

  // vouchers/{voucherId:int}/prepare
  public static PrepareVoucher = environment.BaseUrl + "/vouchers/{0}/prepare";

  // vouchers/prepare
  public static PrepareVouchers = environment.BaseUrl + "/vouchers/prepare";

  // vouchers/review
  public static ReviewVouchers = environment.BaseUrl + "/vouchers/review";

  // vouchers/reject
  public static RejectVouchers = environment.BaseUrl + "/vouchers/reject";

  // vouchers/confirm
  public static ConfirmVouchers = environment.BaseUrl + "/vouchers/confirm";

  // vouchers/approve
  public static ApproveVouchers = environment.BaseUrl + "/vouchers/approve";

  // vouchers/{voucherId:min(1)}/review
  public static ReviewVoucher = environment.BaseUrl + "/vouchers/{0}/review";

  // vouchers/{voucherId:min(1)}/reject
  public static RejectVoucher = environment.BaseUrl + "/vouchers/{0}/reject";

  // vouchers/{voucherId:min(1)}/confirm
  public static ConfirmVoucher = environment.BaseUrl + "/vouchers/{0}/confirm";

  // vouchers/{voucherId:min(1)}/approve
  public static ApproveVoucher = environment.BaseUrl + "/vouchers/{0}/approve";

  // vouchers/{voucherId:min(1)}/check
  public static CheckVoucher = environment.BaseUrl + "/vouchers/{0}/check";

  // vouchers/{voucherId:min(1)}/uncheck
  public static UncheckVoucher = environment.BaseUrl + "/vouchers/{0}/uncheck";

  // vouchers/{voucherId:min(1)}/articles
  public static VoucherArticles = environment.BaseUrl + "/vouchers/{0}/articles";

  // vouchers/{voucherId:min(1)}/articles/count
  public static VoucherArticleCount = environment.BaseUrl + "/vouchers/{0}/articles/count";

  // vouchers/articles/{articleId:min(1)}
  public static VoucherArticle = environment.BaseUrl + "/vouchers/articles/{0}";

  // vouchers/articles/{articleId:min(1)}/details
  public static VoucherArticleDetails = environment.BaseUrl + "/vouchers/articles/{0}/details";

  // vouchers/articles/metadata
  public static VoucherArticleMetadata = environment.BaseUrl + "/vouchers/articles/metadata";
}
