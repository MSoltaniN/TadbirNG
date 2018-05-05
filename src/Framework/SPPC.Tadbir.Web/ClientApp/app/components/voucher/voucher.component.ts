import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { VoucherService, VoucherInfo, FiscalPeriodService } from '../../service/index';
import { Voucher } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';

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
import { SppcLoadingService } from '../../controls/sppcLoading/index';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}

@Component({
    selector: 'voucher',
    templateUrl: './voucher.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class VoucherComponent extends DefaultComponent implements OnInit {

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public fiscalPeriodRows: any[];
    public totalRecords: number;
    public fpId: number;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteVoucherId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newVoucher: boolean;
    voucher: Voucher = new VoucherInfo


    editDataItem?: Voucher = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {
        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private voucherService: VoucherService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.Voucher, Metadatas.Voucher);

    }

    getRowsCount() {
        return this.voucherService.getCount(this.currentOrder, this.currentFilter).map(response => <any>(<Response>response).json());
    }

    selectionKey(context: RowArgs): string {

        return context.dataItem.id + " " + context.index;
        //return context.dataItem.id;
    }

    deleteVouchers() {
        this.sppcLoading.show();
        this.voucherService.deleteVouchers(this.selectedRows).subscribe(res => {
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

    reloadGrid(insertedVoucher?: Voucher) {

        this.sppcLoading.show();

        var filter = this.currentFilter;
        var order = this.currentOrder;

        if (this.totalRecords == this.skip && this.totalRecords != 0) {
            this.skip = this.skip - this.pageSize;
        }

        this.voucherService.search(this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            this.properties = resData.metadata.properties;
            var totalCount = 0;


            if (insertedVoucher) {
                var rows = (resData.list as Array<Voucher>);
                var index = rows.findIndex(p => p.id == insertedVoucher.id);
                if (index >= 0) {
                    resData.list.splice(index, 1);
                    rows.splice(0, 0, insertedVoucher);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.list.splice(this.pageSize - 1, 1);
                    }

                    rows.splice(0, 0, insertedVoucher);
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

    deleteVoucher(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.voucherService.delete(this.deleteVoucherId).subscribe(response => {
                this.deleteVoucherId = 0;
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

        this.deleteVoucherId = arg.dataItem.id;
        this.deleteConfirm = true;
    }


    public editHandler(arg: any) {

        //this.editDataItem = arg.dataItem;
        this.sppcLoading.show();
        this.voucherService.getVoucherById(arg.dataItem.id).subscribe(res => {
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
        this.editDataItem = new VoucherInfo();
        this.errorMessage = '';
    }

    public saveHandler(voucher: Voucher) {

        voucher.branchId = this.BranchId;
        voucher.fiscalPeriodId = this.FiscalPeriodId;

        this.sppcLoading.show();
        if (!this.isNew) {
            this.voucherService.editVoucher(voucher)
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
            this.voucherService.insertVoucher(voucher)
                .subscribe((response: any) => {

                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedVoucher = JSON.parse(response._body);
                    this.reloadGrid(insertedVoucher);

                }, (error => {

                    this.isNew = true;
                    this.errorMessage = error;

                }));
        }
        this.sppcLoading.hide();
    }

}


