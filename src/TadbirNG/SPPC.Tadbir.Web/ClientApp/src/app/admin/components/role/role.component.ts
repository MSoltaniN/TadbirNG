import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { AutoGeneratedGridComponent } from '@sppc/shared/class';
import { Permission } from '@sppc/core';
import { GridService, MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { String } from '@sppc/shared/class/source';
import { Role, RoleFull } from '@sppc/admin/models';
import { RelatedItemsInfo, RoleDetailsInfo, RoleService, RoleInfo } from '@sppc/admin/service';
import { RoleApi } from '@sppc/admin/service/api';
import { ViewName } from '@sppc/shared/security';
import { RelatedItems, IEntity } from '@sppc/shared/models';
import { ReloadOption } from '@sppc/shared/class/reload-option';
import { ReloadStatusType } from '@sppc/shared/enum';
import { ResultOption } from '@sppc/shared/class/result.option';


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


export class RoleComponent extends AutoGeneratedGridComponent<Role> implements OnInit {

  rolesList: boolean = false;
  isNew: boolean;  
  isActive: boolean;

  usersList: boolean;
  roleBranches: boolean;
  roleFiscalPeriod: boolean;
  roleDetail: boolean;
  companyList: boolean;
  roleName: string;
  

  //editDataItem?: Role | undefined = undefined;
  permissionsData: Permission;
  roleUsersData: RelatedItemsInfo;
  roleCompaniesData: RelatedItemsInfo;
  roleBranchesData: RelatedItemsInfo;
  roleFiscalPeriodsData: RelatedItemsInfo;
  roleDetailData: RoleDetailsInfo;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public roleService: RoleService,
    public settingService: SettingService, public ngZone: NgZone, ) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone, RoleApi.Roles, RoleApi.Role);
  }

  ngOnInit() {
    this.entityName = Entities.Role;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = String.Format(RoleApi.Roles, this.FiscalPeriodId, this.BranchId);
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  removeHandler(arg: any) {
    if (this.groupOperation) {
      //عملیات مربوط به حذف گروهی
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
      this.deleteConfirm = true;
    }
    else {
      var recordId = this.selectedRows[0];//.id;
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
      this.deleteConfirm = true;
    }
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];//.id;
    this.grid.loading = true;
    this.roleService.getRoleFull(recordId).subscribe(res => {
      this.editDataItem = res.role;
      this.permissionsData = res.permissions;
      this.grid.loading = false;
    });
    this.isNew = false;
    this.isActive = false;
    this.errorMessages = undefined;
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.isNew = false;
    this.isActive = false;
    this.errorMessages = undefined;
  }

  public saveRole(model: RoleFull) {
    debugger
    var serviceUrl = this.isNew ? RoleApi.Roles : String.Format(RoleApi.Role, model.id);  
    this.saveHandler(model, this.isNew, this.roleService, serviceUrl)
      .then((success: ResultOption) => {
        this.isActive = false;
      })
      .catch((resultOption: ResultOption) => {
        // error handler is called
        this.isActive = true;
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(resultOption.error);
      });    
  }  

  public addNew() {
    this.grid.loading = true;
    this.isNew = true;
    this.isActive = true;
    this.editDataItem = new RoleInfo();
    this.roleService.getNewRoleFull().subscribe(res => {
      this.permissionsData = res.permissions;

      this.grid.loading = false;
    });
    this.errorMessages = undefined;
    this.grid.loading = false;
  }

  detailHandler(roleId: number) {
    this.roleDetail = true;
    this.grid.loading = true;
    this.roleService.getRoleDetail(roleId).subscribe(res => {
      this.roleDetailData = res;
      this.grid.loading = false;
    });
    this.errorMessages = undefined;
  }

  cancelRoleDetailHandler() {
    this.roleDetail = false;
    this.errorMessages = undefined;
  }

  cancelRoleUsersHandler() {
    this.usersList = false;
    this.errorMessages = undefined;
    this.roleName = '';
  }

  cancelRoleFiscalPeriodHandler() {
    this.roleFiscalPeriod = false;
    this.errorMessages = undefined;
    this.roleName = '';
  }

  cancelRoleBranchesHandler() {
    this.roleBranches = false;
    this.errorMessages = undefined;
    this.roleName = '';
  }

  cancelRoleCompaniesHandler() {
    this.companyList = false;
    this.errorMessages = undefined;
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

    this.errorMessages = undefined;
  }

  companiesHandler(roleId: number, roleName: string) {
    if (this.IsAdmin) {
      this.companyList = true;
      this.grid.loading = true;
      this.roleService.getRoleCompanies(roleId).subscribe(res => {
        this.roleCompaniesData = res;
        this.roleName = roleName;
        this.grid.loading = false;
      });

      this.errorMessages = undefined;
    }
    else {
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }
  }

  saveRoleCompaniesHandler(roleCompanies: RelatedItems) {
    this.grid.loading = true;
    this.roleService.modifiedRoleCompanies(roleCompanies)
      .subscribe(response => {
        this.companyList = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
      },(resultOption: ResultOption) => {
        // error handler is called
        this.grid.loading = false;
        if(resultOption)
          this.errorMessages = this.errorHandlingService.handleError(resultOption.error);
      });    
  }

  saveRoleUsersHandler(roleUsers: RelatedItems) {
    this.grid.loading = true;
    this.roleService.modifiedRoleUsers(roleUsers)
      .subscribe(response => {
        this.usersList = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
      }, (resultOption: ResultOption) => {
        // error handler is called
        this.grid.loading = false;
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(resultOption.error);
      });
  }

  branchHandler(roleId: number, roleName: string) {
    this.roleBranches = true;
    this.grid.loading = true;
    this.roleService.getRoleBranches(roleId).subscribe(res => {
      this.roleBranchesData = res;
      this.roleName = roleName;
      this.grid.loading = false;
    })
    this.errorMessages = undefined;
  }

  saveRoleBranchesHandler(roleBranches: RelatedItems) {
    this.grid.loading = true;
    this.roleService.modifiedRoleBranches(roleBranches)
      .subscribe(response => {
        this.roleBranches = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
      }, (resultOption: ResultOption) => {
        // error handler is called
        this.grid.loading = false;
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(resultOption.error);
      });
  }

  fiscalPeriodHandler(roleId: number, roleName: string) {
    this.roleFiscalPeriod = true;
    this.grid.loading = true;
    this.roleService.getRoleFiscalPeriods(roleId).subscribe(res => {
      this.roleFiscalPeriodsData = res;
      this.roleName = roleName;
      this.grid.loading = false;
    })
    //this.errorMessage = '';
    this.errorMessages = undefined;
  }

  saveRoleFiscalPeriodHandler(roleBranches: RelatedItems) {
    this.grid.loading = true;
    this.roleService.modifiedRoleFiscalPeriods(roleBranches)
      .subscribe(response => {
        this.roleFiscalPeriod = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
      }, (resultOption: ResultOption) => {
        // error handler is called
        this.grid.loading = false;
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(resultOption.error);
      });
  }



}


