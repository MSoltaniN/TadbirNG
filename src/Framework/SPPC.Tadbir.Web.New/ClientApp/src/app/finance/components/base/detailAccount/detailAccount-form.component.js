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
var default_component_1 = require("../../class/default.component");
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var detail_component_1 = require("../../class/detail.component");
var viewName_1 = require("../../security/viewName");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var DetailAccountFormComponent = /** @class */ (function (_super) {
    __extends(DetailAccountFormComponent, _super);
    function DetailAccountFormComponent(toastrService, translate, renderer, metadata) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, environment_1.Entities.DetailAccount, environment_1.Metadatas.DetailAccount) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.parentScopeValue = 0;
        _this.parentFullCode = '';
        _this.level = 0;
        _this.isNew = false;
        _this.errorMessage = '';
        _this.save = new core_1.EventEmitter();
        _this.cancel = new core_1.EventEmitter();
        return _this;
    }
    //create properties
    //Events
    DetailAccountFormComponent.prototype.onSave = function (e) {
        e.preventDefault();
        var model = this.editForm.value;
        if (this.editForm.valid) {
            if (this.model.id > 0) {
                model.branchId = this.model.branchId;
                model.fiscalPeriodId = this.model.fiscalPeriodId;
                model.companyId = this.model.companyId;
            }
            else {
                model.branchId = this.BranchId;
                model.fiscalPeriodId = this.FiscalPeriodId;
                model.companyId = this.CompanyId;
                model.parentId = this.parent ? this.parent.id : undefined;
                model.level = this.level;
            }
            this.save.emit(model);
        }
    };
    DetailAccountFormComponent.prototype.onCancel = function (e) {
        e.preventDefault();
        this.cancel.emit();
    };
    DetailAccountFormComponent.prototype.escPress = function () {
        this.cancel.emit();
    };
    //Events
    DetailAccountFormComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.viewId = viewName_1.ViewName.DetailAccount;
        this.editForm.reset();
        this.parentScopeValue = 0;
        if (this.parent) {
            this.parentFullCode = this.parent.fullCode;
            this.model.fullCode = this.parentFullCode;
            this.parentScopeValue = this.parent.branchScope;
            this.level = this.parent.level + 1;
        }
        else {
            this.level = 0;
        }
        if (this.model && this.model.code)
            this.model.fullCode = this.parentFullCode + this.model.code;
        setTimeout(function () {
            _this.editForm.reset(_this.model);
        });
    };
    __decorate([
        core_1.Input()
    ], DetailAccountFormComponent.prototype, "parent", void 0);
    __decorate([
        core_1.Input()
    ], DetailAccountFormComponent.prototype, "model", void 0);
    __decorate([
        core_1.Input()
    ], DetailAccountFormComponent.prototype, "isNew", void 0);
    __decorate([
        core_1.Input()
    ], DetailAccountFormComponent.prototype, "errorMessage", void 0);
    __decorate([
        core_1.Output()
    ], DetailAccountFormComponent.prototype, "save", void 0);
    __decorate([
        core_1.Output()
    ], DetailAccountFormComponent.prototype, "cancel", void 0);
    DetailAccountFormComponent = __decorate([
        core_1.Component({
            selector: 'detailAccount-form-component',
            styles: [
                "input[type=text],textarea { width: 100%; }  /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}"
            ],
            templateUrl: './detailAccount-form.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }, default_component_1.DefaultComponent]
        })
    ], DetailAccountFormComponent);
    return DetailAccountFormComponent;
}(detail_component_1.DetailComponent));
exports.DetailAccountFormComponent = DetailAccountFormComponent;
//# sourceMappingURL=detailAccount-form.component.js.map