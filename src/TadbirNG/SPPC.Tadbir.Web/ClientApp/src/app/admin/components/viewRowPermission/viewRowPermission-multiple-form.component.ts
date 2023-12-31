import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  Renderer2,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { RTL } from "@progress/kendo-angular-l10n";
import { TreeItem, TreeItemLookup } from "@progress/kendo-angular-treeview";
import {
  ItemInfo,
  ViewRowPermissionInfo,
  ViewRowPermissionService,
} from "@sppc/admin/service";
import { environment } from "@sppc/env/environment";
import {
  DetailComponent,
  Filter,
  FilterExpression,
  FilterExpressionBuilder,
  String,
} from "@sppc/shared/class";
import { Entities, Layout } from "@sppc/shared/enum/metadata";
import { BrowserStorageService, MetaDataService } from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "viewRowPermission-multiple-form-component",
  templateUrl: "./viewRowPermission-multiple-form.component.html",
  styles: [
    `
      ::ng-deep #multipleForm > .k-dialog {
        width: 800px;
      }
      @media screen and (max-width: 800px) {
        ::ng-deep #multipleForm > .k-dialog {
          width: 90%;
          min-width: 250px;
        }
      }
      #main-section {
        border: solid 1px #337ab7;
        border-radius: 3px;
        margin: 0;
      }
      #main-section > .col-sm-6 {
        padding: 0;
      }
      @media screen and (max-width: 768px) {
        #main-section > .col-sm-6 {
          float: unset !important;
        }
      }
      .section-header {
        border: solid 1px #337ab7;
        padding: 7px 10px;
      }
      .section-header .input-search-form {
        width: calc(100% - 200px);
      }
      .section-body {
        padding: 10px;
        border-right: solid 1px #337ab7;
        border-left: solid 1px #337ab7;
        height: 450px;
        overflow-y: scroll;
      }
      .header-label {
        margin: 6px 0 5px;
        display: block;
      }
      #frm-btn {
        margin-top: 15px;
      }
      .k-treeview {
        white-space: unset !important;
      }
    `,
  ],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class ViewRowPermissionMultipleFormComponent extends DetailComponent {
  roleItem: ItemInfo;

  noData: boolean = false;
  searchValue: string;
  fetchUrl: string;
  rowPermission: ViewRowPermissionInfo;

  entityName: string = "";

  public rowList: ItemInfo[] = [];
  public rowCheckedKeys: number[] = [];

  public selectedRowList: ItemInfo[] = [];
  public selectedRowKeys: number[] = [];

  //create properties
  @Input() public active: boolean = false;
  //@Input() public errorMessage: string = '';

  @Input() public set entity(item: ItemInfo) {
    if (item) {
      this.entityName = item.value;
    }
  }

  @Input() public set dataItem(item: ViewRowPermissionInfo) {
    this.rowList = [];
    this.rowCheckedKeys = [];
    this.selectedRowList = [];
    this.selectedRowKeys = [];

    if (item) {
      this.rowPermission = item;
      this.getFetchUrl().subscribe((res: any) => {
        this.fetchUrl = res.fetchUrl;
        this.onSearch();
      });
    }
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<number[]> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    this.noData = false;
    this.save.emit(this.selectedRowKeys);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.active = false;
    this.noData = false;
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }
  //Events

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public viewRowPermissionService: ViewRowPermissionService,
    public bStorageService: BrowserStorageService,
    public elem: ElementRef
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      Entities.RowAccess,
      undefined,
      elem
    );
  }

  getFetchUrl(): Observable<any> {
    return this.metadata
      .getMetaDataById(this.rowPermission.viewId)
      .pipe((res) => res);
  }

  public checkByKey(item: TreeItem) {
    return item.dataItem.key;
  }

  public handleCheckedChange(itemLookup: TreeItemLookup): void {
    var item = itemLookup.item.dataItem;
    
    if (this.selectedRowKeys.find((f) => f == item.key)) {
      var index = this.selectedRowList.findIndex((f) => f.key == item.key);
      if (index > -1) {
        var index2 = this.selectedRowList.findIndex((f) => f.key == item.key);
        if (index2 > -1) {
            this.selectedRowKeys.splice(index2, 1);
        }
        this.selectedRowList.splice(index, 1);
      }
    } else {
      this.selectedRowList.push(item);
      this.selectedRowKeys.push(item.key);
    }
  }

  public handleSelectedRowCheckedChange(itemLookup: TreeItemLookup): void {
    var item = itemLookup.item.dataItem;
    if (this.rowCheckedKeys.find((f) => f == item.key)) {
      var index = this.rowCheckedKeys.findIndex((f) => f == item.key);
      if (index > -1) {
        this.rowCheckedKeys.splice(index, 1);
      }
    }

    var index = this.selectedRowList.findIndex((f) => f.key == item.key);
    if (index > -1) {
      this.selectedRowList.splice(index, 1);
    }
  }

  onSearch() {
    if (this.fetchUrl) {
      let filterExp: FilterExpression | undefined;

      if (this.searchValue) {
        var filterExpBuilder = new FilterExpressionBuilder();
        filterExp = filterExpBuilder
          .New(
            new Filter(
              "Value",
              this.searchValue,
              ".Contains({0})",
              "System.String"
            )
          )
          .Build();
      }

      this.viewRowPermissionService
        .getRowList(environment.BaseUrl + this.fetchUrl, filterExp)
        .subscribe((res) => {
          this.rowList = res;
          for (let item of this.rowList) {
            if (
              this.rowPermission.items &&
              this.rowPermission.items.find((f) => f == item.key) &&
              !this.rowCheckedKeys.find((f) => f == item.key)
            ) {
              this.rowCheckedKeys.push(item.key);
              this.selectedRowList.push(item);
              this.selectedRowKeys.push(item.key);
            }
          }
        });
    } else this.noData = true;
  }

  /**
   * حذف فیلتر
   */
  removeFilter() {
    this.searchValue = "";
    this.onSearch();
  }

  removeAllSelected() {
    this.rowCheckedKeys = [];
    this.selectedRowKeys = [];
    this.selectedRowList = [];
  }

  selectAllRow() {
    this.removeAllSelected();
    for (let item of this.rowList) {
      this.rowCheckedKeys.push(item.key);
      this.selectedRowList.push(item);
      this.selectedRowKeys.push(item.key);
    }
  }

  getTitleText(text: string) {
    return String.Format(text, this.entityName);
  }
}
