import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService, String } from '@sppc/shared/class';
import { OperationId } from '@sppc/shared/enum/operationId';
import { BrowserStorageService } from '@sppc/shared/services';
import { map } from 'rxjs';
import { CheckBook } from '../models/checkBook';
import { CheckBooksApi } from './api/checkBooksApi';

export class CheckBookInfo implements CheckBook {
  id: number = 0;
  checkBookNo: number;
  name: string;
  issueDate: string;
  startNo: string;
  endNo: string;
  bankName: string;
  isArchived: boolean = false;
  branchId: number;
  accountId: number;
  detailId: number;
  costCenterId: number;
  projectId: number;
}
@Injectable({
  providedIn: 'root'
})
export class CheckBookService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  getPages(id:number,listChanged:boolean = false) {
    var postItem = {
      listChanged: listChanged,
      operation: OperationId.View,
    };
    var headers = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (headers)
      headers = headers.append("X-Tadbir-GridOptions", base64Body);
    var url = String.Format(CheckBooksApi.CheckBookPages, id);
    var options = { headers: headers};

    return this.http.get(url,options)
    .pipe(map((response) => <any>(<Response>response)));
  }
}
