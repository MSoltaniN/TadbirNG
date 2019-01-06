import { Component, OnInit, Input, Renderer2, Optional, Host, SkipSelf, ViewChild } from '@angular/core';
import { BranchService, BranchInfo, RelatedItemsInfo, SettingService } from '../../service/index';
import { Branch, RelatedItems } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
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
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { BranchApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { BranchPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'branch',
  templateUrl: './branch.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class BranchComponent extends DefaultComponent implements OnInit {

  //#region Fields
  public Childrens: Array<BranchComponent>;
  @ViewChild(GridComponent) grid: GridComponent;

  @Input() public parent: Branch;
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

  showloadingMessage: boolean = true;
  rolesList: boolean = false;

  editDataItem?: Branch = undefined;
  branchRolesData: RelatedItemsInfo;
  isNew: boolean;
  errorMessage: string;
  groupDelete: boolean = false;

  addToContainer: boolean = false;


  parentTitle: string = '';
  parentValue: string = '';

  isChildExpanding: boolean;
  componentParentId: number;
  goLastPage: boolean;
  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.Branch, BranchPermissions.View);

    if (this.parentComponent && this.parentComponent.isChildExpanding) {
      this.goLastPage = true;
      this.parentComponent.isChildExpanding = false;
    }

    this.reloadGrid();
    if (this.parentComponent) {
      this.parentComponent.addChildComponent(this);
      this.parentId = this.parent.id;
      this.componentParentId = this.parentId;
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
    this.branchService.getById(String.Format(BranchApi.Branch, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.setTitle(res.parentId);

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

  public saveHandler(model: Branch) {
    model.companyId = this.CompanyId;

    if (!this.isNew) {
      this.isNew = false;
      this.branchService.edit<Branch>(String.Format(BranchApi.Branch, model.id), model)
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
      //set parentid for childs accounts
      if (this.parentId) {
        model.parentId = this.parentId;

        //var currentLevel = this.parent ? this.parent.level : 0;
        var parentCom = this.parentComponent;
        var currentLevel = 0;

        while (parentCom) {
          currentLevel++;
          parentCom = parentCom.parentComponent
        }

        model.level = currentLevel + 1;

        this.parentId = undefined;
      }
      else if (this.parent) {
        model.parentId = this.parent.id;
        model.level = this.parent.level + 1;
      }
      this.branchService.insert<Branch>(BranchApi.Branches, model)
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

  public rolesHandler(branchId: number) {
    this.rolesList = true;
    this.branchService.getBranchRoles(branchId).subscribe(res => {
      this.branchRolesData = res;
    });

    this.errorMessage = '';
  }

  public cancelBranchRolesHandler() {
    this.rolesList = false;
    this.errorMessage = '';
  }

  public saveBranchRolesHandler(userRoles: RelatedItems) {
    this.branchService.modifiedBranchRoles(userRoles)
      .subscribe(response => {
        this.rolesList = false;
        this.showMessage(this.getText("Branch.UpdateRoles"), MessageType.Succes);
      }, (error => {
        this.errorMessage = error;
      }));
  }
  //#endregion

  //#region Constructor

  constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
    private branchService: BranchService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService,
    @SkipSelf() @Host() @Optional() private parentComponent: BranchComponent) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Branch, Metadatas.Branch);
  }

  //#endregion

  //#region Methods

  /**
   * کامپوننت های فرزند را در متغیری اضافه میکند
   * @param branchComponent کامپوننت شعبه
   */
  public addChildComponent(branchComponent: BranchComponent) {

    if (this.Childrens == undefined) this.Childrens = new Array<BranchComponent>();
    if (this.Childrens.findIndex(p => p.parent.id === branchComponent.parent.id) == -1)
      this.Childrens.push(branchComponent);

  }


  reloadGridEvent() {
    this.reloadGrid();
  }

  public reloadGrid(insertedModel?: Branch) {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
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
      if (this.parentComponent != null && (this.goLastPage || (insertedModel && !this.addToContainer))) {

        //call top 1 for get totalcount
        this.branchService.getAll(String.Format(BranchApi.CompanyBranches, this.CompanyId),
          0, 1, this.sort, filter).subscribe((res) => {
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

            this.loadGridData(insertedModel, filter);
          });
      }
      //#endregion
      else {
        if (insertedModel && this.addToContainer)
          this.goToLastPage(this.totalRecords);

        this.loadGridData(insertedModel, filter);
      }
    }
    else {
      this.rowData = {
        data: [],
        total: 0
      }
    }
  }

  loadGridData(insertedModel?: Branch, filter?: FilterExpression) {

    this.branchService.getAll(String.Format(BranchApi.CompanyBranches, this.CompanyId),
      this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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
          var rows = (this.rowData.data as Array<Branch>);
          var index = rows.findIndex(p => p.id == insertedModel.parentId);
          if (index == -1 && this.parentComponent != null) {
            var rows = (this.parentComponent.rowData.data as Array<Branch>);
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
        //حذف گروهی
      }
      else {

        this.grid.loading = true;
        this.branchService.delete(String.Format(BranchApi.Branch, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);
          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.selectedRows = [];
          this.reloadGrid();
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

  private setTitle(parentModelId?: number) {
    if (parentModelId != undefined) {

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
        var level = +parentRow.level;
        this.parentTitle = this.getText("App.Level") + " " + (level + 2).toString();
        this.parentValue = parentRow.name;
      }
    }
    else if (this.parent != undefined) {
      this.parentTitle = this.getText("App.Level") + " " + (this.parent.level + 2).toString();
      this.parentValue = this.parent.name;
    }
    else {
      this.parentTitle = '';
      this.parentValue = '';
    }

  }

  public addNew(parentModelId?: number, addToThis?: boolean) {
    this.isNew = true;
    this.editDataItem = new BranchInfo();
    this.setTitle(parentModelId);

    if (parentModelId)
      this.parentId = parentModelId;

    if (addToThis)
      this.addToContainer = addToThis;
    else
      this.addToContainer = false;

    this.errorMessage = '';
  }

  public showOnlyParent(dataItem: Branch, index: number): boolean {
    return dataItem.childCount > 0;
  }

  public checkShow(dataItem: Branch) {
    return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
  }

  //#endregion

}


