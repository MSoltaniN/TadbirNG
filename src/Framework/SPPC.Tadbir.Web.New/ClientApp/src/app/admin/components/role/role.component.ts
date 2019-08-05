import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { AutoGeneratedGridComponent } from '@sppc/shared/class';
import { Permission } from '@sppc/core';
import { GridService, MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { String } from '@sppc/shared/class/source';
import { Role, RoleFull } from '@sppc/admin/models';
import { RelatedItemsInfo, RoleDetailsInfo, RoleService, RoleInfo } from '@sppc/admin/service';
import { RoleApi } from '@sppc/admin/service/api';
import { ViewName } from '@sppc/shared/security';
import { RelatedItems } from '@sppc/shared/models';


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


export class RoleComponent extends AutoGeneratedGridComponent implements OnInit {

  rolesList: boolean = false;
  isNew: boolean;
  errorMessage: string;

  usersList: boolean;
  roleBranches: boolean;
  roleFiscalPeriod: boolean;
  roleDetail: boolean;
  roleName: string;

  editDataItem?: Role | undefined = undefined;
  permissionsData: Permission;
  roleUsersData: RelatedItemsInfo;
  roleBranchesData: RelatedItemsInfo;
  roleFiscalPeriodsData: RelatedItemsInfo;
  roleDetailData: RoleDetailsInfo;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public roleService: RoleService,
    public settingService: SettingService, public ngZone: NgZone, ) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.Role;
    this.viewId = ViewName[this.entityTypeName];

    this.reloadGrid();
    this.cdref.detectChanges();
  }

  removeHandler(arg: any) {
    if (this.groupOperation) {
      //عملیات مربوط به حذف گروهی
    }
    else {
      var recordId = this.selectedRows[0].id;
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
      this.deleteConfirm = true;
    }
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0].id;
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
          this.selectedRows = [];
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
          this.selectedRows = [];
        }, (error => {
          this.isNew = true;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
  }

  reloadGrid(insertedModel?: Role) {
   
      this.grid.loading = true;
      var filter = this.currentFilter;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      this.reportFilter = filter;

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

  deleteModel(confirm: boolean) {
    if (confirm) {
      if (this.groupOperation) {
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

  detailHandler(roleId: number) {
    this.roleDetail = true;
    this.grid.loading = true;
    this.roleService.getRoleDetail(roleId).subscribe(res => {
      this.roleDetailData = res;
      this.grid.loading = false;
    });
    this.errorMessage = '';
  }

  cancelRoleDetailHandler() {
    this.roleDetail = false;
    this.errorMessage = '';
  }

  cancelRoleUsersHandler() {
    this.usersList = false;
    this.errorMessage = '';
    this.roleName = '';
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



}


