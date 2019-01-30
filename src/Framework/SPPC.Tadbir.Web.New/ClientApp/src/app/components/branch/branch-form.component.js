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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
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
var BranchFormComponent = /** @class */ (function (_super) {
    __extends(BranchFormComponent, _super);
    //Events
    function BranchFormComponent(toastrService, translate, renderer, metadata, defaultComponent) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, environment_1.Entities.Branch, environment_1.Metadatas.Branch) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metadata = metadata;
        //create properties
        _this.active = false;
        _this.isNew = false;
        _this.errorMessage = '';
        _this.parentTitle = '';
        _this.parentValue = '';
        _this.cancel = new core_1.EventEmitter();
        _this.save = new core_1.EventEmitter();
        return _this;
    }
    Object.defineProperty(BranchFormComponent.prototype, "model", {
        set: function (branch) {
            this.editForm.reset(branch);
            this.active = branch !== undefined || this.isNew;
        },
        enumerable: true,
        configurable: true
    });
    //create properties
    //Events
    BranchFormComponent.prototype.onSave = function (e) {
        e.preventDefault();
        if (this.editForm.valid) {
            this.save.emit(this.editForm.value);
            this.active = true;
        }
    };
    BranchFormComponent.prototype.onCancel = function (e) {
        e.preventDefault();
        this.closeForm();
    };
    BranchFormComponent.prototype.closeForm = function () {
        this.isNew = false;
        this.active = false;
        this.cancel.emit();
    };
    BranchFormComponent.prototype.escPress = function () {
        this.closeForm();
    };
    __decorate([
        core_1.Input()
    ], BranchFormComponent.prototype, "isNew", void 0);
    __decorate([
        core_1.Input()
    ], BranchFormComponent.prototype, "errorMessage", void 0);
    __decorate([
        core_1.Input()
    ], BranchFormComponent.prototype, "parentTitle", void 0);
    __decorate([
        core_1.Input()
    ], BranchFormComponent.prototype, "parentValue", void 0);
    __decorate([
        core_1.Input()
    ], BranchFormComponent.prototype, "model", null);
    __decorate([
        core_1.Output()
    ], BranchFormComponent.prototype, "cancel", void 0);
    __decorate([
        core_1.Output()
    ], BranchFormComponent.prototype, "save", void 0);
    BranchFormComponent = __decorate([
        core_1.Component({
            selector: 'branch-form-component',
            styles: [
                "input[type=text],textarea { width: 100%; }"
            ],
            templateUrl: './branch-form.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        }),
        __param(4, core_1.Host())
    ], BranchFormComponent);
    return BranchFormComponent;
}(detail_component_1.DetailComponent));
exports.BranchFormComponent = BranchFormComponent;
//# sourceMappingURL=branch-form.component.js.map