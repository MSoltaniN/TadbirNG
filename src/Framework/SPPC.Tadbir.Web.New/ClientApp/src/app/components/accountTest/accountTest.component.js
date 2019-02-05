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
//#region Imports
var core_1 = require("@angular/core");
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var default_component_1 = require("../../class/default.component");
var service_1 = require("../../service");
var api_1 = require("../../service/api");
var source_1 = require("../../class/source");
var kendo_angular_grid_1 = require("@progress/kendo-angular-grid");
var secureEntity_1 = require("../../security/secureEntity");
var permissions_1 = require("../../security/permissions");
var filter_1 = require("../../class/filter");
var filterExpressionOperator_1 = require("../../class/filterExpressionOperator");
var of_1 = require("rxjs/observable/of");
var accountTest_form_component_1 = require("./accountTest-form.component");
//#endregion
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var AccountTestComponent = /** @class */ (function (_super) {
    __extends(AccountTestComponent, _super);
    function AccountTestComponent(toastrService, translate, accountService, dialogService, renderer, metadata, settingService) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, settingService, environment_1.Entities.Account, environment_1.Metadatas.Account) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.accountService = accountService;
        _this.dialogService = dialogService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        _this.firstTreeNode = [];
        //treeNodes: Array<AccountItemBrief> = [{ id: 0, name: 'حسابهای کل', code: '', fullCode: '', level: 0, childCount: 0, parentId: null, isSelected: true }];
        _this.treeNodes = [];
        _this.expandedKeys = [-1];
        _this.selectedKeys = [-1];
        _this.selectedRows = [];
        _this.groupDelete = false;
        _this.showloadingMessage = true;
        //مشخص میکند که آیتم ها، فرزند دارند یا خیر
        _this.hasChildren = function (item) {
            if (item.childCount > 0 || item.id == -1) {
                return true;
            }
            return false;
        };
        _this.fetchChildren = function (dataItem) {
            if (dataItem.id == -1) {
                return of_1.of(_this.treeNodes.filter(function (f) { return f.parentId == null; }));
            }
            else {
                return _this.accountService.getModels(source_1.String.Format(api_1.AccountApi.AccountChildren, dataItem.id));
            }
        };
        return _this;
    }
    //#endregion
    AccountTestComponent.prototype.ngOnInit = function () {
        this.viewAccess = this.isAccess(secureEntity_1.SecureEntity.Account, permissions_1.AccountPermissions.View);
        this.getTreeNode();
        this.reloadGrid();
    };
    //test() {
    //  this.treeNodes.push({ id: 1000, name: 'testiiiiiiiiii', code: '', fullCode: '', level: 0, childCount: 0, parentId: null, isSelected: true });
    //  this.expandedKeys = [];
    //  setTimeout(() => {
    //    this.expandedKeys.push(-1);
    //  });
    //}
    AccountTestComponent.prototype.getTreeNode = function () {
        var _this = this;
        this.accountService.getModels(api_1.AccountApi.EnvironmentAccountsLedger).subscribe(function (res) {
            _this.firstTreeNode = [{ id: -1, name: 'حسابهای کل', code: '', fullCode: '', level: 0, childCount: 1, parentId: -1, isSelected: true }];
            _this.selectedItem = _this.firstTreeNode[0];
            _this.treeNodes = res;
        });
    };
    AccountTestComponent.prototype.handleSelection = function (item) {
        this.selectedItem = item.dataItem;
        this.parentId = this.selectedItem && this.selectedItem.id > 0 ? this.selectedItem.id : undefined;
        this.currentFilter = undefined;
        this.selectedRows = [];
        this.pageIndex = 0;
        this.getParent();
        this.reloadGrid();
    };
    //getModelChildren() {
    //  this.accountService.getAll(String.Format(AccountApi.AccountChildren, this.selectedItem.id)).subscribe(res => {
    //    var resData = res.body;
    //  })
    //}
    //#region grid
    AccountTestComponent.prototype.reloadGrid = function (insertedModel) {
        var _this = this;
        if (this.viewAccess) {
            this.grid.loading = true;
            var filter = this.currentFilter;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }
            if (insertedModel)
                this.goToLastPage(this.totalRecords);
            var parent_Id = this.parentId ? this.parentId.toString() : "null";
            filter = this.addFilterToFilterExpression(this.currentFilter, new filter_1.Filter("ParentId", parent_Id, "== {0}", "System.Int32"), filterExpressionOperator_1.FilterExpressionOperator.And);
            this.accountService.getAll(api_1.AccountApi.EnvironmentAccounts, this.pageIndex, this.pageSize, this.sort, filter).subscribe(function (res) {
                var resData = res.body;
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
                _this.grid.loading = false;
            });
        }
        else {
            this.rowData = {
                data: [],
                total: 0
            };
        }
    };
    AccountTestComponent.prototype.getParent = function () {
        var _this = this;
        if (this.parentId) {
            this.accountService.getAccountById(this.parentId).subscribe(function (res) {
                _this.parent = res;
            });
        }
        else {
            this.parent = undefined;
        }
    };
    AccountTestComponent.prototype.pageChange = function (event) {
        this.skip = event.skip;
        this.reloadGrid();
    };
    AccountTestComponent.prototype.sortChange = function (sort) {
        this.sort = sort.filter(function (f) { return f.dir != undefined; });
        this.reloadGrid();
    };
    AccountTestComponent.prototype.filterChange = function (filter) {
        var isReload = false;
        if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
            isReload = true;
        this.currentFilter = this.getFilters(filter);
        if (isReload) {
            this.reloadGrid();
        }
    };
    AccountTestComponent.prototype.selectionKey = function (context) {
        if (context.dataItem == undefined)
            return "";
        return context.dataItem.id;
    };
    AccountTestComponent.prototype.onSelectedKeysChange = function (checkedState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    };
    /**
    * باز کردن و مقداردهی اولیه به فرم ویرایشگر
    */
    AccountTestComponent.prototype.openEditorDialog = function (isNew) {
        var _this = this;
        this.dialogRef = this.dialogService.open({
            title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
            content: accountTest_form_component_1.AccountTestFormComponent,
        });
        this.dialogModel = this.dialogRef.content.instance;
        this.dialogModel.parent = this.parent;
        this.dialogModel.model = this.editDataItem;
        this.dialogModel.isNew = isNew;
        this.dialogModel.errorMessage = undefined;
        this.dialogRef.content.instance.save.subscribe(function (res) {
            _this.saveHandler(res, isNew);
        });
        var closeForm = this.dialogRef.content.instance.cancel.subscribe(function (res) {
            _this.dialogRef.close();
        });
    };
    AccountTestComponent.prototype.addNew = function () {
        this.editDataItem = new service_1.AccountInfo();
        this.openEditorDialog(true);
    };
    AccountTestComponent.prototype.editHandler = function (arg) {
        var _this = this;
        var recordId = this.selectedRows[0];
        this.grid.loading = true;
        this.accountService.getById(source_1.String.Format(api_1.AccountApi.Account, recordId)).subscribe(function (res) {
            _this.editDataItem = res;
            _this.openEditorDialog(false);
            _this.grid.loading = false;
        });
    };
    AccountTestComponent.prototype.saveHandler = function (model, isNew) {
        var _this = this;
        this.grid.loading = true;
        if (!isNew) {
            this.accountService.edit(source_1.String.Format(api_1.AccountApi.Account, model.id), model)
                .subscribe(function (response) {
                _this.editDataItem = undefined;
                _this.showMessage(_this.updateMsg, environment_1.MessageType.Succes);
                _this.dialogRef.close();
                _this.dialogModel.parent = undefined;
                _this.dialogModel.errorMessage = undefined;
                _this.dialogModel.model = undefined;
                _this.reloadGrid();
            }, (function (error) {
                _this.editDataItem = model;
                _this.dialogModel.errorMessage = error;
            }));
        }
        else {
            this.accountService.insert(api_1.AccountApi.EnvironmentAccounts, model)
                .subscribe(function (response) {
                _this.editDataItem = undefined;
                _this.showMessage(_this.insertMsg, environment_1.MessageType.Succes);
                var insertedModel = response;
                //if (this.Childrens) {
                //  var childFiltered = this.Childrens.filter(f => f.parent.id == model.parentId);
                //  if (childFiltered.length > 0) {
                //    childFiltered[0].reloadGrid(insertedModel);
                //  }
                //}
                _this.dialogRef.close();
                _this.dialogModel.parent = undefined;
                _this.dialogModel.errorMessage = undefined;
                _this.dialogModel.model = undefined;
                _this.reloadGrid(insertedModel);
            }, (function (error) {
                _this.dialogModel.errorMessage = error;
            }));
        }
        this.grid.loading = false;
    };
    __decorate([
        core_1.ViewChild(kendo_angular_grid_1.GridComponent)
    ], AccountTestComponent.prototype, "grid", void 0);
    AccountTestComponent = __decorate([
        core_1.Component({
            selector: 'accountTest',
            templateUrl: './accountTest.component.html',
            styles: ["\n\n  "],
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], AccountTestComponent);
    return AccountTestComponent;
}(default_component_1.DefaultComponent));
exports.AccountTestComponent = AccountTestComponent;
//# sourceMappingURL=accountTest.component.js.map