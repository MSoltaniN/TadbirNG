import { Injectable } from "@angular/core";
import { BaseService } from "../class/base.service";
import { Http, Response} from "@angular/http";
import { ListFormViewConfig } from "../model/listFormViewConfig";
import { ColumnViewConfig } from "../model/columnViewConfig";
import { SettingsApi } from "./api/settingsApi";
import { String } from '../class/source';
import { SettingBrief } from "../model/settingBrief";
import { ColumnViewDeviceConfig } from "../model/columnViewDeviceConfig";
import { HttpClient } from "@angular/common/http";
import { ColumnVisibility, SessionKeys } from "../../environments/environment";


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

export class ColumnViewDeviceConfigInfo  implements ColumnViewDeviceConfig {
    

    constructor(public designIndex: number = 0,
        public width?: number | undefined,
        public index?: number | undefined,
        public visibility: string = ColumnVisibility.Default, public title: string = ""
        ) { }
    
}

export class ListFormViewConfigInfo implements ListFormViewConfig {
    constructor(public viewId: number = 0,
        public pageSize = 10, public columnViews: ColumnViewConfig[] = [])
    { }
    
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

export class SettingViewModelInfo  {

    constructor(public name: string = "",
        public designIndex: number = 0,
        public width: number | undefined = 0,
        public index: number | undefined = 0,
        public visibility: boolean = true,
        public disabled: boolean = false,
        public title: string = "") { }
    
}


@Injectable()
export class SettingService extends BaseService {

    constructor(public http: HttpClient) {
        super(http);
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

    //#region Setting Helper Method

    public getSettingByViewId(viewId: number): ListFormViewConfig | null {

        var settingsJson = localStorage.getItem(SessionKeys.Setting + this.UserId);
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

        var settingsJson = localStorage.getItem(SessionKeys.Setting + this.UserId);
        if (settingsJson) {
            var settings: Array<ListFormViewConfig> = JSON.parse(settingsJson);

            if (!settings) settings = new Array<ListFormViewConfig>();

            var findIndex = settings.findIndex(s => s.viewId == viewId);
            if (findIndex > -1)
                settings[findIndex] = currentSetting;
            else
                settings.push(currentSetting);

            var jsonSetting = JSON.stringify(settings);

            localStorage.setItem(SessionKeys.Setting + this.UserId, jsonSetting);
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

    //#endregion
}
