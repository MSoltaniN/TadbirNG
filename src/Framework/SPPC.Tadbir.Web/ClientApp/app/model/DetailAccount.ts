import { IEntity } from "./IEntity";

// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.282
//     Template Version: 1.0
//     Generation Date: 05/09/2018 12:23:51 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------


export interface DetailAccount extends IEntity{
    parentId?: number;
    fiscalPeriodId: number;
    branchId: number;
    companyId: number;
    childCount: number;
    id: number;
    code: string;
    fullCode: string;
    name: string;
    level: number;
    description?: string;
}