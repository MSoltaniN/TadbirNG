﻿import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { TransactionService, TransactionInfo, TransactionLineInfo, FiscalPeriodService } from '../../service/index';
import { Transaction, TransactionLine } from '../../model/index';
import { ToastrService, ToastConfig } from 'toastr-ng2'; /** add this component for message in client side */
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';

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
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';


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
    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {
        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private transactionService: TransactionService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, 'Transaction');

    }

    getRowsCount() {
        return this.transactionService.getCount(this.currentOrder, this.currentFilter).map(response => <any>(<Response>response).json());
    }

    selectionKey(context: RowArgs): string {

        return context.dataItem.id + " " + context.index;
        //return context.dataItem.id;
    }

    deleteTransactions() {
        this.sppcLoading.show();
        this.transactionService.deleteTransactions(this.selectedRows).subscribe(res => {
            this.showMessage(this.deleteMsg, MessageType.Info);
            this.selectedRows = [];
            this.reloadGrid();
        }, (error => {
            this.sppcLoading.hide();
            this.showMessage(error, MessageType.Warning);
        }));
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid(insertedTransaction?: Transaction) {

        this.sppcLoading.show();

        var filter = this.currentFilter;
        var order = this.currentOrder;

        if (this.totalRecords == this.skip && this.totalRecords != 0) {
            this.skip = this.skip - this.pageSize;
        }

        this.transactionService.search(this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            this.properties = resData.metadata.properties;
            var totalCount = 0;


            if (insertedTransaction) {
                var rows = (resData.list as Array<Transaction>);
                var index = rows.findIndex(p => p.id == insertedTransaction.id);
                if (index >= 0) {
                    resData.list.splice(index, 1);
                    rows.splice(0, 0, insertedTransaction);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.list.splice(this.pageSize - 1, 1);
                    }

                    rows.splice(0, 0, insertedTransaction);
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

    deleteTransaction(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.transactionService.delete(this.deleteTransactionId).subscribe(response => {
                this.deleteTransactionId = 0;
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

        this.deleteTransactionId = arg.dataItem.id;
        this.deleteConfirm = true;
    }


    public editHandler(arg: any) {

        this.editDataItem = arg.dataItem;
        this.isNew = false;
        this.errorMessage = '';
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.isNew = false;
        this.errorMessage = '';
    }

    public addNew() {
        this.isNew = true;
        this.editDataItem = new TransactionInfo();
        this.errorMessage = '';
    }

    public saveHandler(transaction: Transaction) {

        transaction.branchId = this.BranchId;
        transaction.fiscalPeriodId = this.FiscalPeriodId;

        this.sppcLoading.show();
        if (!this.isNew) {
            this.transactionService.editTransaction(transaction)
                .subscribe(response => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.errorMessage = error;

                }));
        }
        else {
            this.transactionService.insertTransaction(transaction)
                .subscribe((response: any) => {

                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedTransaction = JSON.parse(response._body);
                    this.reloadGrid(insertedTransaction);

                }, (error => {

                    this.isNew = true;
                    this.errorMessage = error;

                }));
        }
        this.sppcLoading.hide();
    }

}


