import { Injectable } from '@angular/core';
import { Http} from '@angular/http';
import { BaseService } from '../class/base.service';

import { Voucher, DocumentAction } from '../model/index';
import { HttpClient } from '@angular/common/http';


export class VoucherInfo implements Voucher {
    fiscalPeriodId: number = 0;
    branchId: number = 0;
    workItemId: number;
    workItemTargetId: number;
    workItemAction: string;
    debitSum: number;
    creditSum: number;
    document: {
        typeId: number;
        statusId: number;
        statusName: string;
        actions: Array<DocumentAction>;
        id: number;
        entityNo: string;
        no: string;
        operationalStatus: string;
    };
    id: number = 0;
    no: string;
    date: Date;
    description?: string | undefined;
}

@Injectable()
export class VoucherService extends BaseService {
    constructor(public http: HttpClient) {
        super(http);
    }
}