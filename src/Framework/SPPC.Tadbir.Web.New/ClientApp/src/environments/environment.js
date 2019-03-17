"use strict";
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
//this class for all variable in system
var MessageType;
(function (MessageType) {
    MessageType[MessageType["Info"] = 0] = "Info";
    MessageType[MessageType["Succes"] = 1] = "Succes";
    MessageType[MessageType["Warning"] = 2] = "Warning";
})(MessageType = exports.MessageType || (exports.MessageType = {}));
//export const Metadatas = {
//    Transaction: 'transaction',
//    Account: 'account' /*'accounts'*/,
//    TransactionArticles: 'TransactionLine'//'transactions/articles'
//}
exports.Metadatas = {
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
    JournalByDateSummaryByDate: "journalbydatesummarybydate"
};
exports.Entities = {
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
    OperationLog: "OperationLog",
    AccountGroup: "AccountGroup",
    AccountCollection: "AccountCollection",
    JournalByDateByRow: "JournalByDateByRow",
    JournalByDateByRowDetail: "JournalByDateByRowDetail",
    JournalByDateByLedger: "JournalByDateByLedger",
    JournalByDateBySubsidiary: "JournalByDateBySubsidiary",
    JournalByDateSummary: "JournalByDateSummary",
    JournalByDateSummaryByDate: "JournalByDateSummaryByDate"
};
exports.ColumnVisibility = {
    Default: "Default",
    AlwaysVisible: "AlwaysVisible",
    AlwaysHidden: "AlwaysHidden",
    Visible: "Visible",
    Hidden: "Hidden"
};
exports.SessionKeys = {
    Menu: 'menu',
    Profile: 'profile',
    Setting: 'setting',
    LastUserBranch: 'lastUserBranch',
    LastUserFpId: 'lastUserFpId',
    CurrentRoute: 'currentRoute',
    CurrentSkin: 'currentSkin',
    NumberConfige: 'numberConfig'
};
var MessagePosition;
(function (MessagePosition) {
    MessagePosition[MessagePosition["TopLeft"] = 0] = "TopLeft";
    MessagePosition[MessagePosition["TopCenter"] = 1] = "TopCenter";
    MessagePosition[MessagePosition["TopRight"] = 2] = "TopRight";
    MessagePosition[MessagePosition["MiddleLeft"] = 3] = "MiddleLeft";
    MessagePosition[MessagePosition["MiddleRight"] = 4] = "MiddleRight";
    MessagePosition[MessagePosition["BottomLeft"] = 5] = "BottomLeft";
    MessagePosition[MessagePosition["BottomCenter"] = 6] = "BottomCenter";
    MessagePosition[MessagePosition["BottomRight"] = 7] = "BottomRight";
})(MessagePosition = exports.MessagePosition || (exports.MessagePosition = {}));
exports.environment = {
    production: false,
    BaseUrl: 'http://localhost:8801',
    AdminTicket: 'eyJVc2VyIjp7IklkIjoxLCJQZXJzb25GaXJzdE5hbWUiOiIiLCJQZXJzb25MYXN0TmFtZSI6IiIsIkJyYW5jaGVzIjpbMSwyXSwiUm9sZXMiOlsxXSwiUGVybWlzc2lvbnMiOlt7IkVudGl0eU5hbWUiOiJBY2NvdW50IiwiRmxhZ3MiOjE1fSx7IkVudGl0eU5hbWUiOiJUcmFuc2FjdGlvbiIsIkZsYWdzIjoxMDIzfSx7IkVudGl0eU5hbWUiOiJVc2VyIiwiRmxhZ3MiOjd9LHsiRW50aXR5TmFtZSI6IlJvbGUiLCJGbGFncyI6NjN9LHsiRW50aXR5TmFtZSI6IlJlcXVpc2l0aW9uVm91Y2hlciIsIkZsYWdzIjoxMjd9LHsiRW50aXR5TmFtZSI6Iklzc3VlUmVjZWlwdFZvdWNoZXIiLCJGbGFncyI6NjN9LHsiRW50aXR5TmFtZSI6IlNhbGVzSW52b2ljZSIsIkZsYWdzIjozMX0seyJFbnRpdHlOYW1lIjoiUHJvZHVjdEludmVudG9yeSIsIkZsYWdzIjoxNX1dfX0=',
    BranchId: 1,
    FiscalPeriodId: 1
};
var Layout = /** @class */ (function () {
    function Layout() {
    }
    Layout.prototype.getLayout = function () {
        var lang = localStorage.getItem('lang');
        if (lang == "en") {
            return false;
        }
        else
            return true;
    };
    Layout = __decorate([
        core_1.Injectable()
    ], Layout);
    return Layout;
}());
exports.Layout = Layout;
;
//# sourceMappingURL=environment.js.map