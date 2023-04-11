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
    fullAccount: FullAccount;
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