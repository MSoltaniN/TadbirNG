import { Component, OnInit, Input, Renderer2, ChangeDetectorRef, NgZone, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { String, AutoGeneratedGridComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { MetaDataService, GridService, BrowserStorageService } from '@sppc/shared/services';
import { FiscalPeriodService, FiscalPeriodInfo } from '@sppc/organization/service';
import { FiscalPeriodApi } from '@sppc/organization/service/api';
import { FiscalPeriod } from '@sppc/organization/models';
import { RelatedItemsInfo } from '@sppc/admin/service';
import { SettingService } from '@sppc/config/service';
import { ViewName } from '@sppc/shared/security';
import { RelatedItems } from '@sppc/shared/models';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier/view-identifier.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'fiscalPeriod',
  templateUrl: './fiscalPeriod.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class FiscalPeriodComponent extends AutoGeneratedGridComponent implements OnInit {

  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  rolesList: boolean = false;
  isNew: boolean;
  errorMessage: string;

  editDataItem?: FiscalPeriod = undefined;
  fiscalPeriodRolesData: RelatedItemsInfo;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private fiscalPeriodService: FiscalPeriodService,
    public settingService: SettingService, public ngZone: NgZone, ) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.FiscalPeriod;
    this.viewId = ViewName[this.entityTypeName];

    this.reloadGrid();
    this.cdref.detectChanges();
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (this.groupOperation) {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
    else {
      var recordId = this.selectedRows[0].id;
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0].id;
    this.grid.loading = true;
    this.fiscalPeriodService.getById(String.Format(FiscalPeriodApi.FiscalPeriod, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.grid.loading = false;
    })
    this.isNew = false;
    this.errorMessage = '';
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.isNew = false;
    this.errorMessage = '';
  }

  public saveFiscalPeriodRolesHandler(fPeriodRoles: RelatedItems) {
    this.grid.loading = true;
    this.fiscalPeriodService.modifiedFiscalPeriodRoles(fPeriodRoles)
      .subscribe(response => {
        this.rolesList = false;
        this.showMessage(this.getText("FiscalPeriod.UpdateRoles"), MessageType.Succes);
        this.grid.loading = false;
      }, (error => {
        this.grid.loading = false;
        this.errorMessage = error;
      }));
  }

  public saveHandler(model: FiscalPeriod) {
    model.companyId = this.CompanyId;
    this.grid.loading = true;
    if (!this.isNew) {
      this.fiscalPeriodService.edit<FiscalPeriod>(String.Format(FiscalPeriodApi.FiscalPeriod, model.id), model)
        .subscribe(response => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
          this.selectedRows = [];
        }, (error => {
          this.grid.loading = false;
          this.errorMessage = error;
        }));
    }
    else {
      this.fiscalPeriodService.insert<FiscalPeriod>(FiscalPeriodApi.FiscalPeriods, model)
        .subscribe((response: any) => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;
          this.reloadGrid(insertedModel);
          this.selectedRows = [];

        }, (error => {
          this.isNew = true;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
  }

  public rolesHandler() {
    if (this.selectedRows.length == 1) {
      this.rolesList = true;
      this.grid.loading = true;
      this.fiscalPeriodService.getFiscalPeriodRoles(this.selectedRows[0].id).subscribe(res => {
        this.fiscalPeriodRolesData = res;
        this.grid.loading = false;
      });

      this.errorMessage = '';
    }
  }

  public cancelFiscalPeriodRolesHandler() {
    this.rolesList = false;
    this.errorMessage = '';
  }

  reloadGrid(insertedModel?: FiscalPeriod) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    this.reportFilter = filter;

    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }


    if (insertedModel)
      this.goToLastPage(this.totalRecords);

    this.fiscalPeriodService.getAll(String.Format(FiscalPeriodApi.CompanyFiscalPeriods, this.CompanyId), this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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

  deleteModel(confirm: boolean) {
    if (confirm) {
      if (this.groupOperation) {
        //حذف گروهی
        this.grid.loading = true;
        let rowsId: Array<number> = [];
        this.selectedRows.forEach(item => {
          rowsId.push(item.id);
        })

        this.fiscalPeriodService.groupDelete(FiscalPeriodApi.FiscalPeriods, rowsId).subscribe(res => {
          this.showMessage(this.deleteMsg, MessageType.Info);

          if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.selectedRows = [];
          this.groupOperation = false;
          this.reloadGrid();

        }, (error => {
          this.grid.loading = false;
          this.showMessage(error, MessageType.Warning);
        }));
      }
      else {
        this.grid.loading = true;
        this.fiscalPeriodService.delete(String.Format(FiscalPeriodApi.FiscalPeriod, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);
          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.reloadGrid();
          this.selectedRows = [];
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

  public addNew() {
    this.isNew = true;
    this.editDataItem = new FiscalPeriodInfo();
    this.errorMessage = '';
  }

  public showReport() {

    if (this.validateReport()) {
      /*this.reportManager.directShowReport().then(Response => {
        if (!Response) {
          this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
          this.showReportSetting();
        }
      });*/

      if (!this.reportManager.directShowReport()) {
        this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
        this.showReportSetting();
      }
    }
  }

  public validateReport() {
    if (!this.rowData || this.rowData.total == 0) {
      this.showMessage(this.getText("Report.QuickReportValidate"));
      return false;
    }
    return true;
  }

  public showReportSetting() {
    if (this.validateReport()) {
      this.reportSetting.showReportSetting(this.gridColumns, this.entityTypeName, this.viewId, this.reportManager);
    }
  }

}
