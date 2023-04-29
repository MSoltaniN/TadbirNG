import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  NgZone,
  OnChanges,
  OnInit,
  Output,
  Renderer2,
  SimpleChanges,
  TemplateRef,
  ViewChild,
} from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { GridComponent, PageChangeEvent, RowArgs } from "@progress/kendo-angular-grid";
import { RTL } from "@progress/kendo-angular-l10n";
import { SettingService } from "@sppc/config/service";
import { AutoGeneratedGridComponent, String } from "@sppc/shared/class";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { ReportViewerComponent } from "@sppc/shared/components/reportViewer";
import { ViewIdentifierComponent } from "@sppc/shared/components/viewIdentifier";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import { ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  GridService,
  MetaDataService,
} from "@sppc/shared/services";
import { PageOperations } from "@sppc/treasury/models/chechBookOperations";
import { CheckBooksApi } from "@sppc/treasury/service/api/checkBooksApi";
import { CheckBookService } from "@sppc/treasury/service/check-book.service";
import { ToastrService } from "ngx-toastr";
import { lastValueFrom } from "rxjs";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "check-book-pages",
  templateUrl: "./check-book-pages.component.html",
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class CheckBookPagesComponent
  extends AutoGeneratedGridComponent
  implements OnInit, OnChanges
{
  errorMessage: string;

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
      CheckBooksApi.CheckBookPages
    );
  }

  @Output() nullPages: EventEmitter<any> = new EventEmitter();

  @Input() checkBookId: string | number;
  @Input() isNew: boolean;

  @Input("pages") set pages(data: any) {
    setTimeout(() => {
      if (data) {
        this.checkBookPages = data;
        let total = data.length;
        this.rowData = {
          data: data,
          total: total,
        };
        this.paginateData();
      }
    }, 0);
  }

  checkBookPages = [];
  selectedItem: any;
  editMode = false;
  confirmMsg: string;
  confirmDialog: boolean = false;
  pageOperationsItem = PageOperations;
  selectedOperation;
  permissionEntityName = "CheckBook";
  
  public get hasPage(): boolean{
    let is = this.rowData?.data.length? true: false;
    return is;
  }

  public get usedPages(): boolean {
    let is = this.rowData?.data.find(
      (item) => item.id == this.selectedRows[0] && item.checkId != null
    );
    return is ? true : false;
  }

  public get checkStatus(): boolean {
    let check = this.rowData?.data.find(
      (item) => item.id == this.selectedRows[0]
    );
    return check?.status;
  }

  ngOnInit() {
    this.entityName = Entities.CheckBookPage;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = String.Format(
      CheckBooksApi.CheckBookPages,
      this.checkBookId
    );
    this.refetchGridColumns();
    if (this.checkBookId != 0 && !this.checkBookPages.length) {
      this.reloadGrid();
    }

    this.pageSize = 10;
    this.cdref.detectChanges();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.getDataUrl = String.Format(
      CheckBooksApi.CheckBookPages,
      this.checkBookId
    );
    this.showloadingMessage = false;
    if (changes.checkBookId && this.checkBookId && !changes.pages) {
      this.reloadGrid();
    }
  }

  public onDataBind(res: any): void {
    this.checkBookPages = res;

    if (!res.length && !this.grid.filter.filters.length) {
      this.nullPages.emit(true);
    } else {
      this.nullPages.emit(false);
    }
  }

  getSelectedRow(item: RowArgs) {
    return item.dataItem.id;
  }

  pageOperations(item: number) {
    switch (item) {
      case PageOperations.Cancel:
        this.confirmDialog = true;
        this.selectedOperation = item;
        this.prepareConfirmMsg(
          "Messages.CheckBookPagesCancelConfirm",
          this.selectedRows[0]
        );
        break;

      case PageOperations.UndoCancel:
        this.confirmDialog = true;
        this.selectedOperation = item;
        this.prepareConfirmMsg(
          "Messages.CheckBookPagesUndoCancelConfirm",
          this.selectedRows[0]
        );
        break;

      case PageOperations.Connect:
        this.confirmDialog = true;
        this.selectedOperation = item;
        this.prepareConfirmMsg(
          "Messages.CheckBookPagesConnectConfirm",
          this.selectedRows[0]
        );
        break;

      case PageOperations.Disconnect:
        this.confirmDialog = true;
        this.selectedOperation = item;
        this.prepareConfirmMsg(
          "Messages.CheckBookPagesDisconnectConfirm",
          this.selectedRows[0]
        );
        break;

      default:
        break;
    }
  }

  cancelCheck(status:boolean) {
    let url;
    if (status)
      url = String.Format(CheckBooksApi.CancelPage, this.selectedRows[0]);
    else
      url = String.Format(CheckBooksApi.UndoCancelPage, this.selectedRows[0]);

    this.grid.loading = true;
    this.checkBookService.updateCheck(url).subscribe({
      next: (res) => {
        this.grid.loading = false;
        this.showMessage(
          this.getText("Messages.OperationSuccessful"),
          MessageType.Succes
        );
        this.reloadGrid();
      },
      error: (error) => {
        this.grid.loading = false;
        if (error)
          this.showMessage(
            this.errorHandlingService.handleError(error),
            MessageType.Warning
          );
      },
      complete: () => {
        this.confirmDialog = false;
      }
    });
  }

  connectCheck(status:boolean) {}

  removeHandler(e) {
    this.deleteConfirm = true;
    this.prepareDeleteConfirm(
      this.getText("CheckBook.CheckBookPages")
    );
  }

  deleteModel(confirm: boolean): void {
    if (confirm) {
      this.groupOperation = false;
      this.checkBookService
        .delete(String.Format(CheckBooksApi.CheckBookPages, this.checkBookId))
        .subscribe({
          next: async (res) => {
            let deleteMsg = await lastValueFrom(this.translate.get("CheckBook.DeletedMsg"));
            let PagesMsg = await lastValueFrom(this.translate.get("CheckBook.CheckBookPages"));
            this.showMessage(String.Format(deleteMsg,PagesMsg), MessageType.Info);
            this.selectedRows = [];
            // this.groupOperation = false;
            this.deleteConfirm = false;
            this.nullPages.emit();
            this.reloadGrid();
          },
          error: (error) => {
            this.grid.loading = false;
            this.deleteConfirm = false;
            if (error)
              this.showMessage(
                this.errorHandlingService.handleError(error),
                MessageType.Warning
              );
          },
        });
    } else {
      this.deleteConfirm = false;
    }
  }

  onSave(event) {}

  showDialog(show: boolean) {
    if (show) {
      // this.confirmDialog = true;
      switch (this.selectedOperation) {
        case PageOperations.Cancel:
          this.cancelCheck(true);
          break;

        case PageOperations.UndoCancel:
          this.cancelCheck(false);
          break;

        case PageOperations.Connect:
          this.connectCheck(true);
          break;

        case PageOperations.Disconnect:
          this.connectCheck(false);
          break;

        default:
          break;
      }
    } else {
      this.confirmDialog = false;
      this.selectedOperation = undefined;
    }
  }

  // /**
  //  * to paginate pages manualy
  //  * @param event [PageChangeEvent]
  //  */
  // public manualPageChange(event: PageChangeEvent): void {
  //   this.skip = event.skip;
  //   this.pageSize = event.take;
  //   this.selectedRows = []
  //   this.paginateData();
  // }

  private paginateData(): void {
    this.rowData = {
      data: this.checkBookPages.slice(this.skip,this.skip + this.pageSize),
      total: this.checkBookPages.length
    }
  }
  /**
   * prepare confim message for delete operation
   * @param text is a part of message that use for delete confirm message
   */
  public prepareConfirmMsg(text1: string, text2?: string | number) {
    this.translate.get(text1).subscribe((msg: string) => {
      this.confirmMsg = String.Format(msg, text2);
    });
  }
}
