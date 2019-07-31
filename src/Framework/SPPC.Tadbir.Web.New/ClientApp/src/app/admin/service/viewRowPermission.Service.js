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
var ItemInfo = /** @class */ (function () {
    function ItemInfo() {
        this.key = 0;
        this.value = '';
    }
    return ItemInfo;
}());
exports.ItemInfo = ItemInfo;
var RowPermissionsForRoleInfo = /** @class */ (function () {
    function RowPermissionsForRoleInfo() {
    }
    return RowPermissionsForRoleInfo;
}());
exports.RowPermissionsForRoleInfo = RowPermissionsForRoleInfo;
var ViewRowPermissionInfo = /** @class */ (function () {
    function ViewRowPermissionInfo() {
        this.items = [];
    }
    return ViewRowPermissionInfo;
}());
exports.ViewRowPermissionInfo = ViewRowPermissionInfo;
var ViewRowPermissionService = /** @class */ (function (_super) {
    __extends(ViewRowPermissionService, _super);
    function ViewRowPermissionService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    //public getRowList(apiUrl: string) {
    //    return this.http.get(apiUrl, this.options)
    //        .map(response => <any>(<Response>response).json());
    //}
    ViewRowPermissionService.prototype.getRowList = function (apiUrl, filter) {
        var intMaxValue = 2147483647;
        var gridPaging = { pageIndex: 1, pageSize: intMaxValue };
        var postItem = { Paging: gridPaging, filter: filter, sortColumns: null };
        var searchHeaders = this.httpHeaders;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders.append('X-Tadbir-GridOptions', base64Body);
        var options = { headers: searchHeaders };
        return this.http.get(apiUrl, options)
            .map(function (response) { return response; });
    };
    ViewRowPermissionService = __decorate([
        core_1.Injectable()
    ], ViewRowPermissionService);
    return ViewRowPermissionService;
}(base_service_1.BaseService));
exports.ViewRowPermissionService = ViewRowPermissionService;
//# sourceMappingURL=viewRowPermission.Service.js.map