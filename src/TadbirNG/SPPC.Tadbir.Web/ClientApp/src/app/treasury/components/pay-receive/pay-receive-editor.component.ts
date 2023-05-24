import { Component, ElementRef, Input, OnInit, Renderer2 } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent, FilterExpression, String } from '@sppc/shared/class';
import { Entities, Layout, MessageType } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, ErrorHandlingService, MetaDataService, SessionKeys } from '@sppc/shared/services';
import { payReceiveOperations, urlPathType } from '@sppc/treasury/enums/payReceive';
import { PayReceive } from '@sppc/treasury/models/payReceive';
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
  payReceiveOperationsItem = payReceiveOperations;
  isFirstItem = false;
  isLastItem = false;
  deleteConfirm = false;

  public get urlPath() {
    return this.route.snapshot.url[0].path;
  }
  
  public set entType(type: string) {
    if (type == urlPathType.receivals) {
      this.entityType = Entities.Receival;
    }
  }

  public set viewID(type: string) {
    if (type == urlPathType.receivals) {
      this.viewId = ViewName.Receival;
      this.metadataKey = String.Format(SessionKeys.MetadataKey, this.viewId ? this.viewId.toString() : '', this.CurrentLanguage);
      this.localizeMsg();
    }
  }

  public get isConfirmed() : boolean {
    return this.model?.confirmedById > 0;
  }
  

  ngOnInit(): void {
    console.log(this.urlPath,this.route,this.viewId,this.entityType);
    this.initform();
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
  public form1: FormGroup;

  payReceiveOperation(mode){
    let url;
    // switch (mode) {
    //   case checkBookOperations.New:
    //     this.addNew();
    //     break;

    //   case checkBookOperations.Next:
    //     this.goNext();
    //     break;

    //   case checkBookOperations.Previous:
    //     this.goPrevious();
    //     break;

    //   case checkBookOperations.Last:
    //     this.goLast();
    //     break;

    //   case checkBookOperations.First:
    //     this.goFirst();
    //     break;

    //   case checkBookOperations.Search:
    //     this.goSearch();
    //     break;

    //   default:
    //     break;
    // }
  }

  initform() {
    this.form1 = new FormGroup({
      id: new FormControl(''),
      fiscalPeriodId: new FormControl(''),
      branchId: new FormControl(''),
      payReceiveNo: new FormControl(''),
      reference: new FormControl(''),
      issuedById: new FormControl(''),
      modifiedById: new FormControl(''),
      confirmedById: new FormControl(''),
      approvedById: new FormControl(''),
      type: new FormControl(''),
      currencyRate: new FormControl(''),
      description: new FormControl(''),
      date: new FormControl(''),
      issuedByName: new FormControl(''),
      modifiedByName: new FormControl(''),
      confirmedByName: new FormControl(''),
      approvedByName: new FormControl(''),
      currency: new FormControl('')
    })
  }

  addNew() {}

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
      this.model.branchId = this.BranchId;
      this.model.fiscalPeriodId = this.FiscalPeriodId;
      this.isNew = true;
      this.searchConfirm = false;
      // this.setEditMode = false;
      this.isLastItem = true;
      this.isFirstItem = false;
    } else {
      this.isNew = false;
      // this.setEditMode = true;
      this.searchConfirm = false;
      this.isLastItem = !this.model.hasNext;
      this.isFirstItem = !this.model.hasPrevious;
    }
    this.errorMessages = [];

    setTimeout(() => {
      this.editForm.reset(this.model);
    }, 0);
  }

  removeHandler() {}

  onSave(e) {}

  showReport() {}
}
