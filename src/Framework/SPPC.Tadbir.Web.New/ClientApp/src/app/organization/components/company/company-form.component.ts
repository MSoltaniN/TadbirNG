import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/env/environment';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { CompanyDb } from '@sppc/organization';
import { DetailComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'company-form-component',
  styles: [`
        input[type=text],textarea ,input[type=password]{ width: 100%; }
    `],
  templateUrl: './company-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class CompanyFormComponent extends DetailComponent {

  //create properties
  active: boolean = false;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public set model(company: CompanyDb) {
    this.editForm.reset(company);

    this.active = company !== undefined || this.isNew;
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<CompanyDb> = new EventEmitter();
  //create properties

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
  //Events

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Company, ViewName.Company);
  }


}
