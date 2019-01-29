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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../../environments/environment");
var core_1 = require("@angular/core");
var detail_component_1 = require("../../class/detail.component");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var core_2 = require("@angular/core");
var reporting_service_1 = require("../../service/report/reporting.service");
var reportApi_1 = require("../../service/api/reportApi");
var source_1 = require("../../class/source");
var reportParameters_component_1 = require("../reportParameters/reportParameters.component");
var kendo_angular_treeview_1 = require("@progress/kendo-angular-treeview");
var filter_1 = require("../../class/filter");
var filterExpressionBuilder_1 = require("../../class/filterExpressionBuilder");
var moment = require("jalali-moment");
var reportViewer_component_1 = require("../reportViewer/reportViewer.component");
var forms_1 = require("@angular/forms");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var ReportManagementComponent = /** @class */ (function (_super) {
    __extends(ReportManagementComponent, _super);
    function ReportManagementComponent(toastrService, translate, renderer, metaDataService, entityType, metaDataName, reportingService) {
        var _this = _super.call(this, toastrService, translate, renderer, metaDataService, entityType, metaDataName) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metaDataService = metaDataService;
        _this.entityType = entityType;
        _this.metaDataName = metaDataName;
        _this.reportingService = reportingService;
        _this.active = false;
        _this.showReportDesigner = false;
        _this.showReportViewer = false;
        _this.showSaveAsDialog = false;
        _this.selectedKeys = [];
        _this.report = new Stimulsoft.Report.StiReport();
        _this.expandedKeys = [];
        _this.deleteConfirm = false;
        _this.disableButtons = false;
        _this.viewer = new Stimulsoft.Viewer.StiViewer(null, 'StiViewer', false);
        _this.reportForm = new forms_1.FormGroup({
            reportName: new forms_1.FormControl("", [forms_1.Validators.required, forms_1.Validators.maxLength(256)]),
        });
        return _this;
    }
    ReportManagementComponent.prototype.ngOnInit = function () {
        this.innerWidth = window.innerWidth;
        this.innerHeight = window.screen.height; //window.innerHeight
        this.initViewer();
        this.disableButtons = true;
    };
    ReportManagementComponent.prototype.onNodeClick = function (e) {
        var data = e.dataItem;
        if (!data.isGroup) {
            this.currentReportId = data.id;
            this.deleteConfirmMsg = source_1.String.Format(this.getText("Report.DeleteReportConfirm"), data.caption);
            this.disableButtons = false;
        }
        else
            this.disableButtons = true;
    };
    ReportManagementComponent.prototype.showDialog = function () {
        var _this = this;
        this.active = true;
        this.reportingService.getAll(reportApi_1.ReportApi.ReportsHierarchy)
            .subscribe(function (res) {
            //var i = res;
            _this.treeData = res.body;
            //expand treeview base on baseid
            //   if(this.baseId)
            //   {
            //     this.expandAndSelectDefault(this.baseId);                      
            //   }
        });
    };
    //select and expand tree node baseon report baseId
    /*public expandAndSelectDefault(baseId: string) {
        var expandKeysArray: string[];

        var defaltReportUrl = String.Format(ReportApi.DefaultSystemReport, this.baseId);
        this.reportingService.getAll(defaltReportUrl)
            .subscribe((res: any) => {
                var report = <Report>res.body;

                expandKeysArray = new Array<any>();
                this.selectedKeys = new Array<any>();

                var nodeData = this.treeData.filter((p: any) => p.id == report.id)[0];
                this.selectedKeys.push(nodeData.id);

                while (nodeData.parentId != null) {
                    expandKeysArray.push(nodeData.parentId);
                    var parentNode = this.treeData.filter((p: any) => p.id == nodeData.parentId);
                    nodeData = parentNode[0];
                }

                this.expandedKeys = expandKeysArray;
            });

    }*/
    ReportManagementComponent.prototype.showParameterForm = function (printInfo) {
        this.reportParameter.showDialog(printInfo);
    };
    ReportManagementComponent.prototype.onOkParams = function (event) {
        this.previewReport(event.params);
    };
    ReportManagementComponent.prototype.initViewer = function () {
        if (this.CurrentLanguage == "fa")
            Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/fa.xml");
        if (this.CurrentLanguage == "en")
            Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/en.xml");
        Stimulsoft.Base.StiLicense.key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHlrzAZzmWmSnQQ4gKFiZ4LJpJv//QjFVXxcHAVbzZfXjyOGPmj/m+BEjr2Z14dWeqLFNGF74GELbTTKs2+Le/9cDIWdGNnOpEK2aGdYllauMPLQsiScC521JIEYSdOspiRHSLcegksxfNedJjyIjGlfI2YrddBRWGiO+uWOHE5oz9hLG8VPBSRo60KmgkscM5X+7+aQ+6vzKKOC2XB+e6BMQC5qNVBUblfGQR2EjNLZKmSJtvek7IbG/OK+XP0j2bwicyJUGC0pyLHqctr3BpcO/gA5LoVfuwqYG3klL//owBkObPPhJV1HD6XsHL0GDryssJFaDCQIyXMrOn7hNQNkEIyx+AJDNgf5XfxPgEgFsRhYCPYq7ccutg2by8duOxbF3xH0gL/uAQN275COXJBV3W62DSLM+o8azChG+Z7y0dF9f4whZ/SKD4DwNPUWK7osEPVwl5BY+0lkdqd67fatlrlc0QU/ZX9f5QcTKfl5ljuNc+kcqxmd9NND6Xzrw9gFsFqIWqqVo++DdoAZFStXMkOp/nTNBQMRA100k3vi2SbbiHq/gVimrQecUhWG0qU5zcemtVGDMs1ruXsoHX8pYX/rMJHH09qCWllVyBykkTLourYEig9g5fhKDYRV05aC0cWsbxR2nj9TH3SLmG4P2Px7uJsq6iOsnIHWuBMwk8oF7xPEugjw+x8lkjVVoV8WWBSdjIxGh4LviZXBEJm9FTJzYcnEHMZRh0uVE1g8crC+TfRVii7dcdZzeQklzyNY+0Q1/hRaIUs+mNPRiqG6YqEv3f+yG4ncxzkCWZDvXPox87y61jbg6Dg73X1RAwwvbIXuJVANbaDOefUELPmpz4SIpHx8zpLSmn1H1u0PolbsimLigcGw2bJQeuU++OBU74vJJde3JdoO6IOfmUJkoxprdszyknLm+zWgnC+jjaCtEZZuOIJqyuVPoqHRiFkqNjbddkvGMmj/4+2D6BdYQot9sEOW7iCgV4SvZ/efC0NlRX+Z+6PODwKJiO+Sen5aAlsJcL2jIUSAjgyS+7im7XTGlYKuRL59EQjA5HArO1ikJ0P/2pk4u91z2J8GRvTPu5BZUI9M0BLGLAVCFMte4JQCOr+f785RgjerSNCSgN4Mfa5+jDQAKTAVAO5tqT/SBEm0M5U1EylQ/fbseKt+dQ1/VzqlQ9SH14jtI0J97ACqk9SBt9xpTgBnJrBSTnnY21l2zWS7/2k5U9LPDJn0Lm32ueoDRFaM4JeK1HoSi2HvOYy1V1hU5pCe893QsBE/HOVp4UWu9lfiEWunHEEdPZOUPgc131KwJrM4K3DYiBbXl442TgbNLfz5IBnAw1NVabMXXyx2LOi6x35xw1YLMRYNWYE9QpocBhoFQtStd2OUZ5CqvxhXf+VaLK3hmm1GvlqpUK6LIDd3eyuQK4f0E7+zVSBaV6eSDI9YJC42Ee+Br8AByGYLRaFISpDculGt2nqwFL6cwltv1Xy11frJR2KqbR8sd6dI0V69XnwBziRzJq1SyAZd9bzClYSpA3ZYPN9ghdaHA+GZak0IYMokWLi6oYquOCRoy8f0sEQM2Uhw2x/E9tgyNoLZhDhrk805/VCsThI5fHn0YWVnmQZTrGkOwnoqLw3VHb7akUmNnjMlk/tD59bR2lgD+fnNuNsBYDDjJpg+fKmgf9araTPEIpuuanp53e6xodRYKIj4o4+39DrPK10eR4CDfSh5UShvnCZz+V0FAkIkoM92U1JTU59P4M4pzc8PswmS1rGTRaZMUrTYrjeGCHC9Hl0CTIR1/rQAx8iIcC3yVNCeiTJAmKMCl830O4GpEfduNHQgDrlsJC4q6RA7J2kUzW2WQvKFKH3bRH1hOc6LZK4DmwMGzXMKDKOxK0dzld2/ImRN6DbPacV/4d0HK06qBOFEgUJqXhMpV1JjsXVvmx/m2LCRgkD5vPEwcuiWtWde7tISLCEg6hjAV9+Hx6zOWpozg7aZMtikT+43uWakRkU/H+ITIGhqxuQhkZkmIddWrjD5lJtdUOSa0FWu969EDp4XB8dmUKSwyrkgOHZu6DutFW5ArtqhNejthWt/sV1FkSbvdd26zn1fSO4pDa4pDmcSo+l/4DChZbEyICc7IQrPjVuRUlVGuAVksZTBX+VYIip8LsJSFLHo7Dnn4QT3qDNIh8aAcY3fnHhph4G5ekbvGOw3+m1qqs8t0m89vdK7k8nJTw==";
    };
    ReportManagementComponent.prototype.createFilters = function (params) {
        var currentFilter;
        var filters = new Array();
        params.forEach(function (param) {
            var operator = "";
            switch (param.operator.toLowerCase()) {
                case "eq":
                    operator = " == {0}";
                    break;
                case "neq":
                    operator = " != {0}";
                    break;
                case "lte":
                    operator = " <= {0}";
                    break;
                case "gte":
                    operator = " >= {0}";
                    break;
                case "lt":
                    operator = " < {0}";
                    break;
                case "gt":
                    operator = " > {0}";
                    break;
                case "contains":
                    operator = ".Contains({0})";
                    break;
                case "doesnotcontain":
                    operator = ".IndexOf({0}) == -1";
                    break;
                case "startswith":
                    operator = ".StartsWith({0})";
                    break;
                case "endswith":
                    operator = ".EndsWith({0})";
                    break;
                default:
                    operator = " == {0}";
            }
            var value = param.value ? param.value : "";
            var filter = new filter_1.Filter(param.fieldName, value, operator, param.dataType);
            filters.push(filter);
        });
        var filterExpBuilder = new filterExpressionBuilder_1.FilterExpressionBuilder();
        var filterExp = filterExpBuilder.And(filters)
            .Build();
        return filterExp;
    };
    ReportManagementComponent.prototype.saveAsReport = function () {
        if (this.currentReportId) {
            this.showSaveAsDialog = true;
            this.reportForm.controls.reportName.setValue("");
        }
    };
    ReportManagementComponent.prototype.okSaveAsClick = function () {
        var _this = this;
        var localReport = new reporting_service_1.LocalReportInfo();
        localReport.template = "";
        localReport.reportId = this.currentReportId;
        var localId = this.CurrentLanguage == 'fa' ? 2 : 1;
        localReport.localeId = localId;
        localReport.caption = this.reportForm.controls.reportName.value;
        var url = reportApi_1.ReportApi.Reports;
        this.reportingService.saveAsReport(url, localReport).subscribe(function (response) {
            _this.showMessage(_this.getText('Report.SaveAsIsOk'));
            _this.reportingService.getAll(reportApi_1.ReportApi.ReportsHierarchy)
                .subscribe(function (res) {
                _this.treeData = res.body;
            });
            _this.showSaveAsDialog = false;
        }, (function (error) {
            _this.showMessage(error);
        }));
    };
    ReportManagementComponent.prototype.cancelReportForm = function () {
        this.showSaveAsDialog = false;
    };
    ReportManagementComponent.prototype.prepareReport = function () {
        var _this = this;
        var url = source_1.String.Format(reportApi_1.ReportApi.Report, this.currentReportId);
        this.reportingService.getAll(url).subscribe(function (res) {
            var printInfo = res.body;
            if (printInfo.parameters.length > 0) {
                _this.currentPrintInfo = printInfo;
                _this.showParameterForm(printInfo);
            }
        });
    };
    ReportManagementComponent.prototype.previewReport = function (params) {
        var _this = this;
        this.showReportViewer = true;
        this.showReportDesigner = false;
        var serviceUrl = environment_1.environment.BaseUrl + "/" + this.currentPrintInfo.serviceUrl;
        var filterExpression = this.createFilters(params);
        var sort = "";
        this.reportingService.getAll(serviceUrl, sort, filterExpression).subscribe(function (response) {
            var fdate = moment(_this.FiscalPeriodStartDate, 'YYYY-M-D HH:mm:ss')
                .locale(_this.CurrentLanguage)
                .format('YYYY/M/D');
            var tdate = moment(_this.FiscalPeriodEndDate, 'YYYY-M-D HH:mm:ss')
                .locale(_this.CurrentLanguage)
                .format('YYYY/M/D');
            var reportData = {
                rows: response.body,
                fromDate: fdate,
                toDate: tdate
            };
            _this.reportViewer.showReportViewer(_this.currentPrintInfo.template, reportData);
        });
    };
    ReportManagementComponent.prototype.designReport = function () {
        var _this = this;
        var url = source_1.String.Format(reportApi_1.ReportApi.ReportDesign, this.currentReportId);
        this.showReportViewer = false;
        this.showReportDesigner = true;
        this.reportingService.getAll(url).subscribe(function (res) {
            var printInfo = res.body;
            var options = new Stimulsoft.Designer.StiDesignerOptions();
            options.appearance.fullScreenMode = true;
            options.toolbar.showPreviewButton = false;
            options.toolbar.showFileMenu = false;
            options.components.showImage = false;
            options.components.showShape = false;
            options.components.showPanel = false;
            options.components.showCheckBox = false;
            options.components.showSubReport = false;
            var designer = new Stimulsoft.Designer.StiDesigner(null, "StiDesigner", false);
            var rpt = new Stimulsoft.Report.StiReport();
            var reportTemplate;
            reportTemplate = printInfo.template;
            rpt.load(reportTemplate);
            designer.report = rpt;
            designer.renderHtml('designer');
            var currentId = _this.currentReportId;
            var service = _this.reportingService;
            var localId = _this.CurrentLanguage == 'fa' ? 2 : 1;
            var thisComponent = _this;
            // Assign the onSaveReport event function
            designer.onSaveReport = function (e) {
                var jsonStr = e.report.saveToJsonString();
                var localReport = new reporting_service_1.LocalReportInfo();
                localReport.template = jsonStr;
                localReport.reportId = currentId;
                localReport.localeId = localId;
                var url = source_1.String.Format(reportApi_1.ReportApi.Report, currentId);
                service.saveReport(url, localReport).subscribe(function (response) {
                    thisComponent.showMessage(thisComponent.getText('Report.SaveIsOk'));
                }, (function (error) {
                    thisComponent.showMessage(error);
                }));
            };
        });
    };
    ReportManagementComponent.prototype.deleteReport = function (deleteFlag) {
        var _this = this;
        this.deleteConfirm = false;
        if (deleteFlag) {
            var reportId = this.currentReportId;
            var url = source_1.String.Format(reportApi_1.ReportApi.Report, reportId);
            this.reportingService.deleteReport(url).subscribe(function (response) {
                _this.showMessage(_this.getText('Report.ReportDeleted'));
                _this.currentReportId = null;
                _this.disableButtons = true;
                _this.reportingService.getAll(reportApi_1.ReportApi.ReportsHierarchy)
                    .subscribe(function (res) {
                    _this.treeData = res.body;
                });
            }, (function (error) {
                _this.showMessage(error);
            }));
        }
    };
    ReportManagementComponent.prototype.showDeleteConfirm = function () {
        this.deleteConfirm = true;
    };
    ReportManagementComponent.prototype.closeDialog = function () {
        this.active = false;
    };
    ReportManagementComponent.prototype.iconClass = function (dataItem) {
        return {
            'k-i-ascx': dataItem.isGroup == false,
            'k-i-folder': dataItem.isGroup == true
        };
    };
    ReportManagementComponent.prototype.setClass = function (dataItem) {
        return {
            'rep-folder': dataItem.isGroup,
            'rep-system': !dataItem.isGroup && dataItem.isSystem,
            'rep-user': !dataItem.isGroup && !dataItem.isSystem
        };
    };
    __decorate([
        core_1.Input()
    ], ReportManagementComponent.prototype, "baseId", void 0);
    __decorate([
        core_1.ViewChild(reportViewer_component_1.ReportViewerComponent)
    ], ReportManagementComponent.prototype, "reportViewer", void 0);
    __decorate([
        core_1.ViewChild(reportParameters_component_1.ReportParametersComponent)
    ], ReportManagementComponent.prototype, "reportParameter", void 0);
    __decorate([
        core_1.ViewChild(kendo_angular_treeview_1.TreeViewComponent)
    ], ReportManagementComponent.prototype, "treeView", void 0);
    ReportManagementComponent = __decorate([
        core_1.Component({
            selector: 'report-management',
            templateUrl: './reportManagement.component.html',
            styleUrls: ['./reportManagement.component.css'],
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        }),
        __param(4, core_2.Optional()),
        __param(4, core_2.Inject('empty')),
        __param(5, core_2.Optional()),
        __param(5, core_2.Inject('empty'))
    ], ReportManagementComponent);
    return ReportManagementComponent;
}(detail_component_1.DetailComponent));
exports.ReportManagementComponent = ReportManagementComponent;
//# sourceMappingURL=reportManagement.component.js.map