import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/env/environment';
import { BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { SettingBriefInfo } from '@sppc/config/service';
import { DetailComponent } from '@sppc/shared/class';
import { DateRangeType, SettingsType } from '@sppc/shared/enum';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  key: string,
  value: string
}

interface Item2 {
  key: number,
  value: string
}

@Component({
  selector: 'settings-form-component',
  styles: [`       
        .settings-form{margin-top: 25px; color: #6f6f6f;}
        .settings-form label{font-weight: normal;}
        `
  ],
  templateUrl: './settings-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class SettingsFormComponent extends DetailComponent {

  public selectedItemModel: SettingBriefInfo;
  public selectedItemType: string = '';


  public accountRelationsForm = new FormGroup({
    //useLeafAccounts: new FormControl(),
    useLeafDetails: new FormControl(),
    useLeafCostCenters: new FormControl(),
    useLeafProjects: new FormControl()
  });

  public dateRangesForm = new FormGroup({
    defaultDateRange: new FormControl()
  });
  public ddlDateRanges: Array<Item>;
  public selectedDDLDateRangeValue: string | undefined;


  public numberDisplayForm = new FormGroup({
    decimalPrecision: new FormControl(),
    maxPrecision: new FormControl(),
    useSeparator: new FormControl(),
    separatorMode: new FormControl(),
    separatorSymbol: new FormControl()
  });

  public financeReportForm = new FormGroup({
    openingAsFirstVoucher: new FormControl(),
    startTurnoverAsInitBalance: new FormControl()
  });

  public ddlNumberPrecision: Array<Item2>;
  public ddlSeparatorMode: Array<Item>;
  public selectedDDLDecimalPrecisionValue: string;
  public selectedDDLMaxPrecisionValue: string;
  public selectedDDLSeparatorModeValue: string;
   

  @Input() public set model(setting: SettingBriefInfo) {
    this.selectedItemModel = setting;
    this.selectedItemType = '';
    if (setting != undefined) {
      this.selectedItemType = setting.modelType;
      this.setFormValue(this.selectedItemModel.values);
    }

  }

  @Output() updateList: EventEmitter<SettingBriefInfo> = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Setting, undefined);

    this.ddlDateRanges = [
      { value: "Settings.DateRanges.FiscalStartToCurrent", key: DateRangeType.FiscalStartToCurrent },
      { value: "Settings.DateRanges.FiscalStartToFiscalEnd", key: DateRangeType.FiscalStartToFiscalEnd },
      { value: "Settings.DateRanges.CurrentToCurrent", key: DateRangeType.CurrentToCurrent }
    ];

    this.ddlNumberPrecision = [
      { value: "0", key: 0 },
      { value: "1", key: 1 },
      { value: "2", key: 2 },
      { value: "3", key: 3 },
      { value: "4", key: 4 },
      { value: "5", key: 5 },
      { value: "6", key: 6 },
      { value: "7", key: 7 },
      { value: "8", key: 8 },
    ];

    this.ddlSeparatorMode = [
      { value: "Settings.NumberDisplay.UseDefault", key: "UseDefault" },
      { value: "Settings.NumberDisplay.UseCustom", key: "UseCustom" }
    ];
  }

  updateListHandler() {
    switch (this.selectedItemModel.modelType) {

      case SettingsType.DateRangeConfig:
        {
          this.selectedItemModel.values = this.dateRangesForm.value;
          this.updateList.emit(this.selectedItemModel);
          break;
        }
      case SettingsType.ViewTreeConfig:
        {
          break;
        }
      case SettingsType.NumberDisplayConfig:
        {
          this.selectedItemModel.values = this.numberDisplayForm.value;
          this.updateList.emit(this.selectedItemModel);
          break;
        }
      case SettingsType.RelationsConfig:
        {
          this.selectedItemModel.values = this.accountRelationsForm.value;
          this.updateList.emit(this.selectedItemModel);
          break;
        }
      case SettingsType.FinanceReportConfig:
        {
          this.selectedItemModel.values = this.financeReportForm.value;
          this.updateList.emit(this.selectedItemModel);
          break;
        }
      default:
        {
          break;
        }
    }

  }

  defaultSettingsHandler() {
    this.setFormValue(this.selectedItemModel.defaultValues);
  }

  setFormValue(objectValue: object) {

    switch (this.selectedItemModel.modelType) {
      case SettingsType.DateRangeConfig:
        {
          var defaultValue = JSON.parse(JSON.stringify(objectValue));
          this.selectedDDLDateRangeValue = defaultValue.defaultDateRange;

          this.dateRangesForm.reset(objectValue);
          break;
        }
      case SettingsType.ViewTreeConfig:
        {
          break;
        }
      case SettingsType.NumberDisplayConfig:
        {
          var defaultValue = JSON.parse(JSON.stringify(objectValue));
          this.selectedDDLDecimalPrecisionValue = defaultValue.decimalPrecision;
          this.selectedDDLMaxPrecisionValue = defaultValue.maxPrecision;
          this.selectedDDLSeparatorModeValue = defaultValue.separatorMode;
          this.numberDisplayForm.reset(objectValue);
          break;
        }
      case SettingsType.FinanceReportConfig:
        {
          this.financeReportForm.reset(objectValue);
          break;
        }
      case SettingsType.RelationsConfig:
        {
          this.accountRelationsForm.reset(objectValue);
          break;
        }
      default:
        {
          break;
        }
    }
  }
}
