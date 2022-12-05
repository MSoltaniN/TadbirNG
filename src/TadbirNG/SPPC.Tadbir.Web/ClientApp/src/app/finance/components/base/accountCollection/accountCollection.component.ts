import { Component, OnInit, Renderer2, ViewChild, ViewEncapsulation, ChangeDetectorRef, NgZone, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, GridComponent } from '@progress/kendo-angular-grid';
// import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { RTL } from '@progress/kendo-angular-l10n';
import { TreeItem } from '@progress/kendo-angular-treeview';
import { String, DefaultComponent, FilterExpression, Filter, FilterExpressionOperator, AutoGeneratedGridComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { AccountCollection, AccountCollectionCategory, AccountCollectionAccount, Account } from '@sppc/finance/models';
import { AccountCollectionApi, AccountApi } from '@sppc/finance/service/api';
import { AccountCollectionService, AccountCollectionAccountInfo, AccountInfo } from '@sppc/finance/service';
import { SettingService } from '@sppc/config/service';
import { BrowserStorageService, MetaDataService, GridService } from '@sppc/shared/services';
import { AccountRelationsType, TypeLevel } from '@sppc/finance/enum';
import { ViewTreeLevelConfig } from '@sppc/config/models';
import { SecureEntity, AccountCollectionPermissions, ViewName } from '@sppc/shared/security';
import { DialogService } from '@progress/kendo-angular-dialog';
import { AccountFullData } from '@sppc/finance/models/accountFullData';
import { AccountCollectionItemType } from '@sppc/finance/enum/shared';



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


export class AccountCollectionComponent extends AutoGeneratedGridComponent implements OnInit {

  //#region Fields
  @ViewChild(GridComponent, {static: true}) grid: GridComponent;

  /**
   *مجموعه حساب انتخاب شده از درخت
   */
  public selectedCollectionItem: AccountCollection;
  public expandedKeys: any[] = [];
  collectionCategory: Array<AccountCollectionCategory> = [];
  Accountlevels: Array<ViewTreeLevelConfig> = [];
  ddlLevelSelected: number = 0;
  accCollectionArray: Array<AccountCollectionAccount> = [];

  accCollectionListChanged: boolean = false;
  accListChanged: boolean = false;
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
  public selectedRowData: GridDataResult;
  public selectedData:Array<AccountCollectionAccount> = [];


  dataloadingMessage: boolean = false;
  selectedDataloadingMessage: boolean = false;
  isChangedList: boolean = false;
  multiSelect = {
    bol: false,
    str: 'single'
  };
  hasCustomerTax = false;
  deleteConfirm = false;

  //#endregion

  //#region Events
  ngOnInit() {
    this.collectionViewAccess = this.isAccess(SecureEntity.AccountCollection, AccountCollectionPermissions.View)
    this.getAccountCollectionCategory();
    this.getAccountLevels();
    this.accCollectionListChanged = true;
    this.accListChanged = false;

    this.listChanged = false;
    this.entityName = Entities.AccountCollection;
    this.viewId = ViewName[this.entityTypeName];
    this.getDataUrl = AccountApi.EnvironmentAccounts;

    this.cdref.detectChanges();
  }


  //#endregion

  constructor(public toastrService: ToastrService,
    public translate: TranslateService,
    public dialogService: DialogService,
    public gridService: GridService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    private accountCollectionService: AccountCollectionService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    public ngZone: NgZone,
    public elem:ElementRef
    ) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone,elem);
  }

  //#endregion

  //#region Methods

  /**
   * لیست مجموعه حسابها را برای ایجاد درختواره میگیرد
   */
  getAccountCollectionCategory() {
    this.accountCollectionService.getAll(AccountCollectionApi.AccountCollections).subscribe(res => {
      this.collectionCategory = res.body;
    }, (err) => {
      this.showloadingMessage = false;
      this.grid.loading = false;
      this.showMessage(
        this.errorHandlingService.handleError(err),
        MessageType.Warning
      );
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
    this.selectedRowData = undefined;
    this.selectedRowsKey = [];
    this.selectableRowsKey = [];
    this.rowData = undefined;
    this.dataloadingMessage = false;
    this.selectedDataloadingMessage = false;
    this.pageIndex = 0;
    this.isChangedList = false;

    if (item.dataItem.id > 0 && !item.dataItem.accountCollections) {
      this.selectedCollectionItem = item.dataItem;
      this.selectedCollectionItem.multiSelect?
        this.multiSelect = {
          bol: true,
          str: 'multiple'
        }:
          this.multiSelect = {
            bol: false,
            str: 'single'
          };
      if (this.selectedCollectionItem.id == AccountCollectionItemType.Bank ||
          this.selectedCollectionItem.id == AccountCollectionItemType.TradeDebtors ||
          this.selectedCollectionItem.id == AccountCollectionItemType.TradeCreditors) {
        this.hasCustomerTax = true;
      } else {
        this.hasCustomerTax = false;
      }
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
    this.reloadGrid();
  }

  
  public onBeforeDataBind() {    
    if (this.selectedCollectionItem) {      
      this.defaultFilter = [];
      if (this.ddlLevelSelected == undefined || this.ddlLevelSelected == null) {
        this.showMessage(this.getText('AccountCollection.SelectLevel'), MessageType.Warning);
        this.cancelLoad = true;
      }
      else {
        if (this.ddlLevelSelected > 0)        
        {          
          this.defaultFilter.push(new Filter("Level", (this.ddlLevelSelected - 1).toString(), "== {0}", "System.Int32"));
        }
      }
    }
    else {
      this.cancelLoad = true;
      this.showMessage(this.getText('AccountCollection.SelectCollection'), MessageType.Warning);
    }
  }

  /**
   *حساب های انتخاب شده برای یک مجموعه حساب را از سرویس میگیرد
   * */
  getSelectedAccount() {    
    if (this.collectionViewAccess) {
      this.accountCollectionService.getAll(String.Format(AccountCollectionApi.AccountCollectionAccounts, this.selectedCollectionItem.id), undefined, undefined,
        undefined, undefined, undefined, this.accCollectionListChanged).subscribe((res) => {
        var resData = res.body;
        this.selectedData = resData;
        this.pageSelectedData();
        this.selectedDataloadingMessage = !(resData.length == 0);        
      })
    }
    else {
      this.selectedData = [];
      this.showMessage(this.getText('AccountCollection.AccountViewAccess'), MessageType.Warning);
    }
  }

  /**
   *اضافه کردن حساب 
   * */
  addAccount() {
    if (this.isSelectableAccount()) {
      let existItem = this.selectedData.find(f => this.selectableRowsKey.includes(f.accountId));
      if (!existItem) {
        this.selectableRowsKey.forEach(rowKey => {
          var row: AccountInfo = this.rowData.data.find(f => f.id == rowKey);
          var accCollection = new AccountCollectionAccountInfo();
          accCollection.accountFullCode = row.fullCode;
          accCollection.accountId = row.id;
          accCollection.accountName = row.name;
          accCollection.branchId = this.BranchId;
          accCollection.fiscalPeriodId = this.FiscalPeriodId;
          accCollection.collectionId = this.selectedCollectionItem.id;

          this.selectedData.push(accCollection);
          this.pageSelectedData();
          this.highLightNewRow(row,true,'.selected-items-grid');
          this.isChangedList = true;
        })
      }
      else {
        this.showMessage(`${this.getText('AccountCollection.SelectedAccount')} '${existItem.accountName}'`, MessageType.Info);
      }
    }
    this.selectableRowsKey = [];
  }

  /**
   *حذف کردن حساب 
   * */
  removeAccount(confirmDeleteTaxInfo = false) {
    let deleted = false;
    var items = this.selectedData.filter(f => this.selectedRowsKey.includes(f.accountId));

    items.forEach(item => {
      if (item.branchId != this.BranchId) {
        this.showMessage(this.getText('AccountCollection.AccountRemoveAccess'), MessageType.Info)
        return;
      }
      if (this.hasCustomerTax && !confirmDeleteTaxInfo) {
        this.gridService
        .getById(String.Format(AccountApi.AccountFullData, item.accountId))
        .subscribe((res:AccountFullData) => {

          if (res.customerTaxInfo.id > 0) {
            this.deleteConfirm = true;
          } else {
            confirmDeleteTaxInfo = true;
            this.removeAccount(true);
          }
        });
      }
      if (!this.hasCustomerTax || confirmDeleteTaxInfo) {
        this.deleteConfirm = false;
        var index = this.selectedData.indexOf(item);
        if (index != -1) {
          this.selectedData.splice(index, 1);
          this.pageSelectedData();
          this.isChangedList = true;
          deleted = true;
        }
      }
    });

    if (deleted) {
      this.selectedRowsKey = [];
      if (this.selectedData.length == 0)
        this.selectedDataloadingMessage = false;
    }
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
 * برای صفحه بندی و تغییر صفحات گرید موارد انتخاب شده
 */
  collectionGridPageSize:number = 10;
  collectionGridSkip:number = 0;
  public collectionGridpageChange(event: PageChangeEvent): void {
    this.collectionGridSkip = event.skip;
    // this.collectionGridPageSize = event.take;
    this.selectedRowsKey = []
    this.pageSelectedData();
  }

  private pageSelectedData(): void {
    this.selectedRowData = {
      data: this.selectedData.slice(this.collectionGridSkip,this.collectionGridSkip + this.collectionGridPageSize),
      total: this.selectedData.length
    }
  }

  /**
   *ارسال حسابهای انتخاب شده به سرویس
   * */
  saveChanges() {
    var collectionId = this.selectedCollectionItem.id;
    this.accCollectionArray = [];

    this.selectedData.forEach(item => {
      var accountCollection = new AccountCollectionAccountInfo();
      
      accountCollection.accountId = item.accountId;
      accountCollection.branchId = item.branchId;
      accountCollection.collectionId = collectionId;
      accountCollection.fiscalPeriodId = this.FiscalPeriodId;
      accountCollection.accountFullCode = item.accountFullCode;
      accountCollection.accountName = item.accountName;

      this.accCollectionArray.push(accountCollection);
    })

    this.accountCollectionService.insert<Array<AccountCollectionAccount>>(String.Format(AccountCollectionApi.AccountCollectionAccounts, this.selectedCollectionItem.id), this.accCollectionArray).subscribe(res => {
      this.showMessage(this.updateMsg, MessageType.Succes);

      this.accCollectionArray = [];
      this.selectedData = [];
      this.selectedRowsKey = [];
      this.selectableRowsKey = [];
      this.selectedDataloadingMessage = false;
      this.isChangedList = false;

      this.getSelectedAccount();
    }, (error => {
        if(error)
          this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);        
    }));

  }

  //قابل انتخاب یا غیر قابل انتخاب بودن یک حساب را بررسی میکند
  isSelectableAccount(): boolean {
    if (!this.selectedCollectionItem.multiSelect && this.selectedData.length > 0) {
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


