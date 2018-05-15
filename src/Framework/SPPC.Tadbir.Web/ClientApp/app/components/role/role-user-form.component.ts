import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { RoleUsersInfo } from '../../service/index';

import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';

import { RoleUsers } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { String } from '../../class/source';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'role-user-form-component',
    styles: [`
       .user-dialog {width: 100% !important; height:100% !important}
       /deep/ .user-dialog .k-dialog{ height:100% !important; min-width: unset !important; }
`
    ],
    templateUrl: './role-user-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class RoleUserFormComponent extends DefaultComponent {


    ////create properties
    public gridData: any;
    public selectedRows: number[] = [];
    public showloadingMessage: boolean = true;
    public model: RoleUsers;
    public roleName: string;

    @Input() public usersList: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set roleUser(roleUser: RoleUsers) {
        this.model = roleUser;
        this.selectedRows = [];
        if (roleUser != undefined) {
            this.gridData = roleUser.users;
            this.roleName = roleUser.name;

            for (let userItem of this.gridData) {
                if (userItem.hasRole) {
                    this.selectedRows.push(userItem.id)
                }
            }
        }
    }

    @Output() cancelRoleUsers: EventEmitter<any> = new EventEmitter();
    @Output() saveRoleUsers: EventEmitter<RoleUsers> = new EventEmitter();
    ////create properties



    //////Events
    public onSave(e: any): void {
        e.preventDefault();

        this.model.users.forEach(f => f.hasRole = false);

        for (let userSelected of this.selectedRows) {
            let userIndex = this.model.users.findIndex(f => f.id == userSelected);
            this.model.users[userIndex].hasRole = true;
        }

        this.saveRoleUsers.emit(this.model);        
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.usersList = false;
        this.selectedRows = [];
        this.cancelRoleUsers.emit();
    }
    ////Events

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id;
    }


    getTitleText(text: string) {
        return String.Format(text, this.roleName);
    }
}