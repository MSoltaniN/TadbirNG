import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CostCenter } from '@sppc/finance/models';
import { BrowserStorageService } from '@sppc/shared/services';
import { BaseService } from '@sppc/shared/class';



export class CostCenterInfo implements CostCenter {
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
export class CostCenterService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

}
