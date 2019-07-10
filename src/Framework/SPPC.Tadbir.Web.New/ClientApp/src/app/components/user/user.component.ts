import { Component, OnInit, Input, Renderer2, ViewChild } from '@angular/core';
import { UserService, UserInfo, RelatedItemsInfo, SettingService } from '../../service/index';
import { User, RelatedItems } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { UserApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { UserPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { ViewName } from '../../security/viewName';
import { BrowserStorageService } from '../../service/browserStorage.service';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'user',
  templateUrl: './user.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class UserComponent extends DefaultComponent implements OnInit {

  //#region Fields

  @ViewChild(GridComponent) grid: GridComponent;

  public rowData: GridDataResult;
  public selectedRows: number[] = [];
  public totalRecords: number;

  //permission flag
  viewAccess: boolean;

  //for add in delete messageText
  deleteModelId: number;

  currentFilter: FilterExpression;

  showloadingMessage: boolean = true;
  rolesList: boolean = false;

  editDataItem?: User = undefined;
  userRolesData: RelatedItemsInfo;
  isNew: boolean;
  errorMessage: string;
  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.User, UserPermissions.View);
    this.reloadGrid();
  }

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    //if (this.selectedRows.length > 1)
    //    this.groupDelete = true;
    //else
    //    this.groupDelete = false;
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    var isReload: boolean = false;
    if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
      isReload = true;

    this.currentFilter = this.getFilters(filter);
    if (isReload) {
      this.reloadGrid();
    }
  }

  public sortChange(sort: SortDescriptor[]): void {

    this.sort = sort.filter(f => f.dir != undefined);

    this.reloadGrid();
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.userService.getById(String.Format(UserApi.User, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.grid.loading = false;
    })
    this.isNew = false;
    this.errorMessage = '';
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.isNew = false;
    this.errorMessage = '';
  }

  public saveHandler(model: User) {
    this.grid.loading = true;
    if (!this.isNew) {
      this.userService.edit<User>(String.Format(UserApi.User, model.id), model)
        .subscribe(response => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
        }, (error => {
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
    else {
      this.userService.insert<User>(UserApi.Users, model)
        .subscribe((response: any) => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;
          this.reloadGrid(insertedModel);
        }, (error => {
          this.isNew = true;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
  }

  public rolesHandler(userId: number) {
    this.rolesList = true;
    this.grid.loading = true;
    this.userService.getUserRoles(userId).subscribe(res => {
      this.userRolesData = res;
      this.grid.loading = false;
    });

    this.errorMessage = '';
  }

  public cancelUserRolesHandler() {
    this.rolesList = false;
    this.errorMessage = '';
  }

  public saveUserRolesHandler(userRoles: RelatedItems) {
    this.grid.loading = true;
    this.userService.modifiedUserRoles(userRoles)
      .subscribe(response => {
        this.rolesList = false;
        this.showMessage(this.getText("User.UpdateRoles"), MessageType.Succes);
        this.grid.loading = false;
      }, (error => {
        this.grid.loading = false;
        this.errorMessage = error;
      }));
  }

  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService, public bStorageService: BrowserStorageService,
    private userService: UserService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.User, ViewName.User);
  }
  //#endregion

  //#region Methods

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: User) {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      this.userService.getAll(String.Format(UserApi.Users, this.FiscalPeriodId, this.BranchId), this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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
    else {
      this.rowData = {
        data: [],
        total: 0
      }
    }

  }

  public addNew() {
    this.isNew = true;
    this.editDataItem = new UserInfo();
    this.errorMessage = '';
  }
  //#endregion
}


