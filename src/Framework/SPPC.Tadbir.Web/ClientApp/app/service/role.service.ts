import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Role, Permission, RoleFullViewModel } from '../model/index';
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


export class RoleInfo implements Role {
    constructor(public id: number = 0, public name: string = "", public description: string="", public permissions: string[] = []) { }
}

export class PermissionInfo implements Permission {
    id: number;
    groupId: number;
    groupName: string;
    isEnabled: boolean;
    name: string;
    flag: number;
    description?: string | undefined;
}

export class RoleFullViewModelInfo implements RoleFullViewModel {
    role: Role;
    permissions: Permission[];
}

@Injectable()
export class RoleService extends BaseService {

    private _getRolesUrl = Environment.BaseUrl + "/roles";
    private _getRoleFullViewModel = Environment.BaseUrl + "/roles/{0}";//roleId
    private _getNewRoleFullViewModel = Environment.BaseUrl + "/roles/new";
    private _postNewRoleUrl = Environment.BaseUrl + "/roles";
    private _putModifiedRolesUrl = Environment.BaseUrl + "/roles/{0}";//roleId
    private _deleteRoleUrl = Environment.BaseUrl + "/roles/{0}";//roleId

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
        var url = this._getRolesUrl;
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

    getNewRoleFullViewModel() {
        var url = this._getNewRoleFullViewModel;
        var options = new RequestOptions({ headers: this.headers });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());
    }

    getRoleFullViewModel(roleId: number) {
        var url = String.Format(this._getRoleFullViewModel, roleId);
        var options = new RequestOptions({ headers: this.headers });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());
    }

    editRole(roleFullViewModel: RoleFullViewModel): Observable<string> {
        var body = JSON.stringify(roleFullViewModel);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        var url = String.Format(this._putModifiedRolesUrl, roleFullViewModel.role.id);

        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertRole(roleFullViewModel: RoleFullViewModel): Observable<string> {
        var body = JSON.stringify(roleFullViewModel);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        return this.http.post(this._postNewRoleUrl, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(roleId: number): Observable<string> {

        var deleteByIdUrl = String.Format(this._deleteRoleUrl, roleId.toString());

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}