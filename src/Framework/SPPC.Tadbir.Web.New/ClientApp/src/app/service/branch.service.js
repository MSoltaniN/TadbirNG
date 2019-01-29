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
var source_1 = require("../class/source");
var index_1 = require("./api/index");
var BranchInfo = /** @class */ (function () {
    function BranchInfo() {
        this.companyId = 0;
        this.childCount = 0;
        this.id = 0;
        this.level = 0;
    }
    return BranchInfo;
}());
exports.BranchInfo = BranchInfo;
var BranchService = /** @class */ (function (_super) {
    __extends(BranchService, _super);
    function BranchService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    BranchService.prototype.getBranchRoles = function (branchId) {
        var url = source_1.String.Format(index_1.BranchApi.BranchRoles, branchId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    BranchService.prototype.modifiedBranchRoles = function (branchRoles) {
        var body = JSON.stringify(branchRoles);
        var options = { headers: this.httpHeaders };
        var url = source_1.String.Format(index_1.BranchApi.BranchRoles, branchRoles.id);
        return this.http.put(url, body, options)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    BranchService = __decorate([
        core_1.Injectable()
    ], BranchService);
    return BranchService;
}(base_service_1.BaseService));
exports.BranchService = BranchService;
//# sourceMappingURL=branch.service.js.map