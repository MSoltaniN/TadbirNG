import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Environment } from "../enviroment";
import { BaseService } from '../class/base.service';
import { LookupApi } from './api/index';
import { Company } from '../model/index';
import { GridOrderBy } from '../class/grid.orderby';
import { Filter } from '../class/filter';


export class CompanyInfo implements Company {
    parentId?: number | undefined;
    childCount: number = 0;
    id: number = 0;
    name: string;
    description?: string | undefined;

}

@Injectable()
export class CompanyService extends BaseService {

    constructor(public http: Http) {
        super(http);
    }

    public getAllByCompanyId(apiUrl: string, start?: number, count?: number, orderby?: string, filters?: Filter[]) {
        var headers = this.headers;
        var gridPaging = { pageIndex: start, pageSize: count };
        var sort = new Array<GridOrderBy>();
        if (orderby) {
            var orderByParts = orderby.split(' ');
            var fieldName = orderByParts[0];
            if (orderByParts[1] != 'undefined')
                sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
        }
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: sort };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });
        var result: any = null;
        var totalCount = 0;
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response));
    }

    getCompanies(userName: string, ticket: string) {
        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });
        this.headers.append('X-Tadbir-AuthTicket', ticket);
        if (ticket == '') return null;
        var jsonContext = atob(ticket);
        var context = JSON.parse(jsonContext);
        var userId = context.User.Id;
        var url = String.Format(LookupApi.UserAccessibleCompanies, userId);
        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());
    }

}