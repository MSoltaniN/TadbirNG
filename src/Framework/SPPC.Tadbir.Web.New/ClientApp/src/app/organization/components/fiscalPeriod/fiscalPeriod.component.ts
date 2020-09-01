import { Component, OnInit, Input, Renderer2, ChangeDetectorRef, NgZone, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { String, AutoGeneratedGridComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { MetaDataService, GridService, BrowserStorageService } from '@sppc/shared/services';
import { FiscalPeriodService, FiscalPeriodInfo } from '@sppc/organization/service';
import { FiscalPeriodApi } from '@sppc/organization/service/api';
import { FiscalPeriod } from '@sppc/organization/models';
import { RelatedItemsInfo, UserService } from '@sppc/admin/service';
import { SettingService } from '@sppc/config/service';
import { ViewName } from '@sppc/shared/security';
import { RelatedItems, Command } from '@sppc/shared/models';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier/view-identifier.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { FiscalPeriodFormComponent } from './fiscalPeriod-form.component';
import { CompanyLoginInfo, AuthenticationService } from '@sppc/core';
import { Router } from '@angular/router';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'fiscalPeriod',
  templateUrl: './fiscalPeriod.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class FiscalPeriodComponent extends AutoGeneratedGridComponent implements OnInit {

  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  rolesList: boolean = false;

  errorMessage: string;
  editDataItem?: FiscalPeriod = undefined;
  fiscalPeriodRolesData: RelatedItemsInfo;

  public dialogRef: DialogRef;
  public dialogModel: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private fiscalPeriodService: FiscalPeriodService,
    public userService: UserService, private router: Router, private authenticationService: AuthenticationService,
    public settingService: SettingService, public ngZone: NgZone, public dialogService: DialogService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.FiscalPeriod;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = String.Format(FiscalPeriodApi.CompanyFiscalPeriods, this.CompanyId);
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {
    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: FiscalPeriodFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessage = undefined;

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.saveHandler(res, isNew);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (this.groupOperation) {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
    else {
      var recordId = this.selectedRows[0];
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.fiscalPeriodService.getById(String.Format(FiscalPeriodApi.FiscalPeriod, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.openEditorDialog(false);
      this.grid.loading = false;
    })

  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.errorMessage = '';
  }

  public saveFiscalPeriodRolesHandler(fPeriodRoles: RelatedItems) {
    this.grid.loading = true;
    this.fiscalPeriodService.modifiedFiscalPeriodRoles(fPeriodRoles)
      .subscribe(response => {
        this.rolesList = false;
        this.showMessage(this.getText("FiscalPeriod.UpdateRoles"), MessageType.Succes);
        this.grid.loading = false;
      }, (error => {
        this.grid.loading = false;
        this.errorMessage = error;
      }));
  }

  public saveHandler(model: FiscalPeriod, isNew: boolean) {
    model.companyId = this.CompanyId;
    if (!isNew) {
      this.fiscalPeriodService.edit<FiscalPeriod>(String.Format(FiscalPeriodApi.FiscalPeriod, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);

          this.dialogRef.close();
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid();
          this.selectedRows = [];
        }, (error => {
          this.editDataItem = model;
          this.dialogModel.errorMessage = error;
        }));
    }
    else {
      this.fiscalPeriodService.insert<FiscalPeriod>(FiscalPeriodApi.FiscalPeriods, model)
        .subscribe((response: any) => {
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;

          this.dialogRef.close();
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid(insertedModel);
          this.selectedRows = [];

        }, (error => {
          this.dialogModel.errorMessage = error;
        }));
    }
  }

  public rolesHandler() {
    if (this.selectedRows.length == 1) {
      this.rolesList = true;
      this.grid.loading = true;
      this.fiscalPeriodService.getFiscalPeriodRoles(this.selectedRows[0]).subscribe(res => {
        this.fiscalPeriodRolesData = res;
        this.grid.loading = false;
      });

      this.errorMessage = '';
    }
  }

  public cancelFiscalPeriodRolesHandler() {
    this.rolesList = false;
    this.errorMessage = '';
  }

  //reloadGrid(insertedModel?: FiscalPeriod) {
  //  this.grid.loading = true;
  //  var filter = this.currentFilter;
  //  this.reportFilter = filter;

  //  if (this.totalRecords == this.skip && this.totalRecords != 0) {
  //    this.skip = this.skip - this.pageSize;
  //  }


  //  if (insertedModel)
  //    this.goToLastPage(this.totalRecords);

  //  this.fiscalPeriodService.getAll(String.Format(FiscalPeriodApi.CompanyFiscalPeriods, this.CompanyId), this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
  //    var resData = res.body;
  //    var totalCount = 0;

  //    if (res.headers != null) {
  //      var headers = res.headers != undefined ? res.headers : null;
  //      if (headers != null) {
  //        var retheader = headers.get('X-Total-Count');
  //        if (retheader != null)
  //          totalCount = parseInt(retheader.toString());
  //      }
  //    }
  //    this.rowData = {
  //      data: resData,
  //      total: totalCount
  //    }
  //    this.showloadingMessage = !(resData.length == 0);
  //    this.totalRecords = totalCount;

  //    this.grid.loading = false;
  //  })

  //}

  deleteModel(confirm: boolean) {
    if (confirm) {
      if (this.groupOperation) {
        //حذف گروهی
        this.grid.loading = true;
        let rowsId: Array<number> = [];
        this.selectedRows.forEach(item => {
          rowsId.push(item);
        })

        this.fiscalPeriodService.groupDelete(FiscalPeriodApi.FiscalPeriods, rowsId).subscribe(res => {
          this.showMessage(this.deleteMsg, MessageType.Info);

          if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;
                    
          this.groupOperation = false;
          this.reloadGrid(undefined, undefined, true);
          this.selectedRows = [];

        }, (error => {
          this.grid.loading = false;
          this.showMessage(error, MessageType.Warning);
        }));
      }
      else {
        this.grid.loading = true;
        this.fiscalPeriodService.delete(String.Format(FiscalPeriodApi.FiscalPeriod, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);
          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.reloadGrid(undefined, undefined, true);
          this.selectedRows = [];
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
    this.editDataItem = new FiscalPeriodInfo();
    this.openEditorDialog(true);
  }

  public showReport() {

    if (this.validateReport()) {
      /*this.reportManager.directShowReport().then(Response => {
        if (!Response) {
          this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
          this.showReportSetting();
        }
      });*/

      if (!this.reportManager.directShowReport()) {
        this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
        this.showReportSetting();
      }
    }
  }

  public validateReport() {
    if (!this.rowData || this.rowData.total == 0) {
      this.showMessage(this.getText("Report.QuickReportValidate"));
      return false;
    }
    return true;
  }

  public showReportSetting() {
    if (this.validateReport()) {
      this.reportSetting.showReportSetting(this.gridColumns, this.entityTypeName, this.viewId, this.reportManager);
    }
  }

  /**
   *تغییر دوره مالی
   * */
  onChangeFiscalPeriod() {
    var fpId = this.selectedRows[0];
    this.bStorageService.setSelectedFiscalPeriodId(fpId);

    var branchId = this.BranchId ? this.BranchId : this.bStorageService.getSelectedBranchId();

    if (branchId) {
      this.getNewTicket(fpId, branchId);
    }
    else {
      this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), MessageType.Info);
    }
  }

  getNewTicket(fpId: number, branchId: number) {
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
          currentUser.permissions = JSON.parse(atob(this.Ticket)).user.permissions;
          currentUser.fiscalPeriodName = contextInfo.fiscalPeriodName;
          currentUser.branchName = contextInfo.branchName;
          currentUser.companyName = contextInfo.companyName;
          currentUser.ticket = newTicket;
          currentUser.roles = contextInfo.roles;
          this.bStorageService.setCurrentContext(currentUser);
          this.bStorageService.setLastUserBranchAndFpId(this.UserId, this.CompanyId.toString(), branchId.toString(), fpId.toString());

          this.getMenuAndReloadPage(newTicket);
        }
      }
    })

    this.getSetting(fpId);
  }

  getMenuAndReloadPage(ticket: string) {
    this.userService.getCurrentUserCommands(ticket).subscribe((res: Array<Command>) => {
      this.bStorageService.setMenu(res);
      this.bStorageService.removeSelectedBranchAndFiscalPeriod();
      this.router.navigate(['/tadbir/dashboard']);
    });
  }

  getSetting(fpId: number) {
    this.authenticationService.getFiscalPeriodById(fpId, this.Ticket).subscribe(res => {
      this.bStorageService.setFiscalPeriod(res);
      this.bStorageService.removeSelectedDateRange();
    })
  }

  onAdvanceFilterOk(): any {
    this.reloadGrid();
  }
}
