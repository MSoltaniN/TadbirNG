import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services';
import { AccountGroup } from '@sppc/finance/models';
import { BaseService } from '@sppc/shared/class';


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
