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
var base_service_1 = require("../class/base.service");
var dashboardApi_1 = require("./api/dashboardApi");
var DashboardService = /** @class */ (function (_super) {
    __extends(DashboardService, _super);
    function DashboardService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    DashboardService.prototype.getDashboardInfo = function () {
        var url = dashboardApi_1.DashboardApi.SummariesUrl;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    DashboardService = __decorate([
        core_1.Injectable()
    ], DashboardService);
    return DashboardService;
}(base_service_1.BaseService));
exports.DashboardService = DashboardService;
//# sourceMappingURL=dashboard.service.js.map