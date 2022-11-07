//#region Imports
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
import { DialogService } from "@progress/kendo-angular-dialog";
import { GridComponent } from "@progress/kendo-angular-grid";
import { RTL } from "@progress/kendo-angular-l10n";
import { SettingService } from "@sppc/config/service";
import { Account } from "@sppc/finance/models";
import { AccountFullData } from "@sppc/finance/models/accountFullData";
import { AccountApi } from "@sppc/finance/service/api";
import { AutoGridExplorerComponent, String } from "@sppc/shared/class";
import { ReloadOption } from "@sppc/shared/class/reload-option";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { ViewIdentifierComponent } from "@sppc/shared/components/viewIdentifier/view-identifier.component";
import { SelectFormComponent } from "@sppc/shared/controls";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import { OperationId } from "@sppc/shared/enum/operationId";
import { AccountPermissions, ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  GridService,
  MetaDataService,
} from "@sppc/shared/services";
import { ShareDataService } from "@sppc/shared/services/share-data.service";
import { ToastrService } from "ngx-toastr";
import { AccountFormComponent } from "./account-form.component";

//#endregion

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "account",
  templateUrl: "./account.component.html",
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class AccountComponent
  extends AutoGridExplorerComponent<Account>
  implements OnInit
{
  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent, {static: true})
  reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true})
  reportSetting: QuickReportSettingComponent;

  strSearch: string;
  selectedAccount: Account;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public service: GridService,
    public dialogService: DialogService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public ngZone: NgZone,
    public elem: ElementRef,
    private sharedDataService: ShareDataService
  ) {
    super(
      toastrService,
      translate,
      service,
      dialogService,
      renderer,
      metadata,
      settingService,
      bStorageService,
      Entities.Account,
      "Account.LedgerAccount",
      "Account.EditorTitleNew",
      "Account.EditorTitleEdit",
      AccountApi.EnvironmentAccounts,
      AccountApi.LedgerAccounts,
      AccountApi.Account,
      AccountApi.AccountChildren,
      AccountApi.NewChildAccount,
      cdref,
      ngZone,
      elem
    );
  }

  ngOnInit(): void {
    this.entityName = Entities.Account;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = AccountApi.EnvironmentAccounts;
    this.treeConfig = this.getViewTreeSettings(this.viewId);
    this.getTreeNode();
    this.reloadGrid();

    //this.cdref.detectChanges();
  }

  public onSelectContextmenu({ item }): void {
    let hasPermission: boolean = false;

    switch (item.mode) {
      case "Remove": {
        hasPermission = this.isAccess(
          Entities.Account,
          AccountPermissions.Delete
        );
        if (hasPermission) this.contextMenuRemoveHandler();
        break;
      }
      case "Edit": {
        hasPermission = this.isAccess(
          Entities.Account,
          AccountPermissions.Edit
        );
        if (hasPermission) {
          this.contextMenuEditHandler();
          this.selectedContextmenu = undefined;
        }
        break;
      }
      case "New": {
        hasPermission = this.isAccess(
          Entities.Account,
          AccountPermissions.Create
        );
        if (hasPermission) {
          this.contextMenuAddNewHandler();
          this.selectedContextmenu = undefined;
        }
        break;
      }
      default:
    }

    if (!hasPermission)
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {
    var errorMsg = this.getText("Messages.TreeLevelsAreTooDeep");
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
        this.dialogModel.errorMessages = undefined;

        this.dialogRef.content.instance.save.subscribe((res) => {
          this.saveHandler(res, isNew);
        });

        const closeForm = this.dialogRef.content.instance.cancel.subscribe(
          (res) => {
            this.dialogRef.close();
          }
        );
      } else {
        this.showMessage(
          String.Format(errorMsg, (this.levelConfig.no - 1).toString()),
          MessageType.Warning
        );
      }
  }

  saveHandler(model: any, isNew: boolean) {
    this.grid.loading = true;
    this.gridService.submitted.next(true)
    if (!isNew) {
      this.service
        .edit<AccountFullData>(
          String.Format(this.modelUrl, model.account.id),
          model
        )
        .subscribe(
          (response) => {
            this.editDataItem = undefined;
            this.showMessage(this.updateMsg, MessageType.Succes);

            this.dialogRef.close();
            this.dialogModel.parent = undefined;
            this.dialogModel.errorMessages = undefined;
            this.dialogModel.model = undefined;
            //log is off after update model
            this.listChanged = false;
            this.reloadGrid();
            this.selectedRows = [];
            this.gridService.submitted.next(false)

            this.highLightNewRow(model.account);
            this.refreshTreeNodes(model.account);
          },
          (error) => {
            this.editDataItem = model;
            this.gridService.submitted.next(false)
            //this.dialogModel.errorMessages = this.errorHandlingService.handleError(error);
            if (error)
              this.dialogModel.errorMessages =
                this.errorHandlingService.handleError(error);
          }
        );
    } else {
      this.service
        .insert<AccountFullData>(this.environmentModelsUrl, model)
        .subscribe(
          (response: any) => {
            this.editDataItem = undefined;
            this.showMessage(this.insertMsg, MessageType.Succes);
            var insertedModel = response.account;

            this.dialogRef.close();
            this.dialogModel.parent = undefined;
            this.dialogModel.errorMessages = undefined;
            this.dialogModel.model = undefined;

            this.selectedRows = [];
            //log is off after update insert
            this.listChanged = false;

            var options = new ReloadOption();
            options.InsertedModel = insertedModel;
            this.reloadGrid(options);
            this.gridService.submitted.next(false)
            this.highLightNewRow(insertedModel);
            this.refreshTreeNodes(insertedModel);
          },
          (error) => {
            this.gridService.submitted.next(false)
            if (error)
              this.dialogModel.errorMessages =
                this.errorHandlingService.handleError(error);
          }
        );
    }
    this.grid.loading = false;
  }

  editHandler() {
    var recordId = this.selectedRows[0]; //.id;

    this.grid.loading = true;
    this.service
      .getById(String.Format(AccountApi.AccountFullData, recordId))
      .subscribe((res) => {
        this.editDataItem = res;
        this.openEditorDialog(false);

        this.grid.loading = false;
      });
  }

  public showReport() {
    if (this.validateReport()) {
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
      this.reportSetting.showReportSetting(
        this.gridColumns,
        this.entityTypeName,
        this.viewId,
        this.reportManager
      );
    }
  }

  openSelectForm() {
    if (this.strSearch) {
      this.dialogRef = this.dialogService.open({
        content: SelectFormComponent,
      });

      this.sharedDataService.selectFormTitle.subscribe((title: string) => {
        this.dialogRef.dialog.instance.title = title;
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

  onAdvanceFilterOk(): any {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }
}
