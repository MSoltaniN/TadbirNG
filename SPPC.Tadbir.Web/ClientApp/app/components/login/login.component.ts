
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import {  AuthenticationService } from '../../service/login/index';
import { ToastrService } from "toastr-ng2/toastr";
import { DefaultComponent } from "../../class/default.component";
import { TranslateService } from "ng2-translate";

import {LoginContainerComponent} from "./login.container.component";
import { Host, Renderer2 } from '@angular/core';
import { MetaDataService } from '../../service/metadata/metadata.service';



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
        private authenticationService: AuthenticationService, public toastrService: ToastrService,
        public translate: TranslateService, @Host() public parent: LoginContainerComponent, public renderer: Renderer2,
        public metadata: MetaDataService) 
    {
        super(toastrService, translate, renderer,'',metadata);
        this.lang = this.currentlang;
    }

    ngOnInit() {
        // reset login status
        this.authenticationService.logout();
        if(this.authenticationService.islogin())
        {
            this.router.navigate(['/account2']);
        }

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
        }

        if (this.currentlang == 'en') {
            this.renderer.addClass(document.body, 'tLtr');
            this.renderer.removeClass(document.body, 'tRtl');            
        }

    }

    login() {
        this.loading = true;
        this.authenticationService.login(this.model.username, this.model.password)
            .subscribe(
            data => {
                //this.router.navigate([this.returnUrl]);
                
                if(localStorage.getItem('currentContext') != undefined)
                {                    
                    
                    //if (localStorage.getItem('currentContext'))
                    //{
                    //    var currentContext = JSON.parse(localStorage.getItem('currentContext'));
                    //    if (currentContext && currentContext.ticket)
                    //    {
                
                    //    }
                    //}
                    this.parent.step1 = false;
                    this.parent.step2 = true;
                

                    //this.stepOne = false;
                }
            },
            error => {
                this.toastrService.error(this.getText("Login.Validation.UserIncorrect") , '', { positionClass: 'toast-top-center' });
                this.loading = false;
            });
    }
}
