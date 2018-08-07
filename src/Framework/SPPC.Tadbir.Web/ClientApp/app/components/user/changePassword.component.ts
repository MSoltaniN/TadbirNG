import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl, AbstractControl } from '@angular/forms';

import { UserProfile } from '../../model/index';

import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { MetaDataService } from '../../service/metadata/metadata.service';

import { Metadatas, Entities, MessageType } from '../../enviroment';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { UserService, UserProfileInfo } from '../../service/index';



@Component({
    selector: 'changePassword-component',
    styles: [
        `input[type=text],input[type=password] { width: 100%; }`
    ],
    templateUrl: './changePassword.component.html'
})

export class ChangePasswordComponent extends DefaultComponent {


    ////create a form controls
    public editForm1 = new FormGroup({
        userName: new FormControl(""),
        oldPassword: new FormControl("", [Validators.required]),
        newPassword: new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(32)]),
        repeatPassword: new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(32)]),
    });

    public model: UserProfile;
    public user_Name: string = "";
    public errorMessage: string = "";

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        this.sppcLoading.show();

        this.model = this.editForm1.value;
        this.model.userName = this.user_Name;

        this.userService.changePassword(this.model).subscribe(res => {
            this.editForm1.reset();
            this.errorMessage = "";
            this.showMessage(this.updateMsg, MessageType.Succes);
        }, (error => {
            this.errorMessage = error;
        }));

        this.sppcLoading.hide();

    }


    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private userService: UserService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.Password, Metadatas.User);

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            this.user_Name = currentContext ? currentContext.userName.toString() : "";
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            this.user_Name = currentContext ? currentContext.userName.toString() : "";
        }
    }



}