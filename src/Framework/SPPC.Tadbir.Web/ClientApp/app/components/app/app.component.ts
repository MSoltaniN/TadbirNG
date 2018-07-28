import { Component, Inject, Injector } from '@angular/core';
import { Context } from "../../model/context";
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DOCUMENT } from '@angular/platform-browser';
import { AuthenticationService } from '../../service/login/index';
import { UserService } from '../../service/user.service';
import { HotkeysService, Hotkey } from 'angular2-hotkeys';
import { SessionKeys } from '../../enviroment';
import { Command } from '../../model/command';
import { ToastrService } from 'ngx-toastr';
import { SppcLoadingService } from '../../controls/sppcLoading/sppc-loading.service';
import { TranslateService } from 'ng2-translate';


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
    
    constructor(location: Location, public router: Router, public authenticationService: AuthenticationService,public userService:UserService,
        @Inject(DOCUMENT) private document: Document) {

        //#region init Lang

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

        //#endregion

        //#region Hide navbar
        if (this.currentContext != undefined) {
            this.showNavbar = true;
        }

        //#endregion

        //#region Event in Each Route 

        router.events.subscribe((val) => {
            if (location.path().toLowerCase() == '/login' || location.path().toString().indexOf('/login?returnUrl=') >= 0) {
                this.showNavbar = false;

                this.isLogin = true;

            }
            else {

                //#region add class to element

                this.isLogin = false;
                this.showNavbar = true;
                
                var spacePad = this.document.getElementById('spacePad')
                var currentLang = localStorage.getItem('lang')
                if (currentLang == 'fa' || currentLang == null) {
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

                //#endregion

                //#region init enviroment variables

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



                    var fps = this.authenticationService.getFiscalPeriod(companyId, ticket);
                    if (fps != null) {
                        fps.subscribe(res => {
                            //this.fiscalPeriods = res;
                            this.fiscalPeriodName = res.filter((p: any) => p.key == fpId)[0].value;
                        });
                    }

                    var branchList = this.authenticationService.getBranches(companyId, ticket);
                    if (branchList != null) {
                        branchList.subscribe(res => {
                            this.branchName = res.filter((p: any) => p.key == branchId)[0].value;
                        });
                    }


                    var companiesList = this.authenticationService.getCompanies(this.userName, ticket);
                    if (companiesList != null) {
                        companiesList.subscribe(res => {
                            this.companyName = res.filter((p: any) => p.key == companyId)[0].value;;

                        });
                    }

                }

                //#endregion
            }
        });

        //#endregion

        //this.initHotKeys();
    }


    
    public hotKeyMap: { [id: string]: string; } = {}
    menuList: Array<Command> = new Array<Command>();

    public errMessage: string;
    public errCode: number;
    public showErrorDialog: boolean = false;

    private closeForm(): void {
        this.showErrorDialog = false;
    }

    doSomething(event: any) {
        // read keyCode or other properties 
        // from event and execute a command
        var ctrl = event.ctrlKey ? 'ctrl' : '';
        var shift = event.shiftKey ? 'shift' : '';
        var alt = event.altKey ? 'alt' : '';
        
        var key = event.code.replace('Key', '').toLowerCase();
        
        var url = '';

        var menus = sessionStorage.getItem(SessionKeys.Menu);
        if (menus) {
            this.menuList = JSON.parse(menus);

            for (var m of this.menuList) {

                var shortcutFound: boolean = true;
                if (m.hotKey == null) continue;
                var it = m.hotKey.toLowerCase();
                if (it.indexOf('ctrl') >= 0) {
                    if (ctrl == '' || it.indexOf(ctrl) == -1) {
                        shortcutFound = false;
                    }
                }

                if (it.indexOf('alt') >= 0) {
                    if (alt == '' || it.indexOf(alt) == -1) {
                        shortcutFound = false;
                    }
                }

                if (it.indexOf('shift') >= 0) {
                    if (shift == '' || it.indexOf(shift) == -1) {
                        shortcutFound = false;
                    }
                }

                if (it.indexOf('+' + key) == -1)
                    shortcutFound = false;

                if (shortcutFound) {
                    url = m.routeUrl;
                    this.router.navigate([url]);
                    return;
                }

            }
        }
        
    }
    
    initHotKeys() {

        var menuList: Array<Command> = new Array<Command>();
        var hotKeys: Array<string> = new Array<string>();
        

        var menus = sessionStorage.getItem(SessionKeys.Menu);
        if (menus)
            menuList = JSON.parse(menus);

        menuList.forEach((obj: Command) => {
            if (obj.hotKey != '' && obj.hotKey != null) {
                this.hotKeyMap[obj.hotKey.toLowerCase()] = obj.routeUrl;
                hotKeys.push(obj.hotKey.toLowerCase());
            }
        });
        
    }
    
}
