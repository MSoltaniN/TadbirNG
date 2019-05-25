import { Injectable } from '@angular/core';
import { Http, RequestOptions, Response } from '@angular/http';
import { BaseService } from '../class/base.service';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators/map';
import { AccountItemRelations, AccountItemBrief } from '../model/index';
import { Filter } from '../class/filter';
import { FilterExpression } from '../class/filterExpression';
import { HttpClient } from '@angular/common/http';



export class AccountItemRelationsInfo implements AccountItemRelations {
  id: number;
  relatedItemIds: number[];
}

export class AccountItemBriefInfo implements AccountItemBrief {
  id: number = 0;
  name: string = '';
  fullCode: string = '';
  isSelected: boolean;
  childCount: number;
  parentId?: number;
  code: string = '';
  level: number;
}

@Injectable()
export class AccountRelationsService extends BaseService {
  constructor(public http: HttpClient) {
    super(http);
  }

  public getChildrens(apiUrl: string) {
    var options = { headers: this.httpHeaders };
    return this.http.get(apiUrl, options)
      .map(response => <any>(<Response>response));
  }

  public getRelatedComponentModel(apiUrl: string, filter?: FilterExpression) {
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

  public getMainComponentModel(apiUrl: string, filter?: FilterExpression) {
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
