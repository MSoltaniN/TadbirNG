import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { CurrencyFormComponent } from './currency-form.component';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { MetaDataService, GridService, BrowserStorageService } from '@sppc/shared/services';
import { Currency } from '@sppc/finance/models';
import { CurrencyService, CurrencyInfo } from '@sppc/finance/service';
import { CurrencyApi } from '@sppc/finance/service/api';
import { SettingService } from '@sppc/config/service';
import { String, AutoGeneratedGridComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';
import { Router } from '@angular/router';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'currency',
  templateUrl: './currency.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class CurrencyComponent extends AutoGeneratedGridComponent implements OnInit {

  editDataItem?: Currency = undefined;

  public dialogRef: DialogRef;
  public dialogModel: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private currencyService: CurrencyService, public dialogService: DialogService,
    public settingService: SettingService, public ngZone: NgZone, public router: Router) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.Currency;
    this.viewId = ViewName[this.entityTypeName];

    this.reloadGrid();
    this.cdref.detectChanges();
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (this.groupOperation) {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
    else {
      var recordId = this.selectedRows[0].id;
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0].id;
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
        }));
    }
    else {
      this.currencyService.insert<Currency>(CurrencyApi.Currencies, model)
        .subscribe((response: any) => {
          this.dialogRef.close();
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;
          this.reloadGrid(insertedModel);
          this.selectedRows = [];

        }, (error => {
          this.grid.loading = false;
        }));
    }
  }

  reloadGrid(insertedModel?: Currency) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }


    if (insertedModel)
      this.goToLastPage(this.totalRecords);

    this.currencyService.getAll(CurrencyApi.Currencies, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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
        data: resData,
        total: totalCount
      }
      this.showloadingMessage = !(resData.length == 0);
      this.totalRecords = totalCount;

      this.grid.loading = false;
    })

  }

  deleteModel(confirm: boolean) {
    if (confirm) {
      if (this.groupOperation) {
        //حذف گروهی
        this.grid.loading = true;
        let rowsId: Array<number> = [];
        this.selectedRows.forEach(item => {
          rowsId.push(item.id);
        })

        this.currencyService.groupDelete(CurrencyApi.Currencies, rowsId).subscribe(res => {
          this.showMessage(this.deleteMsg, MessageType.Info);

          if (this.rowData.data.length == this.selectedRows.length && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.selectedRows = [];
          this.groupOperation = false;
          this.reloadGrid();

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

          this.reloadGrid();
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
    this.editDataItem = new CurrencyInfo();
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
    this.dialogModel.errorMessage = undefined;

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.saveHandler(res, isNew);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  exchangeRate() {
    var currencyId = this.selectedRows[0].id;
    this.router.navigate(['finance/currency-rate', currencyId]);
  }
}
