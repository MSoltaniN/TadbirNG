import {
  Component,
  OnInit,
  Renderer2,
  TemplateRef,
  Output,
  EventEmitter,
  ElementRef,
} from "@angular/core";
import { ControlContainer } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import {
  DialogService,
  DialogRef,
  DialogCloseResult,
} from "@progress/kendo-angular-dialog";
import { MetaDataService, BrowserStorageService } from "@sppc/shared/services";
import { AccountItemBrief, FullAccount } from "@sppc/finance/models";
import { FullAccountInfo, FullAccountService } from "@sppc/finance/service";
import { AccountRelationApi } from "@sppc/finance/service/api";
import { AccountRelationsType } from "@sppc/finance/enum";
import { DetailComponent, String } from "@sppc/shared/class";

@Component({
  selector: "sppc-fullAccount",
  templateUrl: "./sppc-fullAccount.html",
  styleUrls: ["./sppc-fullAccount.css"],
})
export class SppcFullAccountComponent
  extends DetailComponent
  implements OnInit
{
  //#region Fields
  isNew: boolean;
  accountItem: any;
  accounts: any;

  selectedItem: any;

  focusedItem: number;

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

  /**آیدی حساب انتخاب شده در گرید*/
  accountSelectedId: number[] = [];
  /**آیدی تفضیلی شناور انتخاب شده در گرید*/
  detailAccountSelectedId: number[] = [];
  /**آیدی مرکز هزینه انتخاب شده در گرید*/
  costCenterSelectedId: number[] = [];
  /**آیدی پروژه انتخاب شده در گرید*/
  projectSelectedId: number[] = [];

  accountTitle: string;
  detailAccountTitle: string;
  costCenterTitle: string;
  projectTitle: string;
  //accountFullCode: string;

  fullAccount: FullAccountInfo;

  private dialogRef: DialogRef;
  private dialogModel: any;

  @Output() setFocus: EventEmitter<any> = new EventEmitter();
  //#endregion

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public controlContainer: ControlContainer,
    private fullAccountService: FullAccountService,
    private dialogService: DialogService,
    public bStorageService: BrowserStorageService,
    public elem: ElementRef
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      "",
      undefined,
      elem
    );
  }

  ngOnInit(): void {
    this.accountItem = AccountRelationsType;

    this.fullAccount = this.controlContainer.value;

    // if (this.fullAccount.account.id > 0) {
    //   this.isNew = false;
    //   this.accountSelectedId.push(this.fullAccount.account.id);
    //   this.accountTitle = this.fullAccount.account.name;

    // } else this.isNew = true;

    // if (!this.isNew) {
    //   if (this.fullAccount.detailAccount.id > 0) {
    //     this.detailAccountSelectedId.push(this.fullAccount.detailAccount.id);
    //     this.detailAccountTitle = this.fullAccount.detailAccount.name;

    //   }

    //   if (this.fullAccount.costCenter.id > 0) {
    //     this.costCenterSelectedId.push(this.fullAccount.costCenter.id);
    //     this.costCenterTitle = this.fullAccount.costCenter.name;

    //   }

    //   if (this.fullAccount.project.id > 0) {
    //     this.projectSelectedId.push(this.fullAccount.project.id);
    //     this.projectTitle = this.fullAccount.project.name;

    //   }
    // }
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

        this.GetDetailAccounts(
          String.Format(
            AccountRelationApi.UsableDetailAccountsRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
        this.GetCostCenters(
          String.Format(
            AccountRelationApi.UsableCostCentersRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
        this.GetProjects(
          String.Format(
            AccountRelationApi.UsableProjectsRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
        break;
      }
      case AccountRelationsType.DetailAccount: {
        this.costCentersRows = this.costCenterList = [];
        this.projectsRows = this.projectList = [];

        this.costCenterSelectedId = [];
        this.projectSelectedId = [];

        this.GetCostCenters(
          String.Format(
            AccountRelationApi.UsableCostCentersRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
        this.GetProjects(
          String.Format(
            AccountRelationApi.UsableProjectsRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
        break;
      }
      case AccountRelationsType.CostCenter: {
        this.detailAccountsRows = this.detailAccountList = [];
        this.projectsRows = this.projectList = [];

        this.detailAccountSelectedId = [];
        this.projectSelectedId = [];

        this.GetDetailAccounts(
          String.Format(
            AccountRelationApi.UsableDetailAccountsRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
        this.GetProjects(
          String.Format(
            AccountRelationApi.UsableProjectsRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
        break;
      }
      case AccountRelationsType.Project: {
        this.detailAccountsRows = this.detailAccountList = [];
        this.costCentersRows = this.costCenterList = [];

        this.detailAccountSelectedId = [];
        this.costCenterSelectedId = [];

        this.GetDetailAccounts(
          String.Format(
            AccountRelationApi.UsableDetailAccountsRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
        this.GetCostCenters(
          String.Format(
            AccountRelationApi.UsableCostCentersRelatedToAccount,
            this.accountSelectedId[0]
          )
        );
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

      this.GetAccounts(
        String.Format(
          AccountRelationApi.AccountsRelatedToDetailAccount,
          this.detailAccountSelectedId[0]
        )
      );
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

      this.GetAccounts(
        String.Format(
          AccountRelationApi.AccountsRelatedToCostCenter,
          this.costCenterSelectedId[0]
        )
      );
    }
  }

  /**
   * وقتی یک پروژه انتخاب میشود اگر بردار حساب از پروژه شروع شده باشد لیست حسابهای مرتبط را از سرویس میگیرد
   * @param projectId شناسه یکتای پروژه انتخاب شده
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

      this.GetAccounts(
        String.Format(
          AccountRelationApi.AccountsRelatedToProject,
          this.projectSelectedId[0]
        )
      );
    }
  }

  //#endregion

  //#region Call Api
  /**
   * گرفتن لیست حساب ها از سرویس
   * @param url آدرس api
   */
  GetAccounts(url: string) {
    this.fullAccountService.getFullAccountItemList(url).subscribe((res) => {
      this.accountsRows = res;
      this.accountList = res;

      if (
        this.accountTitle &&
        this.selectedItem == AccountRelationsType.Account
      ) {
        this.accFilterValue = this.accountTitle;
        this.handleFilter(AccountRelationsType.Account);
      }
    });
  }

  /**
   * گرفتن لیست تفصیلی های شناور از سرویس
   * @param url آدرس api
   */
  GetDetailAccounts(url: string) {
    this.fullAccountService.getFullAccountItemList(url).subscribe((res) => {
      this.detailAccountsRows = res;
      this.detailAccountList = res;

      if (
        this.detailAccountTitle &&
        this.selectedItem == AccountRelationsType.DetailAccount
      ) {
        this.dAccFilterValue = this.detailAccountTitle;
        this.handleFilter(AccountRelationsType.DetailAccount);
      }
    });
  }

  /**
   * گرفتن لیست مرکز هزینه ها از سرویس
   * @param url آدرس api
   */
  GetCostCenters(url: string) {
    this.fullAccountService.getFullAccountItemList(url).subscribe((res) => {
      this.costCentersRows = res;
      this.costCenterList = res;

      if (
        this.costCenterTitle &&
        this.selectedItem == AccountRelationsType.CostCenter
      ) {
        this.cCenterFilterValue = this.costCenterTitle;
        this.handleFilter(AccountRelationsType.CostCenter);
      }
    });
  }

  /**
   * گرفتن لیست پروژه ها از سرویس
   * @param url آدرس api
   */
  GetProjects(url: string) {
    this.fullAccountService.getFullAccountItemList(url).subscribe((res) => {
      this.projectsRows = res;
      this.projectList = res;

      if (
        this.projectTitle &&
        this.selectedItem == AccountRelationsType.Project
      ) {
        this.pFilterValue = this.projectTitle;
        this.handleFilter(AccountRelationsType.Project);
      }
    });
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
          this.accountsRows = this.accountList.filter(
            (s) =>
              s.name
                .toLowerCase()
                .indexOf(this.accFilterValue.toLowerCase()) !== -1 ||
              s.fullCode
                .toLowerCase()
                .indexOf(this.accFilterValue.toLowerCase()) !== -1
          );
          this.isEnableAccountFilter = true;
        } else {
          this.accountsRows = this.accountList;
          this.isEnableAccountFilter = false;
        }

        break;
      }
      case AccountRelationsType.DetailAccount: {
        if (this.dAccFilterValue) {
          this.detailAccountsRows = this.detailAccountList.filter(
            (s) =>
              s.name
                .toLowerCase()
                .indexOf(this.dAccFilterValue.toLowerCase()) !== -1 ||
              s.fullCode
                .toLowerCase()
                .indexOf(this.dAccFilterValue.toLowerCase()) !== -1
          );
          this.isEnableDetailAccountFilter = true;
        } else {
          this.detailAccountsRows = this.detailAccountList;
          this.isEnableDetailAccountFilter = false;
        }

        break;
      }
      case AccountRelationsType.CostCenter: {
        if (this.cCenterFilterValue) {
          this.costCentersRows = this.costCenterList.filter(
            (s) =>
              s.name
                .toLowerCase()
                .indexOf(this.cCenterFilterValue.toLowerCase()) !== -1 ||
              s.fullCode
                .toLowerCase()
                .indexOf(this.cCenterFilterValue.toLowerCase()) !== -1
          );
          this.isEnableCostCenterFilter = true;
        } else {
          this.costCentersRows = this.costCenterList;
          this.isEnableCostCenterFilter = false;
        }

        break;
      }
      case AccountRelationsType.Project: {
        if (this.pFilterValue) {
          this.projectsRows = this.projectList.filter(
            (s) =>
              s.name.toLowerCase().indexOf(this.pFilterValue.toLowerCase()) !==
                -1 ||
              s.fullCode
                .toLowerCase()
                .indexOf(this.pFilterValue.toLowerCase()) !== -1
          );
          this.isEnableProjectFilter = true;
        } else {
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

  onFocus(item: number) {
    this.focusedItem = item;
  }

  openDialog(template: TemplateRef<any>, item: number) {
    this.selectedItem = item;
    this.onReset();

    this.dialogRef = this.dialogService.open({
      title: this.getText("FullAccount.Title"),
      content: template,
    });

    this.dialogRef.dialog.location.nativeElement.classList.add(
      "fullAccountForm"
    );

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.closeDialog();
        this.setFocus.emit();
      }
    });
  }

  initDialog(item: number) {
    this.fullAccount = this.controlContainer.value;

    if (this.fullAccount.account.id > 0) this.isNew = false;
    else this.isNew = true;

    switch (item) {
      case AccountRelationsType.Account: {
        if (this.fullAccount.account.id > 0)
          this.accountSelectedId.push(this.fullAccount.account.id);

        this.GetAccounts(AccountRelationApi.EnvironmentAccountsLookup);

        if (!this.isNew) {
          this.GetDetailAccounts(
            String.Format(
              AccountRelationApi.UsableDetailAccountsRelatedToAccount,
              this.accountSelectedId[0]
            )
          );
          this.GetCostCenters(
            String.Format(
              AccountRelationApi.UsableCostCentersRelatedToAccount,
              this.accountSelectedId[0]
            )
          );
          this.GetProjects(
            String.Format(
              AccountRelationApi.UsableProjectsRelatedToAccount,
              this.accountSelectedId[0]
            )
          );
        }

        break;
      }
      case AccountRelationsType.DetailAccount: {
        if (this.fullAccount.detailAccount.id > 0)
          this.detailAccountSelectedId.push(this.fullAccount.detailAccount.id);

        this.GetDetailAccounts(
          AccountRelationApi.EnvironmentDetailAccountsLookup
        );

        if (!this.isNew) {
          this.GetAccounts(
            String.Format(
              AccountRelationApi.AccountsRelatedToDetailAccount,
              this.detailAccountSelectedId[0]
            )
          );
        }

        break;
      }
      case AccountRelationsType.CostCenter: {
        if (this.fullAccount.costCenter.id > 0)
          this.costCenterSelectedId.push(this.fullAccount.costCenter.id);

        this.GetCostCenters(AccountRelationApi.EnvironmentCostCentersLookup);

        if (!this.isNew) {
          this.GetAccounts(
            String.Format(
              AccountRelationApi.AccountsRelatedToCostCenter,
              this.costCenterSelectedId[0]
            )
          );
        }

        break;
      }
      case AccountRelationsType.Project: {
        if (this.fullAccount.project.id > 0)
          this.projectSelectedId.push(this.fullAccount.project.id);

        this.GetProjects(AccountRelationApi.EnvironmentProjectsLookup);

        if (!this.isNew) {
          this.GetAccounts(
            String.Format(
              AccountRelationApi.AccountsRelatedToProject,
              this.projectSelectedId[0]
            )
          );
        }

        break;
      }
      default:
    }
  }

  /**
   * ذخیره بردار حساب انتخاب شده
   */
  onSave(item) {
    this.accountTitle = item.account.name;
    this.detailAccountTitle = item.detailAccount.name;
    this.costCenterTitle = item.costCenter.name;
    this.projectTitle = item.project.name;

    this.fullAccount = item;

    this.controlContainer.control.patchValue({
      account: item.account,
      detailAccount: item.detailAccount,
      costCenter: item.costCenter,
      project: item.project,
    });

    this.dialogRef.close();
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

    this.accFilterValue =
      this.dAccFilterValue =
      this.cCenterFilterValue =
      this.pFilterValue =
        undefined;
    this.isEnableAccountFilter =
      this.isEnableDetailAccountFilter =
      this.isEnableCostCenterFilter =
      this.isEnableProjectFilter =
        false;

    this.initDialog(this.selectedItem);
  }

  /**
   * بستن فرم بردار حساب
   */
  closeDialog() {
    // this.selectedItem = undefined;

    // this.accountsRows = this.accountList = [];
    // this.detailAccountsRows = this.detailAccountList = [];
    // this.costCentersRows = this.costCenterList = [];
    // this.projectsRows = this.projectList = [];

    // this.accountSelectedId = [];
    // this.detailAccountSelectedId = [];
    // this.costCenterSelectedId = [];
    // this.projectSelectedId = [];

    // this.patchValue();

    this.dialogRef.close();
  }

  escPress(e: any) {
    this.dialogRef.close();
  }

  patchValue() {
    var defaultModel = this.controlContainer.value;

    if (defaultModel.account.id > 0) {
      this.accountTitle = defaultModel.account.name;
    }

    if (defaultModel.detailAccount.id > 0) {
      this.detailAccountTitle = defaultModel.detailAccount.name;
    }

    if (defaultModel.costCenter.id > 0) {
      this.costCenterTitle = defaultModel.costCenter.name;
    }

    if (defaultModel.project.id > 0) {
      this.projectTitle = defaultModel.project.name;
    }
  }
}
