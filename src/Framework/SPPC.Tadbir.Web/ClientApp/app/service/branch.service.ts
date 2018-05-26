import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { BaseService } from '../class/base.service';
import { LookupApi } from './api/index';
import { Branch } from '../model/index';
import { GridOrderBy } from '../class/grid.orderby';
import { Filter } from '../class/filter';


export class BranchInfo implements Branch {
    companyId: number = 0;
    isAccessible: boolean;
    parentId?: number | undefined;
    childCount: number = 0;
    id: number = 0;
    name: string;
    description?: string | undefined;
    level: number = 0;

}

@Injectable()
export class BranchService extends BaseService {


    constructor(public http: Http) {
        super(http);
    }

    getBranches(companyId: number, ticket: string) {
        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });
        this.headers.append('X-Tadbir-AuthTicket', ticket);
        if (ticket == '') return null;
        var url = String.Format(LookupApi.UserAccessibleCompanyBranches, companyId, this.UserId);
        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());
    }

}