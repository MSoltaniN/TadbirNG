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