import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { String, DetailComponent } from '@sppc/shared/class';
import { Currency, CurrencyService, CurrencyApi } from '@sppc/finance';
import { BrowserStorageService, MetaDataService, LookupService, ViewName } from '@sppc/shared';
import { Entities, MessageType } from 'environments/environment';





interface Item {
  key: string,
  value: string
}


@Component({
  selector: 'currency-form-component',
  styles: [`
    input[type=text],textarea,.ddl-currency { width: 100%; }

.dialog-body{ width: 800px } .dialog-body hr{ border-top: dashed 1px #eee; }
@media screen and (max-width:800px) {
  .dialog-body{
    width: 90%;
    min-width: 250px;
  }
}

`  ],
  templateUrl: './currency-form.component.html'
})

export class CurrencyFormComponent extends DetailComponent implements OnInit {


  currencyNameLookup: Array<Item> = [];
  currencyNameData: Array<Item> = [];
  selectedCurrencyName: string;

  currencyId: number;

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;
  @Input() public model: Currency;



  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Currency> = new EventEmitter();
  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    this.editForm.patchValue({ id: this.currencyId ? this.currencyId : 0 });
    this.save.emit(this.editForm.value);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }
  //Events

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public lookupService: LookupService, public currencyService: CurrencyService, public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.VoucherLine, ViewName.Currency);

  }

  ngOnInit(): void {
    this.currencyId = this.model.id;
    this.editForm.reset();
    this.getCurrencyNames();
  }

  getCurrencyNames() {
    this.currencyService.getModels(CurrencyApi.CurrencyNamesLookup).subscribe(res => {
      this.currencyNameLookup = res;
      this.currencyNameData = res;

      if (!this.isNew) {
        var currencyItem = this.currencyNameData.find(f => f.value == this.model.name);

        this.selectedCurrencyName = currencyItem ? currencyItem.key : undefined;

        this.editForm.reset(this.model);
      }
    })
  }

  onChangeCurrency(item: any) {
    this.editForm.patchValue({ name: item });
    if (item)
      this.currencyService.getModels(String.Format(CurrencyApi.CurrencyInfoByName, item)).subscribe(res => {
        this.editForm.reset(res);
      }, error => {
        if (error.status == 404)
          this.showMessage(this.getText('App.RecordNotFound'), MessageType.Warning);
      })
  }

  handleFilter(value: any) {
    this.currencyNameData = this.currencyNameLookup.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }
}
