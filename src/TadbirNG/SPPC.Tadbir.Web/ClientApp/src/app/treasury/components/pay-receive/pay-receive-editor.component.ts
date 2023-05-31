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
import { take } from 'rxjs';

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
  returnUrl: any;

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

  public get urlPath() {
    return this.route.snapshot.url[0].path;
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

  public get isConfirmed() : boolean {
    return this.model?.confirmedById > 0;
  }
  

  ngOnInit(): void {
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
          this.goSearch();
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
        this.goNext();
        break;

      case PayReceiveOperations.Previous:
        this.goPrevious();
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
      this.router.navigate(['/treasury/check-books/new']);
    } else {
      this.isNew = true;
      this.errorMessages = undefined;
      this.getPayReceive(this.urlPath == UrlPathType.Payments?PayReceiveApi.NewPayment: PayReceiveApi.NewReceipt,true);
    }
  }

  goFirst() {}

  goLast() {}

  goNext() {}

  goPrevious() {}

  goSearch() {}

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

  removeHandler() {}

  onSave(e) {}

  showReport() {}
}
