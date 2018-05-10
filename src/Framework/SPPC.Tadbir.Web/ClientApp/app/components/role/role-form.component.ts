import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { RoleService, RoleInfo } from '../../service/index';

import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';

import { Role, RoleFullViewModel, Permission } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { TreeNode, TreeNodeInfo } from '../../model/role';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'role-form-component',
    styles: [`
        input[type=text],textarea { width: 100%; }
        .permission-dialog {width: 100% !important; min-width: 250px !important; height:100%}
        /deep/ .permission-dialog .k-dialog{ height:100% !important; min-width: unset !important; }
`
    ],
    templateUrl: './role-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class RoleFormComponent extends DefaultComponent {

   

    public treeData: TreeNodeInfo[] = new Array<TreeNodeInfo>();
    
    //create properties
    gridPermissionsData: any;
    public selectedRows: number[] = [];
    active: boolean = false;
    showloadingMessage: boolean = true;

    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set model(role: Role) {

        this.editForm.reset(role);
        this.active = role !== undefined || this.isNew;
    }

    public checkedKeys: string[] = [];
    
    private permissonDictionary: { [id: string]: Permission; } = {}

    @Input() public set permissionModel(permission: any) {

        var levelIndex0: number = -1;
        var levelIndex1: number = 0;

        if (permission != undefined) {
            var groupId = 0;

            this.checkedKeys = [];
            this.treeData = new Array<TreeNodeInfo>();

            if(this.CurrentLanguage == "fa")
                this.treeData.push(new TreeNodeInfo(-1, undefined, "حسابداری"));
            else
                this.treeData.push(new TreeNodeInfo(-1, undefined, "Accounting"));
            

            for (let permissionItem of permission) {

                
                if (groupId != permissionItem.groupId) {
                    this.treeData.push(new TreeNodeInfo(permissionItem.groupId, -1, permissionItem.groupName))
                    groupId = permissionItem.groupId;

                    levelIndex0++;
                    levelIndex1 = -1;
                }

                if (groupId == permissionItem.groupId) {
                    this.treeData.push(new TreeNodeInfo(parseInt(permissionItem.id.toString() + permissionItem.groupId.toString() + '00')
                        , permissionItem.groupId, permissionItem.name))

                    levelIndex1++;
                }



                if (permissionItem.isEnabled)
                    this.checkedKeys.push('0_' + levelIndex0.toString() + '_' + levelIndex1.toString());

                this.permissonDictionary['0_' + levelIndex0.toString() + '_' + levelIndex1.toString()] = permissionItem;
                    
            }
            
        }
        
        this.gridPermissionsData = permission;
        this.showloadingMessage = !(permission != undefined);

        if (permission != undefined) {
            for (let permissionItem of this.gridPermissionsData) {
                if (permissionItem.isEnabled) {
                    this.selectedRows.push(permissionItem.id)
                }
            }
        }
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<RoleFullViewModel> = new EventEmitter();
    //create properties



    ////Events
    public onSave(e: any): void {
        e.preventDefault();

        var permissionData: Array<Permission> = new Array<Permission>();
        
        for (let key in this.permissonDictionary) {
            //permissionItem.isEnabled = false;
            this.permissonDictionary[key].isEnabled = false;
        }

        for (let checked of this.checkedKeys) {
            if (checked.split('_').length == 3) {
                var obj = this.permissonDictionary[checked];
                obj.isEnabled = true;
            }
        }

        
        for (let key in this.permissonDictionary) {
            //permissionItem.isEnabled = false;
            permissionData.push(this.permissonDictionary[key]);
        }

        //for (let permissionSelected of this.selectedRows) {
        //    for (let permissionItem of this.gridPermissionsData) {
        //        if (permissionItem.id == permissionSelected) {
        //            permissionItem.isEnabled = true;
        //        }
        //    }
        //}

        var viewModel: RoleFullViewModel;
        viewModel = {
            id : this.editForm.value.id,
            role: this.editForm.value,
            permissions: permissionData
        }
        this.save.emit(viewModel);
        this.active = true;
        this.selectedRows = [];
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.selectedRows = [];
        this.closeForm();
    }

    private closeForm(): void {
        this.isNew = false;
        this.active = false;
        this.selectedRows = [];
        this.cancel.emit();
    }
    ////Events

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id;
    }

    constructor(private roleService: RoleService, private formBuilder: FormBuilder,
        public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.Role, Metadatas.Role);

    }


}