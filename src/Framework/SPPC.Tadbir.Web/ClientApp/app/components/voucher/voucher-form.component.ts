import { Component, Input, Output, EventEmitter, Renderer2, ChangeDetectionStrategy } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { VoucherService, VoucherInfo, VoucherLineService, FiscalPeriodService } from '../../service/index';

import { Voucher } from '../../model/index';
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

interface Item {
    Key: string,
    Value: string
}


@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'voucher-form-component',
    styles: [
        "input[type=text],textarea { width: 100%; } /deep/ .new-dialog > .k-dialog {width: 450px !important; min-width: 250px !important;}",
        "/deep/ .edit-dialog > .k-dialog {width: 100% !important; min-width: 250px !important; height:100%}",
        "/deep/ .edit-dialog .k-window-titlebar{ padding: 5px 16px !important;}",
        "/deep/ .edit-dialog .edit-form-body { background: #f6f6f6; border: solid 1px #989898; border-radius: 4px; padding-top: 10px;}"
    ],
    templateUrl: './voucher-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})

export class VoucherFormComponent extends DefaultComponent {

    ////create a form controls
    //private editForm = new FormGroup({
    //    id: new FormControl(),
    //    description: new FormControl("", Validators.maxLength(512)),
    //    no: new FormControl("", [Validators.required, Validators.maxLength(64)]),
    //    date: new FormControl("", Validators.required)
    //});

    //create properties
    public voucher_Id: number;
    active: boolean = false;
    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string;

    @Input() public set model(voucher: Voucher) {

        this.editForm.reset(voucher);

        this.active = voucher !== undefined || this.isNew;

        if (voucher != undefined) {
            this.voucher_Id = voucher.id;
        }

    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Voucher> = new EventEmitter();
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

    public onDeleteData() {
        alert("Data deleted.");
    }
    //Events

    constructor(private voucherLineService: VoucherLineService, private fiscalPeriodService: FiscalPeriodService,
        public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.Voucher, Metadatas.Voucher);
    }

}