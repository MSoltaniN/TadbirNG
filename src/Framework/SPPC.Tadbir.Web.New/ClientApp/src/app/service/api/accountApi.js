"use strict";
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.503
//     Template Version: 1.0
//     Generation Date: 1/30/2019 1:02:31 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../../environments/environment");
var AccountApi = /** @class */ (function () {
    function AccountApi() {
    }
    // accounts
    AccountApi.EnvironmentAccounts = environment_1.environment.BaseUrl + "/accounts";
    // accounts/lookup
    AccountApi.EnvironmentAccountsLookup = environment_1.environment.BaseUrl + "/accounts/lookup";
    // accounts/ledger
    AccountApi.EnvironmentAccountsLedger = environment_1.environment.BaseUrl + "/accounts/ledger";
    // accounts/{accountId:min(1)}
    AccountApi.Account = environment_1.environment.BaseUrl + "/accounts/{0}";
    // accounts/{accountId:min(1)}/children
    AccountApi.AccountChildren = environment_1.environment.BaseUrl + "/accounts/{0}/children";
    // accounts/metadata
    AccountApi.AccountMetadata = environment_1.environment.BaseUrl + "/accounts/metadata";
    // accounts/fullcode/{parentId}
    AccountApi.AccountFullCode = environment_1.environment.BaseUrl + "/accounts/fullcode/{0}";
    return AccountApi;
}());
exports.AccountApi = AccountApi;
//# sourceMappingURL=accountApi.js.map