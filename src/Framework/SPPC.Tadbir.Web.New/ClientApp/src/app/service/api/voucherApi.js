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
}
// vouchers
VoucherApi.EnvironmentVouchers = environment.BaseUrl + "/vouchers";
// vouchers/count
VoucherApi.EnvironmentItemCount = environment.BaseUrl + "/vouchers/count";
// vouchers/{voucherId:min(1)}
VoucherApi.Voucher = environment.BaseUrl + "/vouchers/{0}";
// vouchers/metadata
VoucherApi.VoucherMetadata = environment.BaseUrl + "/vouchers/metadata";
// vouchers/{voucherId:int}/prepare
VoucherApi.PrepareVoucher = environment.BaseUrl + "/vouchers/{0}/prepare";
// vouchers/prepare
VoucherApi.PrepareVouchers = environment.BaseUrl + "/vouchers/prepare";
// vouchers/review
VoucherApi.ReviewVouchers = environment.BaseUrl + "/vouchers/review";
// vouchers/reject
VoucherApi.RejectVouchers = environment.BaseUrl + "/vouchers/reject";
// vouchers/confirm
VoucherApi.ConfirmVouchers = environment.BaseUrl + "/vouchers/confirm";
// vouchers/approve
VoucherApi.ApproveVouchers = environment.BaseUrl + "/vouchers/approve";
// vouchers/{voucherId:min(1)}/review
VoucherApi.ReviewVoucher = environment.BaseUrl + "/vouchers/{0}/review";
// vouchers/{voucherId:min(1)}/reject
VoucherApi.RejectVoucher = environment.BaseUrl + "/vouchers/{0}/reject";
// vouchers/{voucherId:min(1)}/confirm
VoucherApi.ConfirmVoucher = environment.BaseUrl + "/vouchers/{0}/confirm";
// vouchers/{voucherId:min(1)}/approve
VoucherApi.ApproveVoucher = environment.BaseUrl + "/vouchers/{0}/approve";
// vouchers/{voucherId:min(1)}/check
VoucherApi.CheckVoucher = environment.BaseUrl + "/vouchers/{0}/check";
// vouchers/{voucherId:min(1)}/uncheck
VoucherApi.UncheckVoucher = environment.BaseUrl + "/vouchers/{0}/uncheck";
// vouchers/{voucherId:min(1)}/articles
VoucherApi.VoucherArticles = environment.BaseUrl + "/vouchers/{0}/articles";
// vouchers/{voucherId:min(1)}/articles/count
VoucherApi.VoucherArticleCount = environment.BaseUrl + "/vouchers/{0}/articles/count";
// vouchers/articles/{articleId:min(1)}
VoucherApi.VoucherArticle = environment.BaseUrl + "/vouchers/articles/{0}";
// vouchers/articles/{articleId:min(1)}/details
VoucherApi.VoucherArticleDetails = environment.BaseUrl + "/vouchers/articles/{0}/details";
// vouchers/articles/metadata
VoucherApi.VoucherArticleMetadata = environment.BaseUrl + "/vouchers/articles/metadata";
//# sourceMappingURL=voucherApi.js.map