import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { DialogRef, DialogService } from '@progress/kendo-angular-dialog';
import { GridComponent } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent, String } from '@sppc/shared/class';
import { ReportViewerComponent, ViewIdentifierComponent } from '@sppc/shared/components';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { SelectFormComponent } from '@sppc/shared/controls';
import { Entities, Layout, MessageType } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, ErrorHandlingService, GridService, MetaDataService, SessionKeys } from '@sppc/shared/services';
import { ShareDataService } from '@sppc/shared/services/share-data.service';
import { CheckBook } from '@sppc/treasury/models/checkBook';
import { CheckBooksApi } from '@sppc/treasury/service/api/checkBooksApi';
import { CheckBookInfo, CheckBookService } from '@sppc/treasury/service/check-book.service';
import { ToastrService } from 'ngx-toastr';
import { lastValueFrom } from 'rxjs';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'check-book',
  templateUrl: './check-book.component.html',
  styleUrls: ['./check-book.component.css'],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class CheckBookComponent extends DetailComponent implements OnInit {

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  @Input() public model: CheckBook = new CheckBookInfo();
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<CheckBook> = new EventEmitter();

  editMode = false;
  set setEditMode(value:boolean){
    this.editForm.get('name')[!value ? 'enable' : 'disable']();
    this.editForm.get('bankName')[!value ? 'enable' : 'disable']();
    this.editForm.get('startNo')[!value ? 'enable' : 'disable']();
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

  constructor(public toastrService: ToastrService,
     public translate: TranslateService,
     public bStorageService: BrowserStorageService,
     public renderer: Renderer2,
     public metadata: MetaDataService,
     public elem:ElementRef,
     public dialogService: DialogService,
     private sharedDataService: ShareDataService,
     private checkBookService: CheckBookService,
     public errorHandlingService: ErrorHandlingService)
  {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.CheckBook, ViewName.CheckBookReport,elem);
  }
  editForm1:FormGroup;

  ngOnInit(): void {
    this.isNew = true;
    this.initFullAccountFromGroup();
    setTimeout(() => {
      if (this.model.id == 0) {
        this.model.branchId = this.BranchId;
        this.model.issueDate = new Date();
        this.model.checkBookNo = 0;
        this.setEditMode = false;
      } else {
        this.setEditMode = true;
      }
      this.editForm.reset(this.model);
      console.log(this.editForm,this.editMode);
    })
  }

  initFullAccountFromGroup() {
    this.editForm1 = new FormGroup({
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
    if (!this.editForm.valid)
      return;
  
      this.editForm.patchValue({
        branchId: this.BranchId,
      })
  }

  selectedAccount;
  onChangeCheckboxFullAccount(event: any, mode: string) {
    if (!event) {
      switch (mode) {
        case "account": {
          this.selectedAccount = undefined;
          break;
        }
        // case "detailAccount": {
        //   this.selectedDetailAccount = undefined;
        //   break;
        // }
        // case "costCenter": {
        //   this.selectedCostCenter = undefined;
        //   break;
        // }
        // case "project": {
        //   this.selectedProject = undefined;
        //   break;
        // }
        default:
      }
    }
  }

  dialogRef: DialogRef;
  dialogModel: any;
  openSelectForm(mode: string) {
    var viewId = 0;
    switch (mode) {
      case "account": {
        viewId = ViewName.Account;
        break;
      }
      case "detailAccount": {
        viewId = ViewName.DetailAccount;
        break;
      }
      case "costCenter": {
        viewId = ViewName.CostCenter;
        break;
      }
      case "project": {
        viewId = ViewName.Project;
        break;
      }
      default:
    }

    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent,
    });

    this.sharedDataService.selectFormTitle.subscribe((title: string) => {
      this.dialogRef.dialog.instance.title = title;
    });

    this.dialogModel = this.dialogRef.content.instance;

    this.dialogModel.viewID = viewId;
    this.dialogModel.isDisableEntities = true;

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {
      this.changeParam();      
      switch (mode) {
        case "account": {
          this.selectedAccount = res.dataItem;
          break;
        }
        // case "detailAccount": {
        //   this.selectedDetailAccount = res.dataItem;
        //   break;
        // }
        // case "costCenter": {
        //   this.selectedCostCenter = res.dataItem;
        //   break;
        // }
        // case "project": {
        //   this.selectedProject = res.dataItem;
        //   break;
        // }
        default:
      }
      console.log(res,this.selectedAccount,mode);

      this.dialogRef.close();
    });
  }

  changeParam(){}

  getCheckBooks() {

  }

  getCheckBookPages(id) {
  }

  cancelHandler() {
    this.errorMessages = undefined;
  }

  setEndNo() {
    let startNoOrigin = this.editForm.value.startNo;
    if (startNoOrigin) {
      let serial = <string>startNoOrigin.replace(/\d/g,'_');
      let startNo:any = <string>startNoOrigin.replace(/\D+/g, '_');
      let endNumber = 0;
      startNo = startNo.split('_');
      for (let i = 0; i < this.selectedPagesCount; i++) {
        endNumber = +startNo[startNo.length - 1] + i;
      }
      startNo[startNo.length - 1] = endNumber.toString();
  
      let endNo = startNo.join('');
      endNo.split('').forEach(d => {
        serial = serial.replace('_',d);
      })
      this.editForm.patchValue({
        endNo: serial
      })
    }
  }

  onSave(e){
    if (!this.editForm.valid)
     return;

    let value = this.editForm.value;
    this.checkBookService.insert(CheckBooksApi.CheckBooks,value).subscribe(
      async (res) => {
        console.log(res);

        this.editForm.reset(res);
        this.model = res as CheckBook;
        this.errorMessages = undefined;
        this.showMessage(this.updateMsg, MessageType.Succes);
        this.setEditMode = true;

        this.checkBookPages = await lastValueFrom(this.checkBookService.insertPages(this.model.id));
        console.log(this.checkBookPages);
        
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
    } else {
      this.otherSizeOfPages = false;
      this.setEndNo();
    }
    console.log(e,this.selectedPagesCount);
  }

  onChangePagesCountInput(e) {
    this.setEndNo();
  }

  onFullAccountInpusFocuse(e) {}

  onSelectFullAccount(e) {
    let fullAccount = this.editForm1.value.fullAccount;
    this.editForm.patchValue({
      accountId: fullAccount.account.id? fullAccount.account.id: null,
      detailAccountId: fullAccount.detailAccount.id? fullAccount.detailAccount.id: null,
      costCenterId: fullAccount.costCenter.id? fullAccount.costCenter.id: null,
      projectId: fullAccount.project.id? fullAccount.project.id: null
    })
  }

  removeHandler(){}

  checkOperation(a){}

  showReport(){}
}
