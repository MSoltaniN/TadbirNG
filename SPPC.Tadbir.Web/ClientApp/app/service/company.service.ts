import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Environment } from "../enviroment";

@Injectable()
export class CompanyService {

    
    private getCompanyUrl = Environment.BaseUrl + "/lookup/companies/user/{0}";

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {

    }


    getCompanies(userName : string,ticket : string) {
        
        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });
        this.headers.append('X-Tadbir-AuthTicket', ticket);

        var jsonContext = atob(ticket);
        var context = JSON.parse(jsonContext);

        var userId = context.User.Id;
        var url = String.Format(this.getCompanyUrl, userId);
        
        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());
    }


}