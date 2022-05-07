import { EnviromentComponent } from "./enviroment.component";
import { GridOrderBy } from "./grid.orderby";
import { Observable } from "rxjs";
import { FilterExpression } from "./filterExpression";
import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { BrowserStorageService } from "../services";
import { BaseService } from "./base.service";





export class ReportBaseService extends BaseService{

 
  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http,bStorageService);
   
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

 
}
