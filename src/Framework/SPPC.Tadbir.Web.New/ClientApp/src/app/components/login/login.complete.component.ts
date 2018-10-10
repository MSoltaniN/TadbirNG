import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthenticationService, CompanyLoginInfo } from '../../service/login/index';
import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from 'ngx-toastr';

import { LoginContainerComponent } from "./login.container.component";
import { Host, Renderer2 } from '@angular/core';
import { ContextInfo } from "../../service/login/authentication.service";
import { MessageType, Layout, MessagePosition, SessionKeys } from "../../../environments/environment";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { TranslateService } from '@ngx-translate/core';
import { UserService, SettingService } from '../../service/index';
import { Command } from '../../model/command';
import { ListFormViewConfig } from '../../model/listFormViewConfig';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';


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
    

    
    //#region Fields
    model: any = {};
    loading = false;
    returnUrl: string;
    ticket: string = '';



    public disabledBranch: boolean = true;
    public disabledFiscalPeriod: boolean = true
    public disabledCompany: boolean = true;    

    public compenies: any = {};

    public branches: any = {};

    public fiscalPeriods: any = {};


    public companyId: string = '';
    public branchId: string = '';
    public fiscalPeriodId: string = '';
    //#endregion

    //#region Constructor
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        public toastrService: ToastrService,
        public translate: TranslateService,
        @Host() parent: LoginContainerComponent,
        public renderer: Renderer2,
        public metadata: MetaDataService,
        public userService: UserService,
        public settingService : SettingService) 
    {
      super(toastrService, translate, renderer, metadata, settingService,'', '');
            
    }

    //#endregion

    //#region Events

    ngOnInit() {        

        this.disabledCompany = true;
        this.getCompany();
        //load setting
        this.loadAllSetting();
        
    }

    //#endregion

    //#region Methods

    public branchChange(value: any) {        
        this.fiscalPeriodId = '';
    }

    public companyChange(value: any): void {
        this.disabledBranch = true;
        this.disabledFiscalPeriod = true;

        this.branches = [];
        this.branchId = '';

        this.fiscalPeriods = [];
        this.fiscalPeriodId = '';

        this.getBranch(value);
        this.getFiscalPeriod(value);

        var lastBranchId = localStorage.getItem(SessionKeys.LastUserBranch + this.UserId + this.companyId);
        var lastFpId = localStorage.getItem(SessionKeys.LastUserFpId + this.UserId + this.companyId);

        if (lastBranchId)
            this.branchId = lastBranchId;

        if (lastFpId)
            this.fiscalPeriodId = lastFpId;

    }

    getCompany() {
        this.authenticationService.getCompanies(this.UserName, this.Ticket).subscribe(res => {
            this.compenies = res;
            this.disabledCompany = false;

            //#region load current setting
            if (this.CompanyId) {
                this.companyId = this.CompanyId.toString();
                this.companyChange(this.companyId);
            }
            //#endregion
        });;
    }


    getBranch(companyId: number) {
        this.authenticationService.getBranches(companyId, this.Ticket).subscribe(res => {
            this.disabledBranch = false;
            this.branches = res;
        });
    }

    getFiscalPeriod(companyId: number) {

        this.authenticationService.getFiscalPeriod(companyId, this.Ticket).subscribe(res => {
            this.disabledFiscalPeriod = false;
            this.fiscalPeriods = res;
        });
    }

    isValidate(): boolean {
        var isValidate: boolean = true;

        if (this.companyId == '') {
            this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
            this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
            this.showMessage(this.getText("AllValidations.Login.CompanyIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
                       
            isValidate = false;
            return isValidate;
        }

        if (this.branchId == '') {
            this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
            isValidate = false;
        }

        if (this.fiscalPeriodId == '') {
            this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
            isValidate = false;
        }

        return isValidate;
    }

    selectParams() {
        
        if (this.isValidate()) {

            if (this.authenticationService.islogin()) {
                
                this.getCompanyTicket();
                
            }
        }
    }

    onCancleClick() {
        if (this.authenticationService.islogin()) {
            var currentUser = this.authenticationService.getCurrentUser();
            if (currentUser != null) {
                this.companyId = currentUser.companyId.toString();
                this.branchId = currentUser.branchId.toString();
                this.fiscalPeriodId = currentUser.fpId.toString();

                this.loadMenuAndRoute(currentUser);
            }
        }
    }

    
    loadAllSetting() {

        var settingList: Array<ListFormViewConfig> = new Array<ListFormViewConfig>();

        this.settingService.getListSettingsByUser(this.UserId).subscribe((res: Array<ListFormViewConfig>) => {

            if (res)
                localStorage.setItem(SessionKeys.Setting + this.UserId, JSON.stringify(res));
        });
    }

    loadMenuAndRoute(currentUser: ContextInfo) {
        //#region load menu
        var menuList: Array < Command > = new Array<Command>();
        
        var commands: any;

        this.userService.getCurrentUserCommands(this.Ticket).subscribe((res: Array<Command>) => {
            var list: Array<Command> = res;
            
            if (this.authenticationService.isRememberMe()) {
                localStorage.setItem(SessionKeys.Menu, JSON.stringify(res));
                localStorage.setItem('currentContext', JSON.stringify(currentUser));
            }
            else {
                sessionStorage.setItem(SessionKeys.Menu, JSON.stringify(res));
                sessionStorage.setItem('currentContext', JSON.stringify(currentUser));
            }                

            this.authenticationService.getFiscalPeriodById(currentUser.fpId, this.Ticket).subscribe(res => {
                if (this.authenticationService.isRememberMe())
                    localStorage.setItem('fiscalPeriod', JSON.stringify(res));
                else
                    sessionStorage.setItem('fiscalPeriod', JSON.stringify(res));
            })

            if (this.route.snapshot.queryParams['returnUrl'] != undefined) {
                var url = this.route.snapshot.queryParams['returnUrl'];
                this.router.navigate([url]);
            }
            else {

                var currentRoute = sessionStorage.getItem(SessionKeys.CurrentRoute);
                if (currentRoute) {
                    this.router.navigate([currentRoute]);
                }
                else {
                    this.router.navigate(['/account']);
                }
            }

        });

        //#endregion
    }

    /**
     * تیکت امنیتی را مطابق شرکت و شعبه و دوره مالی از سرویس میگیرد و جایگزین تیکت قبلی میکند
     */
    getCompanyTicket() {

        var companyLoginModel = new CompanyLoginInfo();
        companyLoginModel.companyId = parseInt(this.companyId);
        companyLoginModel.branchId = parseInt(this.branchId);
        companyLoginModel.fiscalPeriodId = parseInt(this.fiscalPeriodId);

        this.authenticationService.getCompanyTicket(companyLoginModel, this.Ticket).subscribe(res => {

            if (res.headers != null) {
                let newTicket = res.headers.get('X-Tadbir-AuthTicket');

                var currentUser = this.authenticationService.getCurrentUser();
                if (currentUser != null) {

                    currentUser.branchId = parseInt(this.branchId);
                    currentUser.companyId = parseInt(this.companyId);
                    currentUser.fpId = parseInt(this.fiscalPeriodId);
                    currentUser.permissions = JSON.parse(atob(this.Ticket)).user.permissions;

                    currentUser.ticket = newTicket;

                    if (this.authenticationService.isRememberMe())
                        localStorage.setItem('currentContext', JSON.stringify(currentUser));
                    else
                        sessionStorage.setItem('currentContext', JSON.stringify(currentUser));

                    localStorage.setItem(SessionKeys.LastUserBranch + this.UserId + this.companyId, this.branchId);
                    localStorage.setItem(SessionKeys.LastUserFpId + this.UserId + this.companyId, this.fiscalPeriodId);

                    this.loadMenuAndRoute(currentUser);
                }

            }

        })
    }

    //#endregion


   


}
