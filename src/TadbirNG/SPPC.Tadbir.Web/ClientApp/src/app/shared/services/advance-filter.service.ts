import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseService } from "@sppc/shared/class/base.service";
import { String } from "@sppc/shared/class/source";
import { FilterViewModel } from "@sppc/shared/models";
import { FilterApi } from "@sppc/shared/services/api/filterApi";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable()
export class AdvanceFilterService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  public insertFilter(filterModel: FilterViewModel) {
    var body = JSON.stringify(filterModel);
    return this.http
      .post(FilterApi.Filters, body, this.option)
      .pipe(map((res) => res));
  }

  public getFilters(viewId: number): Observable<FilterViewModel[]> {
    var apiUrl = String.Format(FilterApi.FiltersByView, viewId);
    var options = { headers: this.httpHeaders };
    return this.http
      .get(apiUrl, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  public saveFilter(filterId: number, filterModel: FilterViewModel) {
    var apiUrl = String.Format(FilterApi.Filter, filterId);
    var body = JSON.stringify(filterModel);
    return this.http.put(apiUrl, body, this.option).pipe(map((res) => res));
  }

  public deleteFilter(filterId: number) {
    var apiUrl = String.Format(FilterApi.Filter, filterId);
    return this.http.delete(apiUrl, this.option).pipe(map((res) => res));
  }
}
