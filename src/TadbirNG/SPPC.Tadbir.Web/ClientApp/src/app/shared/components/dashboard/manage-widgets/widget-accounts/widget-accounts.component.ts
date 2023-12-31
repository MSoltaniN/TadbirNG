import { ChangeDetectorRef, Component, ElementRef, EventEmitter,
   Input, NgZone, OnInit, Output, Renderer2, TemplateRef, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DialogCloseResult, DialogService } from '@progress/kendo-angular-dialog';
import { GridComponent, GridDataResult, PageChangeEvent, RowArgs } from '@progress/kendo-angular-grid';
import { SettingService } from '@sppc/config/service';
import { AccountRelationsType } from '@sppc/finance/enum';
import { FullAccount } from '@sppc/finance/models';
import { FullAccountInfo } from '@sppc/finance/service';
import { AutoGeneratedGridComponent } from '@sppc/shared/class';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { ReportViewerComponent } from '@sppc/shared/components/reportViewer';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier';
import { BrowserStorageService, GridService, MetaDataService } from '@sppc/shared/services';
import { ToastrService } from 'ngx-toastr';
import { WidgetService } from '../../services/widget.service';

@Component({
  selector: 'widget-accounts',
  templateUrl: './widget-accounts.component.html',
  styleUrls: ['./widget-accounts.component.css']
})
export class WidgetAccountsComponent extends AutoGeneratedGridComponent implements OnInit {

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    private widgetService: WidgetService,
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
    );
  }

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild('itemListRef') accountDetails: TemplateRef<any>;

  public rowData: GridDataResult;

  @Input() widgetId: string|number;
  @Input() isNew: boolean;
  @Input() accountRequired;

  @Input('selectedAccounts') set selectedAccounts(accounts: FullAccount[]) {
    setTimeout(() => {
    if (accounts) {
      this.widgetAccounts = accounts;
      let total = accounts.length;
      this.rowData = {
        data: accounts,
        total: total
      }
    }
    }, 0);

  };

  accountItem:any;
  selectedItem:any;
  fullAccount:FullAccountInfo = new FullAccountInfo();
  widgetAccounts: FullAccount[] = [];
  editMode = false;
  @Output() setWidgetAccounts: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.accountItem = AccountRelationsType;
    this.pageSize = 5;
  }

  addNew() {}

  getSelectedRow(item: RowArgs) {
    return item.index;
  }

  editHandler(event) {
    this.editMode = true;
    this.fullAccount = this.widgetAccounts[this.selectedRows[0]];

    this.openDialog(this.accountDetails,this.accountItem.Account);
  }

  removeHandler() {
    this.selectedRows.forEach(i => {
      if (i != -1) {
        this.widgetAccounts.splice(i, 1);
        this.pageSelectedData();
      }
    });

    this.selectedRows = [];
    // if (this.widgetAccounts.length == 0)
    //   this.dataloadingMessage = false;
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
        this.fullAccount = new FullAccountInfo();
        this.selectedRows = [];
        this.editMode = false;
      }
    });
  }

  onSave(event) {
    if (this.editMode) {
      this.widgetAccounts[this.selectedRows[0]] = event
    } else {
      this.widgetAccounts.push(event);
    }
    this.setWidgetAccounts.emit(this.widgetAccounts);
    this.pageSelectedData();

    this.closeDialog();
  }

  closeDialog() {
    this.dialogRef.close();
  }

  private pageSelectedData(): void {
    this.rowData = {
      data: this.widgetAccounts.slice(this.skip,this.skip + this.pageSize),
      total: this.widgetAccounts.length
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
