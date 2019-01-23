import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { BaseService } from '../class/base.service';
import { AccountGroup, AccountCollectionCategory, AccountCollection } from '../model/index';
import { HttpHeaders, HttpClient, HttpResponse } from '@angular/common/http';
import { AccountCollectionAccount } from '../model/accountCollectionAccount';

export class AccountCollectionCategoryInfo implements AccountCollectionCategory {
  accountCollections: AccountCollection[];
  id: number;
  name: string;
}

export class AccountCollectionInfo implements AccountCollection {
  id: number;
  name: string;
  multiSelect: boolean;
  typeLevel: number;
  inventoryMode: number;
}

export class AccountCollectionAccountInfo implements AccountCollectionAccount {
  id: number = 0;
  accountId: number;
  fiscalPeriodId: number;
  branchId: number;
  collectionId: number;
}


@Injectable()
export class AccountCollectionService extends BaseService {

  constructor(public http: HttpClient) {
    super(http);
  }


}
