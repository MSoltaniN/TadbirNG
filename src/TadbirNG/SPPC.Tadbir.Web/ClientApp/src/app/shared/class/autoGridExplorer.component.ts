import {
  ChangeDetectorRef,
  ElementRef,
  Inject,
  Injectable,
  NgZone,
  OnInit,
  Optional,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { GridDataResult } from "@progress/kendo-angular-grid";
import { ContextMenuComponent } from "@progress/kendo-angular-menu";
import { TreeItem } from "@progress/kendo-angular-treeview";
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { ViewTreeConfig, ViewTreeLevelConfig } from "@sppc/config/models";
import { SettingService } from "@sppc/config/service/settings.service";
import { AccountItemBrief } from "@sppc/finance/models";
import { ServiceLocator } from "@sppc/service.locator";
import { FilterExpression } from "@sppc/shared/class/filterExpression";
import { ErrorListComponent } from "@sppc/shared/components/errorList/errorList.component";
import { MessageType } from "@sppc/shared/enum/metadata";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { GridService } from "@sppc/shared/services/grid.service";
import { MetaDataService } from "@sppc/shared/services/metadata.service";
import { ToastrService } from "ngx-toastr";
import { Observable, of } from "rxjs";
import { map } from "rxjs/operators";
import { ReloadStatusType } from "../enum";
import { OperationId } from "../enum/operationId";
import { ErrorHandlingService } from "../services";
import { AutoGeneratedGridComponent } from "./autoGeneratedGrid.component";
import { Filter } from "./filter";
import { FilterExpressionOperator } from "./filterExpressionOperator";
import { ReloadOption } from "./reload-option";
import { String } from "./source";

@Injectable()
export class AutoGridExplorerComponent<T>
  extends AutoGeneratedGridComponent
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

  @ViewChild("treemenu") public treeContextMenu: ContextMenuComponent;
  public contextmenuItems: any[] = [
    { text: "Buttons.New", icon: "file-add", mode: "New" },
    { text: "Buttons.Edit", icon: "edit", mode: "Edit" },
    { text: "Buttons.Delete", icon: "delete", mode: "Remove" },
  ];
  @ViewChild("treemenulimited")
  public treeContextMenuLimited: ContextMenuComponent;
  public contextmenuLimitedItems: any[] = [
    { text: "Buttons.New", icon: "file-add", mode: "New" },
  ];
  selectedContextmenu: any;

  parentId: number;
  parent: any;
  editDataItem: any;

  clickedRowItem: any = undefined;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public service: GridService,
    public dialogService: DialogService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    @Optional() @Inject("empty") public entity: string,
    @Optional() @Inject("empty") public parentTitlekey: string,
    @Optional() @Inject("empty") public editorNewTitlePattern: string,
    @Optional() @Inject("empty") public editorEditTitlePattern: string,
    @Optional() @Inject("empty") public environmentModelsUrl: string,
    @Optional() @Inject("empty") public environmentModelsLedgerUrl: string,
    @Optional() @Inject("empty") public modelUrl: string,
    @Optional() @Inject("empty") public modelChildrenUrl: string,
    @Optional() @Inject("empty") public modelNewChildUrl: string,
    public cdref: ChangeDetectorRef,
    public ngZone: NgZone,
    public elem: ElementRef,
    @Optional()
    @Inject(ErrorHandlingService)
    public errorHandlingService?: ErrorHandlingService
  ) {
    super(
      toastrService,
      translate,
      service,
      renderer,
      metadata,
      settingService,
      bStorageService,
      cdref,
      ngZone,
      elem
    );

    this.errorHandlingService =
      ServiceLocator.injector.get(ErrorHandlingService);

    if (elem) {
      this.selector = elem.nativeElement.tagName.toLowerCase();
    }
  }

  ngOnInit() {}

  getTreeNode() {
    this.treeNodes = [];
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

    //comment by nouri : با خالی کردن این متغییر فیلتر های گرید نادیده گرفته میشد و کامنت شد
    //this.currentFilter = undefined;
    this.selectedRows = [];
    this.pageIndex = 0;
    this.expandedKeys.push(item.dataItem.id);
    this.getParent();

    //log is off
    this.listChanged = false;
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
    //log is off
    this.listChanged = false;
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
    //log is off
    this.listChanged = false;
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
              //log is off after delete model
              this.listChanged = false;
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
    // to Auto Scroll in treeNodes section
    this.scrollToSelectedItem(item);
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

  filterChange(filter: CompositeFilterDescriptor): void {
    this.listChanged = false;
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

  reloadGrid(options?: ReloadOption) {
    this.grid.loading = true;

    let filter = undefined;
    if (this.currentFilter)
      filter = JSON.parse(JSON.stringify(this.currentFilter));

    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }

    if (options && options.InsertedModel) this.goToLastPage(this.totalRecords);

    if (
      options &&
      (options.Status == ReloadStatusType.AfterFilter ||
        options.Status == ReloadStatusType.None)
    ) {
      this.skip = 0;
    }

    var parent_Id = this.parentId ? this.parentId.toString() : "null";
    filter = this.addFilterToFilterExpression(
      filter,
      parent_Id
        ? new Filter("ParentId", parent_Id, "== {0}", "System.Int32")
        : new Filter("ParentId", "", "== null", ""),
      FilterExpressionOperator.And
    );

    if (this.defaultFilter && this.defaultFilter.length > 0) {
      this.defaultFilter.forEach((item) => {
        filter = this.addFilterToFilterExpression(
          filter,
          item,
          FilterExpressionOperator.And
        );
      });

      if (this.currentFilter) {
        filter = this.andTwoFilterExpression(filter, this.currentFilter);
      }

      if (this.useCustomFilterExpression && this.customFilter) {
        filter = this.andTwoFilterExpression(filter, this.customFilter);
      }
    }

    var filterExp: FilterExpression;
    if (this.quickFilter) {
      this.quickFilter.forEach((item) => {
        filterExp = this.addFilterToFilterExpression(
          filterExp,
          item,
          FilterExpressionOperator.And
        );
      });
    }

    //this code for concat filters to advanceFilters
    if (this.advanceFilters)
      filter = this.andFilterToFilterExpression(filter, this.advanceFilters);

    //comment by nouri because duplicate (parentid == value) added to currentfilter
    //this.currentFilter = filter;
    //برای اینکه مقدار نهایی فیلتر به گزارش فوری پاس داده شود این متغیر مقداردهی میشود
    this.reportFilter = filter;

    //log is off if child item selected
    if (this.parentId) this.listChanged = false;

    if (this.operationId == OperationId.None)
      this.operationId = this.getOperationId(options);

    this.service
      .getAll(
        this.getDataUrl,
        this.pageIndex,
        this.pageSize,
        this.sort,
        filter,
        undefined,
        this.listChanged,
        this.operationId
      )
      .subscribe((res) => {
        var resData = res.body;

        var totalCount = 0;

        this.operationId = OperationId.None;

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

        this.listChanged = true;
      });
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

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {}

  /**
   * برای جستجو در بین لیست درختی
   * @param value مقدار مورد جستجو
   */
  public filterTreeNodes(value: string): void {
    if (value != "") {
    this.firstTreeNode = this.search(this.treeNodes, value);
    } else {
      this.getTreeNode();
    }
  }

  private contains(text: string, term: string): boolean {
    // return text.toLowerCase().indexOf((term || "").toLowerCase()) >= 0;
    return text.toLowerCase().trim().match(term.toLowerCase().trim())?true:false;
  }

  private search(items: any[], term: string): any[] {
    return items.reduce((acc, item) => {
      if (this.contains(item.name, term)) {
        acc.push(item);
      } else if (this.hasChildren(item)) {
        const newItems:any = this.fetchChildren(item);

        if (newItems.length > 0) {
          acc = [...newItems];
        }
      }

      return acc;
    }, []);
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
    var recordId = this.selectedRows[0]; //.id;

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
    if (!this.groupOperation) {
      var recordId = this.selectedRows[0]; //.id;
      var record = this.rowData.data.find((f) => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    } else {
      this.prepareDeleteConfirm(this.getText("Messages.SelectedItems"));
    }
  }

  subsetHandler() {
    var recordId = this.selectedRows[0]; //.id;
    this.clickedRowItem = this.rowData.data.find((f) => f.id == recordId);
    this.rowDoubleClickHandler();
  }

  saveHandler(model: any, isNew: boolean) {
    this.grid.loading = true;
    this.service.submitted.next(true)
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
            //log is off after update model
            this.listChanged = false;
            this.reloadGrid();
            this.selectedRows = [];
            this.highLightNewRow(model);

            this.refreshTreeNodes(model);
            this.service.submitted.next(false)
          },
          (error) => {
            this.service.submitted.next(false)
            this.editDataItem = model;
            this.dialogModel.errorMessages =
              this.errorHandlingService.handleError(error);
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
          //log is off after update insert
          this.listChanged = false;

          var options = new ReloadOption();
          options.Status = ReloadStatusType.AfterDelete;
          this.reloadGrid(options);
          this.highLightNewRow(model);

          this.refreshTreeNodes(insertedModel);
          this.service.submitted.next(false)
        },
        (error) => {
          this.service.submitted.next(false)

          this.dialogModel.errorMessages =
            this.errorHandlingService.handleError(error);
        }
      );
    }
    this.grid.loading = false;
  }

  deleteModel(confirm: boolean) {
    if (confirm) {
      //حذف گروهی از گرید
      if (this.groupOperation && !this.selectedContextmenu) {
        this.grid.loading = true;

        let rowsId: Array<number> = [];

        this.selectedRows.forEach((item) => {
          rowsId.push(item /*.id*/);
        });

        this.service.groupDelete(this.environmentModelsUrl, rowsId).subscribe(
          (res) => {
            var data: any = res;

            //if (data && data.length > 0) {
            //  //show errorlist component
            //  this.openErrorListDialog(data, rowsId.length);
            //}
            //else {
            //  this.showMessage(this.deleteMsg, MessageType.Info);

            //  var pageIndexDecrease = false;
            //  if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
            //    pageIndexDecrease = true;

            //  var pageCount = Math.floor((this.rowData.total - this.selectedRows.length) / this.pageSize) + 1;
            //  if (this.pageIndex > 0 && this.pageIndex > pageCount)
            //    pageIndexDecrease = true;

            //  if (pageIndexDecrease)
            //    this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;
            //}

            //this.selectedRows = [];
            //this.groupOperation = false;
            ////log is off after delete model
            //this.listChanged = false;
            //var options = new ReloadOption();
            //options.Status = ReloadStatusType.AfterDelete;
            //this.reloadGrid(options);

            this.afterGroupDelete(data, rowsId);

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
                //this.deleteModelId = 0;
                //this.showMessage(this.deleteMsg, MessageType.Info);
                //if (this.rowData.data.length == 1 && this.pageIndex > 1)
                //  this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

                //this.selectedRows = [];
                ////log is off after delete model
                //this.listChanged = false;
                //this.reloadGrid();
                this.afterDelete();

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

  afterGroupDelete(data, rowsId) {
    if (data && data.length > 0) {
      //show errorlist component
      this.openErrorListDialog(data, rowsId.length);
    } else {
      this.showMessage(this.deleteMsg, MessageType.Info);

      var pageIndexDecrease = false;
      if (
        this.rowData.data.length == this.selectedRows.length &&
        this.pageIndex > 1
      )
        pageIndexDecrease = true;

      var pageCount =
        Math.floor(
          (this.rowData.total - this.selectedRows.length) / this.pageSize
        ) + 1;
      if (this.pageIndex > 0 && this.pageIndex > pageCount)
        pageIndexDecrease = true;

      if (pageIndexDecrease)
        this.pageIndex = (this.pageIndex - 1) * this.pageSize - this.pageSize;
    }

    this.selectedRows = [];
    this.groupOperation = false;
    //log is off after delete model
    this.listChanged = false;
    var options = new ReloadOption();
    options.Status = ReloadStatusType.AfterDelete;
    this.reloadGrid(options);
  }

  openErrorListDialog(rowData: any[], total: number) {
    this.dialogRef = this.dialogService.open({
      title: this.getText("ErrorList.GroupOperationReport"),
      content: ErrorListComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.rowData = rowData;
    this.dialogModel.totalItems = total;

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();
      }
    );
  }

  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  rowDoubleClickHandler() {
    this.selectedKeys = [];
    if (this.clickedRowItem) {
      this.selectedKeys.push(this.clickedRowItem.id);

      if (this.clickedRowItem.childCount > 0) {
        var nodes = this.treeNodes.filter(
          (f) => f.parentId == this.clickedRowItem.id
        );
        if (nodes.length > 0) {
          this.expandedKeys.push(this.clickedRowItem.id);

          this.handleSelection({ dataItem: this.clickedRowItem, index: "0" });
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

              this.handleSelection({
                dataItem: this.clickedRowItem,
                index: "0",
              });
            });
        }
      } else {
        this.handleSelection({ dataItem: this.clickedRowItem, index: "0" });
      }
    }
  }

  public getExportData(): Observable<GridDataResult> {
    if (!this.exportAccessed) {
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
      return of();
    }

    if (this.getDataUrl) {
      let currentFilter = undefined;
      if (this.currentFilter)
        currentFilter = JSON.parse(JSON.stringify(this.currentFilter));

      var parent_Id = this.parentId ? this.parentId.toString() : "null";
      currentFilter = this.addFilterToFilterExpression(
        currentFilter,
        parent_Id
          ? new Filter("ParentId", parent_Id, "== {0}", "System.Int32")
          : new Filter("ParentId", "", "== null", ""),
        FilterExpressionOperator.And
      );

      if (this.defaultFilter && this.defaultFilter.length > 0) {
        this.defaultFilter.forEach((item) => {
          currentFilter = this.addFilterToFilterExpression(
            currentFilter,
            item,
            FilterExpressionOperator.And
          );
        });

        if (this.useCustomFilterExpression && this.customFilter) {
          currentFilter = this.andTwoFilterExpression(
            currentFilter,
            this.customFilter
          );
        }
      }

      var filterExp: FilterExpression;
      if (this.quickFilter) {
        this.quickFilter.forEach((item) => {
          filterExp = this.addFilterToFilterExpression(
            filterExp,
            item,
            FilterExpressionOperator.And
          );
        });
      }

      //this code for concat filters to advanceFilters
      if (this.advanceFilters)
        currentFilter = this.andFilterToFilterExpression(
          currentFilter,
          this.advanceFilters
        );

      var filter = currentFilter;

      return this.service
        .getAll(
          this.getDataUrl,
          1,
          1000000,
          this.sort,
          filter,
          filterExp,
          true,
          OperationId.Export
        )
        .pipe(
          map(
            (response) =>
              <GridDataResult>{
                data: response.body.items ? response.body.items : response.body,
                total: response.body.items
                  ? response.body.items.length
                  : response.body.length,
              }
          )
        );
    } else {
      this.showMessage(this.getText("App.PleaseLoadData"));
    }
  }

  public allData = (): Observable<GridDataResult> => {
    this.excelFileName = this.getExcelFileName();
    return this.getExportData();
  };
}
