import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { FiscalPeriodService } from '../../service/index';
import { FiscalPeriod } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailComponent } from '../../class/detail.component';
import { ViewName } from '../../security/viewName';
import { BrowserStorageService } from '../../service/browserStorage.service';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  Key: string,
  Value: string
}


@Component({
  selector: 'fiscalPeriod-form-component',
  styles: [
    "input[type=text],textarea { width: 100%; }"
  ],
  templateUrl: './fiscalPeriod-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class FiscalPeriodFormComponent extends DetailComponent {

  //create properties
  public fiscalPeriod_Id: number;
  active: boolean = false;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;

  @Input() public set model(fiscalPeriod: FiscalPeriod) {

    if (fiscalPeriod && this.isNew) {
      fiscalPeriod.startDate = this.getStartDate();
      fiscalPeriod.endDate = this.getEndDate();
      new Date()
    }

    this.editForm.reset(fiscalPeriod);

    this.active = fiscalPeriod !== undefined || this.isNew;

    if (fiscalPeriod != undefined) {
      this.fiscalPeriod_Id = fiscalPeriod.id;
    }

  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<FiscalPeriod> = new EventEmitter();
  //create properties


  getStartDate(): Date {
    if (this.CurrentLanguage == "fa") {
      return new Date(new Date().getFullYear(), 2, 21, 0, 0, 0);
    }
    else {
      return new Date(new Date().getFullYear(), 0, 1, 0, 0, 0);
    }
  }

  getEndDate(): Date {
    if (this.CurrentLanguage == "fa") {
      return new Date(new Date().getFullYear() + 1, 2, 19, 0, 0, 0);
    }
    else {
      return new Date(new Date().getFullYear(), 11, 31, 0, 0, 0);
    }
  }

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      this.save.emit(this.editForm.value);
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

  escPress() {
    this.closeForm();
  }

  public onDeleteData() {
    alert("Data deleted.");
  }
  //Events

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.FiscalPeriod, ViewName.FiscalPeriod);

  }

}
