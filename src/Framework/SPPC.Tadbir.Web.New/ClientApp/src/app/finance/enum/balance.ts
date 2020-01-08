export enum BalanceType {
  ByDate = '1',
  ByVoucher = '2'
}

export enum BalanceDisplayType {
  /**تراز آزمایشی در سطح کل */
  ByLedger = 0,
  /**تراز آزمایشی در سطح معین */
  BySubsidiary = 1,
  /**تراز آزمایشی در سطح تفصیلی */
  ByDetail = 2,
  /**تراز معین های یک حساب کل  */
  BySubsidiaryOfLeader = 3,
  /**تراز تفصیلی های یک حساب معین*/
  ByDetailOfSubsidiary = 4,
  /**تراز تفصیلی شناور سطح 1 */
  ByDetailAccountLevel1 = 5,
  /**تراز تفصیلی شناور سطح 2 */
  ByDetailAccountLevel2 = 6, 
  /**تراز تفصیلی شناور سطح 3 */
  ByDetailAccountLevel3 = 7,
  /**تراز تفصیلی شناور سطح 4 */
  ByDetailAccountLevel4 = 8,
  /**ارزی */
  ByCurrency = 9
}

export enum BalanceFormatType {
  /**تراز آزمایشی 2 ستونی */
  Balance2Column = "2",
  /**تراز آزمایشی 4 ستونی */
  Balance4Column = "4",
  /**تراز آزمایشی 6 ستونی */
  Balance6Column = "6",
  /**تراز آزمایشی 8 ستونی  */
  Balance8Column = "8",
  /**تراز آزمایشی 10 ستونی*/
  Balance10Column = "10"  
}


export enum BalanceFormatTypeResource {
  /**تراز 2 ستونی */
  Balance2Column = "Balance.Format.Balance2Column",
  /**تراز 4 ستونی */
  Balance4Column = "Balance.Format.Balance4Column",
  /**تراز 6 ستونی */
  Balance6Column = "Balance.Format.Balance6Column",
  /**تراز 8 ستونی */
  Balance8Column = "Balance.Format.Balance8Column",
  /**تراز 10 ستونی */
  Balance10Column = "Balance.Format.Balance10Column"  
}

export enum BalanceDisplayTypeResource {
  /**تراز آزمایشی در سطح کل */
  ByLedger = "Balance.DisplayTypes.ByLedger",
  /**تراز آزمایشی در سطح معین */
  BySubsidiary = "Balance.DisplayTypes.BySubsidiary",
  /**تراز آزمایشی در سطح تفصیلی */
  ByDetail = "Balance.DisplayTypes.ByDetail",
  /**تراز معین های یک حساب کل  */
  BySubsidiaryOfLeader = "Balance.DisplayTypes.BySubsidiaryOfLeader",
  /**تراز تفصیلی های یک حساب معین*/
  ByDetailOfSubsidiary = "Balance.DisplayTypes.ByDetailOfSubsidiary",
  /**تراز تفصیلی شناور سطح 1 */
  ByDetailAccountLevel1 = "Balance.DisplayTypes.ByDetailAccountLevel1",
  /**تراز تفصیلی شناور سطح 2 */
  ByDetailAccountLevel2 = "Balance.DisplayTypes.ByDetailAccountLevel2",
  /**تراز تفصیلی شناور سطح 3 */
  ByDetailAccountLevel3 = "Balance.DisplayTypes.ByDetailAccountLevel3",
  /**تراز تفصیلی شناور سطح 4 */
  ByDetailAccountLevel4 = "Balance.DisplayTypes.ByDetailAccountLevel4",
  /**ارزی */
  ByCurrency = "Balance.DisplayTypes.ByCurrency"  
}

export enum VoucherStatusResource {
  /** ثبت شده*/
  Committed = "Balance.VoucherStatuses.Committed",

  /** ثبت قطعی شده*/
  Finalized = "Balance.VoucherStatuses.Finalized",

  /** تایید شده*/
  Confirmed = "Balance.VoucherStatuses.Accepted",

  /** تصویب شده*/
  Approved = "Balance.VoucherStatuses.Approved",

  /** کلیه اسناد*/
  AllVouchers = "Balance.VoucherStatuses.AllVouchers"
}


export enum BranchScopeResource {
  /**شعبه جاری */
  CurrentBranch = "BranchScope.CurrentBranch",

  /**شعبه جاری و زیر مجموعه ها */
  CurrentBranchAndSubsets = "BranchScope.CurrentBranchAndSubsets"
}

export enum BalanceOptions {
  /// <summary>
  /// عدم انتخاب همه گزینه ها
  /// </summary>
  None = 0x0,

  /// <summary>
  /// گزینه نمایش سند اختتامیه
  /// </summary>
  UseClosingVoucher = 0x1,

  /// <summary>
  /// گزینه نمایش سند بستن حساب ها
  /// </summary>
  UseClosingTempVoucher = 0x2,

  /// <summary>
  /// گزینه انعکاس افتتاحیه در ستون مانده ابتدا
  /// </summary>
  OpeningVoucherAsInitBalance = 0x4,

  /// <summary>
  /// گزینه نمایش سرفصل های با مانده صفر
  /// </summary>
  ShowZeroBalanceItems = 0x8
}

