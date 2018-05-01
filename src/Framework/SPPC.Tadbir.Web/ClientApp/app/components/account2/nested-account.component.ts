import { Component, OnInit, Input, Renderer2 } from '@angular/core';

import { AccountService, AccountInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Account, TransactionLine } from '../../model/index';

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
import { GridResult } from '../../service/account.service';
import { Account2Component } from './account2.component';

export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'nested-account',
    templateUrl: './nested-account.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class NestedAccountComponent extends DefaultComponent implements OnInit {

    
    @Input() public parentId : number;

    @Input() public level: number;

    ngOnInit() {

        //this.reloadGrid();    
        this.reloadGrid();
    }

    public show: boolean = true;

    //#region My Region

    public rowData: GridDataResult;

    public selectedRows: string[] = [];
    public accountArticleRows: any[];

    public fiscalPeriodRows: any[];

    public totalRecords: number;

    //#endregion

    public fpId: number;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteAccountsConfirm: boolean;
    deleteAccountId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newAccount: boolean;
    account: Account = new AccountInfo


    editDataItem?: Account = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;


    

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private accountService: AccountService, private transactionLineService: TransactionLineService,
        private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.Account, Metadatas.Account);      

    }

    

    selectionKey(context: RowArgs): string {

        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteAccountsConfirm = true;
    }

    deleteAccounts(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.accountService.deleteAccounts(this.selectedRows).subscribe(res => {
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
        this.deleteAccountsConfirm = false;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }


    reloadGrid(insertedAccount?: Account) {

        this.sppcLoading.show();

        var filter = this.currentFilter;
        var order = this.currentOrder;

        if (this.totalRecords == this.skip) {
            this.skip = this.skip - this.pageSize;
        }

        if (this.parentId && this.parentId > 0) {
            filter.push(new Filter("ParentId", this.parentId.toString(), "== {0}","System.Int32"))
        }        

        this.accountService.search(this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            this.properties = resData.metadata.properties;
            var totalCount = 0;


            if (insertedAccount) {
                var rows = (resData.list as Array<Account>);
                var index = rows.findIndex(p => p.id == insertedAccount.id);
                if (index >= 0) {
                    resData.list.splice(index, 1);
                    rows.splice(0, 0, insertedAccount);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.list.splice(this.pageSize - 1, 1);
                    }

                    rows.splice(0, 0, insertedAccount);

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

            this.show = totalCount > 0;

            this.showloadingMessage = !(resData.list.length == 0);
            this.totalRecords = totalCount;
            this.sppcLoading.hide();

        })


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

    

    /**
     * این متد برای حذف حساب بکار میرود
     * @param confirm در صورتی که مفدار صحیح داشته باشد اکانت حذف میشود
     */
    deleteAccount(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.accountService.delete(this.deleteAccountId).subscribe(response => {
                this.deleteAccountId = 0;
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

        this.deleteAccountId = arg.dataItem.id;
        this.deleteConfirm = true;
    }



    onFiscalPeriodChange(arg: any) {

    }

    //account form events
    public editHandler(arg: any) {

        this.sppcLoading.show();
        this.accountService.getAccountById(arg.dataItem.id).subscribe(res => {
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

    public addNew() {
        this.isNew = true;
        this.editDataItem = new AccountInfo();
        this.errorMessage = '';
    }

    public saveHandler(account: Account) {

        account.branchId = this.BranchId;
        account.fiscalPeriodId = this.FiscalPeriodId;
        //TODO: این کد بعدا باید تغییر پیدا کند البته با اقای اسلامیه هماهنگ شده است 
        account.fullCode = account.code;

        this.sppcLoading.show();

        if (!this.isNew) {
            this.isNew = false;
            this.accountService.editAccount(account)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = account;
                    this.errorMessage = error;

                }));
        }
        else {
            this.accountService.insertAccount(account)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedAccount = JSON.parse(response._body);
                    this.reloadGrid(insertedAccount);

                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));

        }

        this.sppcLoading.hide();
    }

}


