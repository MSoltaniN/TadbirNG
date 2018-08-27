//#region Imports

import { Component, OnInit, Input, Renderer2, ViewChildren, QueryList, ElementRef, Host, Output, SkipSelf, Optional, ViewChild } from '@angular/core';

import { AccountService, AccountInfo, VoucherLineService, FiscalPeriodService } from '../../service/index';

import { Account } from '../../model/index';

import { ToastrService } from 'ngx-toastr'; /** add this component for message in client side */

import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';




import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';

import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { MessageType, Layout, Entities, Metadatas } from "../../enviroment";
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { AccountApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { AccountPermissions } from '../../security/permissions';
import { DefaultComponent } from '../../class/default.component';
import { FilterExpression } from '../../class/filterExpression';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';

//#endregion

export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'account',
    templateUrl: './account.component.html',
    styles: [`
    .accInfoTitle {
        padding-right: 0px;
        padding-left: 0px;
    }
  `],
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class AccountComponent extends DefaultComponent implements OnInit {

    //#region Fields

    public Childrens: Array<AccountComponent>;

    @ViewChild(GridComponent) grid: GridComponent;

    @Input() public parent: Account;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;
    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public accountArticleRows: any[];
    public totalRecords: number;

    //permission flag
    viewAccess: boolean;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteModelsConfirm: boolean;
    deleteModelId: number;

    currentFilter: FilterExpression;
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    editDataItem?: Account = undefined;
    isNew: boolean;
    disableSaveBtn: boolean | undefined;
    errorMessage: string;
    groupDelete: boolean = false;
    addToContainer: boolean = false;

    parentTitle: string = '';
    parentValue: string = '';

    componentParentId: number;
    //#endregion

    //#region Events
    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.Account, AccountPermissions.View);
        this.reloadGrid();
        if (this.parentAccount) {
            this.parentAccount.addChildAccount(this);
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
        this.reloadGrid();
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

    goToLastPage() {
        var pageCount: number = 0;
        pageCount = Math.floor(this.totalRecords / this.pageSize);

        if (this.totalRecords % this.pageSize == 0) {
            this.skip = (pageCount * this.pageSize) - this.pageSize;
            return;
        }
        this.skip = (pageCount * this.pageSize)
    }

    //account form events
    public editHandler(arg: any) {
        this.grid.loading = true;
        this.accountService.getById(String.Format(AccountApi.Account, arg.dataItem.id)).subscribe(res => {
            this.editDataItem = res;
            this.setAccountTitle(res.parentId);

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

    public saveHandler(model: Account) {
        // debugger;
        model.branchId = this.BranchId;
        model.fiscalPeriodId = this.FiscalPeriodId;
        //TODO: این کد بعدا باید تغییر پیدا کند البته با اقای اسلامیه هماهنگ شده است
        //model.fullCode = model.code;
        this.grid.loading = true;
        if (!this.isNew) {
            this.isNew = false;
            this.disableSaveBtn = undefined;
            this.accountService.edit<Account>(String.Format(AccountApi.Account, model.id), model)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = model;
                    this.errorMessage = error;
                    this.disableSaveBtn = false;
                }));
        }
        else {
            //set parentid for childs accounts
            if (this.parentId) {
                model.parentId = this.parentId;

                //var currentLevel = this.parent ? this.parent.level : 0;
                var parentAc = this.parentAccount;
                var currentLevel = 0;

                while (parentAc) {
                    currentLevel++;
                    parentAc = parentAc.parentAccount
                }

                model.level = currentLevel + 1;

                this.parentId = undefined;
            }
            else if (this.parent) {
                model.parentId = this.parent.id;
                model.level = this.parent.level + 1;
            }
            //model.level = this.parent.level + 1;

            //set parentid for childs accounts
            this.disableSaveBtn = undefined;
            this.accountService.insert<Account>(AccountApi.Accounts, model)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedModel = JSON.parse(response._body);

                    if (this.Childrens) {
                        var childFiltered = this.Childrens.filter(f => f.parent.id == model.parentId);
                        if (childFiltered.length > 0) {
                            childFiltered[0].reloadGrid(insertedModel);
                            //childFiltered[0].skip = Math.round(childFiltered[0].rowData.total / childFiltered[0].pageSize);
                            //this.grid.selectable = true;
                            //var selIds: Array<string> = [(childFiltered[0].rowData.total - 1).toString()]
                            //childFiltered[0].selectedRows = selIds;
                            return;
                        }
                    }
                    /*
                    var rows = (this.rowData.data as Array<Account>);
                    var index = rows.findIndex(p => p.id == insertedModel.parentId);
                    if (index >= 0) {
                        this.grid.expandRow(index);
                        if (this.Childrens) {
                            var childFiltered = this.Childrens.filter(f => f.parent.id == model.parentId);
                            if (childFiltered.length > 0) {
                                childFiltered[0].reloadGrid(insertedModel);
                                return;
                            }
                        }
                    }
                    */
                    //if (model.parentId == undefined || this.addToContainer) {
                    //    this.reloadGrid(insertedModel);
                    //    this.addToContainer = false;
                    //}
                    //else if (model.parentId != undefined) {
                    //    this.reloadGrid();
                    //}

                    this.reloadGrid(insertedModel);

                }, (error => {
                    this.isNew = true;
                    this.disableSaveBtn = false;
                    this.errorMessage = error;
                }));

        }
        this.grid.loading = false;

    }

    //#endregion

    //#region Constructor

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private accountService: AccountService, private voucherLineService: VoucherLineService,
        private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2, public metadata: MetaDataService,
        @SkipSelf() @Host() @Optional() private parentAccount: AccountComponent) {
        super(toastrService, translate, renderer, metadata, Entities.Account, Metadatas.Account);
    }

    //#endregion

    //#region Methods

    /**
     * کامپوننت های فرزند را در متغیری اضافه میکند
     * @param accountComponent کامپوننت حساب
     */
    public addChildAccount(accountComponent: AccountComponent) {

        if (this.Childrens == undefined) this.Childrens = new Array<AccountComponent>();
        if (this.Childrens.findIndex(p => p.parent.id === accountComponent.parent.id) == -1)
            //if (this.Childrens.some(p => p === accountComponent) == false)
            this.Childrens.push(accountComponent);


    }

    showConfirm() {
        this.deleteModelsConfirm = true;
    }

    deleteModels(confirm: boolean) {
        if (confirm) {
            this.grid.loading = true;
            this.accountService.groupDelete(AccountApi.Accounts, this.selectedRows).subscribe(res => {
                this.showMessage(this.deleteMsg, MessageType.Info);
                this.selectedRows = [];
                this.reloadGrid();
                //this.groupDelete = false;
                return;
            }, (error => {
                this.grid.loading = false;
                this.showMessage(error, MessageType.Warning);
            }));
        }

        //this.groupDelete = false;
        this.deleteModelsConfirm = false;
    }

    public reloadGrid(insertedModel?: Account) {
        if (this.viewAccess) {
            this.grid.loading = true;
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }
            if (insertedModel)
                this.goToLastPage();

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



            this.accountService.getAll(String.Format(AccountApi.FiscalPeriodBranchAccounts, this.FiscalPeriodId, this.BranchId), this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
                var resData = res.json();
                //this.properties = resData.properties;
                var totalCount = 0;
                //if (insertedModel && this.addToContainer) {
                //    var rows = (resData as Array<Account>);
                //    var index = rows.findIndex(p => p.id == insertedModel.id);
                //    if (index >= 0) {
                //        rows.splice(index, 1);
                //        rows.splice(0, 0, insertedModel);
                //    }
                //    else {
                //        if (rows.length == this.pageSize) {
                //            rows.splice(this.pageSize - 1, 1);
                //        }
                //        rows.splice(0, 0, insertedModel);
                //    }

                //    resData = rows;
                //}
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
                    var rows = (this.rowData.data as Array<Account>);
                    var index = rows.findIndex(p => p.id == insertedModel.parentId);
                    if (index == -1 && this.parentAccount != null) {
                        var rows = (this.parentAccount.rowData.data as Array<Account>);
                        var index = rows.findIndex(p => p.id == insertedModel.parentId);
                        if (index >= 0) {
                            this.parentAccount.grid.expandRow(this.parentAccount.skip + index);
                        }
                    }
                    else if (index >= 0) {
                        this.grid.expandRow(this.skip + index);
                    }
                }

                //زمانی که تعداد رکورد ها صفر باشد باید کامپوننت پدر رفرش شود
                if (totalCount == 0) {
                    if (this.parentAccount && this.parentAccount.Childrens) {
                        var thisIndex = this.parentAccount.Childrens.findIndex(p => p == this);
                        if (thisIndex >= 0)
                            this.parentAccount.Childrens.splice(thisIndex);


                        this.parentAccount.reloadGrid();
                    }

                }

                this.showloadingMessage = !(resData.length == 0);
                this.totalRecords = totalCount;
                this.grid.loading = false;
            })
        }
        else {
            this.rowData = {
                data: [],
                total: 0
            }
        }
    }



    deleteModel(confirm: boolean) {
        if (confirm) {
            this.grid.loading = true;
            this.accountService.delete(String.Format(AccountApi.Account, this.deleteModelId)).subscribe(response => {
                this.deleteModelId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
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



    private setAccountTitle(parentModelId?: number) {
        if (parentModelId != undefined) {

            var parentRow = null;
            var findIndex = this.rowData.data.findIndex(acc => acc.id == parentModelId);

            if (findIndex == -1) {
                findIndex = this.parentAccount.rowData.data.findIndex(acc => acc.id == parentModelId);
                if (findIndex >= 0)
                    parentRow = this.parentAccount.rowData.data[findIndex];
            }
            else
                parentRow = this.rowData.data[findIndex];

            if (parentRow != null) {
                //var parentRow = this.rowData.data[findIndex];

                var prefix = "";

                if (parentRow.level == 0)
                    prefix = this.getText("Account.Kol");

                if (parentRow.level == 1)
                    prefix = this.getText("Account.Moeen");

                if (parentRow.level == 2)
                    prefix = this.getText("Account.Tafzili");

                if (parentRow.level > 2)
                    prefix = this.getText("Account.TafziliToUp") + " " + (parentRow.level - 2);

                this.parentTitle = prefix;
                this.parentValue = parentRow.name;
            }
        }
        else if (this.parent != undefined) {

            var prefix = "";

            if (this.parent.level == 0)
                prefix = this.getText("Account.Kol");

            if (this.parent.level == 1)
                prefix = this.getText("Account.Moeen");

            if (this.parent.level == 2)
                prefix = this.getText("Account.Tafzili");

            if (this.parent.level > 2)
                prefix = this.getText("Account.TafziliToUp") + " " + (this.parent.level - 2);


            this.parentTitle = prefix;
            this.parentValue = this.parent.name;
        }
        else
            this.parentTitle = '';
    }

    public addNew(parentModelId?: number, addToThis?: boolean) {

        //if (parentModelId) {
        //    var rows = (this.rowData.data as Array<Account>);
        //    var index = rows.findIndex(p => p.id == parentModelId);
        //    if (index >= 0) {
        //        this.grid.expandRow(index);
        //    }
        //}





        this.isNew = true;
        this.editDataItem = new AccountInfo();
        this.setAccountTitle(parentModelId);
        //آی دی مربوط به حساب سطح بالاتر برای درج در زیر حساب ها در متغیر parentId مقدار دهی میشود
        if (parentModelId)
            this.parentId = parentModelId;

        if (addToThis)
            this.addToContainer = addToThis;

        this.errorMessage = '';
    }

    public showOnlyParent(dataItem: Account, index: number): boolean {
        return dataItem.childCount > 0;
    }

    public checkShow(dataItem: Account) {
        return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
    }

    //#endregion

}