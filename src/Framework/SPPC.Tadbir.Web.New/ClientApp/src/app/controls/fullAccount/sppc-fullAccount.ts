import { Component, OnInit, Renderer2 } from '@angular/core'
import { FormBuilder, ControlContainer, Validators } from '@angular/forms'
import { FullAccountService, FullAccountInfo } from '../../service/index';
import { DetailComponent } from '../../class/detail.component';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { AccountRelationsType } from '../../enum/accountRelationType';
import { AccountRelationApi } from '../../service/api/index';
import { AccountItemBrief, FullAccount } from '../../model/index';
import { String } from '../../class/source'
import { Entities } from '../../../environments/environment';



@Component({
  selector: 'sppc-fullAccount',
  templateUrl: './sppc-fullAccount.html',
  styleUrls: ['./sppc-fullAccount.css']
})


export class SppcFullAccountComponent extends DetailComponent implements OnInit {

  //#region Fields
  isNew: boolean;
  accountItem: any;
  isOpenDialog: boolean = false;

  selectedItem: any;

  isEnableAccountFilter: boolean = false;
  isEnableDetailAccountFilter: boolean = false;
  isEnableCostCenterFilter: boolean = false;
  isEnableProjectFilter: boolean = false;

  accFilterValue: string;
  dAccFilterValue: string;
  cCenterFilterValue: string;
  pFilterValue: string;

  accountsRows: Array<AccountItemBrief>;
  detailAccountsRows: Array<AccountItemBrief>;
  costCentersRows: Array<AccountItemBrief>;
  projectsRows: Array<AccountItemBrief>;

  accountList: any;
  detailAccountList: any;
  costCenterList: any;
  projectList: any;

  accountSelectedId: number[] = [];
  detailAccountSelectedId: number[] = [];
  costCenterSelectedId: number[] = [];
  projectSelectedId: number[] = [];

  accountTitle: string;
  detailAccountTitle: string;
  costCenterTitle: string;
  projectTitle: string;
  accountFullCode: string;

  fullAccount: FullAccountInfo;
  //#endregion

  constructor(public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService,
    public controlContainer: ControlContainer, private fullAccountService: FullAccountService) {
    super(toastrService, translate, renderer, metadata, '', '');

  }


  ngOnInit(): void {

    this.accountItem = AccountRelationsType;

    this.fullAccount = this.controlContainer.value;

    if (this.fullAccount.account.id > 0) {
      this.isNew = false;
      this.accountSelectedId.push(this.fullAccount.account.id);
      this.accountTitle = this.fullAccount.account.name;

      //this.accountKeysChange([this.fullAccount.account.id]);
      this.accountFullCode = this.fullAccount.account.fullCode + " - ";
    }
    else
      this.isNew = true;

    if (!this.isNew) {
      if (this.fullAccount.detailAccount.id > 0) {
        this.detailAccountSelectedId.push(this.fullAccount.detailAccount.id);
        this.detailAccountTitle = this.fullAccount.detailAccount.name;
        this.accountFullCode += this.fullAccount.detailAccount.fullCode + " - ";
      }
      else
        this.accountFullCode += " - ";

      if (this.fullAccount.costCenter.id > 0) {
        this.costCenterSelectedId.push(this.fullAccount.costCenter.id);
        this.costCenterTitle = this.fullAccount.costCenter.name;
        this.accountFullCode += this.fullAccount.costCenter.fullCode + " - ";
      }
      else
        this.accountFullCode += " - ";

      if (this.fullAccount.project.id > 0) {
        this.projectSelectedId.push(this.fullAccount.project.id);
        this.projectTitle = this.fullAccount.project.name;
        this.accountFullCode += this.fullAccount.project.fullCode;
      }
    }

  }

  //#region Select item
  /**
   * وقتی یک حساب انتخاب میشود با توجه به اینکه از کدام مولفه بردار حساب شروع شده است لیست سایر مولفه ها را از سرویس میگیرد
   * @param accountId شناسه یکتای حساب انتخاب شده
   */
  accountKeysChange(accountId: any) {

    this.accountSelectedId = [];
    this.accountSelectedId = accountId;

    switch (this.selectedItem) {
      case AccountRelationsType.Account: {
        this.detailAccountsRows = this.detailAccountList = [];
        this.costCentersRows = this.costCenterList = [];
        this.projectsRows = this.projectList = [];

        this.detailAccountSelectedId = [];
        this.costCenterSelectedId = [];
        this.projectSelectedId = [];

        this.GetDetailAccounts(String.Format(AccountRelationApi.UsableDetailAccountsRelatedToAccount, this.accountSelectedId[0]));
        this.GetCostCenters(String.Format(AccountRelationApi.UsableCostCentersRelatedToAccount, this.accountSelectedId[0]));
        this.GetProjects(String.Format(AccountRelationApi.UsableProjectsRelatedToAccount, this.accountSelectedId[0]));
        break;
      }
      case AccountRelationsType.DetailAccount: {
        this.costCentersRows = this.costCenterList = [];
        this.projectsRows = this.projectList = [];

        this.costCenterSelectedId = [];
        this.projectSelectedId = [];

        this.GetCostCenters(String.Format(AccountRelationApi.CostCentersRelatedToAccount, this.accountSelectedId[0]));
        this.GetProjects(String.Format(AccountRelationApi.ProjectsRelatedToAccount, this.accountSelectedId[0]));
        break;
      }
      case AccountRelationsType.CostCenter: {
        this.detailAccountsRows = this.detailAccountList = [];
        this.projectsRows = this.projectList = [];

        this.detailAccountSelectedId = [];
        this.projectSelectedId = [];

        this.GetDetailAccounts(String.Format(AccountRelationApi.DetailAccountsRelatedToAccount, this.accountSelectedId[0]));
        this.GetProjects(String.Format(AccountRelationApi.ProjectsRelatedToAccount, this.accountSelectedId[0]));
        break;
      }
      case AccountRelationsType.Project: {
        this.detailAccountsRows = this.detailAccountList = [];
        this.costCentersRows = this.costCenterList = [];

        this.detailAccountSelectedId = [];
        this.costCenterSelectedId = [];

        this.GetDetailAccounts(String.Format(AccountRelationApi.DetailAccountsRelatedToAccount, this.accountSelectedId[0]));
        this.GetCostCenters(String.Format(AccountRelationApi.CostCentersRelatedToAccount, this.accountSelectedId[0]));
        break;
      }
      default:
    }
  }

  /**
   * وقتی یک تفصیلی انتخاب میشود اگر بردار حساب از تفصیلی شروع شده باشد لیست حسابهای مرتبط را از سرویس میگیرد
   * @param dAccountId شناسه یکتای تفصیلی شناور انتخاب شده
   */
  detailAccountKeysChange(dAccountId: any) {
    this.detailAccountSelectedId = [];
    this.detailAccountSelectedId = dAccountId;

    if (this.selectedItem == AccountRelationsType.DetailAccount) {
      this.costCentersRows = this.costCenterList = [];
      this.projectsRows = this.projectList = [];

      this.accountSelectedId = [];
      this.costCenterSelectedId = [];
      this.projectSelectedId = [];

      this.GetAccounts(String.Format(AccountRelationApi.AccountsRelatedToDetailAccount, this.detailAccountSelectedId[0]));
    }
  }

  /**
   * وقتی یک مرکز هزینه انتخاب میشود اگر بردار حساب از مرکز هزینه شروع شده باشد لیست حسابهای مرتبط را از سرویس میگیرد
   * @param cCenterId شناسه یکتای مرکز هزینه انتخاب شده
   */
  costCenterKeysChange(cCenterId: any) {
    this.costCenterSelectedId = [];
    this.costCenterSelectedId = cCenterId;

    if (this.selectedItem == AccountRelationsType.CostCenter) {
      this.detailAccountsRows = this.detailAccountList = [];
      this.projectsRows = this.projectList = [];

      this.accountSelectedId = [];
      this.detailAccountSelectedId = [];
      this.projectSelectedId = [];

      this.GetAccounts(String.Format(AccountRelationApi.AccountsRelatedToCostCenter, this.costCenterSelectedId[0]));
    }
  }

  /**
   * وقتی یک پروژه انتخاب میشود اگر بردار حساب از پروژه شروع شده باشد لیست حسابهای مرتبط را از سرویس میگیرد
   * @param projectId شنتسه یکتای پروژه انتخاب شده
   */
  projectKeysChange(projectId: any) {
    this.projectSelectedId = [];
    this.projectSelectedId = projectId;

    if (this.selectedItem == AccountRelationsType.Project) {
      this.detailAccountsRows = this.detailAccountList = [];
      this.costCentersRows = this.costCenterList = [];

      this.accountSelectedId = [];
      this.detailAccountSelectedId = [];
      this.costCenterSelectedId = [];

      this.GetAccounts(String.Format(AccountRelationApi.AccountsRelatedToProject, this.projectSelectedId[0]));
    }
  }

  //#endregion

  //#region Call Api
  /**
   * گرفتن لیست حساب ها از سرویس
   * @param url آدرس api
   */
  GetAccounts(url: string) {
    this.fullAccountService.getFullAccountItemList(url).subscribe(res => {
      this.accountsRows = res;
      this.accountList = res;

      if (this.accountTitle && this.selectedItem == AccountRelationsType.Account) {
        this.accFilterValue = this.accountTitle;
        this.handleFilter(AccountRelationsType.Account);
      }
    })
  }

  /**
   * گرفتن لیست تفصیلی های شناور از سرویس
   * @param url آدرس api
   */
  GetDetailAccounts(url: string) {
    this.fullAccountService.getFullAccountItemList(url).subscribe(res => {
      this.detailAccountsRows = res;
      this.detailAccountList = res;

      if (this.detailAccountTitle && this.selectedItem == AccountRelationsType.DetailAccount) {
        this.dAccFilterValue = this.detailAccountTitle;
        this.handleFilter(AccountRelationsType.DetailAccount);
      }
    })
  }

  /**
   * گرفتن لیست مرکز هزینه ها از سرویس
   * @param url آدرس api
   */
  GetCostCenters(url: string) {
    this.fullAccountService.getFullAccountItemList(url).subscribe(res => {
      this.costCentersRows = res;
      this.costCenterList = res;

      if (this.costCenterTitle && this.selectedItem == AccountRelationsType.CostCenter) {
        this.cCenterFilterValue = this.costCenterTitle;
        this.handleFilter(AccountRelationsType.CostCenter);
      }
    })
  }

  /**
   * گرفتن لیست پروژه ها از سرویس
   * @param url آدرس api
   */
  GetProjects(url: string) {
    this.fullAccountService.getFullAccountItemList(url).subscribe(res => {
      this.projectsRows = res;
      this.projectList = res;

      if (this.projectTitle && this.selectedItem == AccountRelationsType.Project) {
        this.pFilterValue = this.projectTitle;
        this.handleFilter(AccountRelationsType.Project);
      }
    })
  }
  //#endregion

  //#region Filter
  /**
   * عملیات مربوط به فیلتر کردن لیست های بردار حساب را انجام میدهد
   * @param item مولفه بردار حساب
   */
  handleFilter(item: number) {
    switch (item) {
      case AccountRelationsType.Account: {

        if (this.accFilterValue) {
          this.accountsRows = this.accountList.filter((s) => s.name.toLowerCase().indexOf(this.accFilterValue.toLowerCase()) !== -1 ||
            s.fullCode.toLowerCase().indexOf(this.accFilterValue.toLowerCase()) !== -1);
          this.isEnableAccountFilter = true;
        }
        else {
          this.accountsRows = this.accountList;
          this.isEnableAccountFilter = false;
        }

        break;
      }
      case AccountRelationsType.DetailAccount: {

        if (this.dAccFilterValue) {
          this.detailAccountsRows = this.detailAccountList.filter((s) => s.name.toLowerCase().indexOf(this.dAccFilterValue.toLowerCase()) !== -1 ||
            s.fullCode.toLowerCase().indexOf(this.dAccFilterValue.toLowerCase()) !== -1);
          this.isEnableDetailAccountFilter = true;
        }
        else {
          this.detailAccountsRows = this.detailAccountList;
          this.isEnableDetailAccountFilter = false;
        }

        break;
      }
      case AccountRelationsType.CostCenter: {

        if (this.cCenterFilterValue) {
          this.costCentersRows = this.costCenterList.filter((s) => s.name.toLowerCase().indexOf(this.cCenterFilterValue.toLowerCase()) !== -1 ||
            s.fullCode.toLowerCase().indexOf(this.cCenterFilterValue.toLowerCase()) !== -1);
          this.isEnableCostCenterFilter = true;
        }
        else {
          this.costCentersRows = this.costCenterList
          this.isEnableCostCenterFilter = false;
        }

        break;
      }
      case AccountRelationsType.Project: {

        if (this.pFilterValue) {
          this.projectsRows = this.projectList.filter((s) => s.name.toLowerCase().indexOf(this.pFilterValue.toLowerCase()) !== -1 ||
            s.fullCode.toLowerCase().indexOf(this.pFilterValue.toLowerCase()) !== -1);
          this.isEnableProjectFilter = true;
        }
        else {
          this.projectsRows = this.projectList;
          this.isEnableProjectFilter = false;
        }

        break;
      }
      default:
    }
  }

  /**
   * عملیات مربوط به حذف فیلتر را انجام میدهد
   * @param item مولفه بردار حساب
   */
  clearFilter(item: number) {
    switch (item) {
      case AccountRelationsType.Account: {

        this.accountsRows = this.accountList;
        this.isEnableAccountFilter = false;
        this.accFilterValue = undefined;

        break;
      }
      case AccountRelationsType.DetailAccount: {

        this.detailAccountsRows = this.detailAccountList;
        this.isEnableDetailAccountFilter = false;
        this.dAccFilterValue = undefined;

        break;
      }
      case AccountRelationsType.CostCenter: {

        this.costCentersRows = this.costCenterList;
        this.isEnableCostCenterFilter = false;
        this.cCenterFilterValue = undefined;

        break;
      }
      case AccountRelationsType.Project: {

        this.projectsRows = this.projectList;
        this.isEnableProjectFilter = false;
        this.pFilterValue = undefined;

        break;
      }
      default:
    }
  }
  //#endregion

  openDialog(item: number) {

    this.initItems();

    this.isOpenDialog = true;
    this.selectedItem = item;

    switch (item) {
      case AccountRelationsType.Account: {

        this.GetAccounts(AccountRelationApi.EnvironmentAccountsLookup);

        if (!this.isNew) {
          this.GetDetailAccounts(String.Format(AccountRelationApi.UsableDetailAccountsRelatedToAccount, this.accountSelectedId[0]));
          this.GetCostCenters(String.Format(AccountRelationApi.UsableCostCentersRelatedToAccount, this.accountSelectedId[0]));
          this.GetProjects(String.Format(AccountRelationApi.UsableProjectsRelatedToAccount, this.accountSelectedId[0]));
        }


        break;
      }
      case AccountRelationsType.DetailAccount: {

        this.GetDetailAccounts(AccountRelationApi.EnvironmentDetailAccountsLookup);

        if (!this.isNew) {
          this.GetAccounts(String.Format(AccountRelationApi.AccountsRelatedToDetailAccount, this.detailAccountSelectedId[0]));
          this.GetCostCenters(String.Format(AccountRelationApi.UsableCostCentersRelatedToAccount, this.accountSelectedId[0]));
          this.GetProjects(String.Format(AccountRelationApi.UsableProjectsRelatedToAccount, this.accountSelectedId[0]));
        }

        break;
      }
      case AccountRelationsType.CostCenter: {

        this.GetCostCenters(AccountRelationApi.EnvironmentCostCentersLookup);

        if (!this.isNew) {
          this.GetAccounts(String.Format(AccountRelationApi.AccountsRelatedToCostCenter, this.costCenterSelectedId[0]));
          this.GetDetailAccounts(String.Format(AccountRelationApi.UsableDetailAccountsRelatedToAccount, this.accountSelectedId[0]));
          this.GetProjects(String.Format(AccountRelationApi.UsableProjectsRelatedToAccount, this.accountSelectedId[0]));
        }

        break;
      }
      case AccountRelationsType.Project: {

        this.GetProjects(AccountRelationApi.EnvironmentProjectsLookup);

        if (!this.isNew) {
          this.GetAccounts(String.Format(AccountRelationApi.AccountsRelatedToProject, this.projectSelectedId[0]));
          this.GetDetailAccounts(String.Format(AccountRelationApi.UsableDetailAccountsRelatedToAccount, this.accountSelectedId[0]));
          this.GetCostCenters(String.Format(AccountRelationApi.UsableCostCentersRelatedToAccount, this.accountSelectedId[0]));
        }

        break;
      }
      default:
    }
  }

  /**
   * ذخیره بردار حساب انتخاب شده
   */
  onSave() {
    this.accountTitle = undefined;
    this.detailAccountTitle = undefined;
    this.costCenterTitle = undefined;
    this.projectTitle = undefined;

    this.controlContainer.control.patchValue({
      account: { id: this.accountSelectedId[0] }, detailAccount: { id: this.detailAccountSelectedId[0] },
      costCenter: { id: this.costCenterSelectedId[0] }, project: { id: this.projectSelectedId[0] }
    })

    if (this.accountSelectedId.length > 0) {
      var account = this.accountsRows.find(f => f.id == this.accountSelectedId[0])
      this.accountTitle = account.name;
      this.accountFullCode = account.fullCode + " - ";
    }

    if (this.detailAccountSelectedId.length > 0) {
      var detailAccount = this.detailAccountsRows.find(f => f.id == this.detailAccountSelectedId[0]);
      this.detailAccountTitle = detailAccount.name;
      this.accountFullCode += detailAccount.fullCode + " - ";
    }
    else
      this.accountFullCode += " - ";

    if (this.costCenterSelectedId.length > 0) {
      var costCenter = this.costCentersRows.find(f => f.id == this.costCenterSelectedId[0]);
      this.costCenterTitle = costCenter.name;
      this.accountFullCode += costCenter.fullCode + " - ";
    }
    else
      this.accountFullCode += " - ";

    if (this.projectSelectedId.length > 0) {
      var project = this.projectsRows.find(f => f.id == this.projectSelectedId[0]);
      this.projectTitle = project.name;
      this.accountFullCode += project.fullCode;
    }


    //this.closeDialog();
    this.isOpenDialog = false;
  }

  onReset() {
    this.accountsRows = this.accountList = [];
    this.detailAccountsRows = this.detailAccountList = [];
    this.costCentersRows = this.costCenterList = [];
    this.projectsRows = this.projectList = [];

    this.accountSelectedId = [];
    this.detailAccountSelectedId = [];
    this.costCenterSelectedId = [];
    this.projectSelectedId = [];

    this.accountTitle = this.detailAccountTitle = this.costCenterTitle = this.projectTitle = this.accountFullCode = undefined;  
    this.accFilterValue = this.dAccFilterValue = this.cCenterFilterValue = this.pFilterValue = undefined;
    this.isEnableAccountFilter = this.isEnableDetailAccountFilter = this.isEnableCostCenterFilter = this.isEnableProjectFilter = false;



    this.openDialog(this.selectedItem);
  }

  /**
   * بستن فرم بردار حساب
   */
  closeDialog() {
    this.isOpenDialog = false;

    this.selectedItem = undefined;

    this.accountsRows = this.accountList = [];
    this.detailAccountsRows = this.detailAccountList = [];
    this.costCentersRows = this.costCenterList = [];
    this.projectsRows = this.projectList = [];

    //if (this.isNew) {
    this.accountSelectedId = [];
    this.detailAccountSelectedId = [];
    this.costCenterSelectedId = [];
    this.projectSelectedId = [];

    //  this.accountTitle = this.detailAccountTitle = this.costCenterTitle = this.projectTitle = this.accountFullCode = undefined;
    //}

    //this.controlContainer.reset();
  }



  initItems() {

    this.accFilterValue = undefined;
    this.dAccFilterValue = undefined;
    this.cCenterFilterValue = undefined;  
    this.pFilterValue = undefined;


    this.fullAccount = this.controlContainer.value;

    if (this.fullAccount.account.id > 0) {
      this.isNew = false;
      this.accountSelectedId.push(this.fullAccount.account.id);
    }
    else {
      this.isNew = true;
    }

    if (this.fullAccount.detailAccount.id > 0) 
      this.detailAccountSelectedId.push(this.fullAccount.detailAccount.id);

    if (this.fullAccount.costCenter.id > 0) 
      this.costCenterSelectedId.push(this.fullAccount.costCenter.id);

    if (this.fullAccount.project.id > 0) 
      this.projectSelectedId.push(this.fullAccount.project.id);

  }
}

