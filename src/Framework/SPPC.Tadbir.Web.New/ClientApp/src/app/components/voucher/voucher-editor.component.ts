import { Component, OnInit, Input, Renderer2, TemplateRef, ViewChild, ElementRef } from '@angular/core';
import { VoucherService } from '../../service/index';
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



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'voucher-editor',
  templateUrl: './voucher-editor.component.html',
  styles: [`
.voucher-form-content {margin-top:15px; border: solid 1px #3c8dbc; padding: 15px 10px;}
input[type=text],textarea { width: 100%; }
.voucher-status-item{ display: inline; margin: 0 10px;}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class VoucherEditorComponent extends DetailComponent implements OnInit {


  balancedMode: boolean = false;
  noCommittedMode: boolean = false;
  committedMode: boolean = false;
  finalizedMode: boolean = false;

  errorMessage: string;
  voucherModel: Voucher;


  constructor(private voucherService: VoucherService, public toastrService: ToastrService, public translate: TranslateService, private activeRoute: ActivatedRoute,
    public renderer: Renderer2, public metadata: MetaDataService, public router: Router, private dialogService: DialogService) {

    super(toastrService, translate, renderer, metadata, Entities.Voucher, Metadatas.Voucher);

    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }


  ngOnInit() {

    this.editForm.reset();

    this.activeRoute.params.subscribe(params => {
      switch (params['mode']) {
        case "new": {
          this.newVoucher();
          break;
        }
        case "last": {
          this.lastVoucher();
          break;
        }
        case "by-no": {
          this.byNoVoucher();
          break;
        }
        default:
      }


    })
  }


  newVoucher() {
    this.getVoucher(VoucherApi.NewVoucher);
  }

  byNoVoucher() {
    var voucherNo = this.activeRoute.snapshot.queryParamMap.get('voucherno');

    if (!voucherNo) {
      this.router.navigate(['/home'], { queryParams: { returnUrl: 'voucher/by-no' } });
    }
    else {
      this.getVoucher(String.Format(VoucherApi.VoucherByNo, voucherNo), true);
    }

  }

  getVoucher(apiUrl: string, byNo: boolean = false) {

    this.voucherService.getModels(apiUrl).subscribe(res => {
      this.editForm.reset(res);
      this.voucherModel = res;

      this.balancedMode = res.isBalanced;

      switch (res.statusId) {
        case DocumentStatusValue.Draft: {
          this.noCommittedMode = true;
          this.committedMode = false;
          this.finalizedMode = false;
          break;
        }
        case DocumentStatusValue.NormalCheck: {
          this.noCommittedMode = false;
          this.committedMode = true;
          this.finalizedMode = false;
          break;
        }
        case DocumentStatusValue.FinalCheck: {
          this.noCommittedMode = false;
          this.committedMode = false;
          this.finalizedMode = true;
          break;
        }
        default:
      }

    },
      error => {
        if (error.status == 404) {
          this.showMessage("سند مورد نظر یافت نشد", MessageType.Warning);
          if (byNo)
            this.router.navigate(['/home'], { queryParams: { returnUrl: 'voucher/by-no' } });
        }

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

  setBalanceValue(e: any) {
    this.balancedMode = e;
  }

  nextVoucher() {
    this.getVoucher(String.Format(VoucherApi.NextVoucher, this.voucherModel.no));
  }

  previousVoucher() {
    this.getVoucher(String.Format(VoucherApi.PreviousVoucher, this.voucherModel.no));
  }

  firstVoucher() {
    this.getVoucher(VoucherApi.FirstVoucher);
  }

  lastVoucher() {
    this.getVoucher(VoucherApi.LastVoucher);
  }

  searchVoucher() {
    this.router.navigate(['/home'], { queryParams: { returnUrl: 'voucher/by-no' } });
  }
}


