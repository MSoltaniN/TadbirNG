import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
    
import { Environment } from "../../enviroment";

import { Context } from "../../model/context";

export class ContextInfo implements Context {
    constructor( public userName: string = "", public password: string = "",
        public firstName: string = "", public lastName: string = "", public ticket: string = '',
        public fpId:number = 0,public branchId:number = 0,public companyId:number = 0)
    { }

}

@Injectable()
export class AuthenticationService {

    
    constructor(private http: Http)
    {
           
    }

    login(username: string, password: string, remember:boolean) {
        return this.http.put(Environment.BaseUrl + '/users/login', { username: username, password: password }/*, this.options*/)
            .map((response: Response) => {
                // login successful if there's a jwt token in the response           
                if (response.headers != null) {
                    let ticket = response.headers.get('X-Tadbir-AuthTicket');
                    if (response.status == 200 && ticket != null) {
                        var user = new ContextInfo();
                        user.ticket = ticket;
                        user.userName = username;

                        // در صورتی که تیک بخاطر سپردن بخورد در حافظه storage ذخیره می شود
                        if(remember)
                            localStorage.setItem('currentContext', JSON.stringify(user));
                        else // در صورتی که تیک بخاطر سپردن بخورد در حافظه session ذخیره می شود
                            sessionStorage.setItem('currentContext', JSON.stringify(user));
                    }
                }              
            })              
           
    }

    islogin()
    {
        if (localStorage.getItem('currentContext'))
        {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : ""); 
            if (currentContext.userName != '')
            {
                return true;
            }
        }
        else if (sessionStorage.getItem('currentContext')) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            if (currentContext.userName != '') {
                return true;
            }
        }

        return false;
    }

    isRememberMe() {
        if (localStorage.getItem('currentContext')) {            
            return true;            
        }        

        return false;
    }

    getCurrentUser(): ContextInfo | null {
        var currentUser: ContextInfo;
        var item: string | null = '';
        if (localStorage.getItem('currentContext')) {
            item = localStorage.getItem('currentContext');
        }
        else if (sessionStorage.getItem('currentContext')) {
            item = sessionStorage.getItem('currentContext');
        }    

        if (item) {
            var currentUser: ContextInfo = item !== null ? JSON.parse(item) : null;
            return currentUser;
        }

        return null;
    }

    logout() {
        // remove user from local storage to log user out
        if (localStorage.getItem('currentContext'))
            localStorage.removeItem('currentContext');

        if (sessionStorage.getItem('currentContext'))
            sessionStorage.removeItem('currentContext');
    }
}