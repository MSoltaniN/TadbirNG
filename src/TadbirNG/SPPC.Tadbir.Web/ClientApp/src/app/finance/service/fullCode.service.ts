
import {map} from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services';
import { BaseService } from '@sppc/shared/class';


@Injectable()
export class FullCodeService extends BaseService
{      

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


    getFullCode(apiUrl: string) {
        var options = { headers: this.httpHeaders };
        return this.http.get(apiUrl, options).pipe(
            map(response => <any>(<Response>response)));
    }

}
