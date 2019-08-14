import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { String, DetailComponent } from '@sppc/shared/class';
import { Currency, CurrencyRate } from '@sppc/finance/models';
import { CurrencyService } from '@sppc/finance/service';
import { CurrencyApi } from '@sppc/finance/service/api';
import { BrowserStorageService, MetaDataService, LookupService } from '@sppc/shared/services';
import { Entities, MessageType } from '@sppc/env/environment';
import { ViewName } from '@sppc/shared/security';



@Component({
  selector: 'currencyRate-form-component',
  templateUrl: './currencyRate-form.component.html',
  styles: [`
    input[type=text],textarea { width: 100%; }

.dialog-body{ width: 800px } .dialog-body hr{ border-top: dashed 1px #eee; }
@media screen and (max-width:800px) {
  .dialog-body{
    width: 90%;
    min-width: 250px;
  }
}

`  ],
})

export class CurrencyRateFormComponent extends DetailComponent implements OnInit {

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;
  @Input() public model: CurrencyRate;
  @Input() public currencyName: string;



  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<CurrencyRate> = new EventEmitter();
  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
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

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.CurrencyRate, ViewName.CurrencyRate);

  }

  ngOnInit(): void {
    this.editForm.reset();
    this.editForm.patchValue({
      id: this.model.id,
      currencyId: this.model.currencyId,
      branchId: this.model.branchId,
      branchName: this.model.branchName,
      time: this.model.time,
      multiplier: this.model.multiplier,
      date: this.model.date,
      description: this.model.description
    });
  }


}
