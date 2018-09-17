import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';

import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { String } from '../../class/source';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { RelatedItems } from '../../model/index';
import { DetailComponent } from '../../class/detail.component';


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

export class RoleBranchFormComponent extends DetailComponent {


    ////create properties
    public gridData: any;
    public selectedRows: number[] = [];
    public showloadingMessage: boolean = true;
    public model: RelatedItems;


    @Input() public inputRoleBranches: boolean = false;
    @Input() public errorMessage: string = '';
    @Input() public roleName: string = '';

    @Input() public set roleBranches(roleBranches: RelatedItems) {
        this.model = roleBranches;
        this.selectedRows = [];
        if (roleBranches != undefined) {
            this.gridData = roleBranches.relatedItems;

            for (let branchItem of this.gridData) {
                if (branchItem.isSelected) {
                    this.selectedRows.push(branchItem.id)
                }
            }
        }
    }

    @Output() cancelRoleBranches: EventEmitter<any> = new EventEmitter();
    @Output() saveRoleBranches: EventEmitter<RelatedItems> = new EventEmitter();
    ////create properties



    //////Events
    public onSave(e: any): void {
        e.preventDefault();

        this.model.relatedItems.forEach(f => f.isSelected = false);
        for (let branchSelected of this.selectedRows) {
            let branchIndex = this.model.relatedItems.findIndex(f => f.id == branchSelected);
            this.model.relatedItems[branchIndex].isSelected = true;
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