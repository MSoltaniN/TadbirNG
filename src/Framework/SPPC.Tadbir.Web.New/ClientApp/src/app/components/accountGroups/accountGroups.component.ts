import { Component, OnInit, Input, Renderer2, Optional, Host, SkipSelf, ViewChild } from '@angular/core';
import { AccountGroupsService, AccountGroupInfo, SettingService } from '../../service/index';
import { AccountGroup } from '../../model/index';
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
import { AccountGroupApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { AccountGroupPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'accountGroups',
  templateUrl: './accountGroups.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class AccountGroupsComponent extends DefaultComponent implements OnInit {

  //#region Fields
  @ViewChild(GridComponent) grid: GridComponent;

  public rowData: GridDataResult;
  public selectedRows: string[] = [];
  public totalRecords: number;

  //permission flag
  viewAccess: boolean;

  ////for add in delete messageText
  deleteConfirm: boolean;
  deleteModelsConfirm: boolean;
  deleteModelId: number;

  currentFilter: FilterExpression;
  currentOrder: string = "";
  public sort: SortDescriptor[] = [];

  showloadingMessage: boolean = true;

  editDataItem?: AccountGroup = undefined;
  isNew: boolean;
  errorMessage: string;
  groupDelete: boolean = false;
  addToContainer: boolean = false;

  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.AccountGroup, AccountGroupPermissions.View);
    this.reloadGrid();
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
  }

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

  public editHandler(arg: any) {
    this.grid.loading = true;
    this.accountGroupsService.getById(String.Format(AccountGroupApi.AccountGroup, arg.dataItem.id)).subscribe(res => {
      this.editDataItem = res;
      this.grid.loading = false;
    })
    this.grid.loading = false;
    this.isNew = false;
    this.errorMessage = '';
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.errorMessage = '';
  }

  public saveHandler(model: AccountGroup) {
    this.grid.loading = true;
    if (!this.isNew) {
      this.isNew = false;
      this.accountGroupsService.edit<AccountGroup>(String.Format(AccountGroupApi.AccountGroup, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
        }, (error => {
          this.editDataItem = model;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
    else {
      this.accountGroupsService.insert<AccountGroup>(AccountGroupApi.AccountGroups, model)
        .subscribe((response: any) => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;
          this.reloadGrid(insertedModel);
        }, (error => {
          this.isNew = true;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
  }

  //#endregion

  //#region Constructor

  constructor(public toastrService: ToastrService, public translate: TranslateService,
    private accountGroupsService: AccountGroupsService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.AccountGroup, Metadatas.AccountGroup);
  }

  //#endregion

  //#region Methods
  showConfirm() {
    this.deleteModelsConfirm = true;
  }

  deleteModels(confirm: boolean) {
    if (confirm) {
      //this.sppcLoading.show();
    }

    this.groupDelete = false;
    this.deleteModelsConfirm = false;
  }

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: AccountGroup) {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
      var order = this.currentOrder;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      var url = AccountGroupApi.AccountGroups;
      this.accountGroupsService.getAll(url, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

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

  deleteModel(confirm: boolean) {
    if (confirm) {
      this.grid.loading = true;
      this.accountGroupsService.delete(String.Format(AccountGroupApi.AccountGroup, this.deleteModelId)).subscribe(response => {
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

  public addNew(parentModelId?: number, addToThis?: boolean) {
    this.isNew = true;
    this.editDataItem = new AccountGroupInfo();

    this.errorMessage = '';
  }

  //public showOnlyParent(dataItem: AccountGroup, index: number): boolean {
  //  return dataItem.childCount > 0;
  //}

  //#endregion
}

