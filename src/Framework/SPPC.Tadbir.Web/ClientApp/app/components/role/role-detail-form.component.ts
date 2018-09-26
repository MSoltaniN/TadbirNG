import { Component, EventEmitter, Input, Output } from '@angular/core';

import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';

import { RoleDetails, RoleDetailsViewModel } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { String } from '../../class/source';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { TreeNodeInfo } from '../../model/role';
import { Permission } from '../../model/permission';
import { DetailComponent } from '../../class/detail.component';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'role-detail-form-component',
    styles: [`
       .user-dialog {width: 100% !important; height:100% !important}
       /deep/ .user-dialog .k-dialog{ height:100% !important; min-width: unset !important; }
`
    ],
    templateUrl: './role-detail-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class RoleDetailFormComponent extends DetailComponent {


    ////create properties
    public gridPermissionData: any;
    //public gridBranchesData: any;
    public gridUsersData: any;


    public showloadingPermissionMessage: boolean = true;
    public showloadingBranchesMessage: boolean = true;
    public showloadingUsersMessage: boolean = true;

    public roleName: string;
    public treeData: TreeNodeInfo[] = new Array<TreeNodeInfo>();
    public roleDescription: string;

    private permissonDictionary: { [id: string]: Permission; } = {}
    

    @Input() public roleDetail: boolean = false;
    @Input() public errorMessage: string = '';


    @Input() public set roleDetails(roleDetails: RoleDetails) {

        
        var level0Index: number = -1;
        var level1Index: number = 0;

        if (roleDetails != undefined) {

            var groupId = 0;

            this.treeData = new Array<TreeNodeInfo>();

            if (this.CurrentLanguage == "fa")
                this.treeData.push(new TreeNodeInfo(-1, undefined, "حسابداری"));
            else
                this.treeData.push(new TreeNodeInfo(-1, undefined, "Accounting"));

            var indexId: number = 0;
            var selectAll: boolean = true;

            var sortedPermission = roleDetails.permissions.sort(function (a: Permission, b: Permission) {
                return a.id - b.id;
            });

            for (let permissionItem of sortedPermission) {


                if (groupId != permissionItem.groupId) {
                    this.treeData.push(new TreeNodeInfo(permissionItem.groupId, -1, permissionItem.groupName))

                    level0Index++;
                    level1Index = -1;
                    
                    groupId = permissionItem.groupId;
                }

                if (groupId == permissionItem.groupId) {
                    this.treeData.push(new TreeNodeInfo(parseInt(permissionItem.id.toString() + permissionItem.groupId.toString() + '00')
                        , permissionItem.groupId, permissionItem.name))

                    level1Index++;
                }
                
                this.permissonDictionary['0_' + level0Index.toString() + '_' + level1Index.toString()] = permissionItem;

            }   
            
            //this.gridBranchesData = roleDetails.branches;
            this.gridUsersData = roleDetails.users;

            this.roleName = roleDetails.role.name;
            this.roleDescription = roleDetails.role.description != null ? roleDetails.role.description : "";

            this.showloadingPermissionMessage = !(this.treeData.length == 0);
            //this.showloadingBranchesMessage = !(this.gridBranchesData.length == 0);
            this.showloadingUsersMessage = !(this.gridUsersData.length == 0);


        }

        //this.gridPermissionData = roleDetails.permissions;
        
    }

    @Output() cancelRoleDetail: EventEmitter<any> = new EventEmitter();
    ////create properties



    //////Events
 
    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.roleDetail = false;
        this.gridPermissionData = undefined;
        //this.gridBranchesData = undefined;
        this.gridUsersData = undefined;
        this.cancelRoleDetail.emit();
    }
    ////Events



    getTitleText(text: string) {
        return String.Format(text, this.roleName);
    }

}