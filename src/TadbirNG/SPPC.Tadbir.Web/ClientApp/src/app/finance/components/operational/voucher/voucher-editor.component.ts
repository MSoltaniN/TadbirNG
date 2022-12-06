import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { DialogRef, DialogService } from "@progress/kendo-angular-dialog";
import { RTL } from "@progress/kendo-angular-l10n";
import {
  DocumentStatusValue,
  VoucherOperations,
  VoucherSubjectTypes,
} from "@sppc/finance/enum";
import { Voucher } from "@sppc/finance/models";
import { InventoryBalance } from "@sppc/finance/models/inventoryBalance";
import {
  InventoryBalanceInfo,
  VoucherInfo,
  VoucherService,
} from "@sppc/finance/service";
import { VoucherApi } from "@sppc/finance/service/api";
import { DetailComponent, FilterExpression, String } from "@sppc/shared/class";
import {
  ReportViewerComponent,
  ViewIdentifierComponent,
} from "@sppc/shared/components";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import { Item } from "@sppc/shared/models";
import {
  DraftVoucherPermissions,
  ViewName,
  VoucherPermissions,
} from "@sppc/shared/security";
import {
  BrowserStorageService,
  ErrorHandlingService,
  LookupService,
  MetaDataService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
import "rxjs/Rx";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "voucher-editor",
  templateUrl: "./voucher-editor.component.html",
  styles: [
    `
      .voucher-form-content {
        margin-top: 5px;
        border: solid 1px #3c8dbc;
        padding: 7px 10px 0;
      }
      input[type="text"],
      textarea,
      .ddl-type {
        width: 100%;
      }
      .voucher-status-item {
        display: inline;
        margin: 0 10px;
      }

      /deep/.dialog-padding .k-window-content {
        padding: 15px !important;
      }

      .col-xs-5ths,
      .col-sm-5ths,
      .col-md-5ths,
      .col-lg-5ths,
      .col-sm-4-5ths {
        position: relative;
        min-height: 1px;
        padding-right: 15px;
        padding-left: 15px;
      }

      .col-xs-5ths {
        width: 20%;
        float: left;
      }

      @media (min-width: 768px) {
        .col-sm-5ths {
          width: 20%;
          float: left;
        }
        .col-sm-4-5ths {
          width: 80%;
          float: left;
        }
      }

      @media (min-width: 992px) {
        .col-md-5ths {
          width: 20%;
          float: left;
        }
      }

      @media (min-width: 1200px) {
        .col-lg-5ths {
          width: 20%;
          float: left;
        }
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
export class VoucherEditorComponent extends DetailComponent implements OnInit {
  errorMessage: string;
  voucherModel: Voucher;
  voucherTypeList: Array<Item> = [];
  selectedType: string;
  deleteConfirm: boolean;

  @Input() filter: FilterExpression;
  @Input() quickFilter: FilterExpression;

  @Input() voucherItem: Voucher;
  @Input() isOpenFromList: boolean = false;
  //@Output() reloadGrid: EventEmitter<any> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  isShowBreadcrumb: boolean = true;
  isFirstVoucher: boolean = false;
  isLastVoucher: boolean = false;
  voucherOperationsItem: any;
  deleteConfirmMsg: string;
  subjectMode: number;
  subjectModeTitle: string;
  draftTitle: string;
  normalTitle: string;
  entityNamePermission: string;
  breadcrumbResourceName: string;
  voucherDateType: string;

  currentVoucherNo: number;

  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent)
  reportManager: ReportManagementComponent;

  /**
   * برای باز شدن مودال تعیین تکلیف هنگامی که هیچ سندی جوجود ندارد
   */
  noVoucher = [false,false];
  isNewVoucher = false;

  constructor(
    private voucherService: VoucherService,
    public toastrService: ToastrService,
    public translate: TranslateService,
    private activeRoute: ActivatedRoute,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public router: Router,
    private dialogService: DialogService,
    private lookupService: LookupService,
    public bStorageService: BrowserStorageService,
    public errorHandlingService: ErrorHandlingService,
    public elem: ElementRef
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      Entities.Voucher,
      ViewName.Voucher,
      elem
    );

    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit() {
    this.draftTitle = this.getText("Voucher.NormalVoucher");
    this.normalTitle = this.getText("Voucher.DraftVoucher");
    this.voucherOperationsItem = VoucherOperations;
    this.entityNamePermission = "Voucher";

    this.setDateDisplayType();
    this.editForm.reset();

    if (this.voucherItem) {
      this.initVoucherForm(this.voucherItem);
      this.isShowBreadcrumb = false;
      this.subjectMode = this.voucherItem.subjectType;

      if (this.subjectMode == 1) this.entityNamePermission = "DraftVoucher";
      this.getVoucherType();
    } else {
      this.activeRoute.params.subscribe((params) => {
        if (!this.subjectMode) {
          this.subjectMode = params["type"] == "draft" ? 1 : 0;
          if (this.subjectMode == 1) this.entityNamePermission = "DraftVoucher";
        }

        this.getVoucherType();

        switch (params["mode"]) {
          case "new": {
            this.newVoucher();
            this.isNewVoucher = true;
            this.isLastVoucher = true;
            break;
          }
          case "last": {
            this.isLastVoucher = true;
            if (this.subjectMode == 0) this.getVoucher(VoucherApi.LastVoucher);
            else this.getVoucher(VoucherApi.LastDraftVoucher);

            break;
          }
          case "by-no": {
            this.byNoVoucher();
            break;
          }
          case "first": {
            this.isFirstVoucher = true;
            if (this.subjectMode == 0) this.getVoucher(VoucherApi.FirstVoucher);
            else this.getVoucher(VoucherApi.FirstDraftVoucher);
            break;
          }
          case "next": {
            var voucherNo = this.activeRoute.snapshot.queryParamMap.get("no");
            if (voucherNo) {
              if (this.subjectMode == 0)
                this.getVoucher(
                  String.Format(VoucherApi.NextVoucher, voucherNo),
                  true
                );
              else
                this.getVoucher(
                  String.Format(VoucherApi.NextDraftVoucher, voucherNo),
                  true
                );
            }
            break;
          }
          case "previous": {
            var voucherNo = this.activeRoute.snapshot.queryParamMap.get("no");
            if (voucherNo) {
              if (this.subjectMode == 0)
                this.getVoucher(
                  String.Format(VoucherApi.PreviousVoucher, voucherNo),
                  true
                );
              else
                this.getVoucher(
                  String.Format(VoucherApi.PreviousDraftVoucher, voucherNo),
                  true
                );
            }
            break;
          }
          case "opening-voucher": {
            var voucherNo = this.activeRoute.snapshot.queryParamMap.get("no");
            if (voucherNo) {
              if (this.subjectMode == 0)
                this.getVoucher(
                  String.Format(VoucherApi.VoucherByNo, voucherNo),
                  true
                );
              else
                this.getVoucher(
                  String.Format(VoucherApi.DraftVoucherByNo, voucherNo),
                  true
                );
            } else {
              this.openingVoucherQuery();
            }

            break;
          }
          case "closing-voucher": {
            this.getVoucher(VoucherApi.ClosingVoucher);
            break;
          }
          case "close-temp-accounts": {
            if (this.InventoryMode == 0) {
              var voucherNo = this.activeRoute.snapshot.queryParamMap.get("no");
              if (voucherNo) {
                if (this.subjectMode == 0)
                  this.getVoucher(
                    String.Format(VoucherApi.VoucherByNo, voucherNo),
                    true
                  );
                else
                  this.getVoucher(
                    String.Format(VoucherApi.DraftVoucherByNo, voucherNo),
                    true
                  );
              } else {
                this.checkClosingTmp();
              }
            } else {
              this.closingTmpOnInventoryMode1();
            }

            break;
          }

          default: {
            this.isShowBreadcrumb = false;
            this.newVoucher();
          }
        }
      });
    }

  }

  //report methods
  public showReport() {
    this.reportManager.showDefaultDesignedReport();
  }

  setDateDisplayType() {
    if (this.properties && this.properties.get(this.metadataKey))
      this.voucherDateType = this.properties
        .get(this.metadataKey)
        .filter((p) => p.name == "Date")[0].type;
  }

  openingVoucherQuery() {
    this.voucherService.getOpeningVoucherQuery().subscribe(
      (result: Voucher) => {
        if (result) {
          //show confirm box
          this.router.navigate(["/tadbir/home"], {
            queryParams: {
              returnUrl: "finance/vouchers/opening-voucher",
              mode: "opening-voucher",
            },
          });
        } else if (!result) {
          //create opening voucher
          this.getVoucher(VoucherApi.OpeningVoucher);
        } else {
          this.initVoucherForm(result);
        }
      },
      (err) => {
        if (err.statusCode == 400) {
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
          this.router.navigate(["/finance/voucher"]);
        }

        if (err.value) {
          this.showMessage(err.value, MessageType.Warning);
          this.router.navigate(["/finance/voucher"]);
        }
      }
    );
  }

  /**
   * ساخت سند بستن حسابها براساس سیستم دایمی
   */
  closingTmpOnInventoryMode1() {
    this.voucherService.getClosingAccountsVoucherMode1().subscribe(
      (result: any) => {
        var voucherNo = result.no;
        if (voucherNo) {
          if (this.subjectMode == 0)
            this.getVoucher(
              String.Format(VoucherApi.VoucherByNo, voucherNo),
              true
            );
          else
            this.getVoucher(
              String.Format(VoucherApi.DraftVoucherByNo, voucherNo),
              true
            );
        }
      },
      (err) => {
        if (err.statusCode == 400) {
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
          this.router.navigate(["/finance/voucher"]);
        }

        if (err.value) {
          this.showMessage(err.value, MessageType.Warning);
          this.router.navigate(["/finance/voucher"]);
        }
      }
    );
  }

  checkClosingTmp() {
    var bodyItem = new Array<InventoryBalance>();
    var item = new InventoryBalanceInfo();

    this.voucherService.getClosingAccountsVoucher(bodyItem).subscribe(
      (result) => {
        if (result) {
          //closingAccount created and show voucher
          //this.initVoucherForm(result);
          this.router.navigate(["/tadbir/home"], {
            queryParams: {
              returnUrl: "finance/vouchers/close-temp-accounts",
              mode: "closing-tmp",
            },
          });
        } else {
          //closingAccount not created and show popup
          this.router.navigate(["/tadbir/home"], {
            queryParams: {
              returnUrl: "finance/vouchers/close-temp-accounts",
              mode: "closing-tmp",
            },
          });
        }
      },
      (err) => {
        if (err.statusCode == 400) {
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
          this.router.navigate(["/finance/voucher"]);
        }
      }
    );
  }

  newVoucher() {
    if (this.subjectMode == 0) this.getVoucher(VoucherApi.NewVoucher);
    else this.getVoucher(VoucherApi.NewDraftVoucher);
  }

  getNewVoucher() {
    if (this.voucherItem || this.isOpenFromList)
      if (this.subjectMode == 0) this.getVoucher(VoucherApi.NewVoucher);
      else this.getVoucher(VoucherApi.NewDraftVoucher);
    else {
      if (this.subjectMode == 0) this.redirectTo("/finance/vouchers/new");
      else this.redirectTo("/finance/vouchers/new/draft");
    }
  }

  redirectTo(uri) {
    this.router
      .navigateByUrl("/", { skipLocationChange: true })
      .then(() => this.router.navigate([uri]));
  }

  byNoVoucher() {
    var voucherNo = this.activeRoute.snapshot.queryParamMap.get("no");
    if (!voucherNo) {
      if (this.subjectMode == 0)
        this.router.navigate(["/tadbir/home"], {
          queryParams: { returnUrl: "finance/vouchers/by-no", mode: "by-no" },
        });
      else
        this.router.navigate(["/tadbir/home"], {
          queryParams: {
            returnUrl: "finance/vouchers/by-no",
            mode: "by-no",
            type: "draft",
          },
        });
    } else {
      var type = this.activeRoute.snapshot.queryParamMap.get("type");
      if (this.subjectMode == 1 || type == "draft")
        this.getVoucher(
          String.Format(VoucherApi.DraftVoucherByNo, voucherNo),
          true
        );
      else
        this.getVoucher(String.Format(VoucherApi.VoucherByNo, voucherNo), true);
    }
  }

  getVoucher(apiUrl: string, byNo: boolean = false) {
    this.voucherService
      .getModelsByFilters(apiUrl, this.filter, this.quickFilter)
      .subscribe(
        (res) => {
          res.originName = '';
          this.initVoucherForm(res);
          this.errorMessage = undefined;
          this.isLastVoucher = !res.hasNext;
          this.isFirstVoucher = !res.hasPrevious;
        },
        (err) => {
          if (err == null || err.statusCode == 404) {
            this.showMessage(
              this.getText("Voucher.VoucherNotFound"),
              MessageType.Warning
            );
            this.noVoucher = [true,byNo];
          }

          if (err.statusCode == 400) {
            this.cancel.emit();
            this.showMessage(
              this.errorHandlingService.handleError(err),
              MessageType.Warning
            );
            this.router.navigate(["/finance/voucher"]);
          }
        }
      );
  }

  noVoucherHandler(status:boolean,byNo) {
    if (status == false) {
      if (byNo) {
        this.router.navigate(["/tadbir/home"], {
          queryParams: {
            returnUrl: "finance/vouchers/by-no",
            mode: "by-no",
          },
        });
      } else {
        this.router.navigate(["/finance/voucher"]);
      }
    } else {
      this.router.navigate(['/finance/vouchers/new'])
    }
  }

  closeNoVoucherModal() {
    this.noVoucher[0] = false;
  }

  initVoucherForm(item: Voucher) {
    this.editForm.reset(item);
    this.isLastVoucher = !item.hasNext;
    this.isFirstVoucher = !item.hasPrevious;
    this.voucherModel = item;
    //this.selectedType = this.voucherModel.type.toString();
    this.selectedType = this.voucherModel.subjectType.toString();
    this.subjectMode = this.voucherModel.subjectType;

    this.currentVoucherNo = this.voucherModel.no;
  }

  voucherTypeListChange(value) {
    if (
      this.selectedType == VoucherSubjectTypes.Normal &&
      value == VoucherSubjectTypes.Draft
    ) {
      if (
        this.voucherModel.confirmedById != null ||
        this.voucherModel.statusId == DocumentStatusValue.Finalized
      ) {
        this.showMessage(this.getText("Voucher.SubjectTypeValidation"));
        setTimeout(() => {
          this.selectedType = VoucherSubjectTypes.Normal;
        });
        return;
      }
    }
  }

  getVoucherType() {
    if (this.subjectMode == 0) this.subjectModeTitle = this.normalTitle;

    if (this.subjectMode == 1) this.subjectModeTitle = this.draftTitle;
  }

  onSave(e?: any): void {
    if (this.editForm.valid && this.checkEditPermission()) {
      let model: Voucher = this.editForm.value;
      model.branchId = this.BranchId;
      model.fiscalPeriodId = this.FiscalPeriodId;
      model.statusId = this.voucherModel.statusId;
      model.saveCount = this.voucherModel.saveCount;
      model.subjectType = parseInt(this.selectedType);

      if (model.reference == "") model.reference = null;

      this.voucherService
        .edit<Voucher>(String.Format(VoucherApi.Voucher, model.id), model)
        .subscribe(
          (res) => {
            this.editForm.reset(res);
            this.voucherModel = res;
            this.errorMessages = undefined;
            this.showMessage(this.updateMsg, MessageType.Succes);
          },
          (error) => {
            if (e) {
              if (error)
                this.errorMessages =
                  this.errorHandlingService.handleError(error);
            } else
              this.showMessage(
                this.errorHandlingService.handleError(error),
                MessageType.Warning
              );
          }
        );
    }
  }

  nextVoucher() {
    if (this.voucherItem || this.isOpenFromList) {
      if (this.subjectMode == 0)
        this.getVoucher(
          String.Format(VoucherApi.NextVoucher, this.voucherModel.no)
        );
      else
        this.getVoucher(
          String.Format(VoucherApi.NextDraftVoucher, this.voucherModel.no)
        );
      this.isFirstVoucher = false;
      this.isLastVoucher = false;
    } else {
      if (this.subjectMode == 0)
        this.router.navigate(["/finance/vouchers/next"], {
          queryParams: { no: this.voucherModel.no },
        });
      else
        this.router.navigate(["/finance/vouchers/next/draft"], {
          queryParams: { no: this.voucherModel.no },
        });
    }
  }

  previousVoucher() {
    if (this.voucherItem || this.isOpenFromList) {
      if (this.subjectMode == 0)
        this.getVoucher(
          String.Format(VoucherApi.PreviousVoucher, this.voucherModel.no)
        );
      else
        this.getVoucher(
          String.Format(VoucherApi.PreviousDraftVoucher, this.voucherModel.no)
        );
      this.isFirstVoucher = false;
      this.isLastVoucher = false;
    } else {
      if (this.subjectMode == 0)
        this.router.navigate(["/finance/vouchers/previous"], {
          queryParams: { no: this.voucherModel.no },
        });
      else
        this.router.navigate(["/finance/vouchers/previous/draft"], {
          queryParams: { no: this.voucherModel.no },
        });
    }
  }

  firstVoucher() {
    if (this.voucherItem || this.isOpenFromList) {
      if (this.subjectMode == 0) this.getVoucher(VoucherApi.FirstVoucher);
      else this.getVoucher(VoucherApi.FirstDraftVoucher);
      this.isFirstVoucher = true;
      this.isLastVoucher = false;
    } else {
      if (this.subjectMode == 0)
        this.router.navigate(["/finance/vouchers/first"]);
      else this.router.navigate(["/finance/vouchers/first/draft"]);
    }
  }

  lastVoucher() {
    if (this.voucherItem || this.isOpenFromList) {
      if (this.subjectMode == 0) this.getVoucher(VoucherApi.LastVoucher);
      else this.getVoucher(VoucherApi.LastDraftVoucher);
      this.isFirstVoucher = false;
      this.isLastVoucher = true;
    } else {
      if (this.subjectMode == 0)
        this.router.navigate(["/finance/vouchers/last"]);
      else this.router.navigate(["/finance/vouchers/last/draft"]);
    }
  }

  searchVoucher() {
    this.router.navigate(["/tadbir/home"], {
      queryParams: { returnUrl: "finance/vouchers/by-no", mode: "by-no" },
    });
  }

  checkHandler() {
    var apiUrl = String.Format(
      this.voucherModel.statusId == DocumentStatusValue.NotChecked
        ? VoucherApi.CheckVoucher
        : VoucherApi.UndoCheckVoucher,
      this.voucherModel.id
    );
    if (this.subjectMode == 1) {
      apiUrl = String.Format(
        this.voucherModel.statusId == DocumentStatusValue.NotChecked
          ? VoucherApi.CheckDraftVoucher
          : VoucherApi.UndoCheckDraftVoucher,
        this.voucherModel.id
      );
    }

    this.voucherService.changeVoucherStatus(apiUrl).subscribe(
      (res) => {
        this.voucherModel.statusId =
          this.voucherModel.statusId == DocumentStatusValue.NotChecked
            ? DocumentStatusValue.Checked
            : DocumentStatusValue.NotChecked;

        //this.reloadGrid.emit();
      },
      (error) => {
        this.showMessage(
          this.errorHandlingService.handleError(error),
          MessageType.Warning
        );
      }
    );
  }

  voucherOperation(item: VoucherOperations) {
    var model1 = new VoucherInfo();
    var model2 = new VoucherInfo();

    model1.no = parseInt(this.editForm.value.no);
    model1.reference = this.editForm.value.reference;
    model1.dailyNo = parseInt(this.editForm.value.dailyNo);
    model1.association = this.editForm.value.association;
    model1.date = this.getDate(this.editForm.value.date);
    model1.description = this.editForm.value.description;
    model1.typeName = this.editForm.value.typeName;

    model2.no = this.voucherModel.no;
    model2.reference = this.voucherModel.reference;
    model2.dailyNo = this.voucherModel.dailyNo;
    model2.association = this.voucherModel.association;
    model2.date = this.getDate(this.voucherModel.date);
    model2.description = this.voucherModel.description;
    model2.typeName = this.voucherModel.typeName;

    var isFormDataChenged = true;
    if (JSON.stringify(model2) === JSON.stringify(model1))
      isFormDataChenged = false;

    if (isFormDataChenged) {
      const dialog: DialogRef = this.dialogService.open({
        title: this.getText("Entity.Voucher"),
        content: this.getText("Voucher.SaveChanges"),
        actions: [
          { text: this.getText("Buttons.Yes"), mode: 1, primary: true },
          { text: this.getText("Buttons.No"), mode: 0 },
        ],
        width: 450,
        height: 150,
        minWidth: 250,
      });

      dialog.dialog.location.nativeElement.classList.add("dialog-padding");

      dialog.result.subscribe((result) => {
        let res: any = result;
        if (res.mode == 1) this.onSave();

        this.executeVoucherOperation(item);
      });
    } else {
      this.executeVoucherOperation(item);
    }
  }

  getDate(date: Date): Date {
    var date = new Date(date);
    return new Date(date.getFullYear(), date.getMonth(), date.getDate());
  }

  executeVoucherOperation(item: VoucherOperations) {
    switch (item) {
      case VoucherOperations.First: {
        this.firstVoucher();
        break;
      }
      case VoucherOperations.Last: {
        this.lastVoucher();
        break;
      }
      case VoucherOperations.New: {
        this.getNewVoucher();
        break;
      }
      case VoucherOperations.Next: {
        this.nextVoucher();
        break;
      }
      case VoucherOperations.Previous: {
        this.previousVoucher();
        break;
      }
      case VoucherOperations.Search: {
        this.searchVoucher();
        break;
      }
      case VoucherOperations.CheckVoucher: {
        this.checkHandler();
        break;
      }
      default:
    }
  }

  removeHandler() {
    this.deleteConfirm = true;
    this.prepareDeleteConfirm(this.getText("Messages.SelectedItems"));
  }

  deleteModel(confirm: boolean) {
    if (confirm) {
      this.voucherService
        .delete(String.Format(VoucherApi.Voucher, this.voucherModel.id))
        .subscribe(
          (response) => {
            this.showMessage(
              this.getText("Messages.DeleteOperationSuccessful"),
              MessageType.Info
            );

            var url = VoucherApi.NextVoucher;
            var urlNo = "/finance/vouchers/by-no";
            if (this.subjectMode == 1) {
              url = VoucherApi.NextDraftVoucher;
              urlNo = "/finance/vouchers/by-no/draft";
            }
            //try for next voucher
            this.voucherService
              .getModels(String.Format(url, this.voucherModel.no))
              .subscribe(
                (voucher) => {
                  if (voucher) {
                    this.router.navigate([urlNo], {
                      queryParams: { no: voucher.no },
                    });
                  } else {
                  }
                },
                (error) => {
                  //if next voucher not exists try for previous voucher
                  var url = VoucherApi.PreviousVoucher;
                  if (this.subjectMode == 1) {
                    url = VoucherApi.PreviousDraftVoucher;
                  }
                  this.voucherService
                    .getModels(String.Format(url, this.voucherModel.no))
                    .subscribe(
                      (voucher) => {
                        if (voucher) {
                          this.router.navigate([urlNo], {
                            queryParams: { no: voucher.no },
                          });
                        }
                      },
                      (error) => {
                        //if previous voucher not exists show voucher list
                        this.cancel.emit();
                        this.router.navigate(["/finance/voucher"]);
                      }
                    );
                }
              );
          },
          (error) => {
            this.showMessage(
              this.errorHandlingService.handleError(error),
              MessageType.Warning
            );
          }
        );
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }

  /**
   * prepare confim message for delete operation
   * @param text is a part of message that use for delete confirm message
   */
  public prepareDeleteConfirm(text: string) {
    this.translate
      .get("Messages.VoucherDeleteConfirm")
      .subscribe((msg: string) => {
        this.deleteConfirmMsg = String.Format(msg, text);
      });
  }

  get isVoucherConfirmed(): boolean {
    if (this.voucherModel) return this.voucherModel.statusId > 1;

    return false;
  }

  get hasEditAccess(): boolean {
    return this.checkEditPermission(false);
  }

  checkEditPermission(showMessage: boolean = true) {
    if (
      this.subjectMode == 1 &&
      !this.isAccess(Entities.DraftVoucher, DraftVoucherPermissions.Edit)
    ) {
      if (showMessage)
        this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
      return false;
    }

    if (
      this.subjectMode == 0 &&
      !this.isAccess(Entities.Voucher, VoucherPermissions.Edit)
    ) {
      if (showMessage)
        this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
      return false;
    }

    return true;
  }
}
