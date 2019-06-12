import { Component, OnInit, Input, Renderer2, TemplateRef, ViewChild, ElementRef } from '@angular/core';
import { VoucherService, LookupService, VoucherInfo } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { Layout, Entities, Metadatas, MessageType } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailComponent } from '../../class/detail.component';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService, DialogRef, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { VoucherApi } from '../../service/api';
import { Voucher } from '../../model/index';
import { DocumentStatusValue } from '../../enum/documentStatusValue';
import { Item } from '../../model/item';
import { LookupApi } from '../../service/api/index';
import { ViewName } from '../../security/viewName';
import { VoucherOperations } from '../../enum/voucherOperations';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'voucher-editor',
  templateUrl: './voucher-editor.component.html',
  styles: [`
.voucher-form-content {margin-top:5px; border: solid 1px #3c8dbc; padding: 7px 10px 0;}
input[type=text], textarea, .ddl-type { width: 100%; }
.voucher-status-item{ display: inline; margin: 0 10px;}

/deep/.dialog-padding .k-window-content {padding:15px !important}

.col-xs-5ths,
.col-sm-5ths,
.col-md-5ths,
.col-lg-5ths,
.col-sm-4-5ths{
  position: relative;
  min-height: 1px;
  padding-right: 15px;
  padding-left: 15px;
}

.col-xs-5ths {
  width: 20%;
  float: left;
}

@media (min-width: 768px) {
  .col-sm-5ths {
    width: 20%;
    float: left;
  }
  .col-sm-4-5ths{
    width: 80%;
    float: left;
  }
}

@media (min-width: 992px) {
  .col-md-5ths {
    width: 20%;
    float: left;
  }
}

@media (min-width: 1200px) {
  .col-lg-5ths {
    width: 20%;
    float: left;
  }
}



`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class VoucherEditorComponent extends DetailComponent implements OnInit {

  errorMessage: string;
  voucherModel: Voucher;
  voucherTypeList: Array<Item> = [];
  selectedType: string;

  @Input() voucherItem: Voucher;
  isShowBreadcrumb: boolean = true;
  isFirstVoucher: boolean = false;
  isLastVoucher: boolean = false;

  voucherOperationsItem: any;

  constructor(private voucherService: VoucherService, public toastrService: ToastrService, public translate: TranslateService, private activeRoute: ActivatedRoute,
    public renderer: Renderer2, public metadata: MetaDataService, public router: Router, private dialogService: DialogService, private lookupService: LookupService) {

    super(toastrService, translate, renderer, metadata, Entities.Voucher, ViewName.Voucher);

    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }


  ngOnInit() {
    this.voucherOperationsItem = VoucherOperations;

    this.editForm.reset();

    if (this.voucherItem) {
      this.initVoucherForm(this.voucherItem);
      this.isShowBreadcrumb = false;
    }
    else {
      this.activeRoute.params.subscribe(params => {
        switch (params['mode']) {
          case "new": {
            this.newVoucher();
            break;
          }
          case "last": {
            this.isLastVoucher = true;
            this.getVoucher(VoucherApi.LastVoucher);
            break;
          }
          case "by-no": {
            this.byNoVoucher();
            break;
          }
          case "first": {
            this.isFirstVoucher = true;
            this.getVoucher(VoucherApi.FirstVoucher);
            break
          }
          case "next": {
            var voucherNo = this.activeRoute.snapshot.queryParamMap.get('no')
            if (voucherNo)
              this.getVoucher(String.Format(VoucherApi.NextVoucher, voucherNo), true);
            break
          }
          case "previous": {
            var voucherNo = this.activeRoute.snapshot.queryParamMap.get('no')
            if (voucherNo)
              this.getVoucher(String.Format(VoucherApi.PreviousVoucher, voucherNo), true);
            break
          }
          default: {
            this.isShowBreadcrumb = false;
            this.newVoucher();
          }
        }
      })
    }

    this.getVoucherType();
  }


  newVoucher() {
    this.getVoucher(VoucherApi.NewVoucher);
  }

  getNewVoucher() {
    if (this.voucherItem)
      this.getVoucher(VoucherApi.NewVoucher);
    else {
      this.redirectTo('/vouchers/new')
    }
  }

  redirectTo(uri) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate([uri]));
  }

  byNoVoucher() {
    var voucherNo = this.activeRoute.snapshot.queryParamMap.get('no');

    if (!voucherNo) {
      this.router.navigate(['/home'], { queryParams: { returnUrl: 'vouchers/by-no' } });
    }
    else {
      this.getVoucher(String.Format(VoucherApi.VoucherByNo, voucherNo), true);
    }

  }

  getVoucher(apiUrl: string, byNo: boolean = false) {
    this.voucherService.getModels(apiUrl).subscribe(res => {

      this.initVoucherForm(res);
      this.errorMessage = undefined;
    },
      error => {
        if (error.status == 404) {
          this.showMessage(this.getText("Voucher.VoucherNotFound"), MessageType.Warning);
          if (byNo)
            this.router.navigate(['/home'], { queryParams: { returnUrl: 'vouchers/by-no' } });
        }

      })
  }

  initVoucherForm(item: Voucher) {

    this.editForm.reset(item);

    this.voucherModel = item;
    this.selectedType = this.voucherModel.type.toString();
  }

  getVoucherType() {
    this.lookupService.getModels(LookupApi.VoucherSysTypes).subscribe(res => {
      this.voucherTypeList = res;
    })
  }

  onSave(e?: any): void {
    if (this.editForm.valid) {

      let model: Voucher = this.editForm.value;
      model.branchId = this.BranchId;
      model.fiscalPeriodId = this.FiscalPeriodId;
      model.statusId = this.voucherModel.statusId;
      model.saveCount = this.voucherModel.saveCount;
      debugger;
      this.voucherService.edit<Voucher>(String.Format(VoucherApi.Voucher, model.id), model).subscribe(res => {
        debugger;
        this.editForm.reset(res);
        this.voucherModel = res;
        this.errorMessage = undefined;
        this.showMessage(this.updateMsg, MessageType.Succes);
      }, (error => {
        if (e)
          this.errorMessage = error;
        else
          this.showMessage(error, MessageType.Warning);
      }));

    }
  }

  nextVoucher() {
    if (this.voucherItem) {
      this.getVoucher(String.Format(VoucherApi.NextVoucher, this.voucherModel.no));
      this.isFirstVoucher = false;
      this.isLastVoucher = false;
    }
    else
      this.router.navigate(['/vouchers/next'], { queryParams: { no: this.voucherModel.no } });
  }

  previousVoucher() {
    if (this.voucherItem) {
      this.getVoucher(String.Format(VoucherApi.PreviousVoucher, this.voucherModel.no));
      this.isFirstVoucher = false;
      this.isLastVoucher = false;
    }
    else
      this.router.navigate(['/vouchers/previous'], { queryParams: { no: this.voucherModel.no } });
  }

  firstVoucher() {
    if (this.voucherItem) {
      this.getVoucher(VoucherApi.FirstVoucher);
      this.isFirstVoucher = true;
      this.isLastVoucher = false;
    }
    else
      this.router.navigate(['/vouchers/first']);
  }

  lastVoucher() {
    if (this.voucherItem) {
      this.getVoucher(VoucherApi.LastVoucher);
      this.isFirstVoucher = false;
      this.isLastVoucher = true;
    }
    else
      this.router.navigate(['/vouchers/last']);
  }

  searchVoucher() {
    this.router.navigate(['/home'], { queryParams: { returnUrl: 'vouchers/by-no' } });
  }

  checkHandler() {
    this.errorMessage = undefined;
    if (this.voucherModel.statusId == DocumentStatusValue.Draft) {
      //check
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.CheckVoucher, this.voucherModel.id)).subscribe(res => {

        this.voucherModel.statusId = DocumentStatusValue.NormalCheck;
      }, (error => {
        var message = error.message ? error.message : error;
        this.errorMessage = message;
      }));

    }
    else {
      //uncheck
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.UndoCheckVoucher, this.voucherModel.id)).subscribe(res => {

        this.voucherModel.statusId = DocumentStatusValue.Draft;
      }, (error => {
        var message = error.message ? error.message : error;
        this.errorMessage = message;
      }));
    }


  }

  voucherOperation(item: VoucherOperations) {
    var model1 = new VoucherInfo();
    var model2 = new VoucherInfo();
    
    model1.no = parseInt(this.editForm.value.no);
    model1.reference = this.editForm.value.reference;
    model1.dailyNo = parseInt(this.editForm.value.dailyNo);
    model1.association = this.editForm.value.association;
    model1.date = new Date(this.editForm.value.date);
    model1.description = this.editForm.value.description;
    model1.type = parseInt(this.editForm.value.type);

    model2.no = this.voucherModel.no;
    model2.reference = this.voucherModel.reference;
    model2.dailyNo = this.voucherModel.dailyNo;
    model2.association = this.voucherModel.association;
    model2.date = new Date(this.voucherModel.date);
    model2.description = this.voucherModel.description;
    model2.type = this.voucherModel.type;

    var isFormDataChenged = true;
    if (JSON.stringify(model2) === JSON.stringify(model1))
      isFormDataChenged = false;

    if (isFormDataChenged) {
      
      const dialog: DialogRef = this.dialogService.open({
        title: this.getText('Entity.Voucher'),
        content: this.getText('Voucher.SaveChanges'),
        actions: [
          { text: this.getText('Buttons.Yes'), mode: 1, primary: true },
          { text: this.getText('Buttons.No'), mode: 0 }
        ],
        width: 450,
        height: 150,
        minWidth: 250
      });

      dialog.dialog.location.nativeElement.classList.add('dialog-padding');

      dialog.result.subscribe((result) => {
        let res: any = result;
        if (res.mode == 1)
          this.onSave();

        this.executeVoucherOperation(item);

      });

    }
    else {
      this.executeVoucherOperation(item);
    }

  }

  executeVoucherOperation(item: VoucherOperations) {
    switch (item) {
      case VoucherOperations.First: {
        this.firstVoucher();
        break;
      }
      case VoucherOperations.Last: {
        this.lastVoucher();
        break;
      }
      case VoucherOperations.New: {
        this.getNewVoucher();
        break;
      }
      case VoucherOperations.Next: {
        this.nextVoucher();
        break;
      }
      case VoucherOperations.Previous: {
        this.previousVoucher();
        break;
      }
      case VoucherOperations.Search: {
        this.searchVoucher();
        break;
      }
      default:
    }
  }

}


