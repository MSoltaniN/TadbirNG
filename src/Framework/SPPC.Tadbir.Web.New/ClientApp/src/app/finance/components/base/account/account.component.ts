//#region Imports
import { Component, ChangeDetectorRef, NgZone, Renderer2, OnInit, ViewChild } from '@angular/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from '@progress/kendo-angular-dialog';
import { AccountFormComponent } from './account-form.component';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier/view-identifier.component';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { GridService, BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { SettingService } from '@sppc/config/service';
import { AccountApi } from '@sppc/finance/service/api';
import { String, AutoGridExplorerComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';
import { SelectFormComponent } from '@sppc/shared/controls';
import { Account } from '@sppc/finance/models';


//#endregion

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'account',
  templateUrl: './account.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class AccountComponent extends AutoGridExplorerComponent<Account> implements OnInit {

  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;


  strSearch: string;
  selectedAccount: Account;


  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, bStorageService, Entities.Account,
      "Account.LedgerAccount", "Account.EditorTitleNew", "Account.EditorTitleEdit",
      AccountApi.EnvironmentAccounts, AccountApi.EnvironmentLedgerAccounts, AccountApi.Account, AccountApi.AccountChildren,
      AccountApi.EnvironmentNewChildAccount, cdref, ngZone)
  }


  ngOnInit(): void {
    this.entityName = Entities.Account;
    this.viewId = ViewName[this.entityTypeName];

    this.treeConfig = this.getViewTreeSettings(this.viewId);
    this.getTreeNode();
    this.reloadGrid();

    //this.cdref.detectChanges();
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {

    var errorMsg = this.getText('Messages.TreeLevelsAreTooDeep');
    var editorTitle = this.getEditorTitle(isNew);

    if (this.levelConfig)
      if (this.levelConfig.isEnabled) {
        this.dialogRef = this.dialogService.open({
          title: editorTitle,
          content: AccountFormComponent,
        });

        this.dialogModel = this.dialogRef.content.instance;
        this.dialogModel.parent = this.parent;
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
      else {
        this.showMessage(String.Format(errorMsg, (this.levelConfig.no - 1).toString()), MessageType.Warning);
      }
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


  openSelectForm() {
    if (this.strSearch) {
      this.dialogRef = this.dialogService.open({
        content: SelectFormComponent,
      });

      this.dialogModel = this.dialogRef.content.instance;

      this.dialogModel.viewID = this.viewId;
      this.dialogModel.strSearch = this.strSearch;
      this.dialogModel.isDisableEntities = true;

      this.dialogRef.content.instance.cancel.subscribe((res) => {
        this.dialogRef.close();
      });

      this.dialogRef.content.instance.result.subscribe((res) => {

        this.selectedAccount = res.dataItem;
        //this.selectedViewId = res.viewId;
        //this.initValue();
        this.dialogRef.close();
      });
    }
  }

  clearSearch() {
    this.strSearch = undefined;
    this.selectedAccount = undefined;
  }
}

