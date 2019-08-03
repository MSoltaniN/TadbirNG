import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService, BrowserStorageService } from '@sppc/shared';
import { AccountGroup } from '..';


export class AccountGroupInfo implements AccountGroup {
  id: number = 0;
  name: string;
  category: string;
  description?: string;
}


@Injectable()
export class AccountGroupsService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


}
