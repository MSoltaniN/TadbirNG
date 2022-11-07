import { Component, Renderer2, ElementRef, OnInit, AfterViewInit, ViewChild, HostListener } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { AuthenticationService } from '@sppc/core';
import { environment } from "@sppc/env/environment";
import { MessageType } from '@sppc/shared/enum/metadata';
import { DefaultComponent } from '@sppc/shared/class';
import { Command } from '@sppc/shared/models';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { DialogService } from '@progress/kendo-angular-dialog';
import { LicenseInfoComponent } from '../dashboard/license-info.component';
import { ServiceLocator } from '@sppc/service.locator';
import { ShareDataService } from '@sppc/shared/services/share-data.service';




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
  paths: number[] = [];
  currentRoute: string;
  versionTitle: string;
  @ViewChild('li') menuItems: Array<ElementRef>;

  scopeService: ShareDataService;

  constructor(public toastrService: ToastrService, private authenticationService: AuthenticationService, public bStorageService: BrowserStorageService,
    public translate: TranslateService, public renderer2: Renderer2, public router: Router,
    public metadata: MetaDataService, public el: ElementRef, public settingService: SettingService,
    public location: Location, private dialogService: DialogService,public elem:ElementRef) {

    super(toastrService, translate, bStorageService, renderer2, metadata, settingService, '', undefined);

    this.scopeService = ServiceLocator.injector.get(ShareDataService);
    this.scopeService.setScope(this);

    if(elem)    
    {
      this.selector = elem.nativeElement.tagName.toLowerCase();      
    }

    var menus = this.bStorageService.getMenu()

    if (menus)
      this.menuList = JSON.parse(menus);

    this.currentRoute = this.router.url;   
    
    this.versionTitle = environment.Version;
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

    var menu: Command = null;
    this.menuList.forEach((element) => {
      menu = this.searchTree(element, this.router.url);
      if (menu != null) {
        return;
      }
    });

    for (let parent of this.menuList) {
      if (parent.id == 15) {
        this.profileItems = new Array<Command>();
        for (let item of parent.children) {
          this.profileItems.push(item);
        }
      }
    }
  }

  searchTree(element: Command, route) {
    if (element.routeUrl == route) {
      return element;
    } else if (element.children != null) {
      var i;
      var result = null;
      for (i = 0; result == null && i < element.children.length; i++) {
        result = this.searchTree(element.children[i], route);
        if (result != null) {
          this.paths.push(element.id);
          return result;
        }
      }
      return result;
    }
    return null;
  }

  searchActiveMenu(id: number) {
    return this.paths.findIndex(f => f === id) > -1 ? true : false;
  }


  onClickMenu(item: Command) {
    //for show report manager
    if (item.routeUrl == '/tadbir/reports') {      
      this.reportManager.showDialog();
      return;
    }

    if (item.routeUrl) {
      this.router.navigate([item.routeUrl])

      if (document.querySelector('li.active'))
        document.querySelector('li.active').classList.remove('active');

      if (document.querySelector('#cmd' + item.id))
        document.querySelector('#cmd' + item.id).classList.add('active');
    }
  }

  onMenuClick(event: any, id: number) {   

  }

  clickMenu(event: any, id: number) {

  }

  versionInfoClick() {
    var dialogRef = this.dialogService.open({
      title: this.getText("App.Name"),
      content: LicenseInfoComponent,
      actions: [{ text: this.getText("Buttons.Ok"), primary: true }],
      width: 480,
      height: 400,
      minWidth: 250
    });

    dialogRef.result.subscribe(() => {
      dialogRef.close();
    });

    dialogRef.content.instance.cancel.subscribe((res) => {
      dialogRef.close();
    });
  }

  isCollapsed: boolean = true;

  @HostListener('mouseenter') onMouseEnter() {
    let body = document.querySelector('body');
    if (body.classList.contains('siderbar-closed')) {
      body.classList.remove('sidebar-collapse');
    }
  }

  @HostListener('mouseleave') 
  onMouseLeave() {
    let body = document.querySelector('body');
    if (body.classList.contains('siderbar-closed')) {
      body.classList.add('sidebar-collapse');
    }
  }
}
