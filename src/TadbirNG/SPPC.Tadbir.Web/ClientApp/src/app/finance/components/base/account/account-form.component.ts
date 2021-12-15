import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ElementRef, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { String, DefaultComponent, DetailComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { AccountService, AccountFullDataInfo, AccountInfo, CustomerTaxInfoModel, AccountOwnerInfo } from '@sppc/finance/service';
import { Account, AccountOwner, AccountHolder } from '@sppc/finance/models';
import { MetaDataService, BrowserStorageService, LookupService } from '@sppc/shared/services';
import { BranchApi } from '@sppc/organization/service/api';
import { ViewName } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';
import { FormGroup, FormControl, Validators, FormArray, FormBuilder } from '@angular/forms';
import { AccountFullData } from '@sppc/finance/models/accountFullData';
import { CustomerTaxInfo } from '@sppc/finance/models/customerTaxInfo';
import { HttpEventType } from '@angular/common/http';
import { SppcNationalCode } from '@sppc/shared/directive/Validator/Sppc-nationalCodeValidator';




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
    width: 99%;
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
input[type="file"] {
    display: none;
}
.custom-file-upload {
    padding: 5px 10px;
    cursor: pointer;
    background-color: #337ab7;
    color: #fff;
    border-radius: 3px;
    margin-right: 10px;
}
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

  citiesList: Array<Item> = [];
  filteredCities: Array<Item> = [];


  isDisableCustomerTaxTab: boolean = false;
  isDisableAccountOwnerTab: boolean = false;

  accountModel: Account;
  customerTaxModel: CustomerTaxInfo;
  accountOwnerModel: AccountOwner;
  groupId: number;

  progress: number = 0;
  @ViewChild('myInput') myInputVariable: ElementRef;

  @Input() public parent: Account;
  @Input() public model: AccountFullData;
  @Input() public isNew: boolean = false;

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

  accountTypes: Array<Item> = [
    { key: "0", value: "جاری" },
    { key: "1", value: "پس انداز" }
  ]

  accountForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(512)]),
    groupId: new FormControl('', Validators.required),
    code: new FormControl('', [Validators.required, Validators.maxLength(16)]),
    fullCode: new FormControl('', [Validators.required, Validators.maxLength(256)]),
    description: new FormControl('', Validators.maxLength(256)),
    branchScope: new FormControl('', Validators.required),
  })
  featuresForm = new FormGroup({
    currencyId: new FormControl(''),
    turnoverMode: new FormControl(''),
    isActive: new FormControl(''),
    isCurrencyAdjustable: new FormControl(''),
  })
  customerTaxForm = new FormGroup({
    id: new FormControl(),
    accountId: new FormControl(),
    customerFirstName: new FormControl('', Validators.maxLength(64)),
    customerName: new FormControl('', [Validators.required, Validators.maxLength(64)]),
    personType: new FormControl('', Validators.required),
    buyerType: new FormControl('', Validators.required),
    economicCode: new FormControl('', [Validators.maxLength(12), Validators.minLength(12)]),
    address: new FormControl('', [Validators.required, Validators.maxLength(256)]),
    nationalCode: new FormControl('', [Validators.minLength(11),Validators.maxLength(11), SppcNationalCode.validNationalCode]),
    perCityCode: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.minLength(3)]),
    phoneNo: new FormControl('', [Validators.required, Validators.maxLength(64), Validators.pattern("^[0-9-]+$")]),
    mobileNo: new FormControl('', [Validators.required, Validators.maxLength(64), Validators.pattern("^[0-9-]+$")]),
    postalCode: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.pattern("^[0-9]+$")]),
    provinceCode: new FormControl('', [Validators.required, Validators.maxLength(4)]),
    cityCode: new FormControl('', [Validators.required, Validators.maxLength(16)]),
    description: new FormControl('', Validators.maxLength(1024)),
  })
  ownerForm = new FormGroup({
    id: new FormControl(),
    accountId: new FormControl(),
    bankName: new FormControl('', [Validators.required, Validators.maxLength(64)]),
    accountType: new FormControl('', Validators.required),
    bankBranchName: new FormControl('', [Validators.required, Validators.maxLength(64)]),
    branchIndex: new FormControl('', [Validators.required, Validators.maxLength(64)]),
    accountNumber: new FormControl('', [Validators.required, Validators.maxLength(32), Validators.pattern("^[0-9-.]+$")]),
    cardNumber: new FormControl('', [Validators.maxLength(32), Validators.pattern("^[0-9-]+$")]),
    shabaNumber: new FormControl('', Validators.maxLength(32)),
    description: new FormControl('', Validators.maxLength(512)),
    accountHolders: this.formBuilder.array([])
  })
  accountHolders: FormArray;

  constructor(private accountService: AccountService, public toastrService: ToastrService, public translate: TranslateService, public lookupService: LookupService,
    public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService, public formBuilder: FormBuilder,public elem:ElementRef) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Account, ViewName.Account,elem);
  }


  ngOnInit(): void {
    if (this.model) {      
      this.accountModel = this.model.account;
      this.customerTaxModel = this.model.customerTaxInfo;
      this.accountOwnerModel = this.model.accountOwner;
      this.groupId = (<any>this.model).groupId;

      if (this.isNew || this.model.customerTaxInfo == null) {
        this.isDisableCustomerTaxTab = true;
      }

      if (this.isNew || this.model.accountOwner == null) {
        this.isDisableAccountOwnerTab = true;
      }
    }
    else {
      this.accountModel = new AccountInfo();
      this.customerTaxModel = new CustomerTaxInfoModel();
      this.accountOwnerModel = new AccountOwnerInfo();
    }

    this.viewId = ViewName.Account;

    this.getAccountGroups();
    this.getBranchName();
    this.getCurrencies();
    this.getTurnoverModes();
    this.getProvince()

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
      this.accountForm.reset();
      this.featuresForm.reset();
      this.customerTaxForm.reset();
      this.ownerForm.reset();
    }
    else {
      this.accountForm.patchValue({
        name: this.accountModel.name,
        groupId: this.accountModel.groupId,
        code: this.accountModel.code,
        fullCode: this.accountModel.fullCode,
        description: this.accountModel.description,
        branchScope: this.accountModel.branchScope,
      })

      this.featuresForm.patchValue({
        currencyId: this.accountModel.currencyId,
        turnoverMode: this.accountModel.turnoverMode,
        isActive: this.accountModel.isActive,
        isCurrencyAdjustable: this.accountModel.isCurrencyAdjustable,
      })

      if (this.customerTaxModel) {
        this.customerTaxForm.reset(this.customerTaxModel);

        this.customerTaxForm.patchValue({
          personType: this.customerTaxModel.id > 0 ? this.customerTaxModel.personType.toString() : "1",
          buyerType: this.customerTaxModel.id > 0 ? this.customerTaxModel.buyerType.toString() : "1"
        })

        this.onChangePersonType(this.customerTaxModel.id > 0 ? this.customerTaxModel.personType.toString() : "1");
      }

      if (this.accountOwnerModel) {

        this.ownerForm.reset(this.accountOwnerModel)

        this.ownerForm.patchValue({
          accountType: this.accountOwnerModel.id > 0 ? this.accountOwnerModel.accountType.toString() : "0"
        })

        this.initialAccountHolderData();
      }
    }

    this.onChangeForm();
  }

  isChangeFormValue: boolean = false;

  onChangeForm() {
    this.customerTaxForm.valueChanges.subscribe(val => {
      this.isChangeFormValue = true;
    })

    this.ownerForm.valueChanges.subscribe(val => {
      this.isChangeFormValue = true;
    })
  }

  initialAccountHolderData() {
    if (this.accountOwnerModel.id > 0) {
      this.accountOwnerModel.accountHolders.forEach(item => {
        this.onAddAccountHolder(item);
      })
    }
    else {
      this.onAddAccountHolder();
    }
  }

  getAccountGroups() {
    this.lookupService.GetAccountGroupsLookup().subscribe(res => {
      this.accGroupList = res;

      if (this.accountModel && this.accountModel.groupId) {
        this.accGroupSelected = this.accountModel.groupId.toString();
      }
      else if (this.parent) {
          this.accGroupSelected = this.parent.groupId.toString();
      }
      else if (this.groupId) {
        this.accGroupSelected = this.groupId.toString();
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
    this.lookupService.getModels(LookupApi.Provinces).subscribe(res => {
      if (res.length) {
        this.filteredProcinces = this.provincesList = res;
      }

      if (!this.isNew && this.customerTaxModel && this.customerTaxModel.id > 0) {
        this.onChangeProvince(this.customerTaxModel.provinceCode, true);
      }
    })
  }

  onChangePersonType(personTypeKey: string) {
    if (personTypeKey == "2") {
      //شحص حقوقی
      this.customerTaxForm.patchValue({ customerFirstName: undefined });

      this.customerTaxForm.controls['nationalCode'].clearValidators();
      this.customerTaxForm.controls['nationalCode'].setValidators([Validators.minLength(11),Validators.maxLength(11)]);
      this.customerTaxForm.controls['nationalCode'].updateValueAndValidity();

      this.customerTaxForm.controls['economicCode'].clearValidators();
      this.customerTaxForm.controls['economicCode'].setValidators([Validators.minLength(12),Validators.maxLength(12)]);
      this.customerTaxForm.controls['economicCode'].updateValueAndValidity();
    }
    else {
      //شخص حقیقی
      this.customerTaxForm.controls['nationalCode'].setValidators([SppcNationalCode.validNationalCode]);
      this.customerTaxForm.controls['nationalCode'].updateValueAndValidity();
    }
  }

  onChangeProvince(provinceCode: string, initValue?: boolean) {
    this.filteredCities = this.citiesList = [];
    if (!initValue) {
      this.customerTaxForm.patchValue({ cityCode: undefined });
    }
    this.lookupService.getModels(String.Format(LookupApi.Cities, provinceCode)).subscribe(res => {
      this.filteredCities = this.citiesList = res;
    })
  }

  onAddAccountHolder(dataItem?: AccountHolder) {
    this.accountHolders = this.ownerForm.get('accountHolders') as FormArray;

    let newItem: FormGroup = this.formBuilder.group({
      id: [dataItem ? dataItem.id : 0, [Validators.required]],
      accountOwnerId: [this.accountOwnerModel ? this.accountOwnerModel.id : 0, [Validators.required]],
      firstName: [dataItem ? dataItem.firstName : '', [Validators.required, Validators.maxLength(64)]],
      lastName: [dataItem ? dataItem.lastName : '', [Validators.required, Validators.maxLength(64)]],
      hasSignature: [dataItem ? dataItem.hasSignature : true, [Validators.required]]
    });

    this.accountHolders.push(newItem);
  }

  onRemoveAccountHolder(index: number) {
    this.accountHolders.removeAt(index);
  }

  onSave(e: any): void {
    e.preventDefault();

    console.log('onSave');

    var accountValue = this.accountForm.value;
    var featureValue = this.featuresForm.value;

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

    if (this.accountModel.id <= 0) {
      this.accountModel.branchId = this.BranchId;
      this.accountModel.fiscalPeriodId = this.FiscalPeriodId;
      this.accountModel.companyId = this.CompanyId;
      this.accountModel.parentId = this.parent ? this.parent.id : undefined;
      this.accountModel.level = this.level;      
    }

    if (this.accountModel.level > 0)
      this.accountModel.groupId = undefined;

    var resultModel = new AccountFullDataInfo();

    resultModel.account = this.accountModel;
    resultModel.customerTaxInfo = null;
    resultModel.accountOwner = null;

    if (!this.isNew && this.customerTaxModel && this.customerTaxForm.valid) {
      var customerTaxInfo = this.customerTaxForm.value;
      customerTaxInfo.id = this.customerTaxModel.id;
      customerTaxInfo.accountId = this.accountModel.id;

      resultModel.customerTaxInfo = customerTaxInfo;
    }

    if (!this.isNew && this.accountOwnerModel && this.ownerForm.valid) {
      var accountOwner = this.ownerForm.value;
      accountOwner.id = this.accountOwnerModel.id;
      accountOwner.accountId = this.accountModel.id;

      resultModel.accountOwner = accountOwner;
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
    this.filteredProcinces = this.provincesList.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  handleFilterCity(value: any) {
    this.filteredCities = this.citiesList.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  isDisabledForm = () => {
    if (this.accountForm.valid && this.featuresForm.valid) {
      if (!this.isDisableCustomerTaxTab) {
        if (!this.isChangeFormValue) {
          return false;
        }
        else {
          if (this.customerTaxForm.valid) {
            return false;
          }
          else
            return true;
        }
      }

      if (!this.isDisableAccountOwnerTab) {
        if (!this.isChangeFormValue) {
          return false;
        }
        else {
          if (this.ownerForm.valid) {
            return false;
          }
          else
            return true;
        }
      }

      return false;
    }
    else
      return true;
  }  

  //onFileChange(event: any) {
  //  if (event.target.files && event.target.files.length > 0) {
  //    let file = event.target.files[0];
  //    var fileExtension = file.name.split('.').pop();
  //    var accessExtensions = ["accda", "accdb", "accde", "accdr", "accdt", "mdb", "mde", "mdf", "mda"];
  //    if (accessExtensions.filter(f => f == fileExtension.toLowerCase()).length > 0) {

  //      this.accountService.postFile(file).subscribe(res => {
  //        this.myInputVariable.nativeElement.value = "";

  //        if (res.type === HttpEventType.UploadProgress)
  //          this.progress = Math.round(100 * res.loaded / res.total);
  //        else
  //          if (res.type === HttpEventType.Response) {
  //            this.showMessage(this.getText("Messages.UploadSuccessful"), MessageType.Succes);
  //            this.getProvince();
  //          }
  //      })
  //    }
  //    else {
  //      this.showMessage(this.getText("Messages.IncorrectFileFormat"), MessageType.Warning);
  //    }

  //  }
  //}
}
