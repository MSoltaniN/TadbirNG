import { EnviromentComponent } from "./enviroment.component";
import { RequestOptions, Http, Response } from "@angular/http";
import { Headers } from '@angular/http';
import { Filter } from "./filter";
import { GridOrderBy } from "./grid.orderby";
import { String } from '../class/source';
import { Observable } from "rxjs/Observable";
import { FilterExpression } from "./filterExpression";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { SppcLoadingService } from "../controls/sppcLoading/index";
import { ReflectiveInjector, Injector, Injectable, ErrorHandler } from '@angular/core';
import { HttpErrorResponse, HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";




export class ReportBaseService extends EnviromentComponent{

  //option: any;
  //httpHeaders = new HttpHeaders();

  constructor(public http: HttpClient) {
    super();
   
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
  public getAll(apiUrl: string, orderby?: string, filter?: FilterExpression) {
    
    var sort = new Array<GridOrderBy>();
    if (orderby) {
      var orderByParts = orderby.split(' ');
      var fieldName = orderByParts[0];
      if (orderByParts[1] != 'undefined')
        sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
    }
    var postItem = { filter: filter, sortColumns: sort };
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
