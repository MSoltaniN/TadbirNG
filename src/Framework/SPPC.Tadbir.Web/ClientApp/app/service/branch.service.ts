import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { BaseService } from '../class/base.service';
import { Branch, RelatedItems } from '../model/index';
import { String } from '../class/source';
import { BranchApi } from './api/index';


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

    getBranchRoles(branchId: number) {
        var url = String.Format(BranchApi.BranchRoles, branchId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedBranchRoles(branchRoles: RelatedItems) {
        var body = JSON.stringify(branchRoles);
        var headers = this.headers;
        var url = String.Format(BranchApi.BranchRoles, branchRoles.id);
        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

}