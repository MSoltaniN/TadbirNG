//#region Imports
import { Component, ChangeDetectorRef, NgZone, Renderer2, OnInit, ViewChild } from '@angular/core';
import { Layout, Entities, Metadatas, MessageType } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { SettingService, AccountInfo, GridService } from '../../service';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { AccountApi } from '../../service/api';
import { Account } from '../../model';
import { String } from '../../class/source';
import { DialogService } from '@progress/kendo-angular-dialog';
import { AccountFormComponent } from './account-form.component';
import { ViewName } from '../../security/viewName';
import { GridExplorerComponent } from '../../class/gridExplorer.component';
import { BrowserStorageService } from '../../service/browserStorage.service';
import { AutoGridExplorerComponent } from '../../class/autoGridExplorer.component';
import { ViewIdentifierComponent } from '../viewIdentifier/view-identifier.component';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '../reportManagement/QuickReport-Setting.component';

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

}
