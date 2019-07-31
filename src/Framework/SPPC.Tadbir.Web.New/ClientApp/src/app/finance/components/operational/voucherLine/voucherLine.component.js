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
var index_1 = require("../../service/index");
var kendo_angular_grid_1 = require("@progress/kendo-angular-grid");
require("rxjs/Rx");
var source_1 = require("../../class/source");
var default_component_1 = require("../../class/default.component");
var environment_1 = require("../../../environments/environment");
var index_2 = require("../../service/api/index");
var documentStatusValue_1 = require("../../enum/documentStatusValue");
var reportViewer_component_1 = require("../reportViewer/reportViewer.component");
var kendo_angular_dialog_1 = require("@progress/kendo-angular-dialog");
var voucherLine_form_component_1 = require("../../components/voucherLine/voucherLine-form.component");
var VoucherLineComponent = /** @class */ (function (_super) {
    __extends(VoucherLineComponent, _super);
    //#endregion
    //#region Constructor
    function VoucherLineComponent(toastrService, dialogService, translate, sppcLoading, voucherLineService, renderer, metadata, settingService, reporingService) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, settingService, environment_1.Entities.VoucherLine, environment_1.Metadatas.VoucherArticles) || this;
        _this.toastrService = toastrService;
        _this.dialogService = dialogService;
        _this.translate = translate;
        _this.sppcLoading = sppcLoading;
        _this.voucherLineService = voucherLineService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        _this.reporingService = reporingService;
        _this.selectedRows = [];
        _this.showloadingMessage = true;
        _this.editDataItem = undefined;
        _this.groupDelete = false;
        _this.setFocus = new core_1.EventEmitter();
        return _this;
    }
    //#endregion
    //#region Events
    VoucherLineComponent.prototype.ngOnInit = function () {
        this.documentStatusValue = documentStatusValue_1.DocumentStatusValue;
        this.getVoucher();
        var test = this.voucherId;
        this.reloadGrid();
    };
    /**
     * باز کردن و مقداردهی اولیه به فرم ویرایشگر
     */
    VoucherLineComponent.prototype.openEditorDialog = function (isNew) {
        var _this = this;
        this.dialogRef = this.dialogService.open({
            title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
            content: voucherLine_form_component_1.VoucherLineFormComponent,
        });
        this.dialogRef.dialog.location.nativeElement.classList.add('dialog-style');
        this.dialogModel = this.dialogRef.content.instance;
        this.dialogModel.model = this.editDataItem;
        this.dialogModel.errorMessage = undefined;
        this.dialogModel.isNew = isNew;
        this.dialogModel.isNewBalance = this.isNewBalance;
        this.dialogModel.balance = this.balance;
        this.dialogRef.content.instance.save.subscribe(function (res) {
            _this.saveHandler(res, isNew);
        });
        var closeForm = this.dialogRef.content.instance.cancel.subscribe(function (res) {
            _this.dialogRef.close();
            _this.dialogModel.errorMessage = undefined;
            _this.dialogModel.model = undefined;
            _this.setFocus.emit();
        });
        this.dialogRef.result.subscribe(function (result) {
            if (result instanceof kendo_angular_dialog_1.DialogCloseResult) {
                _this.setFocus.emit();
            }
        });
        this.dialogRef.content.instance.setFocus.subscribe(function (res) {
            debugger;
            //this.dialogRef.dialog.instance.focus();
        });
    };
    VoucherLineComponent.prototype.selectionKey = function (context) {
        if (context.dataItem == undefined)
            return "";
        return context.dataItem.id + " " + context.index;
    };
    VoucherLineComponent.prototype.onSelectedKeysChange = function (checkedState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    };
    VoucherLineComponent.prototype.filterChange = function (filter) {
        var isReload = false;
        if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
            isReload = true;
        this.currentFilter = this.getFilters(filter);
        if (isReload) {
            this.reloadGrid();
        }
    };
    VoucherLineComponent.prototype.sortChange = function (sort) {
        this.sort = sort.filter(function (f) { return f.dir != undefined; });
        this.reloadGrid();
    };
    VoucherLineComponent.prototype.removeHandler = function (arg) {
        this.prepareDeleteConfirm(arg.dataItem.name);
        this.deleteModelId = arg.dataItem.id;
        this.deleteConfirm = true;
    };
    VoucherLineComponent.prototype.pageChange = function (event) {
        this.skip = event.skip;
        this.reloadGrid();
    };
    VoucherLineComponent.prototype.editHandler = function (arg) {
        var _this = this;
        this.grid.loading = true;
        this.voucherLineService.getById(source_1.String.Format(index_2.VoucherApi.VoucherArticle, arg.dataItem.id)).subscribe(function (res) {
            _this.editDataItem = res;
            _this.openEditorDialog(false);
            _this.grid.loading = false;
        });
    };
    VoucherLineComponent.prototype.saveHandler = function (viewModel, isNew) {
        var _this = this;
        this.isNewBalance = false;
        //this.balance = this.debitSum - this.creditSum;
        var model = viewModel.model;
        var isOpen = viewModel.isOpen;
        model.branchId = this.voucherModel.branchId;
        model.fiscalPeriodId = this.voucherModel.fiscalPeriodId;
        model.voucherId = this.voucherModel.id;
        this.grid.loading = true;
        if (!isNew) {
            this.voucherLineService.edit(source_1.String.Format(index_2.VoucherApi.VoucherArticle, model.id), model)
                .subscribe(function (response) {
                _this.editDataItem = undefined;
                _this.showMessage(_this.updateMsg, environment_1.MessageType.Succes);
                _this.dialogRef.close();
                _this.dialogModel.parent = undefined;
                _this.dialogModel.errorMessage = undefined;
                _this.dialogModel.model = undefined;
                _this.reloadGrid();
                if (isOpen) {
                    setTimeout(function () {
                        _this.addNew();
                    });
                }
            }, (function (error) {
                _this.editDataItem = model;
                _this.dialogModel.errorMessage = error;
                _this.grid.loading = false;
            }));
        }
        else {
            this.voucherLineService.insert(source_1.String.Format(index_2.VoucherApi.VoucherArticles, this.voucherId), model)
                .subscribe(function (response) {
                _this.editDataItem = undefined;
                _this.showMessage(_this.insertMsg, environment_1.MessageType.Succes);
                var insertedModel = response;
                _this.dialogRef.close();
                _this.dialogModel.parent = undefined;
                _this.dialogModel.errorMessage = undefined;
                _this.dialogModel.model = undefined;
                _this.reloadGrid(insertedModel);
                if (isOpen) {
                    setTimeout(function () {
                        _this.addNew();
                    });
                }
            }, (function (error) {
                _this.dialogModel.errorMessage = error;
                _this.grid.loading = false;
            }));
        }
    };
    //#endregion
    //#region Methods
    VoucherLineComponent.prototype.deleteModels = function () {
        //    this.transactionLineService.deleteTransactions(this.selectedRows).subscribe(res => {
        //        this.showMessage(this.deleteMsg, MessageType.Info);
        //        this.selectedRows = [];
        //        this.reloadGrid();
        //    }, (error => {
        //        this.showMessage(error, MessageType.Warning);
        //    }));
    };
    VoucherLineComponent.prototype.reloadGridEvent = function () {
        this.reloadGrid();
    };
    VoucherLineComponent.prototype.reloadGrid = function (insertedModel) {
        var _this = this;
        this.grid.loading = true;
        var filter = this.currentFilter;
        if (this.totalRecords == this.skip && this.totalRecords != 0) {
            this.skip = this.skip - this.pageSize;
        }
        if (insertedModel)
            this.goToLastPage(this.totalRecords);
        this.voucherLineService.getAll(source_1.String.Format(index_2.VoucherApi.VoucherArticles, this.voucherId), this.pageIndex, this.pageSize, this.sort, filter).subscribe(function (res) {
            var resData = res.body;
            _this.properties = resData.properties;
            var totalCount = 0;
            if (res.headers != null) {
                var headers = res.headers != undefined ? res.headers : null;
                if (headers != null) {
                    var retheader = headers.get('X-Total-Count');
                    if (retheader != null)
                        totalCount = parseInt(retheader.toString());
                }
            }
            _this.rowData = {
                data: resData,
                total: totalCount
            };
            _this.showloadingMessage = !(resData.length == 0);
            _this.totalRecords = totalCount;
        });
        this.voucherLineService.getVoucherInfo(this.voucherId).subscribe(function (res) {
            _this.debitSum = res.debitSum;
            _this.creditSum = res.creditSum;
            _this.balance = _this.debitSum - _this.creditSum;
            _this.grid.loading = false;
        });
    };
    VoucherLineComponent.prototype.showReport = function () {
        /*
         var url = String.Format(ReportApi.DefaultSystemReport, this.viewer.baseId);
   
         this.reporingService.getAll(url).subscribe((res: Response) => {
             
           var report :Report = <any>res.body;
           var serviceUrl = environment.BaseUrl + "/" + report.serviceUrl;
           //add voucher filter to filters
           var filter = this.addFilter(this.currentFilter,"Id",this.voucherId.toString(),"=={0}")
   
           this.reporingService.getAll(serviceUrl,
             this.sort, filter).subscribe((response: any) => {
     
                const m = moment();
                var dateStr : string;
                m.locale('fa');
                if (m.isValid())
                   dateStr = m.format('jYYYY/jMM/jDD');
     
               var reportData = {rows : response.body , currentDate: dateStr};
               this.viewer.showVoucherStdFormReport(report ,reportData);
             });
           
         });
   
         */
    };
    VoucherLineComponent.prototype.getVoucher = function () {
        var _this = this;
        this.voucherLineService.getById(source_1.String.Format(index_2.VoucherApi.Voucher, this.voucherId)).subscribe(function (res) {
            _this.voucherModel = res;
        });
    };
    VoucherLineComponent.prototype.deleteModel = function (confirm) {
        var _this = this;
        if (confirm) {
            this.grid.loading = true;
            this.voucherLineService.delete(source_1.String.Format(index_2.VoucherApi.VoucherArticle, this.deleteModelId)).subscribe(function (response) {
                _this.deleteModelId = 0;
                _this.showMessage(_this.deleteMsg, environment_1.MessageType.Info);
                if (_this.rowData.data.length == 1 && _this.pageIndex > 1)
                    _this.pageIndex = ((_this.pageIndex - 1) * _this.pageSize) - _this.pageSize;
                _this.reloadGrid();
            }, (function (error) {
                _this.grid.loading = false;
                var message = error.message ? error.message : error;
                _this.showMessage(message, environment_1.MessageType.Warning);
            }));
        }
        //hide confirm dialog
        this.deleteConfirm = false;
    };
    VoucherLineComponent.prototype.addNew = function () {
        this.editDataItem = new index_1.VoucherLineInfo();
        this.openEditorDialog(true);
    };
    VoucherLineComponent.prototype.addNewWithBalance = function () {
        this.isNewBalance = true;
        this.addNew();
    };
    __decorate([
        core_1.ViewChild(kendo_angular_grid_1.GridComponent)
    ], VoucherLineComponent.prototype, "grid", void 0);
    __decorate([
        core_1.ViewChild(reportViewer_component_1.ReportViewerComponent)
    ], VoucherLineComponent.prototype, "viewer", void 0);
    __decorate([
        core_1.Input()
    ], VoucherLineComponent.prototype, "voucherId", void 0);
    __decorate([
        core_1.Input()
    ], VoucherLineComponent.prototype, "documentStatus", void 0);
    __decorate([
        core_1.Output()
    ], VoucherLineComponent.prototype, "setFocus", void 0);
    VoucherLineComponent = __decorate([
        core_1.Component({
            selector: 'voucherLine',
            templateUrl: './voucherLine.component.html',
            styles: ["/deep/ .panel-primary { border-color: #989898; }\n.voucher-balance{text-align: center; display: block; }\n.voucher-balance > .color-red { color: red; } .voucher-balance > .color-green { color: green; }\n.voucher-balance > .balance-value { direction: ltr; display: inline-block; }\n.detail-info { margin:5px 0; } .detail-info > span { padding-left: 15px; }\n.nowrap { white-space: nowrap; overflow: hidden; text-overflow: ellipsis; width: 450px; display: block; }\n/deep/.k-footer-template { background-color: #b3b3b3; }\n"]
        })
    ], VoucherLineComponent);
    return VoucherLineComponent;
}(default_component_1.DefaultComponent));
exports.VoucherLineComponent = VoucherLineComponent;
//# sourceMappingURL=voucherLine.component.js.map