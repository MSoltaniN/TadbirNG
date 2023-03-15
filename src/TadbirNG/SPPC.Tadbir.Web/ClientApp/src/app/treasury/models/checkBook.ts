export interface CheckBook {
    id: number,
    checkBookNo: number,
    name: string,
    issueDate: string,
    startNo: string,
    endNo: string,
    bankName: string,
    isArchived: boolean,
    branchId: number,
    accountId: number,
    detailId: number,
    costCenterId: number,
    projectId: number
}