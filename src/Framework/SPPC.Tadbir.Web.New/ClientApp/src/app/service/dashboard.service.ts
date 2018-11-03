import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';

import { CostCenter } from '../model/index';
import { HttpClient } from '@angular/common/http';
import { DashboardApi } from './api/dashboardApi';
import { String } from '../class/source';


@Injectable()
export class DashboardService extends BaseService {

    constructor(public http: HttpClient) {
        super(http);
    }

    getDashboardInfo() {
        var url = DashboardApi.SummariesUrl;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

}