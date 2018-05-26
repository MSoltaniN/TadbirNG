import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { BranchService, BranchInfo } from '../../service/index';
import { Branch } from '../../model/index';
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
import { BranchApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { BranchPermissions } from '../../security/permissions';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'branch',
    templateUrl: './branch.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class BranchComponent extends DefaultComponent implements OnInit {

    @Input() public parent: Branch;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    //permission flag
    viewAccess: boolean;
    insertAccess: boolean;
    editAccess: boolean;
    deleteAccess: boolean;

    ////for add in delete messageText
    deleteConfirm: boolean;
    deleteBranchesConfirm: boolean;
    deleteBranchId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newBranch: boolean;
    branch: Branch = new BranchInfo;


    editDataItem?: Branch = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.Branch, BranchPermissions.View);
        this.insertAccess = this.isAccess(SecureEntity.Branch, BranchPermissions.Create);
        this.editAccess = this.isAccess(SecureEntity.Branch, BranchPermissions.Edit);
        this.deleteAccess = this.isAccess(SecureEntity.Branch, BranchPermissions.Delete);

        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private branchService: BranchService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.Branch, Metadatas.Branch);
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteBranchesConfirm = true;
    }

    deleteBranches(confirm: boolean) {
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
        this.deleteBranchesConfirm = false;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid(insertedBranch?: Branch) {
        if (this.viewAccess) {
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
            this.branchService.getAll(String.Format(BranchApi.CompanyBranches, this.CompanyId), this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
                var resData = res.json();
                var totalCount = 0;
                if (insertedBranch) {
                    var rows = (resData as Array<Branch>);
                    var index = rows.findIndex(p => p.id == insertedBranch.id);
                    if (index >= 0) {
                        resData.splice(index, 1);
                        rows.splice(0, 0, insertedBranch);
                    }
                    else {
                        if (rows.length == this.pageSize) {
                            resData.splice(this.pageSize - 1, 1);
                        }

                        rows.splice(0, 0, insertedBranch);
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
        else {
            this.rowData = {
                data: [],
                total: 0
            }
        }
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

    deleteBranch(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.branchService.delete(String.Format(BranchApi.Branch, this.deleteBranchId)).subscribe(response => {
                this.deleteBranchId = 0;
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
        this.deleteBranchId = arg.dataItem.id;
        this.deleteConfirm = true;
    }

    //detail account form events
    public editHandler(arg: any) {
        this.sppcLoading.show();
        this.branchService.getById(String.Format(BranchApi.Branch, arg.dataItem.id)).subscribe(res => {
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

    public addNew(parentBranchId?: number) {
        this.isNew = true;
        this.editDataItem = new BranchInfo();
        if (parentBranchId)
            this.parentId = parentBranchId;
        this.errorMessage = '';
    }

    public saveHandler(branch: Branch) {
        branch.companyId = this.CompanyId;
        this.sppcLoading.show();
        if (!this.isNew) {
            this.isNew = false;
            this.branchService.edit<Branch>(String.Format(BranchApi.Branch, branch.id), branch)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = branch;
                    this.errorMessage = error;
                }));
        }
        else {
            //set parentid for childs accounts
            if (this.parentId) {
                branch.parentId = this.parentId;
                this.parentId = undefined;
            }
            else if (this.parent)
                branch.parentId = this.parent.id;
            //set parentid for childs accounts

            this.branchService.insert<Branch>(BranchApi.Branches, branch)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedBranch = JSON.parse(response._body);
                    this.reloadGrid(insertedBranch);
                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));
        }
        this.sppcLoading.hide();
    }

    public showOnlyParent(dataItem: Branch, index: number): boolean {
        return dataItem.childCount > 0;
    }

    public checkShow(dataItem: Branch) {
        return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
    }

}


