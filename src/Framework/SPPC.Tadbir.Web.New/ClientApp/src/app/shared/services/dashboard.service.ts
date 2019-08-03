import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService, BrowserStorageService, DashboardApi } from '@sppc/shared';




@Injectable()
export class DashboardService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

    getDashboardInfo() {
        var url = DashboardApi.SummariesUrl;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

}
