
export interface DetailAccountViewModel {
    id: number;
    code: string;
    fullCode: string;
    name: string;
    level: number;
    description?: string;
    parentId?: number;
    fiscalPeriodId: number;
    branchId: number;
}