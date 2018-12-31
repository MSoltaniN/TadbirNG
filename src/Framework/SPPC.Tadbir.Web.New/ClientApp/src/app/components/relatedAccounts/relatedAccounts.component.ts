import { Component, OnInit, Input, Renderer2, ViewChild, SkipSelf, Host, Optional } from '@angular/core';
import { AccountGroupsService, AccountInfo, SettingService, AccountService } from '../../service/index';
import { Account, AccountGroup } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
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
import { AccountGroupApi, AccountApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { AccountPermissions, AccountGroupPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
import { ActivatedRoute } from '@angular/router';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'relatedAccounts',
  templateUrl: './relatedAccounts.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class RelatedAccountsComponent extends DefaultComponent implements OnInit {

  //#region Fields
  public Childrens: Array<RelatedAccountsComponent>;
  @ViewChild(GridComponent) grid: GridComponent;

  @Input() public parent: Account;
  @Input() public isChild: boolean = false;

  public parentId?: number = undefined;

  public rowData: GridDataResult;
  public selectedRows: number[] = [];
  public totalRecords: number;

  //permission flag
  viewAccess: boolean;

  ////for add in delete messageText
  deleteConfirm: boolean;
  deleteModelId: number;

  currentFilter: FilterExpression;
  currentOrder: string = "";
  public sort: SortDescriptor[] = [];

  showloadingMessage: boolean = true;

  accountGroupModel: AccountGroup;
  accountGroupId: number;
  editDataItem?: Account = undefined;
  parentModel: Account;
  isNew: boolean;
  errorMessage: string;
  groupDelete: boolean = false;

  addToContainer: boolean = false;

  parentTitle: string = '';
  parentValue: string = '';
  parentScope: number = 0;

  isChildExpanding: boolean;
  componentParentId: number;
  goLastPage: boolean;
  //#endregion

  //#region Events
  ngOnInit() {
    this.accountGroupId = this.activatedroute.snapshot.params['groupid'];
    this.getAccountGroup();
  }


  getAccountGroup() {
    this.grid.loading = true;
    this.accountGroupsService.getById(String.Format(AccountGroupApi.AccountGroup, this.accountGroupId)).subscribe(res => {
      this.accountGroupModel = res;

      this.viewAccess = this.isAccess(SecureEntity.Project, AccountPermissions.View);
      if (this.parentComponent && this.parentComponent.isChildExpanding) {
        this.goLastPage = true;
        this.parentComponent.isChildExpanding = false;
      }

      this.reloadGrid();
      if (this.parentComponent) {
        this.parentComponent.addChildComponent(this);
        this.parentId = this.parent.id;
        this.componentParentId = this.parentId;

        this.parentModel = this.parent;
      }

      this.grid.loading = false;
    })
  }



  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
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
  }

  public sortChange(sort: SortDescriptor[]): void {
    if (sort)
      this.currentOrder = sort[0].field + " " + sort[0].dir;
    this.reloadGrid();
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (!this.groupDelete) {
      var recordId = this.selectedRows[0];
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.accountService.getById(String.Format(AccountApi.Account, recordId)).subscribe(res => {
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

    if (!this.isNew) {
      this.isNew = false;
      this.accountService.edit<Account>(String.Format(AccountApi.Account, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
        }, (error => {
          this.editDataItem = model;
          this.errorMessage = error;
        }));
    }
    else {
      model.branchId = this.BranchId;
      model.fiscalPeriodId = this.FiscalPeriodId;
      model.companyId = this.CompanyId;

      this.parentId = this.componentParentId;

      if (this.parentModel) {
        model.parentId = this.parentModel.id;
        model.level = this.parentModel.level + 1;
      }
      else {
        model.parentId = undefined;
        model.level = 0;
      }

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
          this.errorMessage = error;
        }));
    }
  }

  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService, public accountGroupsService: AccountGroupsService, private activatedroute: ActivatedRoute,
    private accountService: AccountService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService,
    @SkipSelf() @Host() @Optional() private parentComponent: RelatedAccountsComponent) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Account, Metadatas.Account);
  }
  //#endregion

  //#region Methods

  /**
  * کامپوننت های فرزند را در متغیری اضافه میکند
  * @param relatedAccountsComponent کامپوننت حساب های مرتبط
  */
  public addChildComponent(relatedAccountsComponent: RelatedAccountsComponent) {

    if (this.Childrens == undefined) this.Childrens = new Array<RelatedAccountsComponent>();
    if (this.Childrens.findIndex(p => p.parent.id === relatedAccountsComponent.parent.id) == -1)
      this.Childrens.push(relatedAccountsComponent);
  }

  reloadGridEvent() {
    this.reloadGrid();
  }

  public reloadGrid(insertedModel?: Account) {

    var apiUrl = String.Format(AccountGroupApi.GroupLedgerAccounts, this.accountGroupId);

    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
      var order = this.currentOrder;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (this.parent) {
        if (this.parent.childCount > 0)
          apiUrl = AccountApi.EnvironmentAccounts;
          filter = this.addFilterToFilterExpression(this.currentFilter,
            new Filter("ParentId", this.parent.id.toString(), "== {0}", "System.Int32"),
            FilterExpressionOperator.And);
      }
      else
        filter = this.addFilterToFilterExpression(this.currentFilter,
          new Filter("ParentId", "null", "== {0}", "System.Int32"),
          FilterExpressionOperator.And);

      //#region load inner grid
      if (this.parentComponent != null && (this.goLastPage || (insertedModel && !this.addToContainer))) {

        //call top 1 for get totalcount
        this.accountService.getAll(apiUrl, 0, 1, order, filter).subscribe((res) => {
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

          this.loadGridData(apiUrl,insertedModel, order, filter);
        });
      }
      //#endregion
      else {
        if (insertedModel && this.addToContainer)
          this.goToLastPage(this.totalRecords);

        this.loadGridData(apiUrl,insertedModel, order, filter);
      }
    }
    else {
      this.rowData = {
        data: [],
        total: 0
      }
    }
  }

  loadGridData(apiUrl:string, insertedModel?: Account, order?: string, filter?: FilterExpression) {

    this.accountService.getAll(apiUrl, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

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
        if (index == -1 && this.parentComponent != null) {
          var rows = (this.parentComponent.rowData.data as Array<Account>);
          var index = rows.findIndex(p => p.id == insertedModel.parentId);
          if (index >= 0) {
            this.parentComponent.isChildExpanding = true;
            this.parentComponent.grid.collapseRow(this.parentComponent.skip + index);
            this.parentComponent.grid.expandRow(this.parentComponent.skip + index);
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
        if (this.parentComponent && this.parentComponent.Childrens) {
          var thisIndex = this.parentComponent.Childrens.findIndex(p => p == this);
          if (thisIndex >= 0)
            this.parentComponent.Childrens.splice(thisIndex);


          this.parentComponent.reloadGrid();
        }

      }

      this.showloadingMessage = !(resData.length == 0);
      this.totalRecords = totalCount;
      this.grid.loading = false;
    })
  }

  deleteModel(confirm: boolean) {
    if (confirm) {
      if (this.groupDelete) {

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
      else {

        this.grid.loading = true;
        this.accountService.delete(String.Format(AccountApi.Account, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);
          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.selectedRows = [];
          this.reloadGrid();
        }, (error => {
          this.grid.loading = false;
          var message = error.message ? error.message : error;
          this.showMessage(error, MessageType.Warning);
        }));

      }
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }

  private setParentModel(parentModelId?: number) {
    if (!parentModelId)
      this.parentModel = undefined;
    else {
      var parentRow = null;
      var findIndex = this.rowData.data.findIndex(acc => acc.id == parentModelId);

      if (findIndex == -1) {
        findIndex = this.parentComponent.rowData.data.findIndex(acc => acc.id == parentModelId);
        if (findIndex >= 0)
          parentRow = this.parentComponent.rowData.data[findIndex];
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


