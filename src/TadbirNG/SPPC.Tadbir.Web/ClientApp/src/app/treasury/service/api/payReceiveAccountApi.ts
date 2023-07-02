import { environment } from "@sppc/env/environment";

export class PayReceiveAccountApi {
    /**
    * API client URL for all account articles in a single payment specified by identifier {paymentId:min(1)}
    */
    public static PaymentAccountArticles = environment.BaseUrl + "/payments/{0}/account-articles";

    /**
    * API client URL for a single payment account article specified by identifier {accountArticleId:min(1)}
    */
    public static PaymentArticle = environment.BaseUrl + "/payments/account-articles/{0}";

    /**
    * API client URL for all available payment account articles
    */
    public static AllPaymentAccountArticles = environment.BaseUrl + "/payments/account-articles";

    /**
    * API client URL for remove invalid account articles a single payment specified by identifier {paymentId:min(1)}
    */
    public static RemovePaymentAccountInvalidRows = environment.BaseUrl + "/payments/{0}/account-articles/remove-Invalid-rows";

    /**
    * API client URL for aggregate account articles a single payment specified by identifier {paymentId:min(1)}
    */
    public static AggregatePaymentAccountArticleRows = environment.BaseUrl + "/payments/{0}/account-articles/aggregate-rows";

    /**
    * API client URL for all account articles in a single receipt specified by identifier {receiptId:min(1)}
    */
    public static ReceiptAccountArticles = environment.BaseUrl + "/receipts/{0}/account-articles";

    /**
    * API client URL for a single receipt account article specified by identifier {accountArticleId:min(1)}
    */
    public static ReceiptArticle = environment.BaseUrl + "/receipts/account-articles/{0}";

    /**
    * API client URL for all available receipt account articles
    */
    public static AllReceiptAccountArticles = environment.BaseUrl + "/receipts/account-articles";

    /**
    * API client URL for remove invalid account articles a single receipt specified by identifier {receiptId:min(1)}
    */
    public static RemoveReceiptAccountInvalidRows = environment.BaseUrl + "/receipts/{0}/account-articles/remove-Invalid-rows";

    /**
    * API client URL for aggregate account articles a single receipt specified by identifier {receiptId:min(1)}
    */
    public static AggregateReceiptAccountArticleRows = environment.BaseUrl + "/receipts/{0}/account-articles/aggregate-rows";
}