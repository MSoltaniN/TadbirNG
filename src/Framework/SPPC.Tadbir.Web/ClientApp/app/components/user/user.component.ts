import { Component, OnInit, Input, Renderer2, ViewChild } from '@angular/core';
import { UserService, UserInfo, RelatedItemsInfo } from '../../service/index';
import { User, RelatedItems } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';

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
import { UserApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { UserPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}

@Component({
    selector: 'user',
    templateUrl: './user.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class UserComponent extends DefaultComponent implements OnInit {
    public filterChange(filter: CompositeFilterDescriptor): void {
        throw new Error("Method not implemented.");
    }

    //#region Fields

    @ViewChild(GridComponent) grid: GridComponent;

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    //permission flag
    viewAccess: boolean;

    //for add in delete messageText
    deleteModelId: number;

    currentFilter: FilterExpression;
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;
    rolesList: boolean = false;

    editDataItem?: User = undefined;
    userRolesData: RelatedItemsInfo;
    isNew: boolean;
    errorMessage: string;
    //#endregion

    //#region Events
    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.User, UserPermissions.View);
        this.reloadGrid();
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        //if (this.selectedRows.length > 1)
        //    this.groupDelete = true;
        //else
        //    this.groupDelete = false;
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

    public editHandler(arg: any) {
        this.grid.loading = true;
        this.userService.getById(String.Format(UserApi.User, arg.dataItem.id)).subscribe(res => {
            this.editDataItem = res;
            this.grid.loading = false;
        })
        this.isNew = false;
        this.errorMessage = '';
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.isNew = false;
        this.errorMessage = '';
    }

    public saveHandler(model: User) {
        this.grid.loading = true;
        if (!this.isNew) {
            this.userService.edit<User>(String.Format(UserApi.User, model.id), model)
                .subscribe(response => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.errorMessage = error;
                    this.grid.loading = false;
                }));
        }
        else {
            this.userService.insert<User>(UserApi.Users, model)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedModel = response;
                    this.reloadGrid(insertedModel);
                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                    this.grid.loading = false;
                }));
        }
    }

    public rolesHandler(userId: number) {
        this.rolesList = true;
        this.grid.loading = true;
        this.userService.getUserRoles(userId).subscribe(res => {
            this.userRolesData = res;
            this.grid.loading = false;
        });

        this.errorMessage = '';
    }

    public cancelUserRolesHandler() {
        this.rolesList = false;
        this.errorMessage = '';
    }

    public saveUserRolesHandler(userRoles: RelatedItems) {
        this.grid.loading = true;
        this.userService.modifiedUserRoles(userRoles)
            .subscribe(response => {
                this.rolesList = false;
                this.showMessage(this.getText("User.UpdateRoles"), MessageType.Succes);
                this.grid.loading = false;
            }, (error => {
                this.grid.loading = false;
                this.errorMessage = error;
            }));
    }
  
    //#endregion

    //#region Constructor
    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private userService: UserService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.User, Metadatas.User);
    }
    //#endregion

    //#region Methods

    reloadGrid(insertedModel?: User) {
        if (this.viewAccess) {
            this.grid.loading = true;
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }

            if (insertedModel)
                this.goToLastPage(this.totalRecords);

            this.userService.getAll(String.Format(UserApi.Users, this.FiscalPeriodId, this.BranchId), this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
                var resData = res.body;
                var totalCount = 0;
                
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
                this.grid.loading = false;
            })
        }
        else {
            this.rowData = {
                data: [],
                total: 0
            }
        }
        
    }

    public addNew() {
        this.isNew = true;
        this.editDataItem = new UserInfo();
        this.errorMessage = '';
    }
     //#endregion
}


