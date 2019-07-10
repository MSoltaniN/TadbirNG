import { Component, OnInit, Input, Renderer2, Optional, Host, SkipSelf, ViewChild } from '@angular/core';
import { CompanyService, CompanyInfo, CompanyDbInfo, SettingService } from '../../service/index';
import { Company, CompanyDb } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { CompanyApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { CompanyPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { ViewName } from '../../security/viewName';
import { BrowserStorageService } from '../../service/browserStorage.service';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'company',
  templateUrl: './company.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class CompanyComponent extends DefaultComponent implements OnInit {

  //#region Fields
  @ViewChild(GridComponent) grid: GridComponent;

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

  editDataItem?: CompanyDb = undefined;
  isNew: boolean;
  errorMessage: string;
  groupDelete: boolean = false;
  addToContainer: boolean = false;

  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.Company, CompanyPermissions.View);
    this.reloadGrid();
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
    this.companyService.getById(String.Format(CompanyApi.Company, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.grid.loading = false;
    })
    this.isNew = false;
    this.errorMessage = '';
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.errorMessage = '';
  }

  public saveHandler(model: CompanyDb) {
    this.grid.loading = true;
    if (!this.isNew) {
      this.isNew = false;
      this.companyService.edit<CompanyDb>(String.Format(CompanyApi.Company, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
        }, (error => {
          this.editDataItem = model;
          this.errorMessage = error;
          this.grid.loading = true;
        }));
    }
    else {
      this.companyService.insert<CompanyDb>(CompanyApi.Companies, model)
        .subscribe((response: any) => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;
          this.reloadGrid(insertedModel);
        }, (error => {
          this.isNew = true;
          this.errorMessage = error;
          this.grid.loading = true;
        }));
    }
  }

  //#endregion

  //#region Constructor

  constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService, public bStorageService: BrowserStorageService,
    private companyService: CompanyService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.Company, ViewName.Company);
  }

  //#endregion

  //#region Methods

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: Company) {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      var url = CompanyApi.Companies;
      this.companyService.getAll(url, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {

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
      if (this.groupDelete) {
        //حذف گروهی
      }
      else {

        this.grid.loading = true;
        this.companyService.delete(String.Format(CompanyApi.Company, this.deleteModelId)).subscribe(response => {
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

  public addNew(parentModelId?: number, addToThis?: boolean) {
    this.isNew = true;
    this.editDataItem = new CompanyDbInfo();

    this.errorMessage = '';
  }

  public showOnlyParent(dataItem: Company, index: number): boolean {
    return dataItem.childCount > 0;
  }

  //#endregion
}


