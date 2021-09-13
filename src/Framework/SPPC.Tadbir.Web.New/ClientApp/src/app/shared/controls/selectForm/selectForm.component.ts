import { Component, OnInit, Renderer2, NgZone, ChangeDetectorRef, Output, EventEmitter, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { ColumnBase, SelectableSettings } from '@progress/kendo-angular-grid';
import { environment } from "@sppc/env/environment";
import { Layout, MessageType } from '@sppc/shared/enum/metadata';
import { MetaDataService, BrowserStorageService, GridService, LookupService } from '@sppc/shared/services';
import { DialogService } from '@progress/kendo-angular-dialog';
import { SettingService } from '@sppc/config/service';
import { AutoGeneratedGridComponent, String, FilterExpression, Filter, FilterExpressionOperator } from '@sppc/shared/class';
import { QuickSearchConfig } from '@sppc/shared/models';
import { LookupApi } from '@sppc/shared/services/api';
import { ViewName } from '@sppc/shared/security';
import { ReloadOption } from '@sppc/shared/class/reload-option';



export interface SearchItem {
  id: number;
  name: string;
  searchUrl: string;
}


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'sppc-selectForm',
  templateUrl: './selectForm.component.html',
  styles: [`
.search-box { margin-top: 5px; position: relative; }
.tRtl .search-box span { position: absolute; top: 4px; left: 4px; font-size: 22px; }
.tLtr .search-box span { position: absolute; top: 4px; right: 4px; font-size: 22px; }
.search-box span:hover { opacity:.5 }
/deep/.sppc-select-form > .k-grid { min-height:600px }
/deep/.sppc-select-form .k-grid-norecords td { vertical-align: top; }
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class SelectFormComponent extends AutoGeneratedGridComponent implements OnInit {

  tempViewId: number;
  baseEntitiesList: Array<SearchItem> = [];
  selectedEntity: number;
  searchValue: string;
  title: string;
    
  quickSearchFilter: FilterExpression;
  currentSetting: QuickSearchConfig;

  @Input() viewID: number;
  @Input() defaultCriteria: Filter;
  @Input() strSearch: string;
  @Input() isDisableEntities: boolean = false;
  @Output() result: EventEmitter<any> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  //0 means unlimited
  @Input() maxSelectionCount: number = 0;
  @Input() multipleSelect: boolean = false;
  @Input() allEntities: boolean = false;

  public selectableSettings: SelectableSettings = {    
    checkboxOnly: true    
  };

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public lookupService: LookupService,
    public settingService: SettingService, public bStorageService: BrowserStorageService, public ngZone: NgZone) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.title = this.getText('SelectForm.Title');
    this.getBaseEntities();
    this.cdref.detectChanges();

    this.selectableSettings.mode = this.multipleSelect ? 'multiple' : 'single';
  }

  getBaseEntities() {

    if (this.viewID)
      this.selectedEntity = this.viewID;
    else {     
      this.selectedEntity = 1;
    }

    var url = LookupApi.BaseEntityViews;
    if (this.allEntities) {      
      url = String.Format(LookupApi.EntityView, this.viewID);      
    }

    this.lookupService.getModels(url).subscribe(res => {
      this.baseEntitiesList = res;
      this.changeEntity();
    })
   
  }

  changeEntity() {
    this.entityName = ViewName[this.selectedEntity];
    this.tempViewId = this.selectedEntity;

    var entity = this.baseEntitiesList.find(f => f.id == this.selectedEntity);
    //get records by companyid
    if (entity.searchUrl.toLowerCase().indexOf('{companyid}')) {
      entity.searchUrl = String.Format(entity.searchUrl.replace('{companyid}', '{0}'), this.CompanyId);
    }

    this.getDataUrl = environment.BaseUrl + entity.searchUrl;
    this.selectedRows = [];
    this.rowData = undefined;

    //this.bStorageService.setSelectForm(this.selectedEntity.toString());
  }

  initQuickSearchFilter() {

    this.quickSearchFilter = undefined;

    if (this.searchValue) {

      var mode = "";
      switch (this.currentSetting.searchMode.toLowerCase()) {
        case "contains": {
          mode = ".Contains({0})";
          break;
        }
        case "startswith": {
          mode = ".StartsWith({0})";
          break;
        }
        case "endswith": {
          mode = ".EndsWith({0})";
          break;
        }
        default:
      }

      this.currentSetting.columns.forEach(item => {
        if (item.isSearched) {
          this.quickSearchFilter = this.addFilterToFilterExpression(this.quickSearchFilter,
            new Filter(item.name, this.searchValue, mode, "System.String"),
            FilterExpressionOperator.Or);
        }

      })
    }
  }

  reloadGrid(reloadOption?: ReloadOption) {    
    if (this.selectedEntity) {
      this.initQuickSearchFilter()

      if (this.getDataUrl) {
        this.grid.loading = true;

        if (this.totalRecords == this.skip && this.totalRecords != 0) {
          this.skip = this.skip - this.pageSize;
        }

        if (reloadOption && reloadOption.InsertedModel)
          this.goToLastPage(this.totalRecords);

        let currentFilter = undefined;
        if (this.currentFilter)
          currentFilter = JSON.parse(JSON.stringify(this.currentFilter));

        if (this.defaultFilter) {
          this.defaultFilter.forEach(item => {
            currentFilter = this.addFilterToFilterExpression(currentFilter,
              item, FilterExpressionOperator.And);
          })
        }

        if (this.quickSearchFilter) {
          if (currentFilter) {
            currentFilter.operator = FilterExpressionOperator.And;
            this.quickSearchFilter.children.push(currentFilter);
          }
        }
        else
          this.quickSearchFilter = currentFilter;

        this.gridService.getAll(this.getDataUrl, this.pageIndex, this.pageSize, this.sort, this.quickSearchFilter).subscribe((res) => {

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

          this.viewId = this.tempViewId;

          if (this.rowData && this.rowData.total > 0) {
            var columnsToFit: Array<ColumnBase> = new Array<ColumnBase>();
            this.grid.leafColumns.forEach(function (item) {
              var column = item as ColumnBase;
              if (column.width == undefined) {
                columnsToFit.push(column);
              }
            });
            this.fitColumns(columnsToFit);
          }
          this.showloadingMessage = !(resData.length == 0);
          this.totalRecords = totalCount;

          this.grid.loading = false;
        }, (err) =>
          {            
            this.showMessage(this.errorHandlingService.handleError(err), MessageType.Warning);                          
            this.grid.loading = false;
          }
        )

      }
    }

    this.cdref.detectChanges();
  }

  getCurrentSetting(event: QuickSearchConfig) {
    this.currentSetting = event;

    let serchededItem: string[] = [];
    this.currentSetting.columns.forEach(item => {
      if (item.isSearched)
        serchededItem.push(item.title);
    })

    var entity = this.baseEntitiesList.find(f => f.id == this.selectedEntity);
    this.title = String.Format(this.getText('SelectForm.EntityTitle'), entity.name, serchededItem.join(', '));

    this.defaultReloadGrid();
  }

  onCancel(): void {
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }  

  onSelectRow() {
    if (!this.multipleSelect) {      
      var id = this.selectedRows[0];
      var item = this.rowData.data.find(p => p.id === id);
      this.result.emit({ dataItem: item, viewId: this.viewId });
    }
    else {
      if (this.selectedRows.length > this.maxSelectionCount) {
        this.showMessage(this.getText('SelectForm.MaxSelectionCount').replace('{0}', this.maxSelectionCount.toString()));
        return;
      }

      var items: Array<any> = [];
      this.selectedRows.forEach((it) => {
      //  //var item = this.rowData.data.find(p => p.id === it);
        items.push(it);        
      });
      this.result.emit({ dataItem: this.selectedRows, viewId: this.viewId });
    }
  }

  rowDoubleClickHandler() {
    if (!this.multipleSelect) {
      if (this.selectedRows.length == 1) {
        var id = this.selectedRows[0];
        var item = this.rowData.data.find(p => p.id === id);
        this.result.emit({ dataItem: item, viewId: this.viewId });
      }
    }
  }

  defaultReloadGrid() {
    if (this.strSearch) {
      this.searchValue = this.strSearch;
      this.reloadGrid();
    }
  }
}


