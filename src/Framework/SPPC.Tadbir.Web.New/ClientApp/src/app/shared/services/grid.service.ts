import { Injectable } from '@angular/core';
import { BaseService } from '../class/base.service';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from './browserStorage.service';




@Injectable()
export class GridService extends BaseService {


  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


}
