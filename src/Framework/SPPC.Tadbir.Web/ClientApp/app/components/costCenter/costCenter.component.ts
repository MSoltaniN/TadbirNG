import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { CostCenterService, CostCenterInfo } from '../../service/index';
import { CostCenter } from '../../model/index';
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


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'costCenter',
    templateUrl: './costCenter.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class CostCenterComponent extends DefaultComponent implements OnInit {

    @Input() public parent: CostCenter;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    ////for add in delete messageText
    deleteConfirm: boolean;
    deleteCostCentersConfirm: boolean;
    deleteCostCenterId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newCostCenter: boolean;
    costCenter: CostCenter = new CostCenterInfo;


    editDataItem?: CostCenter = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {

        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private costCenterService: CostCenterService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.CostCenter, Metadatas.CostCenter);
    }


    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteCostCentersConfirm = true;
    }

    deleteCostCenters(confirm: boolean) {
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
        this.deleteCostCentersConfirm = false;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }


    reloadGrid(insertedCostCenter?: CostCenter) {

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

        this.costCenterService.search(this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            var totalCount = 0;

            if (insertedCostCenter) {
                var rows = (resData as Array<CostCenter>);
                var index = rows.findIndex(p => p.id == insertedCostCenter.id);
                if (index >= 0) {
                    resData.splice(index, 1);
                    rows.splice(0, 0, insertedCostCenter);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.splice(this.pageSize - 1, 1);
                    }

                    rows.splice(0, 0, insertedCostCenter);

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


    deleteCostCenter(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.costCenterService.delete(this.deleteCostCenterId).subscribe(response => {
                this.deleteCostCenterId = 0;
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

        this.deleteCostCenterId = arg.dataItem.id;
        this.deleteConfirm = true;
    }

    //detail account form events
    public editHandler(arg: any) {

        this.sppcLoading.show();
        this.costCenterService.getCostCenterById(arg.dataItem.id).subscribe(res => {
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


    public addNew(parentCostCenterId?: number) {
        this.isNew = true;
        this.editDataItem = new CostCenterInfo();

        if (parentCostCenterId)
            this.parentId = parentCostCenterId;

        this.errorMessage = '';
    } 

    public saveHandler(costCenter: CostCenter) {

        costCenter.branchId = this.BranchId;
        costCenter.fiscalPeriodId = this.FiscalPeriodId;

        this.sppcLoading.show();

        if (!this.isNew) {
            this.isNew = false;
            this.costCenterService.editCostCenter(costCenter)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = costCenter;
                    this.errorMessage = error;

                }));
        }
        else {
            //set parentid for childs accounts
            if (this.parentId) {
                costCenter.parentId = this.parentId;
                this.parentId = undefined;
            }
            else if (this.parent)
                costCenter.parentId = this.parent.id;
            //set parentid for childs accounts

            this.costCenterService.insertCostCenter(costCenter)
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

    public showOnlyParent(dataItem: CostCenter, index: number): boolean {
        return dataItem.childCount > 0;
    }

    public checkShow(dataItem: CostCenter) {
        return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
    }

}


