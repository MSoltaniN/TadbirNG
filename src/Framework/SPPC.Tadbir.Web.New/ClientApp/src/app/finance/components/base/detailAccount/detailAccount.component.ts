import { Component, Renderer2, OnInit, ChangeDetectorRef, NgZone, ViewChild } from '@angular/core';
import { Layout, Entities, MessageType } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { SettingService, GridService } from '../../service';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailAccountApi } from '../../service/api';
import { DetailAccount } from '../../model';
import { String } from '../../class/source';
import { DialogService } from '@progress/kendo-angular-dialog';
import { ViewName } from '../../security/viewName';
import { DetailAccountFormComponent } from './detailAccount-form.component';
import { BrowserStorageService } from '../../service/browserStorage.service';
import { AutoGridExplorerComponent } from '../../class/autoGridExplorer.component';
import { ViewIdentifierComponent } from '../viewIdentifier/view-identifier.component';
import { QuickReportSettingComponent } from '../reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'detailAccount',
  templateUrl: './detailAccount.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class DetailAccountComponent extends AutoGridExplorerComponent<DetailAccount> implements OnInit {

  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, bStorageService, Entities.DetailAccount,
      "DetailAccount.LedgerDetailAccount", "DetailAccount.EditorTitleNew", "DetailAccount.EditorTitleEdit",
      DetailAccountApi.EnvironmentDetailAccounts, DetailAccountApi.EnvironmentDetailAccountsLedger, DetailAccountApi.DetailAccount, DetailAccountApi.DetailAccountChildren,
      DetailAccountApi.EnvironmentNewChildDetailAccount, cdref, ngZone)
  }


  ngOnInit(): void {
    this.entityName = Entities.DetailAccount;
    this.viewId = ViewName[this.entityTypeName];

    this.treeConfig = this.getViewTreeSettings(this.viewId);
    this.getTreeNode();
    this.reloadGrid();

  }


  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {

    var errorMsg = this.getText('Messages.TreeLevelsAreTooDeep');
    var editorTitle = this.getEditorTitle(isNew);

    if (this.levelConfig)
      if (this.levelConfig.isEnabled) {
        this.dialogRef = this.dialogService.open({
          title: editorTitle,
          content: DetailAccountFormComponent,
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

