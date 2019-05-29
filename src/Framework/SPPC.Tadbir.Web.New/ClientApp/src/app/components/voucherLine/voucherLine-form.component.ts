import { Component, Input, Output, EventEmitter, Renderer2, Host, OnInit } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { VoucherLine } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Metadatas, Entities } from '../../../environments/environment';
import { FullAccountService } from '../../service/fullAccount.service';
import { VoucherLineService, AccountService, LookupService } from '../../service/index';
import { DetailComponent } from '../../class/detail.component';
import { LookupApi } from '../../service/api/lookupApi';
import { ViewName } from '../../security/viewName';




interface Item {
  Key: string,
  Value: string
}


@Component({
  selector: 'voucherLine-form-component',
  styles: [`
    input[type=text],textarea,.ddl-currency,.ddl-type { width: 100%; } /deep/ kendo-numerictextbox{ width:100% !important; }

.dialog-body{ width: 800px } .dialog-body hr{ border-top: dashed 1px #eee; }
@media screen and (max-width:800px) {
  .dialog-body{
    width: 90%;
    min-width: 250px;
  }
}

.voucher-mode-item { display: inline; margin: 0 10px; }
`  ],
  templateUrl: './voucherLine-form.component.html'
})

export class VoucherLineFormComponent extends DetailComponent implements OnInit {

  //TODO: create form with metadata
  public editForm1 = new FormGroup({
    id: new FormControl(),
    voucherId: new FormControl(),
    currencyId: new FormControl(),
    debit: new FormControl(),
    credit: new FormControl(),
    currencyValue: new FormControl(),
    typeId: new FormControl(),
    description: new FormControl("", Validators.maxLength(512)),
    fullAccount: new FormGroup({
      account: new FormGroup({
        id: new FormControl("", Validators.required),
        name: new FormControl(),
        fullCode: new FormControl()
      }),
      detailAccount: new FormGroup({
        id: new FormControl(),
        name: new FormControl(),
        fullCode: new FormControl()
      }),
      costCenter: new FormGroup({
        id: new FormControl(),
        name: new FormControl(),
        fullCode: new FormControl()
      }),
      project: new FormGroup({
        id: new FormControl(),
        name: new FormControl(),
        fullCode: new FormControl()
      })
    })
  });

  currenciesRows: Array<Item>;
  voucherLineTypeList: Array<Item> = [];

  selectedCurrencyValue: string | undefined;
  selectedArticleType: string = "0";
  creditDebiteMode: string = "1";

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;
  @Input() public isNewBalance: boolean = false;
  @Input() public balance: number = 0;
  @Input() public model: VoucherLine;

  @Input() public isDisplayCurrency: boolean = false;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<{ model: VoucherLine, isOpen: boolean }> = new EventEmitter();
  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  //create properties

  //Events
  public onSave(e: any, isOpen: boolean): void {
    e.preventDefault();
    if (this.editForm1.valid) {
      var model = this.editForm1.value;

      if (!model.debit)
        model.debit = 0;
      if (!model.credit)
        model.credit = 0;

      if (this.creditDebiteMode == "1")
        model.credit = 0;
      else
        model.debit = 0;
      debugger;
      this.save.emit({ model, isOpen });

    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }
  //Events

  ngOnInit(): void {

    this.editForm1.reset(this.model);

    this.getArticleType();
    this.GetCurrencies();

    if (this.isNewBalance)
      if (this.balance > 0) {
        this.editForm1.patchValue({ 'credit': Math.abs(this.balance) });
        this.creditDebiteMode = "2";
      }
      else
        if (this.balance < 0) {
          this.editForm1.patchValue({ 'debit': Math.abs(this.balance) });
          this.creditDebiteMode = "1";
        }

    if (!this.isNew) {
      if (this.model.credit > 0)
        this.creditDebiteMode = "2";
      else
        this.creditDebiteMode = "1";
    }

  }

  constructor(public toastrService: ToastrService, public translate: TranslateService,
    public lookupService: LookupService, public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.VoucherLine, ViewName.VoucherLine);

  }


  GetCurrencies() {
    this.lookupService.GetCurrenciesLookup().subscribe(res => {
      this.currenciesRows = res;

      if (this.model != undefined && this.model.currencyId != undefined) {
        this.selectedCurrencyValue = this.model.currencyId.toString();
      }

    })
  }

  getArticleType() {
    this.lookupService.getModels(LookupApi.VoucherLineTypes).subscribe(res => {
      this.voucherLineTypeList = res;
    })
  }

  focusHandler(e: any) {
    this.setFocus.emit();
  }

}
