"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var moment = require("jalali-moment");
var SppcGridDatepicker = /** @class */ (function () {
    function SppcGridDatepicker(datepipe, render) {
        this.datepipe = datepipe;
        this.render = render;
        this.dateLocale = 'fa';
        this.parseError = false;
        this.inputDateFormat = 'yyyy/M/d hh:mm';
        this.isDate = false;
        this.isDateTime = false;
        this.mode = 'daytime';
        this.propagateChange = function () { };
    }
    SppcGridDatepicker_1 = SppcGridDatepicker;
    SppcGridDatepicker.prototype.ngOnInit = function () {
        var dateFormat = "YYYY/MM/DD";
        var lang = localStorage.getItem('lang');
        if (lang) {
            this.dateLocale = lang;
            if (lang == "en")
                dateFormat = "MM/DD/YYYY";
        }
        if (this.mode == 'daytime') {
            dateFormat += " hh:mm";
            this.isDateTime = true;
        }
        else {
            this.isDate = true;
        }
        this.dateConfig = {
            format: dateFormat,
            locale: this.dateLocale,
            showMultipleYearsNavigation: true
        };
    };
    SppcGridDatepicker.prototype.ngOnDestroy = function () {
        moment.locale('en');
    };
    SppcGridDatepicker.prototype.DateChange = function (event) {
        var hiddenElement = document.getElementById(this.destinationElementId);
        if (this.dateObject) {
            var date = this.dateObject.format('YYYY/MM/DD');
            var hiddenElement = document.getElementById(this.destinationElementId);
            if (hiddenElement) {
                //hiddenElement.value = date;
                var gDate = moment.from(date, 'fa', 'YYYY/MM/DD').format('YYYY/MM/DD');
                hiddenElement.setAttribute('ng-reflect-model', gDate);
                hiddenElement.value = gDate;
                var eve = new Event('input');
                hiddenElement.dispatchEvent(eve);
            }
        }
        else {
            var hiddenElement = document.getElementById(this.destinationElementId);
            hiddenElement.setAttribute('ng-reflect-model', '');
            hiddenElement.value = '';
            var eve = new Event('input');
            hiddenElement.dispatchEvent(eve);
        }
    };
    SppcGridDatepicker.prototype.writeValue = function (value) {
        if (value) {
            this.date = this.datepipe.transform(value, this.inputDateFormat);
            this.dateObject = moment(this.date);
        }
    };
    SppcGridDatepicker.prototype.registerOnChange = function (fn) {
        this.propagateChange = fn;
    };
    SppcGridDatepicker.prototype.registerOnTouched = function (fn) { };
    SppcGridDatepicker.prototype.validate = function (c) {
        return (!this.parseError) ? null : {
            jsonParseError: {
                valid: false,
            },
        };
    };
    var SppcGridDatepicker_1;
    __decorate([
        core_1.Input()
    ], SppcGridDatepicker.prototype, "date", void 0);
    SppcGridDatepicker = SppcGridDatepicker_1 = __decorate([
        core_1.Component({
            selector: 'sppc-grid-datepicker',
            template: "<dp-date-picker mode=\"{{mode}}\"\n    class=\"k-textbox\" \n    [(ngModel)]=\"dateObject\"\n    (onChange)=\"DateChange($event)\" \n    [config]='dateConfig'\n    theme=\"dp-material\">\n  </dp-date-picker>",
            styles: ["\n    /deep/ dp-date-picker.dp-material .dp-picker-input { width:100% !important; } \n    dp-date-picker{width:100%; direction:ltr;} \n    /deep/ dp-day-calendar{position: fixed;}\n/deep/ dp-time-select{ display:none;}\n/deep/ dp-day-time-calendar { position: fixed; } /deep/ dp-day-time-calendar > dp-day-calendar{position:initial}\n/deep/ dp-day-time-calendar >  dp-time-select { display:block;}\n/deep/ sppc-grid-datepicker{ width:100% }\n/deep/ sppc-grid-datepicker input{ border-color: rgba(0, 0, 0, 0.08); }\n       "],
            providers: [
                {
                    provide: forms_1.NG_VALUE_ACCESSOR,
                    useExisting: core_1.forwardRef(function () { return SppcGridDatepicker_1; }),
                    multi: true
                },
                {
                    provide: forms_1.NG_VALIDATORS,
                    useExisting: core_1.forwardRef(function () { return SppcGridDatepicker_1; }),
                    multi: true,
                }
            ]
        })
    ], SppcGridDatepicker);
    return SppcGridDatepicker;
}());
exports.SppcGridDatepicker = SppcGridDatepicker;
//# sourceMappingURL=sppc-grid-datepicker.js.map