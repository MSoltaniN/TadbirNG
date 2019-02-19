import { Component, OnInit, Input, Renderer2, Optional, Host, SkipSelf, ViewChild, ViewEncapsulation } from '@angular/core';
import { AccountCollectionService, SettingService, AccountCollectionAccountInfo } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent, RowClassArgs } from '@progress/kendo-angular-grid';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { Filter } from "../../class/filter";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { AccountCollectionApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { FilterExpression } from '../../class/filterExpression';
import { AccountCollectionCategory, AccountCollection, ViewTreeLevelConfig, AccountCollectionAccount, Account } from '../../model/index';
import { TreeItem } from '@progress/kendo-angular-treeview';
import { SelectableSettings } from '@progress/kendo-angular-grid';
import { AccountRelationsType } from '../../enum/accountRelationType';
import { AccountCollectionPermissions } from '../../security/permissions';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
import { TypeLevel } from '../../enum/TypeLevel';


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


  public selectedCollectionItem: AccountCollection;
  public expandedKeys: any[] = [];
  collectionCategory: Array<AccountCollectionCategory> = [];
  Accountlevels: Array<ViewTreeLevelConfig> = [];
  ddlLevelSelected: number = 0;
  accCollectionArray: Array<AccountCollectionAccount> = [];


  currentFilter: FilterExpression;
  //permission flag
  viewAccess: boolean;
  public selectableRows: number[] = [];//شناسه یکتای انتخاب شده گرید اول
  public selectedRows: number[] = [];//شناسه یکتای انتخاب شده گرید دوم
  public totalRecords: number;
  public rowData: GridDataResult;
  public selectedRowData: Array<Account> = [];
  newSelectedRowData: Array<Account> = [];
  dataloadingMessage: boolean = false;
  selectedDataloadingMessage: boolean = false;
  public selectableSettings: SelectableSettings;
  isPageChanged: boolean = false;
  isLevelChanged: boolean = false;
  isFilterChanged: boolean = false;




  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.AccountCollection, AccountCollectionPermissions.View);
    this.getAccountCollectionCategory();
    this.getAccountLevels();
  }


  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService,
    private accountCollectionService: AccountCollectionService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.AccountCollection, Metadatas.AccountCollection);
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
    debugger;
    this.selectedCollectionItem = undefined;
    this.selectedRowData = [];
    this.selectedRows = [];
    this.selectableRows = [];
    this.newSelectedRowData = [];
    this.rowData = undefined;
    this.dataloadingMessage = false;
    this.selectedDataloadingMessage = false;
    this.pageIndex = 0;

    if (item.dataItem.id > 0 && !item.dataItem.accountCollections) {
      this.selectedCollectionItem = item.dataItem;
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
    this.selectableRows = [];
    this.pageIndex = 0;
    this.rowData = undefined;
    this.dataloadingMessage = false;
    this.isLevelChanged = true;
  }

  reloadGrid() {
    if (this.selectedCollectionItem) {

      //شماره سطح در جدول حساب از صفر شروع میشود ولی در سطوح حساب از یک شروع میشود به همین دلیل یک واحد از شماره سطح انتخاب شده کم میکنیم

      if (this.viewAccess) {
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

          this.accountCollectionService.getAll(String.Format(AccountCollectionApi.AccountCollectionAccount, this.selectedCollectionItem.id), this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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
              data: resData.allAccounts,
              total: totalCount
            }

            debugger;

            if (!this.isPageChanged && !this.isLevelChanged && !this.isFilterChanged) {
              this.selectedRowData = resData.selectedAccounts.concat(this.newSelectedRowData);
              this.selectedDataloadingMessage = !(this.selectedRowData.length == 0);
            }
            this.isPageChanged = false;
            this.isLevelChanged = false;
            this.isFilterChanged = false;

            this.dataloadingMessage = !(resData.allAccounts.length == 0);
            this.totalRecords = totalCount;
            this.grid.loading = false;
          })
        }
      }
      else {
        this.rowData = {
          data: [],
          total: 0
        }
        this.selectedRowData = [];
      }

    }
    else {
      this.showMessage(this.getText('AccountCollection.SelectCollection'), MessageType.Warning);
    }
  }


  /**
   *اضافه کردن حساب انتخاب شده از گرید اول به گرید دوم
   * */
  addAccount() {
    if (this.isSelectableAccount()) {
      if (!this.selectedRowData.find(f => f.id == this.selectableRows[0])) {
        var row = this.rowData.data.find(f => f.id == this.selectableRows[0]);
        this.selectedRowData.push(row);
        this.newSelectedRowData.push(row);
      }
      else {
        this.showMessage(this.getText('AccountCollection.SelectedAccount'), MessageType.Info);
      }
    }
    this.selectableRows = [];
  }

  /**
   *حذف کردن حساب انتخاب شده در گرید دوم
   * */
  removeAccount() {
    var item = this.selectedRowData.find(f => f.id == this.selectedRows[0])
    var index = this.selectedRowData.indexOf(item);
    if (index != -1) {
      this.selectedRowData.splice(index, 1);
    }

    var newItem = this.newSelectedRowData.find(f => f.id == this.selectedRows[0])
    var newIndex = this.newSelectedRowData.indexOf(newItem);
    if (newIndex != -1) {
      this.newSelectedRowData.splice(newIndex, 1);
    }

    this.selectedRows = [];
    if (this.selectedRowData.length == 0)
      this.selectedDataloadingMessage = false;
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    var isReload: boolean = false;
    if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
      isReload = true;

    this.currentFilter = this.getFilters(filter);
    if (isReload) {
      this.isFilterChanged = true;
      this.reloadGrid();
    }
  }

  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort.filter(f => f.dir != undefined);

    this.reloadGrid();
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.isPageChanged = true;
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
      this.selectedRows = [];
      this.selectableRows = [];
      this.rowData = undefined;
      this.dataloadingMessage = false;
      this.selectedDataloadingMessage = false;
      this.newSelectedRowData = [];

      this.reloadGrid();
    }, (error => {
      this.showMessage(error, MessageType.Warning);
    }));

  }

  //قابل انتخاب یا غیر قابل انتخاب بودن یک سطر را بررسی میکند
  public isSelectableAccount(): boolean {
    if (!this.selectedCollectionItem.multiSelect && this.selectedRowData.length > 0) {
      this.showMessage(this.getText('AccountCollection.MultiSelectValidation'), MessageType.Warning);
      return false;
    }
    else {
      var row = this.rowData.data.find(f => f.id == this.selectableRows[0]);
      var isDisabled = this.selectedCollectionItem.typeLevel == TypeLevel.AllAccounts ||
        (this.selectedCollectionItem.typeLevel == TypeLevel.LeafAccounts && row.childCount == 0) ||
        (this.selectedCollectionItem.typeLevel == TypeLevel.NonLeafAccounts && row.childCount > 0) ? true : false;

      if (!isDisabled)
        this.showMessage(this.getText('AccountCollection.SelectValidation'), MessageType.Warning);
      return isDisabled;
    }
  }

  //#endregion
}


