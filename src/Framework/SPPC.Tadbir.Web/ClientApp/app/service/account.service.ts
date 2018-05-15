import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';

import { Account } from '../model/index';


export class AccountInfo implements Account
{    
    constructor(public id: number = 0, public code: string = "", public name: string = "",
        public fiscalPeriodId: number = 0, public description: string = "",
        public branchId: number = 0, public level: number = 0, public fullCode: string = "0",
        public childCount: number = 0, public parentId: number = 0)
    { }
    
}


@Injectable()
export class AccountService extends BaseService
{   
    constructor(public http: Http) {
        super(http);
    }
}