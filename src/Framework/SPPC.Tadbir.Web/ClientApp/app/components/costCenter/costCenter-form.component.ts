import { Component, Input, Output, EventEmitter, Renderer2, Host } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { CostCenter } from '../../model/index';

import { Property } from "../../class/metadata/property"
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { CostCenterApi } from '../../service/api/index';
import { String } from '../../class/source';
import { DetailComponent } from '../../class/detail.component';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}

interface Item {
    Key: string,
    Value: string
}


@Component({
    selector: 'costCenter-form-component',
    styles: [
        "input[type=text],textarea { width: 100%; }"
    ],
    templateUrl: './costCenter-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class CostCenterFormComponent extends DetailComponent {
    
    //create properties
    active: boolean = false;
    fullCodeApiUrl: string;

    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public parentTitle: string = '';
    @Input() public parentValue: string = '';
    @Input() public parentScopeValue: number = 0;

    @Input() public set parentId(id: number) {
        this.fullCodeApiUrl = String.Format(CostCenterApi.CostCenterFullCode, id ? id : 0);
    }

    @Input() public set model(costCenter: CostCenter) {
        this.editForm.reset(costCenter);

        this.active = costCenter !== undefined || this.isNew;
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<CostCenter> = new EventEmitter();
    //create properties

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        if (this.editForm.valid) {
            this.save.emit(this.editForm.value);
            this.active = true;
        }
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

    constructor(public toastrService: ToastrService, public translate: TranslateService,
        public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.CostCenter, Metadatas.CostCenter);
    }

   
}