import { Component, OnInit, Renderer2, forwardRef, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { AutoGeneratedGridComponent, Filter, FilterExpressionOperator, FilterExpression, String } from '@sppc/shared/class';
import { Layout, environment, MessageType, Entities } from '@sppc/env/environment';
import { BrowserStorageService, MetaDataService, GridService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { Item } from '@sppc/shared/models';
import { BranchScopeResource } from '@sppc/finance/enum';
import { SystemIssueService } from '@sppc/finance/service';
import { SystemIssueApi, VoucherApi } from '@sppc/finance/service/api';
import { SystemIssue } from '@sppc/finance/models';
import { TreeItem, TreeItemLookup } from '@progress/kendo-angular-treeview';
import { DialogService } from '@progress/kendo-angular-dialog';
import { ColumnBase } from '@progress/kendo-angular-grid';
import { ViewName, VoucherPermissions } from '@sppc/shared/security';
import { VoucherEditorComponent } from '../../operational/voucher/voucher-editor.component';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'system-issue',
  templateUrl: './systemIssue.component.html',
  styleUrls: ['./systemIssue.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class SystemIssueComponent extends AutoGeneratedGridComponent implements OnInit {

  clickedRowItem: any = undefined;
  fromDate: Date;
  toDate: Date;
  systemIssuesList: Array<SystemIssue> = [];
  selectedBranchScope: string = '1';
  isEnabledBranchScope: boolean = true;

  checkedIssues: Array<number> = [];
  selectedIssue: Array<number> = [];
  selectedSystemIssue: SystemIssue;
  listTitle: string;
  isShowGrid: boolean = false;

  issuesCountList: Array<{ id: number, count: number }> = [];

  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public systemIssueService: SystemIssueService,
    public bStorageService: BrowserStorageService, public settingService: SettingService, public ngZone: NgZone) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit(): void {
    this.getSystemIssuesList();
  }

  getSystemIssuesList() {
    this.systemIssueService.getModels(SystemIssueApi.SystemIssues).subscribe(res => {
      this.systemIssuesList = res;
    })
  }

  dateValueChange(event: any) {
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;

    this.changeParam();

    this.handelIssueCount();
    this.reloadGrid();
  }

  public checkById(item: TreeItem) {
    return item.dataItem.id;
  }

  issueCount = (issueId: number) => {

    var item = this.issuesCountList.find(f => f.id == issueId);

    return item ? item.count : undefined;
  }

  handleSelection(event: any) {
    this.pageIndex = 0;
    this.selectedSystemIssue = event.dataItem;
    this.isEnabledBranchScope = true;

    if (this.selectedSystemIssue.viewId && this.selectedSystemIssue.apiUrl) {
      var issue = this.systemIssuesList.find(f => f.id == this.selectedSystemIssue.id)
      this.listTitle = issue.title;
      this.isShowGrid = true;
      this.viewId = this.tempViewId = issue.viewId;
      this.entityName = ViewName[this.viewId];

      if (!this.selectedSystemIssue.branchScope) {
        this.isEnabledBranchScope = false;
      }

      this.getDataUrl = environment.BaseUrl + this.selectedSystemIssue.apiUrl;
      this.reloadGrid();
    }
    else {
      this.listTitle = undefined;
      this.isShowGrid = false;
    }
  }

  handleChecked() {
    this.isEnabledBranchScope = true;
    let missing = this.issuesCountList.filter(item => this.checkedIssues.findIndex(f => f == item.id) < 0);

    missing.forEach(item => {
      var missIndex = this.issuesCountList.findIndex(f => f.id == item.id);
      if (missIndex > -1)
        this.issuesCountList.splice(missIndex, 1);
    })

    if (this.checkedIssues.length > 0)
      this.isEnabledBranchScope = false;

    this.handelIssueCount();
  }

  handelIssueCount() {
    this.checkedIssues.forEach(item => {

      var issue = this.systemIssuesList.find(f => f.id == item)
      if (issue.branchScope)
        this.isEnabledBranchScope = true;

      var index = this.issuesCountList.findIndex(f => f.id == item);
      if (index == -1) {
        var issueItem = this.systemIssuesList.find(f => f.id == item);
        if (issueItem.apiUrl && issueItem.viewId)
          this.getIssueCount(issueItem);
      }
    })
  }

  getIssueCount(issue: SystemIssue) {
    var apiUrl = environment.BaseUrl + issue.apiUrl + "?from=" + this.fromDate + "&to=" + this.toDate;

    var currentFilter = this.setFilter(issue.branchScope);

    this.systemIssueService.getAll(apiUrl, this.pageIndex, this.pageSize, this.sort, currentFilter).subscribe(res => {
      if (res.headers != null) {
        var headers = res.headers != undefined ? res.headers : null;
        if (headers != null) {
          var retheader = headers.get('X-Total-Count');
          if (retheader != null) {
            var totalCount = parseInt(retheader.toString());

            var index = this.issuesCountList.findIndex(f => f.id == issue.id);
            if (index > -1) {
              this.issuesCountList.splice(index, 1);
            }
            this.issuesCountList.push({ id: issue.id, count: totalCount })
          }
        }
      }

    })
  }

  onChangeBranchScope() {
    this.changeParam();
    this.handelIssueCount();
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: any) {
    if (this.getDataUrl) {

      this.grid.loading = true;

      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      this.reportFilter = null;

      var currentFilter = this.setFilter(this.selectedSystemIssue.branchScope);

      var apiUrl = this.getDataUrl + "?from=" + this.fromDate + "&to=" + this.toDate;

      this.gridService.getAll(apiUrl, this.pageIndex, this.pageSize, this.sort, currentFilter).subscribe((res) => {
        var resData = res.body;
        var totalCount = 0;

        if (res.headers != null) {
          var headers = res.headers != undefined ? res.headers : null;
          if (headers != null) {
            var retheader = headers.get('X-Total-Count');
            if (retheader != null) {
              totalCount = parseInt(retheader.toString());
            }
          }
          this.rowData = {
            data: resData,
            total: totalCount
          }

          this.viewId = this.tempViewId;

          if (this.rowData && this.rowData.total > 0) {
            var columnsToFit: Array<ColumnBase> = new Array<ColumnBase>();
            this.grid.leafColumns.forEach(function (item) {
              var column = item as ColumnBase;
              if (column.width == undefined) {
                columnsToFit.push(column);
              }
            });
            this.fitColumns(columnsToFit);
          }

          this.SetIssueCount(this.selectedSystemIssue);

          this.showloadingMessage = !(resData.length == 0);
          this.totalRecords = totalCount;
          this.grid.loading = false;
        }
      })
    }
    this.cdref.detectChanges();
  }

  SetIssueCount(issue: SystemIssue) {
    this.checkedIssues.push(issue.id);
    this.handleChecked();
  }

  setFilter(applyBranchScope: boolean): FilterExpression {
    this.defaultFilter = [];
    if (this.selectedBranchScope == "1" && applyBranchScope) {
      this.defaultFilter.push(new Filter("BranchId", this.BranchId.toString(), "== {0}", "System.Int32"));
    }

    var currentFilter = this.currentFilter ? JSON.parse(JSON.stringify(this.currentFilter)) : undefined;
    this.defaultFilter.forEach((item) => {
      currentFilter = this.addFilterToFilterExpression(currentFilter,
        item, FilterExpressionOperator.And);
    });

    return currentFilter;
  }

  changeParam() {
    this.issuesCountList = [];
    this.pageIndex = 0;
  }

  removeHandler() {
    var entityName = this.selectedSystemIssue.viewId == ViewName.VoucherLineDetail || this.selectedSystemIssue.viewId == ViewName.Voucher ?
      Entities.Voucher :
      this.entityTypeName;

    var action = VoucherPermissions.Delete;

    if (this.isAccess(entityName, action)) {
      this.deleteConfirm = true;
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
    else {
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }
  }

  deleteModel(confirm: boolean) {
    if (confirm) {
        this.grid.loading = true;

        let rowsId: Array<number> = [];

        this.selectedRows.forEach(item => {
          rowsId.push(item.id);
        })

        var deleteUrl = environment.BaseUrl + this.selectedSystemIssue.deleteApiUrl;

        this.systemIssueService.groupDelete(deleteUrl, rowsId).subscribe(res => {
          this.showMessage(this.getText("Messages.OperationSuccessful"), MessageType.Info);

          if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.selectedRows = [];
          this.groupOperation = false;
          this.reloadGrid();
          this.getIssueCount(this.selectedSystemIssue);

        }, (error => {
          this.grid.loading = false;
          this.showMessage(error, MessageType.Warning);
        }));
    }
    this.deleteConfirm = false;
  }

  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  rowDoubleClickHandler() {
    if (this.clickedRowItem) {
      var action;
      var entityName;
      var url;

      switch (this.selectedSystemIssue.viewId) {
        case ViewName.Voucher: {
          entityName = Entities.Voucher;
          action = VoucherPermissions.Edit;
          url = String.Format(VoucherApi.VoucherByNo, this.clickedRowItem.no);
          break;
        }
        case ViewName.VoucherLineDetail: {
          entityName = Entities.Voucher;
          action = VoucherPermissions.Edit;
          url = String.Format(VoucherApi.VoucherByNo, this.clickedRowItem.voucherNo);
          break;
        }
        default:
      }
      if (url) {
        if (this.isAccess(entityName, action)) {
          this.grid.loading = true;
          this.systemIssueService.getById(url).subscribe(res => {

            //فراخوانی کامپوننت مطابق با viewid باید انجام شود
            var voucherModel = res;
            if (voucherModel) {
              const dialogRef = this.dialogService.open({
                title: this.getText('Voucher.VoucherDetail'),
                content: VoucherEditorComponent,
              });

              const dialogModel = dialogRef.content.instance;
              dialogModel.voucherItem = voucherModel;
            }

            this.clickedRowItem = undefined;
            this.grid.loading = false;
          })
        }
        else
          this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
      }
    }
  }

}


