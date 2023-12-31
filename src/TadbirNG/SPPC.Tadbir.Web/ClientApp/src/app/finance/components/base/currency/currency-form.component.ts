import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Renderer2,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { Currency, TaxCurrency } from "@sppc/finance/models";
import { CurrencyEntity, CurrencyService } from "@sppc/finance/service";
import { CurrencyApi } from "@sppc/finance/service/api";
import { DetailComponent, String } from "@sppc/shared/class";
import { Entities, MessageType } from "@sppc/shared/enum/metadata";
import { ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  LookupService,
  MetaDataService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";

interface Item {
  key: string;
  value: string;
}

@Component({
  selector: "currency-form-component",
  styles: [
    `
      input[type="text"],
      textarea,
      .ddl-currency {
        width: 100%;
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
    `,
  ],
  templateUrl: "./currency-form.component.html",
})
export class CurrencyFormComponent extends DetailComponent implements OnInit {
  currencyNameLookup: Array<Item> = [];
  currencyNameData: Array<Item> = [];
  selectedCurrencyName: string;
  taxCurrencyList: Array<TaxCurrency> = [];
  taxCurrencyData: Array<TaxCurrency> = [];
  currencyId: number;
  minorUnitKey: string;

  editConfirm: boolean = false;
  selectedCurrencyItem: any;

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;
  @Input() public model: Currency;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Currency> = new EventEmitter();
  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    this.editForm.patchValue({ id: this.currencyId ? this.currencyId : 0 });
    var model = new CurrencyEntity();
    model = this.editForm.value;
    model.minorUnit = this.minorUnitKey;
    model.minorUnitKey = this.minorUnitKey;
    this.save.emit(model);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }
  //Events

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public lookupService: LookupService,
    public currencyService: CurrencyService,
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
      ViewName.Currency,
      elem
    );
  }

  ngOnInit(): void {
    this.currencyId = this.model.id;
    this.editForm.reset();
    this.getCurrencyNames();

    this.getTaxCurrencyList();
  }

  getCurrencyNames() {
    this.currencyService
      .getModels(CurrencyApi.CurrencyNamesLookup)
      .subscribe((res) => {
        this.currencyNameLookup = res;
        this.currencyNameData = res;

        if (!this.isNew) {
          var currencyItem = this.currencyNameData.find(
            (f) => f.value == this.model.name
          );
          this.editForm.reset(this.model);
          this.selectedCurrencyName = currencyItem
            ? currencyItem.key
            : undefined;
          this.minorUnitKey = this.model.minorUnitKey;
        }
      });
  }

  onChangeCurrency(item: any) {
    if (item) {
      this.selectedCurrencyItem = item;

      if (this.isNew) {
        this.getCurrencyInfo(true);
      } else {
        this.currencyService
          .getModels(
            String.Format(CurrencyApi.CurrencyHasRates, this.currencyId)
          )
          .subscribe((res) => {
            this.editConfirm = res;
            if (!this.editConfirm) {
              this.getCurrencyInfo(true);
            }
          });
      }
    }
  }

  getCurrencyInfo(confirm: boolean) {
    if (confirm) {
      this.currencyService
        .getModels(
          String.Format(
            CurrencyApi.CurrencyInfoByName,
            this.selectedCurrencyItem
          )
        )
        .subscribe(
          (res) => {
            var result = res;
            result.taxCode = undefined;
            this.editForm.reset(result);
            this.editForm.patchValue({
              name: this.selectedCurrencyItem,
              taxCode: this.model.taxCode,
            });
            this.minorUnitKey = res.minorUnitKey;
          },
          (error) => {
            if (error.status == 404)
              this.showMessage(
                this.getText("App.RecordNotFound"),
                MessageType.Warning
              );
          }
        );
    } else {
      var currencyItem = this.currencyNameData.find(
        (f) => f.value == this.model.name
      );
      this.selectedCurrencyName = currencyItem ? currencyItem.key : undefined;
    }

    this.editConfirm = false;
  }

  handleFilter(value: any) {
    this.currencyNameData = this.currencyNameLookup.filter(
      (s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }

  handleTaxCodeFilter(value: any) {
    this.taxCurrencyList = this.taxCurrencyData.filter(
      (s) => s.name.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }

  getTaxCurrencyList() {
    this.currencyService
      .getModels(CurrencyApi.TaxCurrencies)
      .subscribe((res) => {
        if (res && res.length > 0) {
          this.taxCurrencyData = res;
          this.taxCurrencyList = res;
        }
      });
  }
}
