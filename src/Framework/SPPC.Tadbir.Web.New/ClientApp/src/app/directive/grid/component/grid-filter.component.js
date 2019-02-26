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
var base_component_1 = require("../../../class/base.component");
var core_1 = require("@angular/core");
var environment_1 = require("../../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var GridFilterComponent = /** @class */ (function (_super) {
    __extends(GridFilterComponent, _super);
    function GridFilterComponent(toastrService, translate, settingService, grid, elRef) {
        var _this = _super.call(this, toastrService) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.settingService = settingService;
        _this.grid = grid;
        _this.elRef = elRef;
        _this.showClearFilter = 0;
        _this.reloadEvent = new core_1.EventEmitter();
        if (_this.grid.filter)
            _this.grid.filter.filters = [];
        return _this;
    }
    GridFilterComponent.prototype.ngOnDestroy = function () {
    };
    GridFilterComponent.prototype.ngOnInit = function () {
        debugger;
        if (this.CurrentLanguage == 'fa')
            this.rtl = true;
        else
            this.rtl = false;
        var self = this;
        //document.addEventListener('keydown', function (ev: KeyboardEvent) {
        //    if (ev.srcElement.hasAttribute('kendofilterinput') && ev.key == 'Enter') {
        //        self.parentComponent.reloadGrid();
        //    }
        //});
        document.addEventListener('keydown', function (ev) {
            if (ev.srcElement.hasAttribute('kendofilterinput') && ev.key == 'Enter') {
                self.parentComponent.reloadGrid();
            }
        });
    };
    GridFilterComponent.prototype.filterGridEmit = function () {
        this.reloadEvent.emit();
    };
    GridFilterComponent.prototype.filterGrid = function () {
        this.showClearFilter = this.grid.filter.filters.length;
        this.parentComponent.reloadGrid();
    };
    GridFilterComponent.prototype.removeFilterGrid = function () {
        this.grid.filter.filters = [];
        this.showClearFilter = this.grid.filter.filters.length;
        this.parentComponent.filterChange(this.grid.filter);
        this.parentComponent.reloadGrid();
    };
    __decorate([
        core_1.Input()
    ], GridFilterComponent.prototype, "showClearFilter", void 0);
    __decorate([
        core_1.Input()
    ], GridFilterComponent.prototype, "parentComponent", void 0);
    __decorate([
        core_1.Output()
    ], GridFilterComponent.prototype, "reloadEvent", void 0);
    GridFilterComponent = __decorate([
        core_1.Component({
            selector: 'grid-filter',
            templateUrl: './grid-filter.component.html',
            styleUrls: ['./grid-filter.component.css'],
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        }),
        __param(3, core_1.Host())
    ], GridFilterComponent);
    return GridFilterComponent;
}(base_component_1.BaseComponent));
exports.GridFilterComponent = GridFilterComponent;
//# sourceMappingURL=grid-filter.component.js.map