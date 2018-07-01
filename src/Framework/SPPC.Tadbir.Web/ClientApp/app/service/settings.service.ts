import { Injectable } from "@angular/core";
import { BaseService } from "../class/base.service";
import { Http, Response} from "@angular/http";
import { ListFormViewConfig } from "../model/listFormViewConfig";
import { ColumnViewConfig } from "../model/columnViewConfig";
import { SettingsApi } from "./api/settingsApi";
import { String } from '../class/source';


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