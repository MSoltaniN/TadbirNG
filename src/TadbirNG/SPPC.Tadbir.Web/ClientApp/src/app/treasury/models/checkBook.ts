export interface CheckBook {
    id: number,
    checkBookNo: number,
    name: string,
    issueDate: Date,
    startNo: string,
    endNo: string,
    bankName: string,
    isArchived: boolean,
    branchId: number,
    accountId: number,
    detailAccountId: number,
    costCenterId: number,
    projectId: number
}
export interface CheckBookPage {
    id: number,
    checkBookID: number,
    checkBookPageID: number,
    checkID: number,
    serialNo: string,
    status: boolean
}