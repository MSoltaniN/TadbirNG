import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone, ViewChild, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { CurrencyFormComponent } from './currency-form.component';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { MetaDataService, GridService, BrowserStorageService } from '@sppc/shared/services';
import { Currency } from '@sppc/finance/models';
import { CurrencyService, CurrencyEntity } from '@sppc/finance/service';
import { CurrencyApi } from '@sppc/finance/service/api';
import { SettingService } from '@sppc/config/service';
import { String, AutoGeneratedGridComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';
import { Router } from '@angular/router';
import { HttpEventType } from '@angular/common/http';
import { ViewIdentifierComponent, ReportViewerComponent } from '@sppc/shared/components';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { GridComponent } from '@progress/kendo-angular-grid';
import { ReloadStatusType } from '@sppc/shared/enum';
import { ReloadOption } from '@sppc/shared/class/reload-option';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'currency',
  templateUrl: './currency.component.html',
  styles: [`
input[type="file"] {
    display: none;
}
.custom-file-upload {
    border: 1px solid #e2e2e2;
    display: inline-block;
    padding: 5px 2px 2px;
    cursor: pointer;
    position: absolute;
    top: 9px;
    width: 29px;
    height: 29px;
    margin-right: 10px;
}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class CurrencyComponent extends AutoGeneratedGridComponent implements OnInit {

  //editDataItem?: Currency = undefined;

  public dialogRef: DialogRef;
  public dialogModel: any;

  progress: number = 0;
  @ViewChild('myInput') myInputVariable: ElementRef;
  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private currencyService: CurrencyService, public dialogService: DialogService,
    public settingService: SettingService, public ngZone: NgZone, public router: Router) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.Currency;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = CurrencyApi.Currencies;
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (this.groupOperation) {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
    else {
      var recordId = this.selectedRows[0];//.id;
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];//.id;
    this.grid.loading = true;
    this.currencyService.getById(String.Format(CurrencyApi.Currency, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.openEditorDialog(false);
      this.grid.loading = false;
    })
  }

  public cancelHandler() {
    this.editDataItem = undefined;
  }
  /*
  public saveHandler(model: Currency, isNew: boolean) {
    this.grid.loading = true;
    if (!isNew) {
      this.currencyService.edit<Currency>(String.Format(CurrencyApi.Currency, model.id), model)
        .subscribe(response => {
          this.dialogRef.close();
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
          this.selectedRows = [];
        }, (error => {
          this.grid.loading = false;
          this.dialogModel.errorMessages = error;
        }));
    }
    else {
      this.currencyService.insert<Currency>(CurrencyApi.Currencies, model)
        .subscribe((response: any) => {
          this.dialogRef.close();
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;

          var options = new ReloadOption();
          options.InsertedModel = insertedModel;
          this.reloadGrid(options);

          this.selectedRows = [];

        }, (error => {
          this.grid.loading = false;
          this.dialogModel.errorMessages = error;
        }));
    }
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

        this.currencyService.groupDelete(CurrencyApi.Currencies, rowsId).subscribe(res => {
          this.showMessage(this.deleteMsg, MessageType.Info);

          if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;
                    
          this.groupOperation = false;

          var options = new ReloadOption();
          options.Status = ReloadStatusType.AfterDelete;
          this.reloadGrid(options);

          this.selectedRows = [];

        }, (error => {
          this.grid.loading = false;
          this.showMessage(error, MessageType.Warning);
        }));
      }
      else {
        //حذف تکی
        this.grid.loading = true;
        this.currencyService.delete(String.Format(CurrencyApi.Currency, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);
          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          var options = new ReloadOption();
          options.Status = ReloadStatusType.AfterDelete;
          this.reloadGrid(options);

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

  public addNew() {
    this.editDataItem = new CurrencyEntity();
    this.openEditorDialog(true);
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {


    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: CurrencyFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessages = undefined;

    this.dialogRef.content.instance.save.subscribe((model) => {      
      var serviceUrl = isNew ? CurrencyApi.Currencies : String.Format(CurrencyApi.Currency, model.id);
      this.saveHandler(model, isNew, this.currencyService, serviceUrl);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  exchangeRate() {
    var currencyId = this.selectedRows[0];//.id;
    this.router.navigate(['finance/currency-rate', currencyId]);
  }

  onFileChange(event: any) {

    if (event.target.files && event.target.files.length > 0) {
      let file = event.target.files[0];
      var fileExtension = file.name.split('.').pop();
      var accessExtensions = ["accda", "accdb", "accde", "accdr", "accdt", "mdb", "mde", "mdf", "mda"];
      if (accessExtensions.filter(f => f == fileExtension.toLowerCase()).length > 0) {

        this.currencyService.postFile(file).subscribe(res => {
          this.myInputVariable.nativeElement.value = "";

          if (res.type === HttpEventType.UploadProgress)
            this.progress = Math.round(100 * res.loaded / res.total);
          else
            if (res.type === HttpEventType.Response) {
              this.showMessage(this.getText("Messages.UploadSuccessful"), MessageType.Succes);

            }
        })
      }
      else {
        this.showMessage(this.getText("Messages.IncorrectFileFormat"), MessageType.Warning);
      }

    }
  }
}
