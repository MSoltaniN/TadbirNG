// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.584
//     Template Version: 1.0
//     Generation Date: 5/19/2019 12:19:44 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------


export interface Voucher {
    fiscalPeriodId: number;
    branchId: number;
    statusId: number;
    statusName: string;
    debitSum: number;
    creditSum: number;
    id: number;
    no: number;
    date: Date;
    reference: string;
    association: string;
    isBalanced: boolean;
    type: number;
    subjectType: number;
    saveCount: number;
    issuedById: number;
    modifiedById: number;
    confirmedById?: number;
    approvedById?: number;
    issuerName: string;
    modifierName: string;
    confirmerName: string;
    approverName: string;
    description?: string;
}
