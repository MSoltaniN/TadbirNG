import { Injectable } from '@angular/core';
import { Http, RequestOptions, Response } from '@angular/http';
import { BaseService } from '../class/base.service';
import { RowPermissionsForRole, ViewRowPermission } from '../model/index';
import { FilterExpression } from '../class/filterExpression';

export interface Item {
    key: number,
    value: string
}

export class ItemInfo implements Item {
    key: number = 0;
    value: string = '';
}

export class RowPermissionsForRoleInfo implements RowPermissionsForRole {
    id: number;
    rowPermissions: ViewRowPermission[];
}

export class ViewRowPermissionInfo implements ViewRowPermission {
    roleId: number;
    viewId: number;
    id: number;
    accessMode: string;
    value: number;
    value2: number;
    textValue: string;
    items: number[]=[];
}

@Injectable()
export class ViewRowPermissionService extends BaseService {

    constructor(public http: Http) {
        super(http);
    }

    public getRowList(apiUrl: string) {
        return this.http.get(apiUrl, this.options)
            .map(response => <any>(<Response>response).json());
    }
}