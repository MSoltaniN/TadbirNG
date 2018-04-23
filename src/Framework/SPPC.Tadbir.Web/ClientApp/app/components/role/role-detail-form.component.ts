import { Component, EventEmitter, Input, Output } from '@angular/core';

import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';

import { RoleBranchesViewModel, RoleDetailsViewModel } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { String } from '../../class/source';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';


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

export class RoleDetailFormComponent extends DefaultComponent {


    ////create properties
    public gridPermissionData: any;
    public gridBranchesData: any;
    public gridUsersData: any;


    public showloadingPermissionMessage: boolean = true;
    public showloadingBranchesMessage: boolean = true;
    public showloadingUsersMessage: boolean = true;

    public roleName: string;
    public roleDescription: string;

    @Input() public roleDetail: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set roleDetailsViewModel(roleDetailsViewModel: RoleDetailsViewModel) {

        if (roleDetailsViewModel != undefined) {
            this.gridPermissionData = roleDetailsViewModel.permissions;
            this.gridBranchesData = roleDetailsViewModel.branches;
            this.gridUsersData = roleDetailsViewModel.users;

            this.roleName = roleDetailsViewModel.role.name;
            this.roleDescription = roleDetailsViewModel.role.description != null ? roleDetailsViewModel.role.description:"";

            this.showloadingPermissionMessage = !(this.gridPermissionData.length == 0);
            this.showloadingBranchesMessage = !(this.gridBranchesData.length == 0);
            this.showloadingUsersMessage = !(this.gridUsersData.length == 0);
        }
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
        this.gridBranchesData = undefined;
        this.gridUsersData = undefined;
        this.cancelRoleDetail.emit();
    }
    ////Events



    getTitleText(text: string) {
        return String.Format(text, this.roleName);
    }

}