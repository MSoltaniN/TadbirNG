import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { RoleService, RoleInfo, RoleFullViewModelInfo, PermissionInfo, RoleUsersViewModelInfo, RoleBranchesViewModelInfo, RoleDetailsViewModelInfo } from '../../service/index';
import { Role, RoleFullViewModel, Permission, RoleUsersViewModel, RoleBranchesViewModel } from '../../model/index';
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
    roleFullViewModel: RoleFullViewModel = new RoleFullViewModelInfo;


    editDataItem?: Role = undefined;
    permissionsData: Permission;
    roleUsersData: RoleUsersViewModelInfo;
    roleBranchesData: RoleBranchesViewModelInfo;
    roleDetailData: RoleDetailsViewModelInfo;


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

        this.roleService.search(this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

            var resData = res.json();
            //this.properties = resData.metadata.properties;
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

    saveRoleUsersHandler(roleUsersViewModel: RoleUsersViewModel) {

        this.sppcLoading.show();

        this.roleService.modifiedRoleUsers(roleUsersViewModel)
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

    saveRoleBranchesHandler(roleBranchesViewModel: RoleBranchesViewModel) {
        this.sppcLoading.show();

        this.roleService.modifiedRoleBranches(roleBranchesViewModel)
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
            this.roleService.delete(this.deleteRoleId).subscribe(response => {
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

        this.roleService.getRoleFullViewModel(arg.dataItem.id).subscribe(res => {
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
        this.roleService.getNewRoleFullViewModel().subscribe(res => {
            this.permissionsData = res.permissions;
        });
        this.errorMessage = '';
        this.sppcLoading.hide();
    }

    public saveHandler(roleFullViewModel: RoleFullViewModel) {

        this.sppcLoading.show();
        if (!this.isNew) {
            this.roleService.editRole(roleFullViewModel)
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
                this.roleService.insertRole(roleFullViewModel)
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


