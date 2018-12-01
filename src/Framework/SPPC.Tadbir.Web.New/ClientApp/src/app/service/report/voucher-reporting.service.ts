import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { ReportBaseService } from '../../class/report-base.service';

import { String } from '../../class/source';

import { HttpClient } from '@angular/common/http';


@Injectable()
export class VoucherReportingService extends ReportBaseService {


    constructor(public http: HttpClient) {
        super(http);
    }

    

}