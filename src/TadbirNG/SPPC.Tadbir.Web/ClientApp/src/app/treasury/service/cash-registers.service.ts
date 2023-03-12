import { Injectable } from '@angular/core';

export class CashRegistersInfo {
  id: number = 0;
  name: string;
  fiscalPeriodId: number;
  branchId: number;
  branchScope: number;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class CashRegistersService {

  constructor() { }
}
