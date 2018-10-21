import { Component, Input, Output, EventEmitter, Renderer2, OnInit, Host, Inject } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { AccountService, AccountInfo, VoucherLineService, FiscalPeriodService } from '../../service/index';
import { Account } from '../../model/index';
import { Property } from "../../class/metadata/property"
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { AccountApi } from '../../service/api/accountApi';
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
  selector: 'account-form-component',
  styles: ["input[type=text],textarea { width: 100%; },"],
  templateUrl: './account-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]
})

export class AccountFormComponent extends DetailComponent implements OnInit {

  //create properties
  viewId: number;
  active: boolean = false;

  fullCodeApiUrl: string;
  editModel: Account;
  parentModel: Account;
  parentScopeValue: number = 0;

  @Input() public disableSaveBtn: boolean = false;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public set parent(parent: Account) {
    //debugger;
    //console.log(parent);
    this.parentModel = parent;
    this.parentScopeValue = 0;
    this.fullCodeApiUrl = String.Format(AccountApi.AccountFullCode, 0);

    if (parent) {
      this.fullCodeApiUrl = String.Format(AccountApi.AccountFullCode, parent.id);
      this.parentScopeValue = parent.branchScope;
    }
  };

  @Input() public set model(account: Account) {
    //debugger;
    this.editModel = account;
    this.editForm.reset(account);

    this.active = account !== undefined || this.isNew;
    this.disableSaveBtn = false;
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Account> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      this.disableSaveBtn = true;
      if (this.editModel.id > 0) {
        let model: Account = this.editForm.value;
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
    this.viewId = ViewName.Account;
    if (this.parentModel)
      this.parentScopeValue = this.parentModel.branchScope;
  }

  constructor(private accountService: AccountService, private voucherLineService: VoucherLineService, private fiscalPeriodService: FiscalPeriodService,
    public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.Account, Metadatas.Account);
  }


}
