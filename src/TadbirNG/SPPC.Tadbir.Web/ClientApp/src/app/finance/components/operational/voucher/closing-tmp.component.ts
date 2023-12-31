import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ElementRef, ViewChild, ViewChildren } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { BrowserStorageService, MetaDataService, LookupService } from '@sppc/shared/services';
import { Layout } from '@sppc/shared/enum/metadata';
import { DefaultComponent, DetailComponent } from '@sppc/shared/class';
import { InventoryBalanceInfo, AccountInfo } from '@sppc/finance/service';
import { SppcNumericInput } from '@sppc/shared/controls';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'closing-tmp-voucher',
  styles: [
    `
    input[type=text],textarea { width: 100%; } ::ng-deep .k-dialog-buttongroup {border-color: #f1f1f1;}
    `
  ],
  templateUrl: './closing-tmp.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]

})

export class ClosingTmpComponent extends DetailComponent implements OnInit {  

  rowData: any[] = [];
  inventoryBalanceData: Array<InventoryBalanceInfo> = [];
  selectedRows: any[] = [];
  creditDebitMode: string = "1";
  creditDebit: number;

  @ViewChild('amount', {static: true}) txtAmount: SppcNumericInput;

  @Output() dataChanged: EventEmitter<any> = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService, public lookupService: LookupService,public elem:ElementRef) {
    super(toastrService, translate, bStorageService, renderer, metadata,'',0,elem);
  }

  ngOnInit(): void {    
    this.lookupService.GetInventoryAccountsLookup().subscribe(result => {
      this.rowData = result;      
      result.forEach(item => {
        var balance = new InventoryBalanceInfo();
        balance.accountId = item.id;
        balance.branchId = item.branchId;
        balance.creditBalance = 0;
        balance.debitBalance = 0;    
        this.inventoryBalanceData.push(balance);
      });

      this.dataChanged.emit(this.inventoryBalanceData);
    })
  }

  onSelectedKeysChange() {
    this.onKeysChange();
  }

  onKeysChange() {
    var index = this.inventoryBalanceData.findIndex(p => p.accountId === this.selectedRows[0]);
    if (index >= 0) {
      if (this.creditDebitMode == "1")
        this.creditDebit = this.inventoryBalanceData[index].debitBalance;
      else
        this.creditDebit = this.inventoryBalanceData[index].creditBalance;
    }
    this.setFocusInAmount();
  }

  setFocusInAmount() {    
    this.txtAmount.setFocus();
    this.txtAmount.select();
  }

  focusOutFunction() {
    var index = this.inventoryBalanceData.findIndex(p => p.accountId === this.selectedRows[0]);    
    if (index >= 0) {
      if (this.creditDebitMode == "1") {
        this.inventoryBalanceData[index].debitBalance = this.creditDebit;
        this.inventoryBalanceData[index].creditBalance = 0;
      }
      else {
        this.inventoryBalanceData[index].creditBalance = this.creditDebit;
        this.inventoryBalanceData[index].debitBalance = 0;
      }
    }

    this.dataChanged.emit(this.inventoryBalanceData);
  }

}
