export const UrlPathType = {
    Payments: "payments",
    Receipts: "receipts"
}
export enum PayReceiveOperations {
    First = 1,
    Last = 2,
    Next = 3,
    Previous = 4,
    New = 5,
    Search = 6,
    Aggregate = 7,
    RemoveInvalidRows = 8
}
/**
* نوع فرم؛ 0 برای دریافت و 1 برای پرداخت
*/
export enum PayReceiveTypes {
    Receipt = 0,
    Payment = 1
}