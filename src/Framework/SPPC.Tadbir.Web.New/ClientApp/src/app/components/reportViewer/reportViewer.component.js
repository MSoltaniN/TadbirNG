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
var default_component_1 = require("../../class/default.component");
require("rxjs/Rx");
var source_1 = require("../../class/source");
var environment_1 = require("../../../environments/environment");
var moment = require("jalali-moment");
var ReportViewerComponent = /** @class */ (function (_super) {
    __extends(ReportViewerComponent, _super);
    function ReportViewerComponent(toastrService, translate, sppcLoading, cdref, voucherService, renderer, metadata, settingService, http, reporingService) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, settingService, environment_1.Entities.Voucher, environment_1.Metadatas.Voucher) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.sppcLoading = sppcLoading;
        _this.cdref = cdref;
        _this.voucherService = voucherService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        _this.http = http;
        _this.reporingService = reporingService;
        _this.viewer = new Stimulsoft.Viewer.StiViewer(null, 'StiViewer', false);
        _this.report = new Stimulsoft.Report.StiReport();
        _this.active = false;
        _this.showViewer = false;
        return _this;
    }
    ReportViewerComponent.prototype.ngOnInit = function () {
        this.innerWidth = window.innerWidth;
        this.registerFunctions();
        this.initViewer();
    };
    ReportViewerComponent.prototype.closeForm = function () {
        this.active = false;
    };
    ReportViewerComponent.prototype.registerFunctions = function () {
        var function1 = function (checklist, state) {
            var result = "";
            return result;
        };
        Stimulsoft.Report.Dictionary.StiFunctions.addFunction("TadbirFunctions", "Accounting", "TestFunction", "this is a test function", "", typeof (source_1.String), "", [typeof (source_1.String)], [""], [""], function (value) {
            var result = value;
            return result.toUpperCase();
        });
    };
    ReportViewerComponent.prototype.initViewer = function () {
        //create header
        //var header = new HttpHeaders();
        //header = header.append('Content-Type', 'text/xml');
        //header = header.append('X-Tadbir-AuthTicket', this.Ticket);
        if (this.CurrentLanguage == "fa") {
            //header = header.append('Accept-Language', 'fa-IR,fa');
            Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/fa.xml");
        }
        if (this.CurrentLanguage == "en") {
            //header = header.append('Accept-Language', 'en-US,en');
            Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/en.xml");
        }
        Stimulsoft.Base.StiLicense.key = "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";
    };
    ReportViewerComponent.prototype.fillResourceVariables = function (reportObject, stiReport) {
        reportObject.resourceKeys.split(',').forEach(function (resKey) {
            var resValue = reportObject.resourceMap[resKey];
            var found = stiReport.dictionary.variables.items.find(function (x) { return x.name === resKey; });
            if (found)
                stiReport.dictionary.variables.getByName(found.name).valueObject = resValue;
        });
    };
    ReportViewerComponent.prototype.showVoucherReport = function (reportObject, reportData) {
        var _this = this;
        this.active = true;
        setTimeout(function () {
            console.log('Load report from url');
            var reportTemplate;
            // if (this.CurrentLanguage == "fa")
            //     reportTemplate = reportObject.template;
            // else
            //     reportTemplate = reportObject.templateLtr;
            _this.report.load(reportTemplate);
            _this.report.regData("Vouchers", "", reportData.rows);
            _this.report.dictionary.variables.getByName("FDate").valueObject = reportData.fromDate;
            _this.report.dictionary.variables.getByName("TDate").valueObject = reportData.toDate;
            _this.fillResourceVariables(reportObject, _this.report);
            _this.report.render();
            _this.viewer.report = _this.report;
            console.log('Rendering the viewer to selected element');
            _this.viewer.renderHtml('viewer');
        }, 10);
    };
    ReportViewerComponent.prototype.showVoucherStdFormReport = function (report, reportData) {
        var _this = this;
        //url
        //this.rowData.data
        this.active = true;
        setTimeout(function () {
            console.log('Load report from url');
            //comment by nouri
            //this.report.load(report.template);
            _this.report.regData("Vouchers", "VouchersStdForm", reportData.rows.lines);
            _this.report.dictionary.variables.getByName("currentDate").valueObject = reportData.currentDate;
            _this.report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
            _this.report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
            _this.report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
            _this.report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;
            _this.report.render();
            _this.viewer.report = _this.report;
            console.log('Rendering the viewer to selected element');
            _this.viewer.renderHtml('viewer');
        }, 10);
    };
    ReportViewerComponent.prototype.showReportViewer = function (reportTemplate, reportData) {
        var _this = this;
        this.active = true;
        this.viewer = new Stimulsoft.Viewer.StiViewer(null, this.Id, false);
        setTimeout(function () {
            console.log('Load report from url');
            _this.report.load(reportTemplate);
            _this.report.regData("Vouchers", "", reportData.rows);
            var parameters = reportData.parameters;
            var localReport = _this.report;
            var lang = _this.CurrentLanguage;
            parameters.forEach(function (param) {
                var value = param.value;
                if (param.dataType == "System.DateTime") {
                    var fdate = moment(param.value, 'YYYY-M-D HH:mm:ss')
                        .locale(lang)
                        .format('YYYY/M/D');
                    value = fdate;
                }
                if (localReport.dictionary.variables.getByName(param.name) != null)
                    localReport.dictionary.variables.getByName(param.name).valueObject = value;
            });
            _this.report = localReport;
            //this.fillResourceVariables(reportObject,this.report);
            _this.report.render();
            _this.viewer.report = _this.report;
            console.log('Rendering the viewer to selected element');
            _this.viewer.renderHtml(_this.Id);
        }, 10);
    };
    __decorate([
        core_1.Input()
    ], ReportViewerComponent.prototype, "baseId", void 0);
    __decorate([
        core_1.Input()
    ], ReportViewerComponent.prototype, "showViewer", void 0);
    __decorate([
        core_1.Input()
    ], ReportViewerComponent.prototype, "Id", void 0);
    ReportViewerComponent = __decorate([
        core_1.Component({
            selector: 'report-viewer',
            templateUrl: './reportViewer.component.html',
            styleUrls: ['./reportViewer.component.css'],
            encapsulation: core_1.ViewEncapsulation.None
        })
    ], ReportViewerComponent);
    return ReportViewerComponent;
}(default_component_1.DefaultComponent));
exports.ReportViewerComponent = ReportViewerComponent;
//# sourceMappingURL=reportViewer.component.js.map