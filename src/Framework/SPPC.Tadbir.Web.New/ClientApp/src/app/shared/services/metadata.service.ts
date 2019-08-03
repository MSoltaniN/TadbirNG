import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import "rxjs/Rx";
import { String, BaseService, BrowserStorageService, MetadataApi, ReportApi } from '@sppc/shared';
import { HttpParams, HttpClient } from "@angular/common/http";


@Injectable()
export class MetaDataService extends BaseService {

  headers: Headers;
  options: RequestOptions;

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  /**
   * return metadata from database for each entity
   * @param entityName is name of entity like 'account' , 'transaction' , ...
   */
  //getMetaData(entityName: string) {



  //    var header = this.httpHeaders;
  //    header = header.delete('Content-Type');
  //    header = header.delete('X-Tadbir-AuthTicket');
  //    header = header.append('Content-Type','application/json; charset=utf-8');
  //    header = header.append('X-Tadbir-AuthTicket', this.Ticket);
  //    //headers.append('X-Tadbir-AuthTicket', this.Ticket);

  //    var options = { headers: header };


  //    var url = String.Format(MetadataApi.ViewMetadata, entityName);
  //    return this.http.get(url, options)
  //        .map(response => (<Response>response));
  //    //var result = null;
  //    //this.http.get(url, options)
  //    //  .map(response => result = (<Response>response).json());

  //    //return result;
  //}    


  getMetaDataById(entityId: number) {
    var header = this.httpHeaders;
    header = header.delete('Content-Type');
    header = header.delete('X-Tadbir-AuthTicket');
    header = header.append('Content-Type', 'application/json; charset=utf-8');
    header = header.append('X-Tadbir-AuthTicket', this.Ticket);
    //headers.append('X-Tadbir-AuthTicket', this.Ticket);

    var options = { headers: header };

    var url = String.Format(MetadataApi.ViewMetadataById, entityId);
    return this.http.get(url, options)
      .map(response => (<Response>response));
  }


  getReportMetaDataById(viewId: number) {
    var header = this.httpHeaders;
    header = header.delete('Content-Type');
    header = header.delete('X-Tadbir-AuthTicket');
    header = header.append('Content-Type', 'application/json; charset=utf-8');
    header = header.append('X-Tadbir-AuthTicket', this.Ticket);

    var options = { headers: header };

    var url = String.Format(ReportApi.ReportMetadataByView, viewId);
    return this.http.get(url, options)
      .map(response => (<Response>response));
  }

}
