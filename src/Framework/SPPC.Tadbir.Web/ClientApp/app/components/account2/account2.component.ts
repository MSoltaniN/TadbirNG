﻿import { Component, OnInit, Input, Renderer2 } from '@angular/core';

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

import { State, CompositeFilterDescriptor  } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";

import { MessageType, Layout, Entities, Metadatas } from "../../enviroment";
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { GridResult } from '../../service/account.service';

export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}  


@Component({
    selector: 'account2',
    templateUrl: './account2.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class Account2Component extends DefaultComponent implements OnInit {

    public rowData: GridDataResult;
    
    public selectedRows: string[] = [];
    public accountArticleRows: any[];
    
    public fiscalPeriodRows: any[];

    public totalRecords: number;

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
    

    editDataItem ? : Account = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    
    ngOnInit() {
        this.getFiscalPeriod();
        
        this.reloadGrid();    
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private accountService: AccountService, private transactionLineService: TransactionLineService,
        private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2, public metadata: MetaDataService)
    {
        super(toastrService, translate, renderer, metadata, Entities.Account, Metadatas.Account);
        
        this.getFiscalPeriod();

        this.reloadGrid();
        
    }
    
    getRowsCount() {

        return this.accountService.getCount(this.currentOrder, this.currentFilter).map(response => <any>(<Response>response).json());

    }

    selectionKey(context: RowArgs): string {        

        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteAccountsConfirm = true;
    }

    deleteAccounts(confirm : boolean)
    {       
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
    

    reloadGrid(insertedAccount ?: Account) {

        this.sppcLoading.show();

    var filter = this.currentFilter;
    var order = this.currentOrder;

    if (this.totalRecords == this.skip) {
        this.skip = this.skip - this.pageSize;                
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
                               
                

        this.showloadingMessage = !(resData.list.length == 0);
        this.totalRecords = totalCount;
        this.sppcLoading.hide();
                
    })
           

    }
    
    

    dataStateChange(state: DataStateChangeEvent): void {
        this.currentFilter = this.getFilters(state.filter);
        if(state.sort)
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
    lazyProjectLoad(account: any) {
        this.sppcLoading.show();
        this.transactionLineService.getAccountArticles(account.data.id).subscribe(res => {
            this.accountArticleRows = res;
            //this.accountArticleRows.set(account.data.accountId, res);
            this.sppcLoading.hide();
            if (res.length == 0)
                this.showloadingMessage = !(res.length == 0);
        });
    }

    
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


    /* load fiscal periods */
    getFiscalPeriod() {
        this.sppcLoading.show();
        this.showloadingMessage = true;
        
        this.fiscalPeriodService.getFiscalPeriod(this.CompanyId).subscribe(res => {
            this.fiscalPeriodRows = res;
            this.showloadingMessage = !(res.length == 0);
            this.sppcLoading.hide();
        });
    }

    onFiscalPeriodChange(arg: any) {

    }
    
    //account form events
    public editHandler(arg: any) {
        this.editDataItem = arg.dataItem;
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

