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
} from "@progress/kendo-angular-grid";
import { SettingService } from "@sppc/config/service";
import { VoucherLineFormComponent } from "@sppc/finance/components/operational/voucherLine/voucherLine-form.component";
import { FullAccount } from "@sppc/finance/models";
import { FullAccountInfo } from "@sppc/finance/service";
import { AutoGeneratedGridComponent, String } from "@sppc/shared/class";
import { Entities, MessageType } from "@sppc/shared/enum/metadata";
import { ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  GridService,
  MetaDataService,
} from "@sppc/shared/services";
import { PayReceiveOperations } from "@sppc/treasury/enums/payReceive";
import { PayReceiveCashAccount } from "@sppc/treasury/models/payReceiveAccount";
import {
  PaymentCashAccountApi,
  ReceiptCashAccountApi,
} from "@sppc/treasury/service/api/payReceiveCashAccountApi";
import { PayReceiveService } from "@sppc/treasury/service/pay-receive.service";
import { ToastrService } from "ngx-toastr";
import { take } from "rxjs";

@Component({
  selector: "cash-account",
  templateUrl: "./cash-account.component.html",
  styleUrls: ["./cash-account.component.css"],
})
export class CashAccountComponent extends AutoGeneratedGridComponent
  implements OnInit,OnChanges {

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

  @ViewChild(GridComponent, { static: true }) grid: GridComponent;
  @ViewChild("itemListRef") accountDetails: TemplateRef<any>;

  public rowData: GridDataResult;

  @Input() isNew: boolean;
  /**
   * 1 fo peyments and 2 for reciepts
   */
  @Input() set payReceiptType(value) {
    this._payReceiptType = value;
    if (value == "2") this.CashAccountApi = ReceiptCashAccountApi;
    else this.CashAccountApi = PaymentCashAccountApi;

    this.environmentModelsUrl = this.CashAccountApi.AllCashAccountArticles;
  }

  @Input("selectedAccounts") set selectedAccounts(accounts: FullAccount[]) {
    setTimeout(() => {
      if (accounts) {
        this.accounts = accounts;
        let total = accounts.length;
        this.rowData = {
          data: accounts,
          total: total,
        };
      }
    }, 0);
  }

  @Input() isConfirmed: boolean; 
  @Input() payReceiveId: number;
  /**
   * 1 fo peyments and 2 for reciepts
   */
  public get payReceiptType(): "1" | "2" {
    return this._payReceiptType;
  }

  private _payReceiptType: "1" | "2";
  payReceiveOperationsItems = PayReceiveOperations;
  CashAccountApi: typeof PaymentCashAccountApi;
  selectedItem: any;
  fullAccount: FullAccountInfo = new FullAccountInfo();
  accounts: FullAccount[] = [];
  editMode = false;
  models = [];
  confirmDialog = false;
  confirmDialogTitle: string;
  confirmMsg: string;
  selectedOperation: number;
  isInitialized = false;

  @Output() setTotalCashAmount: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.entityName = Entities.PayReceiveCashAccount;
    this.viewId = ViewName[this.entityTypeName];

    this.pageSize = 5;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.payReceiveId && !changes.payReceiveId.firstChange) {
      this.getDataUrl = String.Format(this.CashAccountApi.CashAccountArticles,this.payReceiveId);

      this.reloadGrid();
    }
  }

  onDataBind(res: any): void {
    if (this.isInitialized)
      this.setTotalCashAmount.emit();
    
    this.isInitialized = true;
  }

  addNew() {
    this.openEditorDialog(true);
  }

  getSelectedRow(item: RowArgs) {
    return item.index;
  }

  editHandler(event) {
    this.editMode = true;
    this.fullAccount = this.accounts[this.selectedRows[0]];
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
      title: this.getText(isNew ? "Buttons.New" : "Buttons.Edit"),
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

        // this.setFocus.emit();
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
      sourceAppId: viewModel.model.sourceAppId
    };

    let isOpen = viewModel.isOpen;

    var promise = new Promise((resolve) => {
      let serviceUrl = isNew
        ? this.getDataUrl
        : String.Format(this.CashAccountApi.CashAccountArticle, viewModel.model.id);

        this.saveHandler(model, isNew, this.payReceiveService, serviceUrl)
        .then((success) => {
          resolve(true);
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
    this.closeDialog();
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

  pageChange(event: PageChangeEvent): void {
    this.isInitialized = false;
    this.listChanged = false;
    this.skip = event.skip;
    this.pageSize = event.take;
    this.setPageSizeByViewId();
    this.reloadGrid();
  }

}
