import { DetailComponent, BaseComponent } from "@sppc/shared/class";
import { OnInit, Renderer2, Component, ViewChild, Output, EventEmitter } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { BrowserStorageService, MetaDataService } from "@sppc/shared/services";
import { SettingService } from "@sppc/config/service";
import { FormLabelFullConfig, FormLabelConfig, FormLabelConfigEntity } from "@sppc/config/models";
import { RTL } from "@progress/kendo-angular-l10n";
import { Layout } from "@sppc/shared/enum/metadata";
import { Item } from "@sppc/shared/models";
import { GridComponent } from "@progress/kendo-angular-grid";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'sppc-profitlost-label',
  templateUrl: './profitLost.labels.components.html',
  styles: [`
.labels { width:100%!important;height:30px;margin-bottom: 10px; }
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class ProfitLostLabelsComponent extends DetailComponent implements OnInit {
   
  dafaultLabels: Array<Item> = [];
  currentLabels: Array<Item> = [];
  response: FormLabelFullConfig;
  formId: number;

  @Output() save: EventEmitter<Array<Item>> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  constructor(public bStorageService: BrowserStorageService,
    public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService:SettingService) {

    super(toastrService, translate, bStorageService, renderer, metadata,'',0);
  }

  ngOnInit(): void {
    this.settingService.getFormLabelSettingsAsync(this.formId).subscribe((res: FormLabelFullConfig) => {      
      this.response = res;
      Object.keys(res.current.labelMap).forEach((it) => {
        this.currentLabels.push({ key: it, value: res.current.labelMap[it] });
      });

      Object.keys(res.default.labelMap).forEach((it) => {
        this.dafaultLabels.push({ key: it, value: res.default.labelMap[it] });
      });      
    });
  }



  public onSave(e: any): void {
    this.currentLabels.forEach((it) => {      
      this.response.current.labelMap[it.key] = it.value;
    });

    this.settingService.putModifiedFormLabelSettingsAsync(this.formId, this.response.current).subscribe(()=>{
      this.showMessage(this.getText('ProfitLoss.LabelsChangedSuccessfully'));
      this.save.emit();
    }, (error => {
        this.showMessage(this.getText('ProfitLoss.LabelsChangedError'));
    }));    
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }

  onResetToDefault() {    
    this.currentLabels = [];

    Object.keys(this.dafaultLabels).forEach((it) => {
      this.currentLabels.push({ key: this.dafaultLabels[it].key, value: this.dafaultLabels[it].value });
    });    
  }
}
