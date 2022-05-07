import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseService } from "@sppc/shared/class";
import { String } from "@sppc/shared/class/source";
import { Command, RelatedItems, UserProfile } from "@sppc/shared/models";
import { ShortcutCommand } from "@sppc/shared/models/shortcutCommand";
import { BrowserStorageService } from "@sppc/shared/services";
import { map } from "rxjs/operators";
import "rxjs/Rx";
import { User } from "../models";
import { UserApi } from "./api";

export class UserInfo implements User {
  personFirstName: string;
  personLastName: string;
  id: number = 0;
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
  constructor(
    public id: number,
    public title: string = "",
    public routeUrl: string,
    public iconName: string = "",
    public hotKey: string,
    public children: Command[],
    public hasPermission: boolean,
    permissionId?: number | undefined
  ) {}
}

@Injectable()
export class UserService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  changePassword(userProfile: UserProfile) {
    var body = JSON.stringify(userProfile);
    var url = String.Format(UserApi.UserPassword, userProfile.userName);
    var options = { headers: this.httpHeaders };
    return this.http.put(url, body, options).pipe(map((res) => res));
  }

  getUserRoles(userId: number) {
    var url = String.Format(UserApi.UserRoles, userId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(map((res) => res));
  }

  getCurrentUserHotKeys() {
    var url = UserApi.CurrentUserHotKeys;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(map((res: ShortcutCommand) => res));
  }

  modifiedUserRoles(userRoles: RelatedItems) {
    var body = JSON.stringify(userRoles);
    var options = { headers: this.httpHeaders };
    var url = String.Format(UserApi.UserRoles, userRoles.id);
    return this.http.put(url, body, options).pipe(map((res) => res));
  }

  getCurrentUserCommands(ticket: string) {
    var url = UserApi.CurrentUserCommands;

    var header = this.httpHeaders;
    if (header) {
      header = header.delete("X-Tadbir-AuthTicket");
      header = header.delete("Accept-Language");

      header = header.append("X-Tadbir-AuthTicket", ticket);

      if (this.CurrentLanguage == "fa")
        header = header.append("Accept-Language", "fa-IR,fa");

      if (this.CurrentLanguage == "en")
        header = header.append("Accept-Language", "en-US,en");
    }

    var options = { headers: header };

    return this.http.get(url, options).pipe(map((res) => res));
  }

  getDefaultUserCommands(ticket: string) {
    var url = UserApi.UserDefaultCommands;

    var header = this.httpHeaders;
    if (header) {
      header = header.delete("X-Tadbir-AuthTicket");
      header = header.delete("Accept-Language");

      header = header.append("X-Tadbir-AuthTicket", ticket);

      if (this.CurrentLanguage == "fa")
        header = header.append("Accept-Language", "fa-IR,fa");

      if (this.CurrentLanguage == "en")
        header = header.append("Accept-Language", "en-US,en");
    }

    var options = { headers: header };

    return this.http.get(url, options).pipe(map((res) => res));
  }
}
