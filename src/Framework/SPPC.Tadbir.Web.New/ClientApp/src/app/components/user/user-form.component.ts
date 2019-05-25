import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { User } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Entities } from '../../../environments/environment';
import { DetailComponent } from '../../class/detail.component';
import { ViewName } from '../../security/viewName';



@Component({
    selector: 'user-form-component',
    styles: [
        `input[type=text],input[type=password] { width: 100%; } .ddl-fAcc {width:49%}
         /deep/ .k-switch[dir='rtl'] .k-switch-label-off { left: -3.35em; }
         /deep/.k-switch-label-on { left: -4.4em;}
         /deep/.k-switch{width: 9em;}
         /deep/.k-switch[dir='rtl']{width: 6.4em;}
         
`
    ],
    templateUrl: './user-form.component.html'   
})

export class UserFormComponent extends DetailComponent {

    public checked: boolean = false;
    //create a form controls
    private editForm1 = new FormGroup({
        id: new FormControl(),
        personFirstName: new FormControl("", [Validators.required, Validators.maxLength(64)]),
        personLastName: new FormControl("", [Validators.required, Validators.maxLength(64)]),
        userName: new FormControl("", [Validators.required, Validators.maxLength(64)]),
        passwordHash: new FormControl("", [Validators.required, Validators.maxLength(32)]),
        isEnabled: new FormControl(),
    });

    //create properties
    active: boolean = false;
    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string = "";


    @Input() public set model(user: User) {

        //this.editForm.reset(user);
        this.editForm.reset(user);
        this.active = user !== undefined || this.isNew;

    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<User> = new EventEmitter();
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

  escPress() {
    this.closeForm();
  }
    //Events


    constructor(public toastrService: ToastrService, public translate: TranslateService,
        public renderer: Renderer2, public metadata: MetaDataService) {

      super(toastrService, translate, renderer, metadata, Entities.User, ViewName.User);
    }

}
