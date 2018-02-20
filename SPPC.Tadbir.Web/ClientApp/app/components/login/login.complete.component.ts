import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import {  AuthenticationService } from '../../service/login/index';
import { ToastrService } from "toastr-ng2/toastr";
import { DefaultComponent } from "../../class/default.component";
import { TranslateService } from "ng2-translate";

import { CompanyService, BranchService, FiscalPeriodService } from '../../service/index';

import {LoginContainerComponent} from "./login.container.component";
import { Host, Renderer2 } from '@angular/core';
import { ContextInfo } from "../../service/login/authentication.service";
import { MessageType } from "../../enviroment";

@Component({
    selector: 'logincomplete',
    templateUrl: 'login.complete.component.html',
    styleUrls: ['./login.complete.component.css']

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
        public branchService: BranchService,
        public companyService: CompanyService,
        private fiscalPeriodService: FiscalPeriodService,
        @Host() parent: LoginContainerComponent,
        public renderer: Renderer2) 
    {
        super(toastrService, translate,renderer);
            
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


    //getFiscalPeriod(companyId:number) {

    //    if(companyId > 0)
    //    {
            
    //        this.fiscalPeriodService.getFiscalPeriod(0).subscribe(res => {
    //            this.ficalPeriods = res;
    //        });
            
    //    }
    //}


    getCompany() {

        
        
        var userName = '';
        var ticket = '';

        if (localStorage.getItem('currentContext')) {
            const userJson = localStorage.getItem('currentContext');
            var currentUser = userJson !== null ? JSON.parse(userJson) : null;

            if (currentUser != null) {
                userName = currentUser.userName;        
                ticket = currentUser.ticket;
            }


        }

        this.companyService.getCompanies(userName,ticket).subscribe(res => {
            this.compenies = res;
        });
    }


    getBranch(companyId: number) {

        this.branchService.getBranches(companyId).subscribe(res => {
            this.disabledBranch = false;
            this.branches = res;            
        });
    }

    getFiscalPeriod(companyId : number) {

        this.fiscalPeriodService.getFiscalPeriod(companyId).subscribe(res => {
            this.disabledFiscalPeriod = false;
            this.fiscalPeriods = res;                    
        });
    }

    isValidate(): boolean
    {
        var isValidate: boolean = true;

        if (this.companyId == '') {
            this.showMessage(this.getText("Login.Validation.SelectCompany"), MessageType.Info, '', 'toast-top-center');
            isValidate = false;
            return isValidate;
        }

        if (this.branchId == '')
        {
            this.showMessage(this.getText("Login.Validation.SelectBranch"), MessageType.Info, '', 'toast-top-center');
            isValidate = false;
        }

        if (this.fiscalPeriodId == '') {
            this.showMessage(this.getText("Login.Validation.SelectFiscalPeriod"), MessageType.Info, '', 'toast-top-center');
            isValidate = false;
        }

        return isValidate;
    }

    selectParams()
    {

        this.isValidate();

        if (localStorage.getItem('currentContext')) {
            const userJson = localStorage.getItem('currentContext');

            var currentUser: ContextInfo = userJson !== null ? JSON.parse(userJson) : null;

            currentUser.branchId = parseInt(this.branchId);
            currentUser.companyId = parseInt(this.companyId);
            currentUser.fpId = parseInt(this.fiscalPeriodId);

            
            localStorage.setItem('currentContext', JSON.stringify(currentUser));

            this.router.navigate(['/account2']);
            
        }
    }


}
