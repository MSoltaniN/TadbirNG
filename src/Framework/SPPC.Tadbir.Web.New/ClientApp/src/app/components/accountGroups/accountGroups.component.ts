import { Component, OnInit, Renderer2, ViewChild, ChangeDetectorRef, NgZone } from '@angular/core';
import { AccountGroupsService, AccountGroupInfo, SettingService, GridService } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { String } from "../../class/source";
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { ReportingService } from '../../service/report/reporting.service';
import { AutoGeneratedGridComponent } from '../../class/autoGeneratedGrid.component';
import { ViewName } from '../../security/viewName';
import { AccountGroupApi, AccountApi } from '../../service/api';
import { TreeItem } from '@progress/kendo-angular-treeview';
import { AccountItemBrief, ViewTreeConfig, ViewTreeLevelConfig, Account, AccountGroup } from '../../model';
import { of } from 'rxjs/observable/of';
import { Filter } from '../../class/filter';
import { AccountFormComponent } from '../account/account-form.component';
import { AccountGroupsFormComponent } from './accountGroups-form.component';
import { ContextMenuComponent } from '@progress/kendo-angular-menu';
import { ColumnBase } from '@progress/kendo-angular-grid';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'accountGroups',
  templateUrl: './accountGroups.component.html',
  styles: [`
.popup-secton{
    padding: 30px;    
    border: 1px solid rgba(0,0,0,.05);
}

.popup-secton button{
margin:10px;
}

`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class AccountGroupsComponent extends AutoGeneratedGridComponent implements OnInit {

  treeParentTitle: string;

  firstTreeNode: Array<AccountItemBrief> = [];
  treeNodes: Array<AccountItemBrief> = [];
  breadCrumbList: Array<AccountItemBrief> = [];
  treeConfig: ViewTreeConfig;
  levelConfig: ViewTreeLevelConfig;
  public selectedItem: AccountItemBrief;
  public expandedKeys: any[] = ['0'];
  public selectedKeys: any[] = ['0'];
  editDataItem: any;
  parentId: number;
  parent: Account;
  clickedRowItem: any = undefined;

  public dialogRef: DialogRef;
  public dialogModel: any;


  @ViewChild('accountTreeMenu') public accountContextMenu: ContextMenuComponent;
  public accountContextMenuItem: any[] = [
    { text: 'AccountGroup.NewAccount', icon: 'file-add', mode: 'New' },
    { text: 'AccountGroup.EditAccount', icon: 'edit', mode: 'Edit' },
    { text: 'AccountGroup.DeleteAccount', icon: 'delete', mode: 'Remove' }];
  @ViewChild('accountGroupTreeMenu') public accountGroupContextMenu: ContextMenuComponent;
  public accountGroupContextMenuItem: any[] = [
    { text: 'AccountGroup.NewLedgerAccount', icon: 'file-add', mode: 'New' },
    { text: 'AccountGroup.EditAccountGroup', icon: 'edit', mode: 'Edit' },
    { text: 'AccountGroup.Delete', icon: 'delete', mode: 'Remove' }];
  @ViewChild('baseTreeMenu') public baseContextMenu: ContextMenuComponent;
  public baseContextMenuItems: any[] = [
    { text: 'AccountGroup.New', icon: 'file-add', mode: 'New' }];
  selectedContextmenu: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public accountGroupService: AccountGroupsService,
    public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, cdref, ngZone);
  }

  ngOnInit() {
    this.treeConfig = this.getViewTreeSettings(ViewName[Entities.Account]);

    this.metadataUrlType = 1;
    this.entityName = Entities.AccountGroup;
    this.metadataType = Metadatas.AccountGroup;
    this.viewId = ViewName[this.entityName];
    this.getDataUrl = AccountGroupApi.AccountGroups;


    this.getTreeNode();

    this.reloadGrid();

    this.showloadingMessage = false;

    this.cdref.detectChanges();
  }


  getTreeNode() {
    this.accountGroupService.getModels(AccountGroupApi.AccountGroupBrief).subscribe(res => {
      this.treeParentTitle = this.getText("AccountGroup.TreeTitle");

      this.firstTreeNode = [{
        id: -1,
        name: this.getText("AccountGroup.TreeTitle"),
        code: '',
        fullCode: '',
        level: 0,
        childCount: 1,
        parentId: -1,
        isSelected: true
      }];
      this.selectedItem = this.firstTreeNode[0];
      this.treeNodes = res;

      this.breadCrumbList.push(this.firstTreeNode[0]);
    })
  }

  /**مشخص میکند که آیتم ها، فرزند دارند یا خیر*/
  public hasChildren = (item: any) => {
    if (item.childCount > 0 || item.id == -1) {
      return true;
    }
    return false;
  };

  /** فرزندان یک نود را واکشی میکند*/
  public fetchChildren = (dataItem: any) => {
    if (dataItem.id == -1) {
      return of(this.treeNodes.filter(f => f.parentId == null && f.code == null));
    }
    else {
      if (dataItem.code) {
        //حساب
        var nodes = this.treeNodes.filter(f => f.parentId == dataItem.id);
        if (nodes.length > 0) {
          return of(nodes);
        }
        else {
          var newNodes = this.accountGroupService.getModels(String.Format(AccountApi.AccountChildren, dataItem.id));
          newNodes.subscribe(res => {
            this.treeNodes = [...this.treeNodes, ...res];
          })
          return newNodes;
        }
      }
      else {
        //گروه حساب و باید حساب کل به زیر مجموعه اضافه شود
        var allLedgerNodes = this.treeNodes.filter(f => f.parentId == null && f.code && f.groupId == dataItem.id);
        if (allLedgerNodes.length > 0) {
          return of(allLedgerNodes);
        }
        else {
          var ledgerNodes = this.accountGroupService.getModels(String.Format(AccountApi.EnvironmentLedgerAccountsByGroupId, dataItem.id));
          ledgerNodes.subscribe(res => {
            this.treeNodes = [...this.treeNodes, ...res];
          })
          return ledgerNodes;
        }
      }
    }
  }

  public handleSelection(item: TreeItem): void {
    this.parent = undefined;
    this.parentId = undefined;

    this.defaultFilter = [];
    this.selectedItem = item.dataItem;
    this.selectedRows = [];
    this.pageIndex = 0;

    this.expandedKeys.push(item.index);

    var parts = item.index.split('_');

    if (parts.length == 1) {
      //روی نود گروه حساب کلیک شده و باید گرید گروه حساب نمایش داده شود
      this.entityName = Entities.AccountGroup;
      this.metadataType = Metadatas.AccountGroup;
      this.viewId = ViewName[this.entityName];
      this.getDataUrl = AccountGroupApi.AccountGroups
    }
    else
      if (parts.length == 2) {
        // گروه حساب انتخاب شده است و باید حساب های کل در گرید نمایش داده شود
        this.entityName = Entities.Account;
        this.metadataType = Metadatas.Account;
        this.viewId = ViewName[this.entityName];
        this.getDataUrl = AccountApi.EnvironmentAccounts;

        this.parentId = undefined;

        this.defaultFilter.push(new Filter("ParentId", "null", "== {0}", "System.Int32"));
        this.defaultFilter.push(new Filter("GroupId", this.selectedItem.id.toString(), "== {0}", "System.Int32"));
      }
      else if (parts.length > 2) {
        // حساب انتخاب شده است
        this.parentId = this.selectedItem.id;
        this.getParent();
        this.entityName = Entities.Account;
        this.metadataType = Metadatas.Account;
        this.viewId = ViewName[this.entityName];
        this.getDataUrl = AccountApi.EnvironmentAccounts;

        this.defaultFilter.push(new Filter("ParentId", this.parentId.toString(), "== {0}", "System.Int32"));
      }

    this.reloadGrid();

    this.getBreadCrumbItems();
  }

  getBreadCrumbItems() {
    this.breadCrumbList = [];
    this.getBreadCrumbRecursiveItems(this.selectedItem);
  }

  getBreadCrumbRecursiveItems(item: AccountItemBrief) {

    if (item.parentId != null) {
      var parent = this.treeNodes.filter(f => f.id == item.parentId && f.code);
      if (parent.length > 0)
        this.getBreadCrumbRecursiveItems(parent[0]);
    }
    else {
      if (item.groupId != null) {
        var accGroup = this.treeNodes.find(f => f.code == null && f.id == item.groupId);
        if (accGroup)
          this.getBreadCrumbRecursiveItems(accGroup);
      }
      else {
        this.getBreadCrumbRecursiveItems(this.firstTreeNode[0]);
      }
    }
    this.breadCrumbList.push(item);
  }

  selectBreadCrumb(item: AccountItemBrief) {
    var index = this.retriveTreeIndex(item);
    this.handleSelection({ dataItem: item, index: index });

    this.selectedKeys = [];
    this.selectedKeys.push(index);
  }

  retriveTreeIndex(item: AccountItemBrief): string {
    let index: string;

    if (item.id != -1) {

      if (item.parentId == null && item.groupId == null) {
        var groupList = this.treeNodes.filter(f => f.parentId == null && f.groupId == null);
        var itemIndex = groupList.findIndex(f => f.id == item.id);
        index = "0_" + itemIndex;
      }
      else {
        if (item.parentId == null) {
          var group = this.treeNodes.find(f => f.id == item.groupId);
          index = this.retriveTreeIndex(group);
          var ledgerAccountList = this.treeNodes.filter(f => f.parentId == null && f.groupId != null && f.groupId == item.groupId);
          var ledgerAccountIndex = ledgerAccountList.findIndex(f => f.id == item.id);
          index = index + "_" + ledgerAccountIndex;
        }
        else {
          var parent = this.treeNodes.find(f => f.id == item.parentId && (f.parentId != null || f.parentId == null && f.groupId != null));
          index = this.retriveTreeIndex(parent);
          var accountList = this.treeNodes.filter(f => f.parentId == item.parentId);
          var accountIndex = accountList.findIndex(f => f.id == item.id);
          index = index + "_" + accountIndex;
        }
      }

    }
    else {
      index = index ? "0_" + index : "0";
    }

    return index;
  }


  getParent() {
    if (this.parentId) {
      this.accountGroupService.getById(String.Format(AccountApi.Account, this.parentId)).subscribe(res => {
        this.parent = res;
      })
    }
    else {
      this.parent = undefined;
    }
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {

    if (this.selectedItem.id == -1) {
      //account group
      this.dialogRef = this.dialogService.open({
        title: isNew ? this.getText("AccountGroup.New") : this.getText("AccountGroup.EditAccountGroup"),
        content: AccountGroupsFormComponent,
      });

      this.dialogModel = this.dialogRef.content.instance;
      this.dialogModel.model = this.editDataItem;
      this.dialogModel.isNew = isNew;
      this.dialogModel.errorMessage = undefined;

      this.dialogRef.content.instance.save.subscribe((res) => {
        this.saveAccountGroupHandler(res, isNew);
      });

      const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
        this.dialogRef.close();
      });
    }
    else {
      //account
      var errorMsg = this.getText('Messages.TreeLevelsAreTooDeep');
      var editorTitle = this.getEditorTitle(isNew);

      if (this.levelConfig)
        if (this.levelConfig.isEnabled) {
          this.dialogRef = this.dialogService.open({
            title: editorTitle,
            content: AccountFormComponent,
          });

          this.dialogModel = this.dialogRef.content.instance;
          this.dialogModel.parent = this.parent;
          this.dialogModel.model = this.editDataItem;
          this.dialogModel.isNew = isNew;
          this.dialogModel.errorMessage = undefined;

          this.dialogRef.content.instance.save.subscribe((res) => {
            this.saveAccountHandler(res, isNew);
          });

          const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
            this.dialogRef.close();
          });

        }
        else {
          this.showMessage(String.Format(errorMsg, (this.levelConfig.no - 1).toString()), MessageType.Warning);
        }
    }
  }

  getEditorTitle(isNew: boolean): string {
    var editorTitle = '';

    this.treeConfig = this.getViewTreeSettings(ViewName[Entities.Account]);
    if (this.treeConfig) {
      var level = this.parent ? this.parent.level + 2 : 1;
      var viewConfig = this.treeConfig.levels.find(f => f != null && f.no == level);

      this.levelConfig = viewConfig;

      if (viewConfig)
        editorTitle = viewConfig.name;
    }

    return String.Format(this.getText(isNew ? "Account.EditorTitleNew" : "Account.EditorTitleEdit"), editorTitle);
  }

  addNew() {
    if (this.selectedItem.id == -1) {
      //افزودن گروه حساب
      this.editDataItem = new AccountGroupInfo();
      this.openEditorDialog(true);
    }
    else {
      this.grid.loading = true;
      this.accountGroupService.getById(String.Format(AccountApi.EnvironmentNewChildAccount, this.parentId ? this.parentId : "0")).subscribe(res => {
        this.editDataItem = res;
        if (!this.parent)
          this.editDataItem.groupId = this.selectedItem.id;

        this.openEditorDialog(true);

        this.grid.loading = false;
      }, (error => {
        this.grid.loading = false;
        this.showMessage(error.error, MessageType.Warning);
      }));
    }

  }

  editHandler() {
    var recordId = this.selectedRows[0];

    this.grid.loading = true;
    var url = this.selectedItem.id == -1 ? AccountGroupApi.AccountGroup : AccountApi.Account;
    this.accountGroupService.getById(String.Format(url, recordId)).subscribe(res => {

      this.editDataItem = res;
      this.openEditorDialog(false);

      this.grid.loading = false;
    })
  }

  removeHandler() {
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

  deleteModel(confirm: boolean) {

    if (confirm) {
      //حذف گروهی از گرید
      if (this.groupDelete && !this.selectedContextmenu) {

        this.grid.loading = true;
        if (this.selectedItem.id != -1) {//چون متد حذف گروهی برای گروه حساب وجود ندارد این شرط گذاشته شده
          var url = this.selectedItem.id == -1 ? "" : AccountApi.EnvironmentAccounts;
          this.accountGroupService.groupDelete(url, this.selectedRows).subscribe(res => {

            this.showMessage(String.Format(this.getText("Messages.Deleted"), this.getText("Entity." + this.entityName)), MessageType.Info);            

            if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
              this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

            this.reloadGrid();

            if (this.selectedItem.id == -1)
              this.refreshGroupTreeNodes();
            else
              this.refreshTreeNodes();

          }, (error => {
            this.grid.loading = false;
            this.showMessage(error, MessageType.Warning);
          }));
        }
      }
      else {
        //حذف یک سطر از گرید
        if (!this.selectedContextmenu) {
          this.grid.loading = true;
          var url = this.selectedItem.id == -1 ? AccountGroupApi.AccountGroup : AccountApi.Account;
          this.accountGroupService.delete(String.Format(url, this.deleteModelId)).subscribe(response => {

            this.showMessage(String.Format(this.getText("Messages.Deleted"), this.getText("Entity." + this.entityName)), MessageType.Info);            
          
            if (this.rowData.data.length == 1 && this.pageIndex > 1)
              this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

            this.reloadGrid();

            if (this.selectedItem.id == -1)
              this.refreshGroupTreeNodes();
            else
              this.refreshTreeNodes();


          }, (error => {
            this.grid.loading = false;
            var message = error.message ? error.message : error;
            this.showMessage(message, MessageType.Warning);
          }));
        }
        else {
          //حذف از منوی راست  کلیک
          this.removeFromContextmenu();
        }

      }
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }

  subsetHandler() {

    var recordId = this.selectedRows[0];
    this.clickedRowItem = this.rowData.data.find(f => f.id == recordId);

    this.rowDoubleClickHandler();
  }

  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  rowDoubleClickHandler() {

    var recordId = this.clickedRowItem.id;

    let index: string = '';
    let parentItem: AccountItemBrief;

    if (this.selectedItem.id == -1) {
      parentItem = this.treeNodes.find(f => f.id == recordId && f.parentId == null && f.groupId == null);
      index = this.retriveTreeIndex(parentItem);
    }
    else {
      parentItem = this.treeNodes.find(f => f.id == recordId && (f.parentId != null || (f.parentId == null && f.groupId != null)))
      index = this.retriveTreeIndex(parentItem);
    }

    this.selectedKeys = [];
    this.selectedKeys.push(index);

    this.handleSelection({ dataItem: parentItem, index: index });
  }

  onNodeClick(e: any): void {
    if (e.type === 'contextmenu') {
      const originalEvent = e.originalEvent;

      originalEvent.preventDefault();

      this.selectedContextmenu = e.item.dataItem;
      var leftPosition = this.CurrentLanguage == 'fa' ? originalEvent.pageX - 135 : originalEvent.pageX;

      if (this.selectedContextmenu.id == -1) {
        this.baseContextMenu.show({ left: leftPosition, top: originalEvent.pageY });
      }
      else
        if (this.selectedContextmenu.id != -1 && this.selectedContextmenu.parentId == null && this.selectedContextmenu.groupId == null) {
          this.accountGroupContextMenu.show({ left: leftPosition, top: originalEvent.pageY });
        }
        else {
          this.accountContextMenu.show({ left: leftPosition, top: originalEvent.pageY });
        }
    }
  }

  onSelectContextmenu({ item }): void {
    switch (item.mode) {
      case 'Remove': {
        this.contextMenuRemoveHandler();
        break;
      }
      case 'Edit': {
        this.contextMenuEditHandler();
        this.selectedContextmenu = undefined;
        break;
      }
      case 'New': {
        this.contextMenuAddNewHandler();
        this.selectedContextmenu = undefined;
        break;
      }
      default:
    }

  }

  contextMenuAddNewHandler() {

    var index = this.retriveTreeIndex(this.selectedContextmenu);
    this.selectedKeys = [];
    this.selectedKeys.push(index);
    this.handleSelection({ dataItem: this.selectedContextmenu, index: index });

    this.addNew();

  }

  contextMenuEditHandler() {

    var parentItem = this.getParentItem();

    var index = this.retriveTreeIndex(parentItem);
    this.selectedKeys = [];
    this.selectedKeys.push(index);
    this.handleSelection({ dataItem: parentItem, index: index });

    this.grid.loading = true;

    var url = parentItem.id == -1 ? AccountGroupApi.AccountGroup : AccountApi.Account;

    this.accountGroupService.getById(String.Format(url, this.selectedContextmenu.id)).subscribe(res => {

      this.editDataItem = res;
      this.openEditorDialog(false);

      this.grid.loading = false;
    })
  }

  contextMenuRemoveHandler() {
    this.deleteConfirm = true;
    this.prepareDeleteConfirm(this.selectedContextmenu.name);
    this.deleteModelId = this.selectedContextmenu.id;
  }

  removeFromContextmenu() {

    var parentItem = this.getParentItem();

    var index = this.retriveTreeIndex(parentItem);
    this.selectedKeys = [];
    this.selectedKeys.push(index);
    this.selectedItem = parentItem;

    var url = parentItem.id == -1 ? AccountGroupApi.AccountGroup : AccountApi.Account;



    this.grid.loading = true;
    this.accountGroupService.delete(String.Format(url, this.deleteModelId)).subscribe(response => {      

      this.handleSelection({ dataItem: parentItem, index: index });

      //this.showMessage(String.Format(this.getText("Messages.Deleted"), this.getText("Entity." + this.entityName)), MessageType.Info);
      this.showMessage(this.deleteMsg, MessageType.Succes);

      this.selectedRows = [];
      this.selectedRows.push(this.deleteModelId);

      if (this.selectedItem.id == -1)
        this.refreshGroupTreeNodes();
      else
        this.refreshTreeNodes();

      this.selectedContextmenu = undefined;
      this.grid.loading = false;

    }, (error => {
      this.grid.loading = false;
      var message = error.message ? error.message : error;
      this.showMessage(message, MessageType.Warning);
    }));
  }

  getParentItem(): AccountItemBrief {

    let parentItem: AccountItemBrief;

    if (this.selectedContextmenu.parentId == null && this.selectedContextmenu.groupId == null)
      parentItem = this.firstTreeNode[0];
    else {
      if (this.selectedContextmenu.parentId == null && this.selectedContextmenu.groupId != null)
        parentItem = this.treeNodes.find(f => f.id == this.selectedContextmenu.groupId && f.parentId == null && f.groupId == null);
      else
        if (this.selectedContextmenu.parentId != null && this.selectedContextmenu.groupId == null)
          parentItem = this.treeNodes.find(f => f.id == this.selectedContextmenu.parentId && (f.parentId != null || (f.parentId == null && f.groupId != null)));
    }

    return parentItem;

  }

  saveAccountHandler(model: any, isNew: boolean) {
    this.grid.loading = true;
    if (!isNew) {
      this.accountGroupService.edit<Account>(String.Format(AccountApi.Account, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;

          //this.showMessage(String.Format(this.getText("Messages.Updated"), this.getText("Entity." + this.entityName)), MessageType.Info);
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
      this.accountGroupService.insert<Account>(AccountApi.EnvironmentAccounts, model)
        .subscribe((response: any) => {
          this.editDataItem = undefined;

          //this.showMessage(String.Format(this.getText("Messages.Inserted"), this.getText("Entity." + this.entityName)), MessageType.Info);
          this.showMessage(this.insertMsg, MessageType.Succes);

          var insertedModel = response;

          this.dialogRef.close();
          this.dialogModel.parent = undefined;
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.selectedRows = [];
          this.reloadGrid(insertedModel);

          this.refreshTreeNodes(insertedModel);

        }, (error => {
          this.dialogModel.errorMessage = error;
        }));

    }
    this.grid.loading = false;
  }

  saveAccountGroupHandler(model: any, isNew: boolean) {
    this.grid.loading = true;
    if (!isNew) {
      this.accountGroupService.edit<AccountGroup>(String.Format(AccountGroupApi.AccountGroup, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          //this.showMessage(String.Format(this.getText("Messages.Updated"), this.getText("Entity." + this.entityTypeName)), MessageType.Info);
          this.showMessage(this.updateMsg, MessageType.Succes);

          this.dialogRef.close();
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid();

          this.refreshGroupTreeNodes(model);

        }, (error => {
          this.editDataItem = model;
          this.dialogModel.errorMessage = error;
        }));
    }
    else {
      this.accountGroupService.insert<AccountGroup>(AccountGroupApi.AccountGroups, model)
        .subscribe((response: any) => {
          this.editDataItem = undefined;
          //this.showMessage(String.Format(this.getText("Messages.Inserted"), this.getText("Entity." + this.entityTypeName)), MessageType.Info);
          this.showMessage(this.insertMsg, MessageType.Succes);

          var insertedModel = response;

          this.dialogRef.close();
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.selectedRows = [];
          this.reloadGrid(insertedModel);

          this.refreshGroupTreeNodes(insertedModel);

        }, (error => {
          this.dialogModel.errorMessage = error;
        }));

    }
    this.grid.loading = false;
  }

  /**
* یک ایتم به آرایه نودهای درخت اضافه یا  ویرایش یا حذف میکند و درخت را رفرش میکند
* @param model 
*/
  refreshTreeNodes(model?: any) {
    if (model) {
      var item = this.treeNodes.find(f => f.id == model.id && (f.parentId == null && f.groupId != null || f.parentId != null));
      if (item) {
        //edit
        item.code = model.code;
        item.fullCode = model.fullCode;
        item.name = model.name;
      }
      else {
        //add
        if (model.parentId == null || (model.parentId != null && this.treeNodes.filter(f => f.parentId == model.parentId).length > 0)) {
          this.treeNodes.push({
            id: model.id,
            name: model.name,
            parentId: model.parentId,
            fullCode: model.fullCode,
            code: model.code,
            childCount: model.childCount,
            isSelected: true,
            level: model.level,
            groupId: model.groupId
          })
        }
        let parentItem: any;
        if (model.parentId == null && model.groupId != null) {
          parentItem = this.treeNodes.find(f => f.id == model.groupId);
        }
        else {
          parentItem = this.treeNodes.find(f => f.id == model.parentId);
        }

        if (parentItem) {
          parentItem.childCount++;
        }
      }
    }
    else {
      if (this.parentId) {
        //
        this.treeNodes = this.treeNodes.filter(f => f.parentId != this.parentId);
        this.accountGroupService.getModels(String.Format(AccountApi.AccountChildren, this.parentId)).subscribe(res => {
          this.treeNodes = [...this.treeNodes, ...res];
          var parent = this.treeNodes.find(f => f.id == this.parentId);
          if (this.parentId)
            parent.childCount = res.length;
        })
      }
      else {
        // حساب کل
        this.selectedRows.forEach(item => {
          var itemIndex = this.treeNodes.findIndex(f => f.id == item && f.parentId == null && f.groupId != null);
          if (itemIndex > -1)
            this.treeNodes.splice(itemIndex, 1);
        })
        var group = this.treeNodes.find(f => f.id == this.selectedItem.id && f.parentId == null && f.groupId == null);
        group.childCount -= this.selectedRows.length;
      }

      this.groupDelete = false;
      this.selectedRows = [];
      this.deleteModelId = 0;
    }

    var items = this.expandedKeys;
    this.expandedKeys = [];
    setTimeout(() => {
      this.expandedKeys = items;
    })
  }

  refreshGroupTreeNodes(model?: any) {
    if (model) {
      var item = this.treeNodes.find(f => f.id == model.id && f.parentId == null && f.groupId == null);
      if (item) {
        //edit
        item.name = model.name;
      }
      else {
        //add
        this.treeNodes.push({
          id: model.id,
          name: model.name,
          parentId: null,
          fullCode: null,
          code: null,
          childCount: 0,
          isSelected: true,
          level: 0,
          groupId: null
        })
      }
    }
    else {
      //delete
      if (this.groupDelete) {

      }
      else {
        var groupIndex = this.treeNodes.findIndex(f => f.id == this.deleteModelId && f.parentId == null && f.groupId == null);
        if (groupIndex > -1)
          this.treeNodes.splice(groupIndex, 1);
      }

      this.groupDelete = false;
      this.deleteModelId = 0;
      this.selectedRows = [];
    }

    var items = this.expandedKeys;
    this.expandedKeys = [];
    setTimeout(() => {
      this.expandedKeys = items;
    })
  }

  public onDataStateChange(): void {
    if (this.rowData && this.rowData.total > 0) {
      var fcolumns = new Array<ColumnBase>();
      this.grid.columns.forEach(function (column) {
        if (column.width == undefined)
          fcolumns.push(column);
      });
      this.fitColumns(fcolumns);
    }
  } 
}


