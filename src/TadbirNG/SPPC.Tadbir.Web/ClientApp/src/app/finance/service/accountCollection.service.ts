import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services';
import { AccountCollectionAccount, AccountCollection, AccountCollectionCategory } from '@sppc/finance/models';
import { BaseService } from '@sppc/shared/class';


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
  accountFullCode: string;
  accountName: string;
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
