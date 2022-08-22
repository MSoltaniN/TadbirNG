import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone, Input, ViewChild, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { MetaDataService, GridService, BrowserStorageService, LookupService } from '@sppc/shared/services';
import { Currency, CurrencyRate, CurrencyInfo } from '@sppc/finance/models';
import { CurrencyService, CurrencyRateInfo } from '@sppc/finance/service';
import { CurrencyApi } from '@sppc/finance/service/api';
import { SettingService } from '@sppc/config/service';
import { String, AutoGeneratedGridComponent } from '@sppc/shared/class';
import { CurrencyRatePermissions, ViewName } from '@sppc/shared/security';
import { ActivatedRoute } from '@angular/router';
import { CurrencyRateFormComponent } from '@sppc/finance/components/base/currencyRate/currencyRate-form.component';
import { LookupApi } from '@sppc/shared/services/api';
import { GridComponent } from '@progress/kendo-angular-grid';
import { ViewIdentifierComponent, ReportViewerComponent } from '@sppc/shared/components';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReloadOption } from '@sppc/shared/class/reload-option';
import { ReloadStatusType } from '@sppc/shared/enum';
import { OperationId } from '@sppc/shared/enum/operationId';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'currency-rate',
  templateUrl: './currencyRate.component.html',
  styles: [` input[type=text], .ddl-currency { width: 100%; }`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class currencyRateComponent extends AutoGeneratedGridComponent<CurrencyRateInfo> implements OnInit {

  selectedCurrencyId: number;
  selectedCurrency: CurrencyInfo;
  //editDataItem?: CurrencyRate = undefined;
  currenciesRows: Array<CurrencyInfo> = [];
  filteredCurrencies: Array<CurrencyInfo> = [];

  public dialogRef: DialogRef;
  public dialogModel: any;
  viewAccess: boolean;

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private currencyService: CurrencyService, public dialogService: DialogService,
    public settingService: SettingService, public ngZone: NgZone, private activeRoute: ActivatedRoute, public lookupService: LookupService,public elem:ElementRef) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone,elem, CurrencyApi.DeleteCurrencyRates, CurrencyApi.CurrencyRate);
  }

  ngOnInit() {

    this.entityName = Entities.CurrencyRate;
    this.viewId = ViewName[this.entityTypeName];    
    var currId = this.activeRoute.snapshot.paramMap.get('id')
    this.selectedCurrencyId = currId ? parseInt(currId) : 0;

    this.getDataUrl = String.Format(CurrencyApi.CurrencyRates, this.selectedCurrencyId);
    this.getCurrencies();
    this.reloadGrid();
    this.cdref.detectChanges();
    this.viewAccess = this.isAccess(Entities.CurrencyRate, CurrencyRatePermissions.View);
  }

  getCurrencies() {
    this.lookupService.GetLookup(LookupApi.CurrenciesInfo + "?withRate=false").subscribe(res => {
      this.currenciesRows = res;
      this.filteredCurrencies = res;
      this.selectedCurrency = this.currenciesRows.find(f => f.id == this.selectedCurrencyId);
    })
  }

  onChangeCurrency() {
    this.selectedCurrency = this.currenciesRows.find(f => f.id == this.selectedCurrencyId);
    this.getDataUrl = String.Format(CurrencyApi.CurrencyRates, this.selectedCurrencyId);
    this.reloadGrid();
  }

  handleFilter(value: any) {
    this.filteredCurrencies = this.currenciesRows.filter((s) => s.name.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (this.groupOperation) {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
    else {
      var recordId = this.selectedRows[0];//.id;
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];//.id;
    this.grid.loading = true;
    this.currencyService.getById(String.Format(CurrencyApi.CurrencyRate, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.openEditorDialog(false);
      this.grid.loading = false;
    })
  }

  public cancelHandler() {
    this.editDataItem = undefined;
  }

  public addNew() {
    this.editDataItem = new CurrencyRateInfo();
    this.editDataItem.currencyId = this.selectedCurrencyId;
    this.editDataItem.branchId = this.BranchId;
    this.editDataItem.branchName = this.BranchName;
    this.openEditorDialog(true);
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {


    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: CurrencyRateFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessages = undefined;
    this.dialogModel.currencyName = this.selectedCurrency? this.selectedCurrency.name: '';
    
    this.dialogRef.content.instance.save.subscribe((model) => {      
      var serviceUrl = isNew ? String.Format(CurrencyApi.CurrencyRates, this.selectedCurrencyId) : String.Format(CurrencyApi.CurrencyRate, model.id);
      this.saveHandler(model, isNew, this.currencyService, serviceUrl);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  onAdvanceFilterOk(): any {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.reloadGrid();
  }
}
