import { Component, Inject } from '@angular/core';
import { Context } from "../../model/context";
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FiscalPeriodService } from '../../service/fiscal-period.service';
import { CompanyService } from '../../service/company.service';
import { BranchService } from '../../service/branch.service';
import { DOCUMENT } from '@angular/platform-browser';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    currentContext?: Context = undefined;

    public showNavbar: boolean = false;

    public isLogin: boolean = false;

    public isRtl: boolean;

    public lang: string = '';

    public companyName: string;
    public branchName: string;
    public fiscalPeriodName: string;
    public userName: string;

    public compenies: any = {};

    public branches: any = {};

    public fiscalPeriods: any = {};



    constructor(location: Location, router: Router, public branchService: BranchService,
        public companyService: CompanyService,
        private fiscalPeriodService: FiscalPeriodService, @Inject(DOCUMENT) private document: Document) {


        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            this.currentContext = JSON.parse(item != null ? item.toString() : "");            
        }

        var language = localStorage.getItem('lang');
        if (language) {
            this.lang = language;
        }
        else {
            this.lang = "fa";
        }        

        

        if (this.currentContext != undefined) {
            this.showNavbar = true;
        }

        router.events.subscribe((val) => {
            if (location.path().toLowerCase() == '/login' || location.path().toString().indexOf('/login?returnUrl=') >= 0) {
                this.showNavbar = false;

                this.isLogin = true;
                
            }
            else
            {
                this.isLogin = false;
                this.showNavbar = true;

                var spacePad = this.document.getElementById('spacePad')
                var currentLang = localStorage.getItem('lang')
                if (currentLang == 'fa') {
                    if (spacePad) {
                        spacePad.classList.add('pull-right');
                        spacePad.classList.remove('pull-left');
                    }
                }
                else {
                    if (spacePad) {
                        spacePad.classList.add('pull-left');
                        spacePad.classList.remove('pull-right');
                    }
                }


        var branchId: number = 0;
        var companyId: number = 0;
        var fpId: number = 0;
        var ticket: string = "";        
        

        var contextIsEmpty: boolean = true;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            branchId = currentContext ? parseInt(currentContext.branchId) : 0;
            companyId = currentContext ? parseInt(currentContext.companyId) : 0;
            fpId = currentContext ? parseInt(currentContext.fpId) : 0;
            ticket = currentContext ? currentContext.ticket : "";
            this.userName = currentContext ? currentContext.userName.toString() : "";
            

            contextIsEmpty = false;
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            branchId = currentContext ? parseInt(currentContext.branchId) : 0;
            companyId = currentContext ? parseInt(currentContext.companyId) : 0;
            fpId = currentContext ? parseInt(currentContext.fpId) : 0;
            ticket = currentContext ? currentContext.ticket.toString() : "";
            this.userName = currentContext ? currentContext.userName.toString() : "";
            

            contextIsEmpty = false;
        }

        if (!contextIsEmpty) {

           

            this.fiscalPeriodService.getFiscalPeriod(companyId).subscribe(res => {
                //this.fiscalPeriods = res;
                this.fiscalPeriodName = res.filter((p: any) => p.key == fpId)[0].value;
            });

            this.branchService.getBranches(companyId).subscribe(res => {
                this.branchName = res.filter((p: any) => p.key == branchId)[0].value;
            });


            var companiesList = this.companyService.getCompanies(this.userName, ticket);
            if (companiesList != null) {
                companiesList.subscribe(res => {
                    this.companyName = res.filter((p: any) => p.key == companyId)[0].value;;

                });
            }

        }

            }
        });

        
    }
}