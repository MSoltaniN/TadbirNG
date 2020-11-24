import { EnviromentComponent } from "./enviroment.component";
import { Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { String } from '@sppc/shared/class/source';
import { GridOrderBy } from "@sppc/shared/class/grid.orderby";
import { FilterExpression } from "@sppc/shared/class/filterExpression";





export class BaseService extends EnviromentComponent{

  //option: any;
  //httpHeaders = new HttpHeaders();

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(bStorageService);
    //this.httpHeaders = this.httpHeaders.append('Content-Type', 'application/json; charset=utf-8');

    //this.httpHeaders = this.httpHeaders.append('X-Tadbir-AuthTicket', this.Ticket);

    //if (this.CurrentLanguage == "fa")
    //  this.httpHeaders = this.httpHeaders.append('Accept-Language', 'fa-IR,fa');

    //if (this.CurrentLanguage == "en")
    //  this.httpHeaders = this.httpHeaders.append('Accept-Language', 'en-US,en');

    //this.option = { headers: this.httpHeaders };
  }

  public get httpHeaders() {
    var header = new HttpHeaders();
    header = header.append('Content-Type', 'application/json; charset=utf-8');

    header = header.append('X-Tadbir-AuthTicket', this.Ticket);

    if (this.CurrentLanguage == "fa")
      header = header.append('Accept-Language', 'fa-IR,fa');

    if (this.CurrentLanguage == "en")
      header = header.append('Accept-Language', 'en-US,en');

    return header;
  }

  public get option() {
    return { headers: this.httpHeaders }
  }

  /**
   * لیست رکوردها بر اساس فیلتر و مرتب سازی
   * @param apiUrl آدرس‌ کامل api
   * @param start شماره شروع رکورد
   * @param count تعداد رکورد
   * @param orderby مرتب سازی
   * @param filter فیلتر اطلاعات گرید
   * @param quickFilter فیلتر سریع
   */
  public getAll(apiUrl: string, start?: number, count?: number, orderby?: any, filter?: FilterExpression, quickFilter?: FilterExpression, listChangedValue?: boolean) {

    var gridPaging = { pageIndex: start, pageSize: count };
    var sort = new Array<GridOrderBy>();
    if (orderby && orderby.length > 0) {
      for (let item of orderby) {
        sort.push(new GridOrderBy(item.field, item.dir.toUpperCase()));
      }
    }

    if (listChangedValue == undefined)
      listChangedValue = true;

    var postItem = { paging: gridPaging, filter: filter, quickFilter: quickFilter, sortColumns: sort, listChanged: listChangedValue };
    var searchHeaders = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (searchHeaders)
      searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);


    //var options = { headers: searchHeaders, observe: "response"};
    return this.http.get(apiUrl, { headers: searchHeaders, observe: "response" })
      .map(response => <any>(<HttpResponse<any>>response));
  }

  /**
   * لیست رکوردها بر اساس پارامتر
   * @param apiUrl آدرس‌ کامل api
   * @param params پارامتر های فرم   
   */
  public getAllByParams(apiUrl: string, params: any, start?: number, count?: number, orderby?: any, filter?: FilterExpression, quickFilter?: FilterExpression, listChangedValue?: boolean) {

    var searchHeaders = this.httpHeaders;  

    var gridPaging = { pageIndex: start, pageSize: count };
    var sort = new Array<GridOrderBy>();
    if (orderby && orderby.length > 0) {
      for (let item of orderby) {
        sort.push(new GridOrderBy(item.field, item.dir.toUpperCase()));
      }
    }

    if (listChangedValue == undefined)
      listChangedValue = true;

    var postItem = { paging: gridPaging, filter: filter, quickFilter: quickFilter, sortColumns: sort, listChanged: listChangedValue };
    
    
    if (searchHeaders) {

      var postBody = JSON.stringify(postItem);
      base64Body = btoa(encodeURIComponent(postBody));
      searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);

      postBody = JSON.stringify(params);
      var base64Body = btoa(encodeURIComponent(postBody));
      searchHeaders = searchHeaders.append('X-Tadbir-Parameters', base64Body);
    }        

    return this.http.get(apiUrl, { headers: searchHeaders, observe: "response" })
      .map(response => <any>(<HttpResponse<any>>response));
  }

  /**
   * لیستی از اطلاعات را از سرویس میگیرد
   * @param apiUrl آدرس کامل api
   */
  public getModels(apiUrl: string) {
    var options = { headers: this.httpHeaders };
    return this.http.get(apiUrl, options)
      .map(response => <any>(<Response>response));
  }

  /**
   * گرفتن رکورد با استفاده از id رکورد
   * @param apiUrl آدرس کامل api
   * @param modelId شماره id رکورد
   */
  public getById(apiUrl: string) {
    var options = { headers: this.httpHeaders };
    return this.http.get(apiUrl, options)
      .map(response => <any>(<Response>response));
  }

  /**
   * ایجاد رکورد جدید
   * @param apiUrl آدرس کامل api
   * @param model رکورد جدید برای افزودن
   */
  public insert<T>(apiUrl: string, model: T): Observable<string> {
    var body = JSON.stringify(model);
    return this.http.post(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  /**
   * ویرایش رکورد
   * @param apiUrl آدرس کامل api
   * @param model رکورد برای ویرایش
   * @param modelId شماره id مدل
   */
  public edit<T>(apiUrl: string, model: T): Observable<any> {
    var body = JSON.stringify(model);
    return this.http.put(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  /**
   * حذف رکورد جاری
   * @param apiUrl آدرس کامل api
   * @param modelId شماره id رکورد
   */
  public delete(apiUrl: string): Observable<string> {
    return this.http.delete(apiUrl, this.option)
      .map(response => response)
      .catch(this.handleError);
  }

  /**
   * حذف گروهی
   * @param apiUrl آدرس api
   * @param models رکوردها
   */
  public groupDelete(apiUrl: string, models: number[]): Observable<string> {
    let body = JSON.stringify({ paraph: '', items: models });
    return this.http.put(apiUrl, body, this.option)
      .map(response => response)
      .catch(this.handleError);
  }
  /**
   * تعداد رکورد بر اساس فیلتر و مرتب سازی
   * @param apiUrl آدرس کامل api
   * @param orderby مرتب سازی
   * @param filters فیلترها
   */
  public getCount(apiUrl: string, orderby?: string, filters?: any[]) {
    var headers = this.httpHeaders;
    var postItem = { filters: filters };
    var searchHeaders = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (searchHeaders)
      searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);
    var options = { headers: searchHeaders };
    return this.http.get(apiUrl, options)
      .map(response => <any>(<Response>response));
  }

  /**
   * تعداد کل رکوردها
   * @param apiUrl آدرس api
   */
  public getTotalCount(apiUrl: string) {
    var url = String.Format(apiUrl, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }

  /**
   * 
   * @param error
   */
  public handleError(error: any) {
    return Observable.throw(error.error);
  }
}
