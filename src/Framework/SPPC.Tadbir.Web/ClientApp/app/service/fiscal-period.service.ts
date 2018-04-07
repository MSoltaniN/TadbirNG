import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Environment } from "../enviroment";
import { BaseService } from '../class/base.service';

@Injectable()
export class FiscalPeriodService extends BaseService{

    
    private getFiscalPeriodUrl = Environment.BaseUrl +"/lookup/fps/company/1";

    private getFiscalUrl =  Environment.BaseUrl + "/lookup/fps/company/{0}/user/{1}";

    headers: Headers;

    constructor(private http: Http) {
        super();
    }


    //getFiscalPeriods() {
    //    var headers = new Headers();
        
    //    headers.append("Content-Type", "application/json");

    //    headers.append("X-Tadbir-AuthTicket", "eyJVc2VyIjp7IklkIjoxLCJQZXJzb25GaXJzdE5hbWUiOiIiLCJQZXJzb25MYXN0TmFtZSI6IiIsIkJyYW5jaGVzIjpbMSwyXSwiUm9sZXMiOlsxXSwiUGVybWlzc2lvbnMiOlt7IkVudGl0eU5hbWUiOiJBY2NvdW50IiwiRmxhZ3MiOjE1fSx7IkVudGl0eU5hbWUiOiJUcmFuc2FjdGlvbiIsIkZsYWdzIjoxMDIzfSx7IkVudGl0eU5hbWUiOiJVc2VyIiwiRmxhZ3MiOjd9LHsiRW50aXR5TmFtZSI6IlJvbGUiLCJGbGFncyI6NjN9LHsiRW50aXR5TmFtZSI6IlJlcXVpc2l0aW9uVm91Y2hlciIsIkZsYWdzIjoxMjd9LHsiRW50aXR5TmFtZSI6Iklzc3VlUmVjZWlwdFZvdWNoZXIiLCJGbGFncyI6NjN9LHsiRW50aXR5TmFtZSI6IlNhbGVzSW52b2ljZSIsIkZsYWdzIjozMX0seyJFbnRpdHlOYW1lIjoiUHJvZHVjdEludmVudG9yeSIsIkZsYWdzIjoxNX1dfX0=");
        
    //    return this.http.get(this.getFiscalPeriodUrl, { headers: headers })
    //        .map(response => <any>(<Response>response).json());
    //}


    getFiscalPeriod(companyId : number) {

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8'});        
       
        var userId = '';        

        //if (localStorage.getItem('currentContext')) {
        //    const userJson = localStorage.getItem('currentContext');
        //    var currentUser = userJson !== null ? JSON.parse(userJson) : null;
            
        //    if(currentUser != null)
        //    {
        //        var jsonContext = atob(currentUser.ticket);
        //        var context = JSON.parse(jsonContext);

        //        userId = context.User.Id;
               
        //        this.headers.append('X-Tadbir-AuthTicket', currentUser.ticket);       
        //    }

        //}

        if (this.Ticket != '')
        {
            this.headers.append('X-Tadbir-AuthTicket', this.Ticket);       
        }

        var url = String.Format(this.getFiscalUrl, companyId,this.UserId);
        
        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());
    }

}