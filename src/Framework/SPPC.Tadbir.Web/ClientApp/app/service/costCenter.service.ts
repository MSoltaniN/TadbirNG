import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';

import { CostCenter } from '../model/index';
import { HttpClient } from '@angular/common/http';


export class CostCenterInfo implements CostCenter {
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
}

@Injectable()
export class CostCenterService extends BaseService {

    constructor(public http: HttpClient) {
        super(http);
    }

}