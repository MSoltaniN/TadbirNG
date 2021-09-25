import { Component, Inject, AfterViewInit, OnInit, HostListener, ChangeDetectorRef, Renderer } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DOCUMENT, DomSanitizer } from '@angular/platform-browser';
import { Context, AuthenticationService } from '@sppc/core';
import { BrowserStorageService } from '@sppc/shared/services';
import { UserService } from '@sppc/admin/service';
import { Command } from '@sppc/shared/models';


declare var $: any;
declare var Stimulsoft: any;

declare global {
  interface StringConstructor {
    replaceBadChars(s: string): string;
  }
}

String.replaceBadChars = (s: string) => {  
  return s;
};

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
    public sanitizer: DomSanitizer,
    private renderer: Renderer) {

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

  @HostListener('window:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key != "Control" && event.key != "Shift" && event.key != "Alt") {
      

      var ctrl = event.ctrlKey ? true : false;
      var shift = event.shiftKey ? true : false;
      var alt = event.altKey ? true : false;

      if (event.code) {

        var key = event.code.replace('Key', '').toLowerCase();

        var menus = this.bStorageService.getMenu();
        if (menus) {
          this.menuList = JSON.parse(menus);
          var command = this.searchHotKey(ctrl, shift, alt, key, this.menuList);
          if (command) {
            var url = command.routeUrl;
            this.router.navigate([url]);

            event.preventDefault();
          }
        }
      }
    }

    var element: any = event.target;
    if ((event.key == 'ي') || (event.key == 'ك') && (element.parentNode.className == 'k-filtercell-wrapper')) {      

      event.target.addEventListener('blur', function () {        
        this.value = this.value.toString().replaceBadChars(this.value.toString());
      });          

    }


  }  

  /**
   * برای جستجو در شورت کات های منو بکار میرود
   * @param ctrl
   * @param shift
   * @param alt
   * @param key
   * @param commands
   */
  searchHotKey(ctrl: boolean,shift:boolean,alt :boolean , key:string, commands:Command[]) : Command {
    for (let command of commands) {
      if (command.hotKey != null) {
        var result = this.hotkeyUsed(ctrl, alt, shift, key, command);
        if (result) return result;        
      }

      if (command.children !== undefined && command.children.length > 0) {
        let childsearch = this.searchHotKey(ctrl, shift, alt, key, command.children);
        if (childsearch !== undefined) {
          return childsearch
        }
      }
    }
    return undefined;
  }

  
  /**
   * در یک کامند جستجو میکند و تطابق شورت کات زا در کامند بررسی میکند
   * @param ctrl
   * @param alt
   * @param shift
   * @param key
   * @param command
   */
  hotkeyUsed(ctrl: boolean, alt: boolean, shift: boolean, key: string, command: any) {
    var ctrlFound: boolean = false;
    var altFound: boolean = false;
    var shiftFound: boolean = false;
    var keyFound: boolean = false;

    var it = command.hotKey.toLowerCase();
    if (it.indexOf('ctrl') >= 0) {
      ctrlFound = true;
    }

    if (it.indexOf('alt') >= 0) {
      altFound = true;
    }
       
    if (it.indexOf('shift') >= 0) {
      shiftFound = true;
    }
    

    if (it.indexOf('+' + key) >= 0)
      keyFound = true;
       

    if ((ctrlFound && shiftFound && altFound) && keyFound) {
      if (ctrl && shift && alt)
        return command;
    }
    else if ((ctrlFound && shiftFound) && keyFound) {
      if (ctrl && shift)
        return command;
    }
    else if ((ctrlFound && altFound) && keyFound)  {
      if (ctrl && alt)
        return command;
    }
    else if ((shiftFound && altFound) && keyFound)  {
      if (shift && alt)
        return command;
    }
    else if ((ctrlFound) && keyFound){
      if (ctrl) 
        return command;
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
