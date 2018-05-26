﻿import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { Branch } from '../../model/index';

import { Property } from "../../class/metadata/property"
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
    selector: 'branch-form-component',
    styles: [
        "input[type=text],textarea { width: 100%; }"
    ],
    templateUrl: './branch-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class BranchFormComponent extends DefaultComponent {
    
    //create properties
    active: boolean = false;
    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set model(branch: Branch) {
        this.editForm.reset(branch);

        this.active = branch !== undefined || this.isNew;
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Branch> = new EventEmitter();
    //create properties

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        this.save.emit(this.editForm.value);
        this.active = true;
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.isNew = false;
        this.active = false;
        this.cancel.emit();
    }
    //Events

    constructor(public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.Branch, Metadatas.Branch);
    }

   
}