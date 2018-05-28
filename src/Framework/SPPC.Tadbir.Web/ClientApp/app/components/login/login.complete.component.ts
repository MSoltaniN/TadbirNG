﻿import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import {  AuthenticationService } from '../../service/login/index';
import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from 'ngx-toastr';

import {LoginContainerComponent} from "./login.container.component";
import { Host, Renderer2 } from '@angular/core';
import { ContextInfo } from "../../service/login/authentication.service";
import { MessageType, Layout, MessagePosition } from "../../enviroment";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { TranslateService } from 'ng2-translate';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}  

@Component({
    selector: 'logincomplete',
    templateUrl: 'login.complete.component.html',
    styleUrls: ['./login.complete.component.css'],
    providers: [{
        provide: RTL,        
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
   
})


export class LoginCompleteComponent extends DefaultComponent implements OnInit {
    model: any = {};
    loading = false;
    returnUrl: string;    
    ticket: string = '';



    public disabledBranch: boolean = true;

    public disabledFiscalPeriod: boolean = true;

    public compenies: any = {};

    public branches: any = {};

    public fiscalPeriods: any = {};


    public companyId: string = '';
    public branchId: string = '';
    public fiscalPeriodId: string = '';


    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        public toastrService: ToastrService, 
        public translate: TranslateService,
        @Host() parent: LoginContainerComponent,
        public renderer: Renderer2,
        public metadata: MetaDataService) 
    {
        super(toastrService, translate, renderer, metadata,'','');
            
    }

    ngOnInit() {

        this.getCompany();

    }

   
    public companyChange(value: any): void {
        this.disabledBranch = true;
        this.disabledFiscalPeriod = true;

        this.getBranch(value);
        this.getFiscalPeriod(value);        
    }

    getCompany() {
        var companiesList = this.authenticationService.getCompanies(this.UserName, this.Ticket);
        if (companiesList != null) {
            companiesList.subscribe(res => {
                this.compenies = res;
            });
        }
    }


    getBranch(companyId: number) {
        var branchList = this.authenticationService.getBranches(companyId, this.Ticket);
        if (branchList != null) {
            branchList.subscribe(res => {
                this.disabledBranch = false;
                this.branches = res;
            });
        }           
    }

    getFiscalPeriod(companyId : number) {
        var fps = this.authenticationService.getFiscalPeriod(companyId, this.Ticket)
        if (fps != null) {
            fps.subscribe(res => {
                this.disabledFiscalPeriod = false;
                this.fiscalPeriods = res;
            });
        }   
    }

    isValidate(): boolean
    {
        var isValidate: boolean = true;

        if (this.companyId == '') {
            this.showMessage(this.getText("AllValidations.Login.CompanyIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
            isValidate = false;
            return isValidate;
        }

        if (this.branchId == '')
        {
            this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
            isValidate = false;
        }

        if (this.fiscalPeriodId == '') {
            this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
            isValidate = false;
        }

        return isValidate;
    }

    selectParams()
    {
        if (this.isValidate()) {
            
            if (this.authenticationService.islogin()) {

                var currentUser = this.authenticationService.getCurrentUser();
                if (currentUser != null) {

                    currentUser.branchId = parseInt(this.branchId);
                    currentUser.companyId = parseInt(this.companyId);
                    currentUser.fpId = parseInt(this.fiscalPeriodId);
                    currentUser.permissions = JSON.parse(atob(this.Ticket)).User.Permissions;

                    if (this.authenticationService.isRememberMe())
                        localStorage.setItem('currentContext', JSON.stringify(currentUser));
                    else
                        sessionStorage.setItem('currentContext', JSON.stringify(currentUser));

                    if (this.route.snapshot.queryParams['returnUrl'] != undefined) {
                        var url = this.route.snapshot.queryParams['returnUrl'];
                        this.router.navigate([url]);
                    }
                    else
                        this.router.navigate(['/account2']);
                }
            }
        }
    }


}
