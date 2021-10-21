
export enum VoucherOperations {
  First = 1,
  Last = 2,
  Next = 3,
  Previous = 4,
  New = 5,
  Search = 6,
  CheckVoucher = 7
}


export enum VoucherSubjectTypes {
  Normal = "0",
  Draft = "1",
}

export enum VoucherMessageResource {
  /** ثبت نشده*/
  NotCommitted = "Voucher.VoucherStatuses.NotCommitted",

  /** ثبت قطعی نشده*/
  NotFinalized = "Voucher.VoucherStatuses.NotFinalized",

  /** تایید نشده*/
  NotConfirmed = "Voucher.VoucherStatuses.NotConfirmed",

  /** تصویب نشده*/
  NotApproved = "Voucher.VoucherStatuses.NotApproved",
}
