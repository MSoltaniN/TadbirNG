import { Component, OnInit, Input, Renderer2 } from '@angular/core';

import { TransactionService, TransactionInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Transaction, TransactionLine } from '../../model/index';

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

import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout } from "../../enviroment";
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}  

@Component({
    selector: 'transaction',
    templateUrl: './transaction.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class TransactionComponent extends DefaultComponent implements OnInit {

    public rowData: GridDataResult;

    public selectedRows: string[] = [];
    //public accountArticleRows: any[];

    public fiscalPeriodRows: any[];

    public totalRecords: number;

    public fpId: number;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteTransactionId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newTransaction: boolean;
    transaction: Transaction = new TransactionInfo


    editDataItem?: Transaction = undefined;
    isNew: boolean;
    groupDelete: boolean = false;

    ngOnInit() {
        //this.getFiscalPeriod();

        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService,
        private transactionService: TransactionService, private transactionLineService: TransactionLineService,
        private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2) {
        super(toastrService, translate,renderer,"Transaction");

    }

    getRowsCount() {
        return this.transactionService.getCount(this.currentOrder, this.currentFilter).map(response => <any>(<Response>response).json());

    }

    selectionKey(context: RowArgs): string {

        //return context.dataItem.id + " " + context.index;
        return context.dataItem.id ;
    }

    deleteTransactions() {
        this.transactionService.deleteTransactions(this.selectedRows).subscribe(res => {
            this.showMessage(this.deleteMsg, MessageType.Info);
            this.selectedRows = [];
            this.reloadGrid();
        }, (error => {
            this.showMessage(error, MessageType.Warning);
        }));
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid() {
        this.transactionService.getCount(this.currentOrder, this.currentFilter).finally(() => {
            var filter = this.currentFilter;
            var order = this.currentOrder;

            this.transactionService.search(this.pageIndex, this.pageSize, order, filter).subscribe(res => {
                //this.rowData = res;
                this.rowData = {
                    data: res,
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
    //lazyProjectLoad(account: any) {
    //    this.transactionLineService.getAccountArticles(account.data.id).subscribe(res => {
    //        this.accountArticleRows = res;
    //        //this.accountArticleRows.set(account.data.accountId, res);

    //        if (res.length == 0)
    //            this.showloadingMessage = !(res.length == 0);
    //    });
    //}


    deleteTransaction(confirm: boolean) {
        if (confirm) {
            this.transactionService.delete(this.deleteTransactionId).subscribe(response => {
                this.deleteTransactionId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
                this.reloadGrid();
            }, (error => {
                this.showMessage(error, MessageType.Warning);
            }));
        }

        //hide confirm dialog
        this.deleteConfirm = false;
    }

    removeHandler(arg: any) {

        this.prepareDeleteConfirm(arg.dataItem.name);

        this.deleteTransactionId = arg.dataItem.id;
        this.deleteConfirm = true;
    }


    ///* load fiscal periods */
    //getFiscalPeriod() {
    //    this.showloadingMessage = true;
    //    this.fiscalPeriodService.getFiscalPeriods().subscribe(res => {
    //        this.fiscalPeriodRows = res;
    //        this.showloadingMessage = !(res.length == 0);
    //    });
    //}

    //onFiscalPeriodChange(arg: any) {

    //}

    //transaction form events
    public editHandler(arg: any) {
        this.editDataItem = arg.dataItem;
        this.isNew = false;
    }

    public cancelHandler() {
        this.editDataItem = undefined;
    }

    public addNew() {
        this.isNew = true;
        this.editDataItem = new TransactionInfo();
    }

    public saveHandler(transaction: Transaction) {
        if (!this.isNew) {
            this.transactionService.editTransaction(transaction)
                .subscribe(response => {
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.showMessage(error, MessageType.Warning);
                }));
        }
        else {
            this.transactionService.insertTransaction(transaction)
                .subscribe(response => {
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.showMessage(error, MessageType.Warning);
                }));

        }

        this.editDataItem = undefined;
        this.isNew = false;
    }

}


