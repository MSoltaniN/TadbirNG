import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  NgZone,
  OnInit,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { GridComponent } from "@progress/kendo-angular-grid";
import { RTL } from "@progress/kendo-angular-l10n";
import { Role, RoleFull } from "@sppc/admin/models";
import {
  RelatedItemsInfo,
  RoleDetailsInfo,
  RoleInfo,
  RoleService,
} from "@sppc/admin/service";
import { RoleApi } from "@sppc/admin/service/api";
import { SettingService } from "@sppc/config/service";
import { Permission } from "@sppc/core";
import { AutoGeneratedGridComponent } from "@sppc/shared/class";
import { ResultOption } from "@sppc/shared/class/result.option";
import { String } from "@sppc/shared/class/source";
import { ReportViewerComponent, ViewIdentifierComponent } from "@sppc/shared/components";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import { OperationId } from "@sppc/shared/enum/operationId";
import { RelatedItems } from "@sppc/shared/models";
import { ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  GridService,
  MetaDataService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
// import "rxjs/Rx";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "role",
  templateUrl: "./role.component.html",
  styles: [
    `
      .k-button {
        margin: 3px 0;
      }
    `,
  ],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class RoleComponent
  extends AutoGeneratedGridComponent<Role>
  implements OnInit
{

  // Report
  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  //
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

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public roleService: RoleService,
    public settingService: SettingService,
    public ngZone: NgZone,
    public elem: ElementRef
  ) {
    super(
      toastrService,
      translate,
      gridService,
      renderer,
      metadata,
      settingService,
      bStorageService,
      cdref,
      ngZone,
      elem,
      RoleApi.Roles,
      RoleApi.Role
    );
  }

  ngOnInit() {
    this.entityName = Entities.Role;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = String.Format(
      RoleApi.Roles,
      this.FiscalPeriodId,
      this.BranchId
    );
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  removeHandler(arg: any) {
    if (this.groupOperation) {
      //عملیات مربوط به حذف گروهی
      this.prepareDeleteConfirm(this.getText("Messages.SelectedItems"));
      this.deleteConfirm = true;
    } else {
      var recordId = this.selectedRows[0]; //.id;
      var record = this.rowData.data.find((f) => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
      this.deleteConfirm = true;
    }
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0]; //.id;
    this.grid.loading = true;
    this.roleService.getRoleFull(recordId).subscribe((res: any) => {
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
    var serviceUrl = this.isNew
      ? RoleApi.Roles
      : String.Format(RoleApi.Role, model.id);
    this.saveHandler(model, this.isNew, this.roleService, serviceUrl)
      .then((success: ResultOption) => {
        this.isActive = false;
      })
      .catch((resultOption: ResultOption) => {
        // error handler is called
        this.isActive = true;
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(
            resultOption.error
          );
      });
  }
  // Filter
  onAdvanceFilterOk() {
  this.enableViewListChanged(this.viewId);
  this.operationId = OperationId.Filter;
  this.reloadGrid();
  }

  public addNew() {
    this.grid.loading = true;
    this.isNew = true;
    this.isActive = true;
    this.editDataItem = new RoleInfo();
    this.roleService.getNewRoleFull().subscribe((res: any) => {
      this.permissionsData = res.permissions;

      this.grid.loading = false;
    });
    this.errorMessages = undefined;
    this.grid.loading = false;
  }

  detailHandler(roleId: number) {
    this.roleDetail = true;
    this.grid.loading = true;
    this.roleService.getRoleDetail(roleId).subscribe((res) => {
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
    this.roleName = "";
  }

  cancelRoleFiscalPeriodHandler() {
    this.roleFiscalPeriod = false;
    this.errorMessages = undefined;
    this.roleName = "";
  }

  cancelRoleBranchesHandler() {
    this.roleBranches = false;
    this.errorMessages = undefined;
    this.roleName = "";
  }

  cancelRoleCompaniesHandler() {
    this.companyList = false;
    this.errorMessages = undefined;
    this.roleName = "";
  }

  userHandler(roleId: number, roleName: string) {
    this.usersList = true;
    this.grid.loading = true;
    this.roleService.getRoleUsers(roleId).subscribe((res: any) => {
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
      this.roleService.getRoleCompanies(roleId).subscribe((res: any) => {
        this.roleCompaniesData = res;
        this.roleName = roleName;
        this.grid.loading = false;
      });

      this.errorMessages = undefined;
    } else {
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
    }
  }

  saveRoleCompaniesHandler(roleCompanies: RelatedItems) {
    this.grid.loading = true;
    this.gridService.submitted.next(true)
    this.roleService.modifiedRoleCompanies(roleCompanies).subscribe(
      (response) => {
        this.companyList = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
        this.gridService.submitted.next(false)
      },
      (resultOption: ResultOption) => {
        // error handler is called
        this.grid.loading = false;
        this.gridService.submitted.next(false)
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(
            resultOption.error
          );
      }
    );
  }

  saveRoleUsersHandler(roleUsers: RelatedItems) {
    this.grid.loading = true;
    this.gridService.submitted.next(true);
    this.roleService.modifiedRoleUsers(roleUsers).subscribe(
      (response) => {
        this.usersList = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
        this.gridService.submitted.next(false);
      },
      (resultOption: ResultOption) => {
        // error handler is called
        this.grid.loading = false;
        this.gridService.submitted.next(false);
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(
            resultOption.error
          );
      }
    );
  }

  branchHandler(roleId: number, roleName: string) {
    this.roleBranches = true;
    this.grid.loading = true;
    this.roleService.getRoleBranches(roleId).subscribe((res: any) => {
      this.roleBranchesData = res;
      this.roleName = roleName;
      this.grid.loading = false;
    });
    this.errorMessages = undefined;
  }

  saveRoleBranchesHandler(roleBranches: RelatedItems) {
    this.grid.loading = true;
    this.gridService.submitted.next(true);
    this.roleService.modifiedRoleBranches(roleBranches).subscribe(
      (response) => {
        this.roleBranches = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
        this.gridService.submitted.next(false);
      },
      (resultOption: ResultOption) => {
        // error handler is called
        this.grid.loading = false;
        this.gridService.submitted.next(false);
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(
            resultOption.error
          );
      }
    );
  }

  fiscalPeriodHandler(roleId: number, roleName: string) {
    this.roleFiscalPeriod = true;
    this.grid.loading = true;
    this.roleService.getRoleFiscalPeriods(roleId).subscribe((res: any) => {
      this.roleFiscalPeriodsData = res;
      this.roleName = roleName;
      this.grid.loading = false;
    });
    //this.errorMessage = '';
    this.errorMessages = undefined;
  }

  saveRoleFiscalPeriodHandler(roleBranches: RelatedItems) {
    this.grid.loading = true;
    this.gridService.submitted.next(true);
    this.roleService.modifiedRoleFiscalPeriods(roleBranches).subscribe(
      (response) => {
        this.roleFiscalPeriod = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
        this.gridService.submitted.next(false);
      },
      (resultOption: ResultOption) => {
        // error handler is called
        this.grid.loading = false;
        this.gridService.submitted.next(false);
        if (resultOption)
          this.errorMessages = this.errorHandlingService.handleError(
            resultOption.error
          );
      }
    );
  }

  isAdminSelected() {
    if (this.selectedRows.length > 0) {
      if (this.selectedRows.findIndex((s) => s == "1") >= 0) {
        return true;
      }
    } else {
      return true;
    }

    return false;
  }
}
