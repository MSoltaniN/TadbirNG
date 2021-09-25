import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone, ViewChild } from '@angular/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from '@progress/kendo-angular-dialog';
import { String, AutoGridExplorerComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { ProjectApi } from '@sppc/finance/service/api';
import { Project } from '@sppc/finance/models';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier/view-identifier.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { GridService, MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { ProjectFormComponent } from './project-form.component';
import { ViewName, ProjectPermissions } from '@sppc/shared/security';
import { OperationId } from '@sppc/shared/enum/operationId';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'project',
  templateUrl: './project.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class ProjectComponent extends AutoGridExplorerComponent<Project> implements OnInit{

  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, bStorageService, Entities.Project,
      "Project.LedgerProject", "Project.EditorTitleNew", "Project.EditorTitleEdit",
      ProjectApi.EnvironmentProjects, ProjectApi.RootProjects, ProjectApi.Project, ProjectApi.ProjectChildren, ProjectApi.NewChildProject,
      cdref, ngZone)
  }

  ngOnInit(): void {
    this.entityName = Entities.Project;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = ProjectApi.EnvironmentProjects;
    this.treeConfig = this.getViewTreeSettings(this.viewId);
    this.getTreeNode();
    this.reloadGrid();

    //this.cdref.detectChanges();
  }

  public onSelectContextmenu({ item }): void {

    let hasPermission: boolean = false;

    switch (item.mode) {
      case 'Remove': {
        hasPermission = this.isAccess(Entities.Project, ProjectPermissions.Delete);
        if (hasPermission)
          this.contextMenuRemoveHandler();
        break;
      }
      case 'Edit': {
        hasPermission = this.isAccess(Entities.Project, ProjectPermissions.Edit);
        if (hasPermission) {
          this.contextMenuEditHandler();
          this.selectedContextmenu = undefined;
        }
        break;
      }
      case 'New': {
        hasPermission = this.isAccess(Entities.Project, ProjectPermissions.Create);
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
          content: ProjectFormComponent,
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


  //public showReport() {

  //  if (this.validateReport()) {
  //    /*this.reportManager.directShowReport().then(Response => {
  //      if (!Response) {
  //        this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
  //        this.showReportSetting();
  //      }
  //    });*/


  //    if (!this.reportManager.directShowReport()) {
  //      this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
  //      this.showReportSetting();
  //    }
  //  }
  //}

  //public validateReport() {
  //  if (!this.rowData || this.rowData.total == 0) {
  //    this.showMessage(this.getText("Report.QuickReportValidate"));
  //    return false;
  //  }
  //  return true;
  //}

  //public showReportSetting() {
  //  if (this.validateReport()) {
  //    this.reportSetting.showReportSetting(this.gridColumns, this.entityTypeName, this.viewId, this.reportManager);
  //  }
  //}

  onAdvanceFilterOk(): any {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }

}

