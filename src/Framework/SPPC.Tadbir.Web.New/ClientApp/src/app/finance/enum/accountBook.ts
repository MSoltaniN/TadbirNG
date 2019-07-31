
export enum AccountBookDisplayType {
  ByRow = 1,
  VoucherSum = 2,
  DailySum = 3,
  MonthlySum = 4,
}

export enum AccountBookDisplayTypeResource {
  /**مطابق ردیف های سند */
  ByRow = "AccountBook.DisplayByRow",

  /**جمع مبالغ هر سند */
  VoucherSum = "AccountBook.DisplayByVoucherSum",

  /**جمع مبالغ اسناد در هر روز */
  DailySum = "AccountBook.DisplayByDailySum",

  /**جمع مبالغ اسناد در هر ماه */
  MonthlySum = "AccountBook.DisplayByMonthlySum",
}
