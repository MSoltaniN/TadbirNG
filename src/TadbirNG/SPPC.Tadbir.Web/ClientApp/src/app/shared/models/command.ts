import { IEntity } from "./IEntity";

// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.640
//     Template Version: 1.0
//     Generation Date: 2019-07-01 15:47:43 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

export interface Command extends IEntity {
    permissionId?: number;
    children: Array<Command>;
    id: number;
    title: string;
    routeUrl: string;
    iconName: string;
    hotKey: string;
}
