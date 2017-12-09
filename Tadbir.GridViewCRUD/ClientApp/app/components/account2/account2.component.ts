import { Component, OnInit, Input } from '@angular/core';

import { AccountService, AccountInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Account, TransactionLine } from '../../model/index';

import { ToastrService, ToastConfig } from 'toastr-ng2'; /** add this component for message in client side */

import {
    GridDataResult,
    DataStateChangeEvent,
    PageChangeEvent,
    RowArgs
} from '@progress/kendo-angular-grid';

import { Filter } from '../../class/filter';

import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';

import { State, CompositeFilterDescriptor  } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';

declare var jquery: any;
declare var $: any;




@Component({
    selector: 'account2',
    templateUrl: './account2.component.html'
})


export class Account2Component implements OnInit {

    public rowData: GridDataResult;
    
    private selectedRows: string[] = [];
    public accountArticleRows: any[];


    public fiscalPeriodRows: any[];

    public totalRecords: number;

    public fpId: number;

    //for add in delete messageText
    deleteConfirmMsg: string;
    deleteConfirm: boolean;
    deleteAccountId: number;
    
    config: ToastConfig;


    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newAccount: boolean;
    account: Account = new AccountInfo

    updateMsg: string;
    insertMsg: string;
    deleteMsg: string;

    rtlClass: string = "ui-rtl";
    rtlUse: string = "rtl";

    private translateService: TranslateService

    private pageSize: number = 10;
    private skip: number = 0;

    public state: State = {
        skip: 0,
        take: 5,
        // Initial filter descriptor
        filter: {
            logic: "and",
            filters: [{ field: "code", operator: "contains", value: "" }]
        }
    };


    private editDataItem ? : Account = undefined;
    private isNew: boolean;

    ngOnInit() {


        ////how to call jquery in angular 4
        /*
        $(document).ready(function () {
            $('p-datatable table').addClass('table');

            $('p-datatable table th:first').attr('data-breakpoints' , 'xs sm md');
            
            $('p-datatable table').footable()
            //alert($('p-datatable > table').length);
        });
        */

    }


    constructor(private accountService: AccountService, private transactionLineService: TransactionLineService, private fiscalPeriodService: FiscalPeriodService,
        private toastrService: ToastrService, private translate: TranslateService) {
        translate.addLangs(["en", "fa"]);
        translate.setDefaultLang('fa');

        var browserLang = 'fa';//translate.getBrowserLang();
        translate.use(browserLang);

        this.translateService = translate;

        this.localizeMsg();

        this.getFiscalPeriod();

        this.reloadGrid();
    }

    languageChange(value: string) {
        this.translateService.use(value);
        this.localizeMsg();
        switch (value) {
            case "fa":
                {
                    this.rtlUse = "rtl";
                    this.rtlClass = "ui-rtl"
                    break;
                }
            case "en":
                {
                    this.rtlUse = "ltr";
                    this.rtlClass = ""
                    break;
                }
        }


    }
    

    getRowsCount() {


        this.accountService.getCount(this.currentOrder, this.currentFilter).subscribe(res => {
            this.totalRecords = res;
        });

    }

    private selectionKey(context: RowArgs): string {
        return context.dataItem.accountId + " " + context.index;
    }

    reloadGrid() {

        this.getRowsCount();

        var filter = this.currentFilter;
        var order = this.currentOrder;

        this.accountService.search(this.skip, this.pageSize, order, filter).subscribe(res => {
            //this.rowData = res;
            this.rowData = {
                data: res,
                total: this.totalRecords
            }

            this.showloadingMessage = !(res.length == 0);
        });

    }

    getFilters(filter: any): Filter[] {
        let filters: Filter[] = [];

        if (filter.filters.length) {
            for (let i = 0; i < filter.filters.length; i++)
            {
                if (filter.filters[i].value != "")
                {
                    filters.push(new Filter(filter.filters[i].field, filter.filters[i].value, filter.filters[i].operator))

                }
            }
              
        }

        return filters;
    }

    protected dataStateChange(state: DataStateChangeEvent): void {
        this.currentFilter = this.getFilters(state.filter);
        if(state.sort)
        if (state.sort.length > 0)
            this.currentOrder = state.sort[0].field + " " + state.sort[0].dir;

        this.skip = state.skip;
        this.reloadGrid();
    }


    public sortChange(sort: SortDescriptor[]): void {
        if (sort)
            this.currentOrder = sort[0].field + " " + sort[0].dir;

        this.reloadGrid();
    }


    protected pageChange(event: PageChangeEvent): void {
        this.skip = event.skip;
        this.reloadGrid();
    }
    
    /* lazy loading for account articles */
    lazyProjectLoad(account: any) {
        this.transactionLineService.getAccountArticles(account.data.accountId).subscribe(res => {
            this.accountArticleRows = res;
            //this.accountArticleRows.set(account.data.accountId, res);

            if (res.length == 0)
                this.showloadingMessage = !(res.length == 0);
        });
    }

    localizeMsg() {
        // read message format for crud operations
        var entityType = '';
        this.translateService.get("Entity.Account").subscribe((msg: string) => {
            entityType = msg;
        });

        this.translateService.get("Messages.Inserted").subscribe((msg: string) => {
            this.insertMsg = String.Format(msg, entityType);
        });

        this.translateService.get("Messages.Updated").subscribe((msg: string) => {
            this.updateMsg = String.Format(msg, entityType);;
        });

        this.translateService.get("Messages.Deleted").subscribe((msg: string) => {
            this.deleteMsg = String.Format(msg, entityType);;
        });
    }

    deleteAccount(confirm: boolean) {
        if (confirm) {
            this.accountService.delete(this.deleteAccountId).subscribe(response => {
                this.deleteAccountId = 0;
                this.toastrService.info(this.deleteMsg, '', { positionClass: 'toast-top-left' });
                this.reloadGrid();
            });
        }

        //hide confirm dialog
        this.deleteConfirm = false;
    }

    protected removeHandler(arg: any) {

        this.translateService.get("Messages.DeleteConfirm").subscribe((msg: string) => {
            this.deleteConfirmMsg = String.Format(msg, arg.dataItem.name);
        });

        this.deleteAccountId = arg.dataItem.accountId;
        this.deleteConfirm = true;
    }


    /* load fiscal periods */
    getFiscalPeriod() {
        this.showloadingMessage = true;
        this.fiscalPeriodService.getFiscalPeriods().subscribe(res => {
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

    public saveHandler(account: Account) {

        if (!this.isNew) {
            this.accountService.editAccount(account)
                .subscribe(response => {
                    this.toastrService.success(this.updateMsg, '', { positionClass: 'toast-top-left' });
                    this.reloadGrid();
                });            
        }
        else {
            this.accountService.insertAccount(account)
                .subscribe(response => {
                    this.toastrService.success(this.insertMsg, '', { positionClass: 'toast-top-left' });
                    this.reloadGrid();
                });
            
        }

        this.editDataItem = undefined;
    }

    //

}


