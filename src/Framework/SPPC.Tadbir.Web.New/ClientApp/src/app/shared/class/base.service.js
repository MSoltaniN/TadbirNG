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
var source_1 = require("../class/source");
var Observable_1 = require("rxjs/Observable");
var http_1 = require("@angular/common/http");
var BaseService = /** @class */ (function (_super) {
    __extends(BaseService, _super);
    //option: any;
    //httpHeaders = new HttpHeaders();
    function BaseService(http) {
        var _this = _super.call(this) || this;
        _this.http = http;
        return _this;
        //this.httpHeaders = this.httpHeaders.append('Content-Type', 'application/json; charset=utf-8');
        //this.httpHeaders = this.httpHeaders.append('X-Tadbir-AuthTicket', this.Ticket);
        //if (this.CurrentLanguage == "fa")
        //  this.httpHeaders = this.httpHeaders.append('Accept-Language', 'fa-IR,fa');
        //if (this.CurrentLanguage == "en")
        //  this.httpHeaders = this.httpHeaders.append('Accept-Language', 'en-US,en');
        //this.option = { headers: this.httpHeaders };
    }
    Object.defineProperty(BaseService.prototype, "httpHeaders", {
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
    Object.defineProperty(BaseService.prototype, "option", {
        get: function () {
            return { headers: this.httpHeaders };
        },
        enumerable: true,
        configurable: true
    });
    /**
     * لیست رکوردها بر اساس فیلتر و مرتب سازی
     * @param apiUrl آدرس‌ کامل api
     * @param start شماره شروع رکورد
     * @param count تعداد رکورد
     * @param orderby مرتب سازی
     * @param filters فیلتر
     */
    BaseService.prototype.getAll = function (apiUrl, start, count, orderby, filter) {
        var gridPaging = { pageIndex: start, pageSize: count };
        var sort = new Array();
        if (orderby && orderby.length > 0) {
            for (var _i = 0, orderby_1 = orderby; _i < orderby_1.length; _i++) {
                var item = orderby_1[_i];
                sort.push(new grid_orderby_1.GridOrderBy(item.field, item.dir.toUpperCase()));
            }
        }
        var postItem = { Paging: gridPaging, filter: filter, sortColumns: sort };
        var searchHeaders = this.httpHeaders;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);
        //var options = { headers: searchHeaders, observe: "response"};
        return this.http.get(apiUrl, { headers: searchHeaders, observe: "response" })
            .map(function (response) { return response; });
    };
    /**
     * لیستی از اطلاعات را از سرویس میگیرد
     * @param apiUrl آدرس کامل api
     */
    BaseService.prototype.getModels = function (apiUrl) {
        var options = { headers: this.httpHeaders };
        return this.http.get(apiUrl, options)
            .map(function (response) { return response; });
    };
    /**
     * گرفتن رکورد با استفاده از id رکورد
     * @param apiUrl آدرس کامل api
     * @param modelId شماره id رکورد
     */
    BaseService.prototype.getById = function (apiUrl) {
        var options = { headers: this.httpHeaders };
        return this.http.get(apiUrl, options)
            .map(function (response) { return response; });
    };
    /**
     * ایجاد رکورد جدید
     * @param apiUrl آدرس کامل api
     * @param model رکورد جدید برای افزودن
     */
    BaseService.prototype.insert = function (apiUrl, model) {
        var body = JSON.stringify(model);
        return this.http.post(apiUrl, body, this.option)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    /**
     * ویرایش رکورد
     * @param apiUrl آدرس کامل api
     * @param model رکورد برای ویرایش
     * @param modelId شماره id مدل
     */
    BaseService.prototype.edit = function (apiUrl, model) {
        var body = JSON.stringify(model);
        return this.http.put(apiUrl, body, this.option)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    /**
     * حذف رکورد جاری
     * @param apiUrl آدرس کامل api
     * @param modelId شماره id رکورد
     */
    BaseService.prototype.delete = function (apiUrl) {
        return this.http.delete(apiUrl, this.option)
            .map(function (response) { return response; })
            .catch(this.handleError);
    };
    /**
     * حذف گروهی
     * @param apiUrl آدرس api
     * @param models رکوردها
     */
    BaseService.prototype.groupDelete = function (apiUrl, models) {
        var modelId = '';
        var modelArray = Array();
        for (var i = 0; i < models.length; i++) {
            modelArray.push(models[i]);
        }
        var body = JSON.stringify({ paraph: '', items: modelArray });
        return this.http.put(apiUrl, body, this.option)
            .map(function (response) { return response; })
            .catch(this.handleError);
    };
    /**
     * تعداد رکورد بر اساس فیلتر و مرتب سازی
     * @param apiUrl آدرس کامل api
     * @param orderby مرتب سازی
     * @param filters فیلترها
     */
    BaseService.prototype.getCount = function (apiUrl, orderby, filters) {
        var headers = this.httpHeaders;
        var postItem = { filters: filters };
        var searchHeaders = this.httpHeaders;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);
        var options = { headers: searchHeaders };
        return this.http.get(apiUrl, options)
            .map(function (response) { return response; });
    };
    /**
     * تعداد کل رکوردها
     * @param apiUrl آدرس api
     */
    BaseService.prototype.getTotalCount = function (apiUrl) {
        var url = source_1.String.Format(apiUrl, this.FiscalPeriodId, this.BranchId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    /**
     *
     * @param error
     */
    BaseService.prototype.handleError = function (error) {
        return Observable_1.Observable.throw(error.error);
    };
    return BaseService;
}(enviroment_component_1.EnviromentComponent));
exports.BaseService = BaseService;
//# sourceMappingURL=base.service.js.map