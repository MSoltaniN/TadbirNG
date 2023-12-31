import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { DetailComponent } from '@sppc/shared/class';
import { CurrencyRate } from '@sppc/finance/models';
import { CurrencyService } from '@sppc/finance/service';
import { BrowserStorageService, MetaDataService, LookupService } from '@sppc/shared/services';
import { Entities } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';



@Component({
  selector: 'currencyRate-form-component',
  templateUrl: './currencyRate-form.component.html',
  styles: [`
    input[type=text],textarea { width: 100%; }
input.active-input {
  background: #fff !important;
  border: 1px solid #ccc !important;
}
.dialog-body{ width: 800px } .dialog-body hr{ border-top: dashed 1px #eee; }
@media screen and (max-width:800px) {
  .dialog-body{
    width: 99%;
    min-width: 250px;
  }
}

`  ],
})

export class CurrencyRateFormComponent extends DetailComponent implements OnInit {

  parentScopeValue: number = 0;

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;
  @Input() public model: CurrencyRate;
  @Input() public currencyName: string;  
  dateDisplayType: string;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<CurrencyRate> = new EventEmitter();
  @Output() setFocus: EventEmitter<any> = new EventEmitter();
   

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
    public lookupService: LookupService, public currencyService: CurrencyService, public renderer: Renderer2, public metadata: MetaDataService,public elem:ElementRef) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.CurrencyRate, ViewName.CurrencyRate,elem);

  }

  setDateDisplayType() {    
    
    this.dateDisplayType = this.getProperties(this.metadataKey).filter(p => p.name == "Date")[0].type;
  }


  ngOnInit(): void {

    this.setDateDisplayType();

    this.editForm.reset();
    this.editForm.patchValue({
      id: this.model.id,
      currencyId: this.model.currencyId,
      branchId: this.model.branchId,
      branchName: this.model.branchName,
      time: this.model.time,
      multiplier: this.model.multiplier,
      date: this.model.date,
      description: this.model.description,
      branchScope: this.model.branchScope
    });
  }

}
