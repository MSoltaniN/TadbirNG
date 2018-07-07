import { Injectable } from "@angular/core";
import { BaseService } from "../class/base.service";
import { Http, Response} from "@angular/http";
import { ListFormViewConfig } from "../model/listFormViewConfig";
import { ColumnViewConfig } from "../model/columnViewConfig";
import { SettingsApi } from "./api/settingsApi";
import { String } from '../class/source';
import { SettingBrief } from "../model/settingBrief";


export class SettingBriefInfo implements SettingBrief {
    modelType: string;
    id: number;
    title: string;
    description?: string | undefined;
    values: Object;

}

export class SettingTreeNodeInfo {
    constructor(public id: number = 0,
        public parentId: number | undefined,
        public title: string,
        public description: string | undefined,
        public modelType: string | undefined) { }
}

export class ListFormViewConfigInfo implements ListFormViewConfig {
    viewId: number;
    pageSize: number;
    columnViews: ColumnViewConfig[];    
}

export class ColumnViewConfigInfo implements ColumnViewConfig {
    name: string;
    width?: number | undefined;
    index?: number | undefined;
    visibilty: string;
    viewId: number;
    pageSize: number;
    columnViews: ColumnViewConfig[];
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

}