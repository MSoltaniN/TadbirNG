import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Renderer2 } from '@angular/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { TranslateService } from '@ngx-translate/core';
import { Layout, MessageType, MessagePosition } from '@sppc/env/environment';
import { AuthenticationService, ContextInfo, CompanyLoginInfo } from '@sppc/core';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { UserService } from '@sppc/admin/service';
import { Command, ListFormViewConfig } from '@sppc/shared/models';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { DefaultComponent } from '@sppc/shared/class';
import { InitialWizardComponent } from '@sppc/organization/components/initialWizard/initialWizard.component';


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



  public dialogRef: DialogRef;
  public dialogModel: any;
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
    public bStorageService: BrowserStorageService,
    public dialogService: DialogService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);

  }

  //#endregion

  //#region Events

  ngOnInit() {
    this.currentRoute = this.bStorageService.getCurrentRoute();
    this.disabledCompany = true;
    this.getCompany();
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

      if (this.compenies.length == 0 && this.IsAdmin) {
        // create new company, branch and fiscalperiod
        this.createCompany();
      }
      else {
        this.disabledCompany = false;
        //load current setting
        if (this.CompanyId) {
          this.companyId = this.CompanyId.toString();
          this.companyChange(this.companyId);
        }
      }
    });
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
      //this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
      //this.showMessage(this.getText("AllValidations.Login.CompanyIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);

      isValidate = false;
      return isValidate;
    }

    //if (this.branchId == '') {
    //  this.showMessage(this.getText("AllValidations.Login.BranchIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
    //  isValidate = false;
    //}

    //if (this.fiscalPeriodId == '') {
    //  this.showMessage(this.getText("AllValidations.Login.FiscalPeriodIsRequired"), MessageType.Info, '', MessagePosition.TopCenter);
    //  isValidate = false;
    //}

    return isValidate;
  }

  selectParams() {
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
    if (currentUser.fpId) {
      this.authenticationService.getFiscalPeriodById(currentUser.fpId, this.Ticket).subscribe(res => {
        this.bStorageService.setFiscalPeriod(res);
      })
    }

    this.userService.getCurrentUserCommands(this.Ticket).subscribe((res: Array<Command>) => {
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
          this.router.navigate(['/tadbir/dashboard']);
        }
      }

    });

    this.userService.getDefaultUserCommands(this.Ticket).subscribe((res: Array<Command>) => {
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
    companyLoginModel.branchId = this.branchId ? parseInt(this.branchId) : 0;
    companyLoginModel.fiscalPeriodId = this.fiscalPeriodId ? parseInt(this.fiscalPeriodId) : 0;

    this.authenticationService.getCompanyTicket(companyLoginModel, this.Ticket).subscribe(res => {
      if (res.headers != null) {
        let newTicket = res.headers.get('X-Tadbir-AuthTicket');

        let contextInfo = res.body;

        var currentUser = this.bStorageService.getCurrentUser();
        if (currentUser != null) {
          currentUser.branchId = contextInfo.branchId;
          currentUser.companyId = contextInfo.companyId;
          currentUser.fpId = contextInfo.fiscalPeriodId;
          currentUser.permissions = JSON.parse(atob(this.Ticket)).user.permissions;
          currentUser.fiscalPeriodName = contextInfo.fiscalPeriodName;
          currentUser.branchName = contextInfo.branchName;
          currentUser.companyName = contextInfo.companyName;
          currentUser.ticket = newTicket;
          currentUser.roles = contextInfo.roles;

          this.bStorageService.setCurrentContext(currentUser);
          this.bStorageService.setLastUserBranchAndFpId(this.UserId, this.companyId, this.branchId, this.fiscalPeriodId);

          this.loadMenuAndRoute(currentUser);

          this.loadAllSetting();
        }

      }

    })
  }

  //#endregion


  createCompany() {
    this.dialogRef = this.dialogService.open({
      content: InitialWizardComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;

    this.dialogRef.dialog.location.nativeElement.classList.add('dialog-style-wizard');

    this.dialogRef.content.instance.save.subscribe((res) => {

      debugger;

      if (res) {
        this.companyId = res.company ? res.company.id : 0;
        this.branchId = res.branch ? res.branch.id : 0;
        this.fiscalPeriodId = res.fiscalPeriod ? res.fiscalPeriod.id : 0;

        this.getCompanyTicket();
      }
    });



    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });
  }


}
