
export enum JournalDisplayType {
  /**مطابق ردیف های سند */
  ByDateByRow = 1,
  /**مطابق ردیف های سند با سطوح شناور */
  ByDateByRowDetail = 2,
  /**در سطح کل */
  ByDateByLedger = 3,
  /**در سطح معین */
  ByDateBySubsidiary = 4,
  /**سند خلاصه */
  ByDateLedgerSummary = 5,
  /**سند خلاصه به تفکیک تاریخ */
  ByDateLedgerSummaryByDate = 6,
  /**سند خلاصه به تفکیک ماه */
  ByDateLedgerSummaryByMonth = 7
}
