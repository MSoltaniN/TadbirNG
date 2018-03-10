import { Component } from '@angular/core';
import { Context } from "../../model/context";
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    currentContext?: Context = undefined;

    public showNavbar: boolean = false;

    public isLogin: boolean = false;

    public lang: string = '';

    //public langIsFa: boolean = false;
    
    constructor(location: Location,router: Router) {


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

        //if (this.lang == "fa")
        //    this.langIsFa = true;
        //else
        //    this.langIsFa = false;

        if (this.currentContext != undefined) {
            this.showNavbar = true;
        }

        router.events.subscribe((val) => {
            if (location.path() == '/login') {
                this.showNavbar = false;
                this.isLogin = true;
                
            }
            else
            {
                this.isLogin = false;
                this.showNavbar = true;
            }
        });
        

        

    }
}
