import { Injectable } from "@angular/core";

//this class for all variable in system
export enum MessageType {
    Info,
    Succes,
    Warning
}


//export const Metadatas = {
//    Transaction: 'transaction',
//    Account: 'account' /*'accounts'*/,
//    TransactionArticles: 'TransactionLine'//'transactions/articles'
//}

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
    Company: 'companies'
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
    ViewRowPermission:"ViewRowPermission"
}

export const ColumnVisibility =
{
    Default : "Default",
    AlwaysVisible : "AlwaysVisible",
    AlwaysHidden : "AlwaysHidden",
    Visible : "Visible",
    Hidden : "Hidden"
}


export const SessionKeys = {
    Menu: 'menu',
    Setting: 'setting',
    LastUserBranch: 'lastUserBranch',
    LastUserFpId: 'lastUserFpId'
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


export const Environment = {
    BaseUrl: 'http://localhost:8801',
    AdminTicket: 'eyJVc2VyIjp7IklkIjoxLCJQZXJzb25GaXJzdE5hbWUiOiIiLCJQZXJzb25MYXN0TmFtZSI6IiIsIkJyYW5jaGVzIjpbMSwyXSwiUm9sZXMiOlsxXSwiUGVybWlzc2lvbnMiOlt7IkVudGl0eU5hbWUiOiJBY2NvdW50IiwiRmxhZ3MiOjE1fSx7IkVudGl0eU5hbWUiOiJUcmFuc2FjdGlvbiIsIkZsYWdzIjoxMDIzfSx7IkVudGl0eU5hbWUiOiJVc2VyIiwiRmxhZ3MiOjd9LHsiRW50aXR5TmFtZSI6IlJvbGUiLCJGbGFncyI6NjN9LHsiRW50aXR5TmFtZSI6IlJlcXVpc2l0aW9uVm91Y2hlciIsIkZsYWdzIjoxMjd9LHsiRW50aXR5TmFtZSI6Iklzc3VlUmVjZWlwdFZvdWNoZXIiLCJGbGFncyI6NjN9LHsiRW50aXR5TmFtZSI6IlNhbGVzSW52b2ljZSIsIkZsYWdzIjozMX0seyJFbnRpdHlOYW1lIjoiUHJvZHVjdEludmVudG9yeSIsIkZsYWdzIjoxNX1dfX0=',
    BranchId: 1,
    FiscalPeriodId: 1
};


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

