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
var forms_1 = require("@angular/forms");
var environment_1 = require("../../../environments/environment");
var detail_component_1 = require("../../class/detail.component");
var VoucherLineFormComponent = /** @class */ (function (_super) {
    __extends(VoucherLineFormComponent, _super);
    function VoucherLineFormComponent(voucherLineService, accountService, toastrService, translate, lookupService, fullAccountService, renderer, metadata) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, environment_1.Entities.VoucherLine, environment_1.Metadatas.VoucherArticles) || this;
        _this.voucherLineService = voucherLineService;
        _this.accountService = accountService;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.lookupService = lookupService;
        _this.fullAccountService = fullAccountService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        //TODO: create form with metadata
        _this.editForm1 = new forms_1.FormGroup({
            id: new forms_1.FormControl(),
            voucherId: new forms_1.FormControl(),
            currencyId: new forms_1.FormControl("", forms_1.Validators.required),
            debit: new forms_1.FormControl(),
            credit: new forms_1.FormControl(),
            description: new forms_1.FormControl("", forms_1.Validators.maxLength(512)),
            fullAccount: new forms_1.FormGroup({
                account: new forms_1.FormGroup({
                    id: new forms_1.FormControl("", forms_1.Validators.required),
                    name: new forms_1.FormControl(),
                    fullCode: new forms_1.FormControl()
                }),
                detailAccount: new forms_1.FormGroup({
                    id: new forms_1.FormControl(),
                    name: new forms_1.FormControl(),
                    fullCode: new forms_1.FormControl()
                }),
                costCenter: new forms_1.FormGroup({
                    id: new forms_1.FormControl(),
                    name: new forms_1.FormControl(),
                    fullCode: new forms_1.FormControl()
                }),
                project: new forms_1.FormGroup({
                    id: new forms_1.FormControl(),
                    name: new forms_1.FormControl(),
                    fullCode: new forms_1.FormControl()
                })
            })
        });
        _this.isNew = false;
        _this.isNewBalance = false;
        _this.balance = 0;
        _this.cancel = new core_1.EventEmitter();
        _this.save = new core_1.EventEmitter();
        _this.setFocus = new core_1.EventEmitter();
        return _this;
    }
    //create properties
    //Events
    VoucherLineFormComponent.prototype.onSave = function (e, isOpen) {
        e.preventDefault();
        if (this.editForm1.valid) {
            var model = this.editForm1.value;
            if (!model.debit)
                model.debit = 0;
            if (!model.credit)
                model.credit = 0;
            this.save.emit({ model: model, isOpen: isOpen });
        }
    };
    VoucherLineFormComponent.prototype.onCancel = function (e) {
        e.preventDefault();
        this.cancel.emit();
    };
    VoucherLineFormComponent.prototype.escPress = function () {
        this.cancel.emit();
    };
    //Events
    VoucherLineFormComponent.prototype.ngOnInit = function () {
        this.editForm1.reset(this.model);
        this.GetCurrencies();
        if (this.isNewBalance)
            if (this.balance > 0)
                this.editForm1.patchValue({ 'credit': Math.abs(this.balance) });
            else if (this.balance < 0)
                this.editForm1.patchValue({ 'debit': Math.abs(this.balance) });
    };
    VoucherLineFormComponent.prototype.GetCurrencies = function () {
        var _this = this;
        this.lookupService.GetCurrenciesLookup().subscribe(function (res) {
            _this.currenciesRows = res;
            if (_this.model != undefined && _this.model.currencyId != undefined) {
                _this.selectedCurrencyValue = _this.model.currencyId.toString();
            }
        });
    };
    VoucherLineFormComponent.prototype.focusHandler = function (e) {
        this.setFocus.emit();
    };
    __decorate([
        core_1.Input()
    ], VoucherLineFormComponent.prototype, "isNew", void 0);
    __decorate([
        core_1.Input()
    ], VoucherLineFormComponent.prototype, "errorMessage", void 0);
    __decorate([
        core_1.Input()
    ], VoucherLineFormComponent.prototype, "isNewBalance", void 0);
    __decorate([
        core_1.Input()
    ], VoucherLineFormComponent.prototype, "balance", void 0);
    __decorate([
        core_1.Input()
    ], VoucherLineFormComponent.prototype, "model", void 0);
    __decorate([
        core_1.Output()
    ], VoucherLineFormComponent.prototype, "cancel", void 0);
    __decorate([
        core_1.Output()
    ], VoucherLineFormComponent.prototype, "save", void 0);
    __decorate([
        core_1.Output()
    ], VoucherLineFormComponent.prototype, "setFocus", void 0);
    VoucherLineFormComponent = __decorate([
        core_1.Component({
            selector: 'voucherLine-form-component',
            styles: ["\n    input[type=text],textarea { width: 100%; } /deep/ kendo-numerictextbox{ width:100% !important; }\n    /deep/ .dialog-style .k-dialog { width:250px } @media (max-width: 450px) { /deep/ .dialog-style .k-dialog { width:100% } }\n"],
            templateUrl: './voucherLine-form.component.html'
        })
    ], VoucherLineFormComponent);
    return VoucherLineFormComponent;
}(detail_component_1.DetailComponent));
exports.VoucherLineFormComponent = VoucherLineFormComponent;
//# sourceMappingURL=voucherLine-form.component.js.map