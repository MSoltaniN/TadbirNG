﻿import { Component, OnInit, Input, Renderer2, ViewChild, SkipSelf, Host, Optional } from '@angular/core';
import { DetailAccountService, DetailAccountInfo } from '../../service/index';
import { DetailAccount } from '../../model/index';
import { ToastrService } from 'ngx-toastr';

import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';

import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';

import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";

import { MessageType, Layout, Entities, Metadatas } from "../../enviroment";
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { DetailAccountApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { DetailAccountPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'detailAccount',
    templateUrl: './detailAccount.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class DetailAccountComponent extends DefaultComponent implements OnInit {
    public filterChange(filter: CompositeFilterDescriptor): void {
        throw new Error("Method not implemented.");
    }

    //#region Fields
    public Childrens: Array<DetailAccountComponent>;

    @ViewChild(GridComponent) grid: GridComponent;

    @Input() public parent: DetailAccount;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    //permission flag
    viewAccess: boolean;

    ////for add in delete messageText
    deleteConfirm: boolean;
    deleteModelsConfirm: boolean;
    deleteModelId: number;

    currentFilter: FilterExpression;
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    editDataItem?: DetailAccount = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;
    showFilterBtn: boolean = false;
    addToContainer: boolean = false;

    parentTitle: string = '';
    parentValue: string = '';

    isChildExpanding: boolean;
    componentParentId: number;
    goLastPage: boolean;
    //#endregion

    //#region Events
    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.DetailAccount, DetailAccountPermissions.View);
        if (this.parentComponent && this.parentComponent.isChildExpanding) {
            this.goLastPage = true;
            this.parentComponent.isChildExpanding = false;
        }

        this.reloadGrid();
        if (this.parentComponent) {
            this.parentComponent.addChildComponent(this);
            this.parentId = this.parent.id;
            this.componentParentId = this.parentId;
        }
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    dataStateChange(state: DataStateChangeEvent): void {
        this.currentFilter = this.getFilters(state.filter);

        if (state.sort)
            if (state.sort.length > 0)
                this.currentOrder = state.sort[0].field + " " + state.sort[0].dir;
        this.state = state;
        this.skip = state.skip;

        if (this.currentFilter)
            this.showFilterBtn = true;
        else {
            this.showFilterBtn = false;
            this.reloadGrid();
        }
    }

    public sortChange(sort: SortDescriptor[]): void {
        if (sort)
            this.currentOrder = sort[0].field + " " + sort[0].dir;

        this.reloadGrid();
    }

    removeHandler(arg: any) {
        this.prepareDeleteConfirm(arg.dataItem.name);
        this.deleteModelId = arg.dataItem.id;
        this.deleteConfirm = true;
    }

    pageChange(event: PageChangeEvent): void {
        this.skip = event.skip;
        this.reloadGrid();
    }


    public editHandler(arg: any) {
        this.grid.loading = true;
        this.detailAccountService.getById(String.Format(DetailAccountApi.DetailAccount, arg.dataItem.id)).subscribe(res => {
            this.editDataItem = res;
            this.setTitle(res.parentId);

            this.parentId = res.parentId;

            this.grid.loading = false;
        })
        this.isNew = false;
        this.errorMessage = '';
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.errorMessage = '';

        this.parentId = this.componentParentId;
    }

    public saveHandler(model: DetailAccount) {
        model.branchId = this.BranchId;
        model.fiscalPeriodId = this.FiscalPeriodId;
        //this.sppcLoading.show();
        if (!this.isNew) {
            this.isNew = false;
            this.detailAccountService.edit<DetailAccount>(String.Format(DetailAccountApi.DetailAccount, model.id), model)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = model;
                    this.errorMessage = error;
                }));
        }
        else {
            //set parentid for childs accounts
            if (this.parentId) {
                model.parentId = this.parentId;

                //var currentLevel = this.parent ? this.parent.level : 0;
                var parentCom = this.parentComponent;
                var currentLevel = 0;

                while (parentCom) {
                    currentLevel++;
                    parentCom = parentCom.parentComponent
                }

                model.level = currentLevel + 1;

                this.parentId = undefined;
            }
            else if (this.parent) {
                model.parentId = this.parent.id;
                model.level = this.parent.level + 1;
            }

            this.detailAccountService.insert<DetailAccount>(DetailAccountApi.DetailAccounts, model)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedModel = response;

                    if (this.Childrens) {
                        var childFiltered = this.Childrens.filter(f => f.parent.id == model.parentId);
                        if (childFiltered.length > 0) {
                            childFiltered[0].reloadGrid(insertedModel);
                            return;
                        }
                    }
                    this.reloadGrid(insertedModel);
                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));
        }
        //this.sppcLoading.hide();
    }

    //#endregion

    //#region Constructor
    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private detailAccountService: DetailAccountService, public renderer: Renderer2, public metadata: MetaDataService,
        @SkipSelf() @Host() @Optional() private parentComponent: DetailAccountComponent) {
        super(toastrService, translate, renderer, metadata, Entities.DetailAccount, Metadatas.DetailAccount);
    }
    //#endregion


    //#region Methods

    /**
    * کامپوننت های فرزند را در متغیری اضافه میکند
    * @param detailAccountComponent کامپوننت تفصیلی شناور
    */
    public addChildComponent(detailAccountComponent: DetailAccountComponent) {

        if (this.Childrens == undefined) this.Childrens = new Array<DetailAccountComponent>();
        if (this.Childrens.findIndex(p => p.parent.id === detailAccountComponent.parent.id) == -1)
            this.Childrens.push(detailAccountComponent);
    }

    showConfirm() {
        this.deleteModelsConfirm = true;
    }

    deleteModels(confirm: boolean) {
        if (confirm) {
            //this.sppcLoading.show();
            //this.accountService.deleteAccounts(this.selectedRows).subscribe(res => {
            //    this.showMessage(this.deleteMsg, MessageType.Info);
            //    this.selectedRows = [];
            //    this.reloadGrid();
            //    this.groupDelete = false;
            //}, (error => {
            //    //this.sppcLoading.hide();
            //    this.showMessage(error, MessageType.Warning);
            //}));
        }

        this.groupDelete = false;
        this.deleteModelsConfirm = false;
    }

    public reloadGrid(insertedModel?: DetailAccount) {
        if (this.viewAccess) {
            this.grid.loading = true;
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }

            if (this.parent) {
                if (this.parent.childCount > 0)
                    filter = this.addFilterToFilterExpression(this.currentFilter,
                        new Filter("ParentId", this.parent.id.toString(), "== {0}", "System.Int32"),
                        FilterExpressionOperator.And);
            }
            else
                filter = this.addFilterToFilterExpression(this.currentFilter,
                    new Filter("ParentId", "null", "== {0}", "System.Int32"),
                    FilterExpressionOperator.And);

            //#region load inner grid
            if (this.parentComponent != null && (this.goLastPage || (insertedModel && !this.addToContainer))) {

                //call top 1 account for get totalcount
                this.detailAccountService.getAll(String.Format(DetailAccountApi.FiscalPeriodBranchDetailAccounts, this.FiscalPeriodId, this.BranchId),
                    0, 1, order, filter).subscribe((res) => {
                        if (res.headers != null) {
                            var headers = res.headers != undefined ? res.headers : null;
                            if (headers != null) {
                                var retheader = headers.get('X-Total-Count');
                                if (retheader != null)
                                    this.totalRecords = parseInt(retheader.toString());
                            }
                        }

                        this.goToLastPage(this.totalRecords);
                        this.goLastPage = false;

                        this.loadGridData(insertedModel, order, filter);
                    });
            }
            //#endregion
            else {
                if (insertedModel && this.addToContainer)
                    this.goToLastPage(this.totalRecords);

                this.loadGridData(insertedModel, order, filter);
            }
        }
        else {
            this.rowData = {
                data: [],
                total: 0
            }
        }
    }

    loadGridData(insertedModel?: DetailAccount, order?: string, filter?: FilterExpression) {

        this.detailAccountService.getAll(String.Format(DetailAccountApi.FiscalPeriodBranchDetailAccounts, this.FiscalPeriodId, this.BranchId),
            this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
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

                this.rowData = {
                    data: resData,
                    total: totalCount
                }

                this.grid.data = this.rowData;


                //expand new row if has childs
                if (insertedModel) {
                    var rows = (this.rowData.data as Array<DetailAccount>);
                    var index = rows.findIndex(p => p.id == insertedModel.parentId);
                    if (index == -1 && this.parentComponent != null) {
                        var rows = (this.parentComponent.rowData.data as Array<DetailAccount>);
                        var index = rows.findIndex(p => p.id == insertedModel.parentId);
                        if (index >= 0) {
                            this.parentComponent.isChildExpanding = true;
                            this.parentComponent.grid.collapseRow(this.parentComponent.skip + index);
                            this.parentComponent.grid.expandRow(this.parentComponent.skip + index);
                        }
                    }
                    else if (index >= 0) {
                        this.isChildExpanding = true;
                        this.grid.collapseRow(this.skip + index);
                        this.grid.expandRow(this.skip + index);
                    }
                }

                //زمانی که تعداد رکورد ها صفر باشد باید کامپوننت پدر رفرش شود
                if (totalCount == 0) {
                    if (this.parentComponent && this.parentComponent.Childrens) {
                        var thisIndex = this.parentComponent.Childrens.findIndex(p => p == this);
                        if (thisIndex >= 0)
                            this.parentComponent.Childrens.splice(thisIndex);


                        this.parentComponent.reloadGrid();
                    }

                }

                this.showloadingMessage = !(resData.length == 0);
                this.totalRecords = totalCount;
                this.grid.loading = false;
            })
    }

    deleteModel(confirm: boolean) {
        if (confirm) {
            this.grid.loading = true;
            this.detailAccountService.delete(String.Format(DetailAccountApi.DetailAccount, this.deleteModelId)).subscribe(response => {
                this.deleteModelId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
                if (this.rowData.data.length == 1 && this.pageIndex > 1)
                    this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

                this.reloadGrid();
            }, (error => {
                    this.grid.loading = false;
                    var message = error.message ? error.message : error;
                    this.showMessage(message, MessageType.Warning);
            }));
        }

        //hide confirm dialog
        this.deleteConfirm = false;
    }

    private setTitle(parentModelId?: number) {
        if (parentModelId != undefined) {

            var parentRow = null;
            var findIndex = this.rowData.data.findIndex(acc => acc.id == parentModelId);

            if (findIndex == -1) {
                findIndex = this.parentComponent.rowData.data.findIndex(acc => acc.id == parentModelId);
                if (findIndex >= 0)
                    parentRow = this.parentComponent.rowData.data[findIndex];
            }
            else
                parentRow = this.rowData.data[findIndex];

            if (parentRow != null) {
                var level = +parentRow.level;
                this.parentTitle = this.getText("App.Level") + " " + (level + 2).toString();
                this.parentValue = parentRow.name;
            }
        }
        else if (this.parent != undefined) {
            this.parentTitle = this.getText("App.Level") + " " + (this.parent.level + 2).toString();
            this.parentValue = this.parent.name;
        }
        else {
            this.parentTitle = '';
            this.parentValue = '';
        }

    }

    public addNew(parentModelId?: number, addToThis?: boolean) {

        this.isNew = true;
        this.editDataItem = new DetailAccountInfo();
        this.setTitle(parentModelId);

        if (parentModelId)
            this.parentId = parentModelId;

        if (addToThis)
            this.addToContainer = addToThis;
        else
            this.addToContainer = false;

        this.errorMessage = '';
    }

    filterRowData() {
        this.reloadGrid();
    }

    clearFilterRowData() {
        this.state.filter = undefined;
        this.currentFilter = new FilterExpression();
        this.showFilterBtn = false;
        this.reloadGrid();
    }

    public showOnlyParent(dataItem: DetailAccount, index: number): boolean {
        return dataItem.childCount > 0;
    }

    public checkShow(dataItem: DetailAccount) {
        return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
    }

    //#endregion
}


