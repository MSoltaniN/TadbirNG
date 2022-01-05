import { Injectable } from "@angular/core";
import { BaseService } from "../class/base.service";
import { HttpClient } from "@angular/common/http";
import { BrowserStorageService } from '@sppc/shared/services/browserStorage.service';
import { environment } from "@sppc/env/environment";

@Injectable()
export class LicenseService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  GetAppLicense(url: string) {

    var newHeader = this.httpHeaders;
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };

    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }

  ActivateLicense(url: string) {
 
    var newHeader = this.httpHeaders;    
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };

    return this.http.put(url,null, options)
      .map(response => <any>(<Response>response));
  }

  CheckOnlineLicense(url: string) {
    
    var newHeader = this.httpHeaders;    
    newHeader = newHeader.append("X-Tadbir-Instance", environment.InstanceKey);
    var options = { headers: newHeader };

    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }
}
