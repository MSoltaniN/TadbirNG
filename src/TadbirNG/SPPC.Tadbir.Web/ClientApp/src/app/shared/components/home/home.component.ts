import {
  Component,
  OnInit,
  Renderer2,
  TemplateRef,
  ViewChild,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { RTL } from "@progress/kendo-angular-l10n";
import { SettingService } from "@sppc/config/service";
import { VoucherService } from "@sppc/finance/service";
import { DefaultComponent } from "@sppc/shared/class";
import { Layout, MessageType } from "@sppc/shared/enum/metadata";
import {
  BrowserStorageService,
  ErrorHandlingService,
  MetaDataService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
import "rxjs/Rx";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "home",
  templateUrl: "./home.component.html",
  styles: [
    `
      input[type="text"],
      textarea {
        width: 100%;
      }
      .home-img {
        position: absolute;
        top: calc(50% - 250px);
        right: calc(50% - 170px);
        opacity: 0.1;
      }

      .open-voucher-msg {
        padding: 10px;
      }
    `,
  ],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class HomeComponent extends DefaultComponent implements OnInit {
  @ViewChild("itemListRef", {static: true}) el: TemplateRef<any>;
  @ViewChild("dialogActions", {static: true}) actionBtn: TemplateRef<any>;

  @ViewChild("elClsoingTmp", {static: true}) elClsoingTmp: TemplateRef<any>;
  @ViewChild("closingTmpActions", {static: true}) closingTmpActionBtn: TemplateRef<any>;

  @ViewChild("elOpenVoucherConfirmBox", {static: true}) elOVConfirmBox: TemplateRef<any>;
  @ViewChild("elOpenVoucherActions", {static: true}) elOVConfirmBoxActions: TemplateRef<any>;

  private dialog;
  voucherNo: number;
  returnUrl: string;
  mode: string;
  type: string;

  closingTmpData: any;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    private activeRoute: ActivatedRoute,
    public router: Router,
    public bStorageService: BrowserStorageService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    private dialogService: DialogService,
    public voucherService: VoucherService,
    public errorHandlingService: ErrorHandlingService
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      settingService,
      "",
      undefined
    );
  }

  ngOnInit() {
    this.returnUrl = this.activeRoute.snapshot.queryParamMap.get("returnUrl");
    this.mode = this.activeRoute.snapshot.queryParamMap.get("mode");

    if (this.activeRoute.snapshot.queryParamMap.get("type")) {
      this.type = this.activeRoute.snapshot.queryParamMap.get("type");
    }

    switch (this.mode) {
      case "closing-tmp":
        this.showClosingTmpDialog();
        break;
      case "by-no":
        this.showByNoDialog();
        break;
      case "opening-voucher":
        this.showOpeningVoucherConfirm();
        break;
    }
  }

  showClosingTmpDialog() {
    this.dialog = this.dialogService.open({
      title: "دریافت موجودی کالای پایان دوره",
      content: this.elClsoingTmp,
      actions: this.closingTmpActionBtn,
      height: 420,
      width: 150,
    });
  }

  showByNoDialog() {
    this.dialog = this.dialogService.open({
      title: "شماره سند",
      content: this.el,
      actions: this.actionBtn,
    });
  }

  showOpeningVoucherConfirm() {
    this.dialog = this.dialogService.open({
      title: "پیغام تایید",
      content: this.elOVConfirmBox,
      actions: this.elOVConfirmBoxActions,
      width: 430,
      height: 245,
    });
  }

  openingVoucherOk() {
    this.voucherService.getOpeningVoucher(true).subscribe(
      (result: any) => {
        this.close();
        this.router.navigate([this.returnUrl], {
          queryParams: { no: result.no },
        });
      },
      (err) => {
        if (err.statusCode == 400) {
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
        }

        if (err.value) {
          this.showMessage(err.value, MessageType.Warning);
        }

        this.router.navigate(["/finance/voucher"]);
      }
    );
  }

  closingDataChanged(data: any) {
    this.closingTmpData = data;
  }

  closingTmpOk() {
    this.voucherService
      .getClosingAccountsVoucher(this.closingTmpData)
      .subscribe(
        (result: any) => {
          this.close();
          this.router.navigate([this.returnUrl], {
            queryParams: { no: result.no },
          });
        },
        (err) => {
          if (err.statusCode == 400) {
            this.showMessage(
              this.errorHandlingService.handleError(err),
              MessageType.Warning
            );
          }

          if (err.value) {
            this.showMessage(err.value, MessageType.Warning);
          }

          this.router.navigate(["/finance/voucher"]);
        }
      );
  }

  openVouchers() {
    this.router.navigate(["/finance/voucher"]);
  }

  openingVoucherCancel() {
    this.voucherService.getOpeningVoucher(false).subscribe(
      (result: any) => {
        this.close();
        this.router.navigate([this.returnUrl], {
          queryParams: { no: result.no },
        });
      },
      (err) => {
        if (err.statusCode == 400) {
          this.showMessage(
            this.errorHandlingService.handleError(err),
            MessageType.Warning
          );
        }

        if (err.value) {
          this.showMessage(err.value, MessageType.Warning);
        }

        this.router.navigate(["/finance/voucher"]);
      }
    );
  }

  passVoucherId() {
    this.close();
    if (this.type != "")
      this.router.navigate([this.returnUrl], {
        queryParams: { no: this.voucherNo, type: this.type },
      });
    else
      this.router.navigate([this.returnUrl], {
        queryParams: { no: this.voucherNo },
      });
  }

  public close() {
    if (this.dialog) {
      this.dialog.close();
      this.router.navigate(["/finance/voucher"]);
    }
  }
}
