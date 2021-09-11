
import { Injectable } from "@angular/core";

//this class for all variable in system
export enum MessageType {
  Info,
  Succes,
  Warning,
  Error
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
  OperationLog: 'system/oplog',
  AccountGroup: "accgroups",
  AccountCollection: "acccollections",
  JournalByDateByRow: "journalbydatebyrow",
  JournalByDateByRowDetail: "journalbydatebyrowdetail",
  JournalByDateByLedger: "journalbydatebyledger",
  JournalByDateBySubsidiary: "journalbydatebysubsidiary",
  JournalByDateSummary: "journalbydatesummary",
  JournalByDateSummaryByDate: "journalbydatesummarybydate",
  JournalByDateSummaryByMonth: "journalbydatesummarybymonth",
  JournalByNoByRow: "journalbynobyrow",
  JournalByNoByRowDetail: "journalbynobyrowdetail",
  JournalByNoByLedger: "journalbynobyledger",
  JournalByNoBySubsidiary: "journalbynobysubsidiary",
  JournalByNoSummary: "journalbynosummary",
  NumberList: 'NumberList',
  VoucherLineDetail: 'VoucherLineDetail',  
}


export const Entities = {
  None:'',
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
  Setting: "Setting",
  RowAccess: 'RowAccess',
  OperationLog: "OperationLog",
  AccountGroup: "AccountGroup",
  AccountCollection: "AccountCollection",
  Journal: "Journal",
  AccountBook: "AccountBook",
  JournalByDateByRow: "JournalByDateByRow",
  JournalByDateByRowDetail: "JournalByDateByRowDetail",
  JournalByDateByLedger: "JournalByDateByLedger",
  JournalByDateBySubsidiary: "JournalByDateBySubsidiary",
  JournalByDateSummary: "JournalByDateSummary",
  JournalByDateSummaryByDate: "JournalByDateSummaryByDate",
  JournalByDateSummaryByMonth: "JournalByDateSummaryByMonth",
  JournalByNoByRow: "JournalByNoByRow",
  JournalByNoByRowDetail: "JournalByNoByRowDetail",
  JournalByNoByLedger: "JournalByNoByLedger",
  JournalByNoBySubsidiary: "JournalByNoBySubsidiary",
  JournalByNoSummary: "JournalByNoSummary",
  AccountBookSingle: "AccountBookSingle",
  AccountBookSingleSummary: "AccountBookSingleSummary",
  AccountBookSummary: "AccountBookSummary",
  Currency: "Currency",
  CurrencyRate: 'CurrencyRate',
  TestBalance: 'TestBalance',
  TestBalance2Column: 'TestBalance2Column',
  TestBalance4Column: 'TestBalance4Column',
  TestBalance6Column: 'TestBalance6Column',
  TestBalance8Column: 'TestBalance8Column',
  TestBalance10Column: 'TestBalance10Column',
  CurrencyBook: 'CurrencyBook',
  CurrencyBookSingle: 'CurrencyBookSingle',
  CurrencyBookSingleSummary: 'CurrencyBookSingleSummary',
  CurrencyBookSummary: 'CurrencyBookSummary',
  SystemIssue: 'SystemIssue',
  NumberList: 'NumberList',
  VoucherLineDetail: 'VoucherLineDetail',
  ItemBalance: 'ItemBalance',
  DetailAccountBalance2Column: 'DetailAccountBalance2Column',
  DetailAccountBalance4Column: 'DetailAccountBalance4Column',
  DetailAccountBalance6Column: 'DetailAccountBalance6Column',
  DetailAccountBalance8Column: 'DetailAccountBalance8Column',
  DetailAccountBalance10Column: 'DetailAccountBalance10Column',
  CostCenterBalance2Column: 'CostCenterBalance2Column',
  CostCenterBalance4Column: 'CostCenterBalance4Column',
  CostCenterBalance6Column: 'CostCenterBalance6Column',
  CostCenterBalance8Column: 'CostCenterBalance8Column',
  CostCenterBalance10Column: 'CostCenterBalance10Column',
  ProjectBalance2Column: 'ProjectBalance2Column',
  ProjectBalance4Column: 'ProjectBalance4Column',
  ProjectBalance6Column: 'ProjectBalance6Column',
  ProjectBalance8Column: 'ProjectBalance8Column',
  ProjectBalance10Column: 'ProjectBalance10Column',
  BalanceByAccount: 'BalanceByAccount',
  SysOperationLog: 'SysOperationLog',
  SysOperationLogArchive: 'SysOperationLogArchive',
  OperationLogArchive: 'OperationLogArchive',
  ProfitLost: 'ProfitLost',
  GroupActionResult: 'GroupActionResult',
  ProfitLossSimple: 'ProfitLossSimple',
  ComparativeProfitLoss: 'ComparativeProfitLoss',
  ComparativeProfitLossSimple: 'ComparativeProfitLossSimple',
  DraftVoucher: 'DraftVoucher',
  DraftVouchers: 'DraftVouchers',
  Vouchers: 'Vouchers',
  BalanceSheet:'BalanceSheet'
}

export const CustomForm =
{
  ProfitLoss: "1"
}

export const ColumnVisibility =
{
  Default: "Default",
  AlwaysVisible: "AlwaysVisible",
  AlwaysHidden: "AlwaysHidden",
  Visible: "Visible",
  Hidden: "Hidden"
}


//export const SessionKeys = {
//  Menu: 'menu',
//  Profile: 'profile',
//  Setting: 'setting',
//  LastUserBranch: 'lastUserBranch',
//  LastUserFpId: 'lastUserFpId',
//  CurrentRoute: 'currentRoute',
//  CurrentSkin: 'currentSkin',
//  NumberConfige: 'numberConfig',
//  DateRangeConfig: 'DateRangeConfig',
//  DateRangeSelected: 'DateRangeSelected',
//  MetadataKey: 'metadata_view_{0}_{1}',
//  QuickSearchConfig: 'QuickSearchConfig',
//  SelectForm: 'SelectForm'
//}



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
  //Please dont change BaseUrl variable , If do you need to change this variable after changing and using this file please undo this file !!!
  BaseUrl: 'http://130.185.76.7:8884',
  LicenseServerUrl: "http://130.185.76.7:8886",
  InstanceKey: "Qe2XSr9+vnUsmW7PUX1CbsXZoXxwolNgS/7frHEhjuNTVvDXC2th4L+GKaHek3DoBAwSUIPDVS5bkq6gUuGd6p3AX1bFdokhw56iS6Q/xVNKm/QgPjuKLOoG0pI8xMn9DM6tssGy2ORtOGTDUQtkNFePNX885Pp3xmcydRiNu59WiB+aKngWIEXc5Lx8vIjb1DzzLn24EHD98I0Ag9TQag==",
  Version: "1.1.1222.0"
};
