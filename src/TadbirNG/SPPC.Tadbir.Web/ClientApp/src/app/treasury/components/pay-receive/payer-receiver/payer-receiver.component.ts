import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, NgZone, OnChanges, OnInit, Output, Renderer2, SimpleChanges, TemplateRef, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DialogCloseResult, DialogService } from '@progress/kendo-angular-dialog';
import { GridComponent, GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SettingService } from '@sppc/config/service';
import { VoucherLineFormComponent } from '@sppc/finance/components/operational/voucherLine/voucherLine-form.component';
import { AccountRelationsType } from '@sppc/finance/enum';
import { FullAccount, VoucherLine } from '@sppc/finance/models';
import { FullAccountInfo } from '@sppc/finance/service';
import { AutoGeneratedGridComponent, String } from '@sppc/shared/class';
import { Entities, MessageType } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, GridService, MetaDataService } from '@sppc/shared/services';
import { PayReceiveOperations } from '@sppc/treasury/enums/payReceive';
import { PayReceiveAccount } from '@sppc/treasury/models/payReceiveAccount';
import { PayReceiveAccountApi } from '@sppc/treasury/service/api/payReceiveAccountApi';
import { PayReceiveService } from '@sppc/treasury/service/pay-receive.service';
import { ToastrService } from 'ngx-toastr';
import { lastValueFrom, take } from 'rxjs';

@Component({
  selector: 'payer-receiver',
  templateUrl: './payer-receiver.component.html',
  styleUrls: ['./payer-receiver.component.css']
})
export class PayerReceiverComponent extends AutoGeneratedGridComponent implements OnInit,OnChanges {

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
    );
  }

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild('itemListRef') accountDetails: TemplateRef<any>;


  @Input() isNew: boolean;
  @Input() accountRequired;
  /**
  * 1 fo peyments and 2 for reciepts
  */
  @Input() set type(value) {
    this._type = value;
  }
  @Input() payReceiveId: number;
  @Input() isConfirmed: boolean;  
  @Output() setTotalCashAmount: EventEmitter<any> = new EventEmitter();

  /**
  * 1 fo peyments and 2 for reciepts
  */
  public get type() : '1'|'2' {
    return this._type;
  }
  
  private _type: '1'|'2';
  payReceiveOperationsItems = PayReceiveOperations;
  rowData: GridDataResult;
  accountItem: any;
  selectedItem: any;
  editMode = false;
  model: PayReceiveAccount;
  totalCashAmount:number = 0;
  confirmDialog = false;
  confirmDialogTitle: string;
  confirmMsg: string;
  selectedOperation: number;

  ngOnInit() {
    this.entityName = Entities.PayReceiveAccount
    this.viewId = ViewName[this.entityTypeName];

    this.accountItem = AccountRelationsType;
    this.pageSize = 5;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.payReceiveId && !changes.payReceiveId.firstChange) {
      this.reload();
    }
  }

  reload() {
    if (this.type == '1') {
      this.getDataUrl = String.Format(PayReceiveAccountApi.PaymentAccountArticles,this.payReceiveId);
      this.environmentModelsUrl = PayReceiveAccountApi.AllPaymentAccountArticles;
    } else {
      this.getDataUrl = String.Format(PayReceiveAccountApi.ReceiptAccountArticles,this.payReceiveId);
      this.environmentModelsUrl = PayReceiveAccountApi.AllReceiptAccountArticles;
    }

    this.reloadGrid();
  }

  onDataBind(res: PayReceiveAccount[]): void {
    let sum = 0;

    res.forEach(model => {
      sum += model.amount;
    });

    this.setTotalCashAmount.emit(sum);
  }

  addNew() {
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
      title: this.getText(isNew ? "Buttons.New" : "Buttons.Edit"),
      content: VoucherLineFormComponent,
    });


    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.errorMessages = undefined;
    this.dialogModel.isNew = isNew;
    this.dialogModel.creditDebiteMode = this.type;
    this.dialogModel.isNewBalance = true;
    this.dialogModel.isPayReciept = true;
    this.dialogModel.balance = this.type == '1'? -this.editDataItem?.amount: this.editDataItem?.amount;


    this.dialogRef.content.instance.save.subscribe((viewModel) => {
      let model: PayReceiveAccount = {
        id: viewModel.model.id? viewModel.model.id: 0,
        amount: 0,
        payReceiveId: this.payReceiveId,
        fullAccount: viewModel.model.fullAccount,
        description: viewModel.model.description,
      };
  
      if (this.type == '1') {
        model.amount = +viewModel.model.debit;
      } else {
        model.amount = +viewModel.model.credit;
      }
      let isOpen = viewModel.isOpen;

      var promise = new Promise((resolve) => {
        let serviceUrl = isNew
          ? this.getDataUrl
          : String.Format(this.type == '1'? PayReceiveAccountApi.PaymentArticle: PayReceiveAccountApi.ReceiptArticle, viewModel.model.id);

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

  closeDialog() {
    this.dialogRef.close();
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
    let apiUrl = this.type == '1' ?
      String.Format(PayReceiveAccountApi.AggregatePaymentAccountArticleRows,this.payReceiveId) :
      String.Format(PayReceiveAccountApi.AggregateReceiptAccountArticleRows, this.payReceiveId);

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
    let apiUrl = this.type == '1' ?
      String.Format(PayReceiveAccountApi.RemovePaymentAccountInvalidRows,this.payReceiveId):
      String.Format(PayReceiveAccountApi.RemoveReceiptAccountInvalidRows,this.payReceiveId);

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

  closeAlert() {
    this.errorMessages = [];
  }

  getSelectedRow(item: RowArgs) {
    return item.dataItem.id;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    setTimeout(() => {
      if (this.selectedRows.length > 1) this.groupOperation = true;
      else {
        this.groupOperation = false;
        if (this.type == '1')
          this.modelUrl = String.Format(PayReceiveAccountApi.PaymentArticle,this.selectedRows[0]);
        else
          this.modelUrl = String.Format(PayReceiveAccountApi.ReceiptArticle,this.selectedRows[0]);
      }
    }, 0);
  }

}
