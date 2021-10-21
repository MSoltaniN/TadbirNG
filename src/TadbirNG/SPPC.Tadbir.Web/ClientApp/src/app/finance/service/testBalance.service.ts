import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Project } from '@sppc/finance/models';
import { BaseService } from '@sppc/shared/class';
import { BrowserStorageService } from '@sppc/shared/services';



@Injectable()
export class TestBalanceService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }
}
