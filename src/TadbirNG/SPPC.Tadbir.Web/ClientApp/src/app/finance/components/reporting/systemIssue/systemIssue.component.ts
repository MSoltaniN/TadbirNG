import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  NgZone,
  OnInit,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { GridComponent } from "@progress/kendo-angular-grid";
import { RTL } from "@progress/kendo-angular-l10n";
import { TreeItem } from "@progress/kendo-angular-treeview";
import { SettingService } from "@sppc/config/service";
import { environment } from "@sppc/env/environment";
import { BranchScopeResource } from "@sppc/finance/enum";
import { BranchScopeType } from "@sppc/finance/enum/shared";
import { SystemIssue } from "@sppc/finance/models";
import { SystemIssueService } from "@sppc/finance/service";
import { SystemIssueApi, VoucherApi } from "@sppc/finance/service/api";
import {
  AutoGeneratedGridComponent,
  Filter,
  FilterExpression,
  FilterExpressionOperator,
  String,
} from "@sppc/shared/class";
import { ReportViewerComponent, ViewIdentifierComponent } from "@sppc/shared/components";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { GridFilterComponent } from "@sppc/shared/directive/grid/component/grid-filter.component";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import { Item } from "@sppc/shared/models";
import { ViewName, VoucherPermissions } from "@sppc/shared/security";
import {
  BrowserStorageService,
  ErrorHandlingService,
  GridService,
  MetaDataService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
import "rxjs/Rx";
import { VoucherEditorComponent } from "../../operational/voucher/voucher-editor.component";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "system-issue",
  templateUrl: "./systemIssue.component.html",
  styleUrls: ["./systemIssue.component.css"],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class SystemIssueComponent
  extends AutoGeneratedGridComponent
  implements OnInit
{
  // Report
  @ViewChild(GridComponent, {static: false}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: false}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: false}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: false}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: false}) reportSetting: QuickReportSettingComponent;

  clickedRowItem: any = undefined;
  fromDate: Date;
  toDate: Date;
  systemIssuesList: Array<SystemIssue> = [];
  selectedBranchScope: string = BranchScopeType.CurrentBranch;
  isEnabledBranchScope: boolean = true;

  clickedIssues: Array<number> = [];
  checkedIssues: Array<number> = [];
  selectedIssue: Array<number> = [];
  selectedSystemIssue: SystemIssue;
  listTitle: string;
  isShowGrid: boolean = false;

  issuesCountList: Array<{ id: number; count: number }> = [];

  branchScope: Array<Item> = [
    {
      value: BranchScopeResource.CurrentBranch,
      key: BranchScopeType.CurrentBranch,
    },
    {
      value: BranchScopeResource.CurrentBranchAndSubsets,
      key: BranchScopeType.CurrentBranchAndSubsets,
    },
  ];

  @ViewChild(GridFilterComponent, {static: false}) gridFilter: GridFilterComponent;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public dialogService: DialogService,
    public gridService: GridService,
    public cdref: ChangeDetectorRef,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public systemIssueService: SystemIssueService,
    public bStorageService: BrowserStorageService,
    public settingService: SettingService,
    public ngZone: NgZone,
    public errorHandlingService: ErrorHandlingService,
    public elem: ElementRef
  ) {
    super(
      toastrService,
      translate,
      gridService,
      renderer,
      metadata,
      settingService,
      bStorageService,
      cdref,
      ngZone,
      elem
    );
  }

  ngOnInit(): void {
    this.getSystemIssuesList();
    this.clickedIssues = [];
  }

  getSystemIssuesList() {
    this.systemIssueService
      .getModels(SystemIssueApi.SystemIssues)
      .subscribe((res) => {
        this.systemIssuesList = res;
      });
  }

  dateValueChange(event: any) {
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;

    this.changeParam();

    this.handelIssueCount();
    this.getReportData();
  }

  public checkById(item: TreeItem) {
    return item.dataItem.id;
  }

  issueCount = (issueId: number) => {
    var item = this.issuesCountList.find((f) => f.id == issueId);

    return item ? item.count : undefined;
  };

  handleSelection(event: any) {
    this.pageIndex = 0;
    this.selectedSystemIssue = event.dataItem;
    this.isEnabledBranchScope = true;

    if (this.selectedSystemIssue.viewId && this.selectedSystemIssue.apiUrl) {
      var issue = this.systemIssuesList.find(
        (f) => f.id == this.selectedSystemIssue.id
      );
      this.listTitle = issue.title;
      this.isShowGrid = true;
      this.viewId = this.tempViewId = issue.viewId;
      this.entityName = ViewName[this.viewId];

      if (!this.selectedSystemIssue.branchScope) {
        this.isEnabledBranchScope = false;
      }

      this.getDataUrl = environment.BaseUrl + this.selectedSystemIssue.apiUrl;
      this.getReportData();
    } else {
      this.listTitle = undefined;
      this.isShowGrid = false;
    }
  }

  handleChecked(issue?: SystemIssue) {
    this.isEnabledBranchScope = true;

    this.handelIssueCount(issue);
  }

  handelIssueCount(issue?: SystemIssue) {
    var issueId = 0;
    if (issue) issueId = issue.id;
    else {
      if (!event || !event.target) return;
      const node = <Element>event.target;
      const hdnId = <any>node.closest(".k-item").querySelector(".hdn-id");
      const value = hdnId.value;

      issueId = parseInt(value);
    }

    if (this.checkedIssues.findIndex((p) => p == issueId) >= 0) {
      var issue = this.systemIssuesList.find((f) => f.id == issueId);
      if (issue.branchScope) this.isEnabledBranchScope = true;

      var index = this.issuesCountList.findIndex((f) => f.id == issueId);
      if (index == -1) {
        var issueItem = this.systemIssuesList.find((f) => f.id == issueId);
        this.getIssueCount(issueItem);
      }
    }
  }

  getIssueCount(issue: SystemIssue) {
    var apiUrl =
      String.Format(SystemIssueApi.SystemIssuesSummary, issue.id) +
      "?from=" +
      this.fromDate +
      "&to=" +
      this.toDate;

    var currentFilter = this.getDefaultFilter();
    var quickFilter = this.getQuickFilter(issue.branchScope);

    this.systemIssueService
      .getAll(
        apiUrl,
        this.pageIndex,
        this.pageSize,
        this.sort,
        currentFilter,
        quickFilter,
        false
      )
      .subscribe((res) => {
        var items = <Array<any>>res.body;
        items.forEach((issue) => {
          if (issue.itemCount != null) {
            var index = this.issuesCountList.findIndex((f) => f.id == issue.id);
            if (index > -1) {
              this.issuesCountList.splice(index, 1);
            }
            this.issuesCountList.push({ id: issue.id, count: issue.itemCount });
          }
        });
      });
  }

  onChangeBranchScope() {
    this.changeParam();
    this.handelIssueCount();
    this.getReportData();
  }

  getReportData() {
    if (this.getDataUrl) {
      this.quickFilter = [];
      this.currentFilter = undefined;
      this.defaultFilter = [];

      if (this.selectedSystemIssue && this.selectedSystemIssue.branchScope) {
        if (this.selectedBranchScope == "1") {
          this.quickFilter.push(
            new Filter(
              "BranchId",
              this.BranchId.toString(),
              "== {0}",
              "System.Int32"
            )
          );
        }
      }

      this.getDataUrl =
        this.getDataUrl + "?from=" + this.fromDate + "&to=" + this.toDate;

      if (
        this.checkedIssues.findIndex((i) => i == this.selectedSystemIssue.id) ==
        -1
      ) {
        this.listChanged = true;
        this.clickedIssues.push(this.selectedSystemIssue.id);
      } else this.listChanged = false;

      this.reloadGrid();
      this.gridFilter.removeFilterGridOnly();
    }
  }

  onDataBind() {
    this.viewId = this.tempViewId;
    this.SetIssueCount(this.selectedSystemIssue);
  }

  SetIssueCount(issue: SystemIssue) {
    var index = this.checkedIssues.findIndex((f) => f == issue.id);
    if (index < 0) this.checkedIssues.push(issue.id);
    this.handleChecked(issue);
  }

  getIssueCounts(issue: SystemIssue) {
    var apiUrl =
      environment.BaseUrl +
      issue.apiUrl +
      "?from=" +
      this.fromDate +
      "&to=" +
      this.toDate;

    var currentFilter = this.getDefaultFilter();
    var quickFilter = this.getQuickFilter(issue.branchScope);

    this.systemIssueService
      .getAll(
        apiUrl,
        this.pageIndex,
        this.pageSize,
        this.sort,
        currentFilter,
        quickFilter,
        false
      )
      .subscribe((res) => {
        if (res.headers != null) {
          var headers = res.headers != undefined ? res.headers : null;
          if (headers != null) {
            var retheader = headers.get("X-Total-Count");
            if (retheader != null) {
              var totalCount = parseInt(retheader.toString());

              var index = this.issuesCountList.findIndex(
                (f) => f.id == issue.id
              );
              if (index > -1) {
                this.issuesCountList.splice(index, 1);
              }
              this.issuesCountList.push({ id: issue.id, count: totalCount });
            }
          }
        }
      });
  }

  getDefaultFilter(): FilterExpression {
    this.defaultFilter = [];

    var currentFilter = this.currentFilter
      ? JSON.parse(JSON.stringify(this.currentFilter))
      : undefined;
    this.defaultFilter.forEach((item) => {
      currentFilter = this.addFilterToFilterExpression(
        currentFilter,
        item,
        FilterExpressionOperator.And
      );
    });

    return currentFilter;
  }

  getQuickFilter(applyBranchScope: boolean): FilterExpression {
    this.quickFilter = [];
    if (this.selectedBranchScope == "1" && applyBranchScope) {
      this.defaultFilter.push(
        new Filter(
          "BranchId",
          this.BranchId.toString(),
          "== {0}",
          "System.Int32"
        )
      );
    }

    let quickFilter: FilterExpression;

    this.quickFilter.forEach((item) => {
      quickFilter = this.addFilterToFilterExpression(
        quickFilter,
        item,
        FilterExpressionOperator.And
      );
    });

    return quickFilter;
  }

  changeParam() {
    this.issuesCountList = [];
    this.pageIndex = 0;
  }

  removeHandler() {
    var entityName =
      this.selectedSystemIssue.viewId == ViewName.VoucherLineDetail ||
      this.selectedSystemIssue.viewId == ViewName.Voucher
        ? Entities.Voucher
        : this.entityTypeName;

    var action = VoucherPermissions.Delete;

    if (this.isAccess(entityName, action)) {
      this.deleteConfirm = true;
      this.prepareDeleteConfirm(this.getText("Messages.SelectedItems"));
    } else {
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
    }
  }

  deleteModel(confirm: boolean) {
    if (confirm) {
      this.grid.loading = true;

      let rowsId: Array<number> = [];

      this.selectedRows.forEach((item) => {
        rowsId.push(item);
      });

      var deleteUrl =
        environment.BaseUrl + this.selectedSystemIssue.deleteApiUrl;

      this.systemIssueService.groupDelete(deleteUrl, rowsId).subscribe(
        (res) => {
          this.showMessage(
            this.getText("Messages.OperationSuccessful"),
            MessageType.Info
          );

          if (
            this.rowData.data.length == this.selectedRows.length &&
            this.pageIndex > 1
          )
            this.pageIndex =
              (this.pageIndex - 1) * this.pageSize - this.pageSize;

          this.selectedRows = [];
          this.groupOperation = false;
          this.getReportData();
          this.getIssueCount(this.selectedSystemIssue);
        },
        (error) => {
          this.grid.loading = false;
          this.showMessage(
            this.errorHandlingService.handleError(error),
            MessageType.Warning
          );
        }
      );
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
          url = String.Format(
            VoucherApi.VoucherByNo,
            this.clickedRowItem.voucherNo
          );
          break;
        }
        default:
      }
      if (url) {
        if (this.isAccess(entityName, action)) {
          this.grid.loading = true;
          this.systemIssueService.getById(url).subscribe((res) => {
            //فراخوانی کامپوننت مطابق با viewid باید انجام شود
            var voucherModel = res;
            if (voucherModel) {
              const dialogRef = this.dialogService.open({
                title: this.getText("Voucher.VoucherDetail"),
                content: VoucherEditorComponent,
              });

              const dialogModel = dialogRef.content.instance;
              dialogModel.voucherItem = voucherModel;
            }

            this.clickedRowItem = undefined;
            this.grid.loading = false;
          });
        } else
          this.showMessage(
            this.getText("App.AccessDenied"),
            MessageType.Warning
          );
      }
    }
  }
}
