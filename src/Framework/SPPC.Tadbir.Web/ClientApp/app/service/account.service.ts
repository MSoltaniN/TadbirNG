import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { BaseService } from '../class/base.service';

import { Account } from '../model/index';
import { FiscalPeriodApi } from './api/index';
import { String } from '../class/source';
import { AccountApi } from './api/accountApi';
import { Environment } from '../enviroment';
import { HttpHeaders, HttpClient, HttpResponse } from '@angular/common/http';
import { EnviromentComponent } from '../class/enviroment.component';
import { FilterExpression } from '../class/filterExpression';
import { GridOrderBy } from '../class/grid.orderby';
import { Observable } from 'rxjs/Observable';

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

    constructor(public http: HttpClient) {
        super(http);
    }


    getAccountById(id: number) {
        var url = String.Format(AccountApi.Account, id);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }
    

    getAccountFullCode(parentId: number) {
        var url = String.Format(AccountApi.AccountFullCode, parentId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

}