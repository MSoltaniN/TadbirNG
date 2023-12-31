import { DOCUMENT, Location } from '@angular/common';
import { AfterViewInit, Component, ElementRef, HostListener, Inject, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { UserService } from '@sppc/admin/service';
import { AuthenticationService, Context } from '@sppc/core';
import { environment } from '@sppc/env/environment';
import { ServiceLocator } from '@sppc/service.locator';
import { Command } from '@sppc/shared/models';
import { ShortcutCommand } from '@sppc/shared/models/shortcutCommand';
import { BrowserStorageService, LicenseService } from '@sppc/shared/services';
import { ShareDataService } from '@sppc/shared/services/share-data.service';
import { ShortcutService } from '@sppc/shared/services/shortcut.service';
import { LicenseApi } from './shared/services/api/licenseApi';
import { AutoGeneratedGridComponent, AutoGridExplorerComponent } from './shared/class';
import { FiscalPeriodComponent } from './organization/components/fiscalPeriod/fiscalPeriod.component';

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

  scopeService: ShareDataService;  
  stimulsoftRegister : any;

  ngOnInit() {    
    
    this.stimulsoftRegister = setInterval(()=>{
      
      if(Stimulsoft)
      { 
        clearInterval(this.stimulsoftRegister);

        this.registerFunctions();
        this.addStimulsoftFonts();
      }
      
    } , 20000);
    
    this.manageComponentScopes();

    this.setAliveSessionTimer();    
  }

  setAliveSessionTimer()
  {    
      setInterval(()=>{
        if(this.bStorageService.getCurrentUser())
        {
          this.licenseService.PutSessionAsActive(LicenseApi.SetCurrentSessionAsActiveUrl).subscribe();
        }
      },environment.SessionAliveInterval);    
  }

  addStimulsoftFonts()
  {
    //load fonts     
    Stimulsoft.StiOptions.Export.Pdf.AllowImportSystemLibraries = true;
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BZar.ttf", "B Zar");
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BZar.ttf", "B Zar", Stimulsoft.System.Drawing.FontStyle.Bold);
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BTitrBold.ttf", "B Titr", Stimulsoft.System.Drawing.FontStyle.Bold);
    var language = localStorage.getItem('lang');
    if (language == "fa") {

      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/IRANSansWeb.ttf", "IRANSansWeb");
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/IRANSansWeb_Bold.ttf", "IRANSansWeb", Stimulsoft.System.Drawing.FontStyle.Bold);        

    }
    else {
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/IranSans/ttf/IRANSansWeb.ttf", "IRANSansWeb");
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/IranSans/ttf/IRANSansWeb_Bold.ttf", "IRANSansWeb", Stimulsoft.System.Drawing.FontStyle.Bold);
    }  
  }

  manageComponentScopes()
  {
    this.scopeService = ServiceLocator.injector.get(ShareDataService);
    
    this.scopeService.getScope().subscribe((component) => {      
      if (component) {
        var componentName = component.selector?.toString().toLowerCase();
        if(ShareDataService.exceptionComponents.findIndex(s=>s == componentName) == -1
        && ShareDataService.components.findIndex(s=>s.selector.toString().toLowerCase() == componentName) == -1)
          ShareDataService.components.unshift(component);
      }
      else
      {
        if(ShareDataService.removedComponent)
        {
            var findIndex = ShareDataService.components.findIndex(s=>s.selector.toString().toLowerCase()  == ShareDataService.removedComponent.selector.toString().toLowerCase());
            if(findIndex >= 0)
            {
              ShareDataService.removedComponent = undefined;
              ShareDataService.components.splice(findIndex,1)
            }
        }     
      }
    })
  }

  registerFunctions() {    
        Stimulsoft.Report.Dictionary.StiFunctions.addFunction("TadbirFunctions", "Accounting", "TestFunction",
        "this is a test function", "", typeof (String), "", [typeof (String)], [""], [""], function (value) {
          var result: string = value;
          return result.toUpperCase();
        });

        Stimulsoft.Report.Dictionary.StiFunctions.addFunction("TadbirFunctions", "Accounting", "ToShamsi",
        "Convert miladi date to shamsi", "", typeof (String), "", [typeof (String)], [""], [""], function (value) {
          
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
    private shortcutService:ShortcutService,
    public elem:ElementRef,
    private licenseService:LicenseService) {       

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
    //if kendodialog is activated on screen exit from function
    

    this.replaceBadCharacters(event);

    if (event.key != "Control" && event.key != "Shift" && event.key != "Alt") {      

      var ctrl = event.ctrlKey ? true : false;
      var shift = event.shiftKey ? true : false;
      var alt = event.altKey ? true : false;
      var activeComponents = new Array();

      var isDialog : boolean = false;

      if (event.code) {
        var result = false;
        var activeElement = document.activeElement;
        var parentElement = null;

        if(activeElement.tagName.toLowerCase() == "kendo-dialog")          
            parentElement = activeElement          
        else
          parentElement = (<any>document.activeElement.parentNode).closest('kendo-dialog');
        
        if(!parentElement) 
        {
          parentElement = document;
        }
        else
        {
          isDialog = true;
        }

        
        var components = ShareDataService.components; 
        console.log('shared components', components);
        var selectors = "";
        components.forEach((item)=>{
          if(parentElement.querySelector(item.selector))
          {
            selectors += item.selector + ",";
          }            
        });

        if(selectors == "") return;

        selectors = selectors.substring(0,selectors.length - 1);
        console.log('selectors', selectors);
        var elements = parentElement.querySelectorAll(selectors);
        console.log(elements);
        if(elements.length > 0)
        {
          elements.forEach((item)=>{
            var parentSelector = item.tagName;
            var index = components.findIndex(c=>c.selector.toLowerCase() == parentSelector.toLowerCase());
            activeComponents.push(components[index]);
          });            
        }

        if(!isDialog)
        { 
          result = this.checkKeysForNavigate(ctrl,shift,alt,event.code);
        }
        
        if(!result)
        {
          this.checkKeysForCommand(ctrl,shift,alt,event.code,activeComponents);
        }
      }

    }  
  }  

  checkKeysForNavigate(ctrl,shift,alt,code):boolean
  {
    var key = code.replace('Key', '').toLowerCase();

    var menus = this.bStorageService.getMenu();
    if (menus) {
      this.menuList = JSON.parse(menus);
      var command = this.searchHotKey(ctrl, shift, alt, key, this.menuList);
      if (command) {
        var url = command.routeUrl;

        if (url == '/tadbir/reports') return;        

        this.router.navigate([url]);

        event.preventDefault();

        return true;
      }
    }

    return false;
  }

  checkAutoGridExplorerComponent(componentName: any): boolean{
    return componentName instanceof FiscalPeriodComponent ? true : false;
  }

  checkAutoGeneratedGridComponent(componentName: any): boolean{
    return componentName instanceof AutoGeneratedGridComponent ? true : false;
  }
  
  

  checkKeysForCommand(ctrl,shift,alt,code,activeComponents:any[])
  {
    var key = code.replace('Key', '').toLowerCase();
    var shortcuts: ShortcutCommand[];
    
    shortcuts = JSON.parse(this.bStorageService.getShortcut())
    var shortcutCommand = this.shortcutService.searchShortcutCommand(ctrl, shift, alt, key, shortcuts);
    if (shortcutCommand) {          
      if(shortcutCommand.scope)
      {       
        var scopeIndex = -1;    
        // debugger
        // if(shortcutCommand.scope.indexOf(",") == -1)
        // {           
        //   scopeIndex = activeComponents.findIndex(f=> f.scopes && f.scopes.findIndex(scope => scope.toLowerCase() == shortcutCommand.scope.toLowerCase()) >= 0);          
        // }
        // else
        // {    
          var scopes = shortcutCommand.scope.toLowerCase().split(",");
          scopeIndex = activeComponents.findIndex(f=> f.scopes && f.scopes.findIndex(scope => scopes.find(s=>s == scope.toLowerCase())) >= 0);             
                    
        //}
        console.log(activeComponents);
        console.log(scopes);
        console.log(scopeIndex);
        if(scopeIndex >= 0)
        {          
          var component = activeComponents[scopeIndex]; 
          component[shortcutCommand.method]();
          event.preventDefault();          
        }        
      }      
    }
  }

  replaceBadCharacters(event)
  {
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
        var result = this.shortcutService.hotkeyUsed(ctrl, alt, shift, key, command);
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
