import { Component, Renderer2, Renderer, ElementRef } from '@angular/core';
import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from "ng2-translate";
import { MetaDataService } from '../../service/metadata/metadata.service';
import { UserService } from '../../service/user.service';
import { Command } from '../../model/command';
import { SessionKeys } from '../../enviroment';


@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent extends DefaultComponent {

    menuList: Array<Command> = new Array<Command>();
    public icons: { [id: string]: string; } = {};

    constructor(public toastrService: ToastrService,
        public translate: TranslateService, public renderer2: Renderer2, public metadata: MetaDataService,public el: ElementRef) {

        super(toastrService, translate, renderer2, metadata, '', '');

        var menus = sessionStorage.getItem(SessionKeys.Menu);
        if(menus)
            this.menuList = JSON.parse(menus);
        
       
    }

    onMenuClick() {
        this.renderer2.removeClass(this.el.nativeElement.querySelector('.navbar-collapse'), 'in');
    }

    isCollapsed: boolean = true;

   
}
