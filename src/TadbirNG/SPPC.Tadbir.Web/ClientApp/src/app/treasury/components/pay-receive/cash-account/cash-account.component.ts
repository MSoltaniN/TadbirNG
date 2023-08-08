import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  NgZone,
  OnChanges,
  OnInit,
  Output,
  Renderer2,
  SimpleChanges,
  TemplateRef,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import {
  DialogCloseResult,
  DialogService,
} from "@progress/kendo-angular-dialog";
import {
  GridComponent,
  GridDataResult,
  PageChangeEvent,
  RowArgs,
  SelectAllCheckboxState,
} from "@progress/kendo-angular-grid";
import { SettingService } from "@sppc/config/service";
import { VoucherLineFormComponent } from "@sppc/finance/components/operational/voucherLine/voucherLine-form.component";
import { FullAccount } from "@sppc/finance/models";
import { FullAccountInfo } from "@sppc/finance/service";
import { AutoGeneratedGridComponent, String } from "@sppc/shared/class";
import { ReportViewerComponent, ViewIdentifierComponent } from "@sppc/shared/components";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { Entities, MessageType } from "@sppc/shared/enum/metadata";
import { OperationId } from "@sppc/shared/enum/operationId";
import { ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  GridService,
  MetaDataService,
} from "@sppc/shared/services";
import { PayReceiveOperations } from "@sppc/treasury/enums/payReceive";
import { TreasuryOperationId } from "@sppc/treasury/enums/treasuryOperationID";
import { PayReceiveCashAccount } from "@sppc/treasury/models/PayReceiveCashAccount";
import {
  PaymentCashAccountApi,
  ReceiptCashAccountApi,
} from "@sppc/treasury/service/api/payReceiveCashAccountApi";
import { PayReceiveService } from "@sppc/treasury/service/pay-receive.service";
import { ToastrService } from "ngx-toastr";
import { Observable, lastValueFrom, take } from "rxjs";

@Component({
  selector: "cash-account",
  templateUrl: "./cash-account.component.html",
  styleUrls: ["./cash-account.component.css"],
})
export class CashAccountComponent extends AutoGeneratedGridComponent
  implements OnInit,OnChanges {

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
    public dialogService: DialogService,
    public settingService: SettingService,
    private payReceiveService: PayReceiveService,
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
      PaymentCashAccountApi.CashAccountArticles
    );
  }

  public rowData: GridDataResult;

  @Input() isNew: boolean;
  /**
   * 1 fo peyments and 2 for reciepts
   */
  @Input() set payReceiptType(value) {
    this._payReceiptType = value;
    if (value == "0") this.CashAccountApi = ReceiptCashAccountApi;
    else this.CashAccountApi = PaymentCashAccountApi;

    this.environmentModelsUrl = this.CashAccountApi.AllCashAccountArticles;
    this.permissionEntityTypeName = value == '1'? Entities.Payment: Entities.Receipt;
  }

  @Input() disableOperations: boolean; 
  @Input() payReceiveId: number;
  /**
   * 1 fo peyments and 2 for reciepts
   */
  public get payReceiptType(): "1" | "0" {
    return this._payReceiptType;
  }

  private _payReceiptType: "1" | "0";
  payReceiveOperationsItems = PayReceiveOperations;
  CashAccountApi: typeof PaymentCashAccountApi;
  selectedItem: any;
  editMode = false;
  confirmDialog = false;
  confirmDialogTitle: string;
  confirmMsg: string;
  selectedOperation: number;
  permissionEntityTypeName: string;
  operationItems = TreasuryOperationId;

  @Output() setTotalAmount: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.entityName = Entities.PayReceiveCashAccount;
    this.viewId = ViewName[this.entityTypeName];

    this.pageSize = 1000;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.payReceiveId && !changes.payReceiveId.firstChange) {
      this.getDataUrl = String.Format(this.CashAccountApi.CashAccountArticles,this.payReceiveId);

      this.reloadGrid();
    }
  }

  onDataBind(res: PayReceiveCashAccount[]): void {
    let totalCashAmount = 0;
    res.forEach(i => {
      totalCashAmount += i.amount;
    });
    this.setTotalAmount.emit({totalCashAmount});
  }

  addNew() {
    this.editDataItem = new PayReceiveCashAccount();
    this.openEditorDialog(true);
  }

  async editHandler(event) {
    this.editMode = true;

    this.editDataItem = await lastValueFrom(this.payReceiveService.getById(this.modelUrl));
    this.openEditorDialog(false);
  }

  removeHandler() {
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

  openEditorDialog(isNew: boolean) {
    this.dialogRef = this.dialogService.open({
      title: String.Format(this.getText(isNew ? "Form.New" : "Form.Edit"),this.getText("Entity.PayReceiveCashAccount")),
      content: VoucherLineFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.errorMessages = undefined;
    this.dialogModel.isNew = isNew;
    this.dialogModel.isNewBalance = true; //this.isNewBalance;
    this.dialogModel.isPayReciept = true;
    this.dialogModel.isSourceApp = true;
    this.dialogModel.creditDebiteMode = this.payReceiptType;

    this.dialogRef.content.instance.save.subscribe((viewModel) => {
      this.onSave(viewModel,isNew);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();

        this.dialogModel.errorMessages = undefined;
        this.dialogModel.model = undefined;
      }
    );

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        // this.isNewBalance = false;
        // this.setFocus.emit();
      }
    });

    this.dialogRef.content.instance.setFocus.subscribe((res) => {
      //this.dialogRef.dialog.instance.focus();
    });
  }

  onSave(viewModel,isNew:boolean) {

    let model: PayReceiveCashAccount = {
      id: viewModel.model.id? viewModel.model.id: 0,
      amount: viewModel.model.amount,
      payReceiveId: this.payReceiveId,
      fullAccount: viewModel.model.fullAccount,
      remarks: viewModel.model.remarks,
      isBank: viewModel.model.isBank,
      bankOrderNo: viewModel.model.bankOrderNo,
      sourceAppId: viewModel.model.sourceAppId,
      sourceAppName: ''
    };

    let isOpen = viewModel.isOpen;

    var promise = new Promise((resolve) => {
      let serviceUrl = isNew
        ? this.getDataUrl
        : String.Format(this.CashAccountApi.CashAccountArticle, viewModel.model.id);

        this.saveHandler(model, isNew, this.payReceiveService, serviceUrl)
        .then((success) => {
          resolve(true);
          this.closeDialog();
        })
        .catch((error) => {
          // error handler is called
          this.dialogModel.isEnableSaveBtn = true;
        });;
    });

    promise.then(() => {
      if (isOpen) {
        this.dialogRef.close();
        setTimeout(() => {
          this.addNew();
        }, 500);
      }
    });
  }

  callOperation(operation:number) {
    switch (operation) {
      case PayReceiveOperations.Aggregate:
        this.confirmDialog = true;
        this.selectedOperation = operation;
        this.confirmDialogTitle = "PayReceipt.AggregateRows";
        this.confirmMsg = "Messages.AggregateConfirm"
        break;
      case PayReceiveOperations.RemoveInvalidRows:
        this.confirmDialog = true;
        this.selectedOperation = operation;
        this.confirmDialogTitle = "PayReceipt.RemoveInvalidRows";
        this.confirmMsg = "Messages.RemoveInvalidRowsConfirm"
        break;

      default:
        break;
    }
  }

  confirmOperation(status:boolean) {
    if (status) {
      switch (this.selectedOperation) {
        case PayReceiveOperations.Aggregate:
          this.aggregateRows();
          break;
        case PayReceiveOperations.RemoveInvalidRows:
          this.removeInvalidRows();
          break;
  
        default:
          break;
      }
    } else {
      this.selectedOperation = undefined;
    }
    this.confirmDialog = false;
  }

  aggregateRows() {
    let apiUrl = String.Format(this.CashAccountApi.AggregateCashAccountArticleRows,this.payReceiveId)

    this.payReceiveService.changeStatus(apiUrl).pipe(
      take(2)
    ).subscribe({
      next: (res) => {
        this.reloadGrid();
      },
      error: (error) => {
        this.showMessage(
          this.errorHandlingService.handleError(error),
          MessageType.Warning
        );
      }
    });
  }

  removeInvalidRows() {
    let apiUrl = String.Format(this.CashAccountApi.RemoveCashAccountInvalidRows,this.payReceiveId)

    this.payReceiveService.delete(apiUrl).pipe(
      take(2)
    ).subscribe({
      next: (res) => {
        this.reloadGrid();
      },
      error: (error) => {
        this.showMessage(
          this.errorHandlingService.handleError(error),
          MessageType.Warning
        );
      }
    });
  }

  closeDialog() {
    this.dialogRef.close();
  }

  getSelectedRow(item: RowArgs) {
    return item.dataItem.id;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    setTimeout(() => {
      if (this.selectedRows.length > 1) this.groupOperation = true;
      else {
        this.groupOperation = false;

        this.modelUrl = String.Format(this.CashAccountApi.CashAccountArticle,this.selectedRows[0]);
      }
    }, 0);
  }

  onAdvanceFilterOk(): any {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }

  /**
   * for set custom operationId in excel export
   */
  public allData = (): Observable<any> => {
    this.excelFileName = this.getExcelFileName();
    return this.getExportData(TreasuryOperationId.ExportCashAccountLines);
  };
}
