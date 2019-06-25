import { Component, OnInit, OnDestroy, Renderer2, NgZone, ChangeDetectorRef, ViewChild, AfterViewInit, Output, EventEmitter, Input } from '@angular/core';
import { SettingService, GridService, VoucherService, VoucherLineService, Item, LookupService } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { Layout, Metadatas, Entities, MessageType, environment, SessionKeys } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DialogService } from '@progress/kendo-angular-dialog';
import { AutoGeneratedGridComponent } from '../../class/autoGeneratedGrid.component';
import { LookupApi, AccountApi } from '../../service/api/index';
import { ColumnBase } from '@progress/kendo-angular-grid';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
import { ViewName } from '../../security/viewName';
import { FilterExpression } from '../../class/filterExpression';
import { QuickSearchConfig } from '../../model/index';
import { Filter } from '../../class/filter';
import { String } from '../../class/source';


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
  @Output() result: EventEmitter<any> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();


  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public lookupService: LookupService,
    public settingService: SettingService, public ngZone: NgZone) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, cdref, ngZone);
  }

  ngOnInit() {
    this.title = this.getText('SelectForm.Title');
    this.getBaseEntities();
    this.cdref.detectChanges();
  }

  getBaseEntities() {
    this.lookupService.getModels(LookupApi.BaseEntityViews).subscribe(res => {
      this.baseEntitiesList = res;

      if (this.viewID)
        this.selectedEntity = this.viewID;
      else {
        if (sessionStorage.getItem(SessionKeys.SelectForm))
          this.selectedEntity = parseInt(sessionStorage.getItem(SessionKeys.SelectForm));
        else
          this.selectedEntity = 1;
      }
      
      this.changeEntity();
    })
  }


  changeEntity() {
    this.entityName = ViewName[this.selectedEntity];
    this.tempViewId = this.selectedEntity;

    var entity = this.baseEntitiesList.find(f => f.id == this.selectedEntity);
    this.getDataUrl = environment.BaseUrl + entity.searchUrl;

    this.rowData = undefined;

    sessionStorage.setItem(SessionKeys.SelectForm, this.selectedEntity.toString());
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


  reloadGrid(insertedModel?: any) {

    if (this.selectedEntity) {
      this.initQuickSearchFilter()

      if (this.getDataUrl) {
        this.grid.loading = true;

        if (this.totalRecords == this.skip && this.totalRecords != 0) {
          this.skip = this.skip - this.pageSize;
        }

        if (insertedModel)
          this.goToLastPage(this.totalRecords);

        var currentFilter = this.currentFilter;
        this.defaultFilter.forEach(item => {
          currentFilter = this.addFilterToFilterExpression(currentFilter,
            item, FilterExpressionOperator.And);
        })

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
        })

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
    this.title = String.Format(this.getText('SelectForm.EntityTitle'), entity.name, serchededItem.join(', '))
  }

  onCancel(): void {
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }

  onSelectRow() {
    this.result.emit({ dataItem: this.selectedRows[0], viewId: this.viewId });
  }
}


