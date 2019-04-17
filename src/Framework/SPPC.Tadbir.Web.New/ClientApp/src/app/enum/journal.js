"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var JournalType;
(function (JournalType) {
    JournalType["ByDate"] = "1";
    JournalType["ByVoucher"] = "2";
})(JournalType = exports.JournalType || (exports.JournalType = {}));
var JournalDisplayType;
(function (JournalDisplayType) {
    /**مطابق ردیف های سند */
    JournalDisplayType[JournalDisplayType["ByDateByRow"] = 1] = "ByDateByRow";
    /**مطابق ردیف های سند با سطوح شناور */
    JournalDisplayType[JournalDisplayType["ByDateByRowDetail"] = 2] = "ByDateByRowDetail";
    /**در سطح کل */
    JournalDisplayType[JournalDisplayType["ByDateByLedger"] = 3] = "ByDateByLedger";
    /**در سطح معین */
    JournalDisplayType[JournalDisplayType["ByDateBySubsidiary"] = 4] = "ByDateBySubsidiary";
    /**سند خلاصه */
    JournalDisplayType[JournalDisplayType["ByDateLedgerSummary"] = 5] = "ByDateLedgerSummary";
    /**سند خلاصه به تفکیک تاریخ */
    JournalDisplayType[JournalDisplayType["ByDateLedgerSummaryByDate"] = 6] = "ByDateLedgerSummaryByDate";
    /**سند خلاصه به تفکیک ماه */
    JournalDisplayType[JournalDisplayType["ByDateLedgerSummaryByMonth"] = 7] = "ByDateLedgerSummaryByMonth";
})(JournalDisplayType = exports.JournalDisplayType || (exports.JournalDisplayType = {}));
var JournalDisplayTypeResource;
(function (JournalDisplayTypeResource) {
    /**مطابق ردیف های سند */
    JournalDisplayTypeResource["VoucherRows"] = "Journal.DisplayTypes.VoucherRows";
    /**مطابق ردیف های سند با سطوح شناور */
    JournalDisplayTypeResource["VoucherRowsWithFloatingAccounts"] = "Journal.DisplayTypes.VoucherRowsWithFloatingAccounts";
    /**در سطح کل */
    JournalDisplayTypeResource["LedgerLevel"] = "Journal.DisplayTypes.LedgerLevel";
    /**در سطح معین */
    JournalDisplayTypeResource["SubsidiaryLevel"] = "Journal.DisplayTypes.SubsidiaryLevel";
    /**سند خلاصه */
    JournalDisplayTypeResource["BriefVoucher"] = "Journal.DisplayTypes.BriefVoucher";
    /**سند خلاصه به تفکیک تاریخ */
    JournalDisplayTypeResource["BriefVoucherByDate"] = "Journal.DisplayTypes.BriefVoucherByDate";
    /**سند خلاصه به تفکیک ماه */
    JournalDisplayTypeResource["BriefVoucherByMonth"] = "Journal.DisplayTypes.BriefVoucherByMonth";
})(JournalDisplayTypeResource = exports.JournalDisplayTypeResource || (exports.JournalDisplayTypeResource = {}));
var VoucherStatusResource;
(function (VoucherStatusResource) {
    /** ثبت شده*/
    VoucherStatusResource["Committed"] = "Journal.VoucherStatuses.Committed";
    /** ثبت قطعی شده*/
    VoucherStatusResource["Finalized"] = "Journal.VoucherStatuses.Finalized";
    /** تایید شده*/
    VoucherStatusResource["Accepted"] = "Journal.VoucherStatuses.Accepted";
    /** تصویب شده*/
    VoucherStatusResource["Approved"] = "Journal.VoucherStatuses.Approved";
    /** کلیه اسناد*/
    VoucherStatusResource["AllVouchers"] = "Journal.VoucherStatuses.AllVouchers";
})(VoucherStatusResource = exports.VoucherStatusResource || (exports.VoucherStatusResource = {}));
var BranchScopeResource;
(function (BranchScopeResource) {
    /**شعبه جاری */
    BranchScopeResource["CurrentBranch"] = "BranchScope.CurrentBranch";
    /**شعبه جاری و زیر مجموعه ها */
    BranchScopeResource["CurrentBranchAndSubsets"] = "BranchScope.CurrentBranchAndSubsets";
})(BranchScopeResource = exports.BranchScopeResource || (exports.BranchScopeResource = {}));
var ArticleTypesResource;
(function (ArticleTypesResource) {
    /** کلیه آرتیکل ها*/
    ArticleTypesResource["AllVoucherLines"] = "Journal.ArticleTypes.AllVoucherLines";
    /** آرتیکل های علامت گذاری شده*/
    ArticleTypesResource["MarkedVoucherLines"] = "Journal.ArticleTypes.MarkedVoucherLines";
    /** آرتیکل های علامت گذاری نشده*/
    ArticleTypesResource["UncheckedVoucherLines"] = "Journal.ArticleTypes.UncheckedVoucherLines";
})(ArticleTypesResource = exports.ArticleTypesResource || (exports.ArticleTypesResource = {}));
//# sourceMappingURL=journal.js.map