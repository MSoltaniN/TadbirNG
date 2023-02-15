import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  NgZone,
  OnInit,
  Output,
  Renderer2,
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
  RowArgs,
  SelectAllCheckboxState,
} from "@progress/kendo-angular-grid";
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { SettingService } from "@sppc/config/service";
import { DocumentStatusValue, VoucherOperations } from "@sppc/finance/enum";
import { Voucher, VoucherLine } from "@sppc/finance/models";
import {
  VoucherLineInfo,
  VoucherLineService,
  VoucherService,
} from "@sppc/finance/service";
import { VoucherApi } from "@sppc/finance/service/api";
import {
  AutoGeneratedGridComponent,
  FilterExpression,
  String,
} from "@sppc/shared/class";
import { ReloadOption } from "@sppc/shared/class/reload-option";
import { ResultOption } from "@sppc/shared/class/result.option";
import { ErrorListComponent } from "@sppc/shared/components/errorList/errorList.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { ReportViewerComponent } from "@sppc/shared/components/reportViewer/reportViewer.component";
import { ViewIdentifierComponent } from "@sppc/shared/components/viewIdentifier/view-identifier.component";
import { Entities, MessageType } from "@sppc/shared/enum/metadata";
import { ViewName, VoucherPermissions } from "@sppc/shared/security";
import {
  BrowserStorageService,
  GridService,
  MetaDataService,
  ReportingService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
// import "rxjs/Rx";
import { VoucherLineFormComponent } from "./voucherLine-form.component";

@Component({
  selector: "voucherLine",
  templateUrl: "./voucherLine.component.html",
  styles: [
    `
      ::ng-deep .panel-primary {
        border-color: #989898;
      }
      .voucher-balance {
        text-align: center;
        display: block;
      }
      .voucher-balance > .color-red {
        color: red;
      }
      .voucher-balance > .color-green {
        color: green;
      }
      .voucher-balance > .balance-value {
        direction: ltr;
        display: inline-block;
      }
      .detail-info {
        margin: 5px 0;
      }
      .detail-info > span {
        padding-left: 15px;
      }
      .nowrap {
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
        width: 450px;
        display: block;
      }
      ::ng-deep.k-footer-template {
        background-color: #b3b3b3;
      }

      input[type="text"],
      textarea,
      .article-description input[type="text"] {
        width: 100%;
      }
      .article-status-item,
      .voucher-status-item {
        display: inline;
        margin: 0 10px;
      }
      .article-status-item input[type="text"] {
        width: 200px;
      }
      .article-description {
        margin-top: 10px;
      }
    `,
  ],
})
export class VoucherLineComponent
  extends AutoGeneratedGridComponent
  implements OnInit
{
  //#region Fields
  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true})
  reportManager: ReportManagementComponent;

  public rowData: GridDataResult;
  public selectedRows: any[] = [];
  public totalRecords: number;

  public debitSum: number;
  public creditSum: number;
  public balance: number;

  //for add in delete messageText
  deleteConfirm: boolean;
  deleteModelId: number;

  currentFilter: FilterExpression;

  showloadingMessage: boolean = true;

  //editDataItem?: VoucherLine = undefined;
  selectedModel: VoucherLine;

  isNewBalance: boolean;
  groupDelete: boolean = false;
  voucherId: number;

  isIssued: boolean = false;
  isConfirmed: boolean = false;
  isApproved: boolean = false;
  voucherNo: number;

  balancedMode: boolean = false;
  committedMode: boolean = false;
  finalizedMode: boolean = false;

  documentStatusId: number;
  saveCountNumber: number = 0;

  gridColumnsRow: any[] = [];

  @Input() set voucherID(id: number) {
    this.voucherId = id;
  }

  @Input() voucherInfo: Voucher;

  entityNamePermission: string;

  @Input() subjectMode: number;

  @Input() set saveCount(no: number) {
    this.saveCountNumber = no;
  }

  @Input() set documentStatus(id: number) {
    this.documentStatusId = id;

    switch (id) {
      case DocumentStatusValue.NotChecked: {
        this.committedMode = false;
        this.finalizedMode = false;
        break;
      }
      case DocumentStatusValue.Checked: {
        this.committedMode = true;
        this.finalizedMode = false;
        break;
      }
      case DocumentStatusValue.Finalized: {
        this.committedMode = false;
        this.finalizedMode = true;
        break;
      }
      default:
    }
  }

  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  voucherModel: Voucher;
  documentStatusValue: any;
  documentStatusNotChecked: any;

  refreshForm = false;
  //#endregion

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public dialogService: DialogService,
    public gridService: GridService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public voucherLineService: VoucherLineService,
    public voucherService: VoucherService,
    public settingService: SettingService,
    public reporingService: ReportingService,
    public ngZone: NgZone,
    public bStorageService: BrowserStorageService,
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
      elem
    );
  }

  //#region Events
  ngOnInit() {
    this.setViewId();

    var url = VoucherApi.VoucherArticles;
    this.entityNamePermission = "Voucher";
    if (this.subjectMode == 1) {
      this.entityNamePermission = "DraftVoucher";
      url = VoucherApi.DraftVoucherArticles;
    }

    this.getDataUrl = String.Format(url, this.voucherId);
    //this.documentStatusValue = DocumentStatusValue.NotChecked;
    this.documentStatusNotChecked = DocumentStatusValue.NotChecked;

    if (this.voucherInfo) {
      this.setVoucherProperties(this.voucherInfo);
    } else {
      this.getVoucher();
    }

    this.reloadGrid();

    this.voucherService.changeVoucher$.asObservable().subscribe(res => {
      setTimeout(() => {
        if (res == 'changed'){
          this.reloadGrid();
          this.voucherService.changeVoucher$.next('')
        }
      }, 0);
    })
  }

  /**
   * باز کردن و مقداردهی اولیه به فرم ویرایشگر
   */
  openEditorDialog(isNew: boolean) {
    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? "Buttons.New" : "Buttons.Edit"),
      content: VoucherLineFormComponent,
    });

    //this.dialogRef.dialog.location.nativeElement.classList.add('dialog-style');

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.errorMessages = undefined;
    this.dialogModel.isNew = isNew;
    this.dialogModel.isNewBalance = this.isNewBalance;
    this.dialogModel.balance = this.balance;

    this.dialogRef.content.instance.save.subscribe((viewModel) => {
      this.isNewBalance = false;
      var model = viewModel.model;
      var isOpen = viewModel.isOpen;

      model.branchId = this.BranchId;
      model.fiscalPeriodId = this.voucherModel.fiscalPeriodId;
      model.voucherId = this.voucherModel.id;

      var promise = new Promise((resolve) => {
        var serviceUrl = isNew
          ? String.Format(VoucherApi.VoucherArticles, this.voucherId)
          : String.Format(VoucherApi.VoucherArticle, model.id);

        if (this.subjectMode == 1) {
          serviceUrl = isNew
            ? String.Format(VoucherApi.DraftVoucherArticles, this.voucherId)
            : String.Format(VoucherApi.DraftVoucherArticle, model.id);
        }

        this.refreshForm = true;
        this.saveHandler(model, isNew, this.voucherLineService, serviceUrl)
          .then((success: ResultOption) => {
            resolve(true);
          })
          .catch((error: ResultOption) => {
            // error handler is called
            this.dialogModel.isEnableSaveBtn = true;
          });
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

        this.setFocus.emit();
      }
    );

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.isNewBalance = false;
        this.setFocus.emit();
      }
    });

    this.dialogRef.content.instance.setFocus.subscribe((res) => {
      //this.dialogRef.dialog.instance.focus();
    });
  }

  /** برای تنظیم عدد اولیه ویو میباشد */
  setViewId() {
    this.entityName = Entities.VoucherLine;
    this.viewId = ViewName[this.entityTypeName];
  }

  selectionKey(context: RowArgs): any {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    if (this.selectedRows.length > 1) this.groupDelete = true;
    else this.groupDelete = false;

    this.selectedModel = undefined;
    if (this.selectedRows.length == 1) {
      var index = this.rowData.data.findIndex(
        (rd) => rd.id === this.selectedRows[0]
      );
      this.selectedModel = this.rowData.data[index];
    }
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    var isReload: boolean = false;
    if (
      this.currentFilter &&
      this.currentFilter.children.length > filter.filters.length
    )
      isReload = true;

    this.currentFilter = this.getFilters(filter);
    if (isReload) {
      this.reloadGrid();
    }
  }

  removeHandler() {
    this.deleteConfirm = true;
    if (!this.groupDelete) {
      var record = this.selectedRows[0];

      this.prepareDeleteConfirm("");
      this.deleteModelId = record;
    } else {
      this.prepareDeleteConfirm(this.getText("Messages.SelectedItems"));
    }
  }

  public editHandler() {
    var id = this.selectedRows[0];
    this.grid.loading = true;
    this.voucherLineService
      .getById(String.Format(VoucherApi.VoucherArticle, id))
      .subscribe((res) => {
        this.editDataItem = res;
        this.openEditorDialog(false);

        this.grid.loading = false;
      });
  }

  //#endregion

  deleteModel(confirm: boolean) {
    if (confirm) {
      //حذف گروهی از گرید
      if (this.groupDelete) {
        this.grid.loading = true;
        let rowsId: Array<number> = [];
        this.selectedRows.forEach((item) => {
          rowsId.push(item);
        });

        var url = VoucherApi.AllVoucherArticles;
        if (this.subjectMode == 1) url = VoucherApi.AllDraftVoucherArticles;

        this.voucherLineService.groupDelete(url, rowsId).subscribe(
          (res) => {
            var data: any = res;
            if (data && data.length > 0) {
              //show errorlist component
              this.openErrorListDialog(data, rowsId.length);
            } else {
              this.showMessage(this.deleteMsg, MessageType.Info);
            }

            if (
              this.rowData.data.length == this.selectedRows.length &&
              this.pageIndex > 1
            )
              this.pageIndex =
                (this.pageIndex - 1) * this.pageSize - this.pageSize;

            //check pagenumber  and correction
            if (this.rowData) {
              var pageCount =
                Math.floor(
                  (this.rowData.total - this.selectedRows.length) /
                    this.pageSize
                ) + 1;
              if (this.pageIndex > 0 && this.pageIndex > pageCount)
                this.pageIndex =
                  (this.pageIndex - 1) * this.pageSize - this.pageSize;
            }
            //check pagenumber  and correction

            this.selectedRows = [];
            this.groupOperation = false;
            this.reloadGrid();
          },
          (error) => {
            this.grid.loading = false;
            //this.showMessage(error, MessageType.Warning);
            if (error)
              this.showMessage(
                this.errorHandlingService.handleError(error),
                MessageType.Warning
              );
          }
        );
      } else {
        //حذف یک سطر از گرید
        this.grid.loading = true;

        var url = VoucherApi.VoucherArticle;
        if (this.subjectMode == 1) url = VoucherApi.DraftVoucherArticle;

        this.voucherLineService
          .delete(String.Format(url, this.deleteModelId))
          .subscribe(
            (response) => {
              this.deleteModelId = 0;
              this.showMessage(this.deleteMsg, MessageType.Info);
              if (this.rowData.data.length == 1 && this.pageIndex > 1)
                this.pageIndex =
                  (this.pageIndex - 1) * this.pageSize - this.pageSize;

              this.selectedRows = [];
              this.reloadGrid();
            },
            (error) => {
              this.grid.loading = false;
              //var message = error.message ? error.message : error;
              if (error)
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

  openErrorListDialog(rowData: any[], total: number) {
    this.dialogRef = this.dialogService.open({
      title: this.getText("ErrorList.GroupOperationReport"),
      content: ErrorListComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.rowData = rowData;
    this.dialogModel.totalItems = total;

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();
      }
    );
  }

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(options?: ReloadOption) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    this.reportFilter = filter;

    //call page size from setting
    this.getPageSize();
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }

    if (options && options.InsertedModel) this.goToLastPage(this.totalRecords);

    var url = VoucherApi.VoucherArticles;

    this.voucherLineService
      .getAll(
        String.Format(url, this.voucherId),
        this.pageIndex,
        this.pageSize,
        this.sort,
        filter
      )
      .subscribe((res) => {
        var resData = res.body;
        this.properties = resData.properties;
        var totalCount = 0;

        if (res.headers != null) {
          var headers = res.headers != undefined ? res.headers : null;
          if (headers != null) {
            var retheader = headers.get("X-Total-Count");
            if (retheader != null) totalCount = parseInt(retheader.toString());
          }
        }
        this.rowData = {
          data: resData,
          total: totalCount,
        };

        if (this.refreshForm) {
          let debits = res.body.filter(i => i.debit).reduce((a,b) => a + parseInt(b.debit),0);
          let credits = res.body.filter(i => i.credit).reduce((a,b) => a + parseInt(b.credit),0);

          this.debitSum = debits;
          this.creditSum = credits;
  
          this.balance = this.debitSum - this.creditSum;
          this.balancedMode = this.balance == 0 ? true : false;

          this.refreshForm = false;
        }

        this.showloadingMessage = !(resData.length == 0);
        this.totalRecords = totalCount;
        this.selectedRows = [];
        this.selectedModel = undefined;
      });

    if (!this.refreshForm) {
      if (this.voucherInfo) {
        this.debitSum = this.voucherInfo.debitSum;
        this.creditSum = this.voucherInfo.creditSum;
  
        this.balance = this.debitSum - this.creditSum;
        this.balancedMode = this.balance == 0 ? true : false;
        this.grid.loading = false;
      } else {
        this.voucherLineService
          .getVoucherInfo(this.voucherId)
          .subscribe((res) => {
            this.voucherModel = res;
  
            this.debitSum = res.debitSum;
            this.creditSum = res.creditSum;
  
            this.balance = this.debitSum - this.creditSum;
            this.balancedMode = this.balance == 0 ? true : false;
  
            this.grid.loading = false;
          });
      }
    }
  }

  //report methods
  public showReport() {
    this.reportManager.showDefaultReport();
  }

  getVoucher() {
    this.voucherService
      .getById(String.Format(VoucherApi.Voucher, this.voucherId))
      .subscribe((res) => {
        this.setVoucherProperties(res);
      });
  }

  setVoucherProperties(voucher: Voucher) {
    this.voucherModel = voucher;
    this.isIssued = this.voucherModel.issuedById ? true : false;
    this.isApproved = this.voucherModel.approvedById ? true : false;
    this.isConfirmed = this.voucherModel.confirmedById ? true : false;
    this.voucherNo = this.voucherModel.no;

    this.balancedMode = this.voucherModel.isBalanced;

    this.saveCountNumber = this.voucherModel.saveCount;

    switch (this.voucherModel.statusId) {
      case DocumentStatusValue.NotChecked: {
        this.committedMode = false;
        this.finalizedMode = false;
        break;
      }
      case DocumentStatusValue.Checked: {
        this.committedMode = true;
        this.finalizedMode = false;
        break;
      }
      case DocumentStatusValue.Finalized: {
        this.committedMode = true;
        this.finalizedMode = true;
        break;
      }
      default:
    }
  }

  addNew() {
    if (this.documentStatusId == this.documentStatusNotChecked) {
      this.editDataItem = new VoucherLineInfo();
      this.openEditorDialog(true);
    }
  }

  addNewWithBalance() {
    this.isNewBalance = true;
    this.addNew();
  }

  changeVoucherStatus(mode: string) {
    let apiUrl: string;
    let hasPermission: boolean = false;

    if (this.subjectMode == 1) {
      this.showMessage(
        this.getText("VoucherLine.ApproveActionNotPermitted"),
        MessageType.Warning
      );
      setTimeout(() => {
        this.isConfirmed = false;
        this.isApproved = false;
      });
      return;
    }

    switch (mode) {
      case "confirm": {
        apiUrl = String.Format(
          this.isConfirmed
            ? VoucherApi.ConfirmVoucher
            : VoucherApi.UndoConfirmVoucher,
          this.voucherId
        );
        hasPermission = this.isAccess(
          Entities.Voucher,
          this.isConfirmed
            ? VoucherPermissions.Confirm
            : VoucherPermissions.UndoConfirm
        );
        break;
      }
      case "approve": {
        apiUrl = String.Format(
          this.isApproved
            ? VoucherApi.ApproveVoucher
            : VoucherApi.UndoApproveVoucher,
          this.voucherId
        );
        hasPermission = this.isAccess(
          Entities.Voucher,
          this.isApproved
            ? VoucherPermissions.Approve
            : VoucherPermissions.UndoApprove
        );
        break;
      }
      default:
    }

    if (hasPermission) {
      this.voucherService.changeVoucherStatus(apiUrl).subscribe(
        (res) => {
          this.getVoucher();
        },
        (error) => {
          this.getVoucher();
          //this.showMessage(error, MessageType.Warning);
          if (error)
            this.showMessage(
              this.errorHandlingService.handleError(error),
              MessageType.Warning
            );
        }
      );
    } else {
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
      this.getVoucher();
    }
  }
  //#endregion
}
