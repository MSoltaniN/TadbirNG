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
var CompanyFormComponent = /** @class */ (function (_super) {
    __extends(CompanyFormComponent, _super);
    //Events
    function CompanyFormComponent(toastrService, translate, renderer, metadata) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, environment_1.Entities.Company, environment_1.Metadatas.Company) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metadata = metadata;
        //create properties
        _this.active = false;
        _this.isNew = false;
        _this.errorMessage = '';
        _this.cancel = new core_1.EventEmitter();
        _this.save = new core_1.EventEmitter();
        return _this;
    }
    Object.defineProperty(CompanyFormComponent.prototype, "model", {
        set: function (company) {
            this.editForm.reset(company);
            this.active = company !== undefined || this.isNew;
        },
        enumerable: true,
        configurable: true
    });
    //create properties
    //Events
    CompanyFormComponent.prototype.onSave = function (e) {
        e.preventDefault();
        if (this.editForm.valid) {
            this.save.emit(this.editForm.value);
            this.active = true;
        }
    };
    CompanyFormComponent.prototype.onCancel = function (e) {
        e.preventDefault();
        this.closeForm();
    };
    CompanyFormComponent.prototype.closeForm = function () {
        this.isNew = false;
        this.active = false;
        this.cancel.emit();
    };
    CompanyFormComponent.prototype.escPress = function () {
        this.closeForm();
    };
    __decorate([
        core_1.Input()
    ], CompanyFormComponent.prototype, "isNew", void 0);
    __decorate([
        core_1.Input()
    ], CompanyFormComponent.prototype, "errorMessage", void 0);
    __decorate([
        core_1.Input()
    ], CompanyFormComponent.prototype, "model", null);
    __decorate([
        core_1.Output()
    ], CompanyFormComponent.prototype, "cancel", void 0);
    __decorate([
        core_1.Output()
    ], CompanyFormComponent.prototype, "save", void 0);
    CompanyFormComponent = __decorate([
        core_1.Component({
            selector: 'company-form-component',
            styles: ["\n        input[type=text],textarea ,input[type=password]{ width: 100%; }\n    "],
            templateUrl: './company-form.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], CompanyFormComponent);
    return CompanyFormComponent;
}(detail_component_1.DetailComponent));
exports.CompanyFormComponent = CompanyFormComponent;
//# sourceMappingURL=company-form.component.js.map