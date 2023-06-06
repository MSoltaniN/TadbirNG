import { Component, ElementRef, Input, OnInit, Renderer2 } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent, FilterExpression, String } from '@sppc/shared/class';
import { Entities, Layout, MessageType } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, ErrorHandlingService, MetaDataService, SessionKeys } from '@sppc/shared/services';
import { PayReceiveTypes, PayReceiveOperations, UrlPathType } from '@sppc/treasury/enums/payReceive';
import { PayReceiveApi } from '@sppc/treasury/service/api';
import { PayReceiveInfo, PayReceiveService } from '@sppc/treasury/service/pay-receive.service';
import { ToastrService } from 'ngx-toastr';
import { shareReplay, take } from 'rxjs';

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
  searchConfirm: boolean;
  urlMode: string;

  constructor(public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public elem:ElementRef,
    private route: ActivatedRoute,
    private payReceive: PayReceiveService,
    private router: Router,
    public errorHandlingService: ErrorHandlingService)
  {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Payment, ViewName.Payment,elem);
    this.entType = this.urlPath;
    this.viewID = this.urlPath;
  }

  isShowBreadcrumb = true;
  payReceiveOperationsItem = PayReceiveOperations;
  isFirstItem = false;
  isLastItem = false;
  deleteConfirm = false;
  type:PayReceiveTypes;
  payReceiveNo;

  public get urlPath() {
    return this.route.snapshot.url[0].path.toLowerCase();
  }
  
  public set entType(type: string) {
    if (type == UrlPathType.Receipts) {
      this.entityType = Entities.Receipt;
      this.type = PayReceiveTypes.Receipt;
    } else {
      this.type = PayReceiveTypes.Payment;
    }
  }

  public set viewID(type: string) {
    if (type == UrlPathType.Receipts) {
      this.viewId = ViewName.Receipt;
      this.metadataKey = String.Format(SessionKeys.MetadataKey, this.viewId ? this.viewId.toString() : '', this.CurrentLanguage);
      this.localizeMsg();
    }
  }

  get returnUrl() {
    let rurl = this.route.snapshot.queryParamMap.get('returnUrl');
    return rurl?rurl.toLowerCase():'';
  }

  public get isConfirmed() : boolean {
    return this.model?.confirmedById > 0;
  }

  get noQueryParam() {
    let no = this.route.snapshot.queryParamMap.get('no');
    return no?no:'';
  }

  ngOnInit(): void {
    console.log(this.route);

    this.route.paramMap.subscribe(param => {
      this.urlMode = param.get('mode');
      switch (param.get('mode')) {
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
          this.goNext();
          break;

        case 'previous':
          this.goPrevious();
          break;

        case 'by-no':
          if (!this.noQueryParam)
            this.searchConfirm = true;
          else {
            let baseUrl = this.urlPath == 'payments'? PayReceiveApi.PaymentByNo: PayReceiveApi.ReceiptByNo;
            let url = String.Format(baseUrl,this.noQueryParam);
            this.getPayReceive(url);
          }
          break;

        default:
          break;
      }
    })
  }

  payReceiveOperation(mode){
    let url;
    switch (mode) {
      case PayReceiveOperations.New:
        this.addNew();
        break;

      case PayReceiveOperations.Next:
        if (!this.isFirstItem) {
          let baseUrl = this.urlPath == 'payments'? PayReceiveApi.NextPayment: PayReceiveApi.NextReceipt;
          url = String.Format(baseUrl,
            this.noQueryParam? this.noQueryParam:this.model.payReceiveNo);
          this.getPayReceive(url);
        }
        break;

      case PayReceiveOperations.Previous:
        let baseUrl = this.urlPath == 'payments'? PayReceiveApi.PreviousPayment: PayReceiveApi.PreviousReceipt;

        if (this.noQueryParam || this.model.id) {
          url = String.Format(baseUrl,
            this.noQueryParam?this.noQueryParam:this.model.payReceiveNo);
        } else {
          url = this.urlPath == 'payments'? PayReceiveApi.LastPayment: PayReceiveApi.LastReceipt;;
        }
        this.getPayReceive(url);
        break;

      case PayReceiveOperations.Last:
        this.goLast();
        break;

      case PayReceiveOperations.First:
        this.goFirst();
        break;

      case PayReceiveOperations.Search:
        this.goSearch();
        break;

      default:
        break;
    }    
  }

  addNew() {
    if (this.urlMode != 'new' && !this.dialogMode){
      this.router.navigate([`/treasury/${this.urlPath}/new`]);
    } else {
      this.isNew = true;
      this.errorMessages = undefined;
      this.getPayReceive(this.urlPath == UrlPathType.Payments?PayReceiveApi.NewPayment: PayReceiveApi.NewReceipt,true);
    }
  }

  goFirst() {
    let url;
    if (this.urlMode != 'first' && !this.dialogMode) {
      this.router.navigate([`/treasury/${this.urlPath}/first`]);
    } else {
      url = this.urlPath == 'payments'? PayReceiveApi.FirstPayment: PayReceiveApi.FirstReceipt;
      this.getPayReceive(url);
    }
  }

  goLast() {
    let url;
    if (this.urlMode != 'first' && !this.dialogMode) {
      this.router.navigate([`/treasury/${this.urlPath}/last`]);
    } else {
      url = this.urlPath == 'payments'? PayReceiveApi.LastPayment: PayReceiveApi.LastPayment;
      this.getPayReceive(url);
    }
  }

  goNext() {
    let baseUrl = this.urlPath == 'payments'? PayReceiveApi.NextPayment: PayReceiveApi.NextReceipt;
    let apiUrl;

    if (this.urlMode != 'next' && !this.dialogMode) {
      let no = this.model.id > 0? this.model.payReceiveNo: '';
      this.router.navigate([`/treasury/${this.urlPath}/next`],{
        queryParams: {
          no: no
        }
      });
    } else {
      if (!this.isLastItem) {
        apiUrl = String.Format(baseUrl,this.model.payReceiveNo);
        this.getPayReceive(apiUrl);
        let no = this.model.id > 0? this.model.payReceiveNo: '';
        if (!this.dialogMode)
          this.router.navigate([`/treasury/${this.urlPath}/next`],{
            queryParams: {
              no: no
            }
          });
      }
    }
  }

  goPrevious() {
    let baseUrl = this.urlPath == 'payments'? PayReceiveApi.PreviousPayment: PayReceiveApi.PreviousReceipt;
    let url;

    if (this.urlMode != 'previous' && !this.dialogMode) {
      let no = this.model.id > 0? this.model.payReceiveNo: '';
      this.router.navigate([`/treasury/${this.urlPath}/previous`],{
        queryParams: {
          no: no
        }
      });
    } else {
      if (this.model.id) {
        url = String.Format(baseUrl,this.model.payReceiveNo);
      } else {
        url = this.urlPath == 'payments'? PayReceiveApi.LastPayment: PayReceiveApi.LastReceipt;
      }
      this.getPayReceive(url);
      let no = this.model.id > 0? this.model.payReceiveNo: '';
      if (!this.dialogMode)
        this.router.navigate([`/treasury/${this.urlPath}/previous`],{
          queryParams: {
            no: no
          }
        });
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
    let url;
    if (searchConfirm) {
      if (this.payReceiveNo && !this.dialogMode) {
        this.router.navigate([`/treasury/${this.urlPath}/by-no`],{queryParams:{
          no: this.payReceiveNo,
          returnUrl: this.returnUrl
        }});
        url = String.Format(baseUrl,this.payReceiveNo);
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
        this.initCheckBookForm()
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

  initCheckBookForm() {
    if (this.model.id == 0) {
      this.isNew = true;
      this.searchConfirm = false;
      this.isLastItem = true;
      this.isFirstItem = false;
    } else {
      this.isNew = false;

      this.searchConfirm = false;
      this.isLastItem = !this.model.hasNext;
      this.isFirstItem = !this.model.hasPrevious;
    }
    this.errorMessages = [];

    setTimeout(() => {
      this.editForm.reset(this.model);
      console.log(this.editForm);
      
    }, 0);
  }

  removeHandler() {
    this.deleteConfirm = true;
    let ent = this.urlPath == "payments"? "Entity.Payment": "Entity.Receipt";
    this.prepareDeleteConfirm(this.getText(ent));
  }

  onSave(e) {
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
        if (this.model.id>0)
          this.showMessage(this.updateMsg, MessageType.Succes);
        else {
          // res.checkBook.hasPrevious = this.lastModel.hasPrevious;
          // res.checkBook.hasNext = this.lastModel.hasNext;
          this.showMessage(this.insertMsg, MessageType.Succes);
        }
        
        this.model = res as PayReceiveInfo;
        this.initCheckBookForm();
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

  showReport() {}

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
}
