import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
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
import { NumberValidators } from '@sppc/shared/directive/Validator/Sppc-nationalCodeValidator';
import { Entities, Layout, MessageType } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, ErrorHandlingService, MetaDataService } from '@sppc/shared/services';
import { checkBookOperations } from '@sppc/treasury/models/chechBookOperations';
import { CheckBook, CheckBookPage } from '@sppc/treasury/models/checkBook';
import { CheckBooksApi } from '@sppc/treasury/service/api/checkBooksApi';
import { CheckBookInfo, CheckBookService } from '@sppc/treasury/service/check-book.service';
import { ToastrService } from 'ngx-toastr';
import { concatMap, exhaustMap, lastValueFrom, map, take } from 'rxjs';


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

  @Input() public model: CheckBookInfo = new CheckBookInfo();
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';
  @Input() filter: FilterExpression;
  @Input() quickFilter: FilterExpression;
  @Input() dialogMode = false;
  @Input() set checkBookItem(value:CheckBookInfo) {
    this.model = value;
    this.initFullAccountFromGroup();
    this.initCheckBookForm();
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter()
  editMode = false;
  set setEditMode(value:boolean){
    this.editForm.get('checkBookNo')[!value ? 'enable' : 'disable']();
    this.editForm.get('name')[!value ? 'enable' : 'disable']();
    this.editForm.get('bankName')[!value ? 'enable' : 'disable']();
    this.editForm.get('startNo')[!value ? 'enable' : 'disable']();
    this.editForm.get('endNo')[!value ? 'enable' : 'disable']();
    this.editForm.get('sayyadStartNo')[!value ? 'enable' : 'disable']();
    this.editForm.get('seriesNo')[!value ? 'enable' : 'disable']();

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
    {key: -1, value: "CheckBook.Other"}
  ];
  selectedPagesCount: number;
  pageCountValidator: string;
  checkBookPages = [];
  isFirstCheckBook = false;
  isLastCheckBook = false;
  checkBookOperationsItem = checkBookOperations;
  fullAccountForm:FormGroup;
  deleteConfirmBox = false;
  searchConfirm = false;
  checkBookNo:number;
  lastModel: CheckBookInfo = new CheckBookInfo();

  get urlMode() {
    let mode = this.route.snapshot.paramMap.get('mode');
    return mode?mode.toLowerCase():'';
  }

  get returnUrl() {
    let rurl = this.route.snapshot.queryParamMap.get('returnUrl');
    return rurl?rurl.toLowerCase():'';
  }

  get dateQueryParam() {
    let date = this.route.snapshot.queryParamMap.get('date');
    return date?date:'';
  }

  get noQueryParam() {
    let no = this.route.snapshot.queryParamMap.get('no');
    return no?no:'';
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
       ViewName.CheckBook,elem);
       this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  viewId;
  entityTypeName;
  public set entityName(name: string) {
    this.entityTypeName = name;
    this.localizeMsg();
  }
  breadCrumbTitle;

  ngOnInit(): void {
    this.entityName = Entities.CheckBook;
    this.viewId = ViewName[this.entityTypeName];

    if (this.urlMode) {
      this.model = new CheckBookInfo();
    }
    this.initFullAccountFromGroup();
    this.initCheckBookForm();
    let url;

    this.route.paramMap.subscribe(param => {
      switch (param.get('mode')) {
        case 'new':
          this.addNew();
          break;
  
        case 'next':
          this.breadCrumbTitle = 'CheckBook';

          if (!this.isFirstCheckBook) {
            url = String.Format(CheckBooksApi.NextCheckBook,
              this.dateQueryParam?this.dateQueryParam:this.model.issueDate.toISOString());
            this.getCheckBook(url);
          }
          break;
  
        case 'previous':
          this.breadCrumbTitle = 'CheckBook';
          if (this.dateQueryParam || this.model.id) {
            url = String.Format(CheckBooksApi.PreviousCheckBook,
              this.dateQueryParam?this.dateQueryParam:this.model.issueDate.toISOString());
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
          if (!this.noQueryParam)
            this.searchConfirm = true;
          else {
            let url = String.Format(CheckBooksApi.CheckBookByNo,this.noQueryParam);
            this.getCheckBook(url);
          }
          break;
      
        default:
          if(this.isNew)
            this.addNew();
          break;
      }
    });

  }

  initCheckBookForm(insert=false) {
    if (this.model.id == 0) {
      this.model.branchId = this.BranchId;
      this.model.issueDate = new Date();
      this.checkBookPages = [];
      this.selectedPagesCount = undefined;
      this.fullAccountForm.reset();

      this.isNew = true;
      this.searchConfirm = false;
      this.setEditMode = false;
      this.isLastCheckBook = true;
      this.isFirstCheckBook = false;
    } else {
      if (this.model.fullAccount.account.id) {
        this.fullAccountForm.patchValue({
          fullAccount: this.model.fullAccount
        });
      } else {
        this.model.fullAccount = this.fullAccountForm.value.fullAccount;
      }

      this.selectedPagesCount = this.model.pageCount;

      this.isNew = false;
      this.setEditMode = true;
      this.searchConfirm = false;
      if (!insert) {
        this.isLastCheckBook = !this.model.hasNext;
        this.isFirstCheckBook = !this.model.hasPrevious;
      }
    }
    this.errorMessages = [];

    setTimeout(() => {
      this.editForm.reset(this.model);
    }, 0);
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
    this.breadCrumbTitle = 'NewCheckBook';
    if (this.urlMode != 'new' && !this.dialogMode){
      this.router.navigate(['/treasury/check-books/new']);
    } else {
      this.model = new CheckBookInfo();
      this.initCheckBookForm();
      this.checkBookPages = [];
      this.isNew = true;
      this.errorMessages = undefined;
      this.getCheckBook(CheckBooksApi.NewCheckBook,true)
    }
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
        .subscribe({
          next: res =>{
            this.deleteConfirmBox = false;
    
            this.showMessage(this.deleteMsg,MessageType.Info);
            lastValueFrom(this.checkBookService.
              getModelsByFilters(String.Format(CheckBooksApi.NextCheckBook,this.model.issueDate),
                this.filter,
                this.quickFilter
                )
              ).then((next:CheckBook) => {
                this.checkBookItem = next;

                if (!this.dialogMode) {
                  history.pushState(null,null,`/treasury/check-books/by-no?no=${next.checkBookNo}`)
                }
                  // this.router.navigate(['/treasury/check-books/by-no'],{queryParams:{
                  //   no: next.checkBookNo
                  // }});
              }).catch(err => {
                //if next checkbook not exists try for previous checkbook;
                this.addNew();
                  if (!this.dialogMode)
                    this.router.navigate(["/treasury/check-books/"]);
              })
          },
          error: err =>{
            this.showMessage(this.errorHandlingService.handleError(err),MessageType.Error);
          }
        })
      }
    } else {
      this.deleteConfirmBox = false
    }
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
  getCheckBook(getUrl:string,isNew=false) {
    this.checkBookService.getModelsByFilters(getUrl,this.filter,this.quickFilter)
    .pipe(
      take(2)
    )
    .subscribe({
      next: res => {
        if (this.urlMode == 'by-no') {
          this.searchConfirm = false;
        }

        this.model = res;
        this.initCheckBookForm();
      },
      error: (err) => {
        if (err == null || err.statusCode == 404) {
          this.isFirstCheckBook = true;
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
              this.router.navigate(['/treasury/check-books/new']);
          };
        }

        if (err != null && err.statusCode == 400) {
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
          this.router.navigate(["/treasury/check-books"]);
        }
      }
    })
  }

  goPrevious() {
    this.breadCrumbTitle = "CheckBook";
    let url;

    if (this.urlMode != 'previous' && !this.dialogMode) {
      let issueDate = this.model.id > 0? this.model.issueDate: '';
      this.router.navigate(['/treasury/check-books/previous'],{
        queryParams: {
          date: issueDate
        }
      });
    } else {
      if (this.model.id) {
        url = String.Format(CheckBooksApi.PreviousCheckBook,this.model.issueDate);
      } else {
        url = CheckBooksApi.LastCheckBook;
      }
      this.getCheckBook(url);
      let issueDate = this.model.id > 0? this.model.issueDate: '';
      if (!this.dialogMode)
        this.router.navigate(['/treasury/check-books/previous'],{
          queryParams: {
            date: issueDate
          }
        });
    }
  }

  goNext() {
    this.breadCrumbTitle = "CheckBook";
    let url;
    if (this.urlMode != 'next' && !this.dialogMode) {
      let issueDate = this.model.id > 0? this.model.issueDate: '';
      this.router.navigate(['/treasury/check-books/next'],{
        queryParams: {
          date: issueDate
        }
      });
    } else {
      if (!this.isLastCheckBook) {
        url = String.Format(CheckBooksApi.NextCheckBook,this.model.issueDate);
        this.getCheckBook(url);
        let issueDate = this.model.id > 0? this.model.issueDate: '';
        if (!this.dialogMode)
          this.router.navigate(['/treasury/check-books/next'],{
            queryParams: {
              date: issueDate
            }
          });
      }
    }
  }

  goFirst() {
    this.breadCrumbTitle = 'CheckBook';
    let url;
    if (this.urlMode != 'first' && !this.dialogMode) {
      this.router.navigate(['/treasury/check-books/first']);
    } else {
      url = CheckBooksApi.FirstCheckBook;
      this.getCheckBook(url);
    }
  }

  goLast() {
    this.breadCrumbTitle = 'LastCheckBook';
    let url;
    if (this.urlMode != 'last' && !this.dialogMode) {
      this.router.navigate(['/treasury/check-books/last']);
    } else {
      url = CheckBooksApi.LastCheckBook;
      this.getCheckBook(url);
    }
  }

  goSearch() {
    this.breadCrumbTitle = "CheckBook";
    this.searchConfirm = true;
    if (this.urlMode != 'by-no' && !this.dialogMode) {
      this.router.navigate(['/treasury/check-books/by-no'],{queryParams:{
        returnUrl: "/treasury/check-books/"+this.urlMode
      }});
    }
  }

  searchByNo(searchConfirm = false) {
    this.breadCrumbTitle = "CheckBook";
    let url;
    if (searchConfirm) {
      if (this.checkBookNo && !this.dialogMode) {
        this.router.navigate(['/treasury/check-books/by-no'],{queryParams:{
          no: this.checkBookNo,
          returnUrl: this.returnUrl
        }});
        url = String.Format(CheckBooksApi.CheckBookByNo,this.checkBookNo);
        // this.getCheckBook(url);
      } else {
        return;
      }
    } else {
      this.searchConfirm = false;
      if (this.returnUrl)
        this.router.navigate([this.returnUrl]);
      else
        this.router.navigate(['/treasury/check-books/new']);
    }
  }

  cancelHandler() {
    this.errorMessages = undefined;
  }

  onSave(e){
    let value = this.editForm.value;

    let request = this.model.id>0?
      this.checkBookService.edit(String.Format(CheckBooksApi.CheckBook,this.model.id),value):
      this.checkBookService.insert(CheckBooksApi.CheckBooks,value);

    request
    .pipe(
      concatMap(checkBook => {
        return this.checkBookService.insertPages(checkBook.id)
        .pipe(
          map((pages:CheckBookPage[]) => {
            return {checkBook: checkBook, pages: pages}
          })
        );
      })
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
        
        this.model = res.checkBook as CheckBookInfo;
        this.initCheckBookForm(true);
        this.setEditMode = true;
        this.errorMessages = undefined;

        this.checkBookPages = res.pages;
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
    }
    )
    
  }

  // Events
  onChangePagesCountDropDown(e) {
    console.log(this.editForm.get('issueDate').value);
    
    if (e == -1) {
      this.otherSizeOfPages = true;
      this.selectedPagesCount = 1;
    } else {
      this.otherSizeOfPages = false;
      this.editForm.patchValue({
        pageCount: this.selectedPagesCount
      });
    }
  }

  onChangePagesCountInput(e) {
    if (this.selectedPagesCount > 1000) {
      this.editForm.get('pageCount').setValidators(NumberValidators.minMax({max: 1000,min:1}));
      this.selectedPagesCount = 1000;
    } else if(this.selectedPagesCount < 1) {
      this.editForm.get('pageCount').setValidators(NumberValidators.minMax({max: 1000,min:1}));
      this.selectedPagesCount = 1;
    }

    let count = this.selectedPagesCount;
    if (this.pagesCount.find(i => i.key != this.selectedPagesCount))
      this.selectedPagesCount = -1;
    setTimeout(() => {
      this.selectedPagesCount = count;
      this.editForm.patchValue({
        pageCount: this.selectedPagesCount
      });
    }, 0);
  }

  onFullAccountInpusFocuse(e) {}

  onSelectFullAccount(e) {
    let fullAccount = this.fullAccountForm.value.fullAccount;
    this.editForm.patchValue({
      fullAccount: fullAccount
    });
  }

  showReport(){}

  nullPages(isNull:boolean) {
    this.setEditMode = !isNull;
  }
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
