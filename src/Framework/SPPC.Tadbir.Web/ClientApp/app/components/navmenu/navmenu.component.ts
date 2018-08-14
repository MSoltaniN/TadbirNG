import { Component, Renderer2, Renderer, ElementRef, OnInit } from '@angular/core';
import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from "ng2-translate";
import { MetaDataService } from '../../service/metadata/metadata.service';
import { UserService, CommandInfo } from '../../service/user.service';
import { Command } from '../../model/command';
import { SessionKeys } from '../../enviroment';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent extends DefaultComponent implements OnInit {

    menuList: Array<Command> = new Array<Command>();
    public icons: { [id: string]: string; } = {};



    constructor(public toastrService: ToastrService,
        public translate: TranslateService, public renderer2: Renderer2,
        public metadata: MetaDataService, public el: ElementRef,
        public location: Location) {

        super(toastrService, translate, renderer2, metadata, '', '');

        var menus = sessionStorage.getItem(SessionKeys.Menu);
        if(menus)
            this.menuList = JSON.parse(menus);
        
        
    }

    public expandedSubMenuId: number = -1;
    public rightAlign: boolean = true;
    public profileItems: Array<Command>;

    ngOnInit() {


        if (this.CurrentLanguage == 'fa')
            this.rightAlign = false;

        var subMenuId = -1;
        for (let parent of this.menuList) {
            if (parent.children.findIndex(p => p.routeUrl.toLowerCase() == this.location.path().toLowerCase()) > -1) {
                this.expandedSubMenuId = parent.id;
                break;
            }
        }

        for (let parent of this.menuList) {
            if (parent.id == 15)
            {
                this.profileItems = new Array<Command>();
                for (let item of parent.children) {
                    this.profileItems.push(item);
                }
            }
        }
        
    }

    onMenuClick(event: any,id:number) {
        if(event)
            if (event.srcElement.className == "menuitem nav-link child")
                this.renderer2.removeClass(this.el.nativeElement.querySelector('.navbar-collapse'), 'in');

        for (let mnu of this.menuList) {
            if (id != mnu.id)
                this.renderer2.removeClass(this.el.nativeElement.querySelector('.navbar-collapse').querySelector('#div' + mnu.id.toString()), 'in');
        }

        event.srcElement.scrollIntoView();
        
    }

   

    isCollapsed: boolean = true;

   
}
