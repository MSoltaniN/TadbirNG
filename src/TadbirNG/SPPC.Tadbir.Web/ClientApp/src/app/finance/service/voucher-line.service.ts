import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { String, BaseService } from '@sppc/shared/class';
import { VoucherLine, AccountItemBrief } from '@sppc/finance/models';
import { VoucherApi } from '@sppc/finance/service/api';
import { BrowserStorageService } from '@sppc/shared/services';



export class VoucherLineInfo implements VoucherLine {
  id: number = 0;
  debit: number;
  credit: number;
  description?: string | undefined;
  fiscalPeriodId: number = 0;
  branchId: number = 0;
  voucherId: number = 0;
  currencyId: number;
  fullAccount: {
    account: AccountItemBrief;
    detailAccount: AccountItemBrief;
    costCenter: AccountItemBrief;
    project: AccountItemBrief;    
  };
  mark:string | undefined;
}

@Injectable()
export class VoucherLineService extends BaseService {
  
  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


  getVoucherInfo(voucherId: number) {
    var url = String.Format(VoucherApi.Voucher, voucherId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));;
  }

  putArticleMark(articleId: number, mark: string) {
    var url = String.Format(VoucherApi.VoucherArticleMark, articleId);
    var body = JSON.stringify({ id: articleId, mark: mark });
    return this.http.put(url, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

}
