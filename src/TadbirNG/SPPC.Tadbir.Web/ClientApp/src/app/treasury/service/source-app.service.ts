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
