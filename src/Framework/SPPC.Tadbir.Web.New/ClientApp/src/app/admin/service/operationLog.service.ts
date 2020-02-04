import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '@sppc/shared/class';
import { BrowserStorageService } from '@sppc/shared/services';
import { OperationLog } from '../models';
import { String } from '@sppc/shared/class/source';
import { OperationLogApi } from './api';


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

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  postSelectedLogsAsArchived(fromDate: string, toDate: string) {    
    var options = { headers: this.httpHeaders };
    var url = OperationLogApi.OperationLogsArchiveUrl + String.Format("?from={0}&to={1}", fromDate, toDate);
    return this.http.post(url,undefined,options)
      .map(res => res)
      .catch(this.handleError);
  }
}
