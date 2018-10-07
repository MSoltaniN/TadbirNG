import { Component, Input, Output, EventEmitter, Renderer2, Host } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
//import { requiredValidatorLogic } from './required.directive';
//import { VoucherLineService, VoucherLineInfo, AccountService, LookupService } from '../../service/index';

import { VoucherLine } from '../../model/index';

import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { MetaDataService } from '../../service/metadata/metadata.service';

//import createNumberMask from 'text-mask-addons/dist/createNumberMask';
import { Metadatas, Entities } from '../../../environments/environment';
import { FullAccountService } from '../../service/fullAccount.service';
import { VoucherLineService, AccountService, LookupService } from '../../service/index';
import { DetailComponent } from '../../class/detail.component';




interface Item {
  Key: string,
  Value: string
}


@Component({
  selector: 'voucherLine-form-component',
  styles: [
    "input[type=text],textarea { width: 100%; } .ddl-fAcc {width:49%} /deep/ kendo-numerictextbox{ width:100% !important; }"
  ],
  templateUrl: './voucherLine-form.component.html'
})

export class VoucherLineFormComponent extends DetailComponent {

  //TODO: create form with metadata
  public editForm1 = new FormGroup({
    id: new FormControl(),
    voucherId: new FormControl(),
    currencyId: new FormControl("", Validators.required),
    debit: new FormControl("", Validators.required),
    credit: new FormControl("", Validators.required),
    description: new FormControl("", Validators.maxLength(512)),
    fullAccount: new FormGroup({
      accountId: new FormControl("", Validators.required),
      detailId: new FormControl(),
      costCenterId: new FormControl(),
      projectId: new FormControl(),
    })
  });

  public currenciesRows: Array<Item>;

  public selectedCurrencyValue: string | undefined;

  active: boolean = false;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;


  @Input() public set model(voucherLine: VoucherLine) {

    this.editForm1.reset(voucherLine);

    this.active = voucherLine !== undefined || this.isNew;

    if (voucherLine != undefined && voucherLine.currencyId != undefined)
      this.selectedCurrencyValue = voucherLine.currencyId.toString();

  }


  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<{ model: VoucherLine, isOpen: boolean }> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any, isOpen: boolean): void {
    e.preventDefault();
    if (this.editForm1.valid) {

      var model = this.editForm1.value;
      this.save.emit({ model, isOpen });

      this.active = true;
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.isNew = false;
    this.active = false;
    this.cancel.emit();
  }
  //Events

  constructor(private voucherLineService: VoucherLineService, private accountService: AccountService,
    public toastrService: ToastrService, public translate: TranslateService,
    public lookupService: LookupService, private fullAccountService: FullAccountService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.VoucherLine, Metadatas.VoucherArticles);

    this.GetCurrencies();
  }


  GetCurrencies() {
    this.lookupService.GetCurrenciesLookup().subscribe(res => {
      this.currenciesRows = res;
    })
  }

}
