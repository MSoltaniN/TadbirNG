import { Component, Input, Output, EventEmitter, Renderer2, Host, OnInit } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { VoucherLine } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { MetaDataService } from '../../service/metadata/metadata.service';
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
  styles: [`
    input[type=text],textarea { width: 100%; } /deep/ kendo-numerictextbox{ width:100% !important; }
    /deep/ .dialog-style .k-dialog { width:250px } @media (max-width: 450px) { /deep/ .dialog-style .k-dialog { width:100% } }
`  ],
  templateUrl: './voucherLine-form.component.html'
})

export class VoucherLineFormComponent extends DetailComponent implements OnInit {

  //TODO: create form with metadata
  public editForm1 = new FormGroup({
    id: new FormControl(),
    voucherId: new FormControl(),
    currencyId: new FormControl("", Validators.required),
    debit: new FormControl(),
    credit: new FormControl(),
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

  public currenciesRows: Array<Item>;

  public selectedCurrencyValue: string | undefined;

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;
  @Input() public isNewBalance: boolean = false;
  @Input() public balance: number = 0;
  @Input() public model: VoucherLine;


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
    this.editForm1.reset();

    this.editForm1.reset(this.model);

    if (this.model != undefined && this.model.currencyId != undefined) {
      this.selectedCurrencyValue = this.model.currencyId.toString();
    }

    if (this.isNewBalance)
      if (this.balance > 0)
        this.editForm1.patchValue({ 'credit': Math.abs(this.balance) });
      else
        if (this.balance < 0)
          this.editForm1.patchValue({ 'debit': Math.abs(this.balance) });


    setTimeout(() => {
      this.editForm1.reset(this.model);
    })
  }

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

  focusHandler(e: any) {
    this.setFocus.emit();
  }

}
