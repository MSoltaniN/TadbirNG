import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { ViewRowPermissionService, ItemInfo, Item, RowPermissionsForRoleInfo, ViewRowPermissionInfo, SettingService } from '../../service/index';
import { LookupApi, RoleApi } from '../../service/api/index';
import { PermissionType } from '../../enum/permissionType';



export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}

@Component({
    selector: 'accountRelations',
    templateUrl: './viewRowPermission.component.html',
    styles: [`
.input-frm{ width:100%; }
.input-frm-btn { width: calc(100% - 125px); }
#permission-type-panle { height: 150px; border: solid 1px #337ab7; margin-top: 20px;}
.panel-type-btn{ padding: 40px 25px 40px 10px; }.panel-type,.panel-type-multiple{ padding: 40px 25px; }
@media screen and (max-width:768px){.panel-type-multiple{ padding:15px 0; }}
`],
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})

export class ViewRowPermissionComponent extends DefaultComponent implements OnInit {
    public filterChange(filter: CompositeFilterDescriptor): void {
        throw new Error("Method not implemented.");
    }
    public reloadGrid(insertedModel?: any): void {
        throw new Error("Method not implemented.");
    }

    public isActiveSingleForm: boolean = false;
    public isActiveMultipleForm: boolean = false;
    public isChangeMultipleForm: boolean = false;
    public errorMessage = String.Empty;

    public dataItem: RowPermissionsForRoleInfo;
    public dataRowPermission: ViewRowPermissionInfo | undefined;
    public viewId: number = -1;

    //public lableTitle: string;
    public rolesArray: Array<Item>;
    public ddlRolesData: Array<Item>;

    public ddlPermissionTypeData: Array<Item>;

    public ddlRoleSelected: number = 0;
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

    public ngOnInit(): void {
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
      private viewRowPermissionService: ViewRowPermissionService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
      super(toastrService, translate, renderer, metadata, settingService, Entities.ViewRowPermission, undefined);

        this.getRoles();

        this.ddlPermissionTypeData = [
            { value: "ViewRowPermission.DdlPermissionItems.Default", key: PermissionType.Default },
            { value: "ViewRowPermission.DdlPermissionItems.AllRecordsCreatedByUser", key: PermissionType.AllRecordsCreatedByUser },
            { value: "ViewRowPermission.DdlPermissionItems.SpecificRecords", key: PermissionType.SpecificRecords },
            { value: "ViewRowPermission.DdlPermissionItems.AllExceptSpecificRecords", key: PermissionType.AllExceptSpecificRecords },
            { value: "ViewRowPermission.DdlPermissionItems.SpecificReference", key: PermissionType.SpecificReference },
            { value: "ViewRowPermission.DdlPermissionItems.AllExceptSpecificReference", key: PermissionType.AllExceptSpecificReference },
            { value: "ViewRowPermission.DdlPermissionItems.MaxMoneyValue", key: PermissionType.MaxMoneyValue },
            { value: "ViewRowPermission.DdlPermissionItems.MaxQuantityValue", key: PermissionType.MaxQuantityValue }
        ];
    }

    handleRoleChange(item: any) {
        //this.sppcLoading.show();
        this.viewRowPermissionService.getById(String.Format(RoleApi.RowAccessSettings, item)).subscribe(res => {
            this.dataItem = res;

            this.singleFormSelectedValue = '';
            this.singleFormSelectedModel = undefined;
            this.ddlPermissionTypeSelected = 0;

            this.permissionValue3 = String.Empty;
            this.permissionValue4 = String.Empty;
            this.permissionValue5 = String.Empty;
            this.permissionValue6 = String.Empty;
            this.numberValue = 0;
            this.numberValue1 = 0;
            this.numberValue2 = 0;

            //this.sppcLoading.hide();
        })
    }


    handlePermissionTypeChange(item: any) {
    }

    getRoles() {
        //this.sppcLoading.show();
        this.viewRowPermissionService.getAll(LookupApi.Roles).subscribe(res => {
            var data = res.body;
            this.rolesArray = data;
            this.ddlRolesData = data;
            //this.sppcLoading.hide();
        })
    }

    handleFilter(value: any) {
        this.ddlRolesData = this.rolesArray.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
    }

    openSingleForm() {
        this.errorMessage = '';
        this.updateDataItem();
        this.isActiveSingleForm = true;
    }

    cancelSingleFormHandler() {
        this.errorMessage = '';
        this.isActiveSingleForm = false;
    }

    saveSingleFormHandler(model: Item) {
        this.singleFormSelectedModel = model;
        this.singleFormSelectedValue = model.value;
        this.viewId = model.key;
        var rowPermission = this.dataItem.rowPermissions.find(f => f.viewId == this.viewId);
        if (rowPermission) {

            if (rowPermission.items.length > 0 && rowPermission.accessMode == "SpecificRecords") {
                this.permissionValue3 = "سطرهای انتخاب شده";
            }
            else {
                this.permissionValue3 = "سطری انتخاب نشده";
            }

            if (rowPermission.items.length > 0 && rowPermission.accessMode == "AllExceptSpecificRecords") {
                this.permissionValue4 = "سطرهای انتخاب شده";
            }
            else {
                this.permissionValue4 = "سطری انتخاب نشده";
            }

            switch (rowPermission.accessMode) {
                case "Default": {
                    this.ddlPermissionTypeSelected = PermissionType.Default;
                    break;
                }
                case "AllRecordsCreatedByUser": {
                    this.ddlPermissionTypeSelected = PermissionType.AllRecordsCreatedByUser;
                    break;
                }
                case "SpecificRecords": {
                    this.ddlPermissionTypeSelected = PermissionType.SpecificRecords;
                    if (rowPermission.items.length > 0) {
                        this.permissionValue3 = "سطرهای انتخاب شده";
                    }
                    else {
                        this.permissionValue3 = "سطری انتخاب نشده";
                    }
                    break;
                }
                case "AllExceptSpecificRecords": {
                    this.ddlPermissionTypeSelected = PermissionType.AllExceptSpecificRecords;
                    if (rowPermission.items.length > 0) {
                        this.permissionValue4 = "سطرهای انتخاب شده";
                    }
                    else {
                        this.permissionValue4 = "سطری انتخاب نشده";
                    }
                    break;
                }
                case "SpecificReference": {
                    this.ddlPermissionTypeSelected = PermissionType.SpecificReference;
                    this.permissionValue5 = rowPermission.textValue;
                    break;
                }
                case "AllExceptSpecificReference": {
                    this.ddlPermissionTypeSelected = PermissionType.AllExceptSpecificReference;
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
        this.errorMessage = '';

        this.entity = this.singleFormSelectedModel;
        var row = this.dataItem.rowPermissions.find(f => f.viewId == this.viewId);
        if (row)
            this.dataRowPermission = row;

        this.isActiveMultipleForm = true;
    }

    cancelMultipleFormHandler() {
        this.errorMessage = '';
        this.entity = undefined;
        this.dataRowPermission = undefined;
        this.multipleFormItemsSelected = [];
        this.isActiveMultipleForm = false;
        this.isChangeMultipleForm = false;
    }

    saveMultipleFormHandler(items: number[]) {
        this.multipleFormItemsSelected = items;
        this.isChangeMultipleForm = true;
        this.updateDataItem();

        this.isActiveMultipleForm = false;
    }

    saveRowPermission() {
        this.errorMessage = '';
        this.updateDataItem();
        this.viewRowPermissionService.edit<RowPermissionsForRoleInfo>(String.Format(RoleApi.RowAccessSettings, this.ddlRoleSelected), this.dataItem).subscribe(res => {

            this.showMessage(this.updateMsg, MessageType.Succes);

            this.ddlRoleSelected = 0;
            this.singleFormSelectedValue = '';
            this.ddlPermissionTypeSelected = 0;

        }, (error => {
            this.errorMessage = error;
        }))

    }

    updateDataItem() {
        if (this.viewId > -1) {
            let rowPermissionsArray: Array<ViewRowPermissionInfo> = this.dataItem.rowPermissions;
            var rowPermissionItem = rowPermissionsArray.find(f => f.viewId == this.viewId);
            if (rowPermissionItem) {
                var index = rowPermissionsArray.indexOf(rowPermissionItem);
                switch (this.ddlPermissionTypeSelected) {
                    case PermissionType.Default: {
                        rowPermissionsArray[index].accessMode = "Default";
                        rowPermissionsArray[index].value = 0;
                        rowPermissionsArray[index].value2 = 0;
                        rowPermissionsArray[index].textValue = '';
                        rowPermissionsArray[index].items = [];
                        break;
                    }
                    case PermissionType.AllRecordsCreatedByUser: {
                        rowPermissionsArray[index].accessMode = "AllRecordsCreatedByUser";
                        rowPermissionsArray[index].value = 0;
                        rowPermissionsArray[index].value2 = 0;
                        rowPermissionsArray[index].textValue = '';
                        rowPermissionsArray[index].items = [];
                        break;
                    }
                    case PermissionType.SpecificRecords: {
                        if (this.isChangeMultipleForm) {
                            rowPermissionsArray[index].accessMode = "SpecificRecords";
                            rowPermissionsArray[index].value = 0;
                            rowPermissionsArray[index].value2 = 0;
                            rowPermissionsArray[index].textValue = '';
                            rowPermissionsArray[index].items = this.multipleFormItemsSelected;
                        }
                        break;
                    }
                    case PermissionType.AllExceptSpecificRecords: {
                        if (this.isChangeMultipleForm) {
                            rowPermissionsArray[index].accessMode = "AllExceptSpecificRecords";
                            rowPermissionsArray[index].value = 0;
                            rowPermissionsArray[index].value2 = 0;
                            rowPermissionsArray[index].textValue = '';
                            rowPermissionsArray[index].items = this.multipleFormItemsSelected;
                        }
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
                        rowPermissionsArray[index].accessMode = "AllExceptSpecificReference";
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
                        rowPermissionsArray[index].textValue = '';
                        rowPermissionsArray[index].items = [];
                        this.numberValue1 = 0;
                        this.numberValue2 = 0;
                        break;
                    }
                    case PermissionType.MaxQuantityValue: {
                        rowPermissionsArray[index].accessMode = "MaxQuantityValue";
                        rowPermissionsArray[index].value = this.numberValue;
                        rowPermissionsArray[index].value2 = 0;
                        rowPermissionsArray[index].textValue = '';
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
        this.ddlRoleSelected = 0;
        this.singleFormSelectedValue = '';
        this.viewId = -1;
        this.ddlPermissionTypeSelected = 0;
    }
}
