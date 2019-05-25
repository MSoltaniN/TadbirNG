import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { AccountService, LookupService } from '../../service/index';
import { Account } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
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
  selector: 'relatedAccounts-form-component',
  styles: ["input[type=text],textarea,.ddl-accGroup { width: 100%; } /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}"],
  templateUrl: './relatedAccounts-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]
})

export class RelatedAccountsFormComponent extends DetailComponent implements OnInit {

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
  @Input() public accGroupId: number;

  @Output() save: EventEmitter<Account> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();

    let model: Account = this.editForm.value;
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
    this.viewId = ViewName.Account;

    this.editForm.reset();

    this.parentScopeValue = 0;

    debugger;

    if (this.parent) {
      this.parentFullCode = this.parent.fullCode;
      this.model.fullCode = this.parentFullCode;
      this.parentScopeValue = this.parent.branchScope;
      this.level = this.parent.level + 1;
    }
    else {
      this.level = 0;

      this.editForm.patchValue({ groupId: this.accGroupId });
      
      setTimeout(() => {
        this.getAccountGroups();
      })
    }

    if (this.model && this.model.code)
      this.model.fullCode = this.parentFullCode + this.model.code;

    setTimeout(() => {
      this.editForm.reset(this.model);
    })

  }

  constructor(private accountService: AccountService, public toastrService: ToastrService, public translate: TranslateService, public lookupService: LookupService,
    public renderer: Renderer2, public metadata: MetaDataService) {
    super(toastrService, translate, renderer, metadata, Entities.Account, ViewName.Account);
  }

  getAccountGroups() {
    this.lookupService.GetAccountGroupsLookup().subscribe(res => {
      this.accGroupList = res;

      this.accGroupSelected = this.accGroupId.toString();
    })
  }

  //getAccountGroups() {
  //  this.lookupService.GetAccountGroupsLookup().subscribe(res => {
  //    this.accGroupList = res;

  //    if (this.model && this.model.groupId) {
  //      this.accGroupSelected = this.model.groupId.toString();
  //    }
  //  })
  //}
}
