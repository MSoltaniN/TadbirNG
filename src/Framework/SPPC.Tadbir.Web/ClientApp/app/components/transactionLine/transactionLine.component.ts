import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { TransactionService, TransactionLineInfo, TransactionLineService, FiscalPeriodService } from '../../service/index';
import { TransactionLine } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';

import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Entities, Metadatas } from "../../enviroment";
import { Filter } from "../../class/filter";
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';


@Component({
    selector: 'transactionLine',
    templateUrl: './transactionLine.component.html',
    styles: ["/deep/ .panel-primary { border-color: #989898; }"]
})


export class TransactionLineComponent extends DefaultComponent implements OnInit {

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    public debitSum: number;
    public creditSum: number;

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
    errorMessage: string;
    groupDelete: boolean = false;

    @Input() transactionId: number;


    ngOnInit() {
        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private transactionLineService: TransactionLineService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.TransactionLine, Metadatas.TransactionArticles);
    }

    getRowsCount() {
        return this.transactionLineService.getCount(this.transactionId, this.currentOrder, this.currentFilter).map(response => <any>(<Response>response).json());
    }

    selectionKey(context: RowArgs): string {

        return context.dataItem.id + " " + context.index;
        //return context.dataItem.id;
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

    reloadGrid(insertedTransactionLine?: TransactionLine) {

        this.sppcLoading.show();

        var filter = this.currentFilter;
        var order = this.currentOrder;

        if (this.totalRecords == this.skip && this.totalRecords != 0) {
            this.skip = this.skip - this.pageSize;
        }

        this.transactionLineService.search(this.transactionId, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            this.properties = resData.metadata.properties;
            var totalCount = 0;


            if (insertedTransactionLine) {
                var rows = (resData.list as Array<TransactionLine>);
                var index = rows.findIndex(p => p.id == insertedTransactionLine.id);
                if (index >= 0) {
                    resData.list.splice(index, 1);
                    rows.splice(0, 0, insertedTransactionLine);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.list.splice(this.pageSize - 1, 1);
                    }

                    rows.splice(0, 0, insertedTransactionLine);
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
            

        })

        this.transactionLineService.getTransactionInfo(this.transactionId).subscribe(res => {
            this.debitSum = res.item.debitSum;
            this.creditSum = res.item.creditSum;

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
        //this.reloadGrid();
    }


    deleteTransactionLine(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.transactionLineService.delete(this.deleteTransactionLineId).subscribe(response => {
                this.deleteTransactionLineId = 0;
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

        this.deleteTransactionLineId = arg.dataItem.id;
        this.deleteConfirm = true;
    }


    //transaction form events
    public editHandler(arg: any) {

        this.sppcLoading.show();

        this.transactionLineService.getTransactionLineById(arg.dataItem.id).subscribe(res => {
            this.editDataItem = res.item;

            this.sppcLoading.hide();
        })
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
        this.errorMessage = '';
        var article = new TransactionLineInfo();
        article.transactionId = this.transactionId;
        this.editDataItem = article;
    }

    public saveHandler(transactionLine: TransactionLine) {

        transactionLine.branchId = this.BranchId;
        transactionLine.fiscalPeriodId = this.FiscalPeriodId;
        this.sppcLoading.show();
        if (!this.isNew) {

            this.isNew = false;

            this.transactionLineService.editTransactionLine(transactionLine)
                .subscribe(response => {

                    this.editDataItem = undefined;

                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();

                }, (error => {
                    this.editDataItem = transactionLine;
                    this.errorMessage = error;

                }));
        }
        else {
            this.transactionLineService.insertTransactionLine(this.transactionId, transactionLine)
                .subscribe((response: any) => {

                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedTransactionLine = JSON.parse(response._body);
                    this.reloadGrid(insertedTransactionLine);

                }, (error => {

                    this.isNew = true;
                    this.errorMessage = error;

                }));
        }
        this.sppcLoading.hide();
    }

}


