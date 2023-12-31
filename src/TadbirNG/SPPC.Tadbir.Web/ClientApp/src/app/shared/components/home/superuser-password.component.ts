import { Component, OnInit, Output, EventEmitter, Renderer2, ElementRef } from '@angular/core';
import { DetailComponent } from '@sppc/shared/class/detail.component';
import { BrowserStorageService, MetaDataService, ErrorHandlingService } from '@sppc/shared/services';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Entities, MessageType } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';
import { AuthenticationService } from '@sppc/core/services/authentication.service';

@Component({
  selector: 'superuser-password',
  templateUrl: './superuser-password.component.html',
  styles: [`
    @font-face{font-family:'text-security-disc';
      src:url('../../../../assets/resources/fonts/text-security-disc-compat.woff2') format('woff2')
    }
    .password-field {
      display: flex;
      margin: 6px 0;
      align-items: center;
      gap: 10px;
    }
    .password-field input {
      width:80%;
      -webkit-text-security: square;
      font-family:'text-security-disc';
    }
  `]
})
export class SuperuserPasswordComponent extends DetailComponent implements OnInit {

  @Output() result: EventEmitter<any> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  showGetPasswordModal: boolean;
  specialPassword: string;
  value: string;
  showErrorMessage: boolean = false;

  constructor(public bStorageService: BrowserStorageService,
    public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService, 
    public authService: AuthenticationService, public errorHandlingService: ErrorHandlingService,public elem:ElementRef) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.None, ViewName.None,elem);
  }

  ngOnInit() {
  }

  onCancel(): void {
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }

  onPasswordChange(value) {
    this.specialPassword = value;
    if (this.specialPassword == '') {
      this.showErrorMessage = true;
    } else {
      this.showErrorMessage = false;
    }

  }

  onOk() {
    //this.result.emit({ password : this.specialPassword });
    this.checkPasswordModel(this.specialPassword);
  }

  checkPasswordModel(password: string) {
    var specialpassword = password;
    this.authService.checkSpecialPassword(specialpassword, this.Ticket).subscribe(res => {
      this.result.emit();
    }, (error => {      
        if (error)
          this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);        
    }));
  } 


}
