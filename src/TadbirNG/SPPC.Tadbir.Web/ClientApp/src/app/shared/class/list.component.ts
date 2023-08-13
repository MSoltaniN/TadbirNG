import { DefaultComponent } from '@sppc/shared/class/default.component';
import { Injectable, OnDestroy, Renderer2, ChangeDetectorRef, NgZone, EventEmitter, ViewChild, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { GridService, BrowserStorageService, MetaDataService } from '../services';
import { SettingService } from '@sppc/config/service';
import { ServiceLocator } from "@sppc/service.locator";
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { AdvanceFilterComponent } from "@sppc/shared/components/advanceFilter/advance-filter.component";
import { Permissions, GlobalPermissions, DashboardPermissions } from "@sppc/shared/security/permissions";
import { FilterExpression } from '@sppc/shared/class/filterExpression';
import { FilterRow } from "@sppc/shared/models";
import { MessageType, Entities } from '@sppc/shared/enum/metadata';
import * as moment from 'jalali-moment';
import { ViewName } from "@sppc/shared/security";
import { lastValueFrom } from 'rxjs';
import { String } from './source';

@Injectable()
export class ListComponent extends DefaultComponent implements OnDestroy {

  public advanceFilters: FilterExpression;
  public advanceFilterList: Array<FilterRow>;
  public selectedGroupFilter: number;

  dialogService: DialogService;
  permission: Permissions;
  filterDialogRef: DialogRef;
  excelFileName: string;

  exportAccessed: boolean;
  printAccessed: boolean;
  filterAccessed: boolean;

  /**این تابع قبل از نمایش تنظیمات گزارش فوری اجرا میشود*/
  public onBeforeQuickReportSetting() {
    /*console.log('base ondatabind')*/
  }

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService,
    public renderer: Renderer2, public metadataService: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, bStorageService, renderer, metadataService, settingService, '', undefined);

    this.dialogService = ServiceLocator.injector.get(DialogService);
    this.permission = new Permissions();    
  } 
  

  showAdvanceFilterComponent(viewId: number, onOk: EventEmitter<any>, onCancel: EventEmitter<any>) {    
    //(async () => {

    this.getGlobalPermissions().then(() => {
      if (!this.filterAccessed) {
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
    });     
     

    //})();
  }

  showReportManager(viewId: number, parent: any, reportSetting: any, reportManager: any) {     
    //(async () => {
      this.getGlobalPermissions().then(() => {
        if (!this.printAccessed) {
          this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
          return;
        }

        if (this.validateReport(parent)) {
          if (!reportManager.directShowReport()) {
            this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
            this.onBeforeQuickReportSetting();
            reportSetting.showReportSetting(parent.gridColumns, parent.entityTypeName, this.viewId, reportManager);
          }
        }     
      })      
    //})(); 
  }
  

  async getGlobalPermissions() {
    
    if (this.viewId) {
      var entityName = await this.getEntityName(this.viewId);

      var code = entityName == Entities.Dashboard? 
        <number>DashboardPermissions.ManageWidgets:
        <number>GlobalPermissions.Export;
      this.exportAccessed = this.isAccess(entityName, code);

      code = entityName == Entities.Dashboard? 
        <number>DashboardPermissions.ManageWidgets:
        <number>GlobalPermissions.Filter;
      this.filterAccessed = this.isAccess(entityName, code);

      code = entityName == Entities.Dashboard? 
        <number>DashboardPermissions.ManageWidgets:
        <number>GlobalPermissions.Print;
      this.printAccessed = this.isAccess(entityName, code);      
    }    
  }

  showQuickReportSetting(viewId: number, parent: any, reportSetting: any, reportManager:any) {    
    //(async () => {
      //var entityName = await this.getEntityName(viewId);

      //this.getGlobalPermissions();

    this.getGlobalPermissions().then(() => {
      if (!this.printAccessed) {
        this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
        return;
      }

      if (this.validateReport(parent)) {
        this.onBeforeQuickReportSetting();
        reportSetting.showReportSetting(parent.gridColumns, parent.entityTypeName, this.viewId, reportManager);
      }
    });
      
    //})();    
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

    if (this.viewId == ViewName[Entities.ComparativeProfitLoss] || this.viewId == ViewName[Entities.ComparativeProfitLossSimple]) {
      localizedViewName = this.getText("ProfitLoss.ComparativeProfitLoss");
    }
    
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

  async changeStateConfirmDialog(toActivate,cb?:{onSave?: Function, onDiscard?: Function}) {
    let result;
    let msg = toActivate?
      String.Format(this.getText("Messages.ChangeStateConfirm"),this.getText("Buttons.Aactivate")):
      String.Format(this.getText("Messages.ChangeStateConfirm"),this.getText("Buttons.Deactivate"));
    const dialog: DialogRef = this.dialogService.open({
      title: this.getText("Form.ChangeStatus"),
      content: msg,
      actions: [
        { text: this.getText("Buttons.Yes"), mode: true, primary: true },
        { text: this.getText("Buttons.No"), mode: false },
      ],
      width: 450,
      height: 150,
      minWidth: 250,
      cssClass: 'global-confirm-box'
    });

    dialog.dialog.location.nativeElement.classList.add("dialog-padding");

    result = await lastValueFrom(dialog.result);
    
    return result?.mode;
  }

  // updateActiveState(toActivate:boolean);
  // updateActiveState(toActivate:boolean, args:{apiUrl:string,model:any});
  updateActiveState(toActivate:boolean,apiUrl?:string,model?:any) {

    let self:any = this;

    if (model.state == this.getText("Form.Active") && toActivate) {
      this.showMessage(
        String.Format(
          this.getText("Messages.StateAlreadyChanged"),
          this.getText("Form.Activated")
        )
      )
      return;
    }

    if (model.state == this.getText("Form.Inactive") && !toActivate) {
      this.showMessage(
        String.Format(
          this.getText("Messages.StateAlreadyChanged"),
          this.getText("Form.Deactivated")
        )
      )
      return;
    }    

    this.changeStateConfirmDialog(toActivate)
    .then(confirm => {
      if (confirm) {

        lastValueFrom(this.settingService.updateActiveState(apiUrl, model))
          .then(res => {
            self.reloadGrid();
          })
          .catch(error => {
            if (error && error.statusCode == 400)
              this.showMessage(
                self.errorHandlingService.handleError(error),
                MessageType.Warning
              );
          });
      }
    })
  }

  stringFormat(format:string,...args) {
    return String.Format(format,...args);
  }
}
