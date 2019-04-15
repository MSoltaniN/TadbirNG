"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SppcNumConfigPipe = /** @class */ (function () {
    function SppcNumConfigPipe(settingService) {
        this.settingService = settingService;
    }
    SppcNumConfigPipe.prototype.transform = function (value, withDecimal) {
        if (value == null || value == undefined)
            return "";
        var result = value;
        var config;
        var hasDecimal = true;
        if (withDecimal != null) {
            hasDecimal = withDecimal;
        }
        config = this.settingService.getNumberConfigBySettingId();
        if (config) {
            if (config.useSeparator) {
                result = this.setSeperator(value, config.separatorSymbol);
            }
        }
        if (parseInt(value) > 0 && hasDecimal) {
            result = this.setDecimalPrecision(result, config);
        }
        return result;
    };
    SppcNumConfigPipe.prototype.setSeperator = function (num, char) {
        var parts = num.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, char);
        return parts.join(".");
    };
    SppcNumConfigPipe.prototype.setDecimalPrecision = function (num, config) {
        if (config.decimalPrecision > 0) {
            var parts = num.toString().split(".");
            var number = num;
            var decimalPrecisionDigit = config.maxPrecision < config.decimalPrecision ? config.maxPrecision : config.decimalPrecision;
            if (parts.length > 1) {
                number = parts[1];
                if (number.length != decimalPrecisionDigit) {
                    if (number.length < decimalPrecisionDigit) {
                        for (var i = 0; i <= decimalPrecisionDigit - number.length; i++) {
                            number += "0";
                        }
                    }
                    else {
                        number = number.substring(0, decimalPrecisionDigit);
                    }
                }
                parts[1] = number;
            }
            else {
                var decimalNum = "";
                for (var i = 0; i < decimalPrecisionDigit; i++) {
                    decimalNum += "0";
                }
                parts.push(decimalNum);
            }
            return parts.join(".");
        }
        return num;
    };
    SppcNumConfigPipe = __decorate([
        core_1.Pipe({
            name: 'SppcNumConfig'
        })
    ], SppcNumConfigPipe);
    return SppcNumConfigPipe;
}());
exports.SppcNumConfigPipe = SppcNumConfigPipe;
//# sourceMappingURL=sppc.numConfig.pipe.js.map