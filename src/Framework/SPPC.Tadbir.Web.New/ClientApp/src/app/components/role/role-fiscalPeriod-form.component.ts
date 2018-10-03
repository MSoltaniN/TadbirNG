import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
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
import { DetailComponent } from '../../class/detail.component';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'role-fiscalPeriod-form-component',
    templateUrl: './role-fiscalPeriod-form.component.html',
    styles: ['/deep/ .k-window-title{font-size:15px}'],
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class RoleFiscalPeriodFormComponent extends DetailComponent {


    ////create properties
    public gridData: any;
    public selectedRows: number[] = [];
    public showloadingMessage: boolean = true;
    public model: RelatedItems;


    @Input() public fiscalPeriodList: boolean = false;
    @Input() public errorMessage: string = '';
    @Input() public roleName: string = '';

    @Input() public set roleFiscalPeriod(roleFiscalPeriod: RelatedItems) {
        this.model = roleFiscalPeriod;
        this.selectedRows = [];
        if (roleFiscalPeriod != undefined) {
            this.gridData = roleFiscalPeriod.relatedItems;

            for (let fPeriodItem of this.gridData) {
                if (fPeriodItem.isSelected) {
                    this.selectedRows.push(fPeriodItem.id)
                }
            }
        }
    }

    @Output() cancelRoleFiscalPeriod: EventEmitter<any> = new EventEmitter();
    @Output() saveRoleFiscalPeriod: EventEmitter<RelatedItems> = new EventEmitter();
    ////create properties

    //////Events
    public onSave(e: any): void {
        e.preventDefault();
        this.model.relatedItems.forEach(f => f.isSelected = false);

        for (let fPeriodSelected of this.selectedRows) {
            let userIndex = this.model.relatedItems.findIndex(f => f.id == fPeriodSelected);
            this.model.relatedItems[userIndex].isSelected = true;
        }
        this.saveRoleFiscalPeriod.emit(this.model);        
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.fiscalPeriodList = false;
        this.selectedRows = [];
        this.cancelRoleFiscalPeriod.emit();
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
