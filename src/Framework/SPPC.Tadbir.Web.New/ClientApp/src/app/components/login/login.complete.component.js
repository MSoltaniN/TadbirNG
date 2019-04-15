"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var index_1 = require("../../service/login/index");
var default_component_1 = require("../../class/default.component");
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var settingsKey_1 = require("../../enum/settingsKey");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var LoginCompleteComponent = /** @class */ (function (_super) {
    __extends(LoginCompleteComponent, _super);
    //#endregion
    //#region Constructor
    function LoginCompleteComponent(route, router, authenticationService, toastrService, translate, renderer, metadata, userService, settingService) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, settingService, '', '') || this;
        _this.route = route;
        _this.router = router;
        _this.authenticationService = authenticationService;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.userService = userService;
        _this.settingService = settingService;
        //#region Fields
        _this.model = {};
        _this.loading = false;
        _this.ticket = '';
        _this.disabledBranch = true;
        _this.disabledFiscalPeriod = true;
        _this.disabledCompany = true;
        _this.compenies = [];
        _this.branches = [];
        _this.fiscalPeriods = [];
        _this.companyId = '';
        _this.branchId = '';
        _this.fiscalPeriodId = '';
        return _this;
    }
    //#endregion
    //#region Events
    LoginCompleteComponent.prototype.ngOnInit = function () {
        debugger;
        this.currentRoute = sessionStorage.getItem(environment_1.SessionKeys.CurrentRoute);
        this.disabledCompany = true;
        this.getCompany();
        //load setting
        this.loadAllSetting();
        // var currentLang = localStorage.getItem('lang')
        // if(currentLang == 'fa')
        //      this.document.getElementById('adminlte').setAttribute('href', 'assets/dist/css/AdminLTE.Rtl.css');
        // else
        //      this.document.getElementById('adminlte').setAttribute('href', 'assets/dist/css/AdminLTE.min.css');
        // this.document.getElementById('adminlteSkin').setAttribute('href', 'assets/dist/css/skins/_all-skins.min.css');
    };
    //#endregion
    //#region Methods
    LoginCompleteComponent.prototype.branchChange = function (value) {
        this.fiscalPeriodId = '';
    };
    LoginCompleteComponent.prototype.companyChange = function (value) {
        this.disabledBranch = true;
        this.disabledFiscalPeriod = true;
        this.branches = [];
        this.branchId = '';
        this.fiscalPeriods = [];
        this.fiscalPeriodId = '';
        this.getBranch(value);
        this.getFiscalPeriod(value);
        var lastBranchId = localStorage.getItem(environment_1.SessionKeys.LastUserBranch + this.UserId + this.companyId);
        var lastFpId = localStorage.getItem(environment_1.SessionKeys.LastUserFpId + this.UserId + this.companyId);
        if (lastBranchId)
            this.branchId = lastBranchId;
        if (lastFpId)
            this.fiscalPeriodId = lastFpId;
    };
    LoginCompleteComponent.prototype.getCompany = function () {
        var _this = this;
        this.authenticationService.getCompanies(this.UserName, this.Ticket).subscribe(function (res) {
            _this.compenies = res;
            _this.disabledCompany = false;
            //#region load current setting
            if (_this.CompanyId) {
                _this.companyId = _this.CompanyId.toString();
                _this.companyChange(_this.companyId);
            }
            //#endregion
        });
        ;
    };
    LoginCompleteComponent.prototype.getBranch = function (companyId) {
        var _this = this;
        this.authenticationService.getBranches(companyId, this.Ticket).subscribe(function (res) {
            _this.disabledBranch = false;
            _this.branches = res;
        });
    };
    LoginCompleteComponent.prototype.getFiscalPeriod = function (companyId) {
        var _this = this;
        this.authenticationService.getFiscalPeriod(companyId, this.Ticket).subscribe(function (res) {
            _this.disabledFiscalPeriod = false;
            _this.fiscalPeriods = res;
        });
    };
    LoginCompleteComponent.prototype.isValidate = function () {
        var isValidate = true;
        if (this.companyId == '') {
            this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), environment_1.MessageType.Info, '', environment_1.MessagePosition.TopCenter);
            this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), environment_1.MessageType.Info, '', environment_1.MessagePosition.TopCenter);
            this.showMessage(this.getText("AllValidations.Login.CompanyIsRequired"), environment_1.MessageType.Info, '', environment_1.MessagePosition.TopCenter);
            isValidate = false;
            return isValidate;
        }
        if (this.branchId == '') {
            this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), environment_1.MessageType.Info, '', environment_1.MessagePosition.TopCenter);
            isValidate = false;
        }
        if (this.fiscalPeriodId == '') {
            this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), environment_1.MessageType.Info, '', environment_1.MessagePosition.TopCenter);
            isValidate = false;
        }
        return isValidate;
    };
    LoginCompleteComponent.prototype.selectParams = function () {
        //sessionStorage.removeItem("viewTreeConfig");
        if (this.isValidate()) {
            if (this.authenticationService.islogin()) {
                this.getCompanyTicket();
            }
        }
    };
    LoginCompleteComponent.prototype.onCancleClick = function () {
        if (this.authenticationService.islogin()) {
            var currentUser = this.authenticationService.getCurrentUser();
            if (currentUser != null) {
                this.companyId = currentUser.companyId.toString();
                this.branchId = currentUser.branchId.toString();
                this.fiscalPeriodId = currentUser.fpId.toString();
                this.loadMenuAndRoute(currentUser);
            }
        }
    };
    LoginCompleteComponent.prototype.loadAllSetting = function () {
        var _this = this;
        var settingList = new Array();
        this.settingService.getListSettingsByUser(this.UserId).subscribe(function (res) {
            if (res)
                localStorage.setItem(environment_1.SessionKeys.Setting + _this.UserId, JSON.stringify(res));
        });
        this.settingService.getSettingById(settingsKey_1.SettingKey.NumberDisplayConfig).subscribe(function (res) {
            if (res)
                localStorage.setItem(environment_1.SessionKeys.NumberConfige, JSON.stringify(res.values));
        });
        this.settingService.getSettingById(settingsKey_1.SettingKey.DateRangeConfig).subscribe(function (res) {
            if (res)
                localStorage.setItem(environment_1.SessionKeys.DateRangeConfig, JSON.stringify(res.values));
        });
    };
    LoginCompleteComponent.prototype.loadMenuAndRoute = function (currentUser) {
        var _this = this;
        //#region load menu
        var menuList = new Array();
        var commands;
        this.authenticationService.getFiscalPeriodById(currentUser.fpId, this.Ticket).subscribe(function (res) {
            if (_this.authenticationService.isRememberMe())
                localStorage.setItem('fiscalPeriod', JSON.stringify(res));
            else
                sessionStorage.setItem('fiscalPeriod', JSON.stringify(res));
        });
        this.userService.getCurrentUserCommands(this.Ticket).subscribe(function (res) {
            var list = res;
            if (_this.authenticationService.isRememberMe()) {
                localStorage.setItem(environment_1.SessionKeys.Menu, JSON.stringify(res));
                localStorage.setItem('currentContext', JSON.stringify(currentUser));
            }
            else {
                sessionStorage.setItem(environment_1.SessionKeys.Menu, JSON.stringify(res));
                sessionStorage.setItem('currentContext', JSON.stringify(currentUser));
            }
            if (_this.route.snapshot.queryParams['returnUrl'] != undefined) {
                var url = _this.route.snapshot.queryParams['returnUrl'];
                _this.router.navigate([url]);
            }
            else {
                debugger;
                var currentRoute = sessionStorage.getItem(environment_1.SessionKeys.CurrentRoute);
                if (currentRoute) {
                    sessionStorage.removeItem(environment_1.SessionKeys.CurrentRoute);
                    _this.router.navigate([currentRoute]);
                }
                else {
                    _this.router.navigate(['/dashboard']);
                }
            }
        });
        this.userService.getDefaultUserCommands(this.Ticket).subscribe(function (res) {
            var list = res;
            if (_this.authenticationService.isRememberMe()) {
                localStorage.setItem(environment_1.SessionKeys.Profile, JSON.stringify(res));
            }
            else {
                sessionStorage.setItem(environment_1.SessionKeys.Profile, JSON.stringify(res));
            }
        });
        //#endregion
    };
    /**
     * تیکت امنیتی را مطابق شرکت و شعبه و دوره مالی از سرویس میگیرد و جایگزین تیکت قبلی میکند
     */
    LoginCompleteComponent.prototype.getCompanyTicket = function () {
        var _this = this;
        var companyLoginModel = new index_1.CompanyLoginInfo();
        companyLoginModel.companyId = parseInt(this.companyId);
        companyLoginModel.branchId = parseInt(this.branchId);
        companyLoginModel.fiscalPeriodId = parseInt(this.fiscalPeriodId);
        this.authenticationService.getCompanyTicket(companyLoginModel, this.Ticket).subscribe(function (res) {
            if (res.headers != null) {
                var newTicket = res.headers.get('X-Tadbir-AuthTicket');
                var currentUser = _this.authenticationService.getCurrentUser();
                if (currentUser != null) {
                    currentUser.branchId = parseInt(_this.branchId);
                    currentUser.companyId = parseInt(_this.companyId);
                    currentUser.fpId = parseInt(_this.fiscalPeriodId);
                    currentUser.permissions = JSON.parse(atob(_this.Ticket)).user.permissions;
                    currentUser.ticket = newTicket;
                    if (_this.authenticationService.isRememberMe())
                        localStorage.setItem('currentContext', JSON.stringify(currentUser));
                    else
                        sessionStorage.setItem('currentContext', JSON.stringify(currentUser));
                    localStorage.setItem(environment_1.SessionKeys.LastUserBranch + _this.UserId + _this.companyId, _this.branchId);
                    localStorage.setItem(environment_1.SessionKeys.LastUserFpId + _this.UserId + _this.companyId, _this.fiscalPeriodId);
                    _this.loadMenuAndRoute(currentUser);
                }
            }
        });
    };
    LoginCompleteComponent = __decorate([
        core_1.Component({
            selector: 'logincomplete',
            templateUrl: 'login.complete.component.html',
            styleUrls: ['./login.complete.component.css'],
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], LoginCompleteComponent);
    return LoginCompleteComponent;
}(default_component_1.DefaultComponent));
exports.LoginCompleteComponent = LoginCompleteComponent;
//# sourceMappingURL=login.complete.component.js.map