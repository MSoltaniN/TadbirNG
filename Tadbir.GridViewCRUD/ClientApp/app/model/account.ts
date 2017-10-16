
/** define account interface */

export interface Account {
    accountId: number;
    code: string;
    name: string;
    description?: string;
    fiscalPeriodId: number;
    branchId: number;
}

