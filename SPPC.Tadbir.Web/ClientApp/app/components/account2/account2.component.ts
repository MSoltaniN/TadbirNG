import { Component, OnInit, Input, Renderer2 } from '@angular/core';

import { AccountService, AccountInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Account, TransactionLine } from '../../model/index';

import { ToastrService, ToastConfig } from 'toastr-ng2'; /** add this component for message in client side */

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

import { MessageType, Layout } from "../../enviroment";
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';

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
    deleteAccountId: number;
    
    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newAccount: boolean;
    account: Account = new AccountInfo
       

    editDataItem ? : Account = undefined;
    isNew: boolean;
    groupDelete: boolean = false;
    
    ngOnInit() {
        this.getFiscalPeriod();
        
        this.reloadGrid();    
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService,
        private accountService: AccountService, private transactionLineService: TransactionLineService,
        private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2, public metadata: MetaDataService)
    {
        super(toastrService, translate, renderer, metadata,'Account');
        
        this.getFiscalPeriod();

        this.reloadGrid();
        
    }
    
    getRowsCount() {

        return this.accountService.getCount(this.currentOrder, this.currentFilter).map(response => <any>(<Response>response).json());

    }

    selectionKey(context: RowArgs): string {        

        return context.dataItem.id + " " + context.index;
    }

    deleteAccounts()
    {
        this.accountService.deleteAccounts(this.selectedRows).subscribe(res => {            
            this.showMessage(this.deleteMsg, MessageType.Info);
            this.selectedRows = [];
            this.reloadGrid();            
        });
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)        
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid() {

        


        this.accountService.getCount(this.currentOrder, this.currentFilter).finally(() => {
            var filter = this.currentFilter;
            var order = this.currentOrder;

            this.accountService.search(this.pageIndex, this.pageSize, order, filter).subscribe(res => {
                //this.rowData = res;
                this.properties = res.metadata.properties;

                this.rowData = {
                    data: res.list,
                    total: this.totalRecords                   
                }

                this.showloadingMessage = !(res.length == 0);
                
            })
        }).subscribe(res => {
            this.totalRecords = res;
        });       

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
        this.transactionLineService.getAccountArticles(account.data.id).subscribe(res => {
            this.accountArticleRows = res;
            //this.accountArticleRows.set(account.data.accountId, res);

            if (res.length == 0)
                this.showloadingMessage = !(res.length == 0);
        });
    }

    
    deleteAccount(confirm: boolean) {
        if (confirm) {
            this.accountService.delete(this.deleteAccountId).subscribe(response => {
                this.deleteAccountId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
                this.reloadGrid();
            });
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
        this.showloadingMessage = true;
        
        this.fiscalPeriodService.getFiscalPeriod(this.CompanyId).subscribe(res => {
            this.fiscalPeriodRows = res;
            this.showloadingMessage = !(res.length == 0);
        });
    }

    onFiscalPeriodChange(arg: any) {

    }
    
    //account form events
    public editHandler(arg: any) {
        this.editDataItem = arg.dataItem;
        this.isNew = false;
    }    

    public cancelHandler() {
        this.editDataItem = undefined;
    }

    public addNew() {
        this.isNew = true;
        this.editDataItem = new AccountInfo();        
    }    

    public saveHandler(account: Account) {

        account.branchId = this.BranchId;
        account.fiscalPeriodId = this.FiscalPeriodId;

        if (!this.isNew) {
            this.accountService.editAccount(account)
                .subscribe(response => {
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                });            
        }
        else {
            this.accountService.insertAccount(account)
                .subscribe(response => {
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    this.reloadGrid();
                });
            
        }

        this.editDataItem = undefined;
        this.isNew = false;
    }
    
}


