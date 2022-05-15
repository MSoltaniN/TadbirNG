import { Component, OnInit, Renderer2 } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { RTL } from "@progress/kendo-angular-l10n";
import { ViewRowPermission } from "@sppc/admin/models";
import {
  Item,
  ItemInfo,
  RowPermissionsForRoleInfo,
  ViewRowPermissionInfo,
  ViewRowPermissionService,
} from "@sppc/admin/service";
import { RoleApi } from "@sppc/admin/service/api";
import { SettingService } from "@sppc/config/service";
import { DefaultComponent, String } from "@sppc/shared/class";
import { PermissionType } from "@sppc/shared/enum";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import {
  BrowserStorageService,
  ErrorHandlingService,
  MetaDataService,
} from "@sppc/shared/services";
import { LookupApi } from "@sppc/shared/services/api";
import { ToastrService } from "ngx-toastr";
import "rxjs/Rx";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "accountRelations",
  templateUrl: "./viewRowPermission.component.html",
  styles: [
    `
      .input-frm {
        width: 100%;
      }
      .input-frm-btn {
        width: calc(100% - 125px);
      }
      #permission-type-panle {
        height: 150px;
        border: solid 1px #337ab7;
        margin-top: 20px;
      }
      .panel-type-btn {
        padding: 40px 25px 40px 10px;
      }
      .panel-type,
      .panel-type-multiple {
        padding: 40px 25px;
      }
      @media screen and (max-width: 768px) {
        .panel-type-multiple {
          padding: 15px 0;
        }
      }
    `,
  ],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class ViewRowPermissionComponent
  extends DefaultComponent
  implements OnInit
{
  public isActiveSingleForm: boolean = false;
  public isActiveMultipleForm: boolean = false;
  public isChangeMultipleForm: boolean = false;
  //public errorMessage = String.Empty;

  public dataItem: RowPermissionsForRoleInfo;
  public dataRowPermission: ViewRowPermissionInfo | undefined;
  public view_Id: number = -1;

  //public lableTitle: string;
  public rolesArray: Array<Item>;
  public ddlRolesData: Array<Item>;

  public ddlPermissionTypeData: Array<Item>;

  public ddlSelectedRole: number = 0;
  public ddlPermissionTypeSelected: number | undefined;

  public entity: ItemInfo | undefined;
  public singleFormSelectedValue: string;
  public singleFormSelectedModel: ItemInfo | undefined;
  public multipleFormItemsSelected: number[];

  permissionValue3: string;
  permissionValue4: string;
  permissionValue5: string;
  permissionValue6: string;
  numberValue: number;
  numberValue1: number;
  numberValue2: number;

  oldSingleFormSelectedModel: ItemInfo | undefined;

  public ngOnInit(): void {}

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    private viewRowPermissionService: ViewRowPermissionService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    public errorHandlingService: ErrorHandlingService
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      settingService,
      Entities.RowAccess,
      undefined
    );

    this.getRoles();
  }

  onPermissionChange(item: any) {
    let selectedPermission = "";
    Object.keys(PermissionType).forEach((key) => {
      if (parseInt(item) == parseInt(key))
        selectedPermission = PermissionType[key];
    });

    if (this.view_Id > -1) {
      let rowPermissionsArray: Array<ViewRowPermissionInfo> =
        this.dataItem.rowPermissions;
      var rowPermissionItem = rowPermissionsArray.find(
        (f) => f.viewId == this.view_Id && f.accessMode == selectedPermission
      );
    }

    if (rowPermissionItem)
      this.updateLabelText(rowPermissionItem.items.length, rowPermissionItem);
    else {
      this.permissionValue3 = "سطری انتخاب نشده";
      this.permissionValue4 = "سطری انتخاب نشده";
    }
  }

  handleRoleChange(item: any) {
    if (item) {
      this.viewRowPermissionService
        .getById(String.Format(RoleApi.RowAccessSettings, item))
        .subscribe((res) => {
          this.dataItem = res;
          this.singleFormSelectedValue = "";
          this.singleFormSelectedModel = undefined;
          this.ddlPermissionTypeSelected = 0;

          this.permissionValue3 = String.Empty;
          this.permissionValue4 = String.Empty;
          this.permissionValue5 = String.Empty;
          this.permissionValue6 = String.Empty;
          this.numberValue = 0;
          this.numberValue1 = 0;
          this.numberValue2 = 0;

          this.multipleFormItemsSelected = [];
        });
    }
  }

  getPermissions(viewId) {
    this.viewRowPermissionService
      .getAll(String.Format(LookupApi.ValidRowPermissions, viewId))
      .subscribe((res) => {
        var data = <Array<any>>res.body;
        this.ddlPermissionTypeData = data.map(function (item) {
          let itemInfo = new ItemInfo();
          itemInfo.key = parseInt(item.value.toString());
          itemInfo.value = item.key.toString();

          return itemInfo;
        });
      });
  }

  getRoles() {
    //this.sppcLoading.show();
    this.viewRowPermissionService.getAll(LookupApi.Roles).subscribe((res) => {
      var data = res.body;
      this.rolesArray = data;
      this.ddlRolesData = data;
      //this.sppcLoading.hide();
    });
  }

  handleFilter(value: any) {
    this.ddlRolesData = this.rolesArray.filter(
      (s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }

  openSingleForm() {
    this.errorMessages = undefined;
    this.updateDataItem();
    this.oldSingleFormSelectedModel = this.singleFormSelectedModel;
    this.isActiveSingleForm = true;
  }

  cancelSingleFormHandler() {
    this.errorMessages = undefined;
    this.isActiveSingleForm = false;
  }

  updateLabelText(itemCounts: number, rowPermission: ViewRowPermission) {
    if (itemCounts > 0) {
      if (rowPermission.accessMode == "SpecificRecords") {
        this.permissionValue3 = "سطرهای انتخاب شده";
      }

      if (rowPermission.accessMode == "AllExceptSpecificRecords") {
        this.permissionValue4 = "سطرهای انتخاب شده";
      }
    } else {
      if (rowPermission.accessMode == "SpecificRecords") {
        this.permissionValue3 = "سطری انتخاب نشده";
      }

      if (rowPermission.accessMode == "AllExceptSpecificRecords") {
        this.permissionValue4 = "سطری انتخاب نشده";
      }
    }
  }

  saveSingleFormHandler(model: Item) {
    this.singleFormSelectedModel = model;
    this.singleFormSelectedValue = model.value;
    if (
      this.oldSingleFormSelectedModel &&
      this.oldSingleFormSelectedModel != this.singleFormSelectedModel
    ) {
      var oldRowPermission = this.dataItem.rowPermissions.find(
        (f) => f.viewId == this.oldSingleFormSelectedModel.key
      );
      oldRowPermission.accessMode = "Default";
    }

    this.view_Id = model.key;
    var rowPermission = this.dataItem.rowPermissions.find(
      (f) => f.viewId == this.view_Id
    );

    this.getPermissions(this.view_Id);

    if (rowPermission) {
      switch (rowPermission.accessMode) {
        case "Default": {
          this.ddlPermissionTypeSelected = PermissionType.Default;
          break;
        }
        case "AllRecordsCreatedByUser": {
          this.ddlPermissionTypeSelected =
            PermissionType.AllRecordsCreatedByUser;
          break;
        }
        case "SpecificRecords": {
          this.ddlPermissionTypeSelected = PermissionType.SpecificRecords;
          this.updateLabelText(rowPermission.items.length, rowPermission);
          break;
        }
        case "AllExceptSpecificRecords": {
          this.ddlPermissionTypeSelected =
            PermissionType.AllExceptSpecificRecords;
          this.updateLabelText(rowPermission.items.length, rowPermission);
          break;
        }
        case "SpecificReference": {
          this.ddlPermissionTypeSelected = PermissionType.SpecificReference;
          this.permissionValue5 = rowPermission.textValue;
          break;
        }
        case "AllExceptSpecificReference": {
          this.ddlPermissionTypeSelected =
            PermissionType.AllExceptSpecificReference;
          this.permissionValue6 = rowPermission.textValue;
          break;
        }
        case "MaxMoneyValue": {
          this.ddlPermissionTypeSelected = PermissionType.MaxMoneyValue;
          this.numberValue1 = rowPermission.value;
          this.numberValue2 = rowPermission.value2;
          break;
        }
        case "MaxQuantityValue": {
          this.ddlPermissionTypeSelected = PermissionType.MaxQuantityValue;
          this.numberValue = rowPermission.value;
          break;
        }
        default:
      }
    }
    this.isActiveSingleForm = false;
  }

  openMultipleForm() {
    let selectedPermission = "";
    Object.keys(PermissionType).forEach((key) => {
      if (this.ddlPermissionTypeSelected == parseInt(key))
        selectedPermission = PermissionType[key];
    });

    this.errorMessages = undefined;
    this.entity = this.singleFormSelectedModel;
    var row = this.dataItem.rowPermissions.find(
      (f) => f.viewId == this.view_Id && f.accessMode == selectedPermission
    );
    if (row) {
      this.dataRowPermission = row;
    } else {
      const rowPermission = new ViewRowPermissionInfo();
      rowPermission.viewId = this.view_Id;
      this.dataRowPermission = rowPermission;
    }

    this.isActiveMultipleForm = true;
  }

  cancelMultipleFormHandler() {
    this.errorMessages = undefined;
    this.entity = undefined;
    this.dataRowPermission = undefined;
    this.isActiveMultipleForm = false;
  }

  saveMultipleFormHandler(items: number[]) {
    this.multipleFormItemsSelected = items;
    //this.isChangeMultipleForm = true;
    this.updateDataItem();

    if (this.view_Id > -1) {
      let rowPermissionsArray: Array<ViewRowPermissionInfo> =
        this.dataItem.rowPermissions;
      var rowPermissionItem = rowPermissionsArray.find(
        (f) => f.viewId == this.view_Id
      );
    }

    this.updateLabelText(items.length, rowPermissionItem);

    this.isActiveMultipleForm = false;
  }

  saveRowPermission() {
    this.errorMessages = undefined;
    this.updateDataItem();
    this.viewRowPermissionService
      .edit<RowPermissionsForRoleInfo>(
        String.Format(RoleApi.RowAccessSettings, this.ddlSelectedRole),
        this.dataItem
      )
      .subscribe(
        (res) => {
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.oldSingleFormSelectedModel = undefined;
          this.singleFormSelectedModel = undefined;
          this.singleFormSelectedValue = "";
          this.ddlPermissionTypeSelected = 0;
        },
        (error) => {
          if (error)
            this.errorMessages = this.errorHandlingService.handleError(error);
        }
      );
  }

  updateDataItem() {
    if (this.view_Id > -1) {
      let rowPermissionsArray: Array<ViewRowPermissionInfo> =
        this.dataItem.rowPermissions;
      var rowPermissionItem = rowPermissionsArray.find(
        (f) => f.viewId == this.view_Id
      );
      if (rowPermissionItem) {
        var index = rowPermissionsArray.indexOf(rowPermissionItem);
        switch (this.ddlPermissionTypeSelected) {
          case PermissionType.Default: {
            rowPermissionsArray[index].accessMode = "Default";
            rowPermissionsArray[index].value = 0;
            rowPermissionsArray[index].value2 = 0;
            rowPermissionsArray[index].textValue = "";
            rowPermissionsArray[index].items = [];
            break;
          }
          case PermissionType.AllRecordsCreatedByUser: {
            rowPermissionsArray[index].accessMode = "AllRecordsCreatedByUser";
            rowPermissionsArray[index].value = 0;
            rowPermissionsArray[index].value2 = 0;
            rowPermissionsArray[index].textValue = "";
            rowPermissionsArray[index].items = [];
            break;
          }
          case PermissionType.SpecificRecords: {
            rowPermissionsArray[index].accessMode = "SpecificRecords";
            rowPermissionsArray[index].value = 0;
            rowPermissionsArray[index].value2 = 0;
            rowPermissionsArray[index].textValue = "";
            rowPermissionsArray[index].items = //this.isChangeMultipleForm
              //?
              this.multipleFormItemsSelected;
            //: [];
            break;
          }
          case PermissionType.AllExceptSpecificRecords: {
            rowPermissionsArray[index].accessMode = "AllExceptSpecificRecords";
            rowPermissionsArray[index].value = 0;
            rowPermissionsArray[index].value2 = 0;
            rowPermissionsArray[index].textValue = "";
            rowPermissionsArray[index].items = //this.isChangeMultipleForm
              //?
              this.multipleFormItemsSelected;
            //: [];
            break;
          }
          case PermissionType.SpecificReference: {
            rowPermissionsArray[index].accessMode = "SpecificReference";
            rowPermissionsArray[index].value = 0;
            rowPermissionsArray[index].value2 = 0;
            rowPermissionsArray[index].textValue = this.permissionValue5;
            rowPermissionsArray[index].items = [];
            this.permissionValue5 = String.Empty;
            break;
          }
          case PermissionType.AllExceptSpecificReference: {
            rowPermissionsArray[index].accessMode =
              "AllExceptSpecificReference";
            rowPermissionsArray[index].value = 0;
            rowPermissionsArray[index].value2 = 0;
            rowPermissionsArray[index].textValue = this.permissionValue6;
            rowPermissionsArray[index].items = [];
            this.permissionValue6 = String.Empty;
            break;
          }
          case PermissionType.MaxMoneyValue: {
            rowPermissionsArray[index].accessMode = "MaxMoneyValue";
            rowPermissionsArray[index].value = this.numberValue1;
            rowPermissionsArray[index].value2 = this.numberValue2;
            rowPermissionsArray[index].textValue = "";
            rowPermissionsArray[index].items = [];
            this.numberValue1 = 0;
            this.numberValue2 = 0;
            break;
          }
          case PermissionType.MaxQuantityValue: {
            rowPermissionsArray[index].accessMode = "MaxQuantityValue";
            rowPermissionsArray[index].value = this.numberValue;
            rowPermissionsArray[index].value2 = 0;
            rowPermissionsArray[index].textValue = "";
            rowPermissionsArray[index].items = [];
            this.numberValue = 0;
            break;
          }
          default: {
            //this.lableTitle = '';
            break;
          }
        }
      }
    }
  }

  cancelRowPermission() {
    this.singleFormSelectedValue = "";
    this.view_Id = -1;
    this.ddlPermissionTypeSelected = 0;
  }
}
