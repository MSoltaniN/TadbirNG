import { Component, OnInit, Renderer2, TemplateRef, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from '@progress/kendo-angular-dialog';
import { Layout, MessageType } from '@sppc/env/environment';
import { MetaDataService, BrowserStorageService, LookupService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { DefaultComponent } from '@sppc/shared/class';
import { VoucherService } from '@sppc/finance/service';
import { error } from 'protractor';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styles: [`
  input[type=text],textarea { width: 100%; }
    .home-img {
      position: absolute;
      top: calc(50% - 250px);
      right: calc(50% - 170px);
      opacity: .1;
    }

  .open-voucher-msg {padding:10px;}
  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class HomeComponent extends DefaultComponent implements OnInit {

  @ViewChild('itemListRef') el: TemplateRef<any>;
  @ViewChild('dialogActions') actionBtn: TemplateRef<any>;


  @ViewChild('elClsoingTmp') elClsoingTmp: TemplateRef<any>;
  @ViewChild('closingTmpActions') closingTmpActionBtn: TemplateRef<any>;

  @ViewChild('elOpenVoucherConfirmBox') elOVConfirmBox: TemplateRef<any>;
  @ViewChild('elOpenVoucherActions') elOVConfirmBoxActions: TemplateRef<any>;

  private dialog;
  voucherNo: number;
  returnUrl: string;
  mode: string;
  type: string;

  closingTmpData: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, private activeRoute: ActivatedRoute, public router: Router, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, private dialogService: DialogService,public voucherService: VoucherService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);
  }

  ngOnInit() {

    this.returnUrl = this.activeRoute.snapshot.queryParamMap.get('returnUrl');
    this.mode = this.activeRoute.snapshot.queryParamMap.get('mode');

    if (this.activeRoute.snapshot.queryParamMap.get('type')) {
      this.type = this.activeRoute.snapshot.queryParamMap.get('type');
    }

    switch (this.mode) {
      case 'closing-tmp':
        this.showClosingTmpDialog();
        break;
      case 'by-no':
        this.showByNoDialog();
        break;
      case 'opening-voucher':
        this.showOpeningVoucherConfirm();        
        break;
    }
  }

  showClosingTmpDialog() {
    this.dialog = this.dialogService.open({
      title: 'دریافت موجودی کالای پایان دوره',
      content: this.elClsoingTmp,
      actions: this.closingTmpActionBtn,      
      height: 420,
      width:150
    });
  }

  showByNoDialog() {
    this.dialog = this.dialogService.open({
      title: 'شماره سند',
      content: this.el,
      actions: this.actionBtn
    });
  }

  showOpeningVoucherConfirm() {
    this.dialog = this.dialogService.open({
      title: 'پیغام تایید',
      content: this.elOVConfirmBox,
      actions: this.elOVConfirmBoxActions,
      width: 430,
      height:245
    });
  }

  openingVoucherOk() {
    this.voucherService.getOpeningVoucher(true).subscribe(result => {
      this.close();
      this.router.navigate([this.returnUrl], { queryParams: { no: result.no } });
    }, (msg) => {
        this.showMessage(msg, MessageType.Warning);
        this.router.navigate(['/finance/voucher']);
      }
    );
  }

  closingDataChanged(data:any) {
    this.closingTmpData = data;
  }

  closingTmpOk() {
    this.voucherService.getClosingAccountsVoucher(this.closingTmpData).subscribe(result => {
      this.close();
      this.router.navigate([this.returnUrl], { queryParams: { no: result.no } });
    }, (msg) => {
        this.showMessage(msg, MessageType.Warning);
        this.router.navigate(['/finance/voucher']);
    });
  }

  openVouchers() {
    this.router.navigate(['/finance/voucher']);
  }

  openingVoucherCancel() {
    this.voucherService.getOpeningVoucher(false).subscribe(result => {
      this.close();
      this.router.navigate([this.returnUrl], { queryParams: { no: result.no } });
    }, (msg) => {
        this.showMessage(msg, MessageType.Warning);
        this.router.navigate(['/finance/voucher']);
    });
  }

  passVoucherId() {
    this.close();
    if (this.type != '')
      this.router.navigate([this.returnUrl], { queryParams: { no: this.voucherNo, type: this.type } });
    else
      this.router.navigate([this.returnUrl], { queryParams: { no: this.voucherNo } });
  }

  public close() {
    if (this.dialog) {
      this.dialog.close();
        this.router.navigate(['/finance/voucher']);
    }
  }
}
