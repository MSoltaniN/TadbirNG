import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { BaseService } from '../class/base.service';
import { AccountGroup } from '../model/index';
import { HttpHeaders, HttpClient, HttpResponse } from '@angular/common/http';

export class AccountGroupInfo implements AccountGroup {
  id: number = 0;
  name: string;
  category: string;
  description?: string;
}


@Injectable()
export class AccountGroupsService extends BaseService {

  constructor(public http: HttpClient) {
    super(http);
  }


}
