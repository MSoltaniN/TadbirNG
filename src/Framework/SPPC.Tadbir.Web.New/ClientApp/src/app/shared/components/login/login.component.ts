import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DefaultComponent } from "../../class/default.component";
import { TranslateService } from '@ngx-translate/core';
import {LoginContainerComponent} from "./login.container.component";
import { Host, Renderer2 } from '@angular/core';
//import { DOCUMENT } from '@angular/common';
import { AuthenticationService } from '@sppc/core';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { DOCUMENT } from '@angular/platform-browser';




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
        public metadata: MetaDataService, public settingService: SettingService, @Inject(DOCUMENT) public document
        ) 
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

        // if(language == 'fa')
        // {
        //     if(this.document.getElementById('adminlte').getAttribute('href') != '../assets/dist/css/AdminLTE.Rtl.css')
        //        this.document.getElementById('adminlte').setAttribute('href', '../assets/dist/css/AdminLTE.Rtl.css');
        //     // this.cssUrl = '../assets/dist/css/AdminLTE.Rtl.css';
        // }
        // else
        // {
        //    if(this.document.getElementById('adminlte').getAttribute('href') != '../assets/dist/css/AdminLTE.min.css')
        //        this.document.getElementById('adminlte').setAttribute('href', '../assets/dist/css/AdminLTE.min.css');
        //     //this.cssUrl = '../assets/dist/css/AdminLTE.min.css';
        // }
    
      
      
        
        // if(this.currentlang == 'fa')
        //     this.document.getElementById('adminlte').setAttribute('href', 'assets/dist/css/AdminLTE.Rtl.css');
        //  else
        //     this.document.getElementById('adminlte').setAttribute('href', 'assets/dist/css/AdminLTE.min.css');

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
                //this.router.navigate([this.returnUrl]);
                
                //if(localStorage.getItem('currentContext') != undefined)
                if (this.authenticationService.islogin())
                {     
                    this.parent.step1 = false;
                    this.parent.step2 = true;
                
                   

                    ////type Activity = typeof Metadatas;
                    //Object.values(Metadatas).map(val => {
                    //  //this.saveMetadataInCache(val);
                    //});
                }
            },
            error => {
                this.toastrService.error(this.getText("Login.UserIncorrect") , '', { positionClass: 'toast-top-center' });
                this.loading = false;
            });
    }

  

}
