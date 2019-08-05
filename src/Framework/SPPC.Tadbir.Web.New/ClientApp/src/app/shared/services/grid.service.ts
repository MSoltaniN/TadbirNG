import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services';
import { BaseService } from '../class';




@Injectable()
export class GridService extends BaseService {


  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


}
