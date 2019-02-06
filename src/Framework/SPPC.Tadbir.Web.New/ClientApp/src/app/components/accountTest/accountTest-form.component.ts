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
import { String } from '../../class/source';
import { BranchApi } from '../../service/api/index';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  Key: string,
  Value: string
}


@Component({
  selector: 'accountTest-form-component',
  styles: [`
input[type=text],.ddl-accGroup { width: 100%; } /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}
/deep/ .dialog-body .k-tabstrip > .k-content { padding:0; }
.dialog-body{ width: 800px } .dialog-body hr{ border-top: dashed 1px #eee; }
@media screen and (max-width:800px) {
  .dialog-body{
    width: 90%;
    min-width: 250px;
  }
}
/deep/ .k-tabstrip-top > .k-tabstrip-items { border-color: #f4f4f4; }
/deep/ .k-tabstrip-top > .k-tabstrip-items .k-item.k-state-active { border-bottom-color: white; }
`],
  templateUrl: './accountTest-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]
})

export class AccountTestFormComponent extends DetailComponent implements OnInit {

  //create properties
  viewId: number;
  parentScopeValue: number = 0;
  parentFullCode: string = '';

  accGroupList: Array<Item> = [];
  accGroupSelected: string;
  level: number = 0;
  branch_Id: number;
  branchName: string;

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

  escPress() {
    this.cancel.emit();
  }
  //Events

  ngOnInit(): void {
    this.viewId = ViewName.Account;

    this.editForm.reset();

    this.getBranchName();

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
    else
      this.model.fullCode = this.parentFullCode;

    


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


  getBranchName() {
    if (this.model && this.model.branchId)
      this.branch_Id = this.model.branchId;
    else
      this.branch_Id = this.BranchId;

    this.accountService.getById(String.Format(BranchApi.Branch, this.branch_Id)).subscribe(res => {
      this.branchName = res.name;
    })

  }
}
