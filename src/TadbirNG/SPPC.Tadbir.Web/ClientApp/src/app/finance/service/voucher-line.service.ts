import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Response } from "@angular/http";
import { AccountItemBrief, VoucherLine } from "@sppc/finance/models";
import { VoucherApi } from "@sppc/finance/service/api";
import { BaseService, String } from "@sppc/shared/class";
import { BrowserStorageService } from "@sppc/shared/services";
import { map } from "rxjs/operators";

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
  mark: string | undefined;
}

@Injectable()
export class VoucherLineService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  getVoucherInfo(voucherId: number) {
    var url = String.Format(VoucherApi.Voucher, voucherId);
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  putArticleMark(articleId: number, mark: string) {
    var url = String.Format(VoucherApi.VoucherArticleMark, articleId);
    var body = JSON.stringify({ id: articleId, mark: mark });
    return this.http.put(url, body, this.option).pipe(map((res) => res));
  }
}
