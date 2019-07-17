import { Component, OnInit, Renderer2, ViewChild, ChangeDetectorRef, NgZone } from '@angular/core';
import { Layout, Entities, MessageType } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { SettingService, GridService } from '../../service';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { CostCenterApi } from '../../service/api';
import { CostCenter } from '../../model';
import { String } from '../../class/source';
import { DialogService } from '@progress/kendo-angular-dialog';
import { ViewName } from '../../security/viewName';
import { CostCenterFormComponent } from './costCenter-form.component';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { ViewIdentifierComponent } from '../viewIdentifier/view-identifier.component';
import { BrowserStorageService } from '../../service/browserStorage.service';
import { AutoGridExplorerComponent } from '../../class/autoGridExplorer.component';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'costCenter',
  templateUrl: './costCenter.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class CostCenterComponent extends AutoGridExplorerComponent<CostCenter> implements OnInit {

  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, bStorageService, Entities.CostCenter,
      "CostCenter.LedgerCostCenter", "CostCenter.EditorTitleNew", "CostCenter.EditorTitleEdit",
      CostCenterApi.EnvironmentCostCenters, CostCenterApi.EnvironmentCostCentersLedger, CostCenterApi.CostCenter, CostCenterApi.CostCenterChildren,
      CostCenterApi.EnvironmentNewChildCostCenter, cdref, ngZone)
  }

  ngOnInit(): void {
    this.entityName = Entities.CostCenter;
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
          content: CostCenterFormComponent,
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
    this.reportManager.DecisionMakingForShowReport();
  }


}


