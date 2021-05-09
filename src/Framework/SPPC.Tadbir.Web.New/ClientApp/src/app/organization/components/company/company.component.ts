import { Component, OnInit, Renderer2, NgZone, ChangeDetectorRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { String, AutoGeneratedGridComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { CompanyService, CompanyDbInfo } from '@sppc/organization/service';
import { CompanyApi } from '@sppc/organization/service/api';
import { CompanyDb } from '@sppc/organization/models';
import { MetaDataService, GridService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { ViewName } from '@sppc/shared/security';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { CompanyFormComponent } from '@sppc/organization/components/company/company-form.component';
import { ReloadOption } from '@sppc/shared/class/reload-option';
import { ReloadStatusType } from '@sppc/shared/enum';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'company',
  templateUrl: './company.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class CompanyComponent extends AutoGeneratedGridComponent implements OnInit {


  errorMessage: string;
  //editDataItem?: CompanyDb = undefined;

  public dialogRef: DialogRef;
  public dialogModel: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private companyService: CompanyService, public dialogService: DialogService,
    public settingService: SettingService, public ngZone: NgZone) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.Company;
    this.viewId = ViewName[this.entityTypeName];

    this.getDataUrl = CompanyApi.Companies;
    this.reloadGrid();
    this.cdref.detectChanges();
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {
    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: CompanyFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.isNew = isNew;
    this.dialogModel.errorMessages = undefined;

    this.dialogRef.content.instance.save.subscribe((model) => {
      var serviceUrl = isNew ? CompanyApi.Companies : String.Format(CompanyApi.Company, model.id);
      this.saveHandler(model, isNew, this.companyService, serviceUrl);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (this.groupOperation) {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
    else {
      var recordId = this.selectedRows[0];
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.companyService.getById(String.Format(CompanyApi.Company, recordId)).subscribe(res => {
      this.editDataItem = res;      
      this.openEditorDialog(false);
      this.grid.loading = false;
    })
  }

  cancelHandler() {
    this.editDataItem = undefined;
    this.errorMessages = undefined;
  }
  /*
  saveHandler(model: CompanyDb, isNew: boolean) {

    if (!isNew) {
      this.companyService.edit<CompanyDb>(String.Format(CompanyApi.Company, model.id), model)
        .subscribe(() => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);

          this.dialogRef.close();
          this.dialogModel.errorMessages = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid();
          this.selectedRows = [];
        }, (error => {
          this.editDataItem = model;
          this.dialogModel.errorMessages = error;
        }));
    }
    else {
      this.companyService.insert<CompanyDb>(CompanyApi.Companies, model)
        .subscribe((response: any) => {
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;

          this.dialogRef.close();
          this.dialogModel.errorMessages = undefined;
          this.dialogModel.model = undefined;

          var options = new ReloadOption();
          options.InsertedModel = insertedModel;
          this.reloadGrid(options);

          this.selectedRows = [];
        }, (error => {
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

        this.companyService.groupDelete(CompanyApi.Companies, rowsId).subscribe(res => {
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
            this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);
        }));
      }
      else {
        this.grid.loading = true;
        this.companyService.delete(String.Format(CompanyApi.Company, this.deleteModelId)).subscribe(response => {
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
          //var message = error.message ? error.message : error;
            this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);
        }));

      }
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }

  addNew() {
    this.editDataItem = new CompanyDbInfo();
    this.openEditorDialog(true);
  }

  onAdvanceFilterOk() {
    this.reloadGrid();
  }

}
