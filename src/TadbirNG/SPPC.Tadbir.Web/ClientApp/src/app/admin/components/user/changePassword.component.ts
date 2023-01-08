import { Component, ElementRef, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { DetailComponent } from '@sppc/shared/class';
import { MetaDataService, BrowserStorageService, ErrorHandlingService } from '@sppc/shared/services';
import { MessageType, Entities } from '@sppc/shared/enum/metadata';
import { UserService } from '@sppc/admin/service';
import { UserProfile } from '@sppc/shared/models';
import { ViewName } from '@sppc/shared/security';




@Component({
  selector: 'changePassword-component',
  styles: [
    `input[type=text],input[type=password] { width: 100%; }`
  ],
  templateUrl: './changePassword.component.html'

})

export class ChangePasswordComponent extends DetailComponent {


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
    //this.sppcLoading.show();

    this.model = <UserProfile>this.editForm1.value;
    this.model.userName = this.user_Name;

    this.userService.changePassword(this.model).subscribe(res => {
      this.editForm1.reset();
      this.errorMessages = undefined;
      this.showMessage(this.updateMsg, MessageType.Succes);
    }, (error => {
        if(error)
          this.errorMessages = this.errorHandlingService.handleError(error);;
    }));

    //this.sppcLoading.hide();

  }


  constructor(public toastrService: ToastrService, public translate: TranslateService,
    private userService: UserService, public renderer: Renderer2, public metadata: MetaDataService,
    public bStorageService: BrowserStorageService, public errorHandlingService:ErrorHandlingService,public elem:ElementRef) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Password, ViewName.User,elem);

    //if (localStorage.getItem('currentContext') != null) {
    //  var item: string | null;
    //  item = localStorage.getItem('currentContext');
    //  var currentContext = JSON.parse(item != null ? item.toString() : "");
    //  this.user_Name = currentContext ? currentContext.userName.toString() : "";
    //}
    //else if (sessionStorage.getItem('currentContext') != null) {
    //  var item: string | null;
    //  item = sessionStorage.getItem('currentContext');
    //  var currentContext = JSON.parse(item != null ? item.toString() : "");
    //  this.user_Name = currentContext ? currentContext.userName.toString() : "";
    //}

    this.user_Name = this.UserName;
  }



}
