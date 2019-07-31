import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService, CompanyLoginInfo } from '../../service/login/index';
import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from 'ngx-toastr';
import { Renderer2 } from '@angular/core';
import { ContextInfo } from "../../service/login/authentication.service";
import { MessageType, Layout, MessagePosition} from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { TranslateService } from '@ngx-translate/core';
import { UserService, SettingService } from '../../service/index';
import { Command } from '../../model/command';
import { ListFormViewConfig } from '../../model/listFormViewConfig';
import { BrowserStorageService } from '../../service/browserStorage.service';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  key: number,
  value: string
}

@Component({
  selector: 'logincomplete',
  templateUrl: 'login.complete.component.html',
  styleUrls: ['./login.complete.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})


export class LoginCompleteComponent extends DefaultComponent implements OnInit {

  //#region Fields
  model: any = {};
  loading = false;
  returnUrl: string;
  ticket: string = '';

  public disabledBranch: boolean = true;
  public disabledFiscalPeriod: boolean = true
  public disabledCompany: boolean = true;

  public compenies: Array<Item> = [];
  public branches: Array<Item> = [];
  public fiscalPeriods: Array<Item> = [];

  public companyId: string = '';
  public branchId: string = '';
  public fiscalPeriodId: string = '';

  currentRoute: string;

  //#endregion

  //#region Constructor
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    public toastrService: ToastrService,
    public translate: TranslateService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public userService: UserService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);

  }

  //#endregion

  //#region Events

  ngOnInit() {    
    this.currentRoute = this.bStorageService.getCurrentRoute();
    this.disabledCompany = true;
    this.getCompany();


    // var currentLang = localStorage.getItem('lang')
    // if(currentLang == 'fa')
    //      this.document.getElementById('adminlte').setAttribute('href', 'assets/dist/css/AdminLTE.Rtl.css');
    // else
    //      this.document.getElementById('adminlte').setAttribute('href', 'assets/dist/css/AdminLTE.min.css');

    // this.document.getElementById('adminlteSkin').setAttribute('href', 'assets/dist/css/skins/_all-skins.min.css');


  }

  //#endregion

  //#region Methods

  public companyChange(value: any): void {

    this.disabledBranch = true;
    this.disabledFiscalPeriod = true;

    this.branches = [];
    this.branchId = undefined;

    this.fiscalPeriods = [];
    this.fiscalPeriodId = undefined;

    this.getBranch(value);
    this.getFiscalPeriod(value);

    var lastBranchId = this.bStorageService.getLastUserBranch(this.UserId, this.companyId);
    var lastFpId = this.bStorageService.getLastUserFpId(this.UserId, this.companyId);

    if (lastBranchId)
      this.branchId = lastBranchId;

    if (lastFpId)
      this.fiscalPeriodId = lastFpId;

  }

  getCompany() {
    this.authenticationService.getCompanies(this.UserName, this.Ticket).subscribe(res => {
      this.compenies = res;
      this.disabledCompany = false;

      //#region load current setting
      if (this.CompanyId) {
        this.companyId = this.CompanyId.toString();
        this.companyChange(this.companyId);
      }
      //#endregion
    });;
  }


  getBranch(companyId: number) {
    this.authenticationService.getBranches(companyId, this.Ticket).subscribe(res => {
      this.disabledBranch = false;
      this.branches = res;
    });
  }

  getFiscalPeriod(companyId: number) {

    this.authenticationService.getFiscalPeriod(companyId, this.Ticket).subscribe(res => {
      this.disabledFiscalPeriod = false;
      this.fiscalPeriods = res;
    });
  }

  isValidate(): boolean {
    var isValidate: boolean = true;

    if (this.companyId == '') {
      this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
      this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
      this.showMessage(this.getText("AllValidations.Login.CompanyIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);

      isValidate = false;
      return isValidate;
    }

    if (this.branchId == '') {
      this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
      isValidate = false;
    }

    if (this.fiscalPeriodId == '') {
      this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
      isValidate = false;
    }

    return isValidate;
  }

  selectParams() {

    //sessionStorage.removeItem("viewTreeConfig");

    if (this.isValidate()) {

      if (this.authenticationService.islogin()) {

        this.getCompanyTicket();

      }
    }
  }

  onCancleClick() {
    if (this.authenticationService.islogin()) {
      var currentUser = this.bStorageService.getCurrentUser();
      if (currentUser != null) {
        this.companyId = currentUser.companyId.toString();
        this.branchId = currentUser.branchId.toString();
        this.fiscalPeriodId = currentUser.fpId.toString();

        this.loadMenuAndRoute(currentUser);
      }
    }
  }


  loadAllSetting() {
    this.bStorageService.checkVersion(this.version, this.UserId);

    this.settingService.getListSettingsByUser(this.UserId).subscribe((res: Array<ListFormViewConfig>) => {

      if (res)
        this.bStorageService.setUserSetting(res, this.UserId);
    });

    this.bStorageService.removeSelectedDateRange();
  }


  loadMenuAndRoute(currentUser: ContextInfo) {
    //#region load menu
    var menuList: Array<Command> = new Array<Command>();

    var commands: any;

    this.authenticationService.getFiscalPeriodById(currentUser.fpId, this.Ticket).subscribe(res => {

      this.bStorageService.setFiscalPeriod(res);

    })

    this.userService.getCurrentUserCommands(this.Ticket).subscribe((res: Array<Command>) => {
      var list: Array<Command> = res;

      this.bStorageService.setCurrentContext(currentUser);
      this.bStorageService.setMenu(res);


      if (this.route.snapshot.queryParams['returnUrl'] != undefined) {
        var url = this.route.snapshot.queryParams['returnUrl'];
        this.router.navigate([url]);
      }
      else {
        var currentRoute = this.bStorageService.getCurrentRoute();
        if (currentRoute) {
          this.bStorageService.removeCurrentRoute();
          this.router.navigate([currentRoute]);
        }
        else {
          this.router.navigate(['/dashboard']);
        }
      }

    });

    this.userService.getDefaultUserCommands(this.Ticket).subscribe((res: Array<Command>) => {
      var list: Array<Command> = res;

      this.bStorageService.setProfile(res);

    });





    //#endregion
  }

  /**
   * تیکت امنیتی را مطابق شرکت و شعبه و دوره مالی از سرویس میگیرد و جایگزین تیکت قبلی میکند
   */
  getCompanyTicket() {

    var companyLoginModel = new CompanyLoginInfo();
    companyLoginModel.companyId = parseInt(this.companyId);
    companyLoginModel.branchId = parseInt(this.branchId);
    companyLoginModel.fiscalPeriodId = parseInt(this.fiscalPeriodId);

    this.authenticationService.getCompanyTicket(companyLoginModel, this.Ticket).subscribe(res => {

      if (res.headers != null) {
        let newTicket = res.headers.get('X-Tadbir-AuthTicket');
        let contextInfo = <ContextInfo>res.body;

        var currentUser = this.bStorageService.getCurrentUser();
        if (currentUser != null) {

          currentUser.branchId = parseInt(this.branchId);
          currentUser.companyId = parseInt(this.companyId);
          currentUser.fpId = parseInt(this.fiscalPeriodId);
          currentUser.permissions = JSON.parse(atob(this.Ticket)).user.permissions;
          currentUser.fiscalPeriodName = contextInfo.fiscalPeriodName;
          currentUser.branchName = contextInfo.branchName;
          currentUser.companyName = contextInfo.companyName;
          currentUser.ticket = newTicket;

          this.bStorageService.setCurrentContext(currentUser);
          this.bStorageService.setLastUserBranchAndFpId(this.UserId, this.companyId, this.branchId, this.fiscalPeriodId);

          this.loadMenuAndRoute(currentUser);

          this.loadAllSetting();
        }

      }

    })
  }

  //#endregion





}
