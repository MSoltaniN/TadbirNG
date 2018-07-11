import { Injectable } from '@angular/core';
import { Http, RequestOptions, Response } from '@angular/http';
import { BaseService } from '../class/base.service';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators/map';
import { AccountItemRelations, AccountItemBrief } from '../model/index';
import { Filter } from '../class/filter';



export class AccountItemRelationsInfo implements AccountItemRelations {
    id: number;
    relatedItemIds: number[];
}

export class AccountItemBriefInfo implements AccountItemBrief {
    id: number = 0;
    name: string;
    fullCode: string;
    isSelected: boolean;
    childCount: number;
}

@Injectable()
export class AccountRelationsService extends BaseService {
    constructor(public http: Http) {
        super(http);
    }

    public getChildrens(apiUrl: string) {
        var options = new RequestOptions({ headers: this.headers });
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response).json());
    }

    public getRelatedComponentModel(apiUrl: string, filters?: Filter[]) {
        var intMaxValue = 2147483647
        var gridPaging = { pageIndex: 1, pageSize: intMaxValue };
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: null };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response).json());
    }

    public getMainComponentModel(apiUrl: string, filters?: Filter[]) {
        var intMaxValue = 2147483647
        var gridPaging = { pageIndex: 1, pageSize: intMaxValue };
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: null };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response));
    }

}