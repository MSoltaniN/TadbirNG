import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';
import { FiscalPeriod } from '../model/index';


export class FiscalPeriodInfo implements FiscalPeriod {
    companyId: number;
    id: number=0;
    name: string;
    startDate: Date = new Date();
    endDate: Date = new Date();
    description?: string | undefined;

}

@Injectable()
export class FiscalPeriodService extends BaseService{

    constructor(public http: Http) {
        super(http);
    }


}