import { Component, OnInit, Renderer2, ChangeDetectorRef, ViewChild, NgZone, HostListener } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService, DialogRef, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { String, AutoGeneratedGridComponent, Filter, FilterExpressionOperator } from '@sppc/shared/class';
import { Layout, Metadatas, Entities, MessageType } from '@sppc/env/environment';
import { MetaDataService, ReportingService, BrowserStorageService, GridService } from '@sppc/shared/services';
import { VoucherService } from '@sppc/finance/service';
import { VoucherApi } from '@sppc/finance/service/api';
import { Voucher } from '@sppc/finance/models';
import { SettingService } from '@sppc/config/service';
import { VoucherEditorComponent } from './voucher-editor.component';
import { DocumentStatusValue, BranchScopeResource } from '@sppc/finance/enum';
import { ViewName, VoucherPermissions } from '@sppc/shared/security';
import { ReportViewerComponent } from '@sppc/shared/components/reportViewer/reportViewer.component';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier/view-identifier.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { Item } from '@sppc/shared/models';
import { AuthenticationService } from '@sppc/core';





export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'voucher',
  templateUrl: './voucher.component.html',
  styles: [`
  .ddl-type { width: 70%; }
  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class VoucherComponent extends AutoGeneratedGridComponent implements OnInit {

  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;

  firstLoad: boolean = true;
  dateFilter: Array<Filter> = [];
  metadataType: string = Metadatas.Voucher;
  DataArray: Array<any> = [];
  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]


  startDate: any;
  endDate: any;


  clickedRowItem: Voucher = undefined;
  editDataItem?: Voucher = undefined;

  returnFromCommitGroup: boolean = false;
  finalizedGroup: boolean = false;
  commitGroup: boolean = false;
  returnFromFinalizedGroup: boolean = false;

  private dialogRef: DialogRef;
  private dialogModel: any;
  UnConfirmGroupConfirm: boolean;
  branchSelected: string;
  showGetPasswordModal: boolean;
  specialPassword: string = null;
  showErrorMessage: boolean = false;
  showFinancBtns: boolean = false;





  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private voucherService: VoucherService, private authenticationService: AuthenticationService, public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone, public bStorageService: BrowserStorageService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.branchSelected = this.branchScope[0].key;
    this.defaultFilter = [];
    if (this.branchSelected == "1") {
      this.defaultFilter.push(new Filter("BranchId", this.BranchId.toString(), " == {0}", "System.Int32"));
    }
    this.entityName = Entities.Voucher;
    this.viewId = ViewName[this.entityTypeName];
    this.getDataUrl = VoucherApi.EnvironmentVouchers;

    this.cdref.detectChanges();
    
    this.showFinancBtns = false;
   

  }
  
  @HostListener('document:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.altKey && event.ctrlKey) {
      if (event.key.toLowerCase() == 'e') {
        var currentContext = this.bStorageService.getCurrentUser();
        if (currentContext.roles[0] == 1) {
          this.showGetPasswordModal = true
        } else {
          this.showGetPasswordModal = false
          return false;
        }
      }
    }
  }


  getPasswordModel(confirm: boolean) {
    if (confirm) {
      this.checkPasswordModel(this.specialPassword)
    } else {
      this.showGetPasswordModal = false
    }

  }

  checkPasswordModel(password: string) {
    var specialpassword = password;
    this.authenticationService.checkSpecialPassword(specialpassword, this.Ticket).subscribe(res => {
      this.showGetPasswordModal = false
      this.showFinancBtns = true;
    }, (error => {
      this.showFinancBtns = false;
      var message = error ? error.error : error.message;
      this.showMessage(message, MessageType.Warning);
    }));
  }

  onPasswordChange(value) {
    this.specialPassword = value;
    if (this.specialPassword == '') {
      this.showErrorMessage = true;
    } else {
      this.showErrorMessage = false;
    }

  }

  /**
   * باز کردن و مقداردهی اولیه به فرم ویرایشگر
   */
  openEditorDialog(isNew: boolean) {

    this.dialogRef = this.dialogService.open({
      title: this.getText('Voucher.VoucherDetail'),
      content: VoucherEditorComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.voucherItem = this.editDataItem;
    this.dialogModel.isOpenFromList = true;
    this.editDataItem = undefined;

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.selectedRows = [];
        this.DataArray = [];
        this.reloadGrid();
      }
    });

  }


  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  rowDoubleClickHandler() {
    if (this.isAccess(Entities.Voucher, VoucherPermissions.Edit)) {
      if (this.clickedRowItem) {
        this.grid.loading = true;
        this.voucherService.getById(String.Format(VoucherApi.Voucher, this.clickedRowItem.id)).subscribe(res => {
          this.editDataItem = res;

          this.openEditorDialog(false);

          this.grid.loading = false;
        })
      }
    }
    else
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (!this.groupOperation) {
      var recordId = this.selectedRows[0];//.id;
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
    else {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
  }

  editHandler(arg: any) {
    var recordId = this.selectedRows[0];//.id;
    this.grid.loading = true;
    this.voucherService.getById(String.Format(VoucherApi.Voucher, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.openEditorDialog(false);

      this.grid.loading = false;
    })
  }

  public showReport() {
    /*
    var url = String.Format(ReportApi.DefaultSystemReport, this.viewer.baseId);

    this.reporingService.getAll(url).subscribe((res: Response) => {    

      var report: Report = <any>res.body;
      var serviceUrl = environment.BaseUrl + "/" + report.serviceUrl;    

      this.reporingService.getAll(serviceUrl,
        this.sort, this.currentFilter).subscribe((response: any) => {

          var fdate = moment(this.FiscalPeriodStartDate, 'YYYY-M-D HH:mm:ss')
            .locale(this.CurrentLanguage)
            .format('YYYY/M/D');

          var tdate = moment(this.FiscalPeriodEndDate, 'YYYY-M-D HH:mm:ss')
            .locale(this.CurrentLanguage)
            .format('YYYY/M/D');


          var reportData = {
            rows: response.body, fromDate: fdate,
            toDate: tdate
          };
          //'/assets/reports/voucher/voucher.summary.mrt'
          this.viewer.showVoucherReport(report, reportData);

        });
    });

    */

    this.reportManager.DecisionMakingForShowReport();
  }

  dateValueChange(event: any) {

    this.startDate = event.fromDate;
    this.endDate = event.toDate;

    this.dateFilter = [];
    this.dateFilter.push(new Filter("Date", this.startDate, " >= {0} ", "System.DateTime"),
      new Filter("Date", this.endDate, " <= {0} ", "System.DateTime"));

    if (this.firstLoad && this.startDate && this.endDate) {

      this.reloadGrid();
    }
  }

  getVouchers() {
    this.defaultFilter = [];
    this.pageIndex = 0;
    if (this.branchSelected == "1") {
      this.defaultFilter.push(new Filter("BranchId", this.BranchId.toString(), " == {0}", "System.Int32"));
    } else {
      this.defaultFilter = []
    }

    this.reloadGrid();
  }
  /*
  reloadGrid(insertedModel?: Voucher) {
    this.firstLoad = false;

    this.grid.loading = true;

    var filter = this.currentFilter;
    this.dateFilter.forEach(item => {
      filter = this.addFilterToFilterExpression(filter,
        item, FilterExpressionOperator.And);
    })

    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }

    if (insertedModel)
      this.goToLastPage(this.totalRecords);

    this.reportFilter = filter;

    this.voucherService.getAll(VoucherApi.EnvironmentVouchers, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
      var resData = res.body;
      this.properties = resData.properties;
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
        data: resData,
        total: totalCount
      }
      this.showloadingMessage = !(resData.length == 0);
      this.totalRecords = totalCount;
      this.grid.loading = false;
    })

    this.cdref.detectChanges();

  }
  */


  deleteModel(confirm: boolean) {
    if (confirm) {
      if (this.groupOperation) {
        //حذف گروهی
        this.grid.loading = true;
        let rowsId: Array<number> = [];
        this.selectedRows.forEach(item => {
          rowsId.push(item);
        })

        this.voucherService.groupDelete(VoucherApi.EnvironmentVouchers, rowsId).subscribe(res => {
          this.showMessage(this.deleteMsg, MessageType.Info);

          if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.groupOperation = false;
          this.reloadGrid(undefined, undefined, true);
          this.selectedRows = [];

        }, (error => {
          this.grid.loading = false;
          this.showMessage(error, MessageType.Warning);
        }));
      }
      else {

        this.grid.loading = true;
        this.voucherService.delete(String.Format(VoucherApi.Voucher, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);

          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.reloadGrid(undefined, undefined, true);
          this.selectedRows = [];
        }, (error => {
          this.grid.loading = false;
          var message = error.message ? error.message : error;
          this.showMessage(message, MessageType.Warning);
        }));

      }
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }

  UnGroupConfirmModel(confirm: boolean) {

    if (confirm) {
      this.grid.loading = true;
      let rowsId: Array<number> = [];
      this.selectedRows.forEach(item => {
        rowsId.push(item);
      })

      this.voucherService.changeVouchersStatus(VoucherApi.UndoConfirmGroupVouchers, rowsId).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.grid.loading = false;
        this.showMessage(message, MessageType.Warning);
      }));

    }
    //hide confirm modal
    this.UnConfirmGroupConfirm = false;
  }


  addNew() {
    this.openEditorDialog(true);
  }

  isDisabledCheckBtn = () => {
    if (this.selectedRows.length == 0)
      return true;
    else if (this.selectedRows.length == 1) {
      var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[0]);
      if (index >= 0 && this.DataArray[index].statusId == DocumentStatusValue.Draft)
        return false;
      else
        return true;
    }
    else {
      let conflit: boolean = false;
      for (var i = 0; i < this.selectedRows.length; i++) {
        var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[i]);
        if (index >= 0 && this.DataArray[index].statusId != DocumentStatusValue.Draft) {
          conflit = true;
          break;
        }
      }
      if (conflit) {
        this.commitGroup = false;
        return true;
      } else {
        this.commitGroup = true;
        return false;
      }
    }

  }

  isDisabledUnCheckBtn = () => {
    if (this.selectedRows.length == 0)
      return true;
    else if (this.selectedRows.length == 1) {
      var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[0]);
      if (index >= 0 && this.DataArray[index].statusId == DocumentStatusValue.NormalCheck)
        return false;
      else
        return true;
    } else {
      let conflit: boolean = false;
      for (var i = 0; i < this.selectedRows.length; i++) {
        var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[i]);
        if (index >= 0 && this.DataArray[index].statusId != DocumentStatusValue.NormalCheck) {
          conflit = true;
          break;
        }
      }
      if (conflit) {
        this.returnFromCommitGroup = false;
        return true;
      } else {
        this.returnFromCommitGroup = true;
        return false;
      }

    }
  }


  isDisabledFinalizeBtn = () => {
    if (this.selectedRows.length == 0)
      return true;
    else if (this.selectedRows.length == 1) {
      var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[0]);
      if (index >= 0 && this.DataArray[index].statusId == DocumentStatusValue.NormalCheck)
        return false;
      else
        return true;
    } else {

      let conflit: boolean = false;
      for (var i = 0; i < this.selectedRows.length; i++) {
        var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[i]);
        if (index >= 0 && this.DataArray[index].statusId != DocumentStatusValue.NormalCheck) {
          conflit = true;
          break;
        }
      }

      if (conflit) {
        this.finalizedGroup = false;
        return true;
      } else {
        this.finalizedGroup = true;
        return false;
      }
    }


  }

  isDisabledUnFinalizedBtn = () => {
    if (this.selectedRows.length == 0)
      return true;
    else if (this.selectedRows.length == 1) {
      var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[0]);
      if (index >= 0 && this.DataArray[index].statusId == DocumentStatusValue.FinalCheck)
        return false;
      else
        return true;
    } else {

      let conflit: boolean = false;
      for (var i = 0; i < this.selectedRows.length; i++) {
        var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[i]);
        if (index >= 0 && this.DataArray[index].statusId != DocumentStatusValue.FinalCheck) {
          conflit = true;
          break;
        }
      }

      if (conflit) {
        this.returnFromFinalizedGroup = false;
        return true;
      } else {
        this.returnFromFinalizedGroup = true;
        return false;
      }

    }
  }

  isDisabledConfirmGroupBtn = () => {

    if (this.selectedRows.length == 1 || this.selectedRows.length == 0) {
      return true;
    } else {
      let conflit: boolean = false;

      for (var i = 0; i < this.selectedRows.length; i++) {
        var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[i]);
        if (index >= 0 && this.DataArray[index].statusId == DocumentStatusValue.NormalCheck) {

          if (
            !(
              (this.DataArray[index].confirmedById == null && this.DataArray[index].confirmerName != null) ||
              (this.DataArray[index].approvedById == null && this.DataArray[index].approverName != null)
            )
          ) {
            conflit = true;
            break;
          }
        }
        if (index >= 0 && this.DataArray[index].statusId != DocumentStatusValue.NormalCheck) {
          conflit = true;
          break;
        }
      }
      if (conflit) {
        return true;
      } else {
        return false;
      }
    }
  }
  isDisabledUnConfirmGroupBtn = () => {
    if (this.selectedRows.length == 1 || this.selectedRows.length == 0) {
      return true;
    } else {
      let conflit: boolean = false;

      for (var i = 0; i < this.selectedRows.length; i++) {
        var index = this.DataArray.findIndex(rd => rd.id === this.selectedRows[i]);
        if (index >= 0 && this.DataArray[index].statusId == DocumentStatusValue.NormalCheck) {
          if (this.DataArray[index].approvedById == null && this.DataArray[index].confirmedById == null) {
            conflit = true;
            break;
          }
        }
        if (index >= 0 && this.DataArray[index].statusId != DocumentStatusValue.NormalCheck) {
          conflit = true;
          break;
        }
      }
      if (conflit) {
        return true;
      } else {
        return false;
      }
    }
  }



  onCheckHandler() {
    if (this.groupOperation) {
      //ثبت گروهی
      this.grid.loading = true;
      let rowsId: Array<number> = [];
      this.selectedRows.forEach(item => {
        rowsId.push(item);
      })

      this.voucherService.changeVouchersStatus(VoucherApi.CheckVouchers, rowsId).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.grid.loading = false;
        this.showMessage(message, MessageType.Warning);
      }));
    }
    else {
      //ثبت تکی
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.CheckVoucher, this.selectedRows[0])).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }
  }

  onUnCheckHandler() {
    if (this.groupOperation) {
      //برگشت سند گروهی

      this.grid.loading = true;
      let rowsId: Array<number> = [];
      this.selectedRows.forEach(item => {
        rowsId.push(item);
      })

      this.voucherService.changeVouchersStatus(VoucherApi.UnDoCheckVouchers, rowsId).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.grid.loading = false;
        this.showMessage(message, MessageType.Warning);
      }));
    }
    else {
      //برگشت سند تکی
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.UndoCheckVoucher, this.selectedRows[0])).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }
  }

  onFinalizeHandler() {
    if (this.groupOperation) {
      //ثبت قطعی گروهی

      this.grid.loading = true;
      let rowsId: Array<number> = [];
      this.selectedRows.forEach(item => {
        rowsId.push(item);
      })

      this.voucherService.changeVouchersStatus(VoucherApi.FinalizeVouchers, rowsId).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.grid.loading = false;
        this.showMessage(message, MessageType.Warning);
      }));
    }
    else {
      //ثبت قطعی تکی
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.FinalizeVoucher, this.selectedRows[0])).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }
  }
  onUnFinalizedHandler() {

    if (this.groupOperation) {
      //برگشت از ثبت قطعی گروهی

      this.grid.loading = true;
      let rowsId: Array<number> = [];
      this.selectedRows.forEach(item => {
        rowsId.push(item);
      })

      this.voucherService.changeVouchersStatus(VoucherApi.UndoFinalizeVouchers, rowsId).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.grid.loading = false;
        this.showMessage(message, MessageType.Warning);
      }));
    }
    else {
      //ثبت قطعی تکی

      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.UndoFinalizeVoucher, this.selectedRows[0])).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }


  }

  onConfirmGroupHandler() {
    if (this.groupOperation) {
      // تایید گروهی

      this.grid.loading = true;
      let rowsId: Array<number> = [];
      this.selectedRows.forEach(item => {
        rowsId.push(item);
      })

      this.voucherService.changeVouchersStatus(VoucherApi.ConfirmGroupVouchers, rowsId).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
        this.DataArray = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.grid.loading = false;
        this.showMessage(message, MessageType.Warning);
      }));
    }
    else {
      return false;
    }
  }
  onUnConfirmGroupHandler() {
    this.UnConfirmGroupConfirm = true;
  }

  onSelectedKeysChange(e: any) {
    this.fillSelectedRowsData(e);
    if (this.selectedRows.length <= 1) {
      this.returnFromCommitGroup = false;
      this.finalizedGroup = false;
      this.commitGroup = false;
      this.returnFromFinalizedGroup = false;
      this.groupOperation = false;
    } else {
      this.groupOperation = true
    }

  }
  fillSelectedRowsData(e: any) {
    for (var i = 0; i < e.length; i++) {
      var result = this.DataArray.findIndex(rd => rd.id === this.selectedRows[i]);
      if (result == -1) {
        var indexRecord = this.rowData.data.findIndex(rd => rd.id === this.selectedRows[i]);
        var selectedRow = this.rowData.data[indexRecord];
        var voucherModel = {
          id: selectedRow.id,
          statusId: selectedRow.statusId,
          approvedById: selectedRow.approvedById,
          approverName: selectedRow.approverName,
          confirmedById: selectedRow.confirmedById,
          confirmerName: selectedRow.confirmerName,
          isApproved: selectedRow.isApproved,
          isVerified: selectedRow.isVerified,
        }
        this.DataArray.push(voucherModel)
      }
    }
    for (var i = 0; i < this.DataArray.length; i++) {
      var findIndex = e.findIndex(rd => rd === this.DataArray[i].id);

      if (findIndex == -1) {
        this.DataArray.splice(i, 1);
      }
    }

  }
  getBalanceValue(columnName: string, dataItem: any, dataType: string): any {
    var colName = columnName.charAt(0).toLowerCase() + columnName.slice(1);
    var res = dataItem[colName];
    var result = res;
    if (res == true) {
      result = "تراز"
    } else {
      result = " نا تراز "
    }
    return result;
  }

  getVerifidAndapproveValue(columnName: string, dataItem: any, dataType: string): any {
    var colName = columnName.charAt(0).toLowerCase() + columnName.slice(1);
    var res = dataItem[colName];
    var result = res;
    if (res == true) {
      result = "بلی"
    } else {
      result = " خیر "
    }
    return result;
  }



  changeParam() {

  }
}


