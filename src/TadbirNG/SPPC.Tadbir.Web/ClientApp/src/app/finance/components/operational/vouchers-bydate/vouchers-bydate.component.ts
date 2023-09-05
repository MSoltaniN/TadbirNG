import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, NgZone, OnInit, Output, Renderer2, ViewChild } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { GridComponent } from "@progress/kendo-angular-grid";
import { SettingService } from "@sppc/config/service";
import { String } from "@sppc/shared/class";
import { Entities } from "@sppc/shared/enum/metadata";
import { ViewName } from "@sppc/shared/security";
import { BrowserStorageService, ErrorHandlingService, GridService, LookupService, MetaDataService, ReportingService } from "@sppc/shared/services";
import { LookupApi } from "@sppc/shared/services/api";
import { ToastrService } from "ngx-toastr";
import { VoucherComponent } from "../voucher/voucher.component";
import { ActivatedRoute } from "@angular/router";
import { VoucherService } from "@sppc/finance/service";

@Component({
  selector: "app-vouchers-bydate",
  templateUrl: "./vouchers-bydate.component.html",
  styleUrls: ["./vouchers-bydate.component.css"],
})
export class VouchersBydateComponent extends VoucherComponent
  implements OnInit {

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  clickedRowItem: any;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public dialogService: DialogService,
    public settingService: SettingService,
    public reporingService: ReportingService,
    private _voucherService: VoucherService,
    private _lookupService: LookupService,
    private _activeRoute: ActivatedRoute,
    public errorHandlingService: ErrorHandlingService,
    public ngZone: NgZone,
    public elem: ElementRef
  ) {
    super(
      toastrService,
      translate,
      dialogService,
      gridService,
      cdref,
      renderer,
      metadata,
      _voucherService,
      settingService,
      reporingService,
      ngZone,
      bStorageService,
      _lookupService,
      _activeRoute,
      errorHandlingService,
      elem,
    );
  }

  selectedVoucher;

  @Input() set vouchreDate(date){
    this.getDataUrl = String.Format(
      LookupApi.VouchersByDate,
      date
    );
  }
  @Output() save:EventEmitter<any> = new EventEmitter();
  @Output() cancel:EventEmitter<any> = new EventEmitter();

  ngOnInit(): void {
    this.entityName = Entities.VouchersByDate;
    this.viewId = ViewName[this.entityTypeName];

    this.reloadGrid()
  }

  registerWithSelectedItem() {
    this.save.emit(this.selectedRows[0]);
  }

  registerWithNewItem() {
    this.save.emit(0);
  }

  onCancel(e?) {
    this.cancel.emit();
  }

  escPress() {
    this.onCancel();
  }
}
