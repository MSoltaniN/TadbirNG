import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';
import { Branch } from '../model/index';


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


    constructor(public http: Http) {
        super(http);
    }

}