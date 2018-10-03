import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';
import { String } from '../../class/source';
import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { RelatedItems } from '../../model/index';
import { SecureEntity } from '../../security/secureEntity';
import { BranchPermissions } from '../../security/permissions';
import { DetailComponent } from '../../class/detail.component';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'branch-roles-form-component',
    templateUrl: './branch-roles-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class BranchRolesFormComponent extends DetailComponent implements OnInit {

    //permission flag   
    AssignRolesAccess: boolean;

    ////create properties
    public gridData: any;
    public selectedRows: number[] = [];
    public showloadingMessage: boolean = true;
    public model: RelatedItems;


    @Input() public rolesList: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set branchRoles(branchRoles: RelatedItems) {
        this.model = branchRoles;
        this.selectedRows = [];
        if (branchRoles != undefined) {
            this.gridData = branchRoles.relatedItems;

            for (let roleItem of this.gridData) {
                if (roleItem.isSelected) {
                    this.selectedRows.push(roleItem.id)
                }
            }
        }
    }

    @Output() cancelBranchRoles: EventEmitter<any> = new EventEmitter();
    @Output() saveBranchRoles: EventEmitter<RelatedItems> = new EventEmitter();
    ////create properties

    ngOnInit() {
        this.AssignRolesAccess = this.isAccess(SecureEntity.Branch, BranchPermissions.AssignRoles);
    }

    //////Events
    public onSave(e: any): void {
        e.preventDefault();
        this.model.relatedItems.forEach(f => f.isSelected = false);

        for (let roleSelected of this.selectedRows) {
            let roleIndex = this.model.relatedItems.findIndex(f => f.id == roleSelected);
            this.model.relatedItems[roleIndex].isSelected = true;
        }
        this.saveBranchRoles.emit(this.model);
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.rolesList = false;
        this.selectedRows = [];
        this.cancelBranchRoles.emit();
    }
    ////Events

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id;
    }
}
