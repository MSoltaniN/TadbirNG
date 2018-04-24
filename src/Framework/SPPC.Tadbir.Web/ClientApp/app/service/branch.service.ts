import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Environment } from "../enviroment";
import { BaseService } from '../class/base.service';

@Injectable()
export class BranchService extends BaseService {
    
    private getBranchUrl = Environment.BaseUrl + "/lookup/branches/company/{0}/user/{1}"; //{0}=companyId  , {1}=userId
    
    constructor(private http: Http) {
        super();
    }


    getBranches(companyId : number) {
        
        var url = String.Format(this.getBranchUrl, companyId, this.UserId);
        
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }


}