import { Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, Renderer2, SimpleChanges, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DialogRef, DialogService } from '@progress/kendo-angular-dialog';
import { GridComponent } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent, FilterExpression, String } from '@sppc/shared/class';
import { ReportViewerComponent, ViewIdentifierComponent } from '@sppc/shared/components';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { SelectFormComponent } from '@sppc/shared/controls';
import { Entities, Layout, MessageType } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, ErrorHandlingService, GridService, MetaDataService, SessionKeys } from '@sppc/shared/services';
import { checkBookOperations } from '@sppc/treasury/models/chechBookOperations';
import { CheckBook } from '@sppc/treasury/models/checkBook';
import { CheckBooksApi } from '@sppc/treasury/service/api/checkBooksApi';
import { CheckBookInfo, CheckBookService } from '@sppc/treasury/service/check-book.service';
import { ToastrService } from 'ngx-toastr';
import { exhaustMap, lastValueFrom, take } from 'rxjs';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'check-book-editor',
  templateUrl: './check-book-editor.component.html',
  styleUrls: ['./check-book-editor.component.css'],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class CheckBookEditorComponent extends DetailComponent implements OnInit {

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  @Input() public model: CheckBookInfo;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';
  @Input() filter: FilterExpression;
  @Input() quickFilter: FilterExpression;

  editMode = false;
  set setEditMode(value:boolean){
    this.editForm.get('checkBookNo')[!value ? 'enable' : 'disable']();
    this.editForm.get('name')[!value ? 'enable' : 'disable']();
    this.editForm.get('bankName')[!value ? 'enable' : 'disable']();
    this.editForm.get('startNo')[!value ? 'enable' : 'disable']();
    this.editForm.get('endNo')[!value ? 'enable' : 'disable']();
    this.editMode = value;
  };
  otherSizeOfPages = false;
  checkBookList: CheckBook[] = [];
  selectedCheckBook;
  pagesCount = [
    {key: 10, value: 10},
    {key: 25, value: 25},
    {key: 50, value: 50},
    {key: 100, value: 100},
    {key: -1, value: "other"}
  ];
  selectedPagesCount: number;
  checkBookPages;
  isFirstCheckBook = false;
  isLastCheckBook = false;
  checkBookOperationsItem = checkBookOperations;
  fullAccountForm:FormGroup;
  deleteConfirmBox = false;
  searchConfirm = false;
  checkBookNo:number;

  get urlMode() {
    let mode = this.route.snapshot.paramMap.get('mode');
    return mode?mode.toLowerCase():'';
  }

  get returnUrl() {
    let rurl = this.route.snapshot.queryParamMap.get('returnUrl');
    return rurl?rurl.toLowerCase():'';
  }

  //
  dialogRef: DialogRef;
  dialogModel: any;

  constructor(public toastrService: ToastrService,
     public translate: TranslateService,
     public bStorageService: BrowserStorageService,
     public renderer: Renderer2,
     public metadata: MetaDataService,
     public elem:ElementRef,
     public dialogService: DialogService,
     private route: ActivatedRoute,
     private checkBookService: CheckBookService,
     public errorHandlingService: ErrorHandlingService,
     private router: Router)
  {
    super(toastrService, translate,
       bStorageService, renderer,
       metadata, Entities.CheckBook,
       ViewName.CheckBookReport,elem);
  }
  viewId;
  entityTypeName;

  ngOnInit(): void {
    this.entityTypeName = Entities.CheckBook;
    this.viewId = ViewName[this.entityTypeName];

    this.isNew = true;
    this.model = new CheckBookInfo();
    this.initFullAccountFromGroup();
    this.initCheckBookForm();
    let url;
    console.log(this.urlMode);
    this.route.paramMap.subscribe(param => {
      switch (param.get('mode')) {
        case 'new':
          // this.model = new CheckBookInfo();
          this.isLastCheckBook = true;
          this.initCheckBookForm();
          break;
  
        case 'next':
          if (!this.isFirstCheckBook) {
            url = String.Format(CheckBooksApi.NextCheckBook,this.model.issueDate);
            this.getCheckBook(url);
          }
          break;
  
        case 'previous':
          if (this.model.id) {
            url = String.Format(CheckBooksApi.PreviousCheckBook,this.model.issueDate);
          } else {
            url = CheckBooksApi.LastCheckBook;
          }
          this.getCheckBook(url);
          break;
  
        case 'first':
          this.goFirst();
          break;
  
        case 'last':
          this.goLast();
          break;
  
        case 'by-no':
          this.searchConfirm = true;
          break;
      
        default:
          break;
      }
    })
  }

  initCheckBookForm() {
    if (this.model.id == 0) {
      this.model.branchId = this.BranchId;
      this.model.issueDate = new Date();
      this.selectedPagesCount = undefined;
      this.fullAccountForm.reset();

      this.searchConfirm = false;
      this.setEditMode = false;
      this.isLastCheckBook = true;
      this.isFirstCheckBook = false;
    } else {
      this.fullAccountForm.patchValue({
        fullAccount: this.model.fullAccount
      });

      this.selectedPagesCount = this.model.pageCount;

      this.setEditMode = true;
      this.searchConfirm = false;
      this.isLastCheckBook = !this.model.hasNext;
      this.isFirstCheckBook = !this.model.hasPrevious;
    }
    console.log(this.editForm,this.fullAccountForm);
    
    this.editForm.reset(this.model);
  }

  initFullAccountFromGroup() {
    this.fullAccountForm = new FormGroup({
      fullAccount: new FormGroup({
        account: new FormGroup({
          id: new FormControl("", Validators.required),
          name: new FormControl(),
          fullCode: new FormControl(),
        }),
        detailAccount: new FormGroup({
          id: new FormControl(),
          name: new FormControl(),
          fullCode: new FormControl(),
        }),
        costCenter: new FormGroup({
          id: new FormControl(),
          name: new FormControl(),
          fullCode: new FormControl(),
        }),
        project: new FormGroup({
          id: new FormControl(),
          name: new FormControl(),
          fullCode: new FormControl(),
        }),
      }),
    });
  }

  addNew() {
    this.model = new CheckBookInfo();
    this.initCheckBookForm();
    this.checkBookPages = undefined;
    this.router.navigate(['/treasury/check-books/new']);
  }

  removeHandler() {
    this.deleteConfirmBox = true;
    this.prepareDeleteConfirm(this.getText("Entity.CheckBook"));
  }

  deleteModel(confirm:boolean) {
    if (confirm) {
      if (this.model.id) {
        let deletePagesURL = String.Format(CheckBooksApi.CheckBookPages,this.model.id);
        let deleteCheckBookURL = String.Format(CheckBooksApi.CheckBook,this.model.id);
        this.checkBookService.delete(deletePagesURL)
        .pipe(
          exhaustMap(() => this.checkBookService.delete(deleteCheckBookURL))
        )
        .subscribe( res =>{
          // this.model = new CheckBookInfo();
          // this.editForm.reset(this.model);
          // this.initCheckBookForm();
          this.router.navigate(['/treasury/check-books/new'])
          this.deleteConfirmBox = false;
  
          this.showMessage(this.deleteMsg,MessageType.Info);
        }, err =>{
          this.showMessage(this.errorHandlingService.handleError(err),MessageType.Error);
        })
      }
    } else {
      this.deleteConfirmBox = false
    }
  }

  nullPages() {
    this.setEditMode = false;
  }

  checkOperation(mode){
    let url;
    switch (mode) {
      case checkBookOperations.New:
        this.addNew();
        break;

      case checkBookOperations.Next:
        this.goNext();
        break;

      case checkBookOperations.Previous:
        this.goPrevious();
        break;

      case checkBookOperations.Last:
        this.goLast();
        break;

      case checkBookOperations.First:
        this.goFirst();
        break;

      case checkBookOperations.Search:
        this.goSearch();
        break;

      default:
        break;
    }
  }

  /**
   * برای واکشی دسته چک
   * @param getUrl آدرس واکشی دیتا
   */
  getCheckBook(getUrl:string) {
    this.checkBookService.getModelsByFilters(getUrl,this.filter,this.quickFilter)
    .pipe(
      take(2)
    )
    .subscribe(res => {
        console.log(res);
        if (this.urlMode == 'by-no') {
          this.searchConfirm = false;
        }
        this.model = res;
        this.initCheckBookForm()
      }, (err) => {
        console.log(err);
        if (err == null || err.statusCode == 404) {
          this.isFirstCheckBook = true;
          this.showMessage(
            this.getText("CheckBook.CheckBookNotFound"),
            MessageType.Warning
          );
        }

        if (err != null && err.statusCode == 400) {
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
          this.router.navigate(["/treasury/check-books"]);
        }
      }
    )
  }

  goPrevious() {
    let url;
    if (this.urlMode != 'previous') {
      this.router.navigate(['/treasury/check-books/previous']);
    } else {
      if (this.model.id) {
        url = String.Format(CheckBooksApi.PreviousCheckBook,this.model.issueDate);
      } else {
        url = CheckBooksApi.LastCheckBook;
      }
      this.getCheckBook(url);
    }
  }

  goNext() {
    let url;
    if (this.urlMode != 'next') {
      this.router.navigate(['/treasury/check-books/next']);
    } else {
      if (!this.isLastCheckBook) {
        url = String.Format(CheckBooksApi.NextCheckBook,this.model.issueDate);
        this.getCheckBook(url);
      }
    }
  }

  goFirst() {
    let url;
    if (this.urlMode != 'first') {
      this.router.navigate(['/treasury/check-books/first']);
    } else {
      url = CheckBooksApi.FirstCheckBook;
      this.getCheckBook(url);
    }
  }

  goLast() {
    let url;
    if (this.urlMode != 'last') {
      this.router.navigate(['/treasury/check-books/last']);
    } else {
      url = CheckBooksApi.LastCheckBook;
      this.getCheckBook(url);
    }
  }

  goSearch() {
    let url;
    if (this.urlMode != 'by-no') {
      setTimeout(() => {
        this.searchConfirm = true;
      }, 0);
      this.router.navigate(['/treasury/check-books/by-no'],{queryParams:{
        returnUrl: "/treasury/check-books/"+this.urlMode
      }});
    }
  }

  searchByNo(searchConfirm = false) {
    let url;
    if (searchConfirm) {
      if (this.checkBookNo) {
        this.router.navigate(['/treasury/check-books/by-no'],{queryParams:{
          returnUrl: "/treasury/check-books/"+this.urlMode,
          no: this.checkBookNo
        }});
        url = String.Format(CheckBooksApi.CheckBookByNo,this.checkBookNo);
        this.getCheckBook(url);
      } else {
        return;
      }
    } else {
      this.searchConfirm = false;
      this.router.navigate([this.returnUrl]);
    }
  }

  cancelHandler() {
    this.errorMessages = undefined;
  }

  onSave(e){
    console.log(this.editForm);
    
    // if (!this.editForm.valid)
    //  return;

    let value = this.editForm.value;
    value.pageCount = this.selectedPagesCount;
    value.fullAccount = this.fullAccountForm.value.fullAccount;

    let request = this.editMode?
      this.checkBookService.edit(CheckBooksApi.CheckBooks,value):
      this.checkBookService.insert(CheckBooksApi.CheckBooks,value);

    request.subscribe(
      async (res) => {
        this.model = res as CheckBookInfo;
        if (this.editMode)
          this.showMessage(this.updateMsg, MessageType.Succes);
        else
          this.showMessage(this.insertMsg, MessageType.Succes);

        this.initCheckBookForm();
        this.errorMessages = undefined;

        this.checkBookPages = await lastValueFrom(this.checkBookService.insertPages(this.model.id));
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
    )
    
  }

  pagesForm1;
  pagesForm() {
    this.pagesForm1 = new FormGroup({
      id: new FormControl(),
      checkBookID: new FormControl("", Validators.required),
      checkBookPageID: new FormControl("", Validators.required),
      checkID: new FormControl("", Validators.required),
      serialNo: new FormControl("", Validators.required),
      status: new FormControl("", Validators.required)
    })
  }

  // Events
  onChangePagesCountDropDown(e) {
    if (e == -1) {
      this.otherSizeOfPages = true;
      this.selectedPagesCount = 1;
    } else {
      this.otherSizeOfPages = false;
      // this.setEndNo();
    }
  }

  onChangePagesCountInput(e) {
    // this.setEndNo();
  }

  onFullAccountInpusFocuse(e) {}

  onSelectFullAccount(e) {
    let fullAccount = this.fullAccountForm.value.fullAccount;
    this.editForm.patchValue({
      accountId: fullAccount.account.id? fullAccount.account.id: null,
      detailAccountId: fullAccount.detailAccount.id? fullAccount.detailAccount.id: null,
      costCenterId: fullAccount.costCenter.id? fullAccount.costCenter.id: null,
      projectId: fullAccount.project.id? fullAccount.project.id: null
    })
  }

  showReport(){}

  /**
   * prepare confim message for delete operation
   * @param text is a part of message that use for delete confirm message
   */
  public prepareDeleteConfirm(text?: string) {
    this.translate
      .get("Messages.DeleteConfirm")
      .subscribe((msg: string) => {
        this.deleteConfirmMsg = String.Format(msg, text);
      });
  }
}
