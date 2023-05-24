import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from '@sppc/shared/class';
import { BrowserStorageService } from '@sppc/shared/services';
import { PayReceive } from '../models/payReceive';

export class PayReceiveInfo implements PayReceive {
  id: number = 0;
  fiscalPeriodId: number;
  branchId: number;
  payReceiveNo: string;
  reference: string;
  issuedById: number;
  modifiedById: number;
  confirmedById: number;
  approvedById: number;
  type: number;
  currencyRate: number;
  description: string;
  createdDate: Date;
  issuedByName: string;
  modifiedByName: string;
  confirmedByName: string;
  approvedByName: string;
  currency: number;
  hasNext: number;
  hasPrevious: number;
}

@Injectable({
  providedIn: 'root'
})
export class PayReceiveService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }
}
