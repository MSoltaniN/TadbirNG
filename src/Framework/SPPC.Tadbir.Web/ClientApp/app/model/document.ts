// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.282
//     Template Version: 1.0
//     Generation Date: 05/09/2018 09:50:17 ق.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { DocumentAction } from "./documentAction";

export interface Document {
    typeId: number;
    statusId: number;
    statusName: string;
    actions: Array<DocumentAction>;
    id: number;
    entityNo: string;
    no: string;
    operationalStatus: string;
}