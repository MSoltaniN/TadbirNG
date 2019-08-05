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

  isNew: boolean;
  errorMessage: string;

  editDataItem?: CompanyDb = undefined;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private companyService: CompanyService,
    public settingService: SettingService, public ngZone: NgZone, ) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.Company;
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

  editHandler(arg: any) {
    var recordId = this.selectedRows[0].id;
    this.grid.loading = true;
    this.companyService.getById(String.Format(CompanyApi.Company, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.grid.loading = false;
    })
    this.isNew = false;
    this.errorMessage = '';
  }

  cancelHandler() {
    this.editDataItem = undefined;
    this.isNew = false;
    this.errorMessage = '';
  }

  saveHandler(model: CompanyDb) {
    this.grid.loading = true;
    if (!this.isNew) {
      this.companyService.edit<CompanyDb>(String.Format(CompanyApi.Company, model.id), model)
        .subscribe(response => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
          this.selectedRows = [];
        }, (error => {
          this.grid.loading = false;
          this.errorMessage = error;
        }));
    }
    else {
      this.companyService.insert<CompanyDb>(CompanyApi.Companies, model)
        .subscribe((response: any) => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;
          this.reloadGrid(insertedModel);
          this.selectedRows = [];
        }, (error => {
          this.isNew = true;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
  }

  reloadGrid(insertedModel?: CompanyDb) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }


    if (insertedModel)
      this.goToLastPage(this.totalRecords);

    this.companyService.getAll(CompanyApi.Companies, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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

        this.companyService.groupDelete(CompanyApi.Companies, rowsId).subscribe(res => {
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
        this.grid.loading = true;
        this.companyService.delete(String.Format(CompanyApi.Company, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);
          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.selectedRows = [];
          this.reloadGrid();
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
    this.isNew = true;
    this.editDataItem = new CompanyDbInfo();
    this.errorMessage = '';
  }

}
