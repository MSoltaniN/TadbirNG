﻿import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { RoleUsersInfo } from '../../service/index';

import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';

import { RoleBranches } from '../../model/index';
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
    selector: 'role-branch-form-component',
    styles: [`
       .user-dialog {width: 100% !important; height:100% !important}
       /deep/ .user-dialog .k-dialog{ height:100% !important; min-width: unset !important; }
`
    ],
    templateUrl: './role-branch-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class RoleBranchFormComponent extends DefaultComponent {


    ////create properties
    public gridData: any;
    public selectedRows: number[] = [];
    public showloadingMessage: boolean = true;
    public model: RoleBranches;
    public roleName: string;

    @Input() public inputRoleBranches: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set roleBranches(roleBranches: RoleBranches) {
        this.model = roleBranches;
        this.selectedRows = [];
        if (roleBranches != undefined) {
            this.gridData = roleBranches.branches;
            this.roleName = roleBranches.name;

            for (let branchItem of this.gridData) {
                if (branchItem.isAccessible) {
                    this.selectedRows.push(branchItem.id)
                }
            }
        }
    }

    @Output() cancelRoleBranches: EventEmitter<any> = new EventEmitter();
    @Output() saveRoleBranches: EventEmitter<RoleBranches> = new EventEmitter();
    ////create properties



    //////Events
    public onSave(e: any): void {
        e.preventDefault();

        this.model.branches.forEach(f => f.isAccessible = false);

        for (let branchSelected of this.selectedRows) {
            let branchIndex = this.model.branches.findIndex(f => f.id == branchSelected);
            this.model.branches[branchIndex].isAccessible = true;
        }

        this.saveRoleBranches.emit(this.model);
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.inputRoleBranches = false;
        this.selectedRows = [];
        this.cancelRoleBranches.emit();
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