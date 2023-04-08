import { ChangeDetectorRef, Component, ElementRef, EventEmitter,
  Input, NgZone, OnChanges, OnInit, Output, Renderer2, SimpleChanges, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { DialogCloseResult, DialogService } from '@progress/kendo-angular-dialog';
import { GridComponent, GridDataResult, PageChangeEvent, RowArgs } from '@progress/kendo-angular-grid';
import { SettingService } from '@sppc/config/service';
import { AutoGeneratedGridComponent, String } from '@sppc/shared/class';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { ReportViewerComponent } from '@sppc/shared/components/reportViewer';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier';
import { Entities } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, GridService, MetaDataService } from '@sppc/shared/services';
import { CheckBooksApi } from '@sppc/treasury/service/api/checkBooksApi';
import { CheckBookService } from '@sppc/treasury/service/check-book.service';
import { ToastrService } from 'ngx-toastr';

@Component({
 selector: 'check-book-pages',
 templateUrl: './check-book-pages.component.html',
 styles: ['']
})
export class CheckBookPagesComponent extends AutoGeneratedGridComponent implements OnInit,OnChanges {

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    private checkBookService: CheckBookService,
    public dialogService: DialogService,
    public settingService: SettingService,
    public ngZone: NgZone,
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
      elem,
      CheckBooksApi.CheckBook,
      CheckBooksApi.CheckBookPages,
    );
  }

  public rowData: GridDataResult;

  @Input() checkBookId: string|number;
  @Input() isNew: boolean;

  @Input('pages') set pages(data: any) {
    setTimeout(() => {
    if (data) {
      this.checkBookPages = data;
      let total = data.length;
      this.rowData = {
        data: data,
        total: total
      }
    }
    }, 0);

  };

  checkBookPages;
  selectedItem:any;
  editMode = false;
  @Output() setWidgetAccounts: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.entityName = Entities.CheckBook;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = String.Format(CheckBooksApi.CheckBookPages,this.checkBookId);
    this.refetchGridColumns();
    if (this.checkBookId != 0){
      console.log(this.checkBookId);
      this.reloadGrid();
    }

    this.pageSize = 5;
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes,this.checkBookId);
    if (changes.checkBookId && this.checkBookId){
      this.getDataUrl = String.Format(CheckBooksApi.CheckBookPages,this.checkBookId);
      this.reloadGrid();
    }
  }

  addNew() {}

  getSelectedRow(item: RowArgs) {
    return item.index;
  }

  editHandler(event) {
    this.editMode = true;;

    // this.openDialog(this.accountDetails,this.selectedRows[0]);
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

  removeHandler() {
    this.selectedRows.forEach(i => {
      if (i != -1) {
        this.checkBookPages.splice(i, 1);
        this.pageSelectedData();
      }
    });

    this.selectedRows = [];
  }

  openDialog(template: TemplateRef<any>, item: number) {
    this.selectedItem = item;

    this.dialogRef = this.dialogService.open({
      title: this.getText("FullAccount.Title"),
      content: template,
    });

    this.dialogRef.dialog.location.nativeElement.classList.add(
      "fullAccountForm"
    );

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.closeDialog();
        // this.fullAccount = new FullAccountInfo();
        // this.selectedRows = [];
        // this.editMode = false;
      }
    });
  }

  onSave(event) {
    if (this.editMode) {
      // this.widgetAccounts[this.selectedRows[0]] = event
    } else {
      // this.widgetAccounts.push(event);
    }
    // this.setWidgetAccounts.emit(this.widgetAccounts);
    this.pageSelectedData();

    this.closeDialog();
  }

  closeDialog() {
    this.dialogRef.close();
  }

  private pageSelectedData(): void {
    this.rowData = {
      data: this.checkBookPages.slice(this.skip,this.skip + this.pageSize),
      total: this.checkBookPages.length
    }
  }

 /**
 * برای صفحه بندی و تغییر صفحات گرید موارد انتخاب شده
 */

  public onPageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    // this.pageSize = event.take;
    this.selectedRows = []
    this.pageSelectedData();
  }


}
