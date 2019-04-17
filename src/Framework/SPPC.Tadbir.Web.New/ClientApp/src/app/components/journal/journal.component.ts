import { Component, OnInit, Renderer2, ChangeDetectorRef } from '@angular/core';
import { SettingService, GridService } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { Layout, Metadatas, Entities } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DialogService } from '@progress/kendo-angular-dialog';
import { AutoGeneratedGridComponent } from '../../class/autoGeneratedGrid.component';
import { Filter } from '../../class/filter';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
import { ReportApi } from '../../service/api/reportApi';
import { Item } from '../../model/index';
import { JournalType, JournalDisplayType, JournalDisplayTypeResource, VoucherStatusResource, BranchScopeResource, ArticleTypesResource } from '../../enum/journal';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'spps-journal',
  templateUrl: './journal.component.html',
  styles: [`
.section-option { margin-top: 15px; background-color: #f6f6f6; border: solid 1px #dadde2; padding: 15px 15px 0; }
.section-option label,input[type=text] { width:100% } /deep/.section-option kendo-dropdownlist { width:100% }
/deep/ .k-switch-on .k-switch-handle { left: -8px !important; }
/deep/ .k-switch-off .k-switch-handle { left: -4px !important; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-on { right: -22px; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-off { left: 0; }
/deep/ .k-switch-label-on,/deep/ .k-switch-label-off { overflow: initial; }
.journal-type { margin:0 15px 10px; } .journal-type label { margin-top:10px; }
/deep/.k-footer-template { background-color: #b3b3b3; color: #000;}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class JournalComponent extends AutoGeneratedGridComponent implements OnInit {

  displayType: Array<Item> = [
    { value: JournalDisplayTypeResource.VoucherRows, key: "1" },
    { value: JournalDisplayTypeResource.VoucherRowsWithFloatingAccounts, key: "2" },
    { value: JournalDisplayTypeResource.LedgerLevel, key: "3" },
    { value: JournalDisplayTypeResource.SubsidiaryLevel, key: "4" },
    { value: JournalDisplayTypeResource.BriefVoucher, key: "5" },
    { value: JournalDisplayTypeResource.BriefVoucherByDate, key: "6" },
    { value: JournalDisplayTypeResource.BriefVoucherByMonth, key: "7" },
  ]
  voucherStatus: Array<Item> = [
    { value: VoucherStatusResource.Committed, key: "2" },
    { value: VoucherStatusResource.Finalized, key: "3" },
    { value: VoucherStatusResource.Accepted, key: "4" },
    { value: VoucherStatusResource.Approved, key: "5" },
    { value: VoucherStatusResource.AllVouchers, key: "0" }
  ]
  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]
  articleType: Array<Item> = [
    { value: ArticleTypesResource.AllVoucherLines, key: "1" },
    { value: ArticleTypesResource.MarkedVoucherLines, key: "2" },
    { value: ArticleTypesResource.UncheckedVoucherLines, key: "3" },
  ]

  displayTypeSelected: string = '1';
  branchScopeSelected: string = '1';
  voucherStatusSelected: string = '2';
  articleTypeSelected: string = '1';

  fromDate: Date;
  toDate: Date;
  journalType: string = '1';

  creditSum: number = 0;
  debitSum: number = 0; 

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, cdref);
  }

  ngOnInit() {
    this.metadataUrlType = 2;
    this.entityTypeName = Entities.JournalByDateByRow;
    this.metadataType = Metadatas.JournalByDateByRow;

    this.showloadingMessage = false;

    this.cdref.detectChanges();
  }

  dateValueChange(event: any) {
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;
  }

  getReportData() {
    if (this.journalType) {

      this.creditSum = 0;
      this.debitSum = 0;
      this.selectedRows = [];
      this.pageIndex = 0;

      this.defaultFilter = [];
      if (this.voucherStatusSelected != "0") {
        this.defaultFilter.push(new Filter("VoucherStatusId", this.voucherStatusSelected, "== {0}", "System.Int32"));
      }

      if (this.branchScopeSelected == "1") {
        this.defaultFilter.push(new Filter("BranchId", this.BranchId.toString(), "== {0}", "System.Int32"));
      }

      if (this.journalType == JournalType.ByDate && this.fromDate && this.toDate) {
        switch (parseInt(this.displayTypeSelected)) {
          case JournalDisplayType.ByDateByRow:
            {
              this.entityTypeName = Entities.JournalByDateByRow;
              this.metadataType = Metadatas.JournalByDateByRow;
              this.getDataUrl = ReportApi.JournalByDateByRow;

              break;
            }
          case JournalDisplayType.ByDateByRowDetail:
            {
              this.entityTypeName = Entities.JournalByDateByRowDetail;
              this.metadataType = Metadatas.JournalByDateByRowDetail;
              this.getDataUrl = ReportApi.JournalByDateByRowDetail;
              break;
            }
          case JournalDisplayType.ByDateByLedger:
            {
              this.entityTypeName = Entities.JournalByDateByLedger;
              this.metadataType = Metadatas.JournalByDateByLedger;
              this.getDataUrl = ReportApi.JournalByDateByLedger;
              break;
            }
          case JournalDisplayType.ByDateBySubsidiary:
            {
              this.entityTypeName = Entities.JournalByDateBySubsidiary;
              this.metadataType = Metadatas.JournalByDateBySubsidiary;
              this.getDataUrl = ReportApi.JournalByDateBySubsidiary;
              break;
            }
          case JournalDisplayType.ByDateLedgerSummary:
            {
              this.entityTypeName = Entities.JournalByDateSummary;
              this.metadataType = Metadatas.JournalByDateSummary;
              this.getDataUrl = ReportApi.JournalByDateLedgerSummary;
              break;
            }
          case JournalDisplayType.ByDateLedgerSummaryByDate:
            {
              this.entityTypeName = Entities.JournalByDateSummaryByDate;
              this.metadataType = Metadatas.JournalByDateSummaryByDate;
              this.getDataUrl = ReportApi.JournalByDateLedgerSummaryByDate;
              break;
            }
          case JournalDisplayType.ByDateLedgerSummaryByMonth:
            {
              this.getDataUrl = "";
              break;
            }
          default:
        }

        this.getDataUrl += "?from=" + this.fromDate + "&to=" + this.toDate;
        this.reloadGrid();
      }

      if (this.journalType == JournalType.ByVoucher) {
        switch (this.displayTypeSelected) {
          case '1':
            this.getDataUrl = "";
            break;
          case '1':
            this.getDataUrl = "";
            break;
          case '1':
            this.getDataUrl = "";
            break;
          case '1':
            this.getDataUrl = "";
            break;
          case '1':
            this.getDataUrl = "";
            break;
          case '1':
            this.getDataUrl = "";
            break;
          case '1':
            this.getDataUrl = "";
            break;
          default:
        }
      }
    }

  }

  reloadGrid(insertedModel?: any) {

    if (this.getDataUrl) {
      //if (this.viewAccess) {
      this.grid.loading = true;

      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      var currentFilter = this.currentFilter;
      this.defaultFilter.forEach(item => {
        currentFilter = this.addFilterToFilterExpression(currentFilter,
          item, FilterExpressionOperator.And);
      })
      var filter = currentFilter;

      this.gridService.getAll(this.getDataUrl, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {

        var resData = res.body;
        var totalCount = 0;

        if (res.headers != null) {
          var headers = res.headers != undefined ? res.headers : null;
          if (headers != null) {
            var retheader = headers.get('X-Total-Count');
            if (retheader != null)
              totalCount = parseInt(retheader.toString());
          }
        }
        this.rowData = {
          data: resData.items,
          total: totalCount
        }

        this.creditSum = resData.creditSum;
        this.debitSum = resData.debitSum;

        this.showloadingMessage = !(resData.items.length == 0);
        this.totalRecords = totalCount;
        this.grid.loading = false;
      })
      //}
      //else {
      //  this.rowData = {
      //    data: [],
      //    total: 0
      //  }
      //}

    }
    this.cdref.detectChanges();
  }
}


