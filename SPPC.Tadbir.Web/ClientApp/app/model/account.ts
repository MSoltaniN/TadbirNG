
/** define account interface */

export interface Account {
    id: number;
    code: string;
    name: string;
    description?: string;
    fiscalPeriodId: number;
    branchId: number;
    fullCode: string;
    level: number;
}

