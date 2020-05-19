import { Component, Inject, AfterViewInit, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DOCUMENT, DomSanitizer } from '@angular/platform-browser';
import { Context, AuthenticationService } from '@sppc/core';
import { BrowserStorageService } from '@sppc/shared/services';
import { UserService } from '@sppc/admin/service';
import { Command } from '@sppc/shared/models';

declare var $: any;
declare var Stimulsoft: any;

@Component({
  selector: 'app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements AfterViewInit, OnInit {


  options = {
    min: 8,
    max: 100,
    ease: 'linear',
    speed: 200,
    trickleSpeed: 300,
    meteor: true,
    spinner: true,
    spinnerPosition: 'right',
    direction: 'ltr+',
    color: 'white',
    thick: false
  };


  currentContext?: Context = undefined;

  public isRtl: boolean;
  public lang: string = '';

  ngOnInit() {

    //load fonts
    Stimulsoft.StiOptions.Export.Pdf.AllowImportSystemLibraries = true;
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BZar.ttf", "B Zar");
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BZar.ttf", "B Zar", Stimulsoft.System.Drawing.FontStyle.Bold);
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BTitrBold.ttf", "B Titr", Stimulsoft.System.Drawing.FontStyle.Bold);
    var language = localStorage.getItem('lang');
    if (language == "fa") {

      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/IRANSansWeb.ttf", "IRANSansWeb");
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/IRANSansWeb_Bold.ttf", "IRANSansWeb", Stimulsoft.System.Drawing.FontStyle.Bold);
      //assets/resources/fonts/IranSans-En/ttf

    }
    else {
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/IranSans/ttf/IRANSansWeb.ttf", "IRANSansWeb");
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/IranSans/ttf/IRANSansWeb_Bold.ttf", "IRANSansWeb", Stimulsoft.System.Drawing.FontStyle.Bold);
    }
    //Stimulsoft.System.Drawing.FontStyle.Italic

    this.registerFunctions();
  }

  registerFunctions() {
    Stimulsoft.Report.Dictionary.StiFunctions.addFunction("TadbirFunctions", "Accounting", "TestFunction",
      "this is a test function", "", typeof (String), "", [typeof (String)], [""], [""], function (value) {
        var result: string = value;
        return result.toUpperCase();
      });

    Stimulsoft.Report.Dictionary.StiFunctions.addFunction("TadbirFunctions", "Accounting", "ToShamsi",
      "Convert miladi date to shamsi", "", typeof (String), "", [typeof (String)], [""], [""], function (value) {
        /*if (value == null || value == undefined)
          return "";
        
        moment.locale('en');
        let MomentDate = moment(value).locale('fa').format("YYYY/MM/DD");
        return MomentDate;*/
        return "Test";

      });
  }


  constructor(location: Location,
    public router: Router,
    public authenticationService: AuthenticationService,
    public bStorageService: BrowserStorageService,
    public userService: UserService,
    @Inject(DOCUMENT) private document: Document,
    public sanitizer: DomSanitizer) {

    //#region init Lang    

    this.currentContext = this.bStorageService.getCurrentUser();

    var language = this.bStorageService.getLanguage();
    if (language) {
      this.lang = language;
    }
    else {
      this.lang = "fa";

    }

    //#endregion

    if (!this.bStorageService.islogin()) {

      //#region add class to element
      var spacePad = this.document.getElementById('spacePad')
      var currentLang = this.bStorageService.getLanguage();
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

      var currentSkin = this.bStorageService.getCurrentSkin();
      if (currentSkin != null) {
        if (!this.document.getElementById('mainBody').classList.contains(currentSkin)) {
          this.document.getElementById('mainBody').classList.add(currentSkin);
          this.document.getElementById('mainBody').classList.remove('skin-blue');
        }

        switch (currentSkin) {
          case 'skin-blue':
            this.document.getElementById('theme').setAttribute('href', 'assets/resources/powder-blue.css?dt=' + Date.now());
            break;
          case 'skin-purple':
            this.document.getElementById('theme').setAttribute('href', 'assets/resources/purple.css?dt=' + Date.now());
            break;
          case 'skin-yellow-light':
            this.document.getElementById('theme').setAttribute('href', 'assets/resources/vintage.css?dt=' + Date.now());
            break;
          case 'skin-black-light':
            this.document.getElementById('theme').setAttribute('href', 'assets/resources/nordic.css?dt=' + Date.now());
            break;

        }

      }

      var lang = this.bStorageService.getLanguage();
      if (lang == 'fa' || lang == null) {
        if (this.document.getElementById('sppcFont').getAttribute('href') != 'assets/resources/IranSans.css')
          this.document.getElementById('sppcFont').setAttribute('href', 'assets/resources/IranSans.css');
      }
      else {
        if (this.document.getElementById('sppcFont').getAttribute('href') != 'assets/resources/IranSans-en.css')
          this.document.getElementById('sppcFont').setAttribute('href', 'assets/resources/IranSans-en.css');
      }

      //#endregion

      //#region set current route to session
      var currentRoute = this.bStorageService.getCurrentRoute();      
      var currentUrl = location.path();

      if (currentRoute && currentRoute != currentUrl)
        this.bStorageService.setPreviousRoute(currentRoute);

      if (currentUrl != '/logout' && currentUrl != '/login')
        this.bStorageService.setCurrentRoute(currentUrl);
      //#endregion
    }

  }

  cssUrl: string;



  ngAfterViewInit() {
    $.fn.bindTree();
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

    var menus = this.bStorageService.getMenu();
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


    var menus = this.bStorageService.getMenu();
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
