import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { User, UserProfile } from '../model/index';
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


export class UserInfo implements User {
    personFirstName: string;
    personLastName: string;
    id: number=0;
    userName: string;
    password: string;
    lastLoginDate?: Date | undefined;
    isEnabled: boolean = false;
}

export class UserProfileInfo implements UserProfile {
    userName: string;
    oldPassword: string;
    newPassword: string;
    repeatPassword: string;
}

@Injectable()
export class UserService extends BaseService {

    private _getUsersUrl = Environment.BaseUrl + "/users";
    private _postNewUsersUrl = Environment.BaseUrl + "/users";
    private _putModifiedUsersUrl = Environment.BaseUrl + "/users/{0}";
    private _getUserByIdUrl = Environment.BaseUrl + "/users/{0}";
    private _putChangePassword = Environment.BaseUrl + "/users/{0}/password";//username

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
       
        var url = String.Format(this._putModifiedUsersUrl, user.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertUser(user: User): Observable<string> {
        var body = JSON.stringify(user);
   
        return this.http.post(this._postNewUsersUrl, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    getUserById(userId: number) {
        var url = String.Format(this._getUserByIdUrl, userId);
       
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    changePassword(userProfile: UserProfile): Observable<string> {
        var body = JSON.stringify(userProfile);
        
        var url = String.Format(this._putChangePassword, userProfile.userName);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}