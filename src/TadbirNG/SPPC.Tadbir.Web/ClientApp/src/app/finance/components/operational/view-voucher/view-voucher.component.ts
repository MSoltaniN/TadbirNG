import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { VoucherEditorComponent } from '../voucher/voucher-editor.component';
import { VoucherService } from '@sppc/finance/service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BrowserStorageService, ErrorHandlingService, LookupService, MetaDataService } from '@sppc/shared/services';
import { DialogService } from '@progress/kendo-angular-dialog';

// برای مشاهده سند بدون دسترسی عملگرها
@Component({
  selector: 'app-view-voucher',
  templateUrl: './view-voucher.component.html',
  styleUrls: ['./view-voucher.component.css']
})
export class ViewVoucherComponent extends VoucherEditorComponent implements OnInit {

  constructor(
    private _voucherService: VoucherService,
    public toastrService: ToastrService,
    public translate: TranslateService,
    private _activeRoute: ActivatedRoute,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public router: Router,
    private _dialogService1: DialogService,
    private _lookupService: LookupService,
    public bStorageService: BrowserStorageService,
    public errorHandlingService: ErrorHandlingService,
    public elem: ElementRef
  ) {
    super(
      _voucherService,
      toastrService,
      translate,
      _activeRoute,
      renderer,
      metadata,
      router,
      _dialogService1,
      _lookupService,
      bStorageService,
      errorHandlingService,
      elem
    );
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

}
