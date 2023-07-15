import { environment } from "@sppc/env/environment";

export class PaymentCashAccountApi {
     /*
    * API client URL for all cash account articles in a single payment specified by identifier {paymentId:min(1)}
    */
    public static CashAccountArticles = environment.BaseUrl + "/payments/{0}/cash/articles";

    /*
    * API client URL for a single payment cash account article specified by identifier {articleId:min(1)}
    */
    public static CashAccountArticle = environment.BaseUrl + "/payments/cash/articles/{0}";

    /*
    * API client URL for all available payment cash account articles
    */
    public static AllCashAccountArticles = environment.BaseUrl + "/payments/cash/articles";

    /*
    * API client URL for remove invalid cash account articles a single payment specified by identifier {paymentId:min(1)}
    */
    public static RemoveCashAccountInvalidRows = environment.BaseUrl + "/payments/{0}/cash/articles/prune";

    /*
    * API client URL for aggregate cash account articles a single payment specified by identifier {paymentId:min(1)}
    */
    public static AggregateCashAccountArticleRows = environment.BaseUrl + "/payments/{0}/cash/articles/aggregate";
}

export class ReceiptCashAccountApi {
    /*
    * API client URL for all cash account articles in a single receipt specified by identifier {receiptId:min(1)}
    */
    public static CashAccountArticles = environment.BaseUrl + "/receipts/{0}/cash/articles";

    /*
    * API client URL for a single receipt cash account article specified by identifier {articleId:min(1)}
    */
    public static CashAccountArticle = environment.BaseUrl + "/receipts/cash/articles/{0}";

    /*
    * API client URL for all available receipt cash account articles
    */
    public static AllCashAccountArticles = environment.BaseUrl + "/receipts/cash/articles";

    /*
    * API client URL for remove invalid cash account articles a single receipt specified by identifier {receiptId:min(1)}
    */
    public static RemoveCashAccountInvalidRows = environment.BaseUrl + "/receipts/{0}/cash/articles/prune";

    /*
    * API client URL for aggregate cash account articles a single receipt specified by identifier {receiptId:min(1)}
    */
    public static AggregateCashAccountArticleRows = environment.BaseUrl + "/receipts/{0}/cash/articles/aggregate";

}