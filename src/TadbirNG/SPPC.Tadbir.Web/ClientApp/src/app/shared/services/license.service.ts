import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "@sppc/env/environment";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { BaseService } from "../class/base.service";

@Injectable()
export class LicenseService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  CheckOfflineLicense(url: string) {
    var newHeader = this.httpHeaders;
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };

    return this.http
      .get(url, options)
      .map((response) => <any>(<Response>response));
  }

  ActivateLicense(url: string) {
    var newHeader = this.httpHeaders;
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };

    return this.http
      .put(url, null, options)
      .map((response) => <any>(<Response>response));
  }

  CheckOnlineLicense(url: string) {
    var newHeader = this.httpHeaders;
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };

    return this.http
      .get(url, options)
      .map((response) => <any>(<Response>response));
  }

  DeleteCurrentSessionAsync(url: string) {
    var newHeader = this.httpHeaders;
    var options = { headers: newHeader };

    return this.http
      .delete(url, options)
      .map((response) => <any>(<Response>response));
  }

  GetOpenSessions(url: string) {
    var newHeader = this.httpHeaders;
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };

    return this.http
      .get(url, options)
      .map((response) => <any>(<Response>response));
  }

  DeleteOpenSessions(url: string, ids: number[]) {
    var newHeader = this.httpHeaders;
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };
    let body = JSON.stringify({ paraph: "", items: ids });

    return this.http
      .put(url, body, options)
      .map((response) => <any>(<Response>response));
  }

  PutSessionAsActive(url: string) {
    var newHeader = this.httpHeaders;
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };

    return this.http
      .put(url, null, options)
      .map((response) => <any>(<Response>response));
  }
}
