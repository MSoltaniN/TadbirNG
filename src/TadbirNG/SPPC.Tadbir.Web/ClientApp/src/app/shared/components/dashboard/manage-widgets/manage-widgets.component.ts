import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  NgZone,
  OnInit,
  Output,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogRef, DialogService } from "@progress/kendo-angular-dialog";
import { GridComponent } from "@progress/kendo-angular-grid";
import { RTL } from "@progress/kendo-angular-l10n";
import { RelatedItemsInfo } from "@sppc/admin/service";
import { SettingService } from "@sppc/config/service";
import { FullAccountInfo } from "@sppc/finance/service";
import { AutoGeneratedGridComponent, String } from "@sppc/shared/class";
import { ResultOption } from "@sppc/shared/class/result.option";
import {
  ReportViewerComponent,
  ViewIdentifierComponent,
} from "@sppc/shared/components";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import { OperationId } from "@sppc/shared/enum/operationId";
import { RelatedItems } from "@sppc/shared/models";
import { Widget } from "@sppc/shared/models/widget";
import { WidgetFunction } from "@sppc/shared/models/widgetFunction";
import { WidgetType } from "@sppc/shared/models/widgetType";
import { ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  GridService,
  MetaDataService,
} from "@sppc/shared/services";
import { DashboardApi } from "@sppc/shared/services/api";
import { ChartService } from "@sppc/shared/services/chart.service";
import { ToastrService } from "ngx-toastr";
import { lastValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { WidgetInfo, WidgetService } from "../services/widget.service";
import { ManageWidgetsFormComponent } from "./manage-widgets-form/manage-widgets-form.component";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "manage-widgets",
  templateUrl: "./manage-widgets.component.html",
  styles: [
    `
      ::ng-deep .manage-widgets > .k-dialog {
        width: 80%;
      }
      .mb-2 {
        margin: 0 0 2rem 0;
      }
      .mx-1 {
        margin: 0 1rem;
      }
    `,
  ],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class ManageWidgetsComponent
  extends AutoGeneratedGridComponent
  implements OnInit
{
  errorMessage: string;

  @ViewChild(GridComponent, { static: true }) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent)
  reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent)
  reportSetting: QuickReportSettingComponent;

  @Output() close = new EventEmitter();
  public dialogRef: DialogRef;
  public dialogModel: any;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    private widgetService: WidgetService,
    public dialogService: DialogService,
    public settingService: SettingService,
    public ngZone: NgZone,
    public elem: ElementRef,
    private chartService: ChartService
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
      DashboardApi.Widgets,
      DashboardApi.Widget
    );
  }

  accountItem: any;
  selectedItem: any;
  widgetAccoubt: Widget;
  fullAccount: FullAccountInfo;
  functionsList: WidgetFunction[];
  typesList: WidgetType[];
  widgetOwner: any;
  selectedOwner: number;
  confirmDeleteUsedWidgetMsg: string;
  confirmDeleteUsedWidget: boolean = false;
  // Widget Roles
  widgetRolesData: RelatedItemsInfo;
  rolesList: boolean = false;

  ngOnInit() {
    this.entityName = Entities.Dashboard;
    this.viewId = ViewName[this.entityTypeName];
    this.localizeMsg("Widget");
    this.setOwnerList();

    this.getDataUrl = DashboardApi.Widgets;
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  async setOwnerList() {
    let me = await this.translate.get("Widget.MyWidgets").toPromise();
    let all = await this.translate.get("Widget.AllWidgets").toPromise();

    this.widgetOwner = [
      { id: 1, title: me },
      { id: 2, title: all },
    ];

    this.selectedOwner = this.widgetOwner[0].id;
  }

  onChangeWidgetOwner(event) {
    this.getDataUrl =
      event == 1 ? DashboardApi.Widgets : DashboardApi.AllWidgets;
    this.reloadGrid();
    this.selectedRows = [];
  }

  async getFunctions() {
    let res = await this.widgetService
      .getFunctions()
      .pipe(map((res) => res as WidgetFunction[]))
      .toPromise();

    this.functionsList = res;
  }

  async getTypes() {
    let res = await this.widgetService
      .getTypes()
      .pipe(map((res) => res as WidgetType[]))
      .toPromise();

    this.typesList = res;
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  async openEditorDialog(isNew: boolean) {
    await this.getFunctions();
    await this.getTypes();

    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? "Buttons.New" : "Buttons.Edit"),
      content: ManageWidgetsFormComponent,
    });

    this.dialogRef.dialog.location.nativeElement.classList.add("widgetForm");
    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.fullAccount = null;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessages = undefined;

    this.dialogModel.typesList = this.typesList;
    this.dialogModel.functionsList = this.functionsList;

    this.dialogRef.content.instance.save.subscribe((model) => {
      var serviceUrl = isNew
        ? DashboardApi.Widgets
        : String.Format(DashboardApi.Widget, model.id);

      this.saveHandler(model, isNew, this.widgetService, serviceUrl)
        .then((success: ResultOption) => {
          this.chartService.refreshDashboard();
        })
        .catch((error: ResultOption) => {});
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();
      }
    );
  }

  addNew() {
    this.editDataItem = new WidgetInfo();
    this.openEditorDialog(true);
  }

  rolesHandler() {
    if (this.selectedRows.length == 1) {
      this.rolesList = true;
      this.grid.loading = true;
      this.widgetService
        .getWidgetRoles(this.selectedRows[0])
        .subscribe((res) => {
          this.widgetRolesData = res;
          this.grid.loading = false;
        });

      this.errorMessages = undefined;
    }
  }

  saveWidgetRolesHandler(wRoles: RelatedItems) {
    this.grid.loading = true;
    this.gridService.submitted.next(true)
    this.widgetService.modifiedWidgetRoles(wRoles).subscribe(
      (response) => {
        this.rolesList = false;
        this.showMessage(
          this.getText("FiscalPeriod.UpdateRoles"),
          MessageType.Succes
        );
        this.gridService.submitted.next(false)
        this.grid.loading = false;
      },
      (error) => {
        this.grid.loading = false;
        this.gridService.submitted.next(false)
        if (error)
          this.errorMessages = this.errorHandlingService.handleError(error);
      }
    );
  }

  cancelWidgetRolesHandler() {
    this.rolesList = false;
    this.errorMessages = undefined;
  }

  editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.widgetService
      .getById(String.Format(DashboardApi.Widget, recordId))
      .subscribe((res) => {
        this.editDataItem = res;
        this.openEditorDialog(false);
        this.grid.loading = false;
      });
  }

  removeHandler(event) {
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

  deleteModel(confirm: boolean) {
    if (confirm) {
      if (this.groupOperation) {
        //حذف گروهی
      } else {
        //حذف تکی
        this.grid.loading = true;
        this.gridService
          .delete(String.Format(this.modelUrl, this.deleteModelId))
          .subscribe(
            async (response) => {
              if (response != null) {
                this.deleteConfirm = false;
                this.confirmDeleteUsedWidget = true;
                this.confirmDeleteUsedWidgetMsg = response.toString();
              } else {
                this.afterDelete();
                this.grid.loading = false;
              }
            },
            (error) => {
              this.grid.loading = false;
              this.showMessage(
                this.errorHandlingService.handleError(error),
                MessageType.Warning
              );
            }
          );
      }
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }

  deleteUsedWidget(confirm: boolean) {
    if (confirm) {
      //حذف تکی
      let url = String.Format(this.modelUrl, this.deleteModelId) + '?confirmed=true'
      this.grid.loading = true;
      this.gridService
        .delete(url)
        .subscribe(
          async (res) => {
            this.afterDelete();
            this.chartService.refreshDashboard();
            this.grid.loading = false;
          },
          (error) => {
            this.grid.loading = false;
            this.showMessage(
              this.errorHandlingService.handleError(error),
              MessageType.Warning
            );
          }
        );
    }

    this.confirmDeleteUsedWidget = false;
    this.grid.loading = false;
  }

  onAdvanceFilterOk() {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }

  private closeForm(): void {
    this.settingService.setTitle("Entity.Dashboard");
    this.close.emit();
  }

  escPress() {
    this.closeForm();
  }
}
