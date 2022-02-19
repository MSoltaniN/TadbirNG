import { Component, Host, Inject, OnInit, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DOCUMENT } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DialogService } from '@progress/kendo-angular-dialog';
import { SettingService } from '@sppc/config/service';
//import { DOCUMENT } from '@angular/common';
import { AuthenticationService, ContextInfo } from '@sppc/core';
import { String } from '@sppc/shared/class/source';
import { MessageType } from '@sppc/shared/enum/metadata';
import { ErrorType } from '@sppc/shared/models';
import { BrowserStorageService, DashboardService, LicenseService, MetaDataService } from '@sppc/shared/services';
import { LicenseApi } from '@sppc/shared/services/api/licenseApi';
import * as moment from 'jalali-moment';
import { ToastrService } from 'ngx-toastr';
import { DefaultComponent } from "../../class/default.component";
import { LoginContainerComponent } from "./login.container.component";


@Component({
    selector: 'login',
    templateUrl: 'login.component.html',
    styleUrls: ['./login.component.css']

})
export class LoginComponent extends DefaultComponent implements OnInit {
   
    model: any = {};
    loading = false;
    returnUrl: string;
    firstStep: boolean = true;
    ticket: string = '';

    currentLogin:ContextInfo;
    currentUserId:number;

    public lang: string = '';
    public stepOne : boolean = true;
    public showActivationForm:boolean = false;
    public onlineLicense:boolean = false;
    public showServerForm:boolean = false;
    public showSessionForm:boolean = false;
    public sessions : any[];

    private duringCheckOfflineLicense:boolean = false;
    private duringCheckOnlineLicense:boolean = false;

    public activationForm = new FormGroup({            
      userName: new FormControl('', [Validators.required, Validators.maxLength(128)]),
      password: new FormControl('', [Validators.required, Validators.maxLength(128)])      
    });

    serverUserName:string = "";
    serverPassword:string = "";

    constructor(
        private route: ActivatedRoute,
        private router: Router,
      private authenticationService: AuthenticationService, public toastrService: ToastrService, public bStorageService: BrowserStorageService,
        public translate: TranslateService, @Host() public parent: LoginContainerComponent, public renderer: Renderer2,
        public metadata: MetaDataService, public settingService: SettingService, @Inject(DOCUMENT) public document,
        private licenseService:LicenseService,private dashborardService:DashboardService,private dialogService:DialogService) 
    {
      super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);
        this.lang = this.currentlang;
       
    }

    ngOnInit() {       

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        
    }

    changeLang(language : string)
    {
        this.changeLanguage(language);
        this.lang = language;

        if (this.currentlang == 'fa') {
            this.renderer.addClass(document.body, 'tRtl');
          this.renderer.removeClass(document.body, 'tLtr');

          if (this.document.getElementById('sppcFont').getAttribute('href') != 'assets/resources/IranSans.css')
              this.document.getElementById('sppcFont').setAttribute('href','assets/resources/IranSans.css');
        }

        if (this.currentlang == 'en') {
            this.renderer.addClass(document.body, 'tLtr');
          this.renderer.removeClass(document.body, 'tRtl');

          if (this.document.getElementById('sppcFont').getAttribute('href') != 'assets/resources/IranSans-en.css')
              this.document.getElementById('sppcFont').setAttribute('href', 'assets/resources/IranSans-en.css');
        }      
    }

    disableLink(fileName : string)
    {
        var links = document.getElementsByTagName("link");
        for(var i=0; i < links.length; i++) {
            var link = links[i];
            if(link.getAttribute("rel").indexOf("style") != -1 && link.getAttribute("href")) {
                //link.disabled = true;
                if(link.getAttribute("href") === fileName)
                    link.disabled = true; 
                    
            }
        }

    }

    login() {
        this.loading = true;
        this.authenticationService.login(this.model.username, this.model.password, this.model.remember)
            .subscribe(
            data => {
                if (this.authenticationService.islogin())
                { 
                  this.currentUserId = this.UserId;
                  if (this.bStorageService.getCurrentUser().lastLoginDate == null || this.bStorageService.getLicense() == null) {                    
                    this.checkOfflineLicense();
                  }

                  if (this.bStorageService.getCurrentUser().lastLoginDate != null && this.bStorageService.getLicense() != null) 
                  {
                    this.parent.step1 = false;
                    this.parent.step2 = true;
                  }
                  
                  
                }
            },
            error => {
                this.toastrService.error(this.getText("Login.UserIncorrect") , '', { positionClass: 'toast-top-center' });
                this.loading = false;
            });
    }

    setLicenseCache(license)
    {
      this.bStorageService.setLicense(license);

        this.parent.step1 = false;
        this.parent.step2 = true;

        this.dashborardService.getLincenseInfo().subscribe((info) => {
          this.bStorageService.setLicenseInfo(info);
        });
    }

    closeSessionForm()
    {
      this.showSessionForm = false;
    }

    checkOfflineLicense(serverUserName?:string,serverPassword?:string)
    {
      this.licenseService.CheckOfflineLicense(String.Format(LicenseApi.UserLicenseUrl,this.currentUserId),serverUserName,serverPassword).subscribe((res) => {
        this.setLicenseCache(res);
        this.duringCheckOfflineLicense = false;
      },
      error => {
        this.currentLogin = this.bStorageService.getCurrentUser();
        this.logOut();

        if(error.statusCode == 403)
        {
          if(error.type == ErrorType.NotActivated)
          {
            if(!this.checkInternetConnection())
            {              
              return;
            }
            else
            {
              this.activateLicense();
            }
          }

          if(error.type == ErrorType.RequiresOnlineLicense)
          {
            this.checkOnlineLicense();
          }

          if(error.type == ErrorType.BadLicense)
          {            
            this.licenseError();
          }

          if(error.type == ErrorType.TooManySessions)
          {            
            this.tooManySessionsMessage();
          }

          if(error.type == ErrorType.InvalidUserPass)
          {
            this.duringCheckOfflineLicense = true;
            this.showServerPasswordForm()          
          }

        }
        else
        {
          this.licenseError();
        }

      });
    }

    showServerPasswordForm()
    {
      this.showMessageWithTime(this.getText("Messages.ActivationPasswordIsNotCorrect"), MessageType.Error,3000);
      this.activationForm.reset();
      this.showServerForm = true;      
    }

    convertToShamsi(date)
    {
      var shamsiDate = moment(date).locale('fa').format("YYYY/MM/DD HH:mm");
      return shamsiDate;
    }

    checkInternetConnection()
    {
      if(!navigator.onLine)
      {
        this.showMessage(this.getText("Messages.InternetConnectionError"), MessageType.Info);
        this.logOut();
        return false;
      }

      return true;
    }

    checkOnlineLicense()
    {
      if(!this.checkInternetConnection()) return;     
      
      this.showOnlineLicense();
    }    

    startActivatingSoftware()
    {
      var userName = this.activationForm.controls.userName.value;
      var password = this.activationForm.controls.password.value;
      
      this.licenseService.ActivateLicense(LicenseApi.ActivateLicenseUrl,userName,password).subscribe((res) => {
        this.closeActivationForm();
        this.showMessageWithTime(this.getText("Messages.ActivationIsSuccessful"), MessageType.Succes,4000);        
        this.bStorageService.setContext(this.currentLogin,this.model.remember);
        this.checkOfflineLicense();
      },
      error => {        
        debugger
        if(error.type == ErrorType.InvalidUserPass)
        {
          this.showMessageWithTime(this.getText("Messages.ActivationPasswordIsNotCorrect"), MessageType.Error,4000);
        }
        else
        {          
          this.showMessageWithTime(this.getText("Messages.ActivationIsNotSuccessful"), MessageType.Error,4000);
        }
        this.logOut();
      });
    }

    startCheckingOnlineLicense(serverUserName?:string,serverPassword?:string)
    {   
      this.closeOnlineLicenseForm();
      this.licenseService.CheckOnlineLicense(String.Format(LicenseApi.OnlineUserLicenseUrl,this.currentUserId),serverUserName,serverPassword).subscribe((res) => {
        this.showMessageWithTime(this.getText("Messages.OnlineLicenseIsSuccessful"), MessageType.Succes,4000);        
        
        this.bStorageService.setContext(this.currentLogin,this.model.remember);        
        this.setLicenseCache(res);
        this.duringCheckOnlineLicense = false;
      },
      error => {    
        if(error.type == ErrorType.InvalidUserPass)
        {
          this.showMessageWithTime(this.getText("Messages.ActivationPasswordIsNotCorrect"), MessageType.Error,2000);
          this.duringCheckOnlineLicense = true;
          this.showServerPasswordForm();
        }
        else
        {
          this.showMessageWithTime(this.getText("Messages.OnlineLicenseIsNotSuccessful"), MessageType.Error,4000);
        }

        this.logOut();
      });
    }
    
    startCheckLicense()
    {
      //check online license with new user and password
      
      var userName = this.activationForm.controls.userName.value;
      var password = this.activationForm.controls.password.value;

      if(this.duringCheckOnlineLicense)
      {        
        this.startCheckingOnlineLicense(userName,password);
        
      }

      if(this.duringCheckOfflineLicense)
      {
        this.checkOfflineLicense(userName,password);        
      }
    }

    closeServerForm()
    {
      this.showServerForm = false;
    }

    closeActivationForm()
    {
      this.showActivationForm = false;
    }

    closeOnlineLicenseForm()
    {
      this.onlineLicense = false;
    }

    logOut()
    {
      this.bStorageService.removeCurrentContext();
      this.loading = false;
    }

    activateLicense()
    {
      this.showActivationForm = true;  
    }

    showOnlineLicense()
    {
      this.onlineLicense = true;
    }

    tooManySessionsMessage()
    {
      this.showMessage(this.getText("Messages.TooManySessions"), MessageType.Info);
      this.logOut();

      this.licenseService.GetOpenSessions(String.Format(LicenseApi.OpenSessionsByUserUrl,this.currentUserId)).subscribe((res)=>
      {
        this.showSessionForm = true;
        this.sessions = res;
      });

    }

    removeSession(id:number)
    {
      let modelsIdArray: Array<number> = [];
      modelsIdArray.push(id);
      this.licenseService.DeleteOpenSessions(String.Format(LicenseApi.OpenSessionsUrl),modelsIdArray).subscribe((res)=>
      {        
        this.sessions.splice(this.sessions.findIndex(f=>f.id == id),1);
        if(this.sessions.length == 0)
        {
          this.showSessionForm = false;
        }
      });      
    }

    licenseError()
    {
      this.showMessage(this.getText("Messages.LicenseError"), MessageType.Error);
      this.logOut();
    }

}
