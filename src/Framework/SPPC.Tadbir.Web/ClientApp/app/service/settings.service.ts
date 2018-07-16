import { Injectable } from "@angular/core";
import { BaseService } from "../class/base.service";
import { Http, Response} from "@angular/http";
import { ListFormViewConfig } from "../model/listFormViewConfig";
import { ColumnViewConfig } from "../model/columnViewConfig";
import { SettingsApi } from "./api/settingsApi";
import { String } from '../class/source';
import { SettingBrief } from "../model/settingBrief";
import { ColumnViewDeviceConfig } from "../model/columnViewDeviceConfig";
import { ColumnVisibility } from "../enviroment";


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
        public visibilty: string = ColumnVisibility.Default, public title: string = ""
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
        public visibilty: boolean = true,
        public disabled: boolean = false,
        public title: string = "") { }
    
}


@Injectable()
export class SettingService extends BaseService {

    constructor(public http: Http) {
        super(http);
    }

    public getSettingsCategories(apiUrl: string) {
        return this.http.get(apiUrl, this.options)
            .map(response => <any>(<Response>response));
    }

    public putSettingsCategories(apiUrl: string, list: Array<SettingBriefInfo>) {
        var body = JSON.stringify(list);
        return this.http.put(apiUrl, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    getListSettingsByUser(userId: number) {
        var url = String.Format(SettingsApi.ListSettingsByUser, userId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    getListSettingsByUserAndView(userId: number, viewId: number) {
        var url = String.Format(SettingsApi.ListSettingsByUserAndView, userId,viewId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
        
    }

    putUserSettings(userId: number, setting: ListFormViewConfig) {
        var url = String.Format(SettingsApi.PutSettingsByUserAndView, userId);
        var body = JSON.stringify(setting);
        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);

    }

}