import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService, BaseService } from '@sppc/shared';


@Injectable()
export class FullCodeService extends BaseService
{      

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


    getFullCode(apiUrl: string) {
        var options = { headers: this.httpHeaders };
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response));
    }

}
