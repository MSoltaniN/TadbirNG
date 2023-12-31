import {
  Inject,
  Injectable,
  OnInit,
  Optional,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogRef, DialogService } from "@progress/kendo-angular-dialog";
import {
  GridComponent,
  GridDataResult,
  PageChangeEvent,
  RowArgs,
  SelectAllCheckboxState,
} from "@progress/kendo-angular-grid";
import { ContextMenuComponent } from "@progress/kendo-angular-menu";
import { TreeItem } from "@progress/kendo-angular-treeview";
import {
  CompositeFilterDescriptor,
  SortDescriptor,
} from "@progress/kendo-data-query";
import { ViewTreeConfig, ViewTreeLevelConfig } from "@sppc/config/models";
import { SettingService } from "@sppc/config/service";
import { AccountItemBrief } from "@sppc/finance/models";
import { ServiceLocator } from "@sppc/service.locator";
import { Entities, MessageType } from "@sppc/shared/enum/metadata";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { GridService } from "@sppc/shared/services/grid.service";
import { MetaDataService } from "@sppc/shared/services/metadata.service";
import { ToastrService } from "ngx-toastr";
import { of } from "rxjs";
import { AccountPermissions } from "../security/permissions";
import { ErrorHandlingService } from "../services";
import { DefaultComponent } from "./default.component";
import { Filter } from "./filter";
import { FilterExpression } from "./filterExpression";
import { FilterExpressionOperator } from "./filterExpressionOperator";
import { ReloadOption } from "./reload-option";
import { String } from "./source";

@Injectable()
export class GridExplorerComponent<T>
  extends DefaultComponent
  implements OnInit
{
  treeParentTitle: string;

  firstTreeNode: Array<AccountItemBrief> = [];
  treeNodes: Array<AccountItemBrief> = [];
  public expandedKeys: any[] = [-1];
  public selectedKeys: number[] = [-1];
  public selectedItem: AccountItemBrief;
  breadCrumbList: Array<AccountItemBrief> = [];
  treeConfig: ViewTreeConfig;
  levelConfig: ViewTreeLevelConfig;

  treeScrollTop: number;

  @ViewChild("treemenu", {static: true}) public treeContextMenu: ContextMenuComponent;
  public contextmenuItems: any[] = [
    { text: "Buttons.New", icon: "file-add", mode: "New" },
    { text: "Buttons.Edit", icon: "edit", mode: "Edit" },
    { text: "Buttons.Delete", icon: "delete", mode: "Remove" },
  ];
  @ViewChild("treemenulimited", {static: true})
  public treeContextMenuLimited: ContextMenuComponent;
  public contextmenuLimitedItems: any[] = [
    { text: "Buttons.New", icon: "file-add", mode: "New" },
  ];
  selectedContextmenu: any;

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;

  public rowData: GridDataResult;
  public selectedRows: number[] = [];
  public totalRecords: number;
  currentFilter: FilterExpression;
  groupDelete: boolean = false;
  parentId: number;
  parent: any;
  editDataItem: any;
  showloadingMessage: boolean = true;
  deleteConfirm: boolean;
  deleteModelId: number;
  clickedRowItem: any = undefined;

  public dialogRef: DialogRef;
  public dialogModel: any;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public service: GridService,
    public dialogService: DialogService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    @Optional() @Inject("empty") public entityName: string,
    @Optional() @Inject("empty") public parentTitlekey: string,
    @Optional() @Inject("empty") public editorNewTitlePattern: string,
    @Optional() @Inject("empty") public editorEditTitlePattern: string,
    @Optional() @Inject("empty") public environmentModelsUrl: string,
    @Optional() @Inject("empty") public environmentModelsLedgerUrl: string,
    @Optional() @Inject("empty") public modelUrl: string,
    @Optional() @Inject("empty") public modelChildrenUrl: string,
    @Optional() @Inject("empty") public modelNewChildUrl: string,
    @Optional() @Inject("empty") public viewId: number,
    @Inject(ErrorHandlingService)
    public errorHandlingService?: ErrorHandlingService
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      settingService,
      entityName,
      viewId
    );

    this.errorHandlingService =
      ServiceLocator.injector.get(ErrorHandlingService);
  }

  ngOnInit() {
    this.treeConfig = this.getViewTreeSettings(this.viewId);
    this.getTreeNode();
    this.reloadGrid();
  }

  getTreeNode() {
    this.service.getModels(this.environmentModelsLedgerUrl).subscribe((res) => {
      this.treeParentTitle = this.getText(this.parentTitlekey);

      this.firstTreeNode = [
        {
          id: -1,
          name: this.getText(this.parentTitlekey),
          code: "",
          fullCode: "",
          level: 0,
          childCount: 1,
          parentId: -1,
          isSelected: true,
        },
      ];
      this.selectedItem = this.firstTreeNode[0];
      this.treeNodes = res;
      this.breadCrumbList.push(this.firstTreeNode[0]);
    });
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
      return of(this.treeNodes.filter((f) => f.parentId == null));
    } else {
      var nodes = this.treeNodes.filter((f) => f.parentId == dataItem.id);
      if (nodes.length > 0) {
        return of(nodes);
      } else {
        var newNodes = this.service.getModels(
          String.Format(this.modelChildrenUrl, dataItem.id)
        );
        newNodes.subscribe((res) => {
          this.treeNodes = [...this.treeNodes, ...res];
        });

        return newNodes;
      }
    }
  };

  public handleSelection(item: TreeItem): void {
    this.selectedItem = item.dataItem;
    this.parentId =
      this.selectedItem && this.selectedItem.id > 0
        ? this.selectedItem.id
        : undefined;
    this.currentFilter = undefined;
    this.selectedRows = [];
    this.pageIndex = 0;
    this.expandedKeys.push(item.dataItem.id);
    this.getParent();
    this.reloadGrid();

    this.getBreadCrumbItems();
  }

  public onNodeClick(e: any): void {
    if (e.type === "contextmenu") {
      const originalEvent = e.originalEvent;

      originalEvent.preventDefault();

      this.selectedContextmenu = e.item.dataItem;
      var leftPosition =
        this.CurrentLanguage == "fa"
          ? originalEvent.pageX - 135
          : originalEvent.pageX;

      if (this.selectedContextmenu.id == -1) {
        this.treeContextMenuLimited.show({
          left: leftPosition,
          top: originalEvent.pageY,
        });
      } else {
        this.treeContextMenu.show({
          left: leftPosition,
          top: originalEvent.pageY,
        });
      }
    }
  }

  public onSelectContextmenu({ item }): void {
    let hasPermission: boolean = false;

    switch (item.mode) {
      case "Remove": {
        hasPermission = this.isAccess(
          Entities.Account,
          AccountPermissions.Delete
        );
        if (hasPermission) this.contextMenuRemoveHandler();
        break;
      }
      case "Edit": {
        hasPermission = this.isAccess(
          Entities.Account,
          AccountPermissions.Edit
        );
        if (hasPermission) {
          this.contextMenuEditHandler();
          this.selectedContextmenu = undefined;
        }
        break;
      }
      case "New": {
        hasPermission = this.isAccess(
          Entities.Account,
          AccountPermissions.Create
        );
        if (hasPermission) {
          this.contextMenuAddNewHandler();
          this.selectedContextmenu = undefined;
        }
        break;
      }
      default:
    }

    if (!hasPermission)
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
  }

  contextMenuAddNewHandler() {
    this.selectedItem = this.selectedContextmenu;
    this.parentId =
      this.selectedItem && this.selectedItem.id > 0
        ? this.selectedItem.id
        : undefined;
    this.currentFilter = undefined;
    this.selectedRows = [];
    this.pageIndex = 0;
    this.expandedKeys.push(this.selectedContextmenu.id);
    this.selectedKeys = [];
    this.selectedKeys.push(this.selectedItem.id);
    this.reloadGrid();

    this.getBreadCrumbItems();

    if (this.parentId) {
      this.service
        .getById(String.Format(this.modelUrl, this.parentId))
        .subscribe((res) => {
          this.parent = res;
          this.addNew();
        });
    } else {
      this.parent = undefined;
      this.addNew();
    }
  }

  contextMenuEditHandler() {
    var parent = this.selectedContextmenu.parentId
      ? this.treeNodes.filter((f) => f.id == this.selectedContextmenu.parentId)
      : this.firstTreeNode;
    this.selectedItem = parent[0];
    this.parentId =
      this.selectedItem && this.selectedItem.id > 0
        ? this.selectedItem.id
        : undefined;
    this.currentFilter = undefined;
    this.selectedRows = [];
    this.pageIndex = 0;
    this.expandedKeys.push(this.selectedContextmenu.id);
    this.selectedKeys = [];
    this.selectedKeys.push(this.selectedItem.id);
    this.reloadGrid();

    this.getBreadCrumbItems();

    this.selectedRows.push(this.selectedContextmenu.id);

    if (this.parentId) {
      this.service
        .getById(String.Format(this.modelUrl, this.parentId))
        .subscribe((res) => {
          this.parent = res;

          this.editHandler();

          this.selectedRows = [];
        });
    } else {
      this.parent = undefined;
      this.editHandler();
      this.selectedRows = [];
    }
  }

  contextMenuRemoveHandler() {
    this.deleteConfirm = true;
    this.prepareDeleteConfirm(this.selectedContextmenu.name);
    this.deleteModelId = this.selectedContextmenu.id;
  }

  removeFromContextmenu() {
    this.grid.loading = true;
    this.service
      .delete(String.Format(this.modelUrl, this.deleteModelId))
      .subscribe(
        (response) => {
          this.showMessage(this.deleteMsg, MessageType.Info);

          if (this.selectedItem.id == this.deleteModelId) {
            if (this.selectedContextmenu.parentId == null) {
              this.selectedItem = this.firstTreeNode[0];
            } else {
              this.selectedItem = this.treeNodes.find(
                (f) => f.id == this.selectedContextmenu.parentId
              );
            }
            this.handleSelection({ dataItem: this.selectedItem, index: "0" });
            this.selectedKeys = [];
            this.selectedKeys.push(this.selectedItem.id);
          } else {
            if (
              this.selectedItem.id == this.selectedContextmenu.parentId ||
              (this.selectedContextmenu.parentId == null &&
                this.selectedItem.id == -1)
            ) {
              if (this.rowData.data.length == 1 && this.pageIndex > 1)
                this.pageIndex =
                  (this.pageIndex - 1) * this.pageSize - this.pageSize;
              this.reloadGrid();
            } else {
              this.parentId = this.selectedContextmenu.parentId;
            }

            this.getBreadCrumbItems();
          }

          this.refreshTreeNodes();

          this.deleteModelId = 0;
          this.selectedContextmenu = undefined;
          this.grid.loading = false;
        },
        (error) => {
          this.grid.loading = false;
          this.showMessage(
            this.errorHandlingService.handleError(error),
            MessageType.Warning
          );
        }
      );
  }

  getBreadCrumbItems() {
    this.breadCrumbList = [];
    this.getBreadCrumbRecursiveItems(this.selectedItem);
  }

  getBreadCrumbRecursiveItems(item: AccountItemBrief) {
    if (item.parentId != null) {
      var parent = this.treeNodes.filter((f) => f.id == item.parentId);
      if (parent.length > 0) this.getBreadCrumbRecursiveItems(parent[0]);
    } else {
      this.getBreadCrumbRecursiveItems(this.firstTreeNode[0]);
    }
    this.breadCrumbList.push(item);
  }

  selectBreadCrumb(item: AccountItemBrief) {
    if (item.id == -1) {
      this.treeScrollTop = 0;
    }
    this.handleSelection({ dataItem: item, index: "0" });
    this.selectedKeys = [];
    this.selectedKeys.push(this.selectedItem.id);
  }

  /**
   * یک ایتم به آرایه نودهای درخت اضافه یا  ویرایش یا حذف میکند و درخت را رفرش میکند
   * @param model
   */
  refreshTreeNodes(model?: any) {
    if (model) {
      var item = this.treeNodes.filter((f) => f.id == model.id);
      if (item.length > 0) {
        item[0].code = model.code;
        item[0].fullCode = model.fullCode;
        item[0].name = model.name;
      } else {
        if (
          model.parentId == null ||
          (model.parentId != null &&
            this.treeNodes.filter((f) => f.parentId == model.parentId).length >
              0)
        ) {
          this.treeNodes.push({
            id: model.id,
            name: model.name,
            parentId: model.parentId,
            fullCode: model.fullCode,
            code: model.code,
            childCount: model.childCount,
            isSelected: true,
            level: model.level,
          });
        }
        var parentItem = this.treeNodes.filter((f) => f.id == model.parentId);
        if (parentItem.length > 0) {
          parentItem[0].childCount++;
        }
      }

      var items = this.expandedKeys;
      this.expandedKeys = [];
      setTimeout(() => {
        this.expandedKeys = items;
      });
    } else {
      this.treeNodes = this.treeNodes.filter(
        (f) => f.parentId != this.parentId
      );
      var url = this.parentId
        ? String.Format(this.modelChildrenUrl, this.parentId)
        : this.environmentModelsLedgerUrl;
      this.service.getModels(url).subscribe((res) => {
        this.treeNodes = [...this.treeNodes, ...res];
        var parent = this.treeNodes.filter((f) => f.id == this.parentId);
        if (this.parentId) parent[0].childCount = res.length;
        var items = this.expandedKeys;
        this.expandedKeys = [];
        setTimeout(() => {
          this.expandedKeys = items;
        });
      });
    }
  }

  //#region grid
  reloadGrid(options?: ReloadOption) {
    //if (this.viewAccess) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }

    if (options && options.InsertedModel) this.goToLastPage(this.totalRecords);

    var parent_Id = this.parentId ? this.parentId.toString() : "null";
    filter = this.addFilterToFilterExpression(
      this.currentFilter,
      new Filter("ParentId", parent_Id, "== {0}", "System.Int32"),
      FilterExpressionOperator.And
    );

    this.currentFilter = filter;

    this.service
      .getAll(
        this.environmentModelsUrl,
        this.pageIndex,
        this.pageSize,
        this.sort,
        filter
      )
      .subscribe((res) => {
        var resData = res.body;

        var totalCount = 0;

        if (res.headers != null) {
          var headers = res.headers != undefined ? res.headers : null;
          if (headers != null) {
            var retheader = headers.get("X-Total-Count");
            if (retheader != null) totalCount = parseInt(retheader.toString());
          }
        }

        this.rowData = {
          data: resData,
          total: totalCount,
        };

        this.showloadingMessage = !(resData.length == 0);
        this.totalRecords = totalCount;
        this.grid.loading = false;
      });
    //}
    //else {
    //  this.rowData = {
    //    data: [],
    //    total: 0
    //  }
    //}
  }

  getParent() {
    if (this.parentId) {
      this.service
        .getById(String.Format(this.modelUrl, this.parentId))
        .subscribe((res) => {
          this.parent = res;
        });
    } else {
      this.parent = undefined;
    }
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  sortChange(sort: SortDescriptor[]): void {
    this.sort = sort.filter((f) => f.dir != undefined);
    this.reloadGrid();
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    var isReload: boolean = false;
    if (
      this.currentFilter &&
      this.currentFilter.children.length > filter.filters.length
    )
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
    if (this.selectedRows.length > 1) this.groupDelete = true;
    else this.groupDelete = false;
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {
    //this.dialogRef = this.dialogService.open({
    //  title: this.getEditorTitle(isNew),
    //  content: AccountFormComponent,
    //});
    //this.dialogModel = this.dialogRef.content.instance;
    //this.dialogModel.parent = this.parent;
    //this.dialogModel.model = this.editDataItem;
    //this.dialogModel.isNew = isNew;
    //this.dialogModel.errorMessages = undefined;
    //this.dialogRef.content.instance.save.subscribe((res) => {
    //  this.saveHandler(res, isNew);
    //});
    //const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
    //  this.dialogRef.close();
    //});
  }

  getEditorTitle(isNew: boolean): string {
    var editorTitle = "";

    this.treeConfig = this.getViewTreeSettings(this.viewId);
    if (this.treeConfig) {
      var level = this.parent ? this.parent.level + 2 : 1;
      var viewConfig = this.treeConfig.levels.find(
        (f) => f != null && f.no == level
      );

      this.levelConfig = viewConfig;

      if (viewConfig) editorTitle = viewConfig.name;
    }

    return String.Format(
      this.getText(
        isNew ? this.editorNewTitlePattern : this.editorEditTitlePattern
      ),
      editorTitle
    );
  }

  addNew() {
    this.grid.loading = true;
    this.service
      .getById(
        String.Format(this.modelNewChildUrl, this.parent ? this.parent.id : "0")
      )
      .subscribe(
        (res) => {
          this.editDataItem = res;
          this.openEditorDialog(true);

          this.grid.loading = false;
        },
        (error) => {
          this.grid.loading = false;
          this.showMessage(
            this.errorHandlingService.handleError(error),
            MessageType.Warning
          );
        }
      );
  }

  editHandler() {
    var recordId = this.selectedRows[0];

    this.grid.loading = true;
    this.service
      .getById(String.Format(this.modelUrl, recordId))
      .subscribe((res) => {
        this.editDataItem = res;
        this.openEditorDialog(false);

        this.grid.loading = false;
      });
  }

  removeHandler() {
    this.deleteConfirm = true;
    if (!this.groupDelete) {
      var recordId = this.selectedRows[0];
      var record = this.rowData.data.find((f) => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    } else {
      this.prepareDeleteConfirm(this.getText("Messages.SelectedItems"));
    }
  }

  subsetHandler() {
    var recordId = this.selectedRows[0];
    this.clickedRowItem = this.rowData.data.find((f) => f.id == recordId);
    this.rowDoubleClickHandler();
  }

  saveHandler(model: any, isNew: boolean) {
    this.grid.loading = true;
    if (!isNew) {
      this.service
        .edit<T>(String.Format(this.modelUrl, model.id), model)
        .subscribe(
          (response) => {
            this.editDataItem = undefined;
            this.showMessage(this.updateMsg, MessageType.Succes);

            this.dialogRef.close();
            this.dialogModel.parent = undefined;
            this.dialogModel.errorMessages = undefined;
            this.dialogModel.model = undefined;

            this.reloadGrid();

            this.refreshTreeNodes(model);
          },
          (error) => {
            this.editDataItem = model;
            this.dialogModel.errorMessages = error;
          }
        );
    } else {
      this.service.insert<T>(this.environmentModelsUrl, model).subscribe(
        (response: any) => {
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;

          this.dialogRef.close();
          this.dialogModel.parent = undefined;
          this.dialogModel.errorMessages = undefined;
          this.dialogModel.model = undefined;

          this.selectedRows = [];
          var options = new ReloadOption();
          options.InsertedModel = insertedModel;
          this.reloadGrid(options);

          this.refreshTreeNodes(insertedModel);
        },
        (error) => {
          this.dialogModel.errorMessages = error;
        }
      );
    }
    this.grid.loading = false;
  }

  deleteModel(confirm: boolean) {
    if (confirm) {
      //حذف گروهی از گرید
      if (this.groupDelete && !this.selectedContextmenu) {
        this.grid.loading = true;
        this.service
          .groupDelete(this.environmentModelsUrl, this.selectedRows)
          .subscribe(
            (res) => {
              this.showMessage(this.deleteMsg, MessageType.Info);

              if (
                this.rowData.data.length == this.selectedRows.length &&
                this.pageIndex > 1
              )
                this.pageIndex =
                  (this.pageIndex - 1) * this.pageSize - this.pageSize;

              this.selectedRows = [];
              this.groupDelete = false;
              this.reloadGrid();

              this.refreshTreeNodes();
            },
            (error) => {
              this.grid.loading = false;
              this.showMessage(
                this.errorHandlingService.handleError(error),
                MessageType.Warning
              );
            }
          );
      } else {
        //حذف یک سطر از گرید
        if (!this.selectedContextmenu) {
          this.grid.loading = true;
          this.service
            .delete(String.Format(this.modelUrl, this.deleteModelId))
            .subscribe(
              (response) => {
                this.deleteModelId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
                if (this.rowData.data.length == 1 && this.pageIndex > 1)
                  this.pageIndex =
                    (this.pageIndex - 1) * this.pageSize - this.pageSize;

                this.selectedRows = [];
                this.reloadGrid();

                this.refreshTreeNodes();
              },
              (error) => {
                this.grid.loading = false;
                this.showMessage(
                  this.errorHandlingService.handleError(error),
                  MessageType.Warning
                );
              }
            );
        } else {
          //حذف از منوی راست  کلیک
          this.removeFromContextmenu();
        }
      }
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }

  //treeScroll(nodeId: number) {
  //  let element = this.elRef.nativeElement.querySelector('#node-' + nodeId);
  //  this.treeScrollTop = element.getBoundingClientRect().top - 100;
  //}

  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  rowDoubleClickHandler() {
    this.selectedKeys = [];
    this.selectedKeys.push(this.clickedRowItem.id);

    if (this.clickedRowItem.childCount > 0) {
      var nodes = this.treeNodes.filter(
        (f) => f.parentId == this.clickedRowItem.id
      );
      if (nodes.length > 0) {
        this.expandedKeys.push(this.clickedRowItem.id);

        this.handleSelection({ dataItem: this.clickedRowItem, index: "0" });

        //this.treeScroll(this.clickedRowItem.id);
      } else {
        this.service
          .getModels(
            String.Format(this.modelChildrenUrl, this.clickedRowItem.id)
          )
          .subscribe((res) => {
            this.treeNodes = [...this.treeNodes, ...res];

            this.expandedKeys.push(this.clickedRowItem.id);
            var items = this.expandedKeys;
            this.expandedKeys = [];
            setTimeout(() => {
              this.expandedKeys = items;
            });

            this.handleSelection({ dataItem: this.clickedRowItem, index: "0" });

            //this.treeScroll(this.clickedRowItem.id);
          });
      }
    } else {
      this.handleSelection({ dataItem: this.clickedRowItem, index: "0" });

      //this.treeScroll(this.clickedRowItem.id);
    }
  }
  //#endregion
}
