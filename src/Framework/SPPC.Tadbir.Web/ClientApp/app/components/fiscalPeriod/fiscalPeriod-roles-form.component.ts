import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { SortDescriptor, orderBy, State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';
import { String } from '../../class/source';
import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities, Metadatas } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { RelatedItems } from '../../model/index';
import { SecureEntity } from '../../security/secureEntity';
import { FiscalPeriodPermissions } from '../../security/permissions';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'fiscalPeriod-roles-form-component',
    templateUrl: './fiscalPeriod-roles-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class FiscalPeriodRolesFormComponent extends DefaultComponent implements OnInit {

    //permission flag   
    AssignRolesAccess: boolean;

    ////create properties
    public gridData: any;
    public selectedRows: number[] = [];
    public showloadingMessage: boolean = true;
    public model: RelatedItems;


    @Input() public rolesList: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set fiscalPeriodRoles(fPeriodRoles: RelatedItems) {
        this.model = fPeriodRoles;
        this.selectedRows = [];
        if (fPeriodRoles != undefined) {
            this.gridData = fPeriodRoles.relatedItems;

            for (let roleItem of this.gridData) {
                if (roleItem.isSelected) {
                    this.selectedRows.push(roleItem.id)
                }
            }
        }
    }

    @Output() cancelFiscalPeriodRoles: EventEmitter<any> = new EventEmitter();
    @Output() saveFiscalPeriodRoles: EventEmitter<RelatedItems> = new EventEmitter();
    ////create properties

    ngOnInit() {
        this.AssignRolesAccess = this.isAccess(SecureEntity.FiscalPeriod, FiscalPeriodPermissions.AssignRoles);
    }

    //////Events
    public onSave(e: any): void {
        e.preventDefault();
        this.model.relatedItems.forEach(f => f.isSelected = false);

        for (let roleSelected of this.selectedRows) {
            let roleIndex = this.model.relatedItems.findIndex(f => f.id == roleSelected);
            this.model.relatedItems[roleIndex].isSelected = true;
        }
        this.saveFiscalPeriodRoles.emit(this.model);
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.rolesList = false;
        this.selectedRows = [];
        this.cancelFiscalPeriodRoles.emit();
    }
    ////Events

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id;
    }
}