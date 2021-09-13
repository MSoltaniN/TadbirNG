import { Component, OnInit, Renderer2, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { RowClassArgs } from '@progress/kendo-angular-grid';
import { String, DefaultComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { MetaDataService, BrowserStorageService, ErrorHandlingService } from '@sppc/shared/services';
import { ViewTreeLevelConfig, ViewTreeConfig } from '@sppc/config/models';
import { SettingService, SettingBriefInfo } from '@sppc/config/service';
import { SettingsApi } from '@sppc/config/service/api';
import { ViewName } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';
import { CurrencyService } from '@sppc/finance/service';
import { CurrencyApi, VoucherApi, AccountApi } from '@sppc/finance/service/api';



export interface Item {
  key: number,
  value: string
}

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'system-configuration',
  templateUrl: './systemConfiguration.component.html',
  styles: [``],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class SystemConfigurationComponent extends DefaultComponent implements OnInit {

  //public errorMessage = String.Empty;

  systemConfigModel: SettingBriefInfo;

  currencyNameLookup: Array<Item> = [];
  currencyNameData: Array<Item> = [];
  selectedCurrencyName: string;
  decimalCount: number;
  selectedCalendar: number = 0;
  calendarList: Array<Item> = [
    { key: 0, value: "Settings.PersianCalendar" },
    { key: 1, value: "Settings.ADCalendar" }
  ];

  isRefreshTreeView: boolean = false;

  //TODO باید از سمت سرویس خوانده شود
  isExistAccount: boolean = false;
  isExistArticle: boolean = false;
  useDefaultCoding: boolean;

  viewTreeValue: Array<any>;

  @Input() public set model(setting: SettingBriefInfo) {
    this.systemConfigModel = setting;
    var configValue = JSON.parse(JSON.stringify(setting.values));
    this.selectedCalendar = configValue.defaultCalendar;
    this.selectedCurrencyName = configValue.defaultCurrencyNameKey;
    this.useDefaultCoding = configValue.usesDefaultCoding;
    this.decimalCount = configValue.defaultDecimalCount;
  }


  constructor(public toastrService: ToastrService, public translate: TranslateService, private formBuilder: FormBuilder, public currencyService: CurrencyService,
    public renderer: Renderer2, public metadata: MetaDataService,
    public settingService: SettingService, public bStorageService: BrowserStorageService, public errorHandlingService: ErrorHandlingService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.Setting, undefined);
  }

  public ngOnInit(): void {
    this.getCurrencyNames();
    this.getAccountsCount();
    this.getVoucherArticlesCount();
  }

  getCurrencyNames() {
    this.currencyService.getModels(CurrencyApi.CurrencyNamesLookup).subscribe(res => {
      this.currencyNameLookup = res;
      this.currencyNameData = res;
    })
  }

  getVoucherArticlesCount() {
    this.settingService.getModels(VoucherApi.VoucherArticlesCount).subscribe(res => {
      this.isExistArticle = res ? true : false;
    })
  }

  getAccountsCount() {
    this.settingService.getModels(AccountApi.AccountsCount).subscribe(res => {
      this.isExistAccount = res ? true : false;
    })
  }

  onChangeCurrency(item: any) {
    if (item)
      this.currencyService.getModels(String.Format(CurrencyApi.CurrencyInfoByName, item)).subscribe(res => {

        var result = res;
        this.decimalCount = result.decimalCount;

      }, error => {
        if (error.status == 404)
          this.showMessage(this.getText('App.RecordNotFound'), MessageType.Warning);
      })
  }

  handleFilter(value: any) {
    this.currencyNameData = this.currencyNameLookup.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  onChangeViewTreeValue(event: any) {
    this.viewTreeValue = event;
  }

  saveSystemConfig() {
    debugger;
    this.isRefreshTreeView = false;
    if (this.viewTreeValue)
      this.saveViewTreeConfig();
    else
      this.saveDefaultCurrency();
  }


  saveDefaultCurrency() {
    this.currencyService.insertDefaultCurrency(String.Format(CurrencyApi.DefaultCurrency, this.selectedCurrencyName)).subscribe(res => {
      this.saveConfig();
    })
  }

  saveConfig() {
    var configValue = JSON.parse(JSON.stringify(this.systemConfigModel.values));
    configValue.defaultCalendar = this.selectedCalendar;
    configValue.defaultCurrencyNameKey = this.selectedCurrencyName;
    configValue.usesDefaultCoding = this.useDefaultCoding;
    configValue.defaultDecimalCount = this.decimalCount;

    this.systemConfigModel.values = configValue;

    this.settingService.edit<SettingBriefInfo>(SettingsApi.SystemConfig, this.systemConfigModel).subscribe(res => {
      this.showMessage(this.updateMsg, MessageType.Succes);
      this.isRefreshTreeView = true;
    })
  }

  saveViewTreeConfig() {
    this.settingService.putViewTreeConfig(SettingsApi.ViewTreeSettings, this.viewTreeValue).subscribe(res => {
      this.saveDefaultCurrency();

      localStorage.removeItem("viewTreeConfig");      
    }, (error => {
        if(error)
          this.errorMessages = this.errorHandlingService.handleError(error);
    }));
  }
}
