import { map } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { BaseService } from "@sppc/shared/class/base.service";
import { DashboardApi } from "./api";

@Injectable()
export class DashboardService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  getDashboardInfo() {
    var url = DashboardApi.Summaries;
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  getLincenseInfo() {
    var url = DashboardApi.LicenseInfo;
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  getWidgetList() {
    var url = DashboardApi.WidgetsUrl;
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  getCurrentDashboard() {
    var url = DashboardApi.CurrentDashboard;
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }
}
