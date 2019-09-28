import { Component, OnInit, Renderer2 } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { RowClassArgs } from '@progress/kendo-angular-grid';
import { String, DefaultComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { ViewTreeLevelConfig, ViewTreeConfig } from '@sppc/config/models';
import { SettingService } from '@sppc/config/service';
import { SettingsApi } from '@sppc/config/service/api';
import { ViewName } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';
import { CurrencyService } from '@sppc/finance/service';
import { CurrencyApi } from '@sppc/finance/service/api';



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

  public errorMessage = String.Empty;

  currencyNameLookup: Array<Item> = [];
  currencyNameData: Array<Item> = [];
  selectedCurrencyName: string;
  decimalCount: number;
  selectedCalendar: number = 0;
  calendarList: Array<Item> = [
    { key: 0, value: "شمسی" },
    { key: 1, value: "میلادی" }
  ];

  //TODO باید از سمت سرویس خوانده شود
  isDefineAccount: boolean = true;
  useDefaultCoding: boolean;

  viewTreeValue: Array<any>;

  constructor(public toastrService: ToastrService, public translate: TranslateService, private formBuilder: FormBuilder, public currencyService: CurrencyService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.Setting, undefined);
  }

  public ngOnInit(): void {
    this.getCurrencyNames();
  }


  getCurrencyNames() {
    this.currencyService.getModels(CurrencyApi.CurrencyNamesLookup).subscribe(res => {
      this.currencyNameLookup = res;
      this.currencyNameData = res;
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

    //TODO  باید کامل شود
    this.settingService.putViewTreeConfig(SettingsApi.ViewTreeSettings, this.viewTreeValue).subscribe(res => {
      //this.maxDepthValue = undefined;
      //this.ddlEntitySelected = undefined;
      //this.viewTreeConfig = undefined;
      //this.viewTreeLevels = [];
      //this.finalViewTreeConfig = [];

      localStorage.removeItem("viewTreeConfig");

      this.showMessage(this.updateMsg, MessageType.Succes);

    }, (error => {
      this.errorMessage = error;
    }));
  }
}
