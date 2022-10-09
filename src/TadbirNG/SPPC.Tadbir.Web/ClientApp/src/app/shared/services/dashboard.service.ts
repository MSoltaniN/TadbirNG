import { map } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { BaseService } from "@sppc/shared/class/base.service";
import { DashboardApi } from "./api";
import { String } from "@sppc/shared/class/source";
import { TabWidget } from "../models";
import { DashboardTab } from "../models/dashboardTab";

@Injectable()
export class DashboardService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  getChartType(type: number) {
    let chartType = "";

    switch (type) {
      case 1: //column
        chartType = "bar";
        break;
      case 2: //bar
        chartType = "horizontalBar";
        break;
      default:
        break;
    }

    return chartType;
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
    var url = DashboardApi.WidgetsLookup;
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  getWidgetData(widgetId) {
    var url = DashboardApi.WidgetData;
    url = String.Format(url, widgetId);

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

  addTabWidget(tabId, tabWidget: TabWidget) {
    var url = DashboardApi.TabWidgets;
    url = String.Format(url, tabId);

    var body = JSON.stringify(tabWidget);
    return this.http.post(url, body, this.option).pipe(map((res) => res));
  }

  removeTabWidget(tabId, widgetId) {
    var url = DashboardApi.TabWidget;
    url = String.Format(url, tabId, widgetId);

    return this.http.delete(url, this.option).pipe(map((res) => res));
  }

  addDashboardTab(dashboardTab: DashboardTab) {
    var url = DashboardApi.DashboardTabs;

    var body = JSON.stringify(dashboardTab);
    return this.http.post(url, body, this.option).pipe(map((res) => res));
  }

  removeDashboardTab(tabId) {
    var url = DashboardApi.DashboardTab;
    url = String.Format(url, tabId);

    return this.http.delete(url, this.option).pipe(map((res) => res));
  }

  saveDashboardWidgets(dashboradTabs: TabWidget[]) {
    var url = DashboardApi.AllTabWidgets;

    var body = JSON.stringify(dashboradTabs);
    return this.http.put(url, body, this.option).pipe(map((res) => res));
  }
}
