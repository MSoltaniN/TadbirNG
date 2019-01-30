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
var index_1 = require("../../service/index");
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var detail_component_1 = require("../../class/detail.component");
var documentStatusValue_1 = require("../../enum/documentStatusValue");
var source_1 = require("../../class/source");
var index_2 = require("../../service/api/index");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var VoucherFormComponent = /** @class */ (function (_super) {
    __extends(VoucherFormComponent, _super);
    //Events
    function VoucherFormComponent(voucherService, fiscalPeriodService, toastrService, translate, renderer, metadata) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, environment_1.Entities.Voucher, environment_1.Metadatas.Voucher) || this;
        _this.voucherService = voucherService;
        _this.fiscalPeriodService = fiscalPeriodService;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.isNew = false;
        _this.cancel = new core_1.EventEmitter();
        _this.save = new core_1.EventEmitter();
        _this.changeMode = new core_1.EventEmitter();
        _this.setFocus = new core_1.EventEmitter();
        return _this;
    }
    //create properties
    //Events
    VoucherFormComponent.prototype.onSave = function (e) {
        e.preventDefault();
        if (this.editForm.valid) {
            var model = this.editForm.value;
            if (this.editModel && this.editModel.id > 0) {
                model.branchId = this.editModel.branchId;
                model.fiscalPeriodId = this.editModel.fiscalPeriodId;
                model.statusId = this.editModel.statusId;
            }
            else {
                model.branchId = this.BranchId;
                model.fiscalPeriodId = this.FiscalPeriodId;
            }
            this.save.emit(model);
        }
    };
    VoucherFormComponent.prototype.onCancel = function (e) {
        e.preventDefault();
        this.closeForm();
    };
    VoucherFormComponent.prototype.closeForm = function () {
        this.isNew = false;
        this.cancel.emit();
    };
    VoucherFormComponent.prototype.onDeleteData = function () {
        alert("Data deleted.");
    };
    VoucherFormComponent.prototype.escPress = function () {
        this.closeForm();
    };
    VoucherFormComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.editForm.reset();
        this.editForm.reset(this.editModel);
        if (this.editModel != undefined) {
            this.voucher_Id = this.editModel.id;
            this.documentStatus = this.editModel.statusId;
        }
        setTimeout(function () {
            _this.editForm.reset(_this.editModel);
        });
    };
    VoucherFormComponent.prototype.checkHandler = function (voucherId, statusId) {
        var _this = this;
        if (statusId == documentStatusValue_1.DocumentStatusValue.Draft) {
            //check
            this.voucherService.changeVoucherStatus(source_1.String.Format(index_2.VoucherApi.CheckVoucher, voucherId)).subscribe(function (res) {
                _this.editModel.statusId = documentStatusValue_1.DocumentStatusValue.NormalCheck;
                _this.showMessage(_this.updateMsg, environment_1.MessageType.Succes);
                _this.documentStatus = documentStatusValue_1.DocumentStatusValue.NormalCheck;
            }, (function (error) {
                var message = error.message ? error.message : error;
                _this.showMessage(message, environment_1.MessageType.Warning);
            }));
        }
        else {
            //uncheck
            this.voucherService.changeVoucherStatus(source_1.String.Format(index_2.VoucherApi.UncheckVoucher, voucherId)).subscribe(function (res) {
                _this.editModel.statusId = documentStatusValue_1.DocumentStatusValue.Draft;
                _this.showMessage(_this.updateMsg, environment_1.MessageType.Succes);
                _this.documentStatus = documentStatusValue_1.DocumentStatusValue.Draft;
            }, (function (error) {
                var message = error.message ? error.message : error;
                _this.showMessage(message, environment_1.MessageType.Warning);
            }));
        }
    };
    VoucherFormComponent.prototype.addNew = function () {
        this.changeMode.emit(true);
        this.isNew = true;
        this.editModel = new index_1.VoucherInfo();
        this.editModel.date = new Date();
        this.editForm.reset(this.editModel);
    };
    VoucherFormComponent.prototype.focusHandler = function (e) {
        this.setFocus.emit();
    };
    __decorate([
        core_1.Input()
    ], VoucherFormComponent.prototype, "isNew", void 0);
    __decorate([
        core_1.Input()
    ], VoucherFormComponent.prototype, "errorMessage", void 0);
    __decorate([
        core_1.Input()
    ], VoucherFormComponent.prototype, "editModel", void 0);
    __decorate([
        core_1.Output()
    ], VoucherFormComponent.prototype, "cancel", void 0);
    __decorate([
        core_1.Output()
    ], VoucherFormComponent.prototype, "save", void 0);
    __decorate([
        core_1.Output()
    ], VoucherFormComponent.prototype, "changeMode", void 0);
    __decorate([
        core_1.Output()
    ], VoucherFormComponent.prototype, "setFocus", void 0);
    VoucherFormComponent = __decorate([
        core_1.Component({
            //changeDetection: ChangeDetectionStrategy.OnPush,
            selector: 'voucher-form-component',
            styles: ["\n    input[type=text],textarea { width: 100%; } /deep/ .new-dialog .k-dialog {width: 450px !important; min-width: 250px !important;}\n    /deep/ .edit-dialog .k-dialog {width: 100% !important; min-width: 250px !important; height:100%}\n    /deep/ .edit-dialog .k-window-titlebar{ padding: 5px 16px !important;}\n    /deep/ .edit-dialog .edit-form-body { background: #f6f6f6; border: solid 1px #989898; border-radius: 4px;}\n    .form-toolbar{border-bottom: solid 1px #e3e3e3; margin-bottom: 10px; padding: 10px;} .form-toolbar button{padding: 5px 6px;}\n    /deep/ .voucher-dialog .k-window .k-overlay { opacity: .6 !important; }\n   /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}\"\n  "],
            templateUrl: './voucher-form.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], VoucherFormComponent);
    return VoucherFormComponent;
}(detail_component_1.DetailComponent));
exports.VoucherFormComponent = VoucherFormComponent;
//# sourceMappingURL=voucher-form.component.js.map