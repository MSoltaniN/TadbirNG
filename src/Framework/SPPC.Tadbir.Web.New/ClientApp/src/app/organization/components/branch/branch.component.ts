import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService } from '@progress/kendo-angular-dialog';
import { BranchFormComponent } from './branch-form.component';
import { BranchRolesFormComponent } from './branch-roles-form.component';
import { String, AutoGridExplorerComponent, Filter, FilterExpressionOperator } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { BranchService, BranchInfo } from '@sppc/organization/service';
import { BranchApi, } from '@sppc/organization/service/api';
import { Branch } from '@sppc/organization/models';
import { GridService, BrowserStorageService, MetaDataService, ErrorHandlingService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { ViewName, BranchPermissions } from '@sppc/shared/security';
import { RelatedItems, Command } from '@sppc/shared/models';
import { ViewIdentifierComponent, ReportViewerComponent } from '@sppc/shared/components';
import { GridComponent } from '@progress/kendo-angular-grid';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { CompanyLoginInfo, AuthenticationService, ContextInfo } from '@sppc/core';
import { UserService } from '@sppc/admin/service';
import { Router } from '@angular/router';
import { OperationId } from '@sppc/shared/enum/operationId';


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


export class BranchComponent extends AutoGridExplorerComponent<Branch> implements OnInit {

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public userService: UserService, private router: Router, private authenticationService: AuthenticationService, public branchService: BranchService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone, public errorHandlingService: ErrorHandlingService) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, bStorageService, Entities.Branch,
      "Branch.LedgerBranch", "", "",
      BranchApi.Branches, BranchApi.RootBranches, BranchApi.Branch, BranchApi.BranchChildren,
      "", cdref, ngZone)
  }

  ngOnInit() {
    this.entityName = Entities.Branch;
    this.viewId = ViewName[this.entityTypeName];

    this.getTreeNode();
    this.getDataUrl = String.Format(BranchApi.CompanyBranches, this.CompanyId);
    this.reloadGrid();
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {
    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: BranchFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.parent = this.parent;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessages = undefined;

    this.dialogRef.content.instance.save.subscribe((res) => {      
      this.saveHandler(res, isNew);
      if (isNew)
        this.refreshTreeNodes();
      if (!this.IsAdmin) {
        this.showMessage(this.getText("Branch.BranchIsNotAccess"), MessageType.Info);
      }
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  public onSelectContextmenu({ item }): void {

    let hasPermission: boolean = false;

    switch (item.mode) {
      case 'Remove': {
        hasPermission = this.isAccess(Entities.Branch, BranchPermissions.Delete);
        if (hasPermission)
          this.contextMenuRemoveHandler();
        break;
      }
      case 'Edit': {
        hasPermission = this.isAccess(Entities.Branch, BranchPermissions.Edit);
        if (hasPermission) {
          this.contextMenuEditHandler();
          this.selectedContextmenu = undefined;
        }
        break;
      }
      case 'New': {
        hasPermission = this.isAccess(Entities.Branch, BranchPermissions.Create);
        if (hasPermission) {
          this.contextMenuAddNewHandler();
          this.selectedContextmenu = undefined;
        }
        break;
      }
      default:
    }

    if (!hasPermission)
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
  }

  addNew() {
    var model = new BranchInfo();
    model.level = this.parent ? this.parent.level + 1 : 0;
    model.parentId = this.parent ? this.parent.id : null;

    this.branchService.insert<Branch>(BranchApi.RootBranches, model).subscribe(res => {
      this.editDataItem = new BranchInfo();
      this.openEditorDialog(true);
    }, (error => {
        this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);
    }));  
  }

  

  rolesHandler() {
    var branchId = this.selectedRows[0];

    this.dialogRef = this.dialogService.open({
      title: this.getText('Branch.RolesTitle'),
      content: BranchRolesFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.branchId = branchId;
    this.dialogModel.errorMessages = undefined;

    this.dialogRef.content.instance.saveBranchRoles.subscribe((res) => {
      this.saveBranchRolesHandler(res);
    });

    const closeForm = this.dialogRef.content.instance.cancelBranchRoles.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  saveBranchRolesHandler(userRoles: RelatedItems) {
    this.branchService.modifiedBranchRoles(userRoles)
      .subscribe(response => {
        this.showMessage(this.getText("Branch.UpdateRoles"), MessageType.Succes);
        this.dialogRef.close();
        this.dialogModel.branchId = undefined;
        this.selectedRows = [];
      }, (error => {
        this.dialogModel.errorMessages = error;
      }));
  }

  /**
   *عملیات تغییر شرکت
   * */
  onChangeBranch() {
    var branchId = this.selectedRows[0];
    this.bStorageService.setSelectedBranchId(branchId);

    var fiscalPeriodId = this.FiscalPeriodId ? this.FiscalPeriodId : this.bStorageService.getSelectedFiscalPeriodId();

    if (fiscalPeriodId) {
      this.getNewTicket(branchId, fiscalPeriodId);
    }
    else {
      this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), MessageType.Info);
    }
  }

  onAdvanceFilterOk() {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }

  getNewTicket(branchId: number, fpId: number) {
    var companyLoginModel = new CompanyLoginInfo();
    companyLoginModel.companyId = this.CompanyId;
    companyLoginModel.branchId = branchId;
    companyLoginModel.fiscalPeriodId = fpId;

    this.authenticationService.getCompanyTicket(companyLoginModel, this.Ticket).subscribe(res => {
      if (res.headers != null) {
        let newTicket = res.headers.get('X-Tadbir-AuthTicket');

        let contextInfo = res.body;
        var currentUser = this.bStorageService.getCurrentUser();
        if (currentUser != null) {
          currentUser.branchId = contextInfo.branchId;
          currentUser.companyId = contextInfo.companyId;
          currentUser.fpId = contextInfo.fiscalPeriodId;

          var context = this.authenticationService.parseJwt(this.Ticket);

          //currentUser.permissions = JSON.parse(atob(this.Ticket)).user.permissions;
          //currentUser.fiscalPeriodName = contextInfo.fiscalPeriodName;
          //currentUser.branchName = contextInfo.branchName;
          //currentUser.companyName = contextInfo.companyName;
          //currentUser.ticket = newTicket;
          //currentUser.roles = contextInfo.roles;

          currentUser.permissions = context.TadbirContext.User.Permissions;
          currentUser.fiscalPeriodName = context.TadbirContext.FiscalPeriodName;
          currentUser.branchName = context.TadbirContext.BranchName;
          currentUser.companyName = context.TadbirContext.CompanyName;
          currentUser.ticket = newTicket;
          currentUser.roles = context.TadbirContext.Roles;

          this.bStorageService.setCurrentContext(currentUser);
          this.bStorageService.setLastUserBranchAndFpId(this.UserId, this.CompanyId.toString(), branchId.toString(), fpId.toString());

          this.getMenuAndReloadPage(newTicket);
        }

      }

    })
  }

  getMenuAndReloadPage(ticket: string) {
    this.userService.getCurrentUserCommands(ticket).subscribe((res: Array<Command>) => {
      this.bStorageService.setMenu(res);
      this.bStorageService.removeSelectedBranchAndFiscalPeriod();
      this.router.navigate(['/tadbir/dashboard']);
    });
  }
}


