import { Injectable } from "@angular/core";
import { Response } from "@angular/http";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { ColumnViewDeviceConfig, ListFormViewConfig, ColumnViewConfig, QuickSearchConfig, QuickSearchColumnConfig } from "@sppc/shared/models";
import { ColumnVisibility } from "@sppc/env/environment";
//import { String, BaseService } from '@sppc/shared/class';
import { String } from '@sppc/shared/class/source';
import { BaseService } from '@sppc/shared/class/base.service';
import { SettingBrief, ViewTreeConfig, ViewTreeLevelConfig, NumberConfig } from "../models";
import { SettingsApi } from "./api";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { DateRangeType, SettingKey } from "@sppc/shared/enum";
import { TestBalanceConfig } from "../models/testBalanceConfig";
import { FormLabelConfig } from "../models/formLabelConfig";



export class SettingBriefInfo implements SettingBrief {
  modelType: string;
  id: number;
  title: string;
  description?: string | undefined;
  values: Object;
  defaultValues: Object;
}

export class SettingTreeNodeInfo {
  constructor(public id: number = 0,
    public parentId: number | undefined,
    public title: string,
    public description: string | undefined,
    public modelType: string | undefined) { }
}

export class ColumnViewDeviceConfigInfo implements ColumnViewDeviceConfig {


  constructor(public designIndex: number = 0,
    public width?: number | undefined,
    public index?: number | undefined,
    public visibility: string = ColumnVisibility.Default, public title: string = ""
  ) { }

}

export class ListFormViewConfigInfo implements ListFormViewConfig {
  constructor(public viewId: number = 0,
    public pageSize = 10, public columnViews: ColumnViewConfig[] = []) { }

}

export class ColumnViewConfigInfo implements ColumnViewConfig {

  constructor(public name: string = ""
  ) { }

  public large: ColumnViewDeviceConfig;
  public medium: ColumnViewDeviceConfig;
  public small: ColumnViewDeviceConfig;
  public extraSmall: ColumnViewDeviceConfig;
  public extraLarge: ColumnViewDeviceConfig;
}

export class SettingViewModelInfo {

  constructor(public name: string = "",
    public designIndex: number = 0,
    public width: number | undefined = 0,
    public index: number | undefined = 0,
    public visibility: boolean = true,
    public disabled: boolean = false,
    public isSearched: boolean = false,
    public title: string = "") { }

}

export class LogSettingNodeViewModelInfo {

  constructor(public id: number = 0,
    public parentId: number = 0,
    public name: string | undefined = "",
    public items: Array<LogSettingItemViewModel> | undefined) { }
}

export class LogSettingItemViewModel {

  constructor(public id: number = 0,
    public operationId: number = 0,
    public operationName: string | undefined = "",
    public isEnabled: boolean | undefined = false) { }
}

export class LogCheckItem {

  constructor(public nodeId: number = 0,
    public detailId: number = 0,
    public operationId: number = 0, 
    public isEnabled: boolean = false) { }
}

export class ViewTreeConfigInfo implements ViewTreeConfig {
  viewId: number;
  maxDepth: number;
  levels: ViewTreeLevelConfig[];
}

export class QuickSearchConfigInfo implements QuickSearchConfig {
  constructor(public viewId: number = 0, public searchMode: string = "", public columns: QuickSearchColumnConfig[] = []) { }
}

export class QuickSearchColumnConfigInfo implements QuickSearchColumnConfig {
  name: string;
  title: string;
  displayIndex: number;
  isDisplayed: boolean;
  isSearched: boolean;
}

@Injectable()
export class SettingService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  public getSettingsCategories(apiUrl: string) {
    var options = { headers: this.httpHeaders };
    return this.http.get(apiUrl, options)
      .map(response => <any>(<Response>response));
  }

  public putSettingsCategories(apiUrl: string, list: Array<SettingBriefInfo>) {
    var body = JSON.stringify(list);
    var options = { headers: this.httpHeaders };
    return this.http.put(apiUrl, body, options)
      .map(res => res)
      .catch(this.handleError);
  }

  getViewTreeSettings(viewId: number) {
    var url = String.Format(SettingsApi.ViewTreeSettingsByView, viewId);
    return this.http.get(url, this.option)
      .map(response => <any>(<Response>response));
  }

  public putViewTreeConfig(apiUrl: string, model: any): Observable<string> {

    var body = JSON.stringify(model);
    return this.http.put(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  getListSettingsByUser(userId: number) {
    var url = String.Format(SettingsApi.ListSettingsByUser, userId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }

  getListSettingsByUserAndView(userId: number, viewId: number) {
    var url = String.Format(SettingsApi.ListSettingsByUserAndView, userId, viewId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  putUserSettings(userId: number, setting: ListFormViewConfig) {
    var url = String.Format(SettingsApi.ListSettingsByUser, userId);
    var body = JSON.stringify(setting);
    var options = { headers: this.httpHeaders };
    return this.http.put(url, body, options)
      .map(res => res)
      .catch(this.handleError);

  }


  getQuickSearchSettingsByUserAndView(userId: number, viewId: number) {
    var url = String.Format(SettingsApi.QuickSearchSettingsByUserAndView, userId, viewId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }

  putUserQuickSearchSettings(userId: number, setting: QuickSearchConfig) {
    var url = String.Format(SettingsApi.QuickSearchSettingsByUser, userId);
    var body = JSON.stringify(setting);
    var options = { headers: this.httpHeaders };
    return this.http.put(url, body, options)
      .map(res => res)
      .catch(this.handleError);
  }

  getFormLabelSettingsAsync(formId: number) {
    var url = String.Format(SettingsApi.FormLabelsConfig, formId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }

  putModifiedFormLabelSettingsAsync(formId: number, formLabel: FormLabelConfig) {
    var url = String.Format(SettingsApi.FormLabelsConfig, formId);
    var body = JSON.stringify(formLabel);
    var options = { headers: this.httpHeaders };
    return this.http.put(url, body, options)
      .map(res => res)
      .catch(this.handleError);
  }

  async getQuickSearchSettingsByUserAndViewAsync(userId: number, viewId: number): Promise<QuickSearchConfig> {
    var qsSetting = this.bStorageService.getQuickSearchConfig(viewId, userId);
    if (qsSetting) {
      return JSON.parse(qsSetting);
    }
    else {
      const response = await this.getQuickSearchSettingsByUserAndView(userId, viewId).toPromise();
      if (response) {
        this.bStorageService.setQuickSearchConfig(viewId, userId,response)
        return response;
      }
    }

    return null;
  }

  setLocalQuickSearchSettings(userId: number, viewId: number, setting: QuickSearchConfig) {
    this.bStorageService.setQuickSearchConfig(viewId, userId, setting);
  }

  getLocalQuickSearchSettings(userId: number, viewId: number): QuickSearchConfig | null {
    var jsonSetting = this.bStorageService.getQuickSearchConfig(viewId, userId);
    if (jsonSetting)
      return JSON.parse(jsonSetting);

    return null;    
  }

  //#region Setting Helper Method

  public getSettingByViewId(viewId: number): ListFormViewConfig | null {

    var settingsJson = this.bStorageService.getUserSettings(this.UserId);
    if (settingsJson) {
      var settings: Array<ListFormViewConfig> = JSON.parse(settingsJson);

      var findIndex = settings.findIndex(s => s.viewId == viewId);
      if (findIndex > -1)
        return settings[findIndex];
    }

    return null;
  }

  public setSettingByViewId(viewId: number, currentSetting: ListFormViewConfig) {

    //var storageId: string = this.grid.wrapper.nativeElement.id + this.defaultComponent.UserId;

    var settingsJson = this.bStorageService.getUserSettings(this.UserId);
    if (settingsJson) {
      var settings: Array<ListFormViewConfig> = JSON.parse(settingsJson);

      if (!settings) settings = new Array<ListFormViewConfig>();

      var findIndex = settings.findIndex(s => s.viewId == viewId);
      if (findIndex > -1)
        settings[findIndex] = currentSetting;
      else
        settings.push(currentSetting);

      this.bStorageService.setUserSetting(settings, this.UserId);
    }
  }

  /** براساس سایز تنظیمات را از انتیتی ارسالی به تابع برمیگرداند */
  public getCurrentColumnViewConfig(columnViewDevice: ColumnViewConfig): ColumnViewDeviceConfig {

    var currentColumnViewDevice: ColumnViewDeviceConfig = columnViewDevice.medium;
    switch (this.media) {
      case "xs":
        currentColumnViewDevice = columnViewDevice.extraSmall;
        break;
      case "sm":
        currentColumnViewDevice = columnViewDevice.small;
        break;
      case "md":
        currentColumnViewDevice = columnViewDevice.medium;
        break;
      case "l":
        currentColumnViewDevice = columnViewDevice.large;
        break;
      case "el":
        currentColumnViewDevice = columnViewDevice.extraLarge;
        break;
    }


    return currentColumnViewDevice;
  }

  /** براساس سایز ، تنظیمات را مقدار دهی و به تابع برمیگرداند */
  public setCurrentColumnViewConfig(columnViewDevice: ColumnViewConfig, value: ColumnViewDeviceConfig): ColumnViewConfig {

    var currentColumnViewDevice: ColumnViewConfig = columnViewDevice;
    switch (this.media) {
      case "xs":
        columnViewDevice.extraSmall = value;
        break;
      case "sm":
        columnViewDevice.small = value;
        break;
      case "md":
        columnViewDevice.medium = value;
        break;
      case "l":
        columnViewDevice.large = value;
        break;
      case "el":
        columnViewDevice.extraLarge = value;
        break;
    }

    return columnViewDevice;
  }

  /**
   * تنظیمات گزارش فوری را برمیگرداند
   * @param userId
   * @param viewId
   */
  public getQuickReportSettingsByUserAndView(userId: number, viewId: number) {
    var url = String.Format(SettingsApi.QuickReportSettingsByUserAndView, userId, viewId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }


  /**
   * تنظیمات مربوط به فرمت اعداد را برمیگرداند
   */

  async getNumberConfigBySettingIdAsync(): Promise<NumberConfig> {
    let config: NumberConfig;

    var numConfig = this.bStorageService.getNumberConfig();
    if (numConfig) {
        config = JSON.parse(numConfig);
        return config;
    }
    else {
      const response = await this.getSettingById(SettingKey.NumberDisplayConfig).toPromise();
      if (response) {
        var res = response.values;
        this.bStorageService.setNumberConfig(res);
        return res;
      }
    }

  }

  /**
   * تنظیمات را با استفاده از شناسه تنظیمات برمیگرداند
   * @param settingId شناسه یکتای تنظیمات
   */
  getSettingById(settingId: number) {
    var url = String.Format(SettingsApi.Setting, settingId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(res => res)
      .catch(this.handleError);
  }

  async getDateConfigAsync(type: string): Promise<Date> {

    let dateRange: any;
    let fromDate: Date;
    let toDate: Date;

    var dateRangeConfig = this.bStorageService.getdateRangeConfig();

    if (dateRangeConfig) {
      var range = JSON.parse(dateRangeConfig);
      dateRange = range ? range.defaultDateRange : DateRangeType.CurrentToCurrent;
    }
    else {
      const response = await this.getSettingById(SettingKey.DateRangeConfig).toPromise();
      if (response) {
        var res = response.values;
        this.bStorageService.setDateRangeConfig(res);
        dateRange = res.defaultDateRange;
      }
    }

    switch (dateRange) {
      case DateRangeType.CurrentToCurrent: {
        fromDate = new Date();
        toDate = new Date();
        break;
      }
      case DateRangeType.FiscalStartToCurrent: {
        fromDate = this.FiscalPeriodStartDate;
        toDate = new Date();
        break;
      }
      case DateRangeType.FiscalStartToFiscalEnd: {
        fromDate = this.FiscalPeriodStartDate;
        toDate = this.FiscalPeriodEndDate;
        break;
      }
      default:
    }

    if (type == "start") {
      return fromDate;
    }
    else
      if (type == "end") {
        return toDate;
      }

    return undefined;
  }

  //#endregion

  /**
 * تنظیمات لاگ شرکت را برمیگرداند
 */
  getLogSettings() {
    var url = SettingsApi.LogSettings;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(res => res)
      .catch(this.handleError);
  }

  /**
 * تنظیمات لاگ سیستمی را برمیگرداند
 */
  getSystemLogSettings() {
    var url = SettingsApi.SystemLogSettings;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(res => res)
      .catch(this.handleError);
  }

  /**
 * تنظیمات لاگ شرکت ها را ذخیره میکند
 */
  putLogSettings(items:Array<LogSettingItemViewModel>) {
    var url = SettingsApi.LogSettings;
    var body = JSON.stringify(items);
    var options = { headers: this.httpHeaders };
    return this.http.put(url,body, options)
      .map(res => res)
      .catch(this.handleError);
  }

  /**
 * تنظیمات لاگ سیستمی را ذخیره میکند
 */
  putSystemLogSettings(items: Array<LogSettingItemViewModel>) {
    var url = SettingsApi.SystemLogSettings;
    var body = JSON.stringify(items);
    var options = { headers: this.httpHeaders };
    return this.http.put(url,body, options)
      .map(res => res)
      .catch(this.handleError);
  }
}
