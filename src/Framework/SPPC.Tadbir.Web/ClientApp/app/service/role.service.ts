import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Role, Permission, RoleFull, RoleUsers, UserBrief, Branch, RoleBranches, RoleDetails } from '../model/index';
import { RoleApi } from './api/index';
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
    permissions: string[] = [];
    id: number = 0;
    name: string;
    description?: string | undefined;
    //constructor(public id: number = 0, public name: string = "", public description: string="", public permissions: string[] = []) { }
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

export class RoleFullInfo implements RoleFull {
    role: Role;
    permissions: Permission[];
}

export class RoleUsersInfo implements RoleUsers {
    id: number;
    name: string;
    users: Array<UserBrief>;
}

export class BranchInfo implements Branch {
    id: number;
    name: string;
    description?: string;
    level: number;
    companyId: number;
    isAccessible: boolean;
}

export class RoleBranchesInfo implements RoleBranches {
    id: number;
    name: string;
    branches: Array<Branch>;
}

export class RoleDetailsInfo implements RoleDetails {
    role: Role;
    permissions: Array<Permission>;
    branches: Array<Branch>;
    users: Array<UserBrief>;
}




@Injectable()
export class RoleService extends BaseService {

    //private _getRolesUrl = Environment.BaseUrl + "/roles";
    //private _getRoleFull = Environment.BaseUrl + "/roles/{0}";//roleId
    //private _getNewRoleFull = Environment.BaseUrl + "/roles/new";
    //private _postNewRoleUrl = Environment.BaseUrl + "/roles";
    //private _putModifiedRolesUrl = Environment.BaseUrl + "/roles/{0}";//roleId
    //private _deleteRoleUrl = Environment.BaseUrl + "/roles/{0}";//roleId
    ////users
    //private _getRoleUsersUrl = Environment.BaseUrl + "/roles/{0}/users";//roleId
    //private _putModifiedRoleUsersUrl = Environment.BaseUrl + "/roles/{0}/users";//roleId
    ////branches
    //private _getRoleBranchesUrl = Environment.BaseUrl + "/roles/{0}/branches";//roleId
    //private _putModifiedRoleBranchesUrl = Environment.BaseUrl + "/roles/{0}/branches";//roleId
    ////detail
    //private _getRoleDetailUrl = Environment.BaseUrl + "/roles/{0}/details";//roleId

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
        var url = RoleApi.Roles;
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));

        if (searchHeaders)
            searchHeaders.set('X-Tadbir-GridOptions', base64Body);

        var options = new RequestOptions({ headers: searchHeaders });

        var result: any = null;
        var totalCount = 0;

        var res = this.http.get(url, options)
            .map(response => <any>(<Response>response));

        return res;
    }

    getNewRoleFull() {
        var url = RoleApi.NewRole;

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    getRoleFull(roleId: number) {
        var url = String.Format(RoleApi.Role, roleId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    editRole(roleFull: RoleFull): Observable<string> {
        var body = JSON.stringify(roleFull);

        var url = String.Format(RoleApi.Role, roleFull.role.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertRole(roleFull: RoleFull): Observable<string> {
        var body = JSON.stringify(roleFull);

        return this.http.post(RoleApi.Roles, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(roleId: number): Observable<string> {

        var deleteByIdUrl = String.Format(RoleApi.Role, roleId.toString());

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    getRoleUsers(roleId: number) {
        var url = String.Format(RoleApi.RoleUsers, roleId);


        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedRoleUsers(roleUsers: RoleUsers) {
        var body = JSON.stringify(roleUsers);
        var headers = this.headers;


        var url = String.Format(RoleApi.RoleUsers, roleUsers.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleBranches(roleId: number) {
        var url = String.Format(RoleApi.RoleBranches, roleId);


        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedRoleBranches(roleBranches: RoleBranches) {

        var body = JSON.stringify(roleBranches);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        var url = String.Format(RoleApi.RoleBranches, roleBranches.id);

        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleDetail(roleId: number) {
        var url = String.Format(RoleApi.RoleDetails, roleId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}