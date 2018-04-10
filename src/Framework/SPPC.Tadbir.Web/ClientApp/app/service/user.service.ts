import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { User } from '../model/index';
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
import { ToastrService } from 'toastr-ng2';
import { BaseService } from '../class/base.service';


export class UserInfo implements User {

    constructor(public id: number = 0, public userName: string = "", public personFirstName: string = "", public personLastName: string = "", public password: string = "",
        public isEnabled: boolean = false, public lastLoginDate: Date = new Date()) {

    }
}

@Injectable()
export class UserService extends BaseService {

    private _getUsersUrl = Environment.BaseUrl + "/users";
    private _postNewUsersUrl = Environment.BaseUrl + "/users";
    private _putModifiedUsersUrl = Environment.BaseUrl + "/users/{0}";

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {

        super();

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });
        this.headers.append('X-Tadbir-AuthTicket', this.Ticket);
        this.options = new RequestOptions({ headers: this.headers });

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
        var url = this._getUsersUrl;
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });

        var result: any = null;
        var totalCount = 0;

        var res = this.http.get(url, options)
            .map(response => <any>(<Response>response));

        return res;
    }

    editUser(user: User): Observable<string> {
        var body = JSON.stringify(user);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        var url = String.Format(this._putModifiedUsersUrl, user.id);

        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertUser(user: User): Observable<string> {
        var body = JSON.stringify(user);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        return this.http.post(this._postNewUsersUrl, body, options)
            .map(res => res)
            .catch(this.handleError);
    }



    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}