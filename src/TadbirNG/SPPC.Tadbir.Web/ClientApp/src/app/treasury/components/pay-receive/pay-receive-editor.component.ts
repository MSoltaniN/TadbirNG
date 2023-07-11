import { Component, ElementRef, Input, OnInit, Renderer2 } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { CurrencyInfo } from '@sppc/finance/models';
import { DetailComponent, FilterExpression, String } from '@sppc/shared/class';
import { Persist } from '@sppc/shared/decorator/persist.decorator';
import { Entities, Layout, MessageType } from '@sppc/shared/enum/metadata';
import { PaymentPermissions, ReceiptPermissions, ViewName } from '@sppc/shared/security';
import { BrowserStorageService, ErrorHandlingService, LookupService, MetaDataService, SessionKeys } from '@sppc/shared/services';
import { LookupApi } from '@sppc/shared/services/api';
import { PayReceiveTypes, PayReceiveOperations, UrlPathType } from '@sppc/treasury/enums/payReceive';
import { PayReceiveApi } from '@sppc/treasury/service/api';
import { PayReceiveInfo, PayReceiveService } from '@sppc/treasury/service/pay-receive.service';
import { ToastrService } from 'ngx-toastr';
import { catchError, concatMap, exhaustMap, lastValueFrom, of, shareReplay, take } from 'rxjs';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'pay-receive-editor',
  templateUrl: './pay-receive-editor.component.html',
  styleUrls: ['./pay-receive-editor.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})
export class PayReceiveEditorComponent extends DetailComponent implements OnInit {

  @Input() public model: PayReceiveInfo;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';
  @Input() filter: FilterExpression;
  @Input() quickFilter: FilterExpression;
  @Input() dialogMode = false;
  /**
  * نوع فرم؛ 0 برای دریافت و 1 برای پرداخت
  */
  @Input() set type (value:any) {
    if (value == PayReceiveTypes.Receipt || value == UrlPathType.Receipts) {
      this._formType = PayReceiveTypes.Receipt;
      this.entityType = Entities.Receipt;
      this.viewId = ViewName.Receipt;
      this.metadataKey = String.Format(SessionKeys.MetadataKey, this.viewId ? this.viewId.toString() : '', this.CurrentLanguage);
      this.localizeMsg();
    } else {
      this._formType = PayReceiveTypes.Payment;
    }
  }

  constructor(public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public elem:ElementRef,
    private route: ActivatedRoute,
    private payReceive: PayReceiveService,
    public lookupService: LookupService,
    private router: Router,
    public errorHandlingService: ErrorHandlingService)
  {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Payment, ViewName.Payment,elem);
    this.type = this.urlPath;
    this.insertedInNew = true;
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  private _formType: PayReceiveTypes;
  searchConfirm: boolean;
  urlMode: string;
  isShowBreadcrumb = true;
  payReceiveOperationsItem = PayReceiveOperations;
  isFirstItem = false;
  isLastItem = false;
  deleteConfirm = false;
  payReceiveNo;
  totalCashAmount: number;
  currenciesRows: Array<CurrencyInfo>;
  selectedCurrencyValue: number;
  decimalCount: number = 0;
  currencyRate: number | undefined;
  currencyValue: number;
  getDataUrl: string;
  breadCrumbTitle: string;
  @Persist() preferedDate;

  public get urlPath() {
    return this.route.snapshot.url[0].path.toLowerCase();
  }
  
  public get type() {
    return this._formType;
  }

  public get returnUrl() {
    let rurl = this.route.snapshot.queryParamMap.get('returnUrl');
    return rurl?rurl.toLowerCase():'';
  }

  public get isConfirmed() : boolean {
    return this.editForm?.value.isConfirmed;
  }

  public get isApproved() : boolean {
    return this.editForm?.value.isApproved;
  }

  public get noQueryParam() {
    let no = this.route.snapshot.queryParamMap.get('no');
    return no?no:'';
  }

  ngOnInit(): void {
    // this.getCurrencies();

    this.route.paramMap.subscribe(param => {
      this.urlMode = param.get('mode');
      this.manageRouting(param.get('mode'));
    })
  }

  manageRouting(mode){
    switch (mode) {
      case 'new':
          this.addNew();
          break;

        case 'first':
          this.goFirst();
          break;

        case 'last':
          this.goLast();
          break;

        case 'next':
          if (!this.isFirstItem) {
            this.getDataUrl = String.Format(this.type == 1? PayReceiveApi.NextPayment: PayReceiveApi.NextReceipt,
                      this.noQueryParam?this.noQueryParam:this.model.payReceiveNo);
            this.getPayReceive(this.getDataUrl);
          }
          break;

        case 'previous':
          if (this.noQueryParam || this.model.id) {
            this.getDataUrl = String.Format(this.type == 1? PayReceiveApi.PreviousPayment: PayReceiveApi.PreviousReceipt,
                  this.noQueryParam?this.noQueryParam:this.model.payReceiveNo);
          } else {
            this.getDataUrl = this.type == 1? PayReceiveApi.LastPayment: PayReceiveApi.LastReceipt;
          }
          this.getPayReceive(this.getDataUrl);
          break;

        case 'by-no':
          if (!this.noQueryParam)
            this.searchConfirm = true;
          else {
            let baseUrl = this.urlPath == 'payments'? PayReceiveApi.PaymentByNo: PayReceiveApi.ReceiptByNo;
            this.getDataUrl = String.Format(baseUrl,this.noQueryParam);
            this.getPayReceive(this.getDataUrl);
          }
          break;

        default:
          break;
    }    
  }

  addNew() {
    this.breadCrumbTitleFormat('Form.New','Entity.'+this.entityType);

    if (this.urlMode != 'new' && !this.dialogMode){
      this.router.navigate([`/treasury/${this.urlPath}/new`]);
    } else {
      this.isNew = true;
      this.errorMessages = undefined;
      this.getDataUrl = this.urlPath == UrlPathType.Payments?PayReceiveApi.NewPayment: PayReceiveApi.NewReceipt;
      this.getPayReceive(this.getDataUrl,true);
    }
  }

  goFirst() {
    this.breadCrumbTitleFormat('Form.First','Entity.'+this.entityType);

    if (this.urlMode != 'first' && !this.dialogMode) {
      this.router.navigate([`/treasury/${this.urlPath}/first`]);
    } else {
      this.getDataUrl = this.urlPath == 'payments'? PayReceiveApi.FirstPayment: PayReceiveApi.FirstReceipt;
      this.getPayReceive(this.getDataUrl);
    }
  }

  goLast() {
    this.breadCrumbTitleFormat('Form.Last','Entity.'+this.entityType);

    if (this.urlMode != 'last' && !this.dialogMode) {
      this.router.navigate([`/treasury/${this.urlPath}/last`]);
    } else {
      this.getDataUrl = this.urlPath == 'payments'? PayReceiveApi.LastPayment: PayReceiveApi.LastReceipt;
      this.getPayReceive(this.getDataUrl);
    }
  }

  goNext() {
    let baseUrl = this.urlPath == 'payments'? PayReceiveApi.NextPayment: PayReceiveApi.NextReceipt;

    if (this.urlMode != 'next' && !this.dialogMode) {
      let no = this.model.id > 0? this.model.payReceiveNo: '';
      this.router.navigate([`/treasury/${this.urlPath}/next`],{
        queryParams: {
          no: no
        }
      });
    } else {
      if (!this.isLastItem) {
        this.getDataUrl = String.Format(baseUrl,this.model.payReceiveNo);

        let no = this.model.id > 0? this.model.payReceiveNo: '';
        if (!this.dialogMode)
          this.router.navigate([`/treasury/${this.urlPath}/next`],{
            queryParams: {
              no: no
            }
          });
        else {
          if (this.isFormChanged()) {
            this.saveChangesConfirmDialog({
              onDiscard: () => {
                this.getPayReceive(this.getDataUrl);
              }
            });
          }
          else {
            this.getPayReceive(this.getDataUrl);
          }
  
        }
      }
      
    }
  }

  goPrevious() {
    let baseUrl = this.urlPath == 'payments'? PayReceiveApi.PreviousPayment: PayReceiveApi.PreviousReceipt;

    if (this.urlMode != 'previous' && !this.dialogMode) {
      let no = this.model.id > 0? this.model.payReceiveNo: '';
      this.router.navigate([`/treasury/${this.urlPath}/previous`],{
        queryParams: {
          no: no
        }
      });
    } else {
      if (this.model.id) {
        this.getDataUrl = String.Format(baseUrl,this.model.payReceiveNo);
      } else {
        this.getDataUrl = this.urlPath == 'payments'? PayReceiveApi.LastPayment: PayReceiveApi.LastReceipt;
      }
      let no = this.model.id > 0? this.model.payReceiveNo: '';
      if (!this.dialogMode)
        this.router.navigate([`/treasury/${this.urlPath}/previous`],{
          queryParams: {
            no: no
          }
        });
      else {
        if (this.isFormChanged()) {
          this.saveChangesConfirmDialog({
            onDiscard: () => {
              this.getPayReceive(this.getDataUrl);
            }
          });
        }
        else {
          this.getPayReceive(this.getDataUrl);
        }

      }
    }
  }

  goSearch() {
    this.searchConfirm = true;
    if (this.urlMode != 'by-no' && !this.dialogMode) {
      this.router.navigate([`/treasury/${this.urlPath}/by-no`],{queryParams:{
        returnUrl: `/treasury/${this.urlPath}/`+this.urlMode
      }});
    }
  }

  searchByNo(searchConfirm = false) {
    let baseUrl = this.urlPath == 'payments'? PayReceiveApi.PaymentByNo: PayReceiveApi.ReceiptByNo;

    if (searchConfirm) {
      if (this.payReceiveNo && !this.dialogMode) {
        this.router.navigate([`/treasury/${this.urlPath}/by-no`],{queryParams:{
          no: this.payReceiveNo,
          returnUrl: this.returnUrl
        }});
        this.getDataUrl = String.Format(baseUrl,this.payReceiveNo);
        // this.getPayReceive(url);
      } else {
        return;
      }
    } else {
      this.searchConfirm = false;
      if (this.returnUrl)
        this.router.navigate([this.returnUrl]);
      else
        this.router.navigate([`/treasury/${this.urlPath}/new`]);
    }
  }

  getPayReceive(apiUrl:string,isNew=false) {
    this.payReceive.getModelsByFilters(apiUrl,this.filter,this.quickFilter)
    .pipe(
      take(2)
    )
    .subscribe({
      next: res => {
        if (this.urlMode == 'by-no') {
          this.searchConfirm = false;
        }
        this.model = res;
        this.initPayReceiveForm()
      },
      error: (err) => {
        if (err == null || err.statusCode == 404) {
          this.isFirstItem = true;
          if (!isNew) {
            this.showMessage(
              this.getText("CheckBook.CheckBookNotFound"),
              MessageType.Warning
            );
            this.addNew();
          }

          if (this.urlMode == 'by-no') {
            if (this.returnUrl)
              this.router.navigate([this.returnUrl]);
            else
              this.router.navigate([`/treasury/${this.urlPath}/new`]);
          };
        }

        if (err != null && err.statusCode == 400) {
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
          this.router.navigate([`/treasury/${this.urlPath}/new`]);
        }
      }
    })
  }

  initPayReceiveForm() {
    if (this.isNew) {
      this.searchConfirm = false;
      this.model.date = this.preferedDate? this.preferedDate: this.model.date;
    } else {
      this.searchConfirm = false;
    }

    this.isLastItem = !this.model.hasNext;
    this.isFirstItem = !this.model.hasPrevious;
    this.errorMessages = [];

    setTimeout(() => {
      this.editForm.reset(this.model);
    }, 0);
  }

  removeHandler() {
    this.deleteConfirm = true;
    let ent = this.urlPath == "payments"? "Entity.Payment": "Entity.Receipt";
    this.prepareDeleteConfirm(this.getText(ent));
  }

  deleteModel(confirm:boolean) {
    if (confirm) {
      if (this.model.id) {
        let deleteURL = String.Format(this.type == 1? PayReceiveApi.Payment: PayReceiveApi.Receipt,this.model.id);
        this.getDataUrl = this.type == 1? PayReceiveApi.NextPayment: PayReceiveApi.NextReceipt;

        this.payReceive.delete(deleteURL)
        .pipe(
          //try for next item
          exhaustMap( () => this.payReceive.getModels(String.Format(this.getDataUrl, this.model.payReceiveNo))
            .pipe(
              catchError(() => {
                //if next voucher not exists try for previous Item
                this.getDataUrl = this.type == 1? PayReceiveApi.PreviousPayment: PayReceiveApi.PreviousReceipt;
                return this.payReceive.getModels(String.Format(this.getDataUrl, this.model.payReceiveNo))
                .pipe(
                  catchError(() => {
                    this.deleteConfirm = false;
                    if (!this.navigateOperation)
                      this.addNew();

                    return of();
                  })
                )
              })
            )
          ),
          take(2)
        )
        .subscribe({
          next: res =>{
            this.deleteConfirm = false;
            this.isNew = false;
    
            this.showMessage(this.deleteMsg,MessageType.Info);

            this.model = res;
            this.initPayReceiveForm();
            
            if (!this.dialogMode) {
              history.pushState({},'',`/treasury/${this.urlPath}/by-no?no=${res.payReceiveNo}`)
            }
          },
          error: err =>{
            this.showMessage(this.errorHandlingService.handleError(err),MessageType.Error);
            this.deleteConfirm = false;

          }
        })
      }
    } else {
      this.deleteConfirm = false
    }
  }

  onSave(e?) {
    let insertUrl = this.urlPath == 'payments'? PayReceiveApi.Payments: PayReceiveApi.Receipts;
    let editUrl = this.urlPath == 'payments'? PayReceiveApi.Payment: PayReceiveApi.Receipt;
    let value = this.editForm.value;

    let request = this.model.id>0?
      this.payReceive.edit(String.Format(editUrl,this.model.id),value):
      this.payReceive.insert(insertUrl,value);

    request
    .pipe(
      shareReplay()
    )
    .subscribe({
      next: async (res) => {
        if (this.isNew){
          this.showMessage(this.insertMsg, MessageType.Succes);
        }
        else {
          this.showMessage(this.updateMsg, MessageType.Succes);
        }
        res.hasPrevious = this.model.hasPrevious;
        res.hasNext = this.model.hasNext;
        
        this.isNew = false;
        this.model = res as PayReceiveInfo;
        this.initPayReceiveForm();
        // this.setEditMode = true;
        this.errorMessages = undefined;
      },
      error: (error) => {
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
    });
  }

  setTotalCashAmount(event) {
    this.totalCashAmount = event;
  }

  getCurrencies() {
    this.lookupService.GetLookup(LookupApi.CurrenciesInfo).subscribe((res) => {
      this.currenciesRows = res;
      if (this.model != undefined && this.model.currencyId != undefined) {
        // this.isDisplayCurrencyInfo = true;
        this.selectedCurrencyValue = this.model.currencyId;
        
        // var cdValue =
        //   this.model.credit > 0 ? this.model.credit : this.model.debit;
        // this.currencyRate =
        //   cdValue && this.model.currencyValue
        //     ? cdValue / this.model.currencyValue
        //     : undefined;

        var currency = this.currenciesRows.find(
          (f) => f.id == this.model.currencyId
        );
        this.decimalCount = currency ? currency.decimalCount : 0;
      }
    });
  }

  onChangeCurrency() {
    if (this.selectedCurrencyValue) {
      var selectedCurrency = this.currenciesRows.find(
        (f) => f.id == this.selectedCurrencyValue
      );
      this.decimalCount = selectedCurrency.decimalCount;
      this.currencyRate = selectedCurrency.lastRate;

      if (this.totalCashAmount) {
        // this.totalCashAmount = this.currencyValue * this.currencyRate;
        this.currencyValue = this.totalCashAmount / this.currencyRate;
      }
    } else {
      this.decimalCount = 0;
      this.currencyRate = undefined;
    }

    if (this.selectedCurrencyValue == 0)
      this.editForm.patchValue({
        currencyId: undefined,
        currencyValue: undefined,
      });
  }

  changeCurrencyValue(e) {
    var cdValue = this.totalCashAmount;

    var currencyValue = this.currencyValue;

    if (this.selectedCurrencyValue) {
      //#region آپشن فعال است و با تغییر مبلغ ارزی، مبلغ ریالی تغییر میکند

      if (this.currencyRate) {
        cdValue = currencyValue ? this.currencyRate * currencyValue : undefined;
      }

      this.totalCashAmount = cdValue;
      //#endregion
    } else {
      //#region آپشن غیرفعال است و با تغییر مبلغ ارزی، نرخ ارز تغییر میکند
      if (this.selectedCurrencyValue) {
        if (cdValue && currencyValue) {
          this.currencyRate = cdValue / currencyValue;
        } else {
          this.currencyRate = this.currenciesRows.find(
            (f) => f.id == this.selectedCurrencyValue
          ).lastRate;
        }
      }

      
      //endregion
    }
  }

  onChangeCurrencyRate() {
    var cdValue = undefined;
    var currencyValue = this.currencyValue;
    if (this.currencyRate && currencyValue) {
      cdValue = this.currencyRate * currencyValue;
    } else {
      cdValue = undefined;
    }

    // this.totalCashAmount = cdValue;
    if (this.totalCashAmount) {
      this.currencyValue = this.totalCashAmount / this.currencyRate;
    }
  }

  confirmedBy(e) {
    let confirmApiUrl = this.type == 1? PayReceiveApi.ConfirmPayment: PayReceiveApi.ConfirmReceipt;
    let undoConfirmApiUrl = this.type == 1? PayReceiveApi.UndoConfirmPayment: PayReceiveApi.UndoConfirmReceipt;

    let apiUrl = String.Format(
      this.isConfirmed
        ? undoConfirmApiUrl
        : confirmApiUrl,
      this.model.id
    );

    let permission = this.isConfirmed? 'UndoConfirm': 'Confirm';

    this.changeStatus(apiUrl, permission, {
      next: () => {
        e.target.checked = !this.isConfirmed;
      },
      error: () => {
        e.target.checked = this.isConfirmed;
      }
    })
  }

  approvedBy(e) {
    let approveApiUrl = this.type == 1? PayReceiveApi.ApprovePayment: PayReceiveApi.ApproveReceipt;
    let undoApproveApiUrl = this.type == 1? PayReceiveApi.UndoApprovePayment: PayReceiveApi.UndoApproveReceipt;

    let apiUrl = String.Format(
      this.isApproved
        ? undoApproveApiUrl
        : approveApiUrl,
      this.model.id
    );

    let permission = this.isConfirmed? 'UndoApprove': 'Approve';

    this.changeStatus(apiUrl, permission,{
      next: () => {
        e.target.checked = !this.isApproved;
      },
      error: () => {
        e.target.checked = this.isApproved;
      }
    })
  }

  changeStatus(apiUrl,permission:string, cb:{next?:Function, error?:Function}) {
    let permissionList = this.type == 1? PaymentPermissions: ReceiptPermissions;

    let hasPermission = this.isAccess(
      this.entityType,
      permissionList[permission]
    );

    if (hasPermission) {
      lastValueFrom(this.payReceive.changeStatus(apiUrl))
      .then((res) => {
        cb.next(res);
        this.getDataUrl = String.Format(
          this.type == PayReceiveTypes.Payment? PayReceiveApi.PaymentByNo: PayReceiveApi.ReceiptByNo,
          this.model.payReceiveNo
        )
        this.getPayReceive(this.getDataUrl);
      })
      .catch((err) => {
        cb.error(err);
        if (err)
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
      })
    } else {
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
    }
  }

  showReport() {}

  saveChangesHandler() {
    this.onSave();
  }

  discardChangesHandler() {
    if (this.isNew && this.insertedInNew) {
      this.deleteModel(true);
    }
  }
  /**
   * prepare confim message for delete operation
   * @param text is a part of message that use for delete confirm message
   */
  public prepareDeleteConfirm(text: string) {
    this.translate
      .get("Messages.DeleteConfirm")
      .subscribe((msg: string) => {
        this.deleteConfirmMsg = String.Format(msg, text);
      });
  }

  stringFormat(format:string,...args) {
    return String.Format(format,...args);
  }

  breadCrumbTitleFormat(format:string,...args) {
    let strings = of(format,...args);
    let textList = [];

    strings
    .pipe(
      concatMap((res) => this.translate.get(res)),
      take(args.length+1)
    )
    .subscribe({
      next: res => {
        textList.push(res);
      },
      complete: () => {
        let textList2 = [].concat(textList);
        let firstText = textList2[0];
        textList.shift()
        
        this.breadCrumbTitle = String.Format(firstText,...textList);
        
        console.log(this.breadCrumbTitle);
      }
    });
  }

}
