import { Component, Input, Output, EventEmitter, Renderer2, Host, OnInit } from '@angular/core';
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
  }, DefaultComponent]

})

export class BranchFormComponent extends DetailComponent implements OnInit {


  @Input() public parent: Branch;
  @Input() public model: Branch;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';


  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Branch> = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
        public renderer: Renderer2, public metadata: MetaDataService, @Host() defaultComponent: DefaultComponent) {
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
}
