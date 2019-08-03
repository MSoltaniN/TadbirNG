import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent } from '@sppc/shared/class';
import { RelatedItems, SecureEntity, UserPermissions } from '@sppc/shared';
import { Layout } from 'environments/environment';



export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'user-roles-form-component',
    templateUrl: './user-roles-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class UserRolesFormComponent extends DetailComponent implements OnInit {

    //permission flag   
    AssignRolesAccess: boolean;

    ////create properties
    public gridData: any;
    public selectedRows: number[] = [];
    public showloadingMessage: boolean = true;
    public model: RelatedItems;


    @Input() public rolesList: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set userRoles(userRoles: RelatedItems) {
        this.model = userRoles;
        this.selectedRows = [];
        if (userRoles != undefined) {
            this.gridData = userRoles.relatedItems;

            for (let roleItem of this.gridData) {
                if (roleItem.isSelected) {
                    this.selectedRows.push(roleItem.id)
                }
            }
        }
    }

    @Output() cancelUserRoles: EventEmitter<any> = new EventEmitter();
    @Output() saveUserRoles: EventEmitter<RelatedItems> = new EventEmitter();
    ////create properties

    ngOnInit() {
        this.AssignRolesAccess = this.isAccess(SecureEntity.User, UserPermissions.AssignRoles);
    }

    //////Events
    public onSave(e: any): void {
        e.preventDefault();
        this.model.relatedItems.forEach(f => f.isSelected = false);

        for (let roleSelected of this.selectedRows) {
            let roleIndex = this.model.relatedItems.findIndex(f => f.id == roleSelected);
            this.model.relatedItems[roleIndex].isSelected = true;
        }
        this.saveUserRoles.emit(this.model);
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.rolesList = false;
        this.selectedRows = [];
        this.cancelUserRoles.emit();
  }

  escPress() {
    this.closeForm();
  }
    ////Events

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id;
    }
}
