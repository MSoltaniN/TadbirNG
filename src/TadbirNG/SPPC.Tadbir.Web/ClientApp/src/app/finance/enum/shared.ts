
export enum VoucherStatusType {
    /** ثبت شده*/
  Committed = "2",

  /** ثبت قطعی شده*/
  Finalized = "3",

  /** تایید شده*/
  Confirmed = "4",

  /** تصویب شده*/
  Approved = "5",

  /** کلیه اسناد*/
  AllVouchers = "0"
  }

  export enum BranchScopeType {
    CurrentBranch = "1",
    CurrentBranchAndSubsets = "2"
 }

 export enum BookDisplayType {
  ByRow = "1",
  VoucherSum = "2",
  DailySum = "3",
  MonthlySum = "4",
}

export enum BookDisplayTypeResource {
  /**مطابق ردیف های سند */
  ByRow = "AccountBook.DisplayByRow",

  /**جمع مبالغ هر سند */
  VoucherSum = "AccountBook.DisplayByVoucherSum",

  /**جمع مبالغ اسناد در هر روز */
  DailySum = "AccountBook.DisplayByDailySum",

  /**جمع مبالغ اسناد در هر ماه */
  MonthlySum = "AccountBook.DisplayByMonthlySum",
}

   