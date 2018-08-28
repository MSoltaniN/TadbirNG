import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';

import { Company } from '../model/index';
import { HttpClient } from '@angular/common/http';





export class CompanyInfo implements Company {
    parentId?: number | undefined;
    childCount: number = 0;
    id: number = 0;
    name: string;
    description?: string | undefined;

}

@Injectable()
export class CompanyService extends BaseService {

    constructor(public http: HttpClient) {
        super(http);
    }

}