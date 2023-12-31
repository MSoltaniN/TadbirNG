import { Component, OnInit, Renderer2 } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
// import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { TreeItemLookup, TreeItem } from '@progress/kendo-angular-treeview';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { MetaDataService, BrowserStorageService, ErrorHandlingService } from '@sppc/shared/services';
import { String, FilterExpression, FilterExpressionBuilder, Filter, DefaultComponent } from '@sppc/shared/class';
import { AccountItemBriefInfo, AccountRelationsService, AccountItemRelationsInfo } from '@sppc/finance/service';
import { AccountRelationApi, AccountApi, DetailAccountApi, CostCenterApi, ProjectApi } from '@sppc/finance/service/api';
import { SettingService } from '@sppc/config/service';
import { AccountRelationsType } from '@sppc/finance/enum';
import { AccountRelationPermissions, SecureEntity } from '@sppc/shared/security';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  key: number,
  value: string
}

@Component({
  selector: 'accountRelations',
  templateUrl: './accountRelations.component.html',
  styleUrls: ['./accountRelations.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class AccountRelationsComponent extends DefaultComponent implements OnInit {


  public isActive: boolean = false;
  public editMode: boolean = false;
  public searchValue: string;
  public noResultMessage: boolean = false;
  public relatedSearchValue: string;
  public noRelatedResultMessage: boolean = false;

  //permission flag
  viewAccess: boolean;

  public mainComponent: Array<Item>;
  public relatedComponent: Array<Item>;
  public selectedMainComponentValue: number | null;
  public selectedRelatedComponentValue: number | null;

  public isDisableRelatedComponnet: boolean = true;
  public isSelectedMainComponent: boolean = false;
  public deleteKey: any[] = [];

  public mainComponentCategories: any[];
  public mainComponentModel: AccountItemBriefInfo | undefined;
  public mainComponentCheckedKeys: any[] = [];
  public mainComponentSelectedItem: number = 0;
  public mainComponentDropdownSelected: number = 0;
  public mainComponentExpandedKeys: any[] = [];
  public isEnableMainComponentSearchBtn: boolean = false;
  public mainComponentApiUrl: string;

  public relatedComponentCategories: any;
  public relatedComponentCheckedKeys: any[] = [];
  public relatedComponentDropdownSelected: number = 0;
  public relatedComponentExpandedKeys: any[] = [];
  public fechedRelatedComponentChildren: Observable<any>;
  public isEnableRelatedComponentSearchBtn: boolean = false;
  public relatedComponentApiUrl: string;

  //public errorMessage = String.Empty;

  public ngOnInit(): void {
    this.viewAccess = this.isAccess(SecureEntity.AccountRelations, AccountRelationPermissions.ViewRelationships);
    if (!this.viewAccess) {
      this.showMessage(this.getText("App.AccessDenied"), MessageType.Warning);
    }
  }

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    private accountRelationsService: AccountRelationsService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public errorHandlingService: ErrorHandlingService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.AccountRelations, undefined);

    this.mainComponent = [
      { value: "AccountRelations.Account", key: AccountRelationsType.Account },
      { value: "AccountRelations.DetailAccount", key: AccountRelationsType.DetailAccount },
      { value: "AccountRelations.CostCenter", key: AccountRelationsType.CostCenter },
      { value: "AccountRelations.Project", key: AccountRelationsType.Project }
    ];
  }

  focusOnSearchBox(ClassName) {
    if (document.activeElement instanceof HTMLElement) {
      document.activeElement.blur();      
    }
    let elm = <HTMLInputElement>document.querySelector(`.${ClassName}`)
    setTimeout(() => {
      elm.focus();
      elm.select();
    }, 50);
  }

  /**
   * هنگامیکه کمبوباکس مولفه اصلی تغییر میکند اجرا میشود
   * @param item
   */
  public handleMainComponentDropDownChange(item: any) {
    this.mainComponentCategories = [];
    this.mainComponentCheckedKeys = [];
    this.mainComponentExpandedKeys = [];
    this.mainComponentDropdownSelected = 0;
    this.mainComponentSelectedItem = 0;
    this.relatedComponentCheckedKeys = [];
    this.relatedComponentCategories = undefined;
    this.relatedComponentDropdownSelected = 0;
    this.noRelatedResultMessage = false;
    this.deleteKey = [];
    this.focusOnSearchBox('MainComponentSearchbox');

    if (item > 0) {
      this.isDisableRelatedComponnet = false;
      this.isEnableMainComponentSearchBtn = true;
      this.mainComponentDropdownSelected = item;
      this.relatedComponent = [
        { value: "AccountRelations.Account", key: AccountRelationsType.Account }
      ];
      switch (item) {
        case AccountRelationsType.Account: {
          this.mainComponentApiUrl = AccountRelationApi.EnvironmentAccounts;
          this.relatedComponent = [
            { value: "AccountRelations.DetailAccount", key: AccountRelationsType.DetailAccount },
            { value: "AccountRelations.CostCenter", key: AccountRelationsType.CostCenter },
            { value: "AccountRelations.Project", key: AccountRelationsType.Project }
          ];
          break;
        }
        case AccountRelationsType.DetailAccount: {
          this.mainComponentApiUrl = AccountRelationApi.EnvironmentDetailAccounts;
          break
        }
        case AccountRelationsType.CostCenter: {
          this.mainComponentApiUrl = AccountRelationApi.EnvironmentCostCenters;
          break
        }
        case AccountRelationsType.Project: {
          this.mainComponentApiUrl = AccountRelationApi.EnvironmentProjects;
          break
        }
        default:
          {
            break;
          }
      }
    }
    else {
      this.selectedRelatedComponentValue = null;
      this.isDisableRelatedComponnet = true;
      this.isEnableMainComponentSearchBtn = false;
    }
  }

  /**
   * هنگامیکه کمبوباکس مولفه مرتبط تغییر میکند اجرا میشود
   * @param item
   */
  public handleRelatedComponentDropDownChange(item: any) {
    this.relatedComponentCheckedKeys = [];
    this.deleteKey = [];
    this.focusOnSearchBox('RelatedComponentSearchbox');

    if (item > 0) {
      this.relatedComponentDropdownSelected = item;
      this.isEnableRelatedComponentSearchBtn = true;
      this.loadRelatedComponent();
    }
    else {
      this.relatedComponentDropdownSelected = 0;
      this.relatedComponentCategories = undefined;
      this.isEnableRelatedComponentSearchBtn = false;
    }
  }

  /**
   * هنگامیکه یک آیتم از مولفه اصلی انتخاب میشود اجرا میشود
   * @param itemLookup
   */
  public handleMainComponentChecking(itemLookup: TreeItemLookup): void {
    var itemId = itemLookup.item.dataItem.id;
    this.mainComponentModel = itemLookup.item.dataItem;
    if (this.mainComponentSelectedItem == itemId) { // !this.mainComponentCheckedKeys.find(f => f == itemId) == itemId
      this.mainComponentCheckedKeys = [];
      this.relatedComponentCheckedKeys = [];
      this.relatedComponentCategories = undefined;
      this.mainComponentSelectedItem = 0;
    }
    else {
      this.mainComponentCheckedKeys = [];
      this.mainComponentCheckedKeys = [itemId];
      this.mainComponentSelectedItem = itemId;
      this.loadRelatedComponent();
    }
  }

  /**
   * هنگامیکه یک آیتم از مولفه مرتبط برای حذف ارتباط انتخاب میشود اجرا میشود
   * آیدی آیتم هایی که باید حذف شوند در یک آرایه قرار میگیرند
   * @param itemLookup
   */
  public handleRelatedComponentChecking(itemLookup: TreeItemLookup): void {
    var item = itemLookup.item.dataItem;

    if (this.deleteKey.find(f => f == item.id)) {
      var index = this.deleteKey.findIndex(f => f == item.id);
      if (index > -1) {
        this.deleteKey.splice(index, 1);
      }
    }
    else {
      this.deleteKey.push(item.id);
    }
  }

  /**
   * آیدی هر آیتم را برای انتخاب مشخص میکنم
   * @param item
   */
  public checkById(item: TreeItem) {
    return item.dataItem.id;
  }

  //مشخص میکند که آیتم ها، فرزند دارند یا خیر
  public hasChildren = (item: any) => {
    if (item.childCount > 0) {
      return true;
    }
    return false;
  };

  // اگر یک آیتم از مولفه اصلی فرزند داشته باشد هنگامیکه زیرمجموعه آن آیتم باز میشود فرزندانش از دیتابیس واکشی میشوند
  public fetchMainComponentChildren = (item: any) => {
    var apiUrl = String.Empty;
    switch (this.mainComponentDropdownSelected) {
      case AccountRelationsType.Account: {
        apiUrl = String.Format(AccountApi.AccountChildren, item.id);
        break;
      }
      case AccountRelationsType.DetailAccount: {
        apiUrl = String.Format(DetailAccountApi.DetailAccountChildren, item.id);
        break;
      }
      case AccountRelationsType.CostCenter: {
        apiUrl = String.Format(CostCenterApi.CostCenterChildren, item.id);
        break;
      }
      case AccountRelationsType.Project: {
        apiUrl = String.Format(ProjectApi.ProjectChildren, item.id);
        break;
      }
      default:
        {
          break;
        }
    }
    return this.accountRelationsService.getChildrens(apiUrl);
  }

  ////بعد از واکشی فرزندان یک آیتم، آیدی هر کدام از فرزندان که در حالت انتخاب هستند در آرایه مربوطه قرار میگیرد
  //public childrenLoadedHandler = (dataItem: any) => {
  //  this.fechedRelatedComponentChildren.subscribe(res => {
  //    for (let item of res) {
  //      if (item.isSelected && !this.relatedComponentCheckedKeys.find(f => f == item.id)) {
  //        this.relatedComponentCheckedKeys.push(item.id);
  //      }
  //    }
  //  })
  //}

  /**
   * بر طبق مولفه اصلی و مولفه مرتبط انتخاب شده، ارتباطات موجود را واکشی میکند
   */
  loadRelatedComponent() {
    this.isDisableRelatedComponnet = false;
    this.relatedComponentCheckedKeys = [];
    this.relatedComponentExpandedKeys = [];
    if (this.relatedComponentDropdownSelected > 0 && this.mainComponentSelectedItem > 0) {
      switch (this.relatedComponentDropdownSelected) {
        case AccountRelationsType.Account: {
          if (this.mainComponentDropdownSelected > 0) {
            switch (this.mainComponentDropdownSelected) {
              case AccountRelationsType.DetailAccount: {
                this.relatedComponentApiUrl = String.Format(AccountRelationApi.AccountsRelatedToDetailAccount, this.mainComponentSelectedItem);
                break;
              }
              case AccountRelationsType.CostCenter: {
                this.relatedComponentApiUrl = String.Format(AccountRelationApi.AccountsRelatedToCostCenter, this.mainComponentSelectedItem);
                break;
              }
              case AccountRelationsType.Project: {
                this.relatedComponentApiUrl = String.Format(AccountRelationApi.AccountsRelatedToProject, this.mainComponentSelectedItem);
                break;
              }
              default: {
                break;
              }
            }
          }
          break;
        }
        case AccountRelationsType.DetailAccount: {
          this.relatedComponentApiUrl = String.Format(AccountRelationApi.DetailAccountsRelatedToAccount, this.mainComponentSelectedItem);
          break;
        }
        case AccountRelationsType.CostCenter: {
          this.relatedComponentApiUrl = String.Format(AccountRelationApi.CostCentersRelatedToAccount, this.mainComponentSelectedItem);
          break;
        }
        case AccountRelationsType.Project: {
          this.relatedComponentApiUrl = String.Format(AccountRelationApi.ProjectsRelatedToAccount, this.mainComponentSelectedItem);
          break;
        }
        default: {
          break;
        }
      }
      //this.sppcLoading.show();
      this.accountRelationsService.getRelatedComponentModel(this.relatedComponentApiUrl,null,!this.editMode).subscribe(res => {
        this.relatedComponentCategories = res;
        if (res.length) {
          this.relatedComponentCategories.map(item => item.parentId = -1);
          this.relatedComponentCategories.push({
            id:-1,
            childCount:0,
            code:null,
            fullCode:null,
            groupId:null,
            isSelected:true,
            level:0,
            name: "AccountRelations.SelectAll",
            parentId:null,
          })
        }
        this.deleteKey = [];
        if (this.relatedComponentCategories.length == 0)
          this.noRelatedResultMessage = true;
        else
          this.noRelatedResultMessage = false;
        ////this.sppcLoading.hide();
      })
    }
  }

  /**
   * هنگامیه روی دکمه ایجاد ارتباط کلیک شود اجرا میشود و فرم ایجاد ارتباط را باز میکند
   */
  onCreateRelation() {
    if (this.relatedComponentDropdownSelected > 0 && this.mainComponentSelectedItem > 0) {
      this.isActive = true;
    } else {
      this.showMessage(this.getText('Messages.ClickOnCearteRelation'),
        MessageType.Warning,
        this.getText('Messages.SelectItemFromMainComponent'))
    }
  }

  /**
   * هنگامیکه روی دکمه حذف ارتباط کلیک شود اجرا میشود 
   */
  DeleteRelation() {
    var model = new AccountItemRelationsInfo();
    model.id = this.mainComponentSelectedItem;
    let indexOfSelectAll = this.relatedComponentCheckedKeys.findIndex(id => id == -1);
    if (indexOfSelectAll>-1) {
      this.relatedComponentCheckedKeys.splice(indexOfSelectAll,1);
    }
    model.relatedItemIds = this.relatedComponentCheckedKeys;


    this.errorMessages = [];

    var apiUrl = this.relationUrl(model);

    this.accountRelationsService.edit<AccountItemRelationsInfo>(apiUrl, model).subscribe(response => {
        
        this.showMessage(this.updateMsg, MessageType.Succes);
        
        this.mainComponentModel = undefined;
      this.editMode=true;
      this.loadRelatedComponent();
      this.editMode=false;

    }, (error => {
        if (error)
          this.errorMessages = this.errorHandlingService.handleError(error);
    }));

  }

  /**
   * هنگامیکه فرم ایجاد ارتباط بسته میشود اجرا میشود
   */
  cancelHandler() {
    this.isActive = false;
    this.mainComponentModel = undefined;
  }

  /**
   * هنگامیکه دکمه تایید در فرم ایجاد ارتباط زده میشود اجرا میشود
   * @param relationModel
   */
  saveHandler(relationModel: AccountItemRelationsInfo) {
    if (relationModel) {
      var keyArray = this.relatedComponentCheckedKeys.concat(relationModel.relatedItemIds, this.deleteKey);
      relationModel.relatedItemIds = keyArray;
      this.saveRelations(relationModel);
      this.isActive = false;
    }
  }

  /**
   * وقتی که ارتباط جدیدی اضافه میشود برای ذخیره در دیتابیس اجرا میشود 
   * @param relationsModel
   */
  saveRelations(relationsModel: AccountItemRelationsInfo) {
    this.errorMessages = [];

    var apiUrl = this.relationUrl(relationsModel);
    this.accountRelationsService.insert<AccountItemRelationsInfo>(apiUrl, relationsModel).subscribe(response => {
      ////this.sppcLoading.hide();
      this.showMessage(this.updateMsg, MessageType.Succes);
      
      this.mainComponentModel = undefined;
      this.editMode = true;
      this.loadRelatedComponent();
      this.editMode = false;

    }, (error => {
        if (error)
          this.errorMessages = this.errorHandlingService.handleError(error);
      ////this.sppcLoading.hide();
    }));


  }

  /**
   * آدرس سرویس برای ایجاد یا حذف ارتباط را برمیگرداند
   * @param relationsModel
   */
  relationUrl(relationsModel: AccountItemRelationsInfo): string {

    var apiUrl = String.Empty;
    if (this.relatedComponentDropdownSelected > 0) {
      switch (this.relatedComponentDropdownSelected) {
        case AccountRelationsType.Account: {
          if (this.mainComponentDropdownSelected > 0) {
            switch (this.mainComponentDropdownSelected) {
              case AccountRelationsType.DetailAccount: {
                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToDetailAccount, relationsModel.id);
                break;
              }
              case AccountRelationsType.CostCenter: {
                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToCostCenter, relationsModel.id);
                break;
              }
              case AccountRelationsType.Project: {
                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToProject, relationsModel.id);
                break;
              }
              default: {
                break;
              }
            }
          }
          break;
        }
        case AccountRelationsType.DetailAccount: {
          apiUrl = String.Format(AccountRelationApi.DetailAccountsRelatedToAccount, relationsModel.id);
          break;
        }
        case AccountRelationsType.CostCenter: {
          apiUrl = String.Format(AccountRelationApi.CostCentersRelatedToAccount, relationsModel.id);
          break;
        }
        case AccountRelationsType.Project: {
          apiUrl = String.Format(AccountRelationApi.ProjectsRelatedToAccount, relationsModel.id);
          break;
        }
        default: {
          break;
        }
      }
    }

    return apiUrl;
  }


  /**
   * وقتی دکمه انصراف در فرم اصلی کلیک میود اجرا میشود
   */
  onCancel() {
    this.mainComponentCategories = [];
    this.relatedComponentCategories = undefined;
    this.mainComponentCheckedKeys = [];
    this.relatedComponentCheckedKeys = [];
    this.mainComponentSelectedItem = 0;
    this.mainComponentDropdownSelected = 0;
    this.relatedComponentDropdownSelected = 0;
    this.errorMessages = [];
    this.isDisableRelatedComponnet = true;
    this.isEnableMainComponentSearchBtn = false;
    this.isEnableRelatedComponentSearchBtn = false;

    this.relatedComponentApiUrl = String.Empty;
    this.mainComponentApiUrl = String.Empty;

    this.searchValue = String.Empty;
    this.relatedSearchValue = String.Empty;
    this.noResultMessage = false;
    this.noRelatedResultMessage = false;
    this.deleteKey = [];
  }

  /**
   * برای جستجو در مولفه اصلی و لود مولفه اصلی میباشد
   */
  onMainComponentSearch() {
    let filterExp: FilterExpression | undefined;

    if (this.searchValue) {
      var filterExpBuilder = new FilterExpressionBuilder();
      filterExp = filterExpBuilder.New(new Filter("Name", this.searchValue, ".Contains({0})", "System.String"))
        .Or(new Filter("FullCode", this.searchValue, ".Contains({0})", "System.String"))
        .Build();
    }

    //this.sppcLoading.show();
    this.accountRelationsService.getMainComponentModel(this.mainComponentApiUrl, filterExp).subscribe(res => {
      this.mainComponentCategories = res;
      if (this.mainComponentCategories.length == 0)
        this.noResultMessage = true;
      else
        this.noResultMessage = false;

      ////this.sppcLoading.hide();
    });
  }

  /**
   *حذف فیلتر مولفه اصلی  
   */
  removeMainComponentFilter() {
    this.searchValue = '';
    this.onMainComponentSearch();
  }

  /**
   * برای جستجو در مولفه مرتبط میباشد
   */
  onRelatedComponentSearch() {
    if (this.relatedComponentDropdownSelected > 0 && this.mainComponentSelectedItem > 0) {

      let filterExp: FilterExpression | undefined;

      if (this.relatedSearchValue) {
        var filterExpBuilder = new FilterExpressionBuilder();
        filterExp = filterExpBuilder.New(new Filter("Name", this.relatedSearchValue, ".Contains({0})", "System.String"))
          .Or(new Filter("FullCode", this.relatedSearchValue, ".Contains({0})", "System.String"))
          .Build();
      }

      //this.sppcLoading.show();
      this.accountRelationsService.getRelatedComponentModel(this.relatedComponentApiUrl, filterExp).subscribe(res => {
        this.relatedComponentCategories = res;
        for (let item of res) {
          if (item.isSelected) {
            this.relatedComponentCheckedKeys.push(item.id)
          }
        }
        if (this.relatedComponentCategories.length == 0)
          this.noRelatedResultMessage = true;
        else
          this.noRelatedResultMessage = false;

        ////this.sppcLoading.hide();
      })
    }
  }

  /**
   * حذف فیلتر مولفه مرتبط
   */
  removeRelatedComponentFilter() {
    this.relatedSearchValue = '';
    this.onRelatedComponentSearch();
  }
}


