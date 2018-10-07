import { Component, Input, Output, EventEmitter, Renderer2, Host } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { DetailAccount } from '../../model/index';
import { Property } from "../../class/metadata/property"
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailAccountApi } from '../../service/api/index';
import { String } from '../../class/source';
import { DetailComponent } from '../../class/detail.component';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  Key: string,
  Value: string
}


@Component({
  selector: 'detailAccount-form-component',
  styles: [
    "input[type=text],textarea { width: 100%; }"
  ],
  templateUrl: './detailAccount-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class DetailAccountFormComponent extends DetailComponent {

  //create properties
  active: boolean = false;
  fullCodeApiUrl: string;
  editModel: DetailAccount;

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public parentTitle: string = '';
  @Input() public parentValue: string = '';
  @Input() public parentScopeValue: number = 0;

  @Input() public set parentId(id: number) {
    this.fullCodeApiUrl = String.Format(DetailAccountApi.DetailAccountFullCode, id ? id : 0);
  }

  @Input() public set model(detailAccount: DetailAccount) {
    this.editModel = detailAccount;
    this.editForm.reset(detailAccount);

    this.active = detailAccount !== undefined || this.isNew;
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<DetailAccount> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      if (this.editForm.valid) {
        if (this.editModel) {
          let model: DetailAccount = this.editForm.value;
          model.branchId = this.editModel.branchId;
          model.fiscalPeriodId = this.editModel.fiscalPeriodId;
          model.companyId = this.editModel.companyId;
          this.save.emit(model);
        }
        else
          this.save.emit(this.editForm.value);
        this.active = true;
      }
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
  //Events

  constructor(public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.DetailAccount, Metadatas.DetailAccount);
  }


}
