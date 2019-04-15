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
var service_1 = require("../../service");
var api_1 = require("../../service/api");
var source_1 = require("../../class/source");
var account_form_component_1 = require("./account-form.component");
var viewName_1 = require("../../security/viewName");
var gridExplorer_component_1 = require("../../class/gridExplorer.component");
//#endregion
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var AccountComponent = /** @class */ (function (_super) {
    __extends(AccountComponent, _super);
    function AccountComponent(toastrService, translate, service, dialogService, renderer, metadata, settingService) {
        var _this = _super.call(this, toastrService, translate, service, dialogService, renderer, metadata, settingService, environment_1.Entities.Account, environment_1.Metadatas.Account, "Account.LedgerAccount", "Account.EditorTitleNew", "Account.EditorTitleEdit", api_1.AccountApi.EnvironmentAccounts, api_1.AccountApi.EnvironmentAccountsLedger, api_1.AccountApi.Account, api_1.AccountApi.AccountChildren, viewName_1.ViewName.Account) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.service = service;
        _this.dialogService = dialogService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        return _this;
    }
    /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
    AccountComponent.prototype.openEditorDialog = function (isNew) {
        var _this = this;
        var errorMsg = this.getText('Messages.TreeLevelsAreTooDeep');
        var editorTitle = this.getEditorTitle(isNew);
        if (this.levelConfig)
            if (this.levelConfig.isEnabled) {
                this.dialogRef = this.dialogService.open({
                    title: editorTitle,
                    content: account_form_component_1.AccountFormComponent,
                });
                this.dialogModel = this.dialogRef.content.instance;
                this.dialogModel.parent = this.parent;
                this.dialogModel.model = this.editDataItem;
                this.dialogModel.isNew = isNew;
                this.dialogModel.errorMessage = undefined;
                this.dialogRef.content.instance.save.subscribe(function (res) {
                    _this.saveHandler(res, isNew);
                });
                var closeForm = this.dialogRef.content.instance.cancel.subscribe(function (res) {
                    _this.dialogRef.close();
                });
            }
            else {
                this.showMessage(source_1.String.Format(errorMsg, (this.levelConfig.no - 1).toString()), environment_1.MessageType.Warning);
            }
    };
    AccountComponent.prototype.addNew = function () {
        this.editDataItem = new service_1.AccountInfo();
        this.openEditorDialog(true);
    };
    AccountComponent = __decorate([
        core_1.Component({
            selector: 'account',
            templateUrl: './account.component.html',
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