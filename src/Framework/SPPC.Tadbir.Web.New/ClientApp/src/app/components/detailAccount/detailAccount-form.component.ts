import { Component, Input, Output, EventEmitter, Renderer2, Host, OnInit } from '@angular/core';
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
import { ViewName } from '../../security/viewName';


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

export class DetailAccountFormComponent extends DetailComponent implements OnInit {

  //create properties
  viewId: number;
  active: boolean = false;
  fullCodeApiUrl: string;
  editModel: DetailAccount;
  parentModel: DetailAccount;
  parentScopeValue: number = 0

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public set parent(parent: DetailAccount) {
    this.parentModel = parent;
    this.parentScopeValue = 0;
    this.fullCodeApiUrl = String.Format(DetailAccountApi.DetailAccountFullCode, 0);

    if (parent) {
      this.fullCodeApiUrl = String.Format(DetailAccountApi.DetailAccountFullCode, parent.id);
      this.parentScopeValue = parent.branchScope;
    }
  };

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

  ngOnInit(): void {
    this.viewId = ViewName.DetailAccount;
  }

  constructor(public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.DetailAccount, Metadatas.DetailAccount);
  }


}