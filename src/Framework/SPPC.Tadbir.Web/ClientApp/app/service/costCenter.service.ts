import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { CostCenter } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";
import { GridOrderBy } from "../class/grid.orderby";
import { HttpParams } from "@angular/common/http";
import { Environment, MessageType } from "../enviroment";
import { Context } from "../model/context";

import { BaseComponent } from "../class/base.component"
import { ToastrService } from 'ngx-toastr';
import { BaseService } from '../class/base.service';

export class CostCenterInfo implements CostCenter {
    id: number = 0;
    code: string;
    fullCode: string;
    name: string;
    level: number = 0;
    description?: string | undefined;
    parentId?: number | undefined;
    childCount: number = 0;
    fiscalPeriodId: number = 0;
    branchId: number = 0;
}

@Injectable()
export class CostCenterService extends BaseService {

    private _getCostCentersUrl = Environment.BaseUrl + "/ccenters/fp/{0}/branch/{1}";//fpId,branchId
    private _postNewCostCenterUrl = Environment.BaseUrl + "/ccenters";
    private _putModifiedCostCenterUrl = Environment.BaseUrl + "/ccenters/{0}";//cCenterId
    private _getCostCenterByIdUrl = Environment.BaseUrl + "/ccenters/{0}";//cCenterId
    private _deleteCostCenterUrl = Environment.BaseUrl + "/ccenters/{0}";//cCenterId

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {

        super();

    }

    search(start?: number, count?: number, orderby?: string, filters?: Filter[]) {
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
        var url = String.Format(this._getCostCentersUrl, this.FiscalPeriodId, this.BranchId);
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });

        var result: any = null;
        var totalCount = 0;

        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    editCostCenter(model: CostCenter): Observable<string> {
        var body = JSON.stringify(model);

        var url = String.Format(this._putModifiedCostCenterUrl, model.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertCostCenter(model: CostCenter): Observable<string> {
        var body = JSON.stringify(model);

        return this.http.post(this._postNewCostCenterUrl, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(costCenterId: number): Observable<string> {

        var deleteByIdUrl = String.Format(this._deleteCostCenterUrl, costCenterId.toString());

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    getCostCenterById(costCenterId: number) {
        var url = String.Format(this._getCostCenterByIdUrl, costCenterId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }


    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}