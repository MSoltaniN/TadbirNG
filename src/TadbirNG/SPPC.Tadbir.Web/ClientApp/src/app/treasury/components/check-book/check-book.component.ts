import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, NgZone, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from '@progress/kendo-angular-dialog';
import { GridComponent } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { SettingService } from '@sppc/config/service';
import { AutoGeneratedGridComponent, DetailComponent, String } from '@sppc/shared/class';
import { ReportViewerComponent, ViewIdentifierComponent } from '@sppc/shared/components';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { Entities, Layout } from '@sppc/shared/enum/metadata';
import { OperationId } from '@sppc/shared/enum/operationId';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, GridService, MetaDataService } from '@sppc/shared/services';
import { CheckBook } from '@sppc/treasury/models/checkBook';
import { CashRegisterApi } from '@sppc/treasury/service/api/cashRegistersApi';
import { CheckBooksApi } from '@sppc/treasury/service/api/checkBooksApi';
import { CashRegistersInfo, CashRegistersService } from '@sppc/treasury/service/cash-registers.service';
import { CheckBookInfo, CheckBookService } from '@sppc/treasury/service/check-book.service';
import { ToastrService } from 'ngx-toastr';
import { CashRegistersFormComponent } from '../cash-registers/cash-registers-form.component';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'check-book',
  templateUrl: './check-book.component.html',
  styles: [''],
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
  otherSizeOfPages = false;
  checkBookList: CheckBook[] = [];
  selectedCheckBook;
  pagesCount = [
    {key: 10, value: 10},
    {key: 25, value: 25},
    {key: 50, value: 50},
    {key: 100, value: 100},
    {key: "other", value: -1}
  ];
  selectedPagesCount: number;

  constructor(public toastrService: ToastrService,
     public translate: TranslateService,
     public bStorageService: BrowserStorageService,
     public renderer: Renderer2,
     public metadata: MetaDataService,
     public elem:ElementRef)
  {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.CheckBook, ViewName.CheckBook,elem);
  }

  ngOnInit(): void {
    setTimeout(() => {
      if (this.model.id == 0) {
        this.model.branchId = this.BranchId;
        this.editMode = true;
      } else {
        this.editMode = false;
      }
      this.editForm.reset(this.model);
    })
  }

  addNew() {
    if (!this.editForm.valid)
      return;
    
      this.editForm.patchValue({
        branchId: this.BranchId,
      })
    
  }

  getCheckBooks() {

  }

  getCheckBookPages(id) {
  }

  cancelHandler() {
    this.errorMessages = undefined;
  }

  setEndNo() {
    let serial = <string>this.editForm.value.startNo.replace(/\d/g,'_')
    let startNo:any = <string>this.editForm.value.startNo.replace(/\D+/g, '_');
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

  onSave(e){}

  // Events
  onChangePagesCountDropDown(e) {
    if (e == -1) {
      this.otherSizeOfPages = true;
    } else {
      this.otherSizeOfPages = false;
      this.setEndNo();
    }
  }

  onChangePagesCountInput(e) {
    console.log(e);
    this.setEndNo();
  }
}
