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
Object.defineProperty(exports, "__esModule", { value: true });
var enviroment_component_1 = require("./enviroment.component");
var grid_orderby_1 = require("./grid.orderby");
var Observable_1 = require("rxjs/Observable");
var http_1 = require("@angular/common/http");
var ReportBaseService = /** @class */ (function (_super) {
    __extends(ReportBaseService, _super);
    function ReportBaseService(http) {
        var _this = _super.call(this) || this;
        _this.http = http;
        return _this;
    }
    Object.defineProperty(ReportBaseService.prototype, "httpHeaders", {
        get: function () {
            var header = new http_1.HttpHeaders();
            header = header.append('Content-Type', 'application/json; charset=utf-8');
            header = header.append('X-Tadbir-AuthTicket', this.Ticket);
            if (this.CurrentLanguage == "fa")
                header = header.append('Accept-Language', 'fa-IR,fa');
            if (this.CurrentLanguage == "en")
                header = header.append('Accept-Language', 'en-US,en');
            return header;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ReportBaseService.prototype, "option", {
        get: function () {
            return { headers: this.httpHeaders };
        },
        enumerable: true,
        configurable: true
    });
    /**
     * لیست رکوردها بر اساس فیلتر و مرتب سازی
     * @param apiUrl آدرس‌ کامل api
     * @param orderby مرتب سازی
     * @param filters فیلتر
     */
    ReportBaseService.prototype.getAll = function (apiUrl, orderby, filter) {
        var sort = new Array();
        if (orderby && orderby.length > 0) {
            for (var _i = 0, orderby_1 = orderby; _i < orderby_1.length; _i++) {
                var item = orderby_1[_i];
                sort.push(new grid_orderby_1.GridOrderBy(item.field, item.dir.toUpperCase()));
            }
        }
        var postItem = { filter: filter, sortColumns: sort };
        var searchHeaders = this.httpHeaders;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);
        return this.http.get(apiUrl, { headers: searchHeaders, observe: "response" })
            .map(function (response) { return response; });
    };
    /**
     *
     * @param error
     */
    ReportBaseService.prototype.handleError = function (error) {
        return Observable_1.Observable.throw(error.error);
    };
    return ReportBaseService;
}(enviroment_component_1.EnviromentComponent));
exports.ReportBaseService = ReportBaseService;
//# sourceMappingURL=report.base.service.js.map