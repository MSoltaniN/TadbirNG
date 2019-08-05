import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService } from '@progress/kendo-angular-dialog';
import { BranchFormComponent } from './branch-form.component';
import { BranchRolesFormComponent } from './branch-roles-form.component';
import { String, AutoGridExplorerComponent, Filter, FilterExpressionOperator } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { Branch, BranchService, BranchApi, BranchInfo } from '@sppc/organization';
import { GridService, BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { ViewName } from '@sppc/shared/security';
import { RelatedItems } from '@sppc/shared/models';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'branch',
  templateUrl: './branch.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class BranchComponent extends AutoGridExplorerComponent<Branch> implements OnInit {

  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public branchService: BranchService, public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, bStorageService, Entities.Branch,
      "Branch.LedgerBranch", "", "",
      BranchApi.Branches, BranchApi.RootBranches, BranchApi.Branch, BranchApi.BranchChildren,
      "", cdref, ngZone)
  }

  ngOnInit() {
    this.entityName = Entities.Branch;
    this.viewId = ViewName[this.entityTypeName];
    
    this.getTreeNode();
    this.reloadGrid();
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {
    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: BranchFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.parent = this.parent;
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


  addNew() {
    this.editDataItem = new BranchInfo();
    this.openEditorDialog(true);
  }

  reloadGrid(insertedModel?: any) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }

    if (insertedModel)
      this.goToLastPage(this.totalRecords);

    var parent_Id = this.parentId ? this.parentId.toString() : "null";
    filter = this.addFilterToFilterExpression(
      this.currentFilter,
      parent_Id ? new Filter("ParentId", parent_Id, "== {0}", "System.Int32") : new Filter("ParentId", "", "== null", ""),
      FilterExpressionOperator.And);

    this.currentFilter = filter;

    this.service.getAll(String.Format(BranchApi.CompanyBranches, this.CompanyId), this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {

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

  rolesHandler() {
    var branchId = this.selectedRows[0].id;

    this.dialogRef = this.dialogService.open({
      title: this.getText('Branch.RolesTitle'),
      content: BranchRolesFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.branchId = branchId;
    this.dialogModel.errorMessage = undefined;

    this.dialogRef.content.instance.saveBranchRoles.subscribe((res) => {
      this.saveBranchRolesHandler(res);
    });

    const closeForm = this.dialogRef.content.instance.cancelBranchRoles.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  saveBranchRolesHandler(userRoles: RelatedItems) {
    this.branchService.modifiedBranchRoles(userRoles)
      .subscribe(response => {
        this.showMessage(this.getText("Branch.UpdateRoles"), MessageType.Succes);
        this.dialogRef.close();
        this.dialogModel.branchId = undefined;
        this.selectedRows = [];
      }, (error => {
        this.dialogModel.errorMessage = error;
      }));
  }
}


