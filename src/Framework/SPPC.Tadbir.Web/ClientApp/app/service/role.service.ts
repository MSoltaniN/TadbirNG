import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Role, Permission, RoleFull, UserBrief, Branch, RoleDetails, RelatedItems, RelatedItem } from '../model/index';
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
    flag: number;
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
    id: number = 0;
    role: Role;
    permissions: Permission[];
}

export class RelatedItemsInfo implements RelatedItems {
    id: number;
    relatedItems: RelatedItem[];
}

export class RoleDetailsInfo implements RoleDetails {
    role: Role;
    permissions: Array<Permission>;
    branches: Array<Branch>;
    users: Array<UserBrief>;
}

@Injectable()
export class RoleService extends BaseService {

    constructor(public http: Http) {
        super(http);
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


    getRoleUsers(roleId: number) {
        var url = String.Format(RoleApi.RoleUsers, roleId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedRoleUsers(roleUsers: RelatedItems) {
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

    modifiedRoleBranches(roleBranches: RelatedItems) {
        var body = JSON.stringify(roleBranches);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });
        var url = String.Format(RoleApi.RoleBranches, roleBranches.id);
        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleFiscalPeriods(roleId: number) {
        var url = String.Format(RoleApi.RoleFiscalPeriods, roleId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedRoleFiscalPeriods(roleFPeriods: RelatedItems) {
        var body = JSON.stringify(roleFPeriods);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });
        var url = String.Format(RoleApi.RoleFiscalPeriods, roleFPeriods.id);
        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleDetail(roleId: number) {
        var url = String.Format(RoleApi.RoleDetails, roleId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

}