import { ChangeDetectorRef, Component, ElementRef, NgZone, OnInit, Renderer2, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DialogCloseResult, DialogService } from '@progress/kendo-angular-dialog';
import { GridComponent } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { SettingService } from '@sppc/config/service';
import { AutoGeneratedGridComponent, String } from '@sppc/shared/class';
import { ReportViewerComponent, ViewIdentifierComponent } from '@sppc/shared/components';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { Entities, Layout, MessageType } from '@sppc/shared/enum/metadata';
import { OperationId } from '@sppc/shared/enum/operationId';
import { CheckBookPermissions, ViewName } from '@sppc/shared/security';
import { BrowserStorageService, GridService, MetaDataService } from '@sppc/shared/services';
import { CheckBook } from '@sppc/treasury/models/checkBook';
import { CheckBookReportApi } from '@sppc/treasury/service/api/checkBookReportApi';
import { CheckBooksApi } from '@sppc/treasury/service/api/checkBooksApi';
import { CheckBookInfo, CheckBookService } from '@sppc/treasury/service/check-book.service';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { CheckBookEditorComponent } from '../check-book/check-book-editor.component';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'check-report',
  templateUrl: './checkBook-report.component.html',
  styles: [''],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class CheckBookReportComponent
       extends AutoGeneratedGridComponent
       implements OnInit {

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
    public elem: ElementRef,
    private router: Router
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
      CheckBooksApi.CheckBooks,
      CheckBooksApi.CheckBook
    );
  }

  clickedRowItem: CheckBook;
  get isArchived() {
    let checks = this.rowData?.data.filter(
      (item) => this.selectedRows.includes(item.id)
    );
    let archived = checks.filter(
      (item) => item.isArchived == true
    );

    if (checks.length == archived.length) {
      return true;
    } else if(archived.length > 0) {
      return 'both'
    } else{
      return false;
    }
  }

  ngOnInit(): void {
    this.entityName = Entities.CheckBookReport;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = CheckBookReportApi.CheckBooksReport;
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  addNew() {
    this.editDataItem = new CheckBookInfo();
    this.openEditorDialog(true);
  }

  editHandler(arg: any) {
    var recordId = this.selectedRows[0]; //.id;
    this.grid.loading = true;
    this.checkBookService
      .getByIdAndFilters(
        String.Format(CheckBooksApi.CheckBook, recordId),
        this.reportFilter,
        this.reportQuickFilter
      )
      .subscribe((res) => {
        this.editDataItem = res;
        this.openEditorDialog(false);

        this.grid.loading = false;
      });
  }

  ArchiveHandler(toArchive:boolean) {
    this.grid.loading = true;
    let url = toArchive? CheckBooksApi.ArchiveCheckBooks: CheckBooksApi.UndoArchiveCheckBooks;
    let items:number[] = [];
    this.selectedRows.forEach(id => items.push(id));
    let body = JSON.stringify({ paraph: "", items: items });

    this.checkBookService.updateCheck(url,body).pipe(
      take(2)
    ).subscribe({
      next: (res) => {
        this.selectedRows = [];
        this.showMessage(
          this.getText("Messages.OperationSuccessful"),
          MessageType.Succes
        );
        this.reloadGrid();
      },
      error: (error) => {
        if (error)
          this.showMessage(
            this.errorHandlingService.handleError(error),
            MessageType.Warning
          );
      },
      complete: () => {
        this.grid.loading = false;
      }
    })
  }

  rowDoubleClickHandler() {
    if (!this.checkEditPermission()) return;

    if (this.clickedRowItem) {
      this.grid.loading = true;
      this.checkBookService
        .getByIdAndFilters(
          String.Format(CheckBooksApi.CheckBook, this.clickedRowItem.id),
          this.reportFilter,
          this.reportQuickFilter
        )
        .subscribe((res) => {
          this.editDataItem = res;

          this.openEditorDialog(false);

          this.grid.loading = false;
        });
    }
  }

  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  openEditorDialog(isNew: boolean) {
    if (!this.checkEditPermission()) return;

    this.dialogRef = this.dialogService.open({
      title: isNew?this.getText("CheckBook.New"):this.getText("CheckBook.CheckBook"),
      content: CheckBookEditorComponent
    });
    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.checkBookItem = this.editDataItem;
    this.dialogModel.isOpenFromList = true;
    this.dialogModel.dialogMode = true;
    this.dialogModel.isNew = isNew;
    this.editDataItem = undefined;

    if (this.reportFilter) {
      this.dialogModel.filter = JSON.parse(JSON.stringify(this.reportFilter));
    }

    if (this.reportQuickFilter) {
      this.dialogModel.quickFilter = JSON.parse(
        JSON.stringify(this.reportQuickFilter)
      );
    }

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.selectedRows = [];
        this.dialogModel.dialogMode = false;
        this.reloadGrid();
      }
    });

    this.dialogRef.content.instance.cancel.subscribe((result) => {
      this.dialogModel.dialogMode = false;
      this.dialogRef.close();
    });
  }

  checkEditPermission() {
    if (
      !this.isAccess(Entities.CheckBook, CheckBookPermissions.View)
    ) {
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
      return false;
    }

    return true;
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (this.groupOperation) {
      this.prepareDeleteConfirm(this.getText("Messages.SelectedItems"));
    } else {
      var recordId = this.selectedRows[0];
      var record = this.rowData.data.find((f) => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  onAdvanceFilterOk() {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }

}
