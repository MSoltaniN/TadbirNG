import { Component, Input, Output, EventEmitter, Renderer2, Host } from '@angular/core';
import { Branch } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailComponent } from '../../class/detail.component';
import { ViewName } from '../../security/viewName';
import { BrowserStorageService } from '../../service/browserStorage.service';



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
  }]

})

export class BranchFormComponent extends DetailComponent {

  //create properties
  active: boolean = false;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public parentTitle: string = '';
  @Input() public parentValue: string = '';

  @Input() public set model(branch: Branch) {
    this.editForm.reset(branch);

    this.active = branch !== undefined || this.isNew;
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Branch> = new EventEmitter();
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
        public renderer: Renderer2, public metadata: MetaDataService, @Host() defaultComponent: DefaultComponent) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Branch, ViewName.Branch);
  }


}
