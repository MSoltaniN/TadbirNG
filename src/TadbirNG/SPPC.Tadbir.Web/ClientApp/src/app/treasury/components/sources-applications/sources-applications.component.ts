import { HttpResponse } from '@angular/common/http';
import { ChangeDetectorRef,
  Component,
  ElementRef,
  NgZone,
  OnInit,
  Renderer2,
  ViewChild }
 from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from '@progress/kendo-angular-dialog';
import { GridComponent } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { SettingService } from '@sppc/config/service';
import { AutoGeneratedGridComponent, String } from '@sppc/shared/class';
import { ReportViewerComponent, ViewIdentifierComponent } from '@sppc/shared/components';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { Entities, Layout, MessageType } from '@sppc/shared/enum/metadata';
import { OperationId } from '@sppc/shared/enum/operationId';
import { RelatedItems } from '@sppc/shared/models';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, GridService, MetaDataService } from '@sppc/shared/services';
import { SourceApp } from '@sppc/treasury/models/soucrceApp';
import { SourceAppApi } from '@sppc/treasury/service/api';
import { SourceAppInfo, SourceAppService } from '@sppc/treasury/service/source-app.service';
import { ToastrService } from 'ngx-toastr';
import { lastValueFrom } from 'rxjs';
import { SourceAppFormComponent } from './sourceApp-form/sourceApp-form.component';


export function getLayoutModule(layout: Layout) {
 return layout.getLayout();
}

@Component({
  selector: 'app-sources-applications',
  templateUrl: './sources-applications.component.html',
  styleUrls: ['./sources-applications.component.css'],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class SourcesApplicationsComponent extends AutoGeneratedGridComponent implements OnInit {

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    private sourceAppService: SourceAppService,
    public dialogService: DialogService,
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
      SourceAppApi.SourceApps,
      SourceAppApi.SourceApp
    );
  }

  ngOnInit(): void {
    this.entityName = Entities.SourceApp;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = SourceAppApi.SourceApps;
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  
  addNew() {
    this.editDataItem = new SourceAppInfo();
    this.openEditorDialog(true);
  }

  editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.sourceAppService
      .getById(String.Format(SourceAppApi.SourceApp, recordId))
      .subscribe((res) => {
        this.editDataItem = res;
        this.openEditorDialog(false);
        this.grid.loading = false;
      });
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (this.groupOperation) {
      this.prepareDeleteConfirm(this.getText("Messages.SelectedItems"));
    } else {
      var recordId = this.selectedRows[0];
      var record = this.rowData.data.find((f) => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  cancelHandler() {
    this.editDataItem = undefined;
    this.errorMessages = undefined;
  }

  onAdvanceFilterOk() {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  async openEditorDialog(isNew: boolean) {
    let preferedCode = await this.getPreferedCode();

    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? "SourceApp.New" : "SourceApp.Edit"),
      content: SourceAppFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.preferedCode = preferedCode;
    this.dialogModel.errorMessages = undefined;

    this.dialogRef.content.instance.save.subscribe((model) => {
      var serviceUrl = isNew
        ? SourceAppApi.SourceApps
        : String.Format(SourceAppApi.SourceApp, model.id);
      this.saveHandler(model, isNew, this.sourceAppService, serviceUrl);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();
      }
    );
  }

  async getPreferedCode() {
    let data:HttpResponse<SourceApp[]> = await lastValueFrom(this.sourceAppService.getAll(SourceAppApi.SourceApps));
    let code = data.body[data.body.length-1]?.code;
    return code;
  }
}
