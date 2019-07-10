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
var accountApi_1 = require("./api/accountApi");
var AccountInfo = /** @class */ (function () {
    function AccountInfo() {
        this.currencyId = 1;
        this.isActive = true;
        this.isCurrencyAdjustable = true;
        this.turnoverMode = -1;
        this.id = 0;
        this.fullCode = "";
        this.level = 0;
        //constructor(public id: number = 0, public code: string = "", public name: string = "", public groupId?: number,
        //      public fiscalPeriodId: number = 0, public description?: string = "", public branchScope: number = 0,
        //      public branchId: number = 0, public level: number = 0, public fullCode: string = "",
        //      public childCount: number = 0, public parentId: number = 0, public companyId: number = 0) { }
    }
    return AccountInfo;
}());
exports.AccountInfo = AccountInfo;
var AccountService = /** @class */ (function (_super) {
    __extends(AccountService, _super);
    function AccountService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    AccountService.prototype.getAccountById = function (id) {
        var url = source_1.String.Format(accountApi_1.AccountApi.Account, id);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    AccountService = __decorate([
        core_1.Injectable()
    ], AccountService);
    return AccountService;
}(base_service_1.BaseService));
exports.AccountService = AccountService;
//# sourceMappingURL=account.service.js.map