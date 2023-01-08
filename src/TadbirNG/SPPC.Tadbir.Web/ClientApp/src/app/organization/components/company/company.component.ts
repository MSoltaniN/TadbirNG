import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  NgZone,
  OnInit,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogRef, DialogService } from "@progress/kendo-angular-dialog";
import { GridComponent } from "@progress/kendo-angular-grid";
import { RTL } from "@progress/kendo-angular-l10n";
import { RelatedItemsInfo } from "@sppc/admin/service";
import { SettingService } from "@sppc/config/service";
import { CompanyFormComponent } from "@sppc/organization/components/company/company-form.component";
import { CompanyDbInfo, CompanyService } from "@sppc/organization/service";
import { CompanyApi } from "@sppc/organization/service/api";
import { AutoGeneratedGridComponent, String } from "@sppc/shared/class";
import { ReportViewerComponent, ViewIdentifierComponent } from "@sppc/shared/components";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import { OperationId } from "@sppc/shared/enum/operationId";
import { RelatedItems } from "@sppc/shared/models";
import { ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  GridService,
  MetaDataService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
// import "rxjs/Rx";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "company",
  templateUrl: "./company.component.html",
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class CompanyComponent
  extends AutoGeneratedGridComponent
  implements OnInit
{
  errorMessage: string;
  //editDataItem?: CompanyDb = undefined;

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  public dialogRef: DialogRef;
  public dialogModel: any;

  companyRolesData: RelatedItemsInfo;
  roleList: boolean;
  companyName: string;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    private companyService: CompanyService,
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
      CompanyApi.Companies,
      CompanyApi.Company
    );
  }

  ngOnInit() {
    this.entityName = Entities.Company;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = CompanyApi.Companies;
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {
    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? "Buttons.New" : "Buttons.Edit"),
      content: CompanyFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessages = undefined;

    this.dialogRef.content.instance.save.subscribe((model) => {
      var serviceUrl = isNew
        ? CompanyApi.Companies
        : String.Format(CompanyApi.Company, model.id);
      this.saveHandler(model, isNew, this.companyService, serviceUrl);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();
      }
    );
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

  editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.companyService
      .getById(String.Format(CompanyApi.Company, recordId))
      .subscribe((res) => {
        this.editDataItem = res;
        this.openEditorDialog(false);
        this.grid.loading = false;
      });
  }

  cancelHandler() {
    this.editDataItem = undefined;
    this.errorMessages = undefined;
  }

  addNew() {
    this.editDataItem = new CompanyDbInfo();
    this.openEditorDialog(true);
  }

  onAdvanceFilterOk() {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }

  rolesHandler(companyId: number, companyName: string) {
    if (this.IsAdmin) {
      this.roleList = true;
      this.grid.loading = true;
      this.companyService.getCompanyRoles(companyId).subscribe((res) => {
        this.companyRolesData = res;
        this.companyName = companyName;
        this.grid.loading = false;
      });

      this.errorMessages = undefined;
    } else {
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
    }
  }

  saveCompanyRoles(companyRoles: RelatedItems) {
    this.grid.loading = true;
    this.companyService.modifiedCompanyRoles(companyRoles).subscribe(
      (response) => {
        this.roleList = false;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.grid.loading = false;
      },
      (error) => {
        this.grid.loading = false;
        this.errorMessages = this.errorHandlingService.handleError(error);
      }
    );
  }

  cancelCompanyRoles() {
    this.roleList = false;
    this.errorMessages = undefined;
    this.companyName = "";
  }
}
