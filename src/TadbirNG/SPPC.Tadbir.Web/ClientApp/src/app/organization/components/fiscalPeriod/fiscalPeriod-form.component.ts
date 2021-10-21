import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { FiscalPeriod } from '@sppc/organization/models';
import { DetailComponent, Property } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';



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

export class FiscalPeriodFormComponent extends DetailComponent implements OnInit {

  public fiscalPeriod_Id: number;

  @Input() public model: FiscalPeriod;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;
  @Input() public isWizard: boolean = false;

  public startDateDisplayType:string;
  public endDateDisplayType: string;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<FiscalPeriod> = new EventEmitter();
  @Output() previousStep: EventEmitter<any> = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.FiscalPeriod, ViewName.FiscalPeriod);

  }

  ngOnInit(): void {
    this.editForm.reset();
    
    this.setDateDisplayType();

    setTimeout(() => {

      if (this.model && this.isNew) {
        this.model.startDate = this.getStartDate();
        this.model.endDate = this.getEndDate();
      }

      this.model.companyId = this.CompanyId;
      this.editForm.reset(this.model);
      
      if (this.model) {
        this.fiscalPeriod_Id = this.model.id;
      }

    })  
  }

  setDateDisplayType() {   
    this.startDateDisplayType = this.getProperties(this.metadataKey).filter(p => p.name == "StartDate")[0].type;
    this.endDateDisplayType = this.getProperties(this.metadataKey).filter(p => p.name == "EndDate")[0].type;
  }

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
      return new Date(new Date().getFullYear() + 1, 2, 20, 0, 0, 0);
    }
    else {
      return new Date(new Date().getFullYear(), 11, 31, 0, 0, 0);
    }
  }

  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      var model = <FiscalPeriod>this.editForm.value;
      model.companyId = this.CompanyId;
      this.save.emit(model);
    }
    else if (this.isWizard) {
      this.save.emit(null);
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }

  public onDeleteData() {
    alert("Data deleted.");
  }

  onPreviousStep() {
    this.previousStep.emit();
  }
}
