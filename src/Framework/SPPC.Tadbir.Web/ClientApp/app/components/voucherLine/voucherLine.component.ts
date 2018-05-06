import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { VoucherLineViewModelInfo, VoucherLineService, FiscalPeriodService } from '../../service/index';
import { VoucherLineViewModel } from '../../model/index';
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
    selector: 'voucherLine',
    templateUrl: './voucherLine.component.html',
    styles: ["/deep/ .panel-primary { border-color: #989898; }"]
})


export class VoucherLineComponent extends DefaultComponent implements OnInit {

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    public debitSum: number;
    public creditSum: number;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteVoucherLineId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newVoucherLine: boolean;
    voucherLineViewModel: VoucherLineViewModel = new VoucherLineViewModelInfo;

    editDataItem?: VoucherLineViewModel = undefined;

    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    @Input() voucherId: number;


    ngOnInit() {
        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private voucherLineService: VoucherLineService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.VoucherLine, Metadatas.VoucherArticles);
    }

    getRowsCount() {
        return this.voucherLineService.getCount(this.voucherId, this.currentOrder, this.currentFilter).map(response => <any>(<Response>response).json());
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    deleteTransactionsLine() {
    //    this.transactionLineService.deleteTransactions(this.selectedRows).subscribe(res => {
    //        this.showMessage(this.deleteMsg, MessageType.Info);
    //        this.selectedRows = [];
    //        this.reloadGrid();
    //    }, (error => {
    //        this.showMessage(error, MessageType.Warning);
    //    }));
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid(voucherLineViewModel?: VoucherLineViewModel) {

        this.sppcLoading.show();

        var filter = this.currentFilter;
        var order = this.currentOrder;

        if (this.totalRecords == this.skip && this.totalRecords != 0) {
            this.skip = this.skip - this.pageSize;
        }

        this.voucherLineService.search(this.voucherId, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            this.properties = resData.metadata.properties;
            var totalCount = 0;


            if (voucherLineViewModel) {
                var rows = (resData.list as Array<VoucherLineViewModel>);
                var index = rows.findIndex(p => p.id == voucherLineViewModel.id);
                if (index >= 0) {
                    resData.list.splice(index, 1);
                    rows.splice(0, 0, voucherLineViewModel);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.list.splice(this.pageSize - 1, 1);
                    }

                    rows.splice(0, 0, voucherLineViewModel);
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

        this.voucherLineService.getVoucherInfo(this.voucherId).subscribe(res => {
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


    deleteVoucherLine(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.voucherLineService.delete(this.deleteVoucherLineId).subscribe(response => {
                this.deleteVoucherLineId = 0;
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

        this.deleteVoucherLineId = arg.dataItem.id;
        this.deleteConfirm = true;
    }


    //voucher form events
    public editHandler(arg: any) {

        this.sppcLoading.show();

        this.voucherLineService.getVoucherLineById(arg.dataItem.id).subscribe(res => {

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
        this.editDataItem = new VoucherLineViewModelInfo();
    }

    public saveHandler(voucherLineViewModel: VoucherLineViewModel) {

        voucherLineViewModel.branchId = this.BranchId;
        voucherLineViewModel.fiscalPeriodId = this.FiscalPeriodId;
        this.sppcLoading.show();
        if (!this.isNew) {

            this.isNew = false;

            this.voucherLineService.editVoucherLine(voucherLineViewModel)
                .subscribe(response => {

                    this.editDataItem = undefined;

                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();

                }, (error => {
                    //this.editDataItem = voucherLineViewModel;
                    this.errorMessage = error;

                }));
        }
        else {
            voucherLineViewModel.voucherId = this.voucherId;
            this.voucherLineService.insertVoucherLine(this.voucherId, voucherLineViewModel)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedVoucherLine = JSON.parse(response._body);
                    this.reloadGrid(insertedVoucherLine);

                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;

                }));
        }
        this.sppcLoading.hide();
    }

}


