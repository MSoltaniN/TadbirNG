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
require("rxjs/Rx");
var source_1 = require("../../class/source");
var base_service_1 = require("../../class/base.service");
var index_1 = require("../api/index");
var MetaDataService = /** @class */ (function (_super) {
    __extends(MetaDataService, _super);
    function MetaDataService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    /**
     * return metadata from database for each entity
     * @param entityName is name of entity like 'account' , 'transaction' , ...
     */
    MetaDataService.prototype.getMetaData = function (entityName) {
        var header = this.httpHeaders;
        header = header.delete('Content-Type');
        header = header.delete('X-Tadbir-AuthTicket');
        header = header.append('Content-Type', 'application/json; charset=utf-8');
        header = header.append('X-Tadbir-AuthTicket', this.Ticket);
        //headers.append('X-Tadbir-AuthTicket', this.Ticket);
        var options = { headers: header };
        var url = source_1.String.Format(index_1.MetadataApi.EntityMetadata, entityName);
        return this.http.get(url, options)
            .map(function (response) { return response; });
        //var result = null;
        //this.http.get(url, options)
        //  .map(response => result = (<Response>response).json());
        //return result;
    };
    MetaDataService.prototype.getMetaDataById = function (entityId) {
        var header = this.httpHeaders;
        header = header.delete('Content-Type');
        header = header.delete('X-Tadbir-AuthTicket');
        header = header.append('Content-Type', 'application/json; charset=utf-8');
        header = header.append('X-Tadbir-AuthTicket', this.Ticket);
        //headers.append('X-Tadbir-AuthTicket', this.Ticket);
        var options = { headers: header };
        var url = source_1.String.Format(index_1.MetadataApi.EntityMetadataById, entityId);
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    MetaDataService = __decorate([
        core_1.Injectable()
    ], MetaDataService);
    return MetaDataService;
}(base_service_1.BaseService));
exports.MetaDataService = MetaDataService;
//# sourceMappingURL=metadata.service.js.map