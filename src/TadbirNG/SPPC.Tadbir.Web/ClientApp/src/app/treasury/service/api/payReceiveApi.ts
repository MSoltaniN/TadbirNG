import { environment } from "@sppc/env/environment";

export class PayReceiveApi {
    /**
    * API client URL for all payment items
    */
    public static Payments = environment.BaseUrl + "/payments";

    /**
    * API client URL for a payment item specified by unique {payReceiveId:min(1)} identifier
    */
    public static Payment = environment.BaseUrl + "/payments/{0}";

    /**
    * API client URL for all Receipt items
    */
    public static Receipts = environment.BaseUrl + "/receipts";

    /**
    * API client URL for a Receipt item specified by unique {payReceiveId:min(1)} identifier
    */
    public static Receipt = environment.BaseUrl + "/receipts/{0}";

    /**
    * API client URL for confirming a single payment specified by {payReceiveId:min(1)} identifier
    */
    public static ConfirmPayment = environment.BaseUrl + "/payments/{0}/confirm";

    /**
    * API client URL for undoing confirmation of a single payment specified by {payReceiveId:min(1)} identifier
    */
    public static UndoConfirmPayment = environment.BaseUrl + "/payments/{0}/confirm/undo";

    /**
    * API client URL for approving a single payment specified by {payReceiveId:min(1)} identifier
    */
    public static ApprovePayment = environment.BaseUrl + "/payments/{0}/approve";

    /**
    * API client URL for undoing approval of a single payment specified by {payReceiveId:min(1)} identifier
    */
    public static UndoApprovePayment = environment.BaseUrl + "/payments/{0}/approve/undo";

    /**
    * API client URL for confirming a single Receipt specified by {payReceiveId:min(1)} identifier
    */
    public static ConfirmReceipt = environment.BaseUrl + "/receipts/{0}/confirm";

    /**
    * API client URL for undoing confirmation of a single Receipt specified by {payReceiveId:min(1)} identifier
    */
    public static UndoConfirmReceipt = environment.BaseUrl + "/receipts/{0}/confirm/undo";

    /**
    * API client URL for approving a single Receipt specified by {payReceiveId:min(1)} identifier
    */
    public static ApproveReceipt = environment.BaseUrl + "/receipts/{0}/approve";

    /**
    * API client URL for undoing approval of a single Receipt specified by {payReceiveId:min(1)} identifier
    */
    public static UndoApproveReceipt = environment.BaseUrl + "/receipts/{0}/approve/undo";

    /**
    * API client URL for a single payment specified by {payReceiveNo:min(1)} number
    */
    public static PaymentByNo = environment.BaseUrl + "/payments/by-no/{0}";

    /**
    * API client URL for the first payment in current environment
    */
    public static FirstPayment = environment.BaseUrl + "/payments/first";

    /**
    * API client URL for previous payment in current environment {payReceiveNo:min(1)}
    */
    public static PreviousPayment = environment.BaseUrl + "/payments/{0}/previous";

    /**
    * API client URL for next payment in current environment {payReceiveNo:min(1)}
    */
    public static NextPayment = environment.BaseUrl + "/payments/{0}/next";

    /**
    * API client URL for the last payment in current environment
    */
    public static LastPayment = environment.BaseUrl + "/payments/last";

    /**
    * API client URL for a single Receipt specified by number {payReceiveNo:min(1)}
    */
    public static ReceiptByNo = environment.BaseUrl + "/receipts/by-no/{0}";

    /**
    * API client URL for the first Receipt in current environment
    */
    public static FirstReceipt = environment.BaseUrl + "/receipts/first";

    /**
    * API client URL for previous Receipt in current environment {payReceiveNo:min(1)}
    */
    public static PreviousReceipt = environment.BaseUrl + "/receipts/{0}/previous";

    /**
    * API client URL for next Receipt in current environment {payReceiveNo:min(1)}
    */
    public static NextReceipt = environment.BaseUrl + "/receipts/{0}/next";

    /**
    * API client URL for the last Receipt in current environment
    */
    public static LastReceipt = environment.BaseUrl + "/receipts/last";

    /**
    * API client URL for a newly initialized
    */
    public static NewPayment = environment.BaseUrl + "/payments/new";

    /**
    * API client URL for a newly initialized
    */
    public static NewReceipt = environment.BaseUrl + "/receipts/new";

    /**
     * {paymentId:min(1)} ,{voucherId:int} 0 for new voucher
     */
    public static RegisterPayment = environment.BaseUrl + "/payments/{0}/register/vouchers/{1}";

    /**
     * {receiptId:min(1)} ,{voucherId:int} 0 for new voucher
     */
    public static RegisterReceipt = environment.BaseUrl + "/receipts/{0}/register/vouchers/{1}";

    /**
     * {paymentId:min(1)}
     */
    public static UndoRegisterPayment = environment.BaseUrl + "/payments/{0}/register/undo";

    /**
     * {receiptId:min(1)}
     */
    public static UndoRegisterReceipt = environment.BaseUrl + "/receipts/{0}/register/undo";

}
