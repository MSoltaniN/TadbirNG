import { DefaultComponent } from '@sppc/shared/class/default.component';
import { Injectable, OnDestroy, Renderer2, ChangeDetectorRef, NgZone, EventEmitter, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { GridService, BrowserStorageService, MetaDataService } from '../services';
import { SettingService } from '@sppc/config/service';
import { ServiceLocator } from "@sppc/service.locator";
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { AdvanceFilterComponent } from "@sppc/shared/components/advanceFilter/advance-filter.component";
import { Permissions, GlobalPermissions } from "@sppc/shared/security/permissions";
import { FilterExpression } from '@sppc/shared/class/filterExpression';
import { FilterRow } from "@sppc/shared/models";
import { MessageType } from '@sppc/env/environment';
import * as moment from 'jalali-moment';

@Injectable()
export class ListComponent extends DefaultComponent implements OnDestroy {

  public advanceFilters: FilterExpression;
  public advanceFilterList: Array<FilterRow>;
  public selectedGroupFilter: number;

  dialogService: DialogService;
  permission: Permissions;
  filterDialogRef: DialogRef;
  excelFileName: string;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService,
    public renderer: Renderer2, public metadataService: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, bStorageService, renderer, metadataService, settingService, '', undefined);

    this.dialogService = ServiceLocator.injector.get(DialogService);
    this.permission = new Permissions();    
  }
 

  showAdvanceFilterComponent(viewId: number, onOk: EventEmitter<any>, onCancel: EventEmitter<any>) {    
    (async () => {
      var entityName = await this.getEntityName(viewId);
      var code = <number>GlobalPermissions.Filter
      if (!this.isAccess(entityName, code)) {
        this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
        return;
      }

      this.filterDialogRef = this.dialogService.open({
        content: AdvanceFilterComponent,
        title: this.getText('AdvanceFilter.Title')
      });

      var filterDialogModel = <AdvanceFilterComponent>this.filterDialogRef.content.instance;
      if (this.advanceFilterList) {
        filterDialogModel.filters = this.advanceFilterList;
        filterDialogModel.gFilterSelected = this.selectedGroupFilter;
      }
      filterDialogModel.viewId = viewId;
      this.filterDialogRef.content.instance.cancel.subscribe((res) => {
        this.filterDialogRef.close();
        onCancel.emit();
      });

      this.filterDialogRef.content.instance.result.subscribe((res) => {
        this.advanceFilters = res.filters;
        this.advanceFilterList = res.filterList;
        this.selectedGroupFilter = res.gFilterSelected;
        this.filterDialogRef.close();
        onOk.emit();
      });

    })();
  }

  showReportManager(viewId: number, parent: any, reportSetting: any, reportManager: any) {     
    (async () => {
      var entityName = await this.getEntityName(viewId);
      var code = <number>GlobalPermissions.Print
      if (!this.isAccess(entityName, code)) {
        this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
        return;
      }

      if (this.validateReport(parent)) {
        if (!reportManager.directShowReport()) {
          this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
          reportSetting.showReportSetting(parent.gridColumns, parent.entityTypeName, this.viewId, reportManager);
        }
      }     
    })(); 
  }

  showQuickReportSetting(viewId: number, parent: any, reportSetting: any, reportManager:any) {    
    (async () => {
      var entityName = await this.getEntityName(viewId);
      var code = <number>GlobalPermissions.Print
      if (!this.isAccess(entityName, code)) {
        this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
        return;
      }

      if (this.validateReport(parent)) {
        reportSetting.showReportSetting(parent.gridColumns, parent.entityTypeName, this.viewId, reportManager);
      }
    })();    
  }

  public validateReport(parent: any) {
    if (!parent.rowData || (parent.rowData.data.length == 0)) {
      this.showMessage(this.getText("Report.QuickReportValidate"));
      return false;
    }
    return true;
  }

  ngOnDestroy(): void {
    throw new Error("Method not implemented.");
  }

  public getExcelFileName()
  {
    var localizedViewName = this.getLocalizedViewName(this.viewId);
    var date = '';
    if (this.CurrentLanguage == 'fa')
      date = moment().locale('fa').format('YYYY/MM/DD HH:mm').replace(' ', '-');
    else
      date = moment().locale('en').format('YYYY/MM/DD HH:mm').replace(' ', '-');

    var name = localizedViewName + '-' + date + ".xlsx";
    return name;
  }

  onFooterExportToExcel(header:any,footer:any) {

  }
}
