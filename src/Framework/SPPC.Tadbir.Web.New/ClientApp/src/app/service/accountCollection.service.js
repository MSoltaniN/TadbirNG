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
var AccountCollectionCategoryInfo = /** @class */ (function () {
    function AccountCollectionCategoryInfo() {
    }
    return AccountCollectionCategoryInfo;
}());
exports.AccountCollectionCategoryInfo = AccountCollectionCategoryInfo;
var AccountCollectionInfo = /** @class */ (function () {
    function AccountCollectionInfo() {
    }
    return AccountCollectionInfo;
}());
exports.AccountCollectionInfo = AccountCollectionInfo;
var AccountCollectionAccountInfo = /** @class */ (function () {
    function AccountCollectionAccountInfo() {
        this.id = 0;
    }
    return AccountCollectionAccountInfo;
}());
exports.AccountCollectionAccountInfo = AccountCollectionAccountInfo;
var AccountCollectionService = /** @class */ (function (_super) {
    __extends(AccountCollectionService, _super);
    function AccountCollectionService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    AccountCollectionService = __decorate([
        core_1.Injectable()
    ], AccountCollectionService);
    return AccountCollectionService;
}(base_service_1.BaseService));
exports.AccountCollectionService = AccountCollectionService;
//# sourceMappingURL=accountCollection.service.js.map