import { Component, Input, Output, EventEmitter, Renderer2, ChangeDetectionStrategy, Host } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { VoucherService, VoucherInfo, VoucherLineService, FiscalPeriodService } from '../../service/index';
import { Voucher } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities, Metadatas, MessageType } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailComponent } from '../../class/detail.component';
import { DocumentStatusValue } from '../../enum/documentStatusValue';
import { String } from '../../class/source';
import { VoucherApi } from '../../service/api/index';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  Key: string,
  Value: string
}


@Component({
  //changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'voucher-form-component',
  styles: [
    "input[type=text],textarea { width: 100%; } /deep/ .new-dialog > .k-dialog {width: 450px !important; min-width: 250px !important;}",
    "/deep/ .edit-dialog > .k-dialog {width: 100% !important; min-width: 250px !important; height:100%}",
    "/deep/ .edit-dialog .k-window-titlebar{ padding: 5px 16px !important;}",
    "/deep/ .edit-dialog .edit-form-body { background: #f6f6f6; border: solid 1px #989898; border-radius: 4px; padding-top: 10px;}"
  ],
  templateUrl: './voucher-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class VoucherFormComponent extends DetailComponent {

  //create properties
  public voucher_Id: number;
  editModel: Voucher;

  active: boolean = false;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;

  @Input() public set model(voucher: Voucher) {
    this.editModel = voucher;
    this.editForm.reset(voucher);

    this.active = voucher !== undefined || this.isNew;

    if (voucher != undefined) {
      this.voucher_Id = voucher.id;
    }

  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Voucher> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      if (this.editModel) {
        let model: Voucher = this.editForm.value;
        model.branchId = this.editModel.branchId;
        model.fiscalPeriodId = this.editModel.fiscalPeriodId;
        this.save.emit(model);
      }
      else
        this.save.emit(this.editForm.value);
      this.active = true;
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.isNew = false;
    this.active = false;
    this.cancel.emit();
  }

  public onDeleteData() {
    alert("Data deleted.");
  }
  //Events

  constructor(private voucherService: VoucherService, private fiscalPeriodService: FiscalPeriodService,
    public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.Voucher, Metadatas.Voucher);
  }


  public checkHandler(voucherId: number, statusId: DocumentStatusValue) {
    if (statusId == DocumentStatusValue.Draft) {
      //check
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.CheckVoucher, voucherId)).subscribe(res => {

        this.editModel.statusId = DocumentStatusValue.NormalCheck;
        this.showMessage(this.updateMsg, MessageType.Succes);

      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));

    }
    else {
      //uncheck
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.UncheckVoucher, voucherId)).subscribe(res => {

        this.editModel.statusId = DocumentStatusValue.Draft;
        this.showMessage(this.updateMsg, MessageType.Succes);

      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }


  }

}
