import { Component, Renderer2, ElementRef, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Command } from '../../model/command';
import { MessageType } from '../../../environments/environment';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { AuthenticationService } from '../../service/login/index';
import { SettingService } from '../../service/index';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { BrowserStorageService } from '../../service/browserStorage.service';

declare var $: any;

@Component({
  selector: 'nav-menu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent extends DefaultComponent implements OnInit, AfterViewInit {

  menuList: Array<Command> = new Array<Command>();
  public icons: { [id: string]: string; } = {};
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;

  constructor(public toastrService: ToastrService, private authenticationService: AuthenticationService, public bStorageService: BrowserStorageService,
    public translate: TranslateService, public renderer2: Renderer2, public router: Router,
    public metadata: MetaDataService, public el: ElementRef, public settingService: SettingService,
    public location: Location) {

    super(toastrService, translate, bStorageService, renderer2, metadata, settingService, '', undefined);
    
    var menus = this.bStorageService.getMenu()

    if (menus)
      this.menuList = JSON.parse(menus);
  }

  public expandedSubMenuId: number = -1;
  public rightAlign: boolean = true;
  public profileItems: Array<Command>;


  ngAfterViewInit() {
    $.fn.bindTree();
  }

  ngOnInit() {
    if (this.CurrentLanguage == 'fa')
      this.rightAlign = false;

    for (let parent of this.menuList) {
      if (parent.children.findIndex(p => p.routeUrl != null && p.routeUrl.toLowerCase() == this.location.path().toLowerCase()) > -1) {
        this.expandedSubMenuId = parent.id;
        break;
      }
    }

    for (let parent of this.menuList) {
      if (parent.id == 15) {
        this.profileItems = new Array<Command>();
        for (let item of parent.children) {
          this.profileItems.push(item);
        }
      }
    }
  }


  onClickMenu(item: Command) {
    if (item.hasPermission) {
      //for show report manager
      if (item.routeUrl == '/reports') {
        this.reportManager.showDialog();
        return;
      }

      if (item.routeUrl)
        this.router.navigate([item.routeUrl])
    }
    else
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
  }

  onMenuClick(event: any, id: number) {
    // if(event)
    //     if (event.srcElement.className == "menuitem child")
    //         this.renderer2.removeClass(this.el.nativeElement.querySelector('.navbar-collapse'), 'in');

    // for (let mnu of this.menuList) {
    //     if (id != mnu.id)
    //         this.renderer2.removeClass(this.el.nativeElement.querySelector('.navbar-collapse').querySelector('#div' + mnu.id.toString()), 'in');
    // }

    // event.srcElement.scrollIntoView();

  }

  clickMenu(event: any, id: number) {
    /*if(event)
         if (this.el.nativeElement.querySelector('#a' + id).parentElement.className.contains("parent treeview"))
         {
             if(this.el.nativeElement.querySelector('.menu-open1') !=null || this.el.nativeElement.querySelector('.menu-open') !=null)                  
             {                    
                 if(this.el.nativeElement.querySelector('.menu-open1 > ul.treeview-menu'))
                 {
                    this.renderer2.setStyle(this.el.nativeElement.querySelector('.menu-open1 > ul.treeview-menu'), 'display','none');
                    this.renderer2.removeClass(this.el.nativeElement.querySelector('.menu-open1'), 'menu-open1');
                    this.renderer2.removeClass(this.el.nativeElement.querySelector('.menu-open1'), 'menu-open');
                 }
                 if(this.el.nativeElement.querySelector('.menu-open > ul.treeview-menu'))
                 {
                    this.renderer2.setStyle(this.el.nativeElement.querySelector('.menu-open > ul.treeview-menu'), 'display','none');
                    this.renderer2.removeClass(this.el.nativeElement.querySelector('.menu-open'), 'menu-open');
                    this.renderer2.removeClass(this.el.nativeElement.querySelector('.menu-open'), 'menu-open1');
                 }
                
             }                 
            else if(this.el.nativeElement.querySelector('#menu' + id).display != 'block')
            {
                this.renderer2.addClass(this.el.nativeElement.querySelector('#a' + id).parentElement,"menu-open1");
                this.renderer2.setStyle(this.el.nativeElement.querySelector('#menu' + id), 'display','block');
            }
         }
        else if  (this.el.nativeElement.querySelector('#a' + id).parentElement.className.contains("menu-open")
        || this.el.nativeElement.querySelector('#a' + id).parentElement.className.contains("menu-open1"))
        //|| this.el.nativeElement.querySelector('#a' + id).parentElement.className == "parent treeview menu-open")
        {                

            this.renderer2.removeClass(this.el.nativeElement.querySelector('#a' + id).parentElement,"menu-open1");
            this.renderer2.removeClass(this.el.nativeElement.querySelector('#a' + id).parentElement,"menu-open");
            this.renderer2.setStyle(this.el.nativeElement.querySelector('#menu' + id), 'display','none');
        }
     
    */

  }



  isCollapsed: boolean = true;

}
