import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Environment } from "../enviroment";

@Injectable()
export class BranchService {
    
    private getBranchUrl = Environment.BaseUrl + "/lookup/branches/company/{0}/user/{1}"; //{0}=companyId  , {1}=userId

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {

    }


    getBranches(companyId : number) {
        
        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8'});        
       
        var userId = '';        

        if (localStorage.getItem('currentContext')) {
            const userJson = localStorage.getItem('currentContext');
            var currentUser = userJson !== null ? JSON.parse(userJson) : null;
            
            if(currentUser != null)
            {
                this.headers.append('X-Tadbir-AuthTicket', currentUser.ticket);       

                var jsonContext = atob(currentUser.ticket);
                var context = JSON.parse(jsonContext);

                userId = context.User.Id;
            }
            

        }

        var url = String.Format(this.getBranchUrl, companyId, userId);
        
        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());
    }


}