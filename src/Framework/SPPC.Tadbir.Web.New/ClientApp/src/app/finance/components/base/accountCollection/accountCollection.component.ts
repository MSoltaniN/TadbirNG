import { Component, OnInit, Renderer2, ViewChild, ViewEncapsulation } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, GridComponent } from '@progress/kendo-angular-grid';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { RTL } from '@progress/kendo-angular-l10n';
import { TreeItem } from '@progress/kendo-angular-treeview';
import { String, DefaultComponent, FilterExpression, Filter, FilterExpressionOperator } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { AccountCollection, AccountCollectionCategory, AccountCollectionAccount, Account, AccountCollectionService, AccountCollectionApi, AccountApi } from '@sppc/finance';
import { SettingService } from '@sppc/config/service';
import { SecureEntity, AccountCollectionPermissions, BrowserStorageService, MetaDataService, ViewName } from '@sppc/shared';
import { AccountRelationsType, TypeLevel } from '@sppc/finance/enum';
import { ViewTreeLevelConfig } from '@sppc/config/models';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'accountCollection',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './accountCollection.component.html',
  styleUrls: ['./accountCollection.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class AccountCollectionComponent extends DefaultComponent implements OnInit {

  //#region Fields
  @ViewChild(GridComponent) grid: GridComponent;

  /**
   *مجموعه حساب انتخاب شده از درخت
   */
  public selectedCollectionItem: AccountCollection;
  public expandedKeys: any[] = [];
  collectionCategory: Array<AccountCollectionCategory> = [];
  Accountlevels: Array<ViewTreeLevelConfig> = [];
  ddlLevelSelected: number = 0;
  accCollectionArray: Array<AccountCollectionAccount> = [];


  currentFilter: FilterExpression;
  collectionViewAccess: boolean;
  /**
  *شناسه سطرهای انتخاب شده از گرید حسابهای قابل انتخاب
  */
  public selectableRowsKey: number[] = [];
  /**
  *شناسه سطرهای انتخاب شده از گرید حساب های انتخاب شده
  */
  public selectedRowsKey: number[] = [];
  public totalRecords: number;
  /**
  *اطلاعات گرید حساب های قابل انتخاب
  */
  public rowData: GridDataResult;
  /**
  *سطرهای گرید حساب های انتخاب شده
  */
  public selectedRowData: Array<Account> = [];

  dataloadingMessage: boolean = false;
  selectedDataloadingMessage: boolean = false;
  isChangedList: boolean = false;


  //#endregion

  //#region Events
  ngOnInit() {
    this.collectionViewAccess = this.isAccess(SecureEntity.AccountCollection, AccountCollectionPermissions.View)
    this.getAccountCollectionCategory();
    this.getAccountLevels();
  }


  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    private accountCollectionService: AccountCollectionService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate,bStorageService, renderer, metadata, settingService, Entities.AccountCollection, ViewName.AccountCollection);
  }
  //#endregion

  //#region Methods

  /**
   * لیست مجموعه حسابها را برای ایجاد درختواره میگیرد
   */
  getAccountCollectionCategory() {
    this.accountCollectionService.getAll(AccountCollectionApi.AccountCollections).subscribe(res => {
      this.collectionCategory = res.body;
    })
  }

  /**
   * لیست سطوح تعریف شده برای حساب را میگیرد
   */
  getAccountLevels() {

    this.settingService.getViewTreeSettings(AccountRelationsType.Account).subscribe(res => {
      this.Accountlevels = res.current.levels.filter(f => f.isEnabled == true);
    })

  }


  /**
   * وقتی مجموعه حساب انتخاب میشود اجرا میشود
   * @param item
   */
  public handleSelection(item: TreeItem): void {

    this.selectedCollectionItem = undefined;
    this.selectedRowData = [];
    this.selectedRowsKey = [];
    this.selectableRowsKey = [];
    this.rowData = undefined;
    this.dataloadingMessage = false;
    this.selectedDataloadingMessage = false;
    this.pageIndex = 0;
    this.isChangedList = false;

    if (item.dataItem.id > 0 && !item.dataItem.accountCollections) {
      this.selectedCollectionItem = item.dataItem;
      this.getSelectedAccount();
    }
    else
      if (item.dataItem.id > 0) {

        this.showMessage(this.getText('AccountCollection.SelectCollection'), MessageType.Warning);

        this.expandedKeys = [];
        this.expandedKeys.push(item.dataItem.id);
      }
  }

  /**
   *وقتی سطح حساب انتخاب میشود
   * */
  public handleLevelChange() {
    this.selectableRowsKey = [];
    this.pageIndex = 0;
    this.rowData = undefined;
    this.dataloadingMessage = false;
  }


  reloadGrid() {
    if (this.selectedCollectionItem) {

      //شماره سطح در جدول حساب از صفر شروع میشود ولی در سطوح حساب از یک شروع میشود به همین دلیل یک واحد از شماره سطح انتخاب شده کم میکنیم

        var filter = this.currentFilter;
        if (this.ddlLevelSelected == undefined || this.ddlLevelSelected == null) {
          this.showMessage(this.getText('AccountCollection.SelectLevel'), MessageType.Warning);
        }
        else {
          if (this.ddlLevelSelected > 0)
            filter = this.addFilterToFilterExpression(this.currentFilter,
              new Filter("Level", (this.ddlLevelSelected - 1).toString(), "== {0}", "System.Int32"), FilterExpressionOperator.And);

          if (this.totalRecords == this.skip && this.totalRecords != 0) {
            this.skip = this.skip - this.pageSize;
          }
          this.grid.loading = true;

          this.accountCollectionService.getAll(AccountApi.EnvironmentAccounts, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
            var resData = res.body;

            var totalCount = 0;

            if (res.headers != null) {
              var headers = res.headers != undefined ? res.headers : null;
              if (headers != null) {
                var retheader = headers.get('X-Total-Count');
                if (retheader != null)
                  totalCount = parseInt(retheader.toString());
              }
            }
            this.rowData = {
              data: resData,
              total: totalCount
            }

            this.dataloadingMessage = !(resData.length == 0);
            this.totalRecords = totalCount;
            this.grid.loading = false;
          })
        }
    }
    else {
      this.showMessage(this.getText('AccountCollection.SelectCollection'), MessageType.Warning);
    }
  }

  /**
   *حساب های انتخاب شده برای یک مجموعه حساب را از سرویس میگیرد
   * */
  getSelectedAccount() {
    if (this.collectionViewAccess) {
      this.accountCollectionService.getAll(String.Format(AccountCollectionApi.AccountCollectionAccount, this.selectedCollectionItem.id)).subscribe((res) => {
        var resData = res.body;
        this.selectedRowData = resData;
        this.selectedDataloadingMessage = !(resData.length == 0);
      })
    }
    else {
      this.selectedRowData = [];
      this.showMessage(this.getText('AccountCollection.AccountViewAccess'), MessageType.Warning);
    }
  }

  /**
   *اضافه کردن حساب 
   * */
  addAccount() {
    if (this.isSelectableAccount()) {
      if (!this.selectedRowData.find(f => f.id == this.selectableRowsKey[0])) {
        var row = this.rowData.data.find(f => f.id == this.selectableRowsKey[0]);
        this.selectedRowData.push(row);
        this.isChangedList = true;
      }
      else {
        this.showMessage(this.getText('AccountCollection.SelectedAccount'), MessageType.Info);
      }
    }
    this.selectableRowsKey = [];
  }

  /**
   *حذف کردن حساب 
   * */
  removeAccount() {
    var item = this.selectedRowData.find(f => f.id == this.selectedRowsKey[0])
    var index = this.selectedRowData.indexOf(item);
    if (index != -1) {
      this.selectedRowData.splice(index, 1);
      this.isChangedList = true;
    }

    this.selectedRowsKey = [];
    if (this.selectedRowData.length == 0)
      this.selectedDataloadingMessage = false;
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    var isReload: boolean = false;
    if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
      isReload = true;

    this.currentFilter = this.getFilters(filter);
    if (isReload) {
      this.reloadGrid();
    }
  }

  sortChange(sort: SortDescriptor[]): void {
    this.sort = sort.filter(f => f.dir != undefined);
    this.reloadGrid();
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.selectableRowsKey = [];
    this.reloadGrid();
  }

  /**
   *ارسال حسابهای انتخاب شده به سرویس
   * */
  saveChanges() {
    var collectionId = this.selectedCollectionItem.id;
    this.accCollectionArray = [];

    this.selectedRowData.forEach(item => {
      this.accCollectionArray.push(
        {
          id: 0,
          accountId: item.id,
          branchId: this.BranchId,
          collectionId: collectionId,
          fiscalPeriodId: this.FiscalPeriodId
        })
    })

    this.accountCollectionService.insert<Array<AccountCollectionAccount>>(String.Format(AccountCollectionApi.AccountCollectionAccount, this.selectedCollectionItem.id), this.accCollectionArray).subscribe(res => {
      this.showMessage(this.updateMsg, MessageType.Succes);

      this.accCollectionArray = [];
      this.selectedRowData = [];
      this.selectedRowsKey = [];
      this.selectableRowsKey = [];
      this.selectedDataloadingMessage = false;
      this.isChangedList = false;

      this.getSelectedAccount();
    }, (error => {
      this.showMessage(error, MessageType.Warning);
    }));

  }

  //قابل انتخاب یا غیر قابل انتخاب بودن یک حساب را بررسی میکند
  isSelectableAccount(): boolean {
    if (!this.selectedCollectionItem.multiSelect && this.selectedRowData.length > 0) {
      this.showMessage(this.getText('AccountCollection.MultiSelectValidation'), MessageType.Warning);
      return false;
    }

    var row = this.rowData.data.find(f => f.id == this.selectableRowsKey[0]);

    if (this.selectedCollectionItem.typeLevel == TypeLevel.LeafAccounts && row.childCount > 0) {
      this.showMessage(this.getText('AccountCollection.LeafAccountsValidation'), MessageType.Warning);
      return false;
    }

    if (this.selectedCollectionItem.typeLevel == TypeLevel.NonLeafAccounts && row.childCount == 0) {
      this.showMessage(this.getText('AccountCollection.NonLeafAccountsValidation'), MessageType.Warning);
      return false;
    }

    return true;
  }

  //#endregion
}


