import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { User, UserProfile, RelatedItems } from '../model/index';
import { UserApi } from './api/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";
import { GridOrderBy } from "../class/grid.orderby";
import { HttpParams } from "@angular/common/http";
import { Environment, MessageType } from "../enviroment";
import { Context } from "../model/context";

import { BaseComponent } from "../class/base.component"
import { ToastrService } from 'ngx-toastr';
import { BaseService } from '../class/base.service';
import { Command } from '../model/command';


export class UserInfo implements User {
    personFirstName: string;
    personLastName: string;
    id: number=0;
    userName: string;
    password: string;
    lastLoginDate?: Date | undefined;
    isEnabled: boolean = false;
}

export class UserProfileInfo implements UserProfile {
    userName: string;
    oldPassword: string;
    newPassword: string;
    repeatPassword: string;
}

export class CommandInfo implements Command {
    permissionId?: number | undefined;
    children: Command[];
    id: number;
    title: string;
    routeUrl: string;
    iconName: string;
    hotKey: string;
}

@Injectable()
export class UserService extends BaseService {

    constructor(public http: Http) {
        super(http);
    }

    changePassword(userProfile: UserProfile): Observable<string> {
        var body = JSON.stringify(userProfile);
        var url = String.Format(UserApi.UserPassword, userProfile.userName);
        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }


    getUserRoles(userId: number) {
        var url = String.Format(UserApi.UserRoles, userId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedUserRoles(userRoles: RelatedItems) {
        var body = JSON.stringify(userRoles);
        var headers = this.headers;
        var url = String.Format(UserApi.UserRoles, userRoles.id);
        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    getCurrentUserCommands(ticket : string) {

        var url = UserApi.CurrentUserCommands;
        if (this.headers)
            this.headers.set('X-Tadbir-AuthTicket', ticket);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }

}