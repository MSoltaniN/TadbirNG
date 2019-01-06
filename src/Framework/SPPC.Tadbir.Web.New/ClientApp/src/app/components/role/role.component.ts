import { Component, OnInit, Input, Renderer2, ViewChild } from '@angular/core';
import { RoleService, RoleInfo, RoleFullInfo, PermissionInfo, RoleDetailsInfo, RelatedItemsInfo, SettingService } from '../../service/index';
import { Role, RoleFull, Permission, RelatedItems } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';

import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';

import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
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

  //#region Fields
  @ViewChild(GridComponent) grid: GridComponent;

  public rowData: GridDataResult;
  public selectedRows: number[] = [];
  public totalRecords: number;

  //permission flag
  viewAccess: boolean;

  //for add in delete messageText
  deleteConfirm: boolean;
  deleteModelId: number;

  currentFilter: FilterExpression;

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
  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.Role, RolePermissions.View);
    this.reloadGrid();
  }

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    if (this.selectedRows.length > 1)
      this.groupDelete = true;
    else
      this.groupDelete = false;
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    var isReload: boolean = false;
    if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
      isReload = true;

    this.currentFilter = this.getFilters(filter);
    if (isReload) {
      this.reloadGrid();
    }
  }

  public sortChange(sort: SortDescriptor[]): void {

    this.sort = sort.filter(f => f.dir != undefined);

    this.reloadGrid();
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (!this.groupDelete) {
      var recordId = this.selectedRows[0];
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.roleService.getRoleFull(recordId).subscribe(res => {
      this.editDataItem = res.role;
      this.permissionsData = res.permissions;
      this.grid.loading = false;
    });
    this.isNew = false;
    this.errorMessage = '';
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.isNew = false;
    this.errorMessage = '';
  }

  public saveHandler(model: RoleFull) {
    this.grid.loading = true;
    if (!this.isNew) {
      this.roleService.edit<RoleFull>(String.Format(RoleApi.Role, model.id), model)
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
      this.roleService.insert<RoleFull>(RoleApi.Roles, model)
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

  cancelRoleFiscalPeriodHandler() {
    this.roleFiscalPeriod = false;
    this.errorMessage = '';
    this.roleName = '';
  }

  cancelRoleBranchesHandler() {
    this.roleBranches = false;
    this.errorMessage = '';
    this.roleName = '';
  }

  cancelRoleDetailHandler() {
    //this.roleUsersData = undefined;
    this.roleDetail = false;
    this.errorMessage = '';
  }

  cancelRoleUsersHandler() {
    this.usersList = false;
    this.errorMessage = '';
    this.roleName = '';
  }
  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
    public roleService: RoleService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Role, Metadatas.Role);
  }
  //#endregion

  //#region Methods

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: Role) {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      this.roleService.getAll(String.Format(RoleApi.Roles, this.FiscalPeriodId, this.BranchId), this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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

  detailHandler(roleId: number) {
    this.roleDetail = true;
    this.grid.loading = true;
    this.roleService.getRoleDetail(roleId).subscribe(res => {
      this.roleDetailData = res;
      this.grid.loading = false;
    });
    this.errorMessage = '';
  }

  userHandler(roleId: number, roleName: string) {
    this.usersList = true;
    this.grid.loading = true;
    this.roleService.getRoleUsers(roleId).subscribe(res => {
      this.roleUsersData = res;
      this.roleName = roleName;
      this.grid.loading = false;
    });

    this.errorMessage = '';
  }

  saveRoleUsersHandler(roleUsers: RelatedItems) {
    this.grid.loading = true;
    this.roleService.modifiedRoleUsers(roleUsers)
      .subscribe(response => {
        this.usersList = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
      }, (error => {
        this.grid.loading = false;
        this.errorMessage = error;
      }));
  }

  branchHandler(roleId: number, roleName: string) {
    this.roleBranches = true;
    this.grid.loading = true;
    this.roleService.getRoleBranches(roleId).subscribe(res => {
      this.roleBranchesData = res;
      this.roleName = roleName;
      this.grid.loading = false;
    })
    this.errorMessage = '';
  }

  saveRoleBranchesHandler(roleBranches: RelatedItems) {
    this.grid.loading = true;
    this.roleService.modifiedRoleBranches(roleBranches)
      .subscribe(response => {
        this.roleBranches = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
      }, (error => {
        this.errorMessage = error;
        this.grid.loading = false;
      }));
  }

  fiscalPeriodHandler(roleId: number, roleName: string) {
    this.roleFiscalPeriod = true;
    this.grid.loading = true;
    this.roleService.getRoleFiscalPeriods(roleId).subscribe(res => {
      this.roleFiscalPeriodsData = res;
      this.roleName = roleName;
      this.grid.loading = false;
    })
    this.errorMessage = '';
  }

  saveRoleFiscalPeriodHandler(roleBranches: RelatedItems) {
    this.grid.loading = true;
    this.roleService.modifiedRoleFiscalPeriods(roleBranches)
      .subscribe(response => {
        this.roleFiscalPeriod = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
      }, (error => {
        this.errorMessage = error;
        this.grid.loading = false;
      }));
  }

  deleteRole(confirm: boolean) {
    if (confirm) {
      if (this.groupDelete) {
        //حذف گروهی
      }
      else {
        this.grid.loading = true;
        this.roleService.delete(String.Format(RoleApi.Role, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);
          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.selectedRows = [];
          this.reloadGrid();
        }, (error => {
          this.grid.loading = false;
          var message = error.message ? error.message : error;
          this.showMessage(message, MessageType.Warning);
        }));
      }
    }
    //hide confirm dialog
    this.deleteConfirm = false;
  }

  public addNew() {
    this.grid.loading = true;
    this.isNew = true;
    this.editDataItem = new RoleInfo();
    this.roleService.getNewRoleFull().subscribe(res => {
      this.permissionsData = res.permissions;

      this.grid.loading = false;
    });
    this.errorMessage = '';
    this.grid.loading = false;
  }


  //#endregion
}


