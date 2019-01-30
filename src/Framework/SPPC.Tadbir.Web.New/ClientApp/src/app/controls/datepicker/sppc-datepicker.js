"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var moment = require("jalali-moment");
var KeyCode_1 = require("../../enum/KeyCode");
var SppcDatepicker = /** @class */ (function () {
    function SppcDatepicker(datepipe, controlContainer) {
        this.datepipe = datepipe;
        this.controlContainer = controlContainer;
        this.dateLocale = 'fa';
        this.parseError = false;
        this.inputDateFormat = 'yyyy/MM/dd hh:mm';
        this.dateFormat = "YYYY/MM/DD";
        this.spliterChar = "/";
        this.isDisplayDate = true;
        this.i = 0;
        this.propagateChange = function () { };
    }
    SppcDatepicker_1 = SppcDatepicker;
    SppcDatepicker.prototype.ngOnInit = function () {
        if (this.controlContainer) {
            if (this.formControlName && this.controlContainer.control != null) {
                this.control = this.controlContainer.control.get(this.formControlName);
            }
        }
        if (this.control != null) {
            this.control.clearValidators();
        }
        //var startDate;
        //var endDate;
        var nowDate = new Date();
        var endDiff;
        var startDiff;
        var endDiffDays = 0;
        var startDiffDays = 0;
        if (this.minDate) {
            this.minDate = this.datepipe.transform(this.minDate, this.inputDateFormat);
            this.startDate = new Date(this.minDate.split(' ')[0]);
        }
        if (this.maxDate) {
            this.maxDate = this.datepipe.transform(this.maxDate, this.inputDateFormat);
            this.endDate = new Date(this.maxDate.split(' ')[0]);
        }
        this.dateObject = moment();
        if (this.endDate != null) {
            endDiff = nowDate.getTime() - this.endDate.getTime();
            endDiffDays = endDiff / (1000 * 3600 * 24);
            if (endDiffDays > 1) {
                this.dateObject = moment(this.endDate);
            }
        }
        if (this.startDate != null) {
            startDiff = this.startDate.getTime() - nowDate.getTime();
            startDiffDays = startDiff / (1000 * 3600 * 24);
            if (startDiffDays > 1) {
                this.dateObject = moment(this.startDate);
            }
        }
        if (this.startDate != null && this.endDate != null) {
            if (endDiffDays < 1 && startDiffDays < 1) {
                this.dateObject = moment();
            }
        }
        var lang = localStorage.getItem('lang');
        if (lang) {
            this.dateLocale = lang;
            if (lang == "en")
                this.dateFormat = "MM/DD/YYYY";
        }
        if (this.displayDate) {
            this.displayDate = this.datepipe.transform(this.displayDate, this.inputDateFormat);
            this.dateObject = moment(this.displayDate);
        }
        //if (!this.isDisplayDate) {
        //    if (this.editDateValue) {
        //        this.dateObject = this.editDateValue;
        //    }
        //    else {
        //        this.dateObject = null;
        //    }
        //    this.onDateFocusOut();
        //}
        this.dateConfig = {
            mode: "day",
            format: this.dateFormat,
            locale: this.dateLocale,
            min: this.minDate,
            max: this.maxDate,
            showGoToCurrent: true,
            showMultipleYearsNavigation: true
        };
    };
    SppcDatepicker.prototype.ngOnDestroy = function () {
        moment.locale('en');
    };
    SppcDatepicker.prototype.LimitationDate = function (toDate, operationIncrese) {
        var endDiff;
        var startDiff;
        var endDiffDays = 0;
        var startDiffDays = 0;
        this.dateObject = moment(toDate);
        var strDate = this.datepipe.transform(toDate, this.inputDateFormat);
        if (strDate != null) {
            var date = new Date(strDate);
            if (this.endDate != null) {
                endDiff = date.getTime() - this.endDate.getTime();
                endDiffDays = endDiff / (1000 * 3600 * 24);
            }
            if (this.startDate != null) {
                startDiff = this.startDate.getTime() - date.getTime();
                startDiffDays = startDiff / (1000 * 3600 * 24);
            }
            if (operationIncrese && this.endDate != null && endDiffDays > 1) {
                this.dateObject = moment(this.endDate);
            }
            else if (!operationIncrese && this.startDate != null && startDiffDays > 1) {
                this.dateObject = moment(this.startDate);
            }
            if (operationIncrese == null) {
                if (this.endDate != null && endDiffDays > 1) {
                    this.dateObject = moment(this.endDate);
                }
                if (this.startDate != null && startDiffDays > 1) {
                    this.dateObject = moment(this.startDate);
                }
                if (this.startDate != null && this.endDate != null && endDiffDays < 1 && startDiffDays < 1) {
                    this.dateObject = moment();
                }
            }
        }
    };
    SppcDatepicker.prototype.onChangeDateKey = function (event) {
        var allowKey = false;
        switch (event) {
            case KeyCode_1.KeyCode.Space: {
                debugger;
                this.dateObject = moment();
                this.LimitationDate(new Date());
                break;
            }
            case KeyCode_1.KeyCode.Page_Up: {
                var newDate = this.dateObject != null ? this.dateObject.add(1, 'months') : moment();
                this.LimitationDate(newDate, true);
                break;
                //var newDate = this.dateObject != null ? this.dateObject.add(1, 'years') : moment();
                //this.LimitationDate(newDate, true);
                //break;
            }
            case KeyCode_1.KeyCode.Page_Down: {
                var newDate = this.dateObject != null ? this.dateObject.add(-1, 'months') : moment();
                this.LimitationDate(newDate, false);
                break;
                //var newDate = this.dateObject != null ? this.dateObject.add(-1, 'years') : moment();
                //this.LimitationDate(newDate, false);
                //break;
            }
            case KeyCode_1.KeyCode.Down_Arrow: {
                var newDate = this.dateObject != null ? this.dateObject.add(-1, 'days') : moment();
                this.LimitationDate(newDate, false);
                break;
                //var newDate = this.dateObject != null ? this.dateObject.add(-1, 'months') : moment();
                //this.LimitationDate(newDate, false);
                //break;
            }
            case KeyCode_1.KeyCode.Up_Arrow: {
                var newDate = this.dateObject != null ? this.dateObject.add(1, 'days') : moment();
                this.LimitationDate(newDate, true);
                break;
                //var newDate = this.dateObject != null ? this.dateObject.add(1, 'months') : moment();
                //this.LimitationDate(newDate, true);
                //break;
            }
            //case KeyCode.Left_Arrow: {
            //  var newDate = this.dateObject != null ? this.dateObject.add(-1, 'days') : moment();
            //  this.LimitationDate(newDate, false);
            //  break;
            //}
            //case KeyCode.Right_Arrow: {
            //  var newDate = this.dateObject != null ? this.dateObject.add(1, 'days') : moment();
            //  this.LimitationDate(newDate, true);
            //  break;
            //}
            default: {
                if ((event >= 48 && event <= 57) || (event >= 96 && event <= 105) || (event == 191) || (event == 111) || (event == 8)) {
                    allowKey = true;
                }
                else {
                    allowKey = false;
                }
                break;
            }
        }
        this.onDateFocusOut();
        return allowKey;
    };
    SppcDatepicker.prototype.onDateChange = function () {
        var _this = this;
        this.i++;
        if (!this.isDisplayDate && this.i <= 2) {
            this.dateObject = null;
            if (this.editDateValue) {
                this.dateObject = this.editDateValue;
            }
        }
        this.parseError = typeof this.dateObject === "object" && this.dateObject != null ? false : true;
        if (this.dateObject == undefined) {
            setTimeout(function () {
                _this.propagateChange("");
            }, 1);
        }
        else {
            this.onDateFocusOut();
        }
    };
    SppcDatepicker.prototype.onDateFocusOut = function () {
        var _this = this;
        this.parseError = false;
        if (this.dateObject != null) {
            if (typeof this.dateObject === "object") {
                this.parseError = false;
                setTimeout(function () {
                    _this.propagateChange(_this.datepipe.transform(_this.dateObject, _this.inputDateFormat));
                }, 1);
            }
            else {
                //this.parseError = false;
                var strDate = this.dateObject;
                var dateArray = void 0;
                if (strDate === undefined) {
                    this.parseError = true;
                }
                else {
                    var yearDate = 0;
                    var monthDate = 0;
                    var dayDate = 0;
                    var formatArray = this.dateFormat.split(this.spliterChar);
                    dateArray = strDate.split(this.spliterChar);
                    if (dateArray.length == 3) {
                        for (var i = 0; i < formatArray.length; i++) {
                            switch (formatArray[i]) {
                                case "YYYY": {
                                    yearDate = +dateArray[i];
                                    break;
                                }
                                case "YY": {
                                    yearDate = +dateArray[i];
                                    break;
                                }
                                case "MM": {
                                    monthDate = +dateArray[i];
                                    break;
                                }
                                case "M": {
                                    monthDate = +dateArray[i];
                                    break;
                                }
                                case "DD": {
                                    dayDate = +dateArray[i];
                                    break;
                                }
                                case "D": {
                                    dayDate = +dateArray[i];
                                    break;
                                }
                                default: {
                                    this.parseError = true;
                                    break;
                                }
                            }
                        }
                        for (var i = 0; i < formatArray.length; i++) {
                            switch (formatArray[i]) {
                                case "YYYY": {
                                    if (dateArray[i].length < 4) {
                                        this.parseError = true;
                                    }
                                    break;
                                }
                                case "YY": {
                                    if (dateArray[i].length < 2) {
                                        this.parseError = true;
                                    }
                                    break;
                                }
                                case "MM": {
                                    var month = +dateArray[i];
                                    if (month == 0 || month > 12) {
                                        this.parseError = true;
                                    }
                                    else {
                                        if (month < 10) {
                                            dateArray[i] = "0" + month.toString();
                                        }
                                        else {
                                            dateArray[i] = month.toString();
                                        }
                                    }
                                    break;
                                }
                                case "M": {
                                    var month = +dateArray[i];
                                    if (month == 0 || month > 12) {
                                        this.parseError = true;
                                    }
                                    else {
                                        dateArray[i] = month.toString();
                                    }
                                    break;
                                }
                                case "DD": {
                                    var day = +dateArray[i];
                                    if (day == 0 || day > 31 || (monthDate > 6 && day > 30)) {
                                        this.parseError = true;
                                    }
                                    else {
                                        if (day < 10) {
                                            dateArray[i] = "0" + day.toString();
                                        }
                                        else {
                                            dateArray[i] = day.toString();
                                        }
                                    }
                                    break;
                                }
                                case "D": {
                                    var day = +dateArray[i];
                                    if (day == 0 || day > 31 || (monthDate > 6 && day > 30)) {
                                        this.parseError = true;
                                    }
                                    else {
                                        dateArray[i] = day.toString();
                                    }
                                    break;
                                }
                                default: {
                                    this.parseError = true;
                                    break;
                                }
                            }
                        }
                    }
                    else {
                        this.parseError = true;
                    }
                }
                if (this.parseError) {
                    setTimeout(function () {
                        _this.propagateChange("");
                    }, 1);
                }
                else {
                    this.dateObject = this.dateLocale == 'fa' ? moment(dateArray.join(this.spliterChar), 'jYYYY/jM/jD') : moment(dateArray.join(this.spliterChar).toString()).locale('en');
                    setTimeout(function () {
                        _this.propagateChange(_this.datepipe.transform(_this.dateObject, _this.inputDateFormat));
                    }, 1);
                }
            }
        }
        else {
            //this.parseError = true;
        }
    };
    SppcDatepicker.prototype.onGoToCurrentDate = function () {
        this.dateObject = moment();
    };
    SppcDatepicker.prototype.writeValue = function (value) {
        if (value) {
            this.date = this.datepipe.transform(value, this.inputDateFormat);
            this.editDateValue = moment(this.date);
            if (this.isDisplayDate) {
                this.dateObject = moment(this.date);
            }
        }
    };
    SppcDatepicker.prototype.registerOnChange = function (fn) {
        this.propagateChange = fn;
    };
    SppcDatepicker.prototype.registerOnTouched = function (fn) {
        //this.propagateChange = fn;
    };
    SppcDatepicker.prototype.validate = function (control) {
        return (!this.parseError) ? null : {
            jsonParseError: {
                valid: false,
            },
        };
    };
    var SppcDatepicker_1;
    __decorate([
        core_1.Input()
    ], SppcDatepicker.prototype, "date", void 0);
    __decorate([
        core_1.Input()
    ], SppcDatepicker.prototype, "isDisplayDate", void 0);
    __decorate([
        core_1.Input()
    ], SppcDatepicker.prototype, "displayDate", void 0);
    __decorate([
        core_1.Input()
    ], SppcDatepicker.prototype, "minDate", void 0);
    __decorate([
        core_1.Input()
    ], SppcDatepicker.prototype, "maxDate", void 0);
    __decorate([
        core_1.Input()
    ], SppcDatepicker.prototype, "formControlName", void 0);
    SppcDatepicker = SppcDatepicker_1 = __decorate([
        core_1.Component({
            selector: 'sppc-datepicker',
            template: "<dp-date-picker\n    class=\"k-textbox\"\n    [(ngModel)]=\"dateObject\"\n    (keydown)=\"onChangeDateKey($event.keyCode)\"\n    (onChange)=\"onDateChange()\" \n    (onGoToCurrent)=\"onGoToCurrentDate()\"\n    [config]='dateConfig'\n    theme=\"dp-material\"\n    (focusout)=\"onDateFocusOut()\">\n  </dp-date-picker>",
            styles: ["\n    /deep/ dp-date-picker.dp-material .dp-picker-input { width:100% !important; } \n    dp-date-picker{width:100%; direction:ltr;} \n    /deep/ dp-day-calendar{position: fixed;}\n    /deep/ sppc-datepicker input{\n    border-color: rgba(0, 0, 0, 0.15);\n    height: calc(1.42857em + (4px * 2) + (1px * 2)) !important;\n    /* border-style: solid; */\n    border-radius: 2px;\n    padding: 4px 8px;\n    width: 12.4em;\n    box-sizing: border-box;\n    border-width: 1px;\n    border-style: solid;\n    outline: 0;\n    font: inherit;\n    font-size: 14px;\n    line-height: 1.42857em;\n    display: inline-flex;\n    vertical-align: middle;\n    position: relative;\n    -webkit-appearance: none;}\n       "],
            providers: [
                {
                    provide: forms_1.NG_VALUE_ACCESSOR,
                    useExisting: core_1.forwardRef(function () { return SppcDatepicker_1; }),
                    multi: true
                },
                {
                    provide: forms_1.NG_VALIDATORS,
                    useExisting: core_1.forwardRef(function () { return SppcDatepicker_1; }),
                    multi: true,
                }
            ]
        }),
        __param(1, core_1.Optional()), __param(1, core_1.Host()), __param(1, core_1.SkipSelf())
    ], SppcDatepicker);
    return SppcDatepicker;
}());
exports.SppcDatepicker = SppcDatepicker;
//# sourceMappingURL=sppc-datepicker.js.map