import { DefaultComponent } from '@sppc/shared/class/default.component';
import { Injectable, OnDestroy, Renderer2, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { GridService, BrowserStorageService, MetaDataService } from '../services';
import { SettingService } from '@sppc/config/service';
import { ServiceLocator } from "@sppc/service.locator";
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { AdvanceFilterComponent } from "@sppc/shared/components/advanceFilter/advance-filter.component";
import { Permissions, GlobalPermissions } from "@sppc/shared/security/permissions";
import { FilterExpression } from '@sppc/shared/class/filterExpression';
import { FilterRow } from "@sppc/shared/models";
import { MessageType } from '@sppc/env/environment';

@Injectable()
export class ListComponent extends DefaultComponent implements OnDestroy {

  advanceFilters: FilterExpression;
  advanceFilterList: Array<FilterRow>;  
  selectedGroupFilter: number; 

  dialogService: DialogService;
  permission: Permissions;
  filterDialogRef: DialogRef;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService,
    public renderer: Renderer2, public metadataService: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, bStorageService, renderer, metadataService, settingService, '', undefined);

    this.dialogService = ServiceLocator.injector.get(DialogService);
    this.permission = new Permissions();
  }


  showAdvanceFilterComponent() {
    debugger;
    (async () => {
      var entityName = await this.getEntityName(this.viewId);
      var code = <number>GlobalPermissions.Filter
      if (!this.isAccess(entityName, code)) {
        this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
        return;
      }

      this.filterDialogRef = this.dialogService.open({
        content: AdvanceFilterComponent,
        title: this.getText('AdvanceFilter.Title')
      });

      var filterDialogModel = <AdvanceFilterComponent>this.filterDialogRef.content.instance;
      if (this.advanceFilterList) {
        filterDialogModel.filters = this.advanceFilterList;
        filterDialogModel.gFilterSelected = this.selectedGroupFilter;
      }
      filterDialogModel.viewId = this.viewId;
      this.filterDialogRef.content.instance.cancel.subscribe((res) => {
        this.filterDialogRef.close();
        this.onAdvanceFilterCancel();
      });

      this.filterDialogRef.content.instance.result.subscribe((res) => {
        this.advanceFilters = res.filters;
        this.advanceFilterList = res.filterList;
        this.selectedGroupFilter = res.gFilterSelected;
        this.filterDialogRef.close();
        this.onAdvanceFilterOk();
      });

    })();
  }

  ngOnDestroy(): void {
    throw new Error("Method not implemented.");
  }

  /**این تابع بعد از ایونت دکمه اوکی فرم فیلتر پیشرفته فراخوانی میشود*/
  public onAdvanceFilterOk(): any {
    /*console.log('base onGenerateParameters')*/
  }

  /**این تابع بعد از ایونت دکمه کنسل فرم فیلتر پیشرفته فراخوانی میشود*/
  public onAdvanceFilterCancel(): any {
    /*console.log('base onGenerateParameters')*/
  }
}
