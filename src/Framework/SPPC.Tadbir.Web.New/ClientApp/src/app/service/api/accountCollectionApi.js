"use strict";
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.492
//     Template Version: 1.0
//     Generation Date: 01/19/2019 04:06:16 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../../environments/environment");
var AccountCollectionApi = /** @class */ (function () {
    function AccountCollectionApi() {
    }
    // acccollections
    AccountCollectionApi.AccountCollections = environment_1.environment.BaseUrl + "/acccollections";
    // acccollections/collection/{collectionId:min(1)}
    AccountCollectionApi.AccountCollectionAccount = environment_1.environment.BaseUrl + "/acccollections/collection/{0}";
    return AccountCollectionApi;
}());
exports.AccountCollectionApi = AccountCollectionApi;
//# sourceMappingURL=accountCollectionApi.js.map