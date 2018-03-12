
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
        public metadata: MetaDataService
        ) 
    {
        super(toastrService, translate, renderer, metadata,'');
        this.lang = this.currentlang;

        this.parent.step2 = true;
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
        }

        if (this.currentlang == 'en') {
            this.renderer.addClass(document.body, 'tLtr');
            this.renderer.removeClass(document.body, 'tRtl');            
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
                }
            },
            error => {
                this.toastrService.error(this.getText("Login.Validation.UserIncorrect") , '', { positionClass: 'toast-top-center' });
                this.loading = false;
            });
    }
}
