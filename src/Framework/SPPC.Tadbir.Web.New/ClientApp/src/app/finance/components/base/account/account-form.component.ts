import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { String, DefaultComponent, DetailComponent } from '@sppc/shared/class';
import { Layout, Entities } from '@sppc/env/environment';
import { AccountService, AccountFullDataInfo, AccountInfo, CustomerTaxInfoModel } from '@sppc/finance/service';
import { Account } from '@sppc/finance/models';
import { MetaDataService, BrowserStorageService, LookupService } from '@sppc/shared/services';
import { BranchApi } from '@sppc/organization/service/api';
import { ViewName } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountFullData } from '@sppc/finance/models/accountFullData';
import { CustomerTaxInfo } from '@sppc/finance/models/customerTaxInfo';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  key: string,
  value: string
}


@Component({
  selector: 'account-form-component',
  styles: [`
input[type=text],.ddl-acc,textarea { width: 100%; } /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}
/deep/ .dialog-body .k-tabstrip > .k-content { padding:15px; }
.dialog-body{ width: 800px } .dialog-body hr{ border-top: dashed 1px #eee; }
@media screen and (max-width:800px) {
  .dialog-body{
    width: 90%;
    min-width: 250px;
  }
}
/deep/ .k-tabstrip-top > .k-tabstrip-items { border-color: #f4f4f4; }
/deep/ .k-tabstrip-top > .k-tabstrip-items .k-item.k-state-active { border-bottom-color: white; }

/deep/ .k-switch-on .k-switch-handle { left: -8px !important; }
/deep/ .k-switch-off .k-switch-handle { left: -4px !important; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-on { right: -22px; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-off { left: 0; }
/deep/ .k-switch-label-on,/deep/ .k-switch-label-off { overflow: initial; }
.acc-form { min-height: 386px; }
`],
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
  branch_Id: number;
  branchName: string;

  selectedCurrencyValue: string;
  currenciesRows: Array<Item>;
  filteredCurrencies: Array<Item>;

  selectedTurnoverModeValue: string = "-1";
  turnovermodes: Array<Item>;

  provincesList: Array<Item> = [];
  filteredProcinces: Array<Item> = [];
  selectedProvince: string;


  isDisableCustomerTaxTab: boolean = false;

  accountModel: Account;
  customerTaxModel: CustomerTaxInfo;

  @Input() public parent: Account;
  @Input() public model: AccountFullData;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Output() save: EventEmitter<AccountFullData> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  personTypes: Array<Item> = [
    { key: "1", value: "حقیقی" },
    { key: "2", value: "حقوقی" }
  ]

  buyerTypes: Array<Item> = [
    { key: "1", value: "مودی مشمول ثبت نام در نظام مالیاتی" },
    { key: "2", value: "مشمولین حقیقی ماده 81" },
    { key: "3", value: "اشخاصی که مشمول ثبت نام در نظام مالیاتی نیستند" },
    { key: "4", value: "مصرف کننده نهایی" },
  ]

  public accountDataForm = new FormGroup({
    account: new FormGroup({
      name: new FormControl('', Validators.required),
      groupId: new FormControl(''),
      code: new FormControl(''),
      fullCode: new FormControl(''),
      description: new FormControl(''),
      branchScope: new FormControl(''),
    }),
    features: new FormGroup({
      currencyId: new FormControl(''),
      turnoverMode: new FormControl(''),
      isActive: new FormControl(''),
      isCurrencyAdjustable: new FormControl(''),
    }),
    customerTax: new FormGroup({
      id: new FormControl(),
      accountId: new FormControl(),
      customerFirstName: new FormControl(''),
      customerName: new FormControl(''),
      personType: new FormControl(''),
      buyerType: new FormControl(''),
      economicCode: new FormControl(''),
      address: new FormControl(''),
      nationalCode: new FormControl(''),
      perCityCode: new FormControl(''),
      phoneNo: new FormControl(''),
      mobileNo: new FormControl(''),
      postalCode: new FormControl(''),
      description: new FormControl(''),
    }),
  });


  constructor(private accountService: AccountService, public toastrService: ToastrService, public translate: TranslateService, public lookupService: LookupService,
    public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Account, ViewName.Account);
  }


  ngOnInit(): void {

    if (this.model) {
      this.accountModel = this.model.account;
      this.customerTaxModel = this.model.customerTaxInfo;

      if (this.isNew || this.model.customerTaxInfo == null) {
        this.isDisableCustomerTaxTab = true;
      }
    }
    else {
      this.accountModel = new AccountInfo();
      this.customerTaxModel = new CustomerTaxInfoModel();
    }

    this.viewId = ViewName.Account;

    ////this.editForm.reset();
    this.getAccountGroups();
    this.getBranchName();
    this.getCurrencies();
    this.getTurnoverModes();
    //this.getProvince()

    this.parentScopeValue = 0;
    if (this.parent) {
      this.parentFullCode = this.parent.fullCode;
      this.accountModel.fullCode = this.parentFullCode;
      this.parentScopeValue = this.parent.branchScope;
      this.level = this.parent.level + 1;
    }
    else {
      this.level = 0;
    }

    if (this.accountModel && this.accountModel.code)
      this.accountModel.fullCode = this.parentFullCode + this.accountModel.code;
    else
      this.accountModel.fullCode = this.parentFullCode;



    if (!this.model) {
      this.accountDataForm.reset();
    }
    else {
      this.accountDataForm.patchValue({
        account: {
          name: this.accountModel.name,
          groupId: this.accountModel.groupId,
          code: this.accountModel.code,
          fullCode: this.accountModel.fullCode,
          description: this.accountModel.description,
          branchScope: this.accountModel.branchScope,
        },
        features: {
          currencyId: this.accountModel.currencyId,
          turnoverMode: this.accountModel.turnoverMode,
          isActive: this.accountModel.isActive,
          isCurrencyAdjustable: this.accountModel.isCurrencyAdjustable,
        }
      })

      if (this.customerTaxModel) {
        this.accountDataForm.patchValue({
          customerTax: {
            id: this.customerTaxModel.id,
            accountId: this.customerTaxModel.accountId,
            customerFirstName: this.customerTaxModel.customerFirstName,
            customerName: this.customerTaxModel.customerName,
            personType: this.customerTaxModel.personType ? this.customerTaxModel.personType.toString() : "1",
            buyerType: this.customerTaxModel.buyerType ? this.customerTaxModel.buyerType.toString() : "1",
            economicCode: this.customerTaxModel.economicCode,
            address: this.customerTaxModel.address,
            nationalCode: this.customerTaxModel.nationalCode,
            perCityCode: this.customerTaxModel.perCityCode,
            phoneNo: this.customerTaxModel.phoneNo,
            mobileNo: this.customerTaxModel.mobileNo,
            postalCode: this.customerTaxModel.postalCode,
            description: this.customerTaxModel.description,
          }
        })
      }
    }
  }

  getAccountGroups() {
    this.lookupService.GetAccountGroupsLookup().subscribe(res => {
      this.accGroupList = res;

      if (this.accountModel && this.accountModel.groupId) {
        this.accGroupSelected = this.accountModel.groupId.toString();
      }
      else
        if (this.parent) {
          this.accGroupSelected = this.parent.groupId.toString();
        }
    })
  }

  getBranchName() {
    if (this.accountModel && this.accountModel.branchId)
      this.branch_Id = this.accountModel.branchId;
    else
      this.branch_Id = this.BranchId;

    this.accountService.getById(String.Format(BranchApi.Branch, this.branch_Id)).subscribe(res => {
      this.branchName = res.name;
    })

  }

  getCurrencies() {
    this.lookupService.GetCurrenciesLookup().subscribe(res => {
      this.currenciesRows = res;
      this.filteredCurrencies = res;
      if (this.accountModel != undefined && this.accountModel.currencyId != undefined) {
        this.selectedCurrencyValue = this.accountModel.currencyId.toString();
      }
    })
  }

  getTurnoverModes() {
    this.lookupService.GetLookup(LookupApi.AccountTurnovers).subscribe(res => {
      this.turnovermodes = res;
      if (this.accountModel != undefined && this.accountModel.turnoverMode != undefined) {
        this.selectedTurnoverModeValue = this.accountModel.turnoverMode.toString();
      }
    })
  }

  handleFilter(value: any) {
    this.filteredCurrencies = this.currenciesRows.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  getProvince() {
    //this.lookupService.getModels(LookupApi.Provinces).subscribe(res => {
    //  this.filteredProcinces = this.provincesList = res;
    //})
  }

  onChangeProvince() {
    //get cities
  }

  onSave(e: any): void {
    e.preventDefault();



    var accountValue = this.accountDataForm.value.account;
    var featureValue = this.accountDataForm.value.features;

    this.accountModel.name = accountValue.name;
    this.accountModel.groupId = accountValue.groupId;
    this.accountModel.code = accountValue.code;
    this.accountModel.fullCode = accountValue.fullCode;
    this.accountModel.description = accountValue.description;
    this.accountModel.branchScope = accountValue.branchScope;
    this.accountModel.currencyId = featureValue.currencyId;
    this.accountModel.turnoverMode = featureValue.turnoverMode;
    this.accountModel.isActive = featureValue.isActive;
    this.accountModel.isCurrencyAdjustable = featureValue.isCurrencyAdjustable;

    if (this.isNew) {

    }
    else {

    }


    //if (this.editForm.valid) {
    if (this.accountModel.id > 0) {
      if (this.accountModel.level > 0)
        this.accountModel.groupId = undefined;
      //this.save.emit(model);
    }
    else {
      this.accountModel.branchId = this.BranchId;
      this.accountModel.fiscalPeriodId = this.FiscalPeriodId;
      this.accountModel.companyId = this.CompanyId;
      this.accountModel.parentId = this.parent ? this.parent.id : undefined;
      this.accountModel.level = this.level;
      if (this.accountModel.level > 0)
        this.accountModel.groupId = undefined;
      //this.save.emit(model);
    }
    //}

    debugger;


    var resultModel = new AccountFullDataInfo();

    resultModel.account = this.accountModel;
    resultModel.customerTaxInfo = null;

    if (!this.isNew && this.customerTaxModel) {
      var customerTaxInfo = this.accountDataForm.value.customerTax;
      customerTaxInfo.id = this.customerTaxModel.id;
      customerTaxInfo.accountId = this.accountModel.id;

      resultModel.customerTaxInfo = customerTaxInfo;
    }


    this.save.emit(resultModel);

  }

  onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }

  handleFilterProvince(value: any) {
    debugger;
    this.filteredProcinces = this.provincesList.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }
}
