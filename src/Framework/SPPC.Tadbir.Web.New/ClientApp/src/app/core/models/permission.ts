import { IEntity } from "@sppc/shared/models";


// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.282
//     Template Version: 1.0
//     Generation Date: 05/09/2018 01:38:54 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------


export interface Permission extends IEntity{
    groupId: number;
    groupName: string;
    isEnabled: boolean;
    id: number;
    name: string;
    flag: number;
    description?: string;
}
