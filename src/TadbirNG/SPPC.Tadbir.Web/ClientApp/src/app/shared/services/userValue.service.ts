import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseService } from "@sppc/shared/class/base.service";
import { String } from "@sppc/shared/class/source";
import { FilterViewModel } from "@sppc/shared/models";
import { FilterApi } from "@sppc/shared/services/api/filterApi";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { UserValue } from "../models/userValue";
import { UserValueApi } from "./api/userValueApi";

@Injectable()
export class UserValueService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  public insertItem(item:UserValue) {
    let url = String.Format(UserValueApi.CategoryValues,item.categoryId);
    var body = JSON.stringify(item);
    return this.http
      .post(url, body, this.option)
      .pipe(map((res) => res));
  }

  public getCategories() {
    var apiUrl = UserValueApi.Categories
    var options = { headers: this.httpHeaders };
    return this.http
      .get(apiUrl, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

}
