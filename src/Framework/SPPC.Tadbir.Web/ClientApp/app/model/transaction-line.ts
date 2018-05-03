import { FullAccountViewModel } from "./fullaccount";

//export interface TransactionLine {
//    id: number;
//    debit: number;
//    credit: number;
//    description?: string;
//    fiscalPeriodId: number;
//    branchId: number;
//    transactionId: number;
//    currencyId: number;
//    accountId: number;
//    detailId?: number;
//    costCenterId?: number;
//    projectId?: number;    
//}

export interface TransactionLineViewModel {
    id: number;
    debit: number;
    credit: number;
    description?: string;
    fiscalPeriodId: number;
    branchId: number;
    voucherId: number;
    currencyId: number; 
    fullAccount: FullAccountViewModel;
}