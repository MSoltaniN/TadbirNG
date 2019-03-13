"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
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
//# sourceMappingURL=journalDisplayType.js.map