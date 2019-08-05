import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/env/environment';
import { DefaultComponent } from '@sppc/shared/class';
import { AutoGeneratedGridComponent } from '@sppc/shared/class';
import { OperationLog } from '@sppc/admin/models';
import { OperationLogService } from '@sppc/admin/service';
import { SystemApi } from '@sppc/admin/service/api';
import { GridService, BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { ViewName } from '@sppc/shared/security';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'operationLogs',
  templateUrl: './operationLogs.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]
})


export class OperationLogsComponent extends AutoGeneratedGridComponent implements OnInit {

  rolesList: boolean = false;
  isNew: boolean;
  errorMessage: string;

  detailDataItem?: OperationLog = undefined;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private operationLogService: OperationLogService,
    public settingService: SettingService, public ngZone: NgZone, ) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.OperationLog;
    this.viewId = ViewName[this.entityTypeName];

    this.reloadGrid();
    this.cdref.detectChanges();
  }


  public editHandler(arg: any) {
    this.detailDataItem = this.selectedRows[0];
  }

  public cancelHandler() {
    this.detailDataItem = undefined;
  }


  reloadGrid(insertedModel?: OperationLog) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }
    this.operationLogService.getAll(SystemApi.AllOperationLogs, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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
      console.log(resData);
      this.rowData = {
        data: resData,
        total: totalCount
      }
      this.showloadingMessage = !(resData.length == 0);
      this.totalRecords = totalCount;
      this.grid.loading = false;
    })
  }

}
