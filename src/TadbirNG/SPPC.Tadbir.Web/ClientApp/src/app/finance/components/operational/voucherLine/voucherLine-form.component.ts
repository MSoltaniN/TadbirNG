import {
  Component,
  Input,
  Output,
  EventEmitter,
  Renderer2,
  OnInit,
  ElementRef,
  HostListener,
} from "@angular/core";
import { Validators, FormGroup, FormControl } from "@angular/forms";
import { TranslateService } from "@ngx-translate/core";
import { ToastrService } from "ngx-toastr";
import {
  LookupService,
  MetaDataService,
  BrowserStorageService,
  SessionKeys,
} from "@sppc/shared/services";
import { VoucherLine, CurrencyInfo } from "@sppc/finance/models";
import { Entities } from "@sppc/shared/enum/metadata";
import { DetailComponent, String } from "@sppc/shared/class";
import { ViewName } from "@sppc/shared/security";
import { LookupApi } from "@sppc/shared/services/api";
import { CurrencyService } from "@sppc/finance/service";
import { CurrencyApi } from "@sppc/finance/service/api";

interface Item {
  Key: string;
  Value: string;
}

@Component({
  selector: "voucherLine-form-component",
  styles: [
    `
      input[type="text"],
      textarea,
      .ddl-currency,
      .ddl-type {
        width: 100%;
      }
      ::ng-deep kendo-numerictextbox {
        width: 100% !important;
      }
      .dialog-body {
        width: 800px;
      }
      .dialog-body hr {
        border-top: dashed 1px #eee;
      }
      @media screen and (max-width: 800px) {
        .dialog-body {
          width: 99%;
          min-width: 250px;
        }
      }

      .voucher-mode-item {
        display: inline;
        margin: 0 10px;
      }
    `,
  ],
  templateUrl: "./voucherLine-form.component.html",
})
export class VoucherLineFormComponent
  extends DetailComponent
  implements OnInit
{
  currencyOption: boolean = true; //این فیلد برای تنظیمات تاثیر مبلغ ریالی و نرخ  و مبلغ ارز میباشد و باید از تنظیمات خوانده شود

  //TODO: create form with metadata
  public editForm1: FormGroup;

  currenciesRows: Array<CurrencyInfo>;
  articleTypeList: Array<Item> = [];
  sourceAppList: Array<Item> = [];

  selectedCurrencyValue: number;
  selectedArticleType: string = "0";
  creditDebiteMode: string = "1";
  selectedSourceApp: string = null;

  isDisplayCurrencyInfo: boolean = false;
  currencyRate: number | undefined;
  decimalCount: number = 0;
  errorMsg: string;
  isFullAccountInputFocused = false;
  isPayReciept = false;
  isBank: boolean = true;
  isActiveSourceAppDropDown = false;

  @Input() public isNew: boolean = false;

  @Input() public isNewBalance: boolean = false;
  @Input() public balance: number = 0;
  @Input() public model: VoucherLine | any;
  @Input() public set payReciept(value: boolean) {
    this.isPayReciept = false;
  }
  @Input() public isSourceApp = false;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<{ model: VoucherLine; isOpen: boolean }> =
    new EventEmitter();
  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public currencyService: CurrencyService,
    public lookupService: LookupService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public elem: ElementRef
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      Entities.VoucherLine,
      ViewName.VoucherLine,
      elem
    );
  }

  ngOnInit(): void {
    this.initFromGroup();
    this.editForm1.reset(this.model);

    this.getSourceApps();

    if (!this.isPayReciept) {
      this.getArticleType();
      this.getCurrencies();
    } else this.initPayReceiptData();

    if (this.isNewBalance)
      if (this.balance > 0) {
        this.editForm1.patchValue({ credit: Math.abs(this.balance) });
        this.creditDebiteMode = "2";
      } else if (this.balance < 0) {
        this.editForm1.patchValue({ debit: Math.abs(this.balance) });
        this.creditDebiteMode = "1";
      }

    if (!this.isNew && !this.isPayReciept) {
      if (this.model.credit > 0) this.creditDebiteMode = "2";
      else this.creditDebiteMode = "1";
    }
    if (!this.isNew && this.model.sourceAppId != null) {
      this.selectedSourceApp = this.model.sourceAppId.toString();
      this.isActiveSourceAppDropDown = true;
    }

    this.onChangeFullAccount();
  }

  initFromGroup() {
    if (this.isPayReciept) {
      this.editForm1 = new FormGroup({
        id: new FormControl(),
        amount: new FormControl(),
        sourceAppId: new FormControl(),
        bankOrderNo: new FormControl(),
        isBank: new FormControl(),
        remarks: new FormControl("", Validators.maxLength(512)),
        fullAccount: new FormGroup({
          account: new FormGroup({
            id: new FormControl("", Validators.required),
            name: new FormControl(),
            fullCode: new FormControl(),
          }),
          detailAccount: new FormGroup({
            id: new FormControl(),
            name: new FormControl(),
            fullCode: new FormControl(),
          }),
          costCenter: new FormGroup({
            id: new FormControl(),
            name: new FormControl(),
            fullCode: new FormControl(),
          }),
          project: new FormGroup({
            id: new FormControl(),
            name: new FormControl(),
            fullCode: new FormControl(),
          }),
        }),
      });
    } else {
      this.editForm1 = new FormGroup({
        id: new FormControl(),
        voucherId: new FormControl(),
        currencyId: new FormControl(),
        debit: new FormControl(),
        credit: new FormControl(),
        currencyValue: new FormControl(),
        typeId: new FormControl(),
        sourceAppId: new FormControl(),
        description: new FormControl("", Validators.maxLength(512)),
        fullAccount: new FormGroup({
          account: new FormGroup({
            id: new FormControl("", Validators.required),
            name: new FormControl(),
            fullCode: new FormControl(),
          }),
          detailAccount: new FormGroup({
            id: new FormControl(),
            name: new FormControl(),
            fullCode: new FormControl(),
          }),
          costCenter: new FormGroup({
            id: new FormControl(),
            name: new FormControl(),
            fullCode: new FormControl(),
          }),
          project: new FormGroup({
            id: new FormControl(),
            name: new FormControl(),
            fullCode: new FormControl(),
          }),
        }),
      });
    }
  }

  initPayReceiptData() {
    if (this.isSourceApp && !this.isNew) {
      this.selectedSourceApp = this.model.sourceAppId?.toString();
      this.isBank = this.model.isBank;
      if (this.model.sourceAppId) this.isActiveSourceAppDropDown = true;
    }
  }

  public onSave(isOpen: boolean): void {
    if (this.editForm1.valid) {
      var model = this.editForm1.value;

      if (!model.debit) model.debit = 0;
      if (!model.credit) model.credit = 0;
      if (model.sourceAppId == 0) model.sourceAppId = null;

      if (this.creditDebiteMode == "1") model.credit = 0;
      else model.debit = 0;
      this.save.emit({ model, isOpen });
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }

  getCurrencies() {
    this.lookupService.GetLookup(LookupApi.CurrenciesInfo).subscribe((res) => {
      this.currenciesRows = res;
      if (this.model != undefined && this.model.currencyId != undefined) {
        this.isDisplayCurrencyInfo = true;
        this.selectedCurrencyValue = this.model.currencyId;

        var cdValue =
          this.model.credit > 0 ? this.model.credit : this.model.debit;
        this.currencyRate =
          cdValue && this.model.currencyValue
            ? cdValue / this.model.currencyValue
            : undefined;

        var currency = this.currenciesRows.find(
          (f) => f.id == this.model.currencyId
        );
        this.decimalCount = currency ? currency.decimalCount : 0;
      }
    });
  }

  getArticleType() {
    this.lookupService
      .getModels(LookupApi.VoucherLineTypes)
      .subscribe((res) => {
        this.articleTypeList = res;
      });
  }

  getSourceApps() {
    let apiUrl = String.Format(LookupApi.SourceApps, this.creditDebiteMode);

    this.lookupService.getModels(apiUrl).subscribe((res) => {
      this.sourceAppList = res;
    });
  }

  hasAccountWithId(accountIdToFind: number) {
    let accountCollections: any = JSON.parse(
      this.bStorageService.getMetadata(SessionKeys.AccountColletion)
    );

    return accountCollections.some(
      (account) => account.id === accountIdToFind
    );
  }

  focusHandler(e: any) {
    let fullAccount = this.editForm1.value.fullAccount;
    this.isActiveSourceAppDropDown = this.hasAccountWithId(
      fullAccount.account.id
    );

    setTimeout(() => {
      let costField = document.querySelector(".cost-field input");
      (<HTMLElement>document.activeElement).blur();
      (<HTMLElement>costField).focus();
    }, 200);
    this.setFocus.emit();
  }

  onCurrencyInfoChange() {
    if (this.isDisplayCurrencyInfo == false) {
      this.editForm1.patchValue({
        currencyId: "",
        currencyValue: "",
      });
      this.currencyRate = 0;
      this.changeCurrencyValue();
      this.onChangeCurrencyRate();
    }
  }

  onChangeCurrency() {
    if (this.selectedCurrencyValue) {
      var selectedCurrency = this.currenciesRows.find(
        (f) => f.id == this.selectedCurrencyValue
      );
      this.decimalCount = selectedCurrency.decimalCount;
      this.currencyRate = selectedCurrency.lastRate;

      if (this.editForm1.value.currencyValue) {
        var cdValue = this.editForm1.value.currencyValue * this.currencyRate;

        if (this.creditDebiteMode == "1")
          this.editForm1.patchValue({ debit: cdValue });
        if (this.creditDebiteMode == "2")
          this.editForm1.patchValue({ credit: cdValue });
      }
    } else {
      this.decimalCount = 0;
      this.currencyRate = undefined;
    }

    if (this.selectedCurrencyValue == 0)
      this.editForm1.patchValue({
        currencyId: undefined,
        currencyValue: undefined,
      });
  }

  changeCurrencyValue() {
    var cdValue = undefined;
    if (this.creditDebiteMode == "1") cdValue = this.editForm1.value.debit;
    if (this.creditDebiteMode == "2") cdValue = this.editForm1.value.credit;

    var currencyValue = this.editForm1.value.currencyValue;

    if (this.currencyOption) {
      //#region آپشن فعال است و با تغییر مبلغ ارزی، مبلغ ریالی تغییر میکند

      if (this.currencyRate) {
        cdValue = currencyValue ? this.currencyRate * currencyValue : undefined;
      }

      if (this.creditDebiteMode == "1")
        this.editForm1.patchValue({ debit: cdValue });

      if (this.creditDebiteMode == "2")
        this.editForm1.patchValue({ credit: cdValue });

      //#endregion
    } else {
      //#region آپشن غیرفعال است و با تغییر مبلغ ارزی، نرخ ارز تغییر میکند
      if (this.selectedCurrencyValue) {
        if (cdValue && currencyValue) {
          this.currencyRate = cdValue / currencyValue;
        } else {
          this.currencyRate = this.currenciesRows.find(
            (f) => f.id == this.selectedCurrencyValue
          ).lastRate;
        }
      }
      //endregion
    }
  }

  @HostListener("document:keydown.enter")
  onEnterKey() {
    if (!this.isFullAccountInputFocused) this.onSave(true);
  }

  @HostListener("document:keydown.control.Alt.s")
  changeCreditDebiteMode() {
    this.creditDebiteMode = this.creditDebiteMode == "1" ? "2" : "1";
  }

  onCreditDebiteModeChange() {
    if (this.creditDebiteMode == "1") {
      if (this.editForm1.value.credit != "")
        this.editForm1.patchValue({ debit: this.editForm1.value.credit });
    } else {
      if (this.editForm1.value.debit != "")
        this.editForm1.patchValue({ credit: this.editForm1.value.debit });
    }
  }

  onDebitChange() {
    var currValue = this.editForm1.value.currencyValue;
    var debit = this.editForm1.value.debit;
    if (this.currencyOption) {
      //#region آپشن فعال است و با تغییر مبلغ ریالی، مبلغ ارزی تغییر میکند
      if (this.currencyRate) {
        currValue = debit ? debit / this.currencyRate : undefined;
      }

      this.editForm1.patchValue({ currencyValue: currValue });
      //#endregion
    } else {
      //#region آپشن غیرفعال است و با تغییر مبلغ ریالی، نرخ ارز تغییر میکند
      if (this.selectedCurrencyValue) {
        if (debit && currValue) {
          this.currencyRate = debit / currValue;
        } else {
          this.currencyRate = this.currenciesRows.find(
            (f) => f.id == this.selectedCurrencyValue
          ).lastRate;
        }
      }
      //endregion
    }
  }

  onCreditChange() {
    var currValue = this.editForm1.value.currencyValue;
    var credit = this.editForm1.value.credit;
    if (this.currencyOption) {
      //#region آپشن فعال است و با تغییر مبلغ ریالی، مبلغ ارزی تغییر میکند
      if (this.currencyRate) {
        currValue = credit ? credit / this.currencyRate : undefined;
      }

      this.editForm1.patchValue({ currencyValue: currValue });
      //#endregion
    } else {
      //#region آپشن غیرفعال است و با تغییر مبلغ ریالی، نرخ ارز تغییر میکند
      if (this.selectedCurrencyValue) {
        if (credit && currValue) {
          this.currencyRate = credit / currValue;
        } else {
          this.currencyRate = this.currenciesRows.find(
            (f) => f.id == this.selectedCurrencyValue
          ).lastRate;
        }
      }
      //endregion
    }
  }

  onChangeCurrencyRate() {
    var cdValue = undefined;
    var currencyValue = this.editForm1.value.currencyValue;
    if (this.currencyRate && currencyValue) {
      cdValue = this.currencyRate * currencyValue;
    } else {
      cdValue = undefined;
    }

    if (this.creditDebiteMode == "1")
      this.editForm1.patchValue({ debit: cdValue });

    if (this.creditDebiteMode == "2")
      this.editForm1.patchValue({ credit: cdValue });
  }

  onChangeFullAccount(): void {
    this.editForm1.get("fullAccount").valueChanges.subscribe((val) => {
      if (!this.selectedCurrencyValue) {
        this.currencyService
          .getModels(
            String.Format(
              CurrencyApi.DefaultCurrencyByFullAccount,
              val.account.id,
              val.detailAccount.id
            )
          )
          .subscribe((res) => {
            if (res) {
              this.selectedCurrencyValue = res.id;
              var currency = this.currenciesRows.find((f) => f.id == res.id);
              this.currencyRate = currency.lastRate;
              this.isDisplayCurrencyInfo = true;
            }
          });
      }
    });
  }

  onBankOrFundChange($e) {}

  fullAccountFocuse(value: boolean) {
    this.isFullAccountInputFocused = value;
  }
}
