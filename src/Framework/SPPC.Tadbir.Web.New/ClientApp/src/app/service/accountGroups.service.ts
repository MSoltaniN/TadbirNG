import { Injectable } from '@angular/core';
import { BaseService } from '../class/base.service';
import { AccountGroup } from '../model/index';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from './browserStorage.service';

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
