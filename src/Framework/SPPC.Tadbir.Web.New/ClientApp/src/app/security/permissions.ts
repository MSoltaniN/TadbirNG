// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.646
//     Template Version: 1.0
//     Generation Date: 04/15/1398 03:40:55 ب.ظ
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

export enum AccountGroupPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    All = 15
}

export enum AccountCollectionPermissions {
    None = 0,
    View = 1,
    Create = 2
}

export enum AccountRelationPermissions {
    None = 0,
    ViewRelationships = 1,
    ManageRelationships = 2
}

export enum RowAccessPermissions {
    None = 0,
    ViewRowAccess = 1,
    ManageRowAccess = 2
}

export enum SettingPermissions {
    None = 0,
    ViewSettings = 1,
    ManageSettings = 2
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
    AssignRoles = 16,
    All = 31
}

export enum BranchPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    AssignRoles = 16,
    All = 31
}

export enum OperationLogPermissions {
    None = 0,
    View = 1
}

export enum UserReportPermissions {
    None = 0,
    Save = 1,
    Delete = 2,
    SetDefault = 4
}

export enum ReportPermissions {
    None = 0,
    View = 1,
    Design = 2,
    QuickReportDesign = 4
}

export enum VoucherPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    Delete = 8,
    Navigate = 16,
    Lookup = 32,
    Filter = 64,
    Print = 128,
    Check = 256,
    UndoCheck = 512,
    Confirm = 1024,
    UndoConfirm = 2048,
    Approve = 4096,
    UndoApprove = 8192,
    Finalize = 16384,
    UndoFinalize = 32768,
    All = 65535
}

export enum UserPermissions {
    None = 0,
    View = 1,
    Create = 2,
    Edit = 4,
    AssignRoles = 8,
    All = 15
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

export enum JournalPermissions {
    None = 0,
    View = 1,
    Lookup = 2,
    Filter = 4,
    Print = 8,
    Mark = 16,
    ByBranch = 32
}

export enum AccountBookPermissions {
    None = 0,
    View = 1,
    Lookup = 2,
    Filter = 4,
    Print = 8,
    Mark = 16,
    ByBranch = 32
}

@Injectable()
export class Permissions {
  getPermission(entity: string, premissionName: string): number {

    var id: number = 0;

    switch (entity.toLowerCase()) {
      case "account":
        id = <any>AccountPermissions[<any>premissionName];
        break;
      case "accountcollection":
        id = <any>AccountCollectionPermissions[<any>premissionName];
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
      case "user":
        id = <any>UserPermissions[<any>premissionName];
        break;
      case "role":
        id = <any>RolePermissions[<any>premissionName];
        break;
      case "company":
        id = <any>CompanyPermissions[<any>premissionName];
        break;
      case "accountgroup":
        id = <any>AccountGroupPermissions[<any>premissionName];
        break;
      case "viewrowpermission":
        id = <any>RowAccessPermissions[<any>premissionName];
        break;
      case "setting":
        id = <any>SettingPermissions[<any>premissionName];
        break;
      case "journal":
        id = <any>JournalPermissions[<any>premissionName];
        break;
      case "accountbook":
        id = <any>AccountBookPermissions[<any>premissionName];
        break;
    }



    return id;

  }
};
