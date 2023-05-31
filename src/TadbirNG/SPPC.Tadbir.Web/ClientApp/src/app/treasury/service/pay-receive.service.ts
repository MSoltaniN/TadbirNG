import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from '@sppc/shared/class';
import { BrowserStorageService } from '@sppc/shared/services';
import { PayReceive } from '../models/payReceive';

export class PayReceiveInfo implements PayReceive {
  id: number = 0;
  fiscalPeriodId: number;
  branchId: number;
  date: Date;
  payReceiveNo: string;
  reference: string;
  confirmedById: number;
  approvedById: number;
  currencyRate: number;
  description: string;
  issuedByName: string;
  confirmedByName: string;
  approvedByName: string;
  currencyId: number;
  isApproved: boolean;
  isConfirmed: boolean;
  hasNext: boolean;
  hasPrevious: boolean;
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
