import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService, BrowserStorageService } from '@sppc/shared';




@Injectable()
export class GridService extends BaseService {


  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


}
