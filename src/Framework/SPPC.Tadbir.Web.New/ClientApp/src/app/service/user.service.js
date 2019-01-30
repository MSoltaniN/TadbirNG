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
var UserInfo = /** @class */ (function () {
    function UserInfo() {
        this.id = 0;
        this.isEnabled = false;
    }
    return UserInfo;
}());
exports.UserInfo = UserInfo;
var UserProfileInfo = /** @class */ (function () {
    function UserProfileInfo() {
    }
    return UserProfileInfo;
}());
exports.UserProfileInfo = UserProfileInfo;
var CommandInfo = /** @class */ (function () {
    function CommandInfo(id, title, routeUrl, iconName, hotKey, children, permissionId) {
        if (title === void 0) { title = ""; }
        if (iconName === void 0) { iconName = ""; }
        this.id = id;
        this.title = title;
        this.routeUrl = routeUrl;
        this.iconName = iconName;
        this.hotKey = hotKey;
        this.children = children;
    }
    return CommandInfo;
}());
exports.CommandInfo = CommandInfo;
var UserService = /** @class */ (function (_super) {
    __extends(UserService, _super);
    function UserService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    UserService.prototype.changePassword = function (userProfile) {
        var body = JSON.stringify(userProfile);
        var url = source_1.String.Format(index_1.UserApi.UserPassword, userProfile.userName);
        var options = { headers: this.httpHeaders };
        return this.http.put(url, body, options)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    UserService.prototype.getUserRoles = function (userId) {
        var url = source_1.String.Format(index_1.UserApi.UserRoles, userId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    UserService.prototype.modifiedUserRoles = function (userRoles) {
        var body = JSON.stringify(userRoles);
        var options = { headers: this.httpHeaders };
        var url = source_1.String.Format(index_1.UserApi.UserRoles, userRoles.id);
        return this.http.put(url, body, options)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    UserService.prototype.getCurrentUserCommands = function (ticket) {
        var url = index_1.UserApi.CurrentUserCommands;
        var header = this.httpHeaders;
        if (header) {
            header = header.delete('X-Tadbir-AuthTicket');
            header = header.delete('Accept-Language');
            header = header.append('X-Tadbir-AuthTicket', ticket);
            if (this.CurrentLanguage == "fa")
                header = header.append('Accept-Language', 'fa-IR,fa');
            if (this.CurrentLanguage == "en")
                header = header.append('Accept-Language', 'en-US,en');
        }
        var options = { headers: header };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    UserService.prototype.getDefaultUserCommands = function (ticket) {
        var url = index_1.UserApi.UserDefaultCommands;
        var header = this.httpHeaders;
        if (header) {
            header = header.delete('X-Tadbir-AuthTicket');
            header = header.delete('Accept-Language');
            header = header.append('X-Tadbir-AuthTicket', ticket);
            if (this.CurrentLanguage == "fa")
                header = header.append('Accept-Language', 'fa-IR,fa');
            if (this.CurrentLanguage == "en")
                header = header.append('Accept-Language', 'en-US,en');
        }
        var options = { headers: header };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    UserService = __decorate([
        core_1.Injectable()
    ], UserService);
    return UserService;
}(base_service_1.BaseService));
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map