import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService, BrowserStorageService } from '@sppc/shared';
import { CompanyDb } from '..';



//export class CompanyInfo implements Company {
//  parentId?: number | undefined;
//  childCount: number = 0;
//  id: number = 0;
//  name: string;
//  description?: string | undefined;

//}

export class CompanyDbInfo implements CompanyDb {
  id: number = 0;
  name: string;
  dbName: string;
  dbPath: string;
  description?: string | undefined;
  server: string;
  userName: string;
  password: string;
}

@Injectable()
export class CompanyService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

}
