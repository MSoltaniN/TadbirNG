import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { BaseService } from '../class/base.service';
import { HttpHeaders, HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class FullCodeService extends BaseService
{      

    constructor(public http: HttpClient) {
        super(http);
    }


    getFullCode(apiUrl: string) {
        var options = { headers: this.httpHeaders };
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response));
    }

}