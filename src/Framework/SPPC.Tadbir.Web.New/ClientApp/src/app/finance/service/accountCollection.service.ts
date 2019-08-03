import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService, BrowserStorageService } from '@sppc/shared';
import { AccountCollectionAccount, AccountCollection, AccountCollectionCategory } from '..';


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

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

}
