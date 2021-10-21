import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocalReport, QuickReportConfigInfo, Parameter, QuickReportColumnModel, QuickReportViewModel, FilterViewModel } from '@sppc/shared/models';
import { BaseService } from '@sppc/shared/class/base.service';
import { BrowserStorageService } from '@sppc/shared/services/browserStorage.service';
import { FilterApi } from '@sppc/shared/services/api/filterApi';
import { String } from '@sppc/shared/class/source';


@Injectable()
export class AdvanceFilterService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  public insertFilter(filterModel: FilterViewModel) {
    var body = JSON.stringify(filterModel);
    return this.http.post(FilterApi.Filters, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public getFilters(viewId: number): Observable<FilterViewModel[]> {
    var apiUrl = String.Format(FilterApi.FiltersByView,viewId)   
    var options = { headers: this.httpHeaders };
    return this.http.get(apiUrl, options)
      .map(response => <any>(<Response>response));
  }

  public saveFilter(filterId:number,filterModel:FilterViewModel) {
    var apiUrl = String.Format(FilterApi.Filter, filterId);
    var body = JSON.stringify(filterModel);
    return this.http.put(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public deleteFilter(filterId: number): Observable<string> {
    var apiUrl = String.Format(FilterApi.Filter, filterId);
    return this.http.delete(apiUrl,this.option)
      .map(res => res)
      .catch(this.handleError);
  } 

}
