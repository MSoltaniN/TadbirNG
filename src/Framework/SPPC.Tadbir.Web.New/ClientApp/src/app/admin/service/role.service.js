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
var index_1 = require("./api/index");
require("rxjs/Rx");
var source_1 = require("../class/source");
var base_service_1 = require("../class/base.service");
var RoleInfo = /** @class */ (function () {
    function RoleInfo() {
        this.permissions = [];
        this.id = 0;
    }
    return RoleInfo;
}());
exports.RoleInfo = RoleInfo;
var PermissionInfo = /** @class */ (function () {
    function PermissionInfo() {
    }
    return PermissionInfo;
}());
exports.PermissionInfo = PermissionInfo;
var RoleFullInfo = /** @class */ (function () {
    function RoleFullInfo() {
        this.id = 0;
    }
    return RoleFullInfo;
}());
exports.RoleFullInfo = RoleFullInfo;
var RelatedItemsInfo = /** @class */ (function () {
    function RelatedItemsInfo() {
    }
    return RelatedItemsInfo;
}());
exports.RelatedItemsInfo = RelatedItemsInfo;
var RoleDetailsInfo = /** @class */ (function () {
    function RoleDetailsInfo() {
    }
    return RoleDetailsInfo;
}());
exports.RoleDetailsInfo = RoleDetailsInfo;
var RoleService = /** @class */ (function (_super) {
    __extends(RoleService, _super);
    function RoleService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    RoleService.prototype.getNewRoleFull = function () {
        var url = index_1.RoleApi.NewRole;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    RoleService.prototype.getRoleFull = function (roleId) {
        var url = source_1.String.Format(index_1.RoleApi.Role, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    RoleService.prototype.getRoleUsers = function (roleId) {
        var url = source_1.String.Format(index_1.RoleApi.RoleUsers, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    RoleService.prototype.modifiedRoleUsers = function (roleUsers) {
        var body = JSON.stringify(roleUsers);
        var options = { headers: this.httpHeaders };
        var url = source_1.String.Format(index_1.RoleApi.RoleUsers, roleUsers.id);
        return this.http.put(url, body, options)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    RoleService.prototype.getRoleBranches = function (roleId) {
        var url = source_1.String.Format(index_1.RoleApi.RoleBranches, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    RoleService.prototype.modifiedRoleBranches = function (roleBranches) {
        var body = JSON.stringify(roleBranches);
        var options = { headers: this.httpHeaders };
        var url = source_1.String.Format(index_1.RoleApi.RoleBranches, roleBranches.id);
        return this.http.put(url, body, options)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    RoleService.prototype.getRoleFiscalPeriods = function (roleId) {
        var url = source_1.String.Format(index_1.RoleApi.RoleFiscalPeriods, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    RoleService.prototype.modifiedRoleFiscalPeriods = function (roleFPeriods) {
        var body = JSON.stringify(roleFPeriods);
        var options = { headers: this.httpHeaders };
        var url = source_1.String.Format(index_1.RoleApi.RoleFiscalPeriods, roleFPeriods.id);
        return this.http.put(url, body, options)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    RoleService.prototype.getRoleDetail = function (roleId) {
        var url = source_1.String.Format(index_1.RoleApi.RoleDetails, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    RoleService = __decorate([
        core_1.Injectable()
    ], RoleService);
    return RoleService;
}(base_service_1.BaseService));
exports.RoleService = RoleService;
//# sourceMappingURL=role.service.js.map