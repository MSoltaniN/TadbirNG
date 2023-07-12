import { Component, OnInit, Renderer2, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
// import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { RowClassArgs } from '@progress/kendo-angular-grid';
import { String, DefaultComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType, CalendarType } from '@sppc/shared/enum/metadata';
import { MetaDataService, BrowserStorageService, ErrorHandlingService, SessionKeys } from '@sppc/shared/services';
import { ViewTreeLevelConfig, ViewTreeConfig } from '@sppc/config/models';
import { SettingService, SettingBriefInfo } from '@sppc/config/service';
import { SettingsApi } from '@sppc/config/service/api';
import { ViewName } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';
import { CurrencyService } from '@sppc/finance/service';
import { CurrencyApi, VoucherApi, AccountApi } from '@sppc/finance/service/api';
import {InventoryMode} from "@sppc/config/enums/inventoryMode";



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
  styles: [`
    .mx-7 {margin: 6px 7px !important;}
    .mr-25 {margin-right: 25px !important}
    .ml-25 {margin-left: 25px !important}
  `],
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
  InventoryModeItem=InventoryMode;
  inventoryMode: number = 0;

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
    this.inventoryMode = configValue.inventoryMode;
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
    configValue.inventoryMode = this.inventoryMode;

    this.systemConfigModel.values = configValue;

    this.settingService.edit<SettingBriefInfo>(SettingsApi.SystemConfig, this.systemConfigModel).subscribe(res => {
      this.showMessage(this.updateMsg, MessageType.Succes);
      this.bStorageService.setSystemConfig(configValue);      
      this.updateMetadatas();
      this.isRefreshTreeView = true;
    },err => {
      this.showMessage(err.messages[0],MessageType.Error);
    })
  }

  /**
   * بروز رسانی متادیتا هایی که شامل ستون از نوع تاریخ می باشد
   * */
  updateMetadatas() {

    var viewIds = new Array<number>();
    for (var entity in Entities) {      
      var viewId = parseInt(ViewName[entity]);
      if (viewId > 0) {
        var metadataKey = String.Format(SessionKeys.MetadataKey, viewId.toString(), this.CurrentLanguage);
        var metadata = JSON.parse(this.bStorageService.getMetadata(metadataKey));
        if (metadata && metadata.columns) {
          metadata.columns.forEach((property) => {
            if (property.scriptType.toLowerCase() == "date" || property.scriptType.toLowerCase() == "datetime") {
              this.bStorageService.removeLocalStorage(metadataKey);
              viewIds.push(viewId);
              return;
            }
          });
        }        
      }
    }

    this.metadata.getViews().subscribe((res: any) => {
      var views: Array<any> = res;
      views.forEach((item) => {        
        var viewId = item.id;
        var metaDataName = String.Format(SessionKeys.MetadataKey, viewId ? viewId.toString() : '', this.CurrentLanguage);
        if(viewIds.findIndex(v=>v == viewId) >= 0)
          this.bStorageService.setMetadata(metaDataName, item);        
      });

    });
    
    
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
