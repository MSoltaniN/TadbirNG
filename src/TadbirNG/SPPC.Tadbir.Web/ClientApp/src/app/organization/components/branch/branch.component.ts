import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
// import "rxjs/Rx";
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
import { ShareDataService } from '@sppc/shared/services/share-data.service';
import { lastValueFrom } from 'rxjs';
import { ServiceLocator } from '@sppc/service.locator';


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


export default class BranchComponent extends AutoGridExplorerComponent<Branch> implements OnInit,OnDestroy {

  scopes = ["BranchComponent","AutoGridExplorerComponent"];

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public userService: UserService, private router: Router, private authenticationService: AuthenticationService, public branchService: BranchService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone, public errorHandlingService: ErrorHandlingService,public shareDataService:ShareDataService,public elem:ElementRef) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, bStorageService, Entities.Branch,
      "Branch.LedgerBranch", "", "",
      BranchApi.Branches, BranchApi.RootBranches, BranchApi.Branch, BranchApi.BranchChildren,
      "", cdref, ngZone,elem);

    this.scopeService = ServiceLocator.injector.get(ShareDataService);    
    this.scopeService.setScope(this);
  }

  ngOnDestroy(): void {    
    this.scopeService = ServiceLocator.injector.get(ShareDataService);    
    this.scopeService.clearScope(this);
    
  }

  ngOnInit() {
    this.entityName = Entities.Branch;
    this.viewId = ViewName[this.entityTypeName];

    this.getTreeNode();
    this.getDataUrl = BranchApi.Branches;
    this.reloadGrid();
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {
    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Branch.New' : 'Branch.Edit'),
      content: BranchFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.parent = this.parent;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessages = undefined;

    this.dialogRef.content.instance.save.subscribe((res) => {      
      this.saveHandler(res, isNew);
      // if (isNew)
      //   this.refreshTreeNodes();
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

  async rolesHandler() {
    var branchId = this.selectedRows[0];

    let selectedRoles = await lastValueFrom(this.branchService.getBranchRoles(branchId));

    this.dialogRef = this.dialogService.open({
      title: this.getText('Branch.RolesTitle'),
      content: BranchRolesFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.branchId = branchId;
    this.dialogModel.errorMessages = undefined;
    this.dialogModel.branchRoles = selectedRoles;

    this.dialogRef.content.instance.saveBranchRoles.subscribe((res) => {
      this.saveBranchRolesHandler(res);
    });

    const closeForm = this.dialogRef.content.instance.cancelBranchRoles.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  saveBranchRolesHandler(userRoles: RelatedItems) {
    this.gridService.submitted.next(true)
    this.branchService.modifiedBranchRoles(userRoles)
      .subscribe(response => {
        this.showMessage(this.getText("Branch.UpdateRoles"), MessageType.Succes);
        this.dialogRef.close();
        this.dialogModel.branchId = undefined;
        this.selectedRows = [];
        this.gridService.submitted.next(false)
      }, (error => {
        this.dialogModel.errorMessages = error;
        this.gridService.submitted.next(false)
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
      this.showMessage(this.getText("AllValidations.Login.PleaseSelectFiscalPeriod"), MessageType.Info);
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

          currentUser.permissions = context.TadbirContext.Permissions;
          currentUser.fiscalPeriodName = context.TadbirContext.FiscalPeriodName;
          currentUser.branchName = context.TadbirContext.BranchName;
          currentUser.companyName = context.TadbirContext.CompanyName;
          currentUser.ticket = newTicket;
          currentUser.roles = context.TadbirContext.Roles;

          this.bStorageService.setCurrentContext(currentUser);
          this.bStorageService.setLastUserBranchAndFpId(this.UserId, this.CompanyId.toString(), branchId.toString(), fpId.toString());

          this.getMenuAndReloadPage(newTicket);
          this.shareDataService.sharingSubjectData.next(currentUser);
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
