import { Component, OnInit, Input, Renderer2 } from '@angular/core';

import { TransactionService, TransactionLineInfo, TransactionLineService, FiscalPeriodService } from '../../service/index';

import { TransactionLine } from '../../model/index';

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
import { MessageType } from "../../enviroment";
import { Filter } from "../../class/filter";


@Component({
    selector: 'transactionLine',
    templateUrl: './transactionLine.component.html',
    styles: ["/deep/ .panel-primary { border-color: #989898; }"]
})


export class TransactionLineComponent extends DefaultComponent implements OnInit {

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteTransactionLineId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newTransactionLine: boolean;
    transactionLine: TransactionLine = new TransactionLineInfo;


    editDataItem?: TransactionLine = undefined;
    isNew: boolean;

    groupDelete: boolean = false;

    @Input() transactionId: number;


    ngOnInit() {
        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService,
        private transactionLineService: TransactionLineService, public renderer: Renderer2) {
        super(toastrService, translate, renderer, "TransactionLine");
    }

    getRowsCount() {
        return this.transactionLineService.getCount(this.transactionId, this.currentOrder, this.currentFilter).map(response => <any>(<Response>response).json());
    }

    selectionKey(context: RowArgs): string {

        //return context.dataItem.id + " " + context.index;
        return context.dataItem.id;
    }

    //deleteTransactionsLine() {
    //    this.transactionLineService.deleteTransactions(this.selectedRows).subscribe(res => {
    //        this.showMessage(this.deleteMsg, MessageType.Info);
    //        this.selectedRows = [];
    //        this.reloadGrid();
    //    }, (error => {
    //        this.showMessage(error, MessageType.Warning);
    //    }));
    //}

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid() {

        this.transactionLineService.getCount(this.transactionId, this.currentOrder, this.currentFilter).finally(() => {
            var filter = this.currentFilter;
            var order = this.currentOrder;
            this.transactionLineService.search(this.transactionId, this.pageIndex, this.pageSize, order, filter).subscribe(res => {
                this.rowData = {
                    data: res.lines,
                    total: this.totalRecords
                }
                this.showloadingMessage = !(res.lines.length == 0);
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


    deleteTransactionLine(confirm: boolean) {
        if (confirm) {
            this.transactionLineService.delete(this.deleteTransactionLineId).subscribe(response => {
                this.deleteTransactionLineId = 0;
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

        this.deleteTransactionLineId = arg.dataItem.id;
        this.deleteConfirm = true;
    }


    //transaction form events
    public editHandler(arg: any) {
        this.editDataItem = arg.dataItem;
        this.isNew = false;
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.isNew = false;
    }

    public addNew() {
        this.isNew = true;
        var article = new TransactionLineInfo();
        article.transactionId = this.transactionId;
        this.editDataItem = article;
    }

    public saveHandler(transactionLine: TransactionLine) {
        if (!this.isNew) {
            this.transactionLineService.editTransactionLine(transactionLine)
                .subscribe(response => {
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.showMessage(error, MessageType.Warning);
                }));
        }
        else {
            this.transactionLineService.insertTransactionLine(this.transactionId,transactionLine)
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


