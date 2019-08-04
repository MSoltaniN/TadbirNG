import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import "rxjs/Rx";
import { HttpClient } from "@angular/common/http";
import { Permission } from '@sppc/core';
import { RelatedItems, RelatedItem, BrowserStorageService } from '@sppc/shared';
import { String } from '@sppc/shared/class';
import { Branch } from '@sppc/organization';
import { BaseService } from '@sppc/shared/class';
import { Role, RoleFull, RoleDetails, UserBrief } from '../models';
import { RoleApi } from './api';



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

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

    getNewRoleFull() {
        var url = RoleApi.NewRole;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    getRoleFull(roleId: number) {
        var url = String.Format(RoleApi.Role, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }


    getRoleUsers(roleId: number) {
        var url = String.Format(RoleApi.RoleUsers, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    modifiedRoleUsers(roleUsers: RelatedItems) {
        var body = JSON.stringify(roleUsers);
        
        var options = { headers: this.httpHeaders };

        var url = String.Format(RoleApi.RoleUsers, roleUsers.id);

        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleBranches(roleId: number) {
        var url = String.Format(RoleApi.RoleBranches, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    modifiedRoleBranches(roleBranches: RelatedItems) {
        var body = JSON.stringify(roleBranches);
        var options = { headers: this.httpHeaders };
        var url = String.Format(RoleApi.RoleBranches, roleBranches.id);
        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleFiscalPeriods(roleId: number) {
        var url = String.Format(RoleApi.RoleFiscalPeriods, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    modifiedRoleFiscalPeriods(roleFPeriods: RelatedItems) {
        var body = JSON.stringify(roleFPeriods);
        var options = { headers: this.httpHeaders };
        var url = String.Format(RoleApi.RoleFiscalPeriods, roleFPeriods.id);
        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    getRoleDetail(roleId: number) {
        var url = String.Format(RoleApi.RoleDetails, roleId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

}
