//import {catchError, map} from 'rxjs/operators';

import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseService } from "@sppc/shared/class";
import { String } from "@sppc/shared/class/source";
import { BrowserStorageService } from "@sppc/shared/services";
import { map } from "rxjs/operators";
import { OperationLog } from "../models";
import { OperationLogApi } from "./api";

export class OperationLogInfo implements OperationLog {
  userId: number;
  companyId: number;
  companyName: string;
  userName: string;
  id: number = 0;
  date: Date;
  time: string;
  entity: string;
  action: string;
  result: string;
  errorMessage: string;
  beforeState: string;
  afterState: string;
  fiscalPeriodId: number;
  branchId: number;
}

@Injectable()
export class OperationLogService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  postSelectedLogsAsArchived(fromDate: string, toDate: string) {
    var options = { headers: this.httpHeaders };
    var url =
      OperationLogApi.OperationLogsArchive +
      String.Format("?from={0}&to={1}", fromDate, toDate);
    return this.http.post(url, undefined, options).pipe(map((res) => res));
  }

  postSelectedSysLogsAsArchived(fromDate: string, toDate: string) {
    var options = { headers: this.httpHeaders };
    var url =
      OperationLogApi.SysOperationLogsArchive +
      String.Format("?from={0}&to={1}", fromDate, toDate);
    return this.http.post(url, undefined, options).pipe(map((res) => res));
  }

  putSelectedLogsAsArchived(ids: number[]) {
    var options = { headers: this.httpHeaders };
    let body = JSON.stringify({ paraph: "", items: ids });
    var url = OperationLogApi.OperationLogs;
    return this.http.put(url, body, options).pipe(map((res) => res));
  }

  putSelectedSysLogsAsArchived(ids: number[]) {
    var options = { headers: this.httpHeaders };
    let body = JSON.stringify({ paraph: "", items: ids });
    var url = OperationLogApi.SysOperationLogs;
    return this.http.put(url, body, options).pipe(map((res) => res));
  }

  PutSelectedArchivedLogsAsDeleted(ids: number[]) {
    var options = { headers: this.httpHeaders };
    let body = JSON.stringify({ paraph: "", items: ids });
    var url = OperationLogApi.OperationLogsArchive;
    return this.http.put(url, body, options).pipe(map((res) => res));
  }

  PutSelectedArchivedSysLogsAsDeleted(ids: number[]) {
    let body = JSON.stringify({ paraph: "", items: ids });
    var options = { headers: this.httpHeaders };
    var url = OperationLogApi.SysOperationLogsArchive;
    return this.http.put(url, body, options).pipe(map((res) => res));
  }
}
