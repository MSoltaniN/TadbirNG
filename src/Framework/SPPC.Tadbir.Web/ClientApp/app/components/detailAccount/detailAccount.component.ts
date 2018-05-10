import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { DetailAccountService, DetailAccountInfo } from '../../service/index';
import { DetailAccount } from '../../model/index';
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
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { GridResult } from '../../service/account.service';


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


export class DetailAccountComponent extends DefaultComponent implements OnInit {

    @Input() public parent: DetailAccount;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;

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
    detailAccount: DetailAccount = new DetailAccountInfo


    editDataItem?: DetailAccount = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {

        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private detailAccountService: DetailAccountService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.DetailAccount, Metadatas.DetailAccount);
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


    reloadGrid(insertedDetailAccount?: DetailAccount) {

        this.sppcLoading.show();
        
        var filter = this.currentFilter;
        var order = this.currentOrder;

        if (this.totalRecords == this.skip) {
            this.skip = this.skip - this.pageSize;
        }

        if (this.totalRecords == this.skip) {
            this.skip = this.skip - this.pageSize;
        }

        if (this.parent) {
            if (this.parent.childCount > 0)
                filter.push(new Filter("ParentId", this.parent.id.toString(), "== {0}", "System.Int32"))
        }
        else
            filter.push(new Filter("ParentId", "null", "== {0}", "System.Int32"))        

        this.detailAccountService.search(this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            var totalCount = 0;

            if (insertedDetailAccount) {
                var rows = (resData as Array<DetailAccount>);
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


    public addNew(parentDetailAccountId?: number) {
        this.isNew = true;
        this.editDataItem = new DetailAccountInfo();

        if (parentDetailAccountId)
            this.parentId = parentDetailAccountId;

        this.errorMessage = '';
    } 

    public saveHandler(detailAccount: DetailAccount) {

        detailAccount.branchId = this.BranchId;
        detailAccount.fiscalPeriodId = this.FiscalPeriodId;

        this.sppcLoading.show();

        if (!this.isNew) {
            this.isNew = false;
            this.detailAccountService.editDetailAccount(detailAccount)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = detailAccount;
                    this.errorMessage = error;

                }));
        }
        else {
            //set parentid for childs accounts
            if (this.parentId) {
                detailAccount.parentId = this.parentId;
                this.parentId = undefined;
            }
            else if (this.parent)
                detailAccount.parentId = this.parent.id;
            //set parentid for childs accounts

            this.detailAccountService.insertDetailAccount(detailAccount)
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

    public showOnlyParent(dataItem: DetailAccount, index: number): boolean {
        return dataItem.childCount > 0;
    }

    public checkShow(dataItem: DetailAccount) {
        return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
    }

}


