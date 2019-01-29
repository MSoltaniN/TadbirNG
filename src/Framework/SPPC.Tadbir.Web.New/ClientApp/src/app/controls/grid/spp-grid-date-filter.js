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
var kendo_angular_grid_1 = require("@progress/kendo-angular-grid");
var sppc_grid_datepicker_1 = require("../datepicker/sppc-grid-datepicker");
var SppcGridDateFilter = /** @class */ (function (_super) {
    __extends(SppcGridDateFilter, _super);
    function SppcGridDateFilter(filterService, con, el) {
        var _this = this;
        var i = 0;
        _this = _super.call(this, filterService) || this;
        return _this;
    }
    SppcGridDateFilter.prototype.ngOnInit = function () {
    };
    __decorate([
        core_1.Input()
    ], SppcGridDateFilter.prototype, "filter", void 0);
    __decorate([
        core_1.Input()
    ], SppcGridDateFilter.prototype, "column", void 0);
    SppcGridDateFilter = __decorate([
        core_1.Component({
            selector: 'sppc-grid-date-filter',
            template: "<kendo-grid-string-filter-cell testdr [column]=\"column\" [filter]=\"filter\">\n    <kendo-filter-eq-operator></kendo-filter-eq-operator>\n    <kendo-filter-neq-operator></kendo-filter-neq-operator>    \n</kendo-grid-string-filter-cell>"
        })
    ], SppcGridDateFilter);
    return SppcGridDateFilter;
}(kendo_angular_grid_1.BaseFilterCellComponent));
exports.SppcGridDateFilter = SppcGridDateFilter;
var FilterDatePickerDirective = /** @class */ (function () {
    function FilterDatePickerDirective(con, el, renderer, componentFactoryResolver, appRef, injector) {
        this.con = con;
        this.el = el;
        this.renderer = renderer;
        this.componentFactoryResolver = componentFactoryResolver;
        this.appRef = appRef;
        this.injector = injector;
        this.conn = con;
        this.elrf = el;
        this.factoryResolver = componentFactoryResolver;
    }
    FilterDatePickerDirective.prototype.ngOnInit = function () {
        var _this = this;
        var id = Guid.newGuid();
        this.hiddenId = id;
        this.elrf.nativeElement.childNodes[1].childNodes[2].style = 'visibility:hidden;width: 0px;margin:0;padding:0;';
        this.elrf.nativeElement.childNodes[1].childNodes[2].setAttribute('id', id);
        this.elrf.nativeElement.childNodes[1].childNodes[5].childNodes[3].setAttribute('id', 'btnClear_' + id);
        var mainElement = document.getElementById('btnClear_' + id);
        if (mainElement)
            mainElement.addEventListener('click', this.clearFilterClick.bind(this));
        setTimeout(function () {
            _this.appendComponent(sppc_grid_datepicker_1.SppcGridDatepicker, _this.elrf.nativeElement.childNodes[1].childNodes[2], _this.elrf.nativeElement.childNodes[1]);
        }, 1);
    };
    FilterDatePickerDirective.prototype.clearFilterClick = function (event) {
        var hiddenElement = document.getElementById('date_' + this.hiddenId);
        hiddenElement.value = '';
    };
    FilterDatePickerDirective.prototype.ngAfterContentInit = function () {
    };
    FilterDatePickerDirective.prototype.appendComponent = function (component, before, host) {
        var componentRef = this.componentFactoryResolver
            .resolveComponentFactory(component)
            .create(this.injector);
        componentRef.instance.destinationElementId = this.hiddenId;
        componentRef.instance.mode = this.value;
        this.appRef.attachView(componentRef.hostView);
        var domElem = componentRef.hostView
            .rootNodes[0];
        var input = (domElem.childNodes[0].childNodes[1].childNodes[1].childNodes[1]);
        input.id = "date_" + this.hiddenId;
        this.renderer.insertBefore(host, domElem, before);
    };
    FilterDatePickerDirective.prototype.onChange = function (event) {
        var elHidden = document.getElementById(this.hiddenId);
        if (elHidden)
            elHidden.nodeValue = event.value;
    };
    __decorate([
        core_1.Input('FilterDatePickerDirective')
    ], FilterDatePickerDirective.prototype, "value", void 0);
    FilterDatePickerDirective = __decorate([
        core_1.Directive({
            selector: '[FilterDatePickerDirective]'
        })
    ], FilterDatePickerDirective);
    return FilterDatePickerDirective;
}());
exports.FilterDatePickerDirective = FilterDatePickerDirective;
var Guid = /** @class */ (function () {
    function Guid() {
    }
    Guid.newGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };
    return Guid;
}());
//# sourceMappingURL=spp-grid-date-filter.js.map