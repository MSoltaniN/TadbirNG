import { Component, OnInit, Renderer2, ViewChild, ChangeDetectorRef, NgZone, ElementRef, OnDestroy } from '@angular/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from '@progress/kendo-angular-dialog';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { CostCenterApi } from '@sppc/finance/service/api';
import { CostCenter } from '@sppc/finance/models';
import { GridService, BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier/view-identifier.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { SettingService } from '@sppc/config/service';
import { CostCenterFormComponent } from './costCenter-form.component';
import { String, AutoGridExplorerComponent } from '@sppc/shared/class';
import { ViewName, CostCenterPermissions } from '@sppc/shared/security';
import { OperationId } from '@sppc/shared/enum/operationId';
import { GridComponent } from '@progress/kendo-angular-grid';
import { ServiceLocator } from '@sppc/service.locator';
import { ShareDataService } from '@sppc/shared/services/share-data.service';
import { lastValueFrom } from 'rxjs';



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


export class CostCenterComponent extends AutoGridExplorerComponent<CostCenter> implements OnInit,OnDestroy {

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  scopes = ["CostCenterComponent","AutoGridExplorerComponent"];

  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone,public elem:ElementRef) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, bStorageService, Entities.CostCenter,
      "CostCenter.LedgerCostCenter", "CostCenter.EditorTitleNew", "CostCenter.EditorTitleEdit",
      CostCenterApi.EnvironmentCostCenters, CostCenterApi.RootCostCenters, CostCenterApi.CostCenter, CostCenterApi.CostCenterChildren,
      CostCenterApi.NewChildCostCenter, cdref, ngZone,elem)

      
    this.scopeService = ServiceLocator.injector.get(ShareDataService);    
    this.scopeService.setScope(this);

  }

  ngOnInit(): void {
    this.entityName = Entities.CostCenter;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = CostCenterApi.EnvironmentCostCenters;
    this.treeConfig = this.getViewTreeSettings(this.viewId);
    this.getTreeNode();
    this.reloadGrid();

  }

  ngOnDestroy(): void {    
    this.scopeService = ServiceLocator.injector.get(ShareDataService);    
    this.scopeService.clearScope(this);
  }


  public onSelectContextmenu({ item }): void {

    let hasPermission: boolean = false;

    switch (item.mode) {
      case 'Remove': {
        hasPermission = this.isAccess(Entities.CostCenter, CostCenterPermissions.Delete);
        if (hasPermission)
          this.contextMenuRemoveHandler();
        break;
      }
      case 'Edit': {
        hasPermission = this.isAccess(Entities.CostCenter, CostCenterPermissions.Edit);
        if (hasPermission) {
          this.contextMenuEditHandler();
          this.selectedContextmenu = undefined;
        }
        break;
      }
      case 'New': {
        hasPermission = this.isAccess(Entities.CostCenter, CostCenterPermissions.Create);
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
        this.dialogModel.errorMessages = undefined;


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

  onAdvanceFilterOk(): any {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }

  updateActiveState(toActivate:boolean) {
    let URL = toActivate == true? CostCenterApi.ReactivateCostCenter: CostCenterApi.DeactivateCostCenter;
    let apiUrl = String.Format(URL,this.selectedRows);
    let model = this.rowData?.data.find(i => i.id == this.selectedRows[0]);

    super.updateActiveState(toActivate,apiUrl,model);
  }
}


