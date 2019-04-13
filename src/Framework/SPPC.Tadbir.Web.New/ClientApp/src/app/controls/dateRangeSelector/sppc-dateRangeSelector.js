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
var enviroment_component_1 = require("../../class/enviroment.component");
var SppcDateRangeSelector = /** @class */ (function (_super) {
    __extends(SppcDateRangeSelector, _super);
    function SppcDateRangeSelector(filterService, formBuilder) {
        var _this = _super.call(this, filterService) || this;
        _this.formBuilder = formBuilder;
        _this.rtl = true;
        _this.isDisplayFromDate = true;
        _this.isDisplayToDate = true;
        _this.valueChange = new core_1.EventEmitter();
        return _this;
    }
    SppcDateRangeSelector.prototype.ngOnInit = function () {
        var _this = this;
        var environment = new enviroment_component_1.EnviromentComponent();
        this.displayFromDate = environment.getDateConfig("start");
        this.displayToDate = environment.getDateConfig("end");
        //this.minDate = this.displayFromDate;
        //this.maxDate = this.displayToDate;
        var lang = "fa";
        if (localStorage.getItem('lang') != null) {
            var item;
            item = localStorage.getItem('lang');
            if (item)
                lang = item;
        }
        else if (sessionStorage.getItem('lang') != null) {
            var item;
            item = sessionStorage.getItem('lang');
            if (item)
                lang = item;
        }
        if (lang == "fa")
            this.rtl = true;
        else
            this.rtl = false;
        this.myForm = this.formBuilder.group({
            fromDate: '',
            toDate: ''
        });
        this.setFilter(this.displayFromDate, this.displayToDate);
        this.myForm.valueChanges.subscribe(function (val) {
            _this.valueChange.emit({ fromDate: val.fromDate, toDate: val.toDate });
            _this.setFilter(val.fromDate, val.toDate);
        });
    };
    SppcDateRangeSelector.prototype.setFilter = function (fDate, tDate) {
        var filters = [];
        if (fDate != undefined && fDate != "") {
            filters.push({
                field: this.Field,
                operator: "gte",
                value: fDate
            });
        }
        if (tDate != undefined && tDate != "") {
            filters.push({
                field: this.Field,
                operator: "lte",
                value: tDate
            });
        }
        this.filterService.filter({
            logic: "and",
            filters: filters
        });
    };
    __decorate([
        core_1.Input()
    ], SppcDateRangeSelector.prototype, "Field", void 0);
    __decorate([
        core_1.Input()
    ], SppcDateRangeSelector.prototype, "minDate", void 0);
    __decorate([
        core_1.Input()
    ], SppcDateRangeSelector.prototype, "maxDate", void 0);
    __decorate([
        core_1.Input()
    ], SppcDateRangeSelector.prototype, "isDisplayFromDate", void 0);
    __decorate([
        core_1.Input()
    ], SppcDateRangeSelector.prototype, "isDisplayToDate", void 0);
    __decorate([
        core_1.Output()
    ], SppcDateRangeSelector.prototype, "valueChange", void 0);
    SppcDateRangeSelector = __decorate([
        core_1.Component({
            selector: 'sppc-dateRangeSelector',
            templateUrl: './sppc-dateRangeSelector.html',
            //template: ``,
            styles: ["\n#drs-content{\n    margin-bottom: 10px;}\n.float-right{float:right;}\n"]
        })
    ], SppcDateRangeSelector);
    return SppcDateRangeSelector;
}(kendo_angular_grid_1.BaseFilterCellComponent));
exports.SppcDateRangeSelector = SppcDateRangeSelector;
//# sourceMappingURL=sppc-dateRangeSelector.js.map