
export interface CostCenterViewModel {
    id: number;
    code: string;
    fullCode: string;
    name: string;
    level: number;
    description?: string;
    parentId?: number;
    childCount: number;
    fiscalPeriodId: number;
    branchId: number;
}