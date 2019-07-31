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
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var detail_component_1 = require("../../class/detail.component");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var FiscalPeriodFormComponent = /** @class */ (function (_super) {
    __extends(FiscalPeriodFormComponent, _super);
    //Events
    function FiscalPeriodFormComponent(fiscalPeriodService, toastrService, translate, renderer, metadata) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, environment_1.Entities.FiscalPeriod, environment_1.Metadatas.FiscalPeriod) || this;
        _this.fiscalPeriodService = fiscalPeriodService;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.active = false;
        _this.isNew = false;
        _this.cancel = new core_1.EventEmitter();
        _this.save = new core_1.EventEmitter();
        return _this;
    }
    Object.defineProperty(FiscalPeriodFormComponent.prototype, "model", {
        set: function (fiscalPeriod) {
            debugger;
            if (fiscalPeriod && this.isNew) {
                fiscalPeriod.startDate = this.getStartDate();
                fiscalPeriod.endDate = this.getEndDate();
                new Date();
            }
            this.editForm.reset(fiscalPeriod);
            this.active = fiscalPeriod !== undefined || this.isNew;
            if (fiscalPeriod != undefined) {
                this.fiscalPeriod_Id = fiscalPeriod.id;
            }
        },
        enumerable: true,
        configurable: true
    });
    //create properties
    FiscalPeriodFormComponent.prototype.getStartDate = function () {
        if (this.CurrentLanguage == "fa") {
            return new Date(new Date().getFullYear(), 2, 21, 0, 0, 0);
        }
        else {
            return new Date(new Date().getFullYear(), 0, 1, 0, 0, 0);
        }
    };
    FiscalPeriodFormComponent.prototype.getEndDate = function () {
        if (this.CurrentLanguage == "fa") {
            return new Date(new Date().getFullYear() + 1, 2, 19, 0, 0, 0);
        }
        else {
            return new Date(new Date().getFullYear(), 11, 31, 0, 0, 0);
        }
    };
    //Events
    FiscalPeriodFormComponent.prototype.onSave = function (e) {
        e.preventDefault();
        if (this.editForm.valid) {
            this.save.emit(this.editForm.value);
            this.active = true;
        }
    };
    FiscalPeriodFormComponent.prototype.onCancel = function (e) {
        e.preventDefault();
        this.closeForm();
    };
    FiscalPeriodFormComponent.prototype.closeForm = function () {
        this.isNew = false;
        this.active = false;
        this.cancel.emit();
    };
    FiscalPeriodFormComponent.prototype.escPress = function () {
        this.closeForm();
    };
    FiscalPeriodFormComponent.prototype.onDeleteData = function () {
        alert("Data deleted.");
    };
    __decorate([
        core_1.Input()
    ], FiscalPeriodFormComponent.prototype, "isNew", void 0);
    __decorate([
        core_1.Input()
    ], FiscalPeriodFormComponent.prototype, "errorMessage", void 0);
    __decorate([
        core_1.Input()
    ], FiscalPeriodFormComponent.prototype, "model", null);
    __decorate([
        core_1.Output()
    ], FiscalPeriodFormComponent.prototype, "cancel", void 0);
    __decorate([
        core_1.Output()
    ], FiscalPeriodFormComponent.prototype, "save", void 0);
    FiscalPeriodFormComponent = __decorate([
        core_1.Component({
            selector: 'fiscalPeriod-form-component',
            styles: [
                "input[type=text],textarea { width: 100%; }"
            ],
            templateUrl: './fiscalPeriod-form.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], FiscalPeriodFormComponent);
    return FiscalPeriodFormComponent;
}(detail_component_1.DetailComponent));
exports.FiscalPeriodFormComponent = FiscalPeriodFormComponent;
//# sourceMappingURL=fiscalPeriod-form.component.js.map