import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FullAccount } from "@sppc/finance/models";
import { BaseService, String } from "@sppc/shared/class";
import { OperationId } from "@sppc/shared/enum/operationId";
import { BrowserStorageService } from "@sppc/shared/services";
import { map } from "rxjs";
import { CheckBook } from "../models/checkBook";
import { CheckBooksApi } from "./api/checkBooksApi";

export class CheckBookInfo {
  id: number = 0;
  checkBookNo: number;
  name: string;
  issueDate: Date;
  startNo: string;
  endNo: string;
  bankName: string;
  isArchived: boolean = false;
  branchId: number;
  accountId: number;
  detailAccountId: null;
  costCenterId: number;
  projectId: number;
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
}
