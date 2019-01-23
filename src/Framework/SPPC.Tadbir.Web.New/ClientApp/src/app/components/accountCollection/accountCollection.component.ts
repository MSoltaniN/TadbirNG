import { Component, OnInit, Input, Renderer2, Optional, Host, SkipSelf, ViewChild, ViewEncapsulation } from '@angular/core';
import { AccountCollectionService, SettingService, AccountCollectionAccountInfo } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent, RowClassArgs } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
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
import { Response } from '@angular/http';
import { AccountCollectionApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
//import { AccountGroupPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { AccountCollectionCategory, AccountCollection, ViewTreeLevelConfig, AccountIdentity, AccountCollectionAccount } from '../../model/index';
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
  public selectedRows: number[] = [];
  public totalRecords: number;
  public rowData: GridDataResult;
  public selectedRowData: Array<AccountIdentity> = [];
  dataloadingMessage: boolean = false;
  selectedDataloadingMessage: boolean = false;
  public selectableSettings: SelectableSettings;




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


  handleLevelChange(level: any) {
    this.reloadGrid();
  }

  /**
   * 
   * @param item
   */
  public handleSelection(item: TreeItem): void {
    this.selectedCollectionItem = undefined;
    this.selectedRowData = [];
    this.selectedRows = [];

    if (item.dataItem.id > 0 && !item.dataItem.accountCollections) {
      this.selectedCollectionItem = item.dataItem;

      this.selectableSettings = {
        checkboxOnly: true,
        mode: this.selectedCollectionItem.multiSelect ? 'multiple' : 'single'
      };

      this.reloadGrid();
    }
    else
      if (item.dataItem.id > 0) {
        this.expandedKeys = [];
        this.expandedKeys.push(item.dataItem.id);
      }
  }

  reloadGrid() {
    if (this.selectedCollectionItem && this.ddlLevelSelected > 0) {

      //شماره سطح در جدول حساب از صفر شروع میشود ولی در سطوح حساب از یک شروع میشود به همین دلیل یک واحد از شماره سطح انتخاب شده کم میکنیم

      if (this.viewAccess) {
        this.grid.loading = true;
        var filter = this.addFilterToFilterExpression(this.currentFilter,
          new Filter("Level", (this.ddlLevelSelected - 1).toString(), "== {0}", "System.Int32"), FilterExpressionOperator.And);

        if (this.totalRecords == this.skip && this.totalRecords != 0) {
          this.skip = this.skip - this.pageSize;
        }

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

          this.selectedRowData = this.selectedRowData.concat(resData.selectedAccounts);

          this.dataloadingMessage = !(resData.allAccounts.length == 0);
          this.selectedDataloadingMessage = !(resData.selectedAccounts.length == 0);
          this.totalRecords = totalCount;
          this.grid.loading = false;
        })
      }
      else {
        this.rowData = {
          data: [],
          total: 0
        }
        this.selectedRowData = [];
      }

    }
  }


  /**
   * وقتی یکی از سطرهای گرید اول انتخاب میشود
   * @param checkedState آرایه ای از آیدی های انتخاب شده
   */
  onSelectableKeysChange(checkedState: any) {
    for (let item of checkedState) {
      if (!this.selectedRowData.find(f => f.id == item)) {
        var row = this.rowData.data.find(f => f.id == item);
        this.selectedRowData.push(row);
      }
    }

    this.onSelectedKeysChange(checkedState);
  }

  /**
   * وقتی یکی از سطرهای گرید دوم از حالت انتخاب بیرون می آید
   * @param checkedState آرایه ای از آیدی های انتخاب شده
   */
  onSelectedKeysChange(checkedState: any) {
    if (checkedState.length == 0) {
      this.selectedRowData = [];
    }
    this.selectedRowData.forEach(item => {
      if (!checkedState.find(f => f == item.id)) {
        var index = this.selectedRowData.indexOf(item);
        if (index != -1) {
          this.selectedRowData.splice(index, 1);
        }
      }
    })

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

  public sortChange(sort: SortDescriptor[]): void {

    this.sort = sort.filter(f => f.dir != undefined);

    this.reloadGrid();
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }


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

    this.accountCollectionService.insert<Array<AccountCollectionAccount>>(AccountCollectionApi.AccountCollections, this.accCollectionArray).subscribe(res => {
      this.showMessage(this.updateMsg, MessageType.Succes);
      this.accCollectionArray = [];
    }, (error => {
      this.showMessage(error, MessageType.Warning);
    }));

  }

  //قابل انتخاب یا غیر قابل انتخاب بودن یک سطر را بررسی میکند
  public selectionToggleCallback = (context: RowClassArgs) => {
    var isDisabled = this.selectedCollectionItem.typeLevel == TypeLevel.AllAccounts ||
      (this.selectedCollectionItem.typeLevel == TypeLevel.LeafAccounts && context.dataItem.childCount == 0) ||
      (this.selectedCollectionItem.typeLevel == TypeLevel.NonLeafAccounts && context.dataItem.childCount > 0) ? false : true;;
    return { 'k-disabled': isDisabled };
  }

  //#endregion
}


