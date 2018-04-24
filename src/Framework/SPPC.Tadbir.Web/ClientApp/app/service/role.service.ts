import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Role, Permission, RoleFullViewModel, RoleUsersViewModel, UserBriefViewModel, BranchViewModel, RoleBranchesViewModel, RoleDetailsViewModel} from '../model/index';
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

export class RoleUsersViewModelInfo implements RoleUsersViewModel {
    id: number;
    name: string;
    users: Array<UserBriefViewModel>;
}

export class BranchViewModelInfo implements BranchViewModel {
    id: number;
    name: string;
    description?: string;
    level: number;
    companyId: number;
    isAccessible: boolean;
}

export class RoleBranchesViewModelInfo implements RoleBranchesViewModel {
    id: number;
    name: string;
    branches: Array<BranchViewModel>;
}

export class RoleDetailsViewModelInfo implements RoleDetailsViewModel {
    role: Role;
    permissions: Array<Permission>;
    branches: Array<BranchViewModel>;
    users: Array<UserBriefViewModel>;
}




@Injectable()
export class RoleService extends BaseService {

    private _getRolesUrl = Environment.BaseUrl + "/roles";
    private _getRoleFullViewModel = Environment.BaseUrl + "/roles/{0}";//roleId
    private _getNewRoleFullViewModel = Environment.BaseUrl + "/roles/new";
    private _postNewRoleUrl = Environment.BaseUrl + "/roles";
    private _putModifiedRolesUrl = Environment.BaseUrl + "/roles/{0}";//roleId
    private _deleteRoleUrl = Environment.BaseUrl + "/roles/{0}";//roleId
    //users
    private _getRoleUsersUrl = Environment.BaseUrl + "/roles/{0}/users";//roleId
    private _putModifiedRoleUsersUrl = Environment.BaseUrl + "/roles/{0}/users";//roleId
    //branches
    private _getRoleBranchesUrl = Environment.BaseUrl + "/roles/{0}/branches";//roleId
    private _putModifiedRoleBranchesUrl = Environment.BaseUrl + "/roles/{0}/branches";//roleId
    //detail
    private _getRoleDetailUrl = Environment.BaseUrl + "/roles/{0}/details";//roleId
    
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
        var url = this._getRolesUrl;
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

    getNewRoleFullViewModel() {
        var url = this._getNewRoleFullViewModel;
      
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    getRoleFullViewModel(roleId: number) {
        var url = String.Format(this._getRoleFullViewModel, roleId);
       
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    editRole(roleFullViewModel: RoleFullViewModel): Observable<string> {
        var body = JSON.stringify(roleFullViewModel);
        
        var url = String.Format(this._putModifiedRolesUrl, roleFullViewModel.role.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertRole(roleFullViewModel: RoleFullViewModel): Observable<string> {
        var body = JSON.stringify(roleFullViewModel);
       
        return this.http.post(this._postNewRoleUrl, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(roleId: number): Observable<string> {

        var deleteByIdUrl = String.Format(this._deleteRoleUrl, roleId.toString());

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    getRoleUsers(roleId: number) {
        var url = String.Format(this._getRoleUsersUrl, roleId);
        

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedRoleUsers(roleUsersViewModel: RoleUsersViewModel) {
        var body = JSON.stringify(roleUsersViewModel);
        var headers = this.headers;
        

        var url = String.Format(this._putModifiedRoleUsersUrl, roleUsersViewModel.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleBranches(roleId: number) {
        var url = String.Format(this._getRoleBranchesUrl, roleId);
        

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedRoleBranches(roleBranchesViewModel: RoleBranchesViewModel) {

        var body = JSON.stringify(roleBranchesViewModel);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        var url = String.Format(this._putModifiedRoleBranchesUrl, roleBranchesViewModel.id);

        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleDetail(roleId: number) {
        var url = String.Format(this._getRoleDetailUrl, roleId);       

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}