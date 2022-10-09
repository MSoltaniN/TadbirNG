import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FullAccount } from "@sppc/finance/models";
import { BaseService } from "@sppc/shared/class";
import { Widget } from "@sppc/shared/models/widget";
import { BrowserStorageService } from "@sppc/shared/services";
import { DashboardApi } from "@sppc/shared/services/api";


export class WidgetInfo implements Widget {
  id: number;
  createdById: number;
  CreatedByFullName: string;
  title: string;
  functionId: string;
  FunctionName: string;
  typeId: string;
  TypeName: string;
  accounts: FullAccount[];
  defaultSettings: string;
  description?: string;
  rowNo: number;
}

@Injectable()
export class WidgetService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  getFunctions() {
    let url = DashboardApi.WidgetFunctionsLookup;
    let options = { headers: this.httpHeaders }
    return this.http.get(url, options);
  }

  getTypes() {
    let url = DashboardApi.WidgetTypesLookup;
    let options = { headers: this.httpHeaders }
    return this.http.get(url, options);
  }

  getWidgets(id) {
    let url = DashboardApi.Widget;
    let options = { headers: this.httpHeaders }
    return this.http.get(url, options);
  }

}
