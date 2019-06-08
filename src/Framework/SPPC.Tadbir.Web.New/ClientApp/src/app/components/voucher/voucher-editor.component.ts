import { Component, OnInit, Input, Renderer2, TemplateRef, ViewChild, ElementRef } from '@angular/core';
import { VoucherService, LookupService } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { Layout, Entities, Metadatas, MessageType } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailComponent } from '../../class/detail.component';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from '@progress/kendo-angular-dialog';
import { VoucherApi } from '../../service/api';
import { Voucher } from '../../model/index';
import { DocumentStatusValue } from '../../enum/documentStatusValue';
import { Item } from '../../model/item';
import { LookupApi } from '../../service/api/index';
import { ViewName } from '../../security/viewName';



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

  constructor(private voucherService: VoucherService, public toastrService: ToastrService, public translate: TranslateService, private activeRoute: ActivatedRoute,
    public renderer: Renderer2, public metadata: MetaDataService, public router: Router, private dialogService: DialogService, private lookupService: LookupService) {

    super(toastrService, translate, renderer, metadata, Entities.Voucher, ViewName.Voucher);

    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }


  ngOnInit() {
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
    else
      this.router.navigate(['/vouchers/new']);
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

  onSave(e: any): void {
    e.preventDefault();

    if (this.editForm.valid) {

      let model: Voucher = this.editForm.value;
      model.branchId = this.BranchId;
      model.fiscalPeriodId = this.FiscalPeriodId;
      model.statusId = this.voucherModel.statusId;

      this.voucherService.edit<Voucher>(String.Format(VoucherApi.Voucher, model.id), model)
        .subscribe(response => {
          this.showMessage(this.updateMsg, MessageType.Succes);
        }, (error => {

          this.errorMessage = error;
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
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.UncheckVoucher, this.voucherModel.id)).subscribe(res => {

        this.voucherModel.statusId = DocumentStatusValue.Draft;
      }, (error => {
        var message = error.message ? error.message : error;
        this.errorMessage = message;
      }));
    }


  }
}


