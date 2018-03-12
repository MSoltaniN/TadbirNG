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
import { MessageType, Layout, MessagePosition } from "../../enviroment";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';


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
        public branchService: BranchService,
        public companyService: CompanyService,
        private fiscalPeriodService: FiscalPeriodService,
        @Host() parent: LoginContainerComponent,
        public renderer: Renderer2,
        public metadata: MetaDataService) 
    {
        super(toastrService, translate, renderer, metadata,'');
            
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

        
        
        //var userName = '';
        //var ticket = '';

        //if (localStorage.getItem('currentContext')) {
        //    const userJson = localStorage.getItem('currentContext');
        //    var currentUser = userJson !== null ? JSON.parse(userJson) : null;

        //    if (currentUser != null) {
        //        userName = currentUser.userName;        
        //        ticket = currentUser.ticket;
        //    }


        //}

        this.companyService.getCompanies(this.UserName,this.Ticket).subscribe(res => {
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
            this.showMessage(this.getText("Login.Validation.SelectCompany"), MessageType.Info, '', MessagePosition.TopCenter);
            isValidate = false;
            return isValidate;
        }

        if (this.branchId == '')
        {
            this.showMessage(this.getText("Login.Validation.SelectBranch"), MessageType.Info, '', MessagePosition.TopCenter);
            isValidate = false;
        }

        if (this.fiscalPeriodId == '') {
            this.showMessage(this.getText("Login.Validation.SelectFiscalPeriod"), MessageType.Info, '', MessagePosition.TopCenter);
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

                    if (this.authenticationService.isRememberMe())
                        localStorage.setItem('currentContext', JSON.stringify(currentUser));
                    else
                        sessionStorage.setItem('currentContext', JSON.stringify(currentUser));

                    this.router.navigate(['/account2']);
                }
            }
        }
    }


}
