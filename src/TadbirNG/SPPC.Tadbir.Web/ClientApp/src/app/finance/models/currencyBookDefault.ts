
export interface CurrencyBookDefault {
  displayType: string;
  currencyId: string;
  branchScope: string;
  voucherStatus: string;
  articleType: string;
  branchSeparation: boolean;
  currencyFreeRows: boolean;
}


export class CurrencyBookDefaultInfo implements CurrencyBookDefault {

  constructor() { }
  displayType: string;
  currencyId: string;
  branchScope: string;
  voucherStatus: string;
  articleType: string;
  branchSeparation: boolean;
  currencyFreeRows: boolean;
}
