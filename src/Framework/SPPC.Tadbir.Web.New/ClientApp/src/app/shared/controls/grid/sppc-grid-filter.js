"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SppcGridFilter = /** @class */ (function () {
    function SppcGridFilter() {
        this.isNumber = false;
        this.isString = false;
        this.isDate = false;
        this.isDateTime = false;
    }
    Object.defineProperty(SppcGridFilter.prototype, "metaData", {
        set: function (value) {
            if (value == undefined)
                return;
            switch (value.scriptType.toLowerCase()) {
                case "number":
                    this.isNumber = true;
                    break;
                case "string":
                    this.isString = true;
                    break;
                case "date":
                    this.isDate = true;
                    break;
                case "datetime":
                    this.isDateTime = true;
                    break;
                default:
                    break;
            }
        },
        enumerable: true,
        configurable: true
    });
    __decorate([
        core_1.Input()
    ], SppcGridFilter.prototype, "column", void 0);
    __decorate([
        core_1.Input()
    ], SppcGridFilter.prototype, "filter", void 0);
    __decorate([
        core_1.Input()
    ], SppcGridFilter.prototype, "isNumber", void 0);
    __decorate([
        core_1.Input()
    ], SppcGridFilter.prototype, "isString", void 0);
    __decorate([
        core_1.Input()
    ], SppcGridFilter.prototype, "isDate", void 0);
    __decorate([
        core_1.Input()
    ], SppcGridFilter.prototype, "isDateTime", void 0);
    __decorate([
        core_1.Input('metaData')
    ], SppcGridFilter.prototype, "metaData", null);
    SppcGridFilter = __decorate([
        core_1.Component({
            selector: 'sppc-grid-filter',
            templateUrl: './sppc-grid-filter.html'
        })
    ], SppcGridFilter);
    return SppcGridFilter;
}());
exports.SppcGridFilter = SppcGridFilter;
//# sourceMappingURL=sppc-grid-filter.js.map