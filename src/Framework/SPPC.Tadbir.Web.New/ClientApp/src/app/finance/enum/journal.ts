
export enum JournalType {
  ByDate = '1',
  ByVoucher = '2'
}

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

export enum JournalDisplayTypeResource {
  /**مطابق ردیف های سند */
  VoucherRows = "Journal.DisplayTypes.VoucherRows",

  /**مطابق ردیف های سند با سطوح شناور */
  VoucherRowsWithFloatingAccounts = "Journal.DisplayTypes.VoucherRowsWithFloatingAccounts",

  /**در سطح کل */
  LedgerLevel = "Journal.DisplayTypes.LedgerLevel",

  /**در سطح معین */
  SubsidiaryLevel = "Journal.DisplayTypes.SubsidiaryLevel",

  /**سند خلاصه */
  BriefVoucher = "Journal.DisplayTypes.BriefVoucher",

  /**سند خلاصه به تفکیک تاریخ */
  BriefVoucherByDate = "Journal.DisplayTypes.BriefVoucherByDate",

  /**سند خلاصه به تفکیک ماه */
  BriefVoucherByMonth = "Journal.DisplayTypes.BriefVoucherByMonth"
}

export enum VoucherStatusResource {
  /** ثبت شده*/
  Committed = "Journal.VoucherStatuses.Committed",

  /** ثبت قطعی شده*/
  Finalized = "Journal.VoucherStatuses.Finalized",

  /** تایید شده*/
  Confirmed = "Journal.VoucherStatuses.Accepted",

  /** تصویب شده*/
  Approved = "Journal.VoucherStatuses.Approved",

  /** کلیه اسناد*/
  AllVouchers = "Journal.VoucherStatuses.AllVouchers"
}

export enum BranchScopeResource {
  /**شعبه جاری */
  CurrentBranch = "BranchScope.CurrentBranch",

  /**شعبه جاری و زیر مجموعه ها */
  CurrentBranchAndSubsets = "BranchScope.CurrentBranchAndSubsets"
}

export enum ArticleTypesResource {
  /** کلیه آرتیکل ها*/
  AllVoucherLines = "Journal.ArticleTypes.AllVoucherLines",

  /** آرتیکل های علامت گذاری شده*/
  MarkedVoucherLines = "Journal.ArticleTypes.MarkedVoucherLines",

  /** آرتیکل های علامت گذاری نشده*/
  UncheckedVoucherLines = "Journal.ArticleTypes.UncheckedVoucherLines"
}

export enum ArticleTypesResourceKey {
  AllVoucherLines = '1',
  MarkedVoucherLines = '2',
  UncheckedVoucherLines = '3'
}
