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

    template: string = `<div class="sk-cube-grid">
        <div class="sk-cube sk-cube1"></div>
        <div class="sk-cube sk-cube2"></div>
        <div class="sk-cube sk-cube3"></div>
        <div class="sk-cube sk-cube4"></div>
        <div class="sk-cube sk-cube5"></div>
        <div class="sk-cube sk-cube6"></div>
        <div class="sk-cube sk-cube7"></div>
        <div class="sk-cube sk-cube8"></div>
        <div class="sk-cube sk-cube9"></div>
    </div>`;

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
