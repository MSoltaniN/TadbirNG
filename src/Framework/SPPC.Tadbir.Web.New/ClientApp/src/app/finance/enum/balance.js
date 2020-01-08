export var BalanceType;
(function (BalanceType) {
    BalanceType["ByDate"] = "1";
    BalanceType["ByVoucher"] = "2";
})(BalanceType || (BalanceType = {}));
export var BalanceDisplayType;
(function (BalanceDisplayType) {
    /**تراز آزمایشی در سطح کل */
    BalanceDisplayType[BalanceDisplayType["ByLedger"] = 0] = "ByLedger";
    /**تراز آزمایشی در سطح معین */
    BalanceDisplayType[BalanceDisplayType["BySubsidiary"] = 1] = "BySubsidiary";
    /**تراز آزمایشی در سطح تفصیلی */
    BalanceDisplayType[BalanceDisplayType["ByDetail"] = 2] = "ByDetail";
    /**تراز معین های یک حساب کل  */
    BalanceDisplayType[BalanceDisplayType["BySubsidiaryOfLeader"] = 3] = "BySubsidiaryOfLeader";
    /**تراز تفصیلی های یک حساب معین*/
    BalanceDisplayType[BalanceDisplayType["ByDetailOfSubsidiary"] = 4] = "ByDetailOfSubsidiary";
    /**تراز تفصیلی شناور سطح 1 */
    BalanceDisplayType[BalanceDisplayType["ByDetailAccountLevel1"] = 5] = "ByDetailAccountLevel1";
    /**تراز تفصیلی شناور سطح 2 */
    BalanceDisplayType[BalanceDisplayType["ByDetailAccountLevel2"] = 6] = "ByDetailAccountLevel2";
    /**تراز تفصیلی شناور سطح 3 */
    BalanceDisplayType[BalanceDisplayType["ByDetailAccountLevel3"] = 7] = "ByDetailAccountLevel3";
    /**تراز تفصیلی شناور سطح 4 */
    BalanceDisplayType[BalanceDisplayType["ByDetailAccountLevel4"] = 8] = "ByDetailAccountLevel4";
    /**ارزی */
    BalanceDisplayType[BalanceDisplayType["ByCurrency"] = 9] = "ByCurrency";
})(BalanceDisplayType || (BalanceDisplayType = {}));
export var BalanceFormatType;
(function (BalanceFormatType) {
    /**تراز آزمایشی 2 ستونی */
    BalanceFormatType["Balance2Column"] = "2";
    /**تراز آزمایشی 4 ستونی */
    BalanceFormatType["Balance4Column"] = "4";
    /**تراز آزمایشی 6 ستونی */
    BalanceFormatType["Balance6Column"] = "6";
    /**تراز آزمایشی 8 ستونی  */
    BalanceFormatType["Balance8Column"] = "8";
    /**تراز آزمایشی 10 ستونی*/
    BalanceFormatType["Balance10Column"] = "10";
})(BalanceFormatType || (BalanceFormatType = {}));
export var BalanceFormatTypeResource;
(function (BalanceFormatTypeResource) {
    /**تراز 2 ستونی */
    BalanceFormatTypeResource["Balance2Column"] = "Balance.Format.Balance2Column";
    /**تراز 4 ستونی */
    BalanceFormatTypeResource["Balance4Column"] = "Balance.Format.Balance4Column";
    /**تراز 6 ستونی */
    BalanceFormatTypeResource["Balance6Column"] = "Balance.Format.Balance6Column";
    /**تراز 8 ستونی */
    BalanceFormatTypeResource["Balance8Column"] = "Balance.Format.Balance8Column";
    /**تراز 10 ستونی */
    BalanceFormatTypeResource["Balance10Column"] = "Balance.Format.Balance10Column";
})(BalanceFormatTypeResource || (BalanceFormatTypeResource = {}));
export var BalanceDisplayTypeResource;
(function (BalanceDisplayTypeResource) {
    /**تراز آزمایشی در سطح کل */
    BalanceDisplayTypeResource["ByLedger"] = "Balance.DisplayTypes.ByLedger";
    /**تراز آزمایشی در سطح معین */
    BalanceDisplayTypeResource["BySubsidiary"] = "Balance.DisplayTypes.BySubsidiary";
    /**تراز آزمایشی در سطح تفصیلی */
    BalanceDisplayTypeResource["ByDetail"] = "Balance.DisplayTypes.ByDetail";
    /**تراز معین های یک حساب کل  */
    BalanceDisplayTypeResource["BySubsidiaryOfLeader"] = "Balance.DisplayTypes.BySubsidiaryOfLeader";
    /**تراز تفصیلی های یک حساب معین*/
    BalanceDisplayTypeResource["ByDetailOfSubsidiary"] = "Balance.DisplayTypes.ByDetailOfSubsidiary";
    /**تراز تفصیلی شناور سطح 1 */
    BalanceDisplayTypeResource["ByDetailAccountLevel1"] = "Balance.DisplayTypes.ByDetailAccountLevel1";
    /**تراز تفصیلی شناور سطح 2 */
    BalanceDisplayTypeResource["ByDetailAccountLevel2"] = "Balance.DisplayTypes.ByDetailAccountLevel2";
    /**تراز تفصیلی شناور سطح 3 */
    BalanceDisplayTypeResource["ByDetailAccountLevel3"] = "Balance.DisplayTypes.ByDetailAccountLevel3";
    /**تراز تفصیلی شناور سطح 4 */
    BalanceDisplayTypeResource["ByDetailAccountLevel4"] = "Balance.DisplayTypes.ByDetailAccountLevel4";
    /**ارزی */
    BalanceDisplayTypeResource["ByCurrency"] = "Balance.DisplayTypes.ByCurrency";
})(BalanceDisplayTypeResource || (BalanceDisplayTypeResource = {}));
export var VoucherStatusResource;
(function (VoucherStatusResource) {
    /** ثبت شده*/
    VoucherStatusResource["Committed"] = "Balance.VoucherStatuses.Committed";
    /** ثبت قطعی شده*/
    VoucherStatusResource["Finalized"] = "Balance.VoucherStatuses.Finalized";
    /** تایید شده*/
    VoucherStatusResource["Confirmed"] = "Balance.VoucherStatuses.Accepted";
    /** تصویب شده*/
    VoucherStatusResource["Approved"] = "Balance.VoucherStatuses.Approved";
    /** کلیه اسناد*/
    VoucherStatusResource["AllVouchers"] = "Balance.VoucherStatuses.AllVouchers";
})(VoucherStatusResource || (VoucherStatusResource = {}));
export var BranchScopeResource;
(function (BranchScopeResource) {
    /**شعبه جاری */
    BranchScopeResource["CurrentBranch"] = "BranchScope.CurrentBranch";
    /**شعبه جاری و زیر مجموعه ها */
    BranchScopeResource["CurrentBranchAndSubsets"] = "BranchScope.CurrentBranchAndSubsets";
})(BranchScopeResource || (BranchScopeResource = {}));
export var BalanceOptions;
(function (BalanceOptions) {
    /// <summary>
    /// عدم انتخاب همه گزینه ها
    /// </summary>
    BalanceOptions[BalanceOptions["None"] = 0] = "None";
    /// <summary>
    /// گزینه نمایش سند اختتامیه
    /// </summary>
    BalanceOptions[BalanceOptions["UseClosingVoucher"] = 1] = "UseClosingVoucher";
    /// <summary>
    /// گزینه نمایش سند بستن حساب ها
    /// </summary>
    BalanceOptions[BalanceOptions["UseClosingTempVoucher"] = 2] = "UseClosingTempVoucher";
    /// <summary>
    /// گزینه انعکاس افتتاحیه در ستون مانده ابتدا
    /// </summary>
    BalanceOptions[BalanceOptions["OpeningVoucherAsInitBalance"] = 4] = "OpeningVoucherAsInitBalance";
    /// <summary>
    /// گزینه نمایش سرفصل های با مانده صفر
    /// </summary>
    BalanceOptions[BalanceOptions["ShowZeroBalanceItems"] = 8] = "ShowZeroBalanceItems";
})(BalanceOptions || (BalanceOptions = {}));
//# sourceMappingURL=balance.js.map