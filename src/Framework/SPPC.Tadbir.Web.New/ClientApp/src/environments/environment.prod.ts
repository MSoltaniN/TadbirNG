
import { Injectable } from "@angular/core";

//this class for all variable in system
export enum MessageType {
  Info,
  Succes,
  Warning
}

export const Metadatas = {
  Voucher: 'vouchers',
  Account: 'accounts',
  VoucherArticles: 'vouchers/articles',
  User: 'users',
  Role: 'roles',
  DetailAccount: 'faccounts',
  CostCenter: 'ccenters',
  Project: 'projects',
  FiscalPeriod: 'fperiods',
  Branch: 'branches',
  Company: 'companies',
  OperationLog: 'system/oplog'
}


export const Entities = {
  Voucher: 'Voucher',
  Account: 'Account',
  VoucherLine: 'VoucherLine',
  User: 'User',
  Role: 'Role',
  Password: 'Password',
  DetailAccount: 'DetailAccount',
  CostCenter: 'CostCenter',
  Project: 'Project',
  FiscalPeriod: 'FiscalPeriod',
  Branch: 'Branch',
  Company: 'Company',
  AccountRelations: "AccountRelations",
  Settings: "Settings",
  ViewRowPermission: "ViewRowPermission",
  OperationLog: "OperationLog"
}

export const ColumnVisibility =
  {
    Default: "Default",
    AlwaysVisible: "AlwaysVisible",
    AlwaysHidden: "AlwaysHidden",
    Visible: "Visible",
    Hidden: "Hidden"
  }


export const SessionKeys = {
  Menu: 'menu',
  Setting: 'setting',
  LastUserBranch: 'lastUserBranch',
  LastUserFpId: 'lastUserFpId',
  CurrentRoute: 'currentRoute'

}



export enum MessagePosition {
  TopLeft,
  TopCenter,
  TopRight,
  MiddleLeft,
  MiddleRight,
  BottomLeft,
  BottomCenter,
  BottomRight
}

@Injectable()
export class Layout {
  getLayout(): boolean {
    var lang = localStorage.getItem('lang');
    if (lang == "en") {
      return false;
    }
    else
      return true;
  }
};

export const environment = {
  production: true,
  BaseUrl: 'http://http://130.185.76.7:9095',
};
