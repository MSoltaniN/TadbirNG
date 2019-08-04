import { Injectable } from '@angular/core';
import { Response} from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { String, BaseService } from '@sppc/shared/class';
import { Branch, BranchApi } from '..';
import { BrowserStorageService, RelatedItems } from '@sppc/shared';


export class BranchInfo implements Branch {
    companyId: number = 0;
    isAccessible: boolean;
    parentId?: number | undefined;
    childCount: number = 0;
    id: number = 0;
    name: string;
    description?: string | undefined;
    level: number = 0;
}

@Injectable()
export class BranchService extends BaseService {


  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

    getBranchRoles(branchId: number) {
        var url = String.Format(BranchApi.BranchRoles, branchId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    modifiedBranchRoles(branchRoles: RelatedItems) {
        var body = JSON.stringify(branchRoles);        
        var options = { headers: this.httpHeaders };
        var url = String.Format(BranchApi.BranchRoles, branchRoles.id);
        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

}
