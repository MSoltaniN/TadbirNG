import { Component, Input, Output, EventEmitter, Renderer2, ChangeDetectionStrategy, Host, OnInit } from '@angular/core';
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
  styles: [`
    input[type=text],textarea { width: 100%; } /deep/ .new-dialog .k-dialog {width: 450px !important; min-width: 250px !important;}
    /deep/ .edit-dialog .k-dialog {width: 100% !important; min-width: 250px !important; height:100%}
    /deep/ .edit-dialog .k-window-titlebar{ padding: 5px 16px !important;}
    /deep/ .edit-dialog .edit-form-body { background: #f6f6f6; border: solid 1px #989898; border-radius: 4px;}
    .form-toolbar{border-bottom: solid 1px #e3e3e3; margin-bottom: 10px; padding: 10px;} .form-toolbar button{padding: 5px 6px;}
    /deep/ .voucher-dialog .k-window .k-overlay { opacity: .6 !important; }
   /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}"
  `],
  templateUrl: './voucher-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class VoucherFormComponent extends DetailComponent implements OnInit {

  //create properties
  public voucher_Id: number;
  documentStatus: number;

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string;
  @Input() public editModel: Voucher;


  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Voucher> = new EventEmitter();
  @Output() changeMode: EventEmitter<boolean> = new EventEmitter();
  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      let model: Voucher = this.editForm.value;
      if (this.editModel && this.editModel.id > 0) {
        model.branchId = this.editModel.branchId;
        model.fiscalPeriodId = this.editModel.fiscalPeriodId;
        model.statusId = this.editModel.statusId;        
      }
      else {
        model.branchId = this.BranchId;
        model.fiscalPeriodId = this.FiscalPeriodId;        
      }
      this.save.emit(model);      
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.isNew = false;
    this.cancel.emit();
  }

  public onDeleteData() {
    alert("Data deleted.");
  }


  escPress() {
    this.closeForm();
  }
  //Events

  constructor(private voucherService: VoucherService, private fiscalPeriodService: FiscalPeriodService,
    public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.Voucher, Metadatas.Voucher);
  }

  ngOnInit(): void {

    this.editForm.reset();

    this.editForm.reset(this.editModel);

    if (this.editModel != undefined) {
      this.voucher_Id = this.editModel.id;
      this.documentStatus = this.editModel.statusId;
    }

    setTimeout(() => {
      this.editForm.reset(this.editModel);
    })

  }


  public checkHandler(voucherId: number, statusId: DocumentStatusValue) {
    if (statusId == DocumentStatusValue.Draft) {
      //check
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.CheckVoucher, voucherId)).subscribe(res => {

        this.editModel.statusId = DocumentStatusValue.NormalCheck;
        this.showMessage(this.updateMsg, MessageType.Succes);

        this.documentStatus = DocumentStatusValue.NormalCheck;

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

        this.documentStatus = DocumentStatusValue.Draft;

      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }


  }


  addNew() {
    this.changeMode.emit(true);
    this.isNew = true;
    this.editModel = new VoucherInfo();
    this.editModel.date = new Date();
    this.editForm.reset(this.editModel);
  }


  focusHandler(e: any) {
    this.setFocus.emit();
  }

}
