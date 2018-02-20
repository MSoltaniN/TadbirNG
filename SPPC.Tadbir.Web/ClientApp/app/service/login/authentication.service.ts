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

    //headers: Headers;
    //options: RequestOptions;

    constructor(private http: Http)
    {
        //this.headers = new Headers({
        //    'Content-Type': 'application/json; charset=utf-8',
        //    'Access-Control-Expose-Headers': 'X-Tadbir-AuthTicket',
        //    //'Access-Control-Allow-Headers' : '*'
        //});   

        //this.options = new RequestOptions({ headers: this.headers });      
    }

    login(username: string, password: string) {
        return this.http.put(Environment.BaseUrl + '/users/login', { username: username, password: password }/*, this.options*/)
            .map((response: Response) => {
                // login successful if there's a jwt token in the response           
                if (response.headers != null) {
                    let ticket = response.headers.get('X-Tadbir-AuthTicket');
                    if (response.status == 200 && ticket != null) {
                        var user = new ContextInfo();
                        user.ticket = ticket;
                        user.userName = username;

                        // store user details and jwt token in local storage to keep user logged in between page refreshes
                        localStorage.setItem('currentContext', JSON.stringify(user));
                    }
                }              
            })
              
            //});
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

        return false;
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentContext');
    }
}