import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { DetailAccountService, DetailAccountViewModelInfo } from '../../service/index';

import { ToastrService } from 'ngx-toastr'; /** add this component for message in client side */

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
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { GridResult } from '../../service/account.service';
import { DetailAccountViewModel } from '../../model/index';

export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'detailAccount',
    templateUrl: './detailAccount.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class DetailAccountComponent extends DefaultComponent {

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    ////for add in delete messageText
    deleteConfirm: boolean;
    deleteDetailAccountsConfirm: boolean;
    deleteDetailAccountId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newDetailAccount: boolean;
    detailAccount: DetailAccountViewModel = new DetailAccountViewModelInfo


    editDataItem?: DetailAccountViewModel = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;


    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private detailAccountService: DetailAccountService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.DetailAccount, Metadatas.DetailAccount);

        this.reloadGrid();

    }


    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteDetailAccountsConfirm = true;
    }

    deleteDetailAccounts(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            //this.accountService.deleteAccounts(this.selectedRows).subscribe(res => {
            //    this.showMessage(this.deleteMsg, MessageType.Info);
            //    this.selectedRows = [];
            //    this.reloadGrid();
            //    this.groupDelete = false;
            //}, (error => {
            //    this.sppcLoading.hide();
            //    this.showMessage(error, MessageType.Warning);
            //}));
        }

        this.groupDelete = false;
        this.deleteDetailAccountsConfirm = false;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }


    reloadGrid(insertedDetailAccount?: DetailAccountViewModel) {

        this.sppcLoading.show();
        
        var filter = this.currentFilter;
        var order = this.currentOrder;

        if (this.totalRecords == this.skip) {
            this.skip = this.skip - this.pageSize;
        }

        this.detailAccountService.search(this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            var totalCount = 0;

            if (insertedDetailAccount) {
                var rows = (resData as Array<DetailAccountViewModel>);
                var index = rows.findIndex(p => p.id == insertedDetailAccount.id);
                if (index >= 0) {
                    resData.splice(index, 1);
                    rows.splice(0, 0, insertedDetailAccount);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.splice(this.pageSize - 1, 1);
                    }

                    rows.splice(0, 0, insertedDetailAccount);

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
                data: resData,
                total: totalCount
            }

            this.showloadingMessage = !(resData.length == 0);
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


    deleteDetailAccount(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.detailAccountService.delete(this.deleteDetailAccountId).subscribe(response => {
                this.deleteDetailAccountId = 0;
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

        this.deleteDetailAccountId = arg.dataItem.id;
        this.deleteConfirm = true;
    }




    //detail account form events
    public editHandler(arg: any) {

        this.sppcLoading.show();
        this.detailAccountService.getDetailAccountById(arg.dataItem.id).subscribe(res => {
            this.editDataItem = res;
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
        this.editDataItem = new DetailAccountViewModelInfo();
        this.errorMessage = '';
    }

    public saveHandler(detailAccountViewModel: DetailAccountViewModel) {

        detailAccountViewModel.branchId = this.BranchId;
        detailAccountViewModel.fiscalPeriodId = this.FiscalPeriodId;

        this.sppcLoading.show();

        if (!this.isNew) {
            this.isNew = false;
            this.detailAccountService.editDetailAccount(detailAccountViewModel)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = detailAccountViewModel;
                    this.errorMessage = error;

                }));
        }
        else {
            this.detailAccountService.insertDetailAccount(detailAccountViewModel)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedDetailAccount = JSON.parse(response._body);
                    this.reloadGrid(insertedDetailAccount);

                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));

        }

        this.sppcLoading.hide();
    }

}


