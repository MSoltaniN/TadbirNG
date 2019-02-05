//#region Imports
import { Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { DefaultComponent } from '../../class/default.component';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { AccountService, SettingService, AccountInfo } from '../../service';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { AccountApi } from '../../service/api';
import { AccountItemBrief, Account } from '../../model';
import { String } from '../../class/source';
import { GridComponent, GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SecureEntity } from '../../security/secureEntity';
import { AccountPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { SortDescriptor, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { Filter } from '../../class/filter';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
import { of } from 'rxjs/observable/of';
import { TreeItem } from '@progress/kendo-angular-treeview';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { AccountTestFormComponent } from './accountTest-form.component';

//#endregion

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'accountTest',
  templateUrl: './accountTest.component.html',
  styles: [`

  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class AccountTestComponent extends DefaultComponent implements OnInit {

  firstTreeNode: Array<AccountItemBrief> = [];
  treeNodes: Array<AccountItemBrief> = [];
  public expandedKeys: any[] = [-1];
  public selectedKeys: number[] = [-1];
  public selectedItem: AccountItemBrief;

  //#region grid
  @ViewChild(GridComponent) grid: GridComponent;
  viewAccess: boolean;
  public rowData: GridDataResult;
  public selectedRows: number[] = [];
  public totalRecords: number;
  currentFilter: FilterExpression;
  groupDelete: boolean = false;
  parentId: number;
  parent: Account;
  editDataItem: Account;
  showloadingMessage: boolean = true;
  deleteConfirm: boolean;
  deleteModelId: number;

  private dialogRef: DialogRef;
  private dialogModel: any;

  //#endregion

  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.Account, AccountPermissions.View);
    this.getTreeNode();

    this.reloadGrid();
  }



  constructor(public toastrService: ToastrService, public translate: TranslateService, private accountService: AccountService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Account, Metadatas.Account);
  }


  getTreeNode() {
    this.accountService.getModels(AccountApi.EnvironmentAccountsLedger).subscribe(res => {
      this.firstTreeNode = [{ id: -1, name: 'حسابهای کل', code: '', fullCode: '', level: 0, childCount: 1, parentId: -1, isSelected: true }];
      this.selectedItem = this.firstTreeNode[0];
      this.treeNodes = res;
    })
  }

  //مشخص میکند که آیتم ها، فرزند دارند یا خیر
  public hasChildren = (item: any) => {
    if (item.childCount > 0 || item.id == -1) {
      return true;
    }
    return false;
  };

  public fetchChildren = (dataItem: any) => {
    if (dataItem.id == -1) {
      return of(this.treeNodes.filter(f => f.parentId == null));
    }
    else {
      var nodes = this.treeNodes.filter(f => f.parentId == dataItem.id);
      if (nodes.length > 0) {
        return of(nodes);
      }
      else {
        var newNodes = this.accountService.getModels(String.Format(AccountApi.AccountChildren, dataItem.id));
        newNodes.subscribe(res => {
          this.treeNodes = [...this.treeNodes, ...res];
        })
        return newNodes;
      }
    }
  }

  public handleSelection(item: TreeItem): void {
    this.selectedItem = item.dataItem;
    this.parentId = this.selectedItem && this.selectedItem.id > 0 ? this.selectedItem.id : undefined;
    this.currentFilter = undefined;
    this.selectedRows = [];
    this.pageIndex = 0;
    this.getParent();
    this.reloadGrid();
  }


  //#region grid

  reloadGrid(insertedModel?: Account) {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      var parent_Id = this.parentId ? this.parentId.toString() : "null";
      filter = this.addFilterToFilterExpression(this.currentFilter,
        new Filter("ParentId", parent_Id, "== {0}", "System.Int32"), FilterExpressionOperator.And);

      this.accountService.getAll(AccountApi.EnvironmentAccounts, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {

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

        this.showloadingMessage = !(resData.length == 0);
        this.totalRecords = totalCount;
        this.grid.loading = false;
      })
    }
    else {
      this.rowData = {
        data: [],
        total: 0
      }
    }
  }

  getParent() {
    if (this.parentId) {
      this.accountService.getAccountById(this.parentId).subscribe(res => {
        this.parent = res;
      })
    }
    else {
      this.parent = undefined;
    }
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort.filter(f => f.dir != undefined);
    this.reloadGrid();
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

  /**
  * باز کردن و مقداردهی اولیه به فرم ویرایشگر
  */
  openEditorDialog(isNew: boolean) {

    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: AccountTestFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.parent = this.parent;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessage = undefined;


    this.dialogRef.content.instance.save.subscribe((res) => {
      this.saveHandler(res, isNew);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });
  }


  public addNew() {
    this.editDataItem = new AccountInfo();
    this.openEditorDialog(true);
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];

    this.grid.loading = true;
    this.accountService.getById(String.Format(AccountApi.Account, recordId)).subscribe(res => {

      this.editDataItem = res;
      this.openEditorDialog(false);

      this.grid.loading = false;
    })
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (!this.groupDelete) {
      var recordId = this.selectedRows[0];
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
    else {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
  }

  public saveHandler(model: Account, isNew: boolean) {
    this.grid.loading = true;
    if (!isNew) {
      this.accountService.edit<Account>(String.Format(AccountApi.Account, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);

          this.dialogRef.close();
          this.dialogModel.parent = undefined;
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid();

          this.refreshTreeNodes(model);

        }, (error => {
          this.editDataItem = model;
          this.dialogModel.errorMessage = error;
        }));
    }
    else {
      this.accountService.insert<Account>(AccountApi.EnvironmentAccounts, model)
        .subscribe((response: any) => {
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;

          this.dialogRef.close();
          this.dialogModel.parent = undefined;
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid(insertedModel);

          this.refreshTreeNodes(insertedModel);

        }, (error => {
          this.dialogModel.errorMessage = error;
        }));

    }
    this.grid.loading = false;

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

          this.refreshTreeNodes();

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

          this.refreshTreeNodes();

        }, (error => {
          this.grid.loading = false;
          var message = error.message ? error.message : error;
          this.showMessage(message, MessageType.Warning);
        }));

      }
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }
  //#endregion


  /**
   * یک ایتم به آرایه نودهای درخت اضافه یا  ویرایش یا حذف میکند و درخت را رفرش میکند
   * @param model 
   */
  refreshTreeNodes(model?: Account) {
    if (model) {
      var item = this.treeNodes.filter(f => f.id == model.id);
      if (item.length > 0) {
        item[0].code = model.code;
        item[0].fullCode = model.fullCode;
        item[0].name = model.name;
      }
      else {
        if (model.parentId == null || (model.parentId != null && this.treeNodes.filter(f => f.parentId == model.parentId).length > 0)) {
          this.treeNodes.push({
            id: model.id,
            name: model.name,
            parentId: model.parentId,
            fullCode: model.fullCode,
            code: model.code,
            childCount: model.childCount,
            isSelected: true,
            level: model.level
          })
        }
        var parentItem = this.treeNodes.filter(f => f.id == model.parentId);
        if (parentItem.length > 0) {
          parentItem[0].childCount++;
        }
      }

      var items = this.expandedKeys;
      this.expandedKeys = [];
      setTimeout(() => {
        this.expandedKeys = items;
      })
    }
    else {
      this.treeNodes = this.treeNodes.filter(f => f.parentId != this.parentId);
      var url = this.parentId ? String.Format(AccountApi.AccountChildren, this.parentId) : AccountApi.EnvironmentAccountsLedger;
      this.accountService.getModels(url).subscribe(res => {
        debugger;
        this.treeNodes = [...this.treeNodes, ...res];
        var parent = this.treeNodes.filter(f => f.id == this.parentId);
        if (this.parentId)
          parent[0].childCount = res.length;
        debugger;
        var items = this.expandedKeys;
        this.expandedKeys = [];
        setTimeout(() => {
          this.expandedKeys = items;
        })
      })
    }
  }
}

