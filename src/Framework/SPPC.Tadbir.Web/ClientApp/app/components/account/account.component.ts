import { Component, OnInit, Input, Renderer2 } from '@angular/core';

import { AccountService, AccountInfo, VoucherLineService, FiscalPeriodService } from '../../service/index';

import { Account } from '../../model/index';

import { ToastrService } from 'ngx-toastr'; /** add this component for message in client side */

import {
    GridDataResult,
    DataStateChangeEvent,
    PageChangeEvent,
    RowArgs,
    SelectAllCheckboxState
} from '@progress/kendo-angular-grid';




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
import { AccountApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { AccountPermissions } from '../../security/permissions';

export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'account',
    templateUrl: './account.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class AccountComponent extends DefaultComponent implements OnInit {

    @Input() public parent: Account;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;
    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public accountArticleRows: any[];
    public totalRecords: number;

    //permission flag
    viewAccess: boolean;
    insertAccess: boolean;
    editAccess: boolean;
    deleteAccess: boolean;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteModelsConfirm: boolean;
    deleteModelId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    editDataItem?: Account = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;


    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.Account, AccountPermissions.View);
        this.insertAccess = this.isAccess(SecureEntity.Account, AccountPermissions.Create);
        this.editAccess = this.isAccess(SecureEntity.Account, AccountPermissions.Edit);
        this.deleteAccess = this.isAccess(SecureEntity.Account, AccountPermissions.Delete);

        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private accountService: AccountService, private voucherLineService: VoucherLineService,
        private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.Account, Metadatas.Account);
    }


    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteModelsConfirm = true;
    }

    deleteModels(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.accountService.groupDelete(AccountApi.Accounts, this.selectedRows).subscribe(res => {
                this.showMessage(this.deleteMsg, MessageType.Info);
                this.selectedRows = [];
                this.reloadGrid();
                this.groupDelete = false;
            }, (error => {
                this.sppcLoading.hide();
                this.showMessage(error, MessageType.Warning);
            }));
        }

        this.groupDelete = false;
        this.deleteModelsConfirm = false;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid(insertedModel?: Account) {
        if (this.viewAccess) {
            this.sppcLoading.show();
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip) {
                this.skip = this.skip - this.pageSize;
            }
            if (this.parent) {
                if (this.parent.childCount > 0)
                    filter.push(new Filter("ParentId", this.parent.id.toString(), "== {0}", "System.Int32"))
            }
            else
                filter.push(new Filter("ParentId", "null", "== {0}", "System.Int32"))
            this.accountService.getAll(String.Format(AccountApi.FiscalPeriodBranchAccounts, this.FiscalPeriodId, this.BranchId), this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
                var resData = res.json();
                this.properties = resData.metadata.properties;
                var totalCount = 0;
                if (insertedModel) {
                    var rows = (resData.list as Array<Account>);
                    var index = rows.findIndex(p => p.id == insertedModel.id);
                    if (index >= 0) {
                        resData.list.splice(index, 1);
                        rows.splice(0, 0, insertedModel);
                    }
                    else {
                        if (rows.length == this.pageSize) {
                            resData.list.splice(this.pageSize - 1, 1);
                        }
                        rows.splice(0, 0, insertedModel);
                    }
                }
                if (res.headers != null) {
                    var headers = res.headers != undefined ? res.headers : null;
                    if (headers != null) {
                        var retheader = headers.get('X-Total-Count');
                        if (retheader != null)
                            totalCount = parseInt(retheader.toString());
                    }
                }
                this.rowData = {
                    data: resData.list,
                    total: totalCount
                }
                this.showloadingMessage = !(resData.list.length == 0);
                this.totalRecords = totalCount;
                this.sppcLoading.hide();
            })
        }
        else {
            this.rowData = {
                data: [],
                total: 0
            }
        }
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

    pageChange(event: PageChangeEvent): void {
        this.skip = event.skip;
        this.reloadGrid();
    }

    /* lazy loading for account articles */
    lazyProjectLoad(model: any) {
        this.sppcLoading.show();
        this.voucherLineService.getAccountArticles(model.data.id).subscribe(res => {
            this.accountArticleRows = res;
            //this.accountArticleRows.set(account.data.accountId, res);
            this.sppcLoading.hide();
            if (res.length == 0)
                this.showloadingMessage = !(res.length == 0);
        });
    }

    deleteModel(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.accountService.delete(String.Format(AccountApi.Account, this.deleteModelId)).subscribe(response => {
                this.deleteModelId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
                this.reloadGrid();
            }, (error => {
                this.sppcLoading.hide();
                this.showMessage(error, MessageType.Warning);
            }));
        }
        //hide confirm dialog
        this.deleteConfirm = false;
    }

    removeHandler(arg: any) {
        this.prepareDeleteConfirm(arg.dataItem.name);
        this.deleteModelId = arg.dataItem.id;
        this.deleteConfirm = true;
    }

    //account form events
    public editHandler(arg: any) {
        this.sppcLoading.show();
        this.accountService.getById(String.Format(AccountApi.Account, arg.dataItem.id)).subscribe(res => {
            this.editDataItem = res.item;
            this.sppcLoading.hide();
        })
        this.isNew = false;
        this.errorMessage = '';
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.errorMessage = '';
    }

    public addNew(parentModelId?: number) {
        this.isNew = true;
        this.editDataItem = new AccountInfo();

        //آی دی مربوط به حساب سطح بالاتر برای درج در زیر حساب ها در متغیر parentId مقدار دهی میشود
        if (parentModelId)
            this.parentId = parentModelId;

        this.errorMessage = '';
    }

    public saveHandler(model: Account) {
        model.branchId = this.BranchId;
        model.fiscalPeriodId = this.FiscalPeriodId;
        //TODO: این کد بعدا باید تغییر پیدا کند البته با اقای اسلامیه هماهنگ شده است
        model.fullCode = model.code;
        this.sppcLoading.show();
        if (!this.isNew) {
            this.isNew = false;
            this.accountService.edit<Account>(String.Format(AccountApi.Account, model.id), model)
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
                this.parentId = undefined;
            }
            else if (this.parent)
                model.parentId = this.parent.id;
            //set parentid for childs accounts
            this.accountService.insert<Account>(AccountApi.Accounts, model)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedModel = JSON.parse(response._body);
                    this.reloadGrid(insertedModel);
                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));
        }
        this.sppcLoading.hide();
    }

    public showOnlyParent(dataItem: Account, index: number): boolean {
        return dataItem.childCount > 0;
    }

    public checkShow(dataItem: Account) {
        return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
    }

}


