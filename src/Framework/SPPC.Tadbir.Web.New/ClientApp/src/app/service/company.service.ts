import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';

import { Company, CompanyDb } from '../model/index';
import { HttpClient } from '@angular/common/http';





export class CompanyInfo implements Company {
    parentId?: number | undefined;
    childCount: number = 0;
    id: number = 0;
    name: string;
    description?: string | undefined;

}

export class CompanyDbInfo implements CompanyDb {   
    id: number = 0;
    name: string;
    dbName: string;
    dbPath: string;
    description?: string | undefined;
    server: string;
    userName: string;
    password: string;
}

@Injectable()
export class CompanyService extends BaseService {

    constructor(public http: HttpClient) {
        super(http);
    }

}