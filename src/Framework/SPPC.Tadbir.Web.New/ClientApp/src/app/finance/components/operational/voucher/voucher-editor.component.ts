import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { String, DetailComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { VoucherService, VoucherInfo, InventoryBalanceInfo } from '@sppc/finance/service';
import { VoucherApi } from '@sppc/finance/service/api';
import { Voucher } from '@sppc/finance/models';
import { MetaDataService, BrowserStorageService, LookupService } from '@sppc/shared/services';
import { DocumentStatusValue, VoucherOperations } from '@sppc/finance/enum';
import { ViewName } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';
import { Item } from '@sppc/shared/models';
import { InventoryBalance } from '@sppc/finance/models/inventoryBalance';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'voucher-editor',
  templateUrl: './voucher-editor.component.html',
  styles: [`
.voucher-form-content {margin-top:5px; border: solid 1px #3c8dbc; padding: 7px 10px 0;}
input[type=text], textarea, .ddl-type { width: 100%; }
.voucher-status-item{ display: inline; margin: 0 10px;}

/deep/.dialog-padding .k-window-content {padding:15px !important}

.col-xs-5ths,
.col-sm-5ths,
.col-md-5ths,
.col-lg-5ths,
.col-sm-4-5ths{
  position: relative;
  min-height: 1px;
  padding-right: 15px;
  padding-left: 15px;
}

.col-xs-5ths {
  width: 20%;
  float: left;
}

@media (min-width: 768px) {
  .col-sm-5ths {
    width: 20%;
    float: left;
  }
  .col-sm-4-5ths{
    width: 80%;
    float: left;
  }
}

@media (min-width: 992px) {
  .col-md-5ths {
    width: 20%;
    float: left;
  }
}

@media (min-width: 1200px) {
  .col-lg-5ths {
    width: 20%;
    float: left;
  }
}



`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class VoucherEditorComponent extends DetailComponent implements OnInit {

  errorMessage: string;
  voucherModel: Voucher;
  voucherTypeList: Array<Item> = [];
  selectedType: string;
  deleteConfirm: boolean;

  @Input() voucherItem: Voucher;
  @Input() isOpenFromList: boolean = false;
  //@Output() reloadGrid: EventEmitter<any> = new EventEmitter();

  isShowBreadcrumb: boolean = true;
  isFirstVoucher: boolean = false;
  isLastVoucher: boolean = false;
  voucherOperationsItem: any;
  deleteConfirmMsg: string;

  constructor(private voucherService: VoucherService, public toastrService: ToastrService, public translate: TranslateService, private activeRoute: ActivatedRoute,
    public renderer: Renderer2, public metadata: MetaDataService, public router: Router, private dialogService: DialogService, private lookupService: LookupService,
    public bStorageService: BrowserStorageService) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Voucher, ViewName.Voucher);

    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }


  ngOnInit() {
    this.voucherOperationsItem = VoucherOperations;

    this.editForm.reset();

    if (this.voucherItem) {
      this.initVoucherForm(this.voucherItem);
      this.isShowBreadcrumb = false;
    }
    else {
      this.activeRoute.params.subscribe(params => {
        switch (params['mode']) {
          case "new": {
            this.newVoucher();
            break;
          }
          case "last": {
            this.isLastVoucher = true;
            this.getVoucher(VoucherApi.LastVoucher);
            break;
          }
          case "by-no": {
            this.byNoVoucher();
            break;
          }
          case "first": {
            this.isFirstVoucher = true;
            this.getVoucher(VoucherApi.FirstVoucher);
            break
          }
          case "next": {
            var voucherNo = this.activeRoute.snapshot.queryParamMap.get('no')
            if (voucherNo)
              this.getVoucher(String.Format(VoucherApi.NextVoucher, voucherNo), true);
            break
          }
          case "previous": {
            var voucherNo = this.activeRoute.snapshot.queryParamMap.get('no')
            if (voucherNo)
              this.getVoucher(String.Format(VoucherApi.PreviousVoucher, voucherNo), true);
            break
          }
          case "opening-voucher": {                      
            var voucherNo = this.activeRoute.snapshot.queryParamMap.get('no');
            if (voucherNo) {
              this.getVoucher(String.Format(VoucherApi.VoucherByNo, voucherNo), true);
            }
            else {
              this.openingVoucherQuery();
            }
            break;
          }
          case "closing-voucher": {
            this.getVoucher(VoucherApi.ClosingVoucher);
            break;
          }
          case "close-temp-accounts":
            {
              if (this.InventoryMode == 0) {
                var voucherNo = this.activeRoute.snapshot.queryParamMap.get('no');
                if (voucherNo) {
                  this.getVoucher(String.Format(VoucherApi.VoucherByNo, voucherNo), true);
                }
                else {
                  this.checkClosingTmp();
                }
              }
              else {
                this.closingTmpOnInventoryMode1();
              }
            break;
          }         
            
          default: {
            this.isShowBreadcrumb = false;
            this.newVoucher();
          }
        }
      })
    }

    this.getVoucherType();
  }

  openingVoucherQuery() {
    this.voucherService.getOpeningVoucherQuery().subscribe(result => {
      if (result == true) {        
        //show confirm box
        this.router.navigate(['/tadbir/home'], { queryParams: { returnUrl: 'finance/vouchers/opening-voucher', mode: 'opening-voucher' } });
      }
      else if (result == false) {
        //create opening voucher
        this.getVoucher(VoucherApi.OpeningVoucher);
      }
      else {
        this.initVoucherForm(result);
      }

    });
  }

  /**
  * ساخت سند بستن حسابها براساس سیستم دایمی  
  */
  closingTmpOnInventoryMode1() {    
    this.voucherService.getClosingAccountsVoucherMode1().subscribe(result => {
      var voucherNo = result.no;
      if (voucherNo) {
        this.getVoucher(String.Format(VoucherApi.VoucherByNo, voucherNo), true);
      }
    });
  }

  checkClosingTmp() {
    var bodyItem = new Array<InventoryBalance>();
    var item = new InventoryBalanceInfo();       

    this.voucherService.getClosingAccountsVoucher(bodyItem).subscribe(result => {
      if (result) {
        //closingAccount created and show voucher
        //this.initVoucherForm(result);
        this.router.navigate(['/tadbir/home'], { queryParams: { returnUrl: 'finance/vouchers/close-temp-accounts', mode: 'closing-tmp' } });
      }
      else {
        //closingAccount not created and show popup
        this.router.navigate(['/tadbir/home'], { queryParams: { returnUrl: 'finance/vouchers/close-temp-accounts',mode:'closing-tmp' } });
      }
    });

  }


  newVoucher() {
    this.getVoucher(VoucherApi.NewVoucher);
  }

  getNewVoucher() {
    if (this.voucherItem || this.isOpenFromList)
      this.getVoucher(VoucherApi.NewVoucher);
    else {
      this.redirectTo('/finance/vouchers/new')
    }
  }

  redirectTo(uri) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate([uri]));
  }

  byNoVoucher() {
    var voucherNo = this.activeRoute.snapshot.queryParamMap.get('no');

    if (!voucherNo) {
      this.router.navigate(['/tadbir/home'], { queryParams: { returnUrl: 'finance/vouchers/by-no',mode: 'by-no' } });
    }
    else {
      this.getVoucher(String.Format(VoucherApi.VoucherByNo, voucherNo), true);
    }

  }

  getVoucher(apiUrl: string, byNo: boolean = false) {
    this.voucherService.getModels(apiUrl).subscribe(res => {

      this.initVoucherForm(res);
      this.errorMessage = undefined;
    },
      err => {
        if (err.status == 404) {
          this.showMessage(this.getText("Voucher.VoucherNotFound"), MessageType.Warning);
          if (byNo)
            this.router.navigate(['/tadbir/home'], { queryParams: { returnUrl: 'finance/vouchers/by-no', mode: 'by-no' } });
        }

        if (err.status == 400) {
          this.showMessage(this.getText(err.error.value), MessageType.Warning);
          this.router.navigate(['/finance/voucher']);
        }
      })
  }

  initVoucherForm(item: Voucher) {

    this.editForm.reset(item);

    this.voucherModel = item;
    this.selectedType = this.voucherModel.type.toString();
  }

  getVoucherType() {
    this.lookupService.getModels(LookupApi.VoucherSysTypes).subscribe(res => {
      this.voucherTypeList = res;
    })
  }

  onSave(e?: any): void {
    if (this.editForm.valid) {

      let model: Voucher = this.editForm.value;
      model.branchId = this.BranchId;
      model.fiscalPeriodId = this.FiscalPeriodId;
      model.statusId = this.voucherModel.statusId;
      model.saveCount = this.voucherModel.saveCount;
      if (model.reference == '')
        model.reference = null;

      this.voucherService.edit<Voucher>(String.Format(VoucherApi.Voucher, model.id), model).subscribe(res => {
        this.editForm.reset(res);
        this.voucherModel = res;
        this.errorMessage = undefined;
        this.showMessage(this.updateMsg, MessageType.Succes);
      }, (error => {
        if (e)
          this.errorMessage = error;
        else
          this.showMessage(error, MessageType.Warning);
      }));

    }
  }

  nextVoucher() {
    if (this.voucherItem || this.isOpenFromList) {
      this.getVoucher(String.Format(VoucherApi.NextVoucher, this.voucherModel.no));
      this.isFirstVoucher = false;
      this.isLastVoucher = false;
    }
    else
      this.router.navigate(['/finance/vouchers/next'], { queryParams: { no: this.voucherModel.no } });
  }

  previousVoucher() {
    if (this.voucherItem || this.isOpenFromList) {
      this.getVoucher(String.Format(VoucherApi.PreviousVoucher, this.voucherModel.no));
      this.isFirstVoucher = false;
      this.isLastVoucher = false;
    }
    else
      this.router.navigate(['/finance/vouchers/previous'], { queryParams: { no: this.voucherModel.no } });
  }

  firstVoucher() {
    if (this.voucherItem || this.isOpenFromList) {
      this.getVoucher(VoucherApi.FirstVoucher);
      this.isFirstVoucher = true;
      this.isLastVoucher = false;
    }
    else
      this.router.navigate(['/finance/vouchers/first']);
  }

  lastVoucher() {
    if (this.voucherItem || this.isOpenFromList) {
      this.getVoucher(VoucherApi.LastVoucher);
      this.isFirstVoucher = false;
      this.isLastVoucher = true;
    }
    else
      this.router.navigate(['/finance/vouchers/last']);
  }

  searchVoucher() {
    this.router.navigate(['/tadbir/home'], { queryParams: { returnUrl: 'finance/vouchers/by-no', mode: 'by-no' } });
  }

  checkHandler() {
    var apiUrl = String.Format(this.voucherModel.statusId == DocumentStatusValue.Draft ? VoucherApi.CheckVoucher : VoucherApi.UndoCheckVoucher, this.voucherModel.id);

    this.voucherService.changeVoucherStatus(apiUrl).subscribe(res => {

      this.voucherModel.statusId = this.voucherModel.statusId == DocumentStatusValue.Draft ? DocumentStatusValue.NormalCheck : DocumentStatusValue.Draft;

      //this.reloadGrid.emit();

    }, (error => {
      var message = error.message ? error.message : error;
      this.showMessage(message, MessageType.Warning);
    }));

  }

  voucherOperation(item: VoucherOperations) {
    var model1 = new VoucherInfo();
    var model2 = new VoucherInfo();

    model1.no = parseInt(this.editForm.value.no);
    model1.reference = this.editForm.value.reference;
    model1.dailyNo = parseInt(this.editForm.value.dailyNo);
    model1.association = this.editForm.value.association;
    model1.date = this.getDate(this.editForm.value.date);
    model1.description = this.editForm.value.description;
    model1.type = parseInt(this.editForm.value.type);

    model2.no = this.voucherModel.no;
    model2.reference = this.voucherModel.reference;
    model2.dailyNo = this.voucherModel.dailyNo;
    model2.association = this.voucherModel.association;
    model2.date = this.getDate(this.voucherModel.date);
    model2.description = this.voucherModel.description;
    model2.type = this.voucherModel.type;

    var isFormDataChenged = true;
    if (JSON.stringify(model2) === JSON.stringify(model1))
      isFormDataChenged = false;

    if (isFormDataChenged) {

      const dialog: DialogRef = this.dialogService.open({
        title: this.getText('Entity.Voucher'),
        content: this.getText('Voucher.SaveChanges'),
        actions: [
          { text: this.getText('Buttons.Yes'), mode: 1, primary: true },
          { text: this.getText('Buttons.No'), mode: 0 }
        ],
        width: 450,
        height: 150,
        minWidth: 250
      });

      dialog.dialog.location.nativeElement.classList.add('dialog-padding');

      dialog.result.subscribe((result) => {
        let res: any = result;
        if (res.mode == 1)
          this.onSave();

        this.executeVoucherOperation(item);

      });

    }
    else {
      this.executeVoucherOperation(item);
    }

  }

  getDate(date: Date): Date {
    var date = new Date(date)
    return new Date(date.getFullYear(), date.getMonth(), date.getDate());
  }

  executeVoucherOperation(item: VoucherOperations) {
    switch (item) {
      case VoucherOperations.First: {
        this.firstVoucher();
        break;
      }
      case VoucherOperations.Last: {
        this.lastVoucher();
        break;
      }
      case VoucherOperations.New: {
        this.getNewVoucher();
        break;
      }
      case VoucherOperations.Next: {
        this.nextVoucher();
        break;
      }
      case VoucherOperations.Previous: {
        this.previousVoucher();
        break;
      }
      case VoucherOperations.Search: {
        this.searchVoucher();
        break;
      }
      case VoucherOperations.CheckVoucher: {
        this.checkHandler();
        break;
      }
      default:
    }
  }

  removeHandler() {
    this.deleteConfirm = true;   
    this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));    
  }

  deleteModel(confirm: boolean) {
    if (confirm) {     
      this.voucherService.delete(String.Format(VoucherApi.Voucher, this.voucherModel.id)).subscribe(response => {
        this.showMessage(this.getText('Messages.DeleteOperationSuccessful'), MessageType.Info);
        
        //try for next voucher
        this.voucherService.getModels(String.Format(VoucherApi.NextVoucher, this.voucherModel.no)).subscribe(voucher => {
          if (voucher) {
            this.router.navigate(['/finance/vouchers/by-no'], { queryParams: { no: voucher.no } });
          }
          else {
            
          }
        }, (error => {
              //if next voucher not exists try for previous voucher
              this.voucherService.getModels(String.Format(VoucherApi.PreviousVoucher, this.voucherModel.no)).subscribe(voucher => {
                if (voucher) {
                  this.router.navigate(['/finance/vouchers/by-no'], { queryParams: { no: voucher.no } });
                }                
              }, (error => {
                  //if previous voucher not exists show voucher list
                  this.router.navigate(['/finance/voucher']);
              }))
        }));

        }, (error => {          
          var message = error.message ? error.message : error;
          this.showMessage(message, MessageType.Warning);
        }));      
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }

  /**
   * prepare confim message for delete operation
   * @param text is a part of message that use for delete confirm message
  */
  public prepareDeleteConfirm(text: string) {
    this.translate.get("Messages.VoucherDeleteConfirm").subscribe((msg: string) => {
      this.deleteConfirmMsg = String.Format(msg, text);
    });
  }

}


