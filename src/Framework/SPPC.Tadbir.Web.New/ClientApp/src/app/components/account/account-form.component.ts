import { Component, Input, Output, EventEmitter, Renderer2, OnInit, Host, Inject } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { AccountService, AccountInfo, VoucherLineService, FiscalPeriodService, LookupService } from '../../service/index';
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
  styles: ["input[type=text],textarea,.ddl-accGroup { width: 100%; } /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}"],
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
  parentScopeValue: number = 0;
  parentFullCode: string = '';

  accGroupList: Array<Item> = [];
  accGroupSelected: string;
  level: number = 0;

  @Input() public parent: Account;
  @Input() public model: Account;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Output() save: EventEmitter<Account> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();


  ////Events
  public onSave(e: any): void {
    e.preventDefault();

    if (this.editForm.valid) {
      if (this.model.id > 0) {
        let model: Account = this.editForm.value;
        model.branchId = this.model.branchId;
        model.fiscalPeriodId = this.model.fiscalPeriodId;
        model.companyId = this.model.companyId;
        this.save.emit(model);
      }
      else {
        let model: Account = this.editForm.value;
        model.branchId = this.BranchId;
        model.fiscalPeriodId = this.FiscalPeriodId;
        model.companyId = this.CompanyId;
        model.parentId = this.parent ? this.parent.id : undefined;
        model.level = this.level;
        this.save.emit(model);
      }
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  //Events

  ngOnInit(): void {

    this.viewId = ViewName.Account;

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
      this.getAccountGroups();
    }

    if (this.model && this.model.code)
      this.model.fullCode = this.parentFullCode + this.model.code;

    setTimeout(() => {
      this.editForm.reset(this.model);
    })

  }

  constructor(private accountService: AccountService, public toastrService: ToastrService, public translate: TranslateService, public lookupService: LookupService,
    public renderer: Renderer2, public metadata: MetaDataService) {
    super(toastrService, translate, renderer, metadata, Entities.Account, Metadatas.Account);

  }

  getAccountGroups() {
    this.lookupService.GetAccountGroupsLookup().subscribe(res => {
      this.accGroupList = res;

      if (this.model && this.model.groupId) {
        this.accGroupSelected = this.model.groupId.toString();
      }
    })
  }
}
