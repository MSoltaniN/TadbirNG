import { Component, Input, Output, EventEmitter, Renderer2,  OnInit } from '@angular/core';
import { DetailAccount } from '../../model/index';
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

interface Item {
  Key: string,
  Value: string
}


@Component({
  selector: 'detailAccount-form-component',
  styles: [
    `input[type=text],textarea { width: 100%; }  /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}`
  ],
  templateUrl: './detailAccount-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]

})

export class DetailAccountFormComponent extends DetailComponent implements OnInit {

  //create properties
  viewId: number;
  parentScopeValue: number = 0;
  parentFullCode: string = '';
  level: number = 0;

  @Input() public parent: DetailAccount;
  @Input() public model: DetailAccount;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Output() save: EventEmitter<DetailAccount> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();

    let model: DetailAccount = this.editForm.value;
    if (this.editForm.valid) {
      if (this.model.id > 0) {
        model.branchId = this.model.branchId;
        model.fiscalPeriodId = this.model.fiscalPeriodId;
        model.companyId = this.model.companyId;
      }
      else {
        model.branchId = this.BranchId;
        model.fiscalPeriodId = this.FiscalPeriodId;
        model.companyId = this.CompanyId;
        model.parentId = this.parent ? this.parent.id : undefined;
        model.level = this.level;
      }
      this.save.emit(model);
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }
  //Events

  ngOnInit(): void {
    this.viewId = ViewName.DetailAccount;

    this.editForm.reset();

    this.parentScopeValue = 0;

    if (this.parent) {
      this.parentFullCode = this.parent.fullCode;
      this.model.fullCode = this.parentFullCode;
      this.parentScopeValue = this.parent.branchScope;
      this.level = this.parent.level + 1;
    }
    else {
      this.level = 0;
    }

    if (this.model && this.model.code)
      this.model.fullCode = this.parentFullCode + this.model.code;

    setTimeout(() => {
      this.editForm.reset(this.model);
    })
  }

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.DetailAccount, ViewName.DetailAccount);
  }


}
