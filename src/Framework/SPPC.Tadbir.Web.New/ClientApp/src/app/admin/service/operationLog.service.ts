import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '@sppc/shared/class';
import { OperationLog } from '..';
import { BrowserStorageService } from '@sppc/shared';



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
}
