import { Component, Input, Output, EventEmitter, Renderer2, ElementRef, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { TreeItem, TreeItemLookup } from '@progress/kendo-angular-treeview';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { AccountItemBriefInfo, AccountItemRelationsInfo, AccountRelationsService } from '@sppc/finance/service';
import { AccountRelationApi } from '@sppc/finance/service/api';
import { AccountRelationsType } from '@sppc/finance/enum';
import { String, DetailComponent, FilterExpression, FilterExpressionBuilder, Filter } from '@sppc/shared/class';
import { KeyCode } from '@sppc/shared/enum';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'accountRelations-form-component',
  styleUrls: ['./accountRelations.component.css'],
  templateUrl: './accountRelations-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class AccountRelationsFormComponent extends DetailComponent implements OnInit {

  public mainComponentModel: AccountItemBriefInfo;
  public mainComponentSelected: number = 0;
  public relatedComponentSelected: number = 0;
  public relatedComponentCategories: any;
  public relatedComponentCheckedKeys: any[] = [];

  public resultCategories: any[] = [];
  public resultCheckedKeys: any[] = [];

  public searchValue: string;
  public apiUrl: string;
  public resultMessage: boolean;
  /*
  / این دو متغیر برای عملکرد صحیح کزینه انتخاب همه در انگولار 14 ایجاد شد
  **/
  public selectAllKeys: number[] = [];
  public selectAllResultKeys: number[] = [];

  //create properties
  @Input() public active: boolean = false;
  //@Input() public errorMessage: string = '';

  @Input() public set model(item: AccountItemBriefInfo) {
    if (item) {
      this.mainComponentModel = item;
    }
    else {
      this.relatedComponentCategories = undefined;
      this.relatedComponentCheckedKeys = [];
      this.resultCategories = [];
      this.resultCheckedKeys = [];
    }
  }
  @Input() public set mainComponent(id: number) {
    this.mainComponentSelected = id;
  };

  @Input() public set relatedComponent(id: number) {
    this.relatedComponentSelected = id;
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<AccountItemRelationsInfo> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();

    let selectAllIndex = this.relatedComponentCheckedKeys.findIndex(id => id == -1);
    if (selectAllIndex > -1) {
      this.relatedComponentCheckedKeys.splice(selectAllIndex,1);
    }

    var relationModel = new AccountItemRelationsInfo();
    relationModel.id = this.mainComponentModel.id;
    relationModel.relatedItemIds = this.relatedComponentCheckedKeys;

    this.relatedComponentCategories = undefined;
    this.relatedComponentCheckedKeys = [];
    this.resultCategories = [];
    this.resultCheckedKeys = [];

    this.save.emit(relationModel);
    //this.active = true;
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.active = false;

    this.relatedComponentCategories = undefined;
    this.relatedComponentCheckedKeys = [];
    this.resultCategories = [];
    this.resultCheckedKeys = [];

    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }
  //Events

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    private accountRelationsService: AccountRelationsService, public renderer: Renderer2, public metadata: MetaDataService,public elem:ElementRef) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.AccountRelations, undefined,elem);
  }
  ngOnInit(): void {
    this.isCheckedKeys = this.isCheckedKeys.bind(this);
    this.isCheckedResultKeys = this.isCheckedResultKeys.bind(this);
  }

  getApiUrl() {
    if (this.mainComponentSelected > 0 && this.mainComponentModel.id > 0) {
      switch (this.relatedComponentSelected) {
        case AccountRelationsType.Account: {
          if (this.mainComponentSelected > 0) {
            switch (this.mainComponentSelected) {
              case AccountRelationsType.DetailAccount: {
                this.apiUrl = String.Format(AccountRelationApi.AccountsNotRelatedToDetailAccount, this.mainComponentModel.id);
                break;
              }
              case AccountRelationsType.CostCenter: {
                this.apiUrl = String.Format(AccountRelationApi.AccountsNotRelatedToCostCenter, this.mainComponentModel.id);
                break;
              }
              case AccountRelationsType.Project: {
                this.apiUrl = String.Format(AccountRelationApi.AccountsNotRelatedToProject, this.mainComponentModel.id);
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
          this.apiUrl = String.Format(AccountRelationApi.DetailAccountsNotRelatedToAccount, this.mainComponentModel.id);
          break;
        }
        case AccountRelationsType.CostCenter: {
          this.apiUrl = String.Format(AccountRelationApi.CostCentersNotRelatedToAccount, this.mainComponentModel.id);
          break;
        }
        case AccountRelationsType.Project: {
          this.apiUrl = String.Format(AccountRelationApi.ProjectsNotRelatedToAccount, this.mainComponentModel.id);
          break;
        }
        default: {
          break;
        }
      }
    }
  }

  public checkById(item: TreeItem) {
    return item.dataItem.id;
  }

  public handleCheckedChange(itemLookup: TreeItemLookup): void {
    var item = itemLookup.item.dataItem;

    if (!this.relatedComponentCheckedKeys.find(f => f == item.id)) {
      let index = this.resultCategories.findIndex(f => f.id == item.id);
      if (index > -1) {
        this.resultCategories.splice(index, 1);
      }
    }
    else if (item.id != -1) {
      this.resultCategories.push(item);
      this.resultCheckedKeys.push(item.id);
    }

    if (item.id == -1) {
      this.selectAll();
    }
  }

  selectAll() {
    let selectedAllItems;
    let resultIsFull;

    if (this.relatedComponentCategories.length > 0) {
      selectedAllItems = this.relatedComponentCategories.length == this.relatedComponentCheckedKeys.length;
      resultIsFull = this.relatedComponentCategories.length-1 == this.resultCategories.length;

      this.relatedComponentCategories.forEach(item => {
          if (selectedAllItems && this.resultCategories.filter(i => i.id == item.id).length == 0 && item.id != -1) {
            this.resultCategories.push(item);
            this.resultCheckedKeys.push(item.id);
          } else {

            let index = this.resultCategories.findIndex(f => f.id == item.id);
            if (index > -1 && resultIsFull) {
              this.resultCategories.splice(index, 1);
            }
          }
      });
    }
  }

  public handleResultCheckedChange(itemLookup: TreeItemLookup): void {
    let item = itemLookup.item.dataItem;

    if (this.relatedComponentCheckedKeys.find(f => f == item.id)) {
      let index = this.relatedComponentCheckedKeys.findIndex(f => f == item.id);
      if (index > -1) {
        this.relatedComponentCheckedKeys.splice(index, 1);
      }
    }

    let index = this.resultCategories.findIndex(f => f.id == item.id);
    if (index > -1) {
      this.resultCategories.splice(index, 1);
    }

    let selectAllIndex = this.relatedComponentCheckedKeys.findIndex(f => f == -1);
    if (selectAllIndex > -1) {
      this.relatedComponentCheckedKeys.splice(selectAllIndex, 1);
    }
  }

  isCheckedKeys(e:any) {
    if (this.relatedComponentCheckedKeys.find(f => f == e.id)) {
      return 'checked'
    } else {
      return 'none'
    }
  }

  isCheckedResultKeys(e:any) {
    if (this.resultCategories.find(f => f.id == e.id)) {
      return 'checked'
    } else {
      return 'none'
    }
  }

  getTitleText(text: string) {
    return String.Format(text, this.mainComponentModel.name + " - (" + this.mainComponentModel.fullCode + ")");
  }

  onKeyChange(e: any) {
    if (KeyCode.Enter == e) {
      this.onSearch();
    }
  }

  onSearch() {
    this.resultMessage = false;

    let filterExp: FilterExpression | undefined;

    if (this.searchValue) {
      var filterExpBuilder = new FilterExpressionBuilder();
      filterExp = filterExpBuilder.New(new Filter("Name", this.searchValue, ".Contains({0})", "System.String"))
        .Or(new Filter("FullCode", this.searchValue, ".Contains({0})", "System.String"))
        .Build();
    }

    //this.sppcLoading.show();
    this.getApiUrl();
    this.accountRelationsService.getRelatedComponentModel(this.apiUrl, filterExp)
    .subscribe((res) => {
      this.relatedComponentCategories = res;
      
      if (this.relatedComponentCategories.length == 0) {
        this.resultMessage = true;
      } else {
        this.relatedComponentCategories.map(item => item.parentId = -1);
        let selectAllExp = "AccountRelations.SelectAll";
        this.relatedComponentCategories.push({
          id: -1,
          parentId: null,
          name: selectAllExp
        });
        console.log(this.relatedComponentCategories);
      }
      
    })
  }

  /**
   * حذف فیلتر
   */
  removeFilter() {
    this.searchValue = '';
    this.onSearch();
  }
}
