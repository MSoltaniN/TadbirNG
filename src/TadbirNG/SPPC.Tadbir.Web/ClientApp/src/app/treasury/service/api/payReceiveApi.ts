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
    * API client URL for all receival items
    */
    public static Receivals = environment.BaseUrl + "/receivals";

    /**
    * API client URL for a receival item specified by unique {payReceiveId:min(1)} identifier
    */
    public static Receival = environment.BaseUrl + "/receivals/{0}";

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
    * API client URL for confirming a single receival specified by {payReceiveId:min(1)} identifier
    */
    public static ConfirmReceival = environment.BaseUrl + "/receivals/{0}/confirm";

    /**
    * API client URL for undoing confirmation of a single receival specified by {payReceiveId:min(1)} identifier
    */
    public static UndoConfirmReceival = environment.BaseUrl + "/receivals/{0}/confirm/undo";

    /**
    * API client URL for approving a single receival specified by {payReceiveId:min(1)} identifier
    */
    public static ApproveReceival = environment.BaseUrl + "/receivals/{0}/approve";

    /**
    * API client URL for undoing approval of a single receival specified by {payReceiveId:min(1)} identifier
    */
    public static UndoApproveReceival = environment.BaseUrl + "/receivals/{0}/approve/undo";

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
    * API client URL for a single receival specified by number {payReceiveNo:min(1)}
    */
    public static ReceivalByNo = environment.BaseUrl + "/receivals/by-no/{0}";

    /**
    * API client URL for the first receival in current environment
    */
    public static FirstReceival = environment.BaseUrl + "/receivals/first";

    /**
    * API client URL for previous receival in current environment {payReceiveNo:min(1)}
    */
    public static PreviousReceival = environment.BaseUrl + "/receivals/{0}/previous";

    /**
    * API client URL for next receival in current environment {payReceiveNo:min(1)}
    */
    public static NextReceival = environment.BaseUrl + "/receivals/{0}/next";

    /**
    * API client URL for the last receival in current environment
    */
    public static LastReceival = environment.BaseUrl + "/receivals/last";

    /**
    * API client URL for a newly initialized
    */
    public static NewPayment = environment.BaseUrl + "/payments/new";

    /**
    * API client URL for a newly initialized
    */
    public static NewReceival = environment.BaseUrl + "/receivals/new";
}
