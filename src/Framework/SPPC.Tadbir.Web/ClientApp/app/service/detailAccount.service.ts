import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';

import { DetailAccount } from '../model/index';
import { HttpClient } from '@angular/common/http';


export class DetailAccountInfo implements DetailAccount {
    id: number = 0;
    code: string;
    fullCode: string;
    name: string;
    level: number = 0;
    description?: string | undefined;
    parentId?: number | undefined;
    childCount: number = 0;
    fiscalPeriodId: number = 0;
    branchId: number = 0;
    companyId: number;
    branchScope: number = 0;
}

@Injectable()
export class DetailAccountService extends BaseService {

    constructor(public http: HttpClient) {
        super(http);
    }

}