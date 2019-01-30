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
var reporting_service_1 = require("../../service/report/reporting.service");
var ReportParametersComponent = /** @class */ (function () {
    function ReportParametersComponent() {
        this.onOkClick = new core_1.EventEmitter();
        this.fieldArray = [];
        this.parameterForm = new forms_1.FormGroup({});
    }
    ReportParametersComponent.prototype.ngOnInit = function () {
    };
    ReportParametersComponent.prototype.showDialog = function (printInfo) {
        this.active = true;
        //add sample parameters
        //TODO: get paramaters from service by reportId
        var paramsForm = new forms_1.FormGroup({});
        var paramArrays = new Array();
        printInfo.parameters.forEach(function (param) {
            var paramInfo = new reporting_service_1.ParameterInfo();
            paramInfo.fieldName = param.fieldName;
            paramInfo.controlType = param.controlType;
            paramInfo.id = param.id;
            paramInfo.defaultValue = param.defaultValue ? param.defaultValue : "";
            paramInfo.captionKey = param.captionKey;
            paramInfo.operator = param.operator;
            paramInfo.dataType = param.dataType;
            paramArrays.push(paramInfo);
            paramsForm.addControl(paramInfo.fieldName, new forms_1.FormControl());
        });
        this.fieldArray = paramArrays;
        this.parameterForm = paramsForm;
        //show dialog
    };
    ReportParametersComponent.prototype.cancelDialog = function () {
        this.active = false;
    };
    ReportParametersComponent.prototype.okDialog = function () {
        this.active = false;
        this.onOkClick.emit({ params: this.fieldArray });
    };
    __decorate([
        core_1.Input()
    ], ReportParametersComponent.prototype, "reportId", void 0);
    __decorate([
        core_1.Input()
    ], ReportParametersComponent.prototype, "active", void 0);
    __decorate([
        core_1.Output()
    ], ReportParametersComponent.prototype, "onOkClick", void 0);
    ReportParametersComponent = __decorate([
        core_1.Component({
            selector: 'report-parameters',
            templateUrl: './reportParameters.component.html',
            styleUrls: ['./reportParameters.component.css']
        })
    ], ReportParametersComponent);
    return ReportParametersComponent;
}());
exports.ReportParametersComponent = ReportParametersComponent;
//# sourceMappingURL=reportParameters.component.js.map