import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2 } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { TranslateService } from "@ngx-translate/core";
import { SettingBriefInfo, SettingService } from "@sppc/config/service";
import { DetailComponent } from "@sppc/shared/class";
import { Entities } from "@sppc/shared/enum/metadata";
import { BrowserStorageService, ErrorHandlingService, MetaDataService } from "@sppc/shared/services";
import { PayReceiptConfig } from "@sppc/treasury/models/payReceive";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "payReceive-settings",
  templateUrl: "./payReceive-settings.component.html",
  styleUrls: ["./payReceive-settings.component.css"],
})
export class PayReceiveSettingsComponent extends DetailComponent
  implements OnInit {

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    public elem: ElementRef,
    public errorHandlingService: ErrorHandlingService
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      Entities.Setting,
      undefined,
      elem
    );
  }

  config: SettingBriefInfo;

  @Input() set model(model){
    this.config = model;
    setTimeout(() => {
      this.setFormValue(model.values);
    }, 1);
  };

  @Output() updateList: EventEmitter<any> = new EventEmitter();

  editForm1: FormGroup = new FormGroup({
    registerFlowConfig: new FormGroup({
      confirmAfterSave: new FormControl(),
      approveAfterConfirm: new FormControl(),
      registerAfterApprove: new FormControl(),
    }),
    registerConfig: new FormGroup({
      registerWithLastValidVoucher: new FormControl(),
      registerWithNewCreatedVoucher: new FormControl(),
      checkedVoucher: new FormControl()
    })
  });

  ngOnInit(): void {
  }

  setValidValues(key) {
    switch (key) {
      case "registerWithLastValidVoucher":
        if (this.getControlValue(key))
          this.editForm1.get("registerConfig").get('registerWithNewCreatedVoucher').disable();
        else
          this.editForm1.get("registerConfig").get('registerWithNewCreatedVoucher').enable();
        break;
    
      case "registerWithNewCreatedVoucher":
        if (this.getControlValue(key)){
          this.editForm1.get("registerConfig").get('registerWithLastValidVoucher').disable();
          this.editForm1.get("registerConfig").get('checkedVoucher').enable();
        }
        else {
          this.editForm1.get("registerConfig").get('registerWithLastValidVoucher').enable();
          this.editForm1.get("registerConfig").get('checkedVoucher').disable();
        }
        break;

      default:
        break;
    }
  }

  saveConfig() {
    let value = this.editForm1.value;
    this.config.values = value;

    this.updateList.emit(this.config);
  }

  onDefaultSettings() {
    this.setFormValue(this.config.values as PayReceiptConfig);
  }

  setFormValue(values: PayReceiptConfig) {
    this.editForm1.reset(values);

    this.setValidValues("registerWithLastValidVoucher");
    this.setValidValues("registerWithNewCreatedVoucher");
  }
  
  getControlValue(key) {
    return this.editForm1.get("registerConfig").get(key).value;
  }
}