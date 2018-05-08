import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { ProjectService, ProjectViewModelInfo } from '../../service/index';
import { ProjectViewModel } from '../../model/index';
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
    selector: 'project',
    templateUrl: './project.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class ProjectComponent extends DefaultComponent implements OnInit {

    @Input() public parent: ProjectViewModel;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    ////for add in delete messageText
    deleteConfirm: boolean;
    deleteProjectsConfirm: boolean;
    deleteProjectId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newProject: boolean;
    project: ProjectViewModel = new ProjectViewModelInfo;


    editDataItem?: ProjectViewModel = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {

        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private projectService: ProjectService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.Project, Metadatas.Project);
    }


    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteProjectsConfirm = true;
    }

    deleteProjects(confirm: boolean) {
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
        this.deleteProjectsConfirm = false;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }


    reloadGrid(insertedProject?: ProjectViewModel) {

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

        this.projectService.search(this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            var totalCount = 0;

            if (insertedProject) {
                var rows = (resData as Array<ProjectViewModel>);
                var index = rows.findIndex(p => p.id == insertedProject.id);
                if (index >= 0) {
                    resData.splice(index, 1);
                    rows.splice(0, 0, insertedProject);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.splice(this.pageSize - 1, 1);
                    }

                    rows.splice(0, 0, insertedProject);

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


    deleteProject(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.projectService.delete(this.deleteProjectId).subscribe(response => {
                this.deleteProjectId = 0;
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

        this.deleteProjectId = arg.dataItem.id;
        this.deleteConfirm = true;
    }

    //detail account form events
    public editHandler(arg: any) {

        this.sppcLoading.show();
        this.projectService.getProjectById(arg.dataItem.id).subscribe(res => {
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


    public addNew(parentProjectId?: number) {
        this.isNew = true;
        this.editDataItem = new ProjectViewModelInfo();

        if (parentProjectId)
            this.parentId = parentProjectId;

        this.errorMessage = '';
    }

    public saveHandler(projectViewModel: ProjectViewModel) {

        projectViewModel.branchId = this.BranchId;
        projectViewModel.fiscalPeriodId = this.FiscalPeriodId;

        this.sppcLoading.show();

        if (!this.isNew) {
            this.isNew = false;
            this.projectService.editProject(projectViewModel)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = projectViewModel;
                    this.errorMessage = error;

                }));
        }
        else {
            //set parentid for childs accounts
            if (this.parentId) {
                projectViewModel.parentId = this.parentId;
                this.parentId = undefined;
            }
            else if (this.parent)
                projectViewModel.parentId = this.parent.id;
            //set parentid for childs accounts

            this.projectService.insertProject(projectViewModel)
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

    public showOnlyParent(dataItem: ProjectViewModel, index: number): boolean {
        return dataItem.childCount > 0;
    }

    public checkShow(dataItem: ProjectViewModel) {
        return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
    }

}


