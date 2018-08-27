import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { RoleService, RoleInfo, RoleFullInfo, PermissionInfo, RoleDetailsInfo, RelatedItemsInfo } from '../../service/index';
import { Role, RoleFull, Permission, RelatedItems } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';

import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';

import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../enviroment";
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { RoleApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { RolePermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';


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

    //permission flag
    viewAccess: boolean;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteModelId: number;

    currentFilter: FilterExpression;
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    editDataItem?: Role | undefined = undefined;
    permissionsData: Permission;
    roleUsersData: RelatedItemsInfo;
    roleBranchesData: RelatedItemsInfo;
    roleFiscalPeriodsData: RelatedItemsInfo;
    roleDetailData: RoleDetailsInfo;


    isNew: boolean;
    usersList: boolean;
    roleBranches: boolean;
    roleFiscalPeriod: boolean;
    roleDetail: boolean;

    errorMessage: string;
    roleName: string;
    groupDelete: boolean = false;

    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.Role, RolePermissions.View);
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

    reloadGrid(insertedModel?: Role) {
        if (this.viewAccess) {
            this.sppcLoading.show();
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }
            this.roleService.getAll(String.Format(RoleApi.Roles, this.FiscalPeriodId, this.BranchId), this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
                var resData = res.body;
                var totalCount = 0;
                if (insertedModel) {
                    var rows = (resData as Array<Role>);
                    var index = rows.findIndex(p => p.id == insertedModel.id);
                    if (index >= 0) {
                        resData.splice(index, 1);
                        rows.splice(0, 0, insertedModel);
                    }
                    else {
                        if (rows.length == this.pageSize) {
                            resData.splice(this.pageSize - 1, 1);
                        }
                        rows.splice(0, 0, insertedModel);
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

    userHandler(roleId: number, roleName: string) {
        this.usersList = true;
        this.sppcLoading.show();
        this.roleService.getRoleUsers(roleId).subscribe(res => {
            this.roleUsersData = res;
            this.roleName = roleName;
            this.sppcLoading.hide();
        });

        this.errorMessage = '';
    }

    cancelRoleUsersHandler() {
        this.usersList = false;
        this.errorMessage = '';
        this.roleName = '';
    }

    saveRoleUsersHandler(roleUsers: RelatedItems) {
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

    branchHandler(roleId: number, roleName: string) {
        this.roleBranches = true;
        this.sppcLoading.show();
        this.roleService.getRoleBranches(roleId).subscribe(res => {
            this.roleBranchesData = res;
            this.roleName = roleName;
            this.sppcLoading.hide();
        })
        this.errorMessage = '';
    }

    cancelRoleBranchesHandler() {
        this.roleBranches = false;
        this.errorMessage = '';
        this.roleName = '';
    }

    saveRoleBranchesHandler(roleBranches: RelatedItems) {
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

    fiscalPeriodHandler(roleId: number, roleName: string) {
        this.roleFiscalPeriod = true;
        this.sppcLoading.show();
        this.roleService.getRoleFiscalPeriods(roleId).subscribe(res => {
            this.roleFiscalPeriodsData = res;
            this.roleName = roleName;
            this.sppcLoading.hide();
        })
        this.errorMessage = '';
    }

    cancelRoleFiscalPeriodHandler() {
        this.roleFiscalPeriod = false;
        this.errorMessage = '';
        this.roleName = '';
    }

    saveRoleFiscalPeriodHandler(roleBranches: RelatedItems) {
        this.sppcLoading.show();
        this.roleService.modifiedRoleFiscalPeriods(roleBranches)
            .subscribe(response => {
                this.roleFiscalPeriod = false;
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
            this.roleService.delete(String.Format(RoleApi.Role, this.deleteModelId)).subscribe(response => {
                this.deleteModelId = 0;
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
        this.deleteModelId = arg.dataItem.id;
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

    public saveHandler(model: RoleFull) {
        this.sppcLoading.show();
        if (!this.isNew) {
            this.roleService.edit<RoleFull>(String.Format(RoleApi.Role, model.id), model)
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
            this.roleService.insert<RoleFull>(RoleApi.Roles, model)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedModel = JSON.parse(response._body);
                    this.reloadGrid(insertedModel);
                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));
        }
        this.sppcLoading.hide();
    }

}


