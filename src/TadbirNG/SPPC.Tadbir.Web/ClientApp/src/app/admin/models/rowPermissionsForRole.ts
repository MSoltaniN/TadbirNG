import { IEntity } from "@sppc/shared/models";
import { ViewRowPermission } from ".";

// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.349
//     Template Version: 1.0
//     Generation Date: 07/22/2018 12:50:26 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------



export interface RowPermissionsForRole extends IEntity{
    id: number;
    rowPermissions: Array<ViewRowPermission>;
}
