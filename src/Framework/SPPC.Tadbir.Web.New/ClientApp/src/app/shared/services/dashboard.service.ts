import { Injectable } from '@angular/core';
import { BaseService } from '../class/base.service';
import { HttpClient } from '@angular/common/http';
import { DashboardApi } from './api/dashboardApi';
import { BrowserStorageService } from './browserStorage.service';


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
