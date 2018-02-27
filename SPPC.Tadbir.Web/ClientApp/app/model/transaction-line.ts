import { FullAccount } from "./fullaccount";


export interface TransactionLine {
    id: number;
    debit: number;
    credit: number;
    description?: string;
    fiscalPeriodId: number;
    branchId: number;
    transactionId: number;
    currencyId: number;
    currencyName: string;
    fullAccount: FullAccount;
}

