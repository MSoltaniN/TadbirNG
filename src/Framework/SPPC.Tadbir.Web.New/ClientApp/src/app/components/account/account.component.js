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
//#region Imports
var core_1 = require("@angular/core");
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var api_1 = require("../../service/api");
var gridExplorer_component_1 = require("../../class/gridExplorer.component");
//#endregion
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var AccountComponent = /** @class */ (function (_super) {
    __extends(AccountComponent, _super);
    function AccountComponent(toastrService, translate, accountService, dialogService, renderer, metadata, settingService) {
        var _this = _super.call(this, toastrService, translate, accountService, dialogService, renderer, metadata, settingService, environment_1.Entities.Account, environment_1.Metadatas.Account) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.accountService = accountService;
        _this.dialogService = dialogService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        return _this;
    }
    AccountComponent.prototype.ngOnInit = function () {
        var _this = this;
        setTimeout(function () {
            _this.treeParentTitle = _this.getText('Account.LedgerAccount');
        });
        //this.treeParentTitle = this.getText('Account.LedgerAccount');
        this.environmentModelsUrl = api_1.AccountApi.EnvironmentAccounts;
        this.environmentModelsLedgerUrl = api_1.AccountApi.EnvironmentAccountsLedger;
        this.modelUrl = api_1.AccountApi.Account;
        this.modelChildrenUrl = api_1.AccountApi.AccountChildren;
        this.getTreeNode();
        this.reloadGrid();
    };
    AccountComponent = __decorate([
        core_1.Component({
            selector: 'account',
            templateUrl: './account.component.html',
            styles: ["\n\n  "],
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], AccountComponent);
    return AccountComponent;
}(gridExplorer_component_1.GridExplorerComponent));
exports.AccountComponent = AccountComponent;
//# sourceMappingURL=account.component.js.map