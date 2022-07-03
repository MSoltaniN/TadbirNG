import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services/browserStorage.service';
import { BaseService } from '@sppc/shared/class/base.service';
import { Subject } from 'rxjs';




@Injectable()
export class GridService extends BaseService {


  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  // to prevent duplicate save data
  submitted: Subject<boolean> = new Subject()
  isSubmitted(){
    return this.submitted.asObservable()
  }

}
