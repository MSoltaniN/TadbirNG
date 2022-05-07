import { HttpClient, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ReportBaseService } from "@sppc/shared/class/report.base.service";
import {
  LocalReport,
  Parameter,
  QuickReportColumnModel,
  QuickReportConfigInfo,
  QuickReportViewModel,
} from "@sppc/shared/models";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { map } from "rxjs/operators";

@Injectable()
export class ReportingService extends ReportBaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  public getBalanceByAccountData(apiUrl: string, postItem: any) {
    var searchHeaders = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (searchHeaders)
      searchHeaders = searchHeaders.append("X-Tadbir-Parameters", base64Body);

    return this.http
      .get(apiUrl, { headers: searchHeaders, observe: "response" })
      .pipe(map((response) => <any>(<HttpResponse<any>>response)));
  }

  public saveReport(apiUrl: string, report: LocalReport) {
    var body = JSON.stringify(report);
    return this.http.put(apiUrl, body, this.option).pipe(map((res) => res));
  }

  public saveAsReport(apiUrl: string, report: LocalReport) {
    var body = JSON.stringify(report);
    return this.http.post(apiUrl, body, this.option).pipe(map((res) => res));
  }

  public setDefaultForAll(apiUrl: string) {
    return this.http.put(apiUrl, null, this.option).pipe(map((res) => res));
  }

  public deleteReport(apiUrl: string) {
    return this.http.delete(apiUrl, this.option).pipe(map((res) => res));
  }

  /*
  public putEnvironmentUserQuickReport(apiUrl: string, viewInfo: QuickReportViewInfo): Observable<string> {

    var body = JSON.stringify(viewInfo);

    return this.http.put(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);

  }*/

  public putEnvironmentUserQuickReport(
    apiUrl: string,
    viewInfo: QuickReportConfigInfo
  ) {
    var body = JSON.stringify(viewInfo);

    return this.http.put(apiUrl, body, this.option).pipe(map((res) => res));
  }
}

export interface ParameterFields {
  value: string;
}

export class ParameterInfo implements Parameter, ParameterFields {
  value: string;
  id: number;
  fieldName: string;
  operator: string;
  dataType: string;
  controlType: string;
  captionKey: string;
  defaultValue: string;
  minValue: string;
  maxValue: string;
  descriptionKey: string;
  name: string;
  source: string;
}

export class LocalReportInfo implements LocalReport {
  reportId: number;
  localeId: number;
  id: number;
  caption: string;
  template: string;
}

export class QuickReportColumnInfo implements QuickReportColumnModel {
  name: string;
  defaultText: string;
  index: number;
  sortMode: number;
  sortOrder: number;
  width: number;
  enabled: boolean;
  order: number;
  userText: string;
  dataType: string;
  visible: boolean;
  type: string;
}

export class QuickReportViewInfo implements QuickReportViewModel {
  reportTitle: string;
  inchValue: number;
  reportLang: string;
  columns: QuickReportColumnModel[];
  parameters: Parameter[];
}
