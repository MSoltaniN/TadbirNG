import { EnviromentComponent } from "./enviroment.component";
import { GridOrderBy } from "./grid.orderby";
import { Observable } from "rxjs/Observable";
import { FilterExpression } from "./filterExpression";
import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { BrowserStorageService } from "../services";





export class ReportBaseService extends EnviromentComponent{

 
  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(bStorageService);
   
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
   * @param orderby مرتب سازی
   * @param filters فیلتر
   */
  public getAll(apiUrl: string, orderby?: any, filter?: FilterExpression,quickFilter?:FilterExpression,operationId:number = 1) {
    
    var sort = new Array<GridOrderBy>();
    if (orderby && orderby.length > 0) {
      for (let item of orderby) {
        sort.push(new GridOrderBy(item.field, item.dir.toUpperCase()));
      }
    }
    var postItem = { filter: filter, sortColumns: sort, operation: operationId, quickFilter: quickFilter};
    var searchHeaders = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (searchHeaders)
      searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);

    return this.http.get(apiUrl, { headers: searchHeaders, observe: "response" })
      .map(response => <any>(<HttpResponse<any>>response));
  }

  /**
   * 
   * @param error
   */
  public handleError(error: any) {
    return Observable.throw(error.error);
  }
}
