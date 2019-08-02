import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { User, UserProfile, RelatedItems } from '../model/index';
import { UserApi } from './api/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { HttpClient } from "@angular/common/http";
import { BaseService } from '../class/base.service';
import { Command } from '../model/command';
import { BrowserStorageService } from './browserStorage.service';


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
    
    constructor(public id: number, public title: string = "", public routeUrl: string,
        public iconName: string = "", public hotKey: string,
        public children: Command[], public hasPermission: boolean, permissionId?: number | undefined) { }
    
}

@Injectable()
export class UserService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

    changePassword(userProfile: UserProfile): Observable<string> {
        var body = JSON.stringify(userProfile);
        var url = String.Format(UserApi.UserPassword, userProfile.userName);
        var options = { headers: this.httpHeaders };
        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }


    getUserRoles(userId: number) {
        var url = String.Format(UserApi.UserRoles, userId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    modifiedUserRoles(userRoles: RelatedItems) {
        var body = JSON.stringify(userRoles);
        var options = { headers: this.httpHeaders };
        var url = String.Format(UserApi.UserRoles, userRoles.id);
        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }
    
    getCurrentUserCommands(ticket : string) {

        var url = UserApi.CurrentUserCommands;

        var header = this.httpHeaders;
        if (header) {
            header = header.delete('X-Tadbir-AuthTicket');
            header = header.delete('Accept-Language');

            header = header.append('X-Tadbir-AuthTicket', ticket);         

           if (this.CurrentLanguage == "fa")
               header = header.append('Accept-Language', 'fa-IR,fa');

           if (this.CurrentLanguage == "en")                
               header = header.append('Accept-Language', 'en-US,en');

        }

        var options = { headers: header };
        
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));

    }

    getDefaultUserCommands(ticket : string) {

        var url = UserApi.UserDefaultCommands;

        var header = this.httpHeaders;
        if (header) {
            header = header.delete('X-Tadbir-AuthTicket');
            header = header.delete('Accept-Language');

            header = header.append('X-Tadbir-AuthTicket', ticket);         

           if (this.CurrentLanguage == "fa")
               header = header.append('Accept-Language', 'fa-IR,fa');

           if (this.CurrentLanguage == "en")                
               header = header.append('Accept-Language', 'en-US,en');

        }

        var options = { headers: header };
        
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));

    }

}