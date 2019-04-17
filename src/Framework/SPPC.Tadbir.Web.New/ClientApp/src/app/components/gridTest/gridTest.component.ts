import { Component, OnInit, Renderer2, ChangeDetectorRef } from '@angular/core';
import { SettingService, GridService } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { Layout, Entities, Metadatas} from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { VoucherApi, AccountApi } from '../../service/api/index';
import { DialogService } from '@progress/kendo-angular-dialog';
import { AutoGeneratedGridComponent } from '../../class/autoGeneratedGrid.component';
import { Filter } from '../../class/filter';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
import { ReportApi } from '../../service/api/reportApi';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'gridTest',
  templateUrl: './gridTest.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class GridTestComponent extends AutoGeneratedGridComponent implements OnInit {

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, cdref);
  }

  ngOnInit() {
    //this.metadataUrlType = 2;
    //this.entityTypeName = Entities.JournalByDateByRow;
    //this.metadataType = Metadatas.JournalByDateByRow;
    //this.getDataUrl = ReportApi.JournalByDateByRow;

    //this.reloadGrid1();

    this.metadataUrlType = 1;
    this.entityTypeName = Entities.Voucher;
    this.metadataType = Metadatas.Voucher;
    this.getDataUrl = VoucherApi.EnvironmentVouchers;

    this.reloadGrid();
  }

  changeEntity() {
    this.currentFilter = this.addFilterToFilterExpression(this.currentFilter,
      new Filter("ParentId", "null", "== {0}", "System.Int32"),
      FilterExpressionOperator.And);

    this.entityTypeName = 'Account';
    this.metadataType = 'accounts';
    this.getDataUrl = AccountApi.EnvironmentAccounts;
    this.reloadGrid();
  }


  reloadGrid1(insertedModel?: any) {

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

        //this.creditSum = resData.creditSum;
        //this.debitSum = resData.debitSum;

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


