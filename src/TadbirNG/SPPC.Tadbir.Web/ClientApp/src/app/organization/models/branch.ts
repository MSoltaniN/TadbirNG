import { IEntity } from "@sppc/shared/models";

// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.298
//     Template Version: 1.0
//     Generation Date: 05/22/2018 01:40:00 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------


export interface Branch extends IEntity {
    parentId?: number;
    id: number;
    name: string;
    description?: string;
    level: number;
}
