import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Project } from '../index';
import { BaseService, BrowserStorageService } from '../../shared/index';



export class ProjectInfo implements Project {
    id: number = 0;
    code: string;
    fullCode: string;
    name: string;
    level: number = 0;
    description?: string | undefined;
    parentId?: number | undefined;
    childCount: number = 0;
    fiscalPeriodId: number = 0;
    branchId: number = 0;
    companyId: number;
    branchScope: number = 0;
}

@Injectable()
export class ProjectService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }
}
