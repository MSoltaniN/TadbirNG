import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';

import { Project, OperationLog } from '../model/index';
import { HttpClient } from '@angular/common/http';


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

    constructor(public http: HttpClient) {
        super(http);
    }
}