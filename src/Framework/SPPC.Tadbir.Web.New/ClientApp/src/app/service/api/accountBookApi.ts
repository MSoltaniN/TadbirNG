// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.631
//     Template Version: 1.0
//     Generation Date: 03/29/1398 01:04:04 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "../../../environments/environment";

export class AccountBookApi {

    // accbook/account/{accountId:min(1)}/by-row
    public static AccountBookByRow = environment.BaseUrl + "/accbook/account/{0}/by-row";

    // accbook/account/{accountId:min(1)}/voucher-sum
    public static AccountBookVoucherSum = environment.BaseUrl + "/accbook/account/{0}/voucher-sum";

    // accbook/account/{accountId:min(1)}/daily-sum
    public static AccountBookDailySum = environment.BaseUrl + "/accbook/account/{0}/daily-sum";

    // accbook/account/{accountId:min(1)}/monthly-sum
    public static AccountBookMonthlySum = environment.BaseUrl + "/accbook/account/{0}/monthly-sum";

    // accbook/faccount/{faccountId:min(1)}/by-row
    public static DetailAccountBookByRow = environment.BaseUrl + "/accbook/faccount/{0}/by-row";

    // accbook/faccount/{faccountId:min(1)}/voucher-sum
    public static DetailAccountBookVoucherSum = environment.BaseUrl + "/accbook/faccount/{0}/voucher-sum";

    // accbook/faccount/{faccountId:min(1)}/daily-sum
    public static DetailAccountBookDailySum = environment.BaseUrl + "/accbook/faccount/{0}/daily-sum";

    // accbook/faccount/{faccountId:min(1)}/monthly-sum
    public static DetailAccountBookMonthlySum = environment.BaseUrl + "/accbook/faccount/{0}/monthly-sum";

    // accbook/ccenter/{ccenterId:min(1)}/by-row
    public static CostCenterBookByRow = environment.BaseUrl + "/accbook/ccenter/{0}/by-row";

    // accbook/ccenter/{ccenterId:min(1)}/voucher-sum
    public static CostCenterBookVoucherSum = environment.BaseUrl + "/accbook/ccenter/{0}/voucher-sum";

    // accbook/ccenter/{ccenterId:min(1)}/daily-sum
    public static CostCenterBookDailySum = environment.BaseUrl + "/accbook/ccenter/{0}/daily-sum";

    // accbook/ccenter/{ccenterId:min(1)}/monthly-sum
    public static CostCenterBookMonthlySum = environment.BaseUrl + "/accbook/ccenter/{0}/monthly-sum";

    // accbook/project/{projectId:min(1)}/by-row
    public static ProjectBookByRow = environment.BaseUrl + "/accbook/project/{0}/by-row";

    // accbook/project/{projectId:min(1)}/voucher-sum
    public static ProjectBookVoucherSum = environment.BaseUrl + "/accbook/project/{0}/voucher-sum";

    // accbook/project/{projectId:min(1)}/daily-sum
    public static ProjectBookDailySum = environment.BaseUrl + "/accbook/project/{0}/daily-sum";

    // accbook/project/{projectId:min(1)}/monthly-sum
    public static ProjectBookMonthlySum = environment.BaseUrl + "/accbook/project/{0}/monthly-sum";
}