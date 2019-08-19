import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ViewChild, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { String, DetailComponent } from '@sppc/shared/class';
import { Currency } from '@sppc/finance/models';
import { CurrencyService } from '@sppc/finance/service';
import { CurrencyApi } from '@sppc/finance/service/api';
import { BrowserStorageService, MetaDataService, LookupService } from '@sppc/shared/services';
import { Entities, MessageType } from '@sppc/env/environment';
import { ViewName } from '@sppc/shared/security';
import { HttpEventType, HttpClient } from '@angular/common/http';


interface Item {
  key: string,
  value: string
}


@Component({
  selector: 'currency-form-component',
  styles: [`
    input[type=text],textarea,.ddl-currency { width: 100%; }

.dialog-body{ width: 800px } .dialog-body hr{ border-top: dashed 1px #eee; }
@media screen and (max-width:800px) {
  .dialog-body{
    width: 90%;
    min-width: 250px;
  }
}
input[type="file"] {
    display: none;
}
.custom-file-upload {
    border: 1px solid #ccc;
    display: inline-block;
    padding: 5px 3px 2px;
    cursor: pointer;
    position: absolute;
    top: 25px;
    width: 30px;
    height: 30px;
}
.related-currency { width: calc(100% - 40px) !important; margin-left: 5px; }
.upload-msg { font-weight:bold;color:green; font-size: 13px; display: block;}
`  ],
  templateUrl: './currency-form.component.html'
})

export class CurrencyFormComponent extends DetailComponent implements OnInit {


  currencyNameLookup: Array<Item> = [];
  currencyNameData: Array<Item> = [];
  selectedCurrencyName: string;

  currencyId: number;

  progress: number = 0;
  message: string;
  @ViewChild('myInput') myInputVariable: ElementRef;

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
    this.save.emit(this.editForm.value);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }
  //Events

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public lookupService: LookupService, public currencyService: CurrencyService, public renderer: Renderer2, public metadata: MetaDataService,
    private http: HttpClient) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.VoucherLine, ViewName.Currency);

  }

  ngOnInit(): void {
    this.currencyId = this.model.id;
    this.editForm.reset();
    this.getCurrencyNames();
  }

  getCurrencyNames() {
    this.currencyService.getModels(CurrencyApi.CurrencyNamesLookup).subscribe(res => {
      this.currencyNameLookup = res;
      this.currencyNameData = res;

      if (!this.isNew) {
        var currencyItem = this.currencyNameData.find(f => f.value == this.model.name);

        this.selectedCurrencyName = currencyItem ? currencyItem.key : undefined;

        this.editForm.reset(this.model);
      }
    })
  }

  onChangeCurrency(item: any) {
    this.editForm.patchValue({ name: item });
    if (item)
      this.currencyService.getModels(String.Format(CurrencyApi.CurrencyInfoByName, item)).subscribe(res => {
        this.editForm.reset(res);
      }, error => {
        if (error.status == 404)
          this.showMessage(this.getText('App.RecordNotFound'), MessageType.Warning);
      })
  }

  handleFilter(value: any) {
    this.currencyNameData = this.currencyNameLookup.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  onFileChange(event: any) {
    this.message = undefined;

    if (event.target.files && event.target.files.length > 0) {
      let file = event.target.files[0];
      var fileExtension = file.name.split('.').pop();
      var accessExtensions = ["accda", "accdb", "accde", "accdr", "accdt", "mdb", "mde", "mdf", "mda"];
      if (accessExtensions.filter(f => f == fileExtension.toLowerCase()).length > 0) {

        this.currencyService.postFile(file).subscribe(res => {
          this.myInputVariable.nativeElement.value = "";

          if (res.type === HttpEventType.UploadProgress)
            this.progress = Math.round(100 * res.loaded / res.total);
          else
            if (res.type === HttpEventType.Response) {
              this.message = res.body.toString();
              //get lookup data              
            }
        })
      }
      else {
        this.showMessage("فرمت فایل انتخابی صحیح نیست", MessageType.Warning);
      }

    }
  }
}
