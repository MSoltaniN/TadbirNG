import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocalReport, QuickReportConfigInfo, Parameter, QuickReportColumnModel, QuickReportViewModel } from '@sppc/shared/models';
import { ReportBaseService } from '@sppc/shared/class/report.base.service';
import { BrowserStorageService } from '@sppc/shared/services/browserStorage.service';


@Injectable()
export class ReportingService extends ReportBaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  public saveReport(apiUrl: string, report: LocalReport): Observable<string> {
    var body = JSON.stringify(report);
    return this.http.put(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public saveAsReport(apiUrl: string, report: LocalReport): Observable<string> {
    var body = JSON.stringify(report);
    return this.http.post(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public setDefaultForAll(apiUrl: string): Observable<string> {
    return this.http.put(apiUrl, null, this.option)
      .map(res => res)
      .catch(this.handleError);

  }

  public deleteReport(apiUrl: string): Observable<string> {
    return this.http.delete(apiUrl, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  /*
  public putEnvironmentUserQuickReport(apiUrl: string, viewInfo: QuickReportViewInfo): Observable<string> {

    var body = JSON.stringify(viewInfo);

    return this.http.put(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);

  }*/

  public putEnvironmentUserQuickReport(apiUrl: string, viewInfo: QuickReportConfigInfo): Observable<string> {

    var body = JSON.stringify(viewInfo);
    
    return this.http.put(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);

  }

}




export interface ParameterFields {
  value: string;
}

export class ParameterInfo implements Parameter, ParameterFields {
  value: string;
  id: number; fieldName: string;
  operator: string;
  dataType: string;
  controlType: string;
  captionKey: string;
  defaultValue: string;
  minValue: string;
  maxValue: string;
  descriptionKey: string;
  name: string;

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
