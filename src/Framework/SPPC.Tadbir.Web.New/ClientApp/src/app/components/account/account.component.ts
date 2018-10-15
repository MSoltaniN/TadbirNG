//#region Imports
import { Component, OnInit, Input, Renderer2, ViewChildren, QueryList, ElementRef, Host, Output, SkipSelf, Optional, ViewChild } from '@angular/core';
import { AccountService, AccountInfo, VoucherLineService, FiscalPeriodService, SettingService } from '../../service/index';
import { Account } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { Filter } from "../../class/filter";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { AccountApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { AccountPermissions } from '../../security/permissions';
import { DefaultComponent } from '../../class/default.component';
import { FilterExpression } from '../../class/filterExpression';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
//#endregion

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'account',
  templateUrl: './account.component.html',
  styles: [`
    .accInfoTitle {
        padding-right: 0px;
        padding-left: 0px;
    }
  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class AccountComponent extends DefaultComponent implements OnInit {


  //#region Fields

  public Childrens: Array<AccountComponent>;

  @ViewChild(GridComponent) grid: GridComponent;

  @Input() public parent: Account;
  @Input() public isChild: boolean = false;

  public parentId?: number = undefined;
  public rowData: GridDataResult;
  public selectedRows: string[] = [];
  public accountArticleRows: any[];
  public totalRecords: number;

  //permission flag
  viewAccess: boolean;

  //for add in delete messageText
  deleteConfirm: boolean;
  deleteModelsConfirm: boolean;
  deleteModelId: number;

  currentFilter: FilterExpression;
  currentOrder: string = "";
  public sort: SortDescriptor[] = [];

  showloadingMessage: boolean = true;

  editDataItem?: Account = undefined;
  parentModel: Account;
  isNew: boolean;
  disableSaveBtn: boolean | undefined;
  errorMessage: string;
  groupDelete: boolean = false;
  addToContainer: boolean = false;

  isChildExpanding: boolean;
  componentParentId: number;
  goLastPage: boolean;
  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.Account, AccountPermissions.View);
    if (this.parentAccount && this.parentAccount.isChildExpanding) {
      this.goLastPage = true;
      this.parentAccount.isChildExpanding = false;
    }

    this.reloadGrid();
    if (this.parentAccount) {
      this.parentAccount.addChildAccount(this);
      this.parentId = this.parent.id;
      this.componentParentId = this.parentId;

      this.parentModel = this.parent;
    }
  }

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id + " " + context.index;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    if (this.selectedRows.length > 1)
      this.groupDelete = true;
    else
      this.groupDelete = false;
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    var isReload: boolean = false;
    if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
      isReload = true;

    this.currentFilter = this.getFilters(filter);
    if (isReload) {
      this.reloadGrid();
    }
    //console.log(filter);

  }

  //dataStateChange(state: DataStateChangeEvent): void {
  //    //this.currentFilter = this.getFilters(state.filter);
  //    if (state.sort)
  //        if (state.sort.length > 0)
  //            this.currentOrder = state.sort[0].field + " " + state.sort[0].dir;
  //    this.state = state;
  //    this.skip = state.skip;
  //    this.reloadGrid();
  //}


  public sortChange(sort: SortDescriptor[]): void {
    if (sort)
      this.currentOrder = sort[0].field + " " + sort[0].dir;
    this.reloadGrid();
  }

  removeHandler(arg: any) {
    this.prepareDeleteConfirm(arg.dataItem.name);
    this.deleteModelId = arg.dataItem.id;
    this.deleteConfirm = true;
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  //account form events
  public editHandler(arg: any) {
    this.grid.loading = true;
    this.accountService.getById(String.Format(AccountApi.Account, arg.dataItem.id)).subscribe(res => {
      this.editDataItem = res;
      this.setParentModel(res.parentId);

      this.parentId = res.parentId;

      this.grid.loading = false;
    })
    this.isNew = false;
    this.errorMessage = '';
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.errorMessage = '';
    this.isNew = false;

    this.parentId = this.componentParentId;    
  }

  public saveHandler(model: Account) {
    this.grid.loading = true;
    if (!this.isNew) {
      this.isNew = false;
      this.disableSaveBtn = undefined;
      this.accountService.edit<Account>(String.Format(AccountApi.Account, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
        }, (error => {
          this.editDataItem = model;
          this.errorMessage = error;
          this.disableSaveBtn = false;
        }));
    }
    else {
      model.branchId = this.BranchId;
      model.fiscalPeriodId = this.FiscalPeriodId;
      model.companyId = this.CompanyId;


      //if (this.parentId) {
      //  this.parentId = undefined;
      //}

      this.parentId = this.componentParentId;

      //set parentid for childs accounts
      //if (this.parentId) {
        //model.parentId = this.parentId;

        ////var currentLevel = this.parent ? this.parent.level : 0;
        //var parentAc = this.parentAccount;
        //var currentLevel = 0;

        //while (parentAc) {
        //  currentLevel++;
        //  parentAc = parentAc.parentAccount
        //}

        //model.level = currentLevel + 1;

      //  this.parentId = undefined;
      //}
      //else if (this.parent) {
      //  model.parentId = this.parent.id;
      //  model.level = this.parent.level + 1;
      //}






      if (this.parentModel) {
        model.parentId = this.parentModel.id;
        model.level = this.parentModel.level + 1;
      }
      else {
        model.parentId = undefined;
        model.level = 0;
      }

      //set parentid for childs accounts
      this.disableSaveBtn = undefined;
      this.accountService.insert<Account>(AccountApi.EnvironmentAccounts, model)
        .subscribe((response: any) => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;

          if (this.Childrens) {
            var childFiltered = this.Childrens.filter(f => f.parent.id == model.parentId);
            if (childFiltered.length > 0) {
              childFiltered[0].reloadGrid(insertedModel);
              return;
            }
          }
          this.reloadGrid(insertedModel);

        }, (error => {
          this.isNew = true;
          this.disableSaveBtn = false;
          this.errorMessage = error;
        }));

    }
    this.grid.loading = false;

  }

  //#endregion

  //#region Constructor

  constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
    private accountService: AccountService, private voucherLineService: VoucherLineService,
    private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService,
    @SkipSelf() @Host() @Optional() private parentAccount: AccountComponent) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Account, Metadatas.Account);
  }

  //#endregion

  //#region Methods

  /**
   * کامپوننت های فرزند را در متغیری اضافه میکند
   * @param accountComponent کامپوننت حساب
   */
  public addChildAccount(accountComponent: AccountComponent) {

    if (this.Childrens == undefined) this.Childrens = new Array<AccountComponent>();
    if (this.Childrens.findIndex(p => p.parent.id === accountComponent.parent.id) == -1)
      //if (this.Childrens.some(p => p === accountComponent) == false)
      this.Childrens.push(accountComponent);


  }

  showConfirm() {
    this.deleteModelsConfirm = true;
  }

  deleteModels(confirm: boolean) {
    if (confirm) {
      this.grid.loading = true;
      this.accountService.groupDelete(AccountApi.EnvironmentAccounts, this.selectedRows).subscribe(res => {
        this.showMessage(this.deleteMsg, MessageType.Info);

        if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
          this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

        this.selectedRows = [];
        this.groupDelete = false;
        this.reloadGrid();
        return;
      }, (error => {
        this.grid.loading = false;
        this.showMessage(error, MessageType.Warning);
      }));
    }

    this.deleteModelsConfirm = false;
  }

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: Account | undefined): void {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
      var order = this.currentOrder;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (this.parent) {
        if (this.parent.childCount > 0)
          filter = this.addFilterToFilterExpression(this.currentFilter,
            new Filter("ParentId", this.parent.id.toString(), "== {0}", "System.Int32"),
            FilterExpressionOperator.And);
      }
      else
        filter = this.addFilterToFilterExpression(this.currentFilter,
          new Filter("ParentId", "null", "== {0}", "System.Int32"),
          FilterExpressionOperator.And);



      //#region load inner grid
      if (this.parentAccount != null && (this.goLastPage || (insertedModel && !this.addToContainer))) {
        //Todo: for all tree component
        //call top 1 account for get totalcount
        this.accountService.getAll(AccountApi.EnvironmentAccounts, 0, 1, order, filter).subscribe((res) => {

          if (res.headers != null) {
            var headers = res.headers != undefined ? res.headers : null;
            if (headers != null) {
              var retheader = headers.get('X-Total-Count');
              if (retheader != null)
                this.totalRecords = parseInt(retheader.toString());
            }
          }

          this.goToLastPage(this.totalRecords);
          this.goLastPage = false;

          this.loadGridData(insertedModel, order, filter);
        });
      }
      //#endregion
      else {
        if (insertedModel && this.addToContainer)
          this.goToLastPage(this.totalRecords);

        this.loadGridData(insertedModel, order, filter);
      }

    }
    else {
      this.rowData = {
        data: [],
        total: 0
      }
    }
  }


  loadGridData(insertedModel?: Account, order?: string, filter?: FilterExpression) {

    this.accountService.getAll(AccountApi.EnvironmentAccounts, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

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

      this.grid.data = this.rowData;


      //expand new row if has childs
      if (insertedModel) {
        var rows = (this.rowData.data as Array<Account>);
        var index = rows.findIndex(p => p.id == insertedModel.parentId);
        if (index == -1 && this.parentAccount != null) {
          var rows = (this.parentAccount.rowData.data as Array<Account>);
          var index = rows.findIndex(p => p.id == insertedModel.parentId);
          if (index >= 0) {
            this.parentAccount.isChildExpanding = true;
            this.parentAccount.grid.collapseRow(this.parentAccount.skip + index);
            this.parentAccount.grid.expandRow(this.parentAccount.skip + index);
          }
        }
        else if (index >= 0) {
          this.isChildExpanding = true;
          this.grid.collapseRow(this.skip + index);
          this.grid.expandRow(this.skip + index);
        }
      }

      //زمانی که تعداد رکورد ها صفر باشد باید کامپوننت پدر رفرش شود
      if (totalCount == 0) {
        if (this.parentAccount && this.parentAccount.Childrens) {
          var thisIndex = this.parentAccount.Childrens.findIndex(p => p == this);
          if (thisIndex >= 0)
            this.parentAccount.Childrens.splice(thisIndex);


          this.parentAccount.reloadGrid();
        }

      }

      this.showloadingMessage = !(resData.length == 0);
      this.totalRecords = totalCount;
      this.grid.loading = false;
    })
  }

  deleteModel(confirm: boolean) {
    if (confirm) {
      this.grid.loading = true;
      this.accountService.delete(String.Format(AccountApi.Account, this.deleteModelId)).subscribe(response => {
        this.deleteModelId = 0;
        this.showMessage(this.deleteMsg, MessageType.Info);
        if (this.rowData.data.length == 1 && this.pageIndex > 1)
          this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

        this.reloadGrid();
      }, (error => {
        this.grid.loading = false;
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }
    //hide confirm dialog
    this.deleteConfirm = false;
  }

  private setParentModel(parentModelId?: number) {
    //debugger;
    if (!parentModelId)
      this.parentModel = undefined;
    else {
      var parentRow = null;
      var findIndex = this.rowData.data.findIndex(acc => acc.id == parentModelId);

      if (findIndex == -1) {
        findIndex = this.parentAccount.rowData.data.findIndex(acc => acc.id == parentModelId);
        if (findIndex >= 0)
          parentRow = this.parentAccount.rowData.data[findIndex];
      }
      else
        parentRow = this.rowData.data[findIndex];
      if (parentRow != null) {
        this.parentModel = parentRow;
      }
    }

  }

  public addNew(parentModelId?: number, addToThis?: boolean) {

    this.isNew = true;
    this.editDataItem = new AccountInfo();
    this.setParentModel(parentModelId);
    //آی دی مربوط به حساب سطح بالاتر برای درج در زیر حساب ها در متغیر parentId مقدار دهی میشود
    if (parentModelId)
      this.parentId = parentModelId;

    if (addToThis)
      this.addToContainer = addToThis;
    else
      this.addToContainer = false;

    this.errorMessage = '';
  }

  public showOnlyParent(dataItem: Account, index: number): boolean {
    return dataItem.childCount > 0;
  }

  public checkShow(dataItem: Account) {
    return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
  }

  //#endregion

}
