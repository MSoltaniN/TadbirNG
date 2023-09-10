import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, NgZone, OnInit, Output, Renderer2, ViewChild } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogCloseResult, DialogService } from "@progress/kendo-angular-dialog";
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
import { ViewVoucherComponent } from "../view-voucher/view-voucher.component";
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

  /**
   * باز کردن و مقداردهی اولیه به فرم ویرایشگر
   */
  openEditorDialog(isNew: boolean) {
    if (this.selectedSubjectType == "-1" && isNew) {
      this.showMessage(this.getText("Voucher.AddNewNotPossible"));
      return;
    } else {
      if (!this.checkEditPermission()) return;
    }

    var subjectType = this.selectedSubjectType;
    if (!isNew) subjectType = this.editDataItem.subjectType;
    // VoucherEditorComponent
    // ViewVoucherComponent
    this.dialogRef = this.dialogService.open({
      title:
        subjectType == "1"
          ? this.getText("Voucher.DraftVoucherDetail")
          : this.getText("Voucher.VoucherDetail"),
      content: ViewVoucherComponent
    });
    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.voucherItem = this.editDataItem;
    this.dialogModel.dialogMode = true;
    this.editDataItem = undefined;
    this.dialogModel.subjectMode = parseInt(this.selectedSubjectType);

    if (this.reportFilter) {
      this.dialogModel.filter = JSON.parse(JSON.stringify(this.reportFilter));
    }

    if (this.reportQuickFilter) {
      this.dialogModel.quickFilter = JSON.parse(
        JSON.stringify(this.reportQuickFilter)
      );
    }

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.selectedRows = [];
        this.DataArray = [];
        this.reloadGrid();
      }
    });

    this.dialogRef.content.instance.cancel.subscribe((result) => {
      this.dialogRef.close();
    });
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
