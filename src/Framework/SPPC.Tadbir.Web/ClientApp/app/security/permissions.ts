import { Injectable } from "@angular/core";

// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.288
//     Template Version: 1.0
//     Generation Date: 2018-05-13 1:13:36 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------


export enum AccountPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum DetailAccountPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum CostCenterPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum ProjectPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum AccountRelationPermissions {
    None = 0,
    ViewRelationships = 1,
    ManageRelationships = 2
}

export enum CurrencyPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum FiscalPeriodPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum BranchPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum VoucherPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    Prepare = 16,
    Review = 32,
    Confirm = 64,
    Approve = 128,
    All = 255
}

export enum BusinessUnitPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum BusinessPartnerPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum UserPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    AssignRoles = 8,
    All = 7
}

export enum RolePermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    AssignUsers = 16,
    AssignBranches = 32,
    AssignFiscalPeriods = 64,
    All = 63
}

export enum ProductInventoryPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum RequisitionPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    Prepare = 16,
    Confirm = 32,
    Approve = 64,
    All = 127
}

export enum CompanyPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}


@Injectable()
export class Permissions {
    getPermission(entity: string, premissionName: string): number {

        var id: number = 0;

        switch (entity.toLowerCase()) {
            case "account":
                id = <any>AccountPermissions[<any>premissionName];
                break;
            case "detailaccount":
                id = <any>DetailAccountPermissions[<any>premissionName];
                break;
            case "costcenter":
                id = <any>CostCenterPermissions[<any>premissionName];
                break;
            case "project":
                id = <any>ProjectPermissions[<any>premissionName];
                break;
            case "accountrelations":
                id = <any>AccountRelationPermissions[<any>premissionName];
                break;
            case "currency":
                id = <any>CurrencyPermissions[<any>premissionName];
                break;
            case "fiscalperiod":
                id = <any>FiscalPeriodPermissions[<any>premissionName];
                break;
            case "branch":
                id = <any>BranchPermissions[<any>premissionName];
                break;
            case "voucher":
                id = <any>VoucherPermissions[<any>premissionName];
                break;
            case "businessunit":
                id = <any>BusinessUnitPermissions[<any>premissionName];
                break;
            case "businesspartner":
                id = <any>BusinessPartnerPermissions[<any>premissionName];
                break;
            case "user":
                id = <any>UserPermissions[<any>premissionName];
                break;
            case "role":
                id = <any>RolePermissions[<any>premissionName];
                break;
            case "productinventory":
                id = <any>ProductInventoryPermissions[<any>premissionName];
                break;
            case "requisition":
                id = <any>RequisitionPermissions[<any>premissionName];
                break;
            case "company":
                id = <any>CompanyPermissions[<any>premissionName];
                break;

        }



        return id;

    }
};

