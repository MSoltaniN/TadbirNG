import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FullAccount } from "@sppc/finance/models";
import { BaseService, String } from "@sppc/shared/class";
import { OperationId } from "@sppc/shared/enum/operationId";
import { BrowserStorageService } from "@sppc/shared/services";
import { map } from "rxjs";
import { CheckBook, CheckBookPage } from "../models/checkBook";
import { CheckBooksApi } from "./api/checkBooksApi";

export class CheckBookInfo implements CheckBook{
  id: number = 0;
  textNo: number;
  name: string;
  issueDate: Date;
  startNo: string;
  endNo: string;
  bankName: string;
  isArchived: boolean = false;
  branchId: number;
  fullAccount: FullAccount;
  pageCount: number;
  hasNext: boolean;
  hasPrevious: boolean;
}
@Injectable({
  providedIn: "root",
})
export class CheckBookService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  getPages(id: number, listChanged: boolean = false) {
    var postItem = {
      listChanged: listChanged,
      operation: OperationId.View,
    };
    var headers = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (headers) headers = headers.append("X-Tadbir-GridOptions", base64Body);
    var url = String.Format(CheckBooksApi.CheckBookPages, id);
    var options = { headers: headers };

    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  insertPages(id: number, listChanged: boolean = true) {
    var postItem = {
      listChanged: listChanged,
      operation: OperationId.View,
    };
    var headers = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (headers) headers = headers.append("X-Tadbir-GridOptions", base64Body);
    var url = String.Format(CheckBooksApi.CheckBookPages, id);
    var options = { headers: headers };

    return this.http
      .post(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  updateCheck(apiUrl:string,data?:any,listChanged: boolean = true) {
    var postItem = {
      listChanged: listChanged
    };
    var headers = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (headers) headers = headers.append("X-Tadbir-GridOptions", base64Body);
    var options = { headers: headers };

    return this.http
      .put(apiUrl,data, options)
      .pipe(map((response) => <any>(<Response>response)));
  }
}
