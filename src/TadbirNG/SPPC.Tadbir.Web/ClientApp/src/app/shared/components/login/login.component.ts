import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DefaultComponent } from "../../class/default.component";
import { TranslateService } from '@ngx-translate/core';
import {LoginContainerComponent} from "./login.container.component";
import { Host, Renderer2 } from '@angular/core';
//import { DOCUMENT } from '@angular/common';
import { AuthenticationService } from '@sppc/core';
import { MetaDataService, BrowserStorageService, SessionKeys, LicenseService, DashboardService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { DOCUMENT } from '@angular/platform-browser';
import { LicenseApi } from '@sppc/shared/services/api/licenseApi';
import { MessageType } from '@sppc/shared/enum/metadata';
import { ErrorType } from '@sppc/shared/models';




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


    public lang: string = '';
    public stepOne : boolean = true;

    

    constructor(
        private route: ActivatedRoute,
        private router: Router,
      private authenticationService: AuthenticationService, public toastrService: ToastrService, public bStorageService: BrowserStorageService,
        public translate: TranslateService, @Host() public parent: LoginContainerComponent, public renderer: Renderer2,
        public metadata: MetaDataService, public settingService: SettingService, @Inject(DOCUMENT) public document,private licenseService:LicenseService,private dashborardService:DashboardService) 
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
                  if (this.bStorageService.getCurrentUser().lastLoginDate == null || this.bStorageService.getLicense() == null) {
                    this.getLicense();
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

    getLicense()
    {
      this.licenseService.GetAppLicense(LicenseApi.LicenseUrl).subscribe((res) => {

        this.bStorageService.setLicense(res);

        this.parent.step1 = false;
        this.parent.step2 = true;

        this.dashborardService.getLincenseInfo().subscribe((info) => {
          this.bStorageService.setLicenseInfo(info);
        });

      },
      error => {
        debugger;
        if(error.statusCode == 403)
        {
          if(error.type == (ErrorType.NotActivated))
          {
            if(!navigator.onLine)
            {
              this.showMessage(this.getText("Messages.InternetConnectionError"), MessageType.Info);
              this.logOut();
              return;
            }
            else
            {
              this.activateLicense();
            }
          }

          if(error.type == (ErrorType.BadLicense))
          {            
            this.licenseError();
          }
        }
        else
        {
          this.licenseError();
        }

      });
    }

    logOut()
    {
      this.bStorageService.removeCurrentContext();
      this.loading = false;
    }

    activateLicense()
    {
      var timeout = 2500;      
      this.showMessageWithTime(this.getText("Messages.StartingActivation"), MessageType.Info,timeout);

      setTimeout(() => {

        this.licenseService.ActivateLicense(LicenseApi.ActivateLicenseUrl).subscribe((res) => {
          this.showMessageWithTime(this.getText("Messages.ActivationIsSuccessful"), MessageType.Succes,timeout);        
          this.getLicense();
        },
        error => {
          this.showMessageWithTime(this.getText("Messages.ActivationIsNotSuccessful"), MessageType.Error,timeout);
          this.logOut();
        });

      }, timeout);
      
    }

    licenseError()
    {
      this.showMessage(this.getText("Messages.LicenseError"), MessageType.Error);
      this.logOut();
    }

}
