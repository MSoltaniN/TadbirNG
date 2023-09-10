import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from '@sppc/shared/class';
import { BrowserStorageService } from '@sppc/shared/services';

export class SourceAppInfo {
  id:number = 0;
  Code:string;
  Name:string;
  Description:string;
  Type:number;
  fiscalPeriodId: number;
  branchId: number;
  branchScope: number;

  static getInstance() {
    const instance = new SourceAppInfo();
    instance.id = 0;
    instance.Code = "";
    instance.Name = "";
    instance.Description = "";
    instance.Type = 0;
    instance.fiscalPeriodId = 0;
    instance.branchId = 0;
    instance.branchScope = 0;

    return instance;
  }
}

@Injectable({
  providedIn: 'root'
})
export class SourceAppService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }
}
