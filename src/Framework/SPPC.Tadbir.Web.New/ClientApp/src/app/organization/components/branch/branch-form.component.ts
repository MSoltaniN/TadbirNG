import { Component, Input, Output, EventEmitter, Renderer2, Host, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { Layout, Entities } from '@sppc/env/environment';
import { Branch } from '@sppc/organization/models';
import { DefaultComponent, DetailComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'branch-form-component',
  styles: [
    "input[type=text],textarea { width: 100%; }"
  ],
  templateUrl: './branch-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]

})

export class BranchFormComponent extends DetailComponent implements OnInit {


  @Input() public parent: Branch;
  @Input() public model: Branch;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';
  @Input() public isWizard: boolean = false;


  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Branch> = new EventEmitter();
  @Output() previousStep: EventEmitter<any> = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Branch, ViewName.Branch);
  }

  ngOnInit(): void {

    this.editForm.reset();

    setTimeout(() => {
      this.editForm.reset(this.model);
    })

  }


  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      let model: Branch = this.editForm.value;
      model.companyId = this.CompanyId;
      if (this.isNew) {
        model.level = this.parent ? this.parent.level + 1 : 0;
        model.parentId = this.parent ? this.parent.id : null;
      }
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

  onPreviousStep() {
    this.previousStep.emit();
  }
}
