import { FullAccount } from "@sppc/finance/models"

export interface CheckBook {
    id: number,
    checkBookNo: number,
    name: string,
    issueDate: Date,
    startNo: string,
    endNo: string,
    bankName: string,
    isArchived: boolean,
    branchId: number;
    fullAccount: FullAccount;
    pageCount: number;
    hasNext: boolean;
    hasPrevious: boolean;
}
export interface CheckBookPage {
    id: number,
    checkBookID: number,
    checkBookPageID: number,
    checkID: number,
    serialNo: string,
    status: boolean
}