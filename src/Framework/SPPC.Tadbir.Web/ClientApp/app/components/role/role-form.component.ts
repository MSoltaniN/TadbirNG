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


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'role-form-component',
    styles: [`
        input[type=text],textarea { width: 100%; }
       /deep/.permission-dialog {width: 100% !important; min-width: 250px !important; height:100%}
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

    //create a form controls
    private editForm = new FormGroup({
        id: new FormControl(),
        name: new FormControl("", [Validators.required, Validators.maxLength(64)]),
        description: new FormControl("", [Validators.maxLength(512)]),
    });

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

    @Input() public set permissionModel(permission: Permission) {

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

        for (let permissionSelected of this.selectedRows) {
            for (let permissionItem of this.gridPermissionsData) {
                if (permissionItem.id == permissionSelected) {
                    permissionItem.isEnabled = true;
                }
            }
        }
        
        var viewModel: RoleFullViewModel;
        viewModel = {
            role: this.editForm.value,
            permissions: this.gridPermissionsData
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
        return context.dataItem.id;
    }

    constructor(private roleService: RoleService, private formBuilder: FormBuilder,
        public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.Role, Metadatas.Role);

    }


}