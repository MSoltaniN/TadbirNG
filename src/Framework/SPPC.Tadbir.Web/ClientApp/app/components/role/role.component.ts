import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { RoleService, RoleInfo, RoleFullInfo, PermissionInfo, RoleUsersInfo, RoleBranchesInfo, RoleDetailsInfo } from '../../service/index';
import { Role, RoleFull, Permission, RoleUsers, RoleBranches } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';

import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';

import { SortDescriptor, orderBy, State, CompositeFilterDescriptor  } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../enviroment";
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { RoleApi } from '../../service/api/index';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}

@Component({
    selector: 'role',
    templateUrl: './role.component.html',
    styles: [`
              .k-button{ margin:3px 0; }
            `],
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class RoleComponent extends DefaultComponent implements OnInit {

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteRoleId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newRole: boolean;
    roleFull: RoleFull = new RoleFullInfo;


    editDataItem?: Role |undefined = undefined;
    permissionsData: Permission;
    roleUsersData: RoleUsersInfo;
    roleBranchesData: RoleBranchesInfo;
    roleDetailData: RoleDetailsInfo;


    isNew: boolean;
    usersList: boolean;
    roleBranches: boolean;
    roleDetail: boolean;

    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {
        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        public roleService: RoleService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.Role, Metadatas.Role);
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid(insertedRole?: Role) {
        this.sppcLoading.show();
        var filter = this.currentFilter;
        var order = this.currentOrder;
        if (this.totalRecords == this.skip && this.totalRecords != 0) {
            this.skip = this.skip - this.pageSize;
        }
        this.roleService.getAll(RoleApi.Roles,this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
            var resData = res.json();
            var totalCount = 0;
            if (insertedRole) {
                var rows = (resData as Array<Role>);
                var index = rows.findIndex(p => p.id == insertedRole.id);
                if (index >= 0) {
                    resData.splice(index, 1);
                    rows.splice(0, 0, insertedRole);
                }
                else {
                    if (rows.length == this.pageSize) {
                        resData.splice(this.pageSize - 1, 1);
                    }
                    rows.splice(0, 0, insertedRole);
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

    detailHandler(roleId: number) {
        this.roleDetail = true;
        this.sppcLoading.show();
        this.roleService.getRoleDetail(roleId).subscribe(res => {
            this.roleDetailData = res;
            this.sppcLoading.hide();
        });
        this.errorMessage = '';
    }

    cancelRoleDetailHandler() {
        //this.roleUsersData = undefined;
        this.roleDetail = false;
        this.errorMessage = '';
    }

    userHandler(roleId: number) {
        this.usersList = true;
        this.sppcLoading.show();
        this.roleService.getRoleUsers(roleId).subscribe(res => {
            this.roleUsersData = res;

            this.sppcLoading.hide();
        });

        this.errorMessage = '';
    }

    cancelRoleUsersHandler() {
        //this.roleUsersData = undefined;
        this.usersList = false;
        this.errorMessage = '';
    }

    saveRoleUsersHandler(roleUsers: RoleUsers) {
        this.sppcLoading.show();
        this.roleService.modifiedRoleUsers(roleUsers)
            .subscribe(response => {
                this.usersList = false;
                this.showMessage(this.updateMsg, MessageType.Succes); 
                this.sppcLoading.hide();
            }, (error => {
                this.sppcLoading.hide();
                this.errorMessage = error;
            }));
    }

    branchHandler(roleId: number) {
        this.roleBranches = true;
        this.sppcLoading.show();
        this.roleService.getRoleBranches(roleId).subscribe(res => {
            this.roleBranchesData = res;
            this.sppcLoading.hide();
        })       
        this.errorMessage = '';
    }

    cancelRoleBranchesHandler() {
        //this.roleUsersData = undefined;
        this.roleBranches = false;
        this.errorMessage = '';
    }

    saveRoleBranchesHandler(roleBranches: RoleBranches) {
        this.sppcLoading.show();
        this.roleService.modifiedRoleBranches(roleBranches)
            .subscribe(response => {
                this.roleBranches = false;
                this.showMessage(this.updateMsg, MessageType.Succes);
                this.sppcLoading.hide();
            }, (error => {
                this.errorMessage = error;
                this.sppcLoading.hide();
            }));       
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

    deleteRole(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.roleService.delete(RoleApi.Role,this.deleteRoleId).subscribe(response => {
                this.deleteRoleId = 0;
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
        this.deleteRoleId = arg.dataItem.id;
        this.deleteConfirm = true;
    }

    public editHandler(arg: any) {
        this.sppcLoading.show();
        this.roleService.getRoleFull(arg.dataItem.id).subscribe(res => {
            this.editDataItem = res.role;
            this.permissionsData = res.permissions;
            this.sppcLoading.hide();
        });
        this.isNew = false;
        this.errorMessage = '';       
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.isNew = false;
        this.errorMessage = '';
    }

    public addNew() {
        this.sppcLoading.show();
        this.isNew = true;
        this.editDataItem = new RoleInfo();
        this.roleService.getNewRoleFull().subscribe(res => {
            this.permissionsData = res.permissions;
        });
        this.errorMessage = '';
        this.sppcLoading.hide();
    }

    public saveHandler(roleFull: RoleFull) {
        this.sppcLoading.show();
        if (!this.isNew) {
            this.roleService.edit<RoleFull>(RoleApi.Role,roleFull, roleFull.id)
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
            this.roleService.insert<RoleFull>(RoleApi.Roles,roleFull)
                    .subscribe((response: any) => {
                        this.isNew = false;
                        this.editDataItem = undefined;
                        this.showMessage(this.insertMsg, MessageType.Succes);
                        var insertedRole = JSON.parse(response._body);
                        this.reloadGrid(insertedRole);
                    }, (error => {
                        this.isNew = true;
                        this.errorMessage = error;
                    }));
            }
        this.sppcLoading.hide();
    }

}


