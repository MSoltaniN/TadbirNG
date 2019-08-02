import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { BaseService } from '../class/base.service';
import { RowPermissionsForRole, ViewRowPermission } from '../model/index';
import { FilterExpression } from '../class/filterExpression';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from './browserStorage.service';

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
  items: number[] = [];
}

@Injectable()
export class ViewRowPermissionService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  //public getRowList(apiUrl: string) {
  //    return this.http.get(apiUrl, this.options)
  //        .map(response => <any>(<Response>response).json());
  //}

  public getRowList(apiUrl: string, filter?: FilterExpression) {
    var intMaxValue = 2147483647
    var gridPaging = { pageIndex: 1, pageSize: intMaxValue };
    var postItem = { Paging: gridPaging, filter: filter, sortColumns: null };
    var searchHeaders = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (searchHeaders)
      searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);
    var options = { headers: searchHeaders };

    return this.http.get(apiUrl, options)
      .map(response => <any>(<Response>response));
  }
}