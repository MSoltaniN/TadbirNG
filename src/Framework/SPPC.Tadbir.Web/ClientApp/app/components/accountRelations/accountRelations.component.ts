import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";

import { MessageType, Layout, Entities, Metadatas } from "../../enviroment";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { AccountRelationApi, AccountApi, DetailAccountApi, CostCenterApi, ProjectApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { AccountRelationPermissions } from '../../security/permissions';
import { AccountRelationsService } from '../../service/index';
import { TreeItemLookup, TreeItem, CheckableSettings } from '@progress/kendo-angular-treeview';
import { AccountItemRelationsInfo } from '../../service/accountRelations.service';
import { Filter } from '../../class/filter';
import { KeyCode } from '../../enum/KeyCode';


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
    //public isEnableSaveBtn: boolean = false;

    public mainComponentCategories: any;
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

    public errorMessage = String.Empty;

    public ngOnInit(): void {
        this.viewAccess = this.isAccess(SecureEntity.AccountRelations, AccountRelationPermissions.ViewRelationships);
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private accountRelationsService: AccountRelationsService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.AccountRelations, '');

        this.mainComponent = [
            { value: "AccountRelations.Account", key: 1 },
            { value: "AccountRelations.DetailAccount", key: 2 },
            { value: "AccountRelations.CostCenter", key: 3 },
            { value: "AccountRelations.Project", key: 4 }
        ];
    }


    public handleMainComponentDropDownChange(item: any) {
        this.mainComponentCategories = undefined;
        this.mainComponentCheckedKeys = [];
        this.mainComponentExpandedKeys = [];
        this.mainComponentDropdownSelected = 0;
        this.mainComponentSelectedItem = 0;
        this.relatedComponentCheckedKeys = [];
        this.relatedComponentCategories = undefined;
        this.relatedComponentDropdownSelected = 0;

        if (item > 0) {
            this.isDisableRelatedComponnet = false;
            this.isEnableMainComponentSearchBtn = true;
            this.mainComponentDropdownSelected = item;
            this.relatedComponent = [
                { value: "AccountRelations.Account", key: 1 }
            ];
            switch (item) {
                case 1: {
                    this.mainComponentApiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchAccounts, this.FiscalPeriodId, this.BranchId);
                    this.relatedComponent = [
                        { value: "AccountRelations.DetailAccount", key: 2 },
                        { value: "AccountRelations.CostCenter", key: 3 },
                        { value: "AccountRelations.Project", key: 4 }
                    ];
                    break;
                }
                case 2: {
                    this.mainComponentApiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchDetailAccounts, this.FiscalPeriodId, this.BranchId);
                    break
                }
                case 3: {
                    this.mainComponentApiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchCostCenters, this.FiscalPeriodId, this.BranchId);
                    break
                }
                case 4: {
                    this.mainComponentApiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchProjects, this.FiscalPeriodId, this.BranchId);
                    break
                }
                default:
                    {
                        break;
                    }
            }
        }
        else {
            this.mainComponentDropdownSelected = 0;
            this.selectedRelatedComponentValue = null;
            this.isDisableRelatedComponnet = true;
            this.isEnableMainComponentSearchBtn = false;
            this.mainComponentCategories = undefined;
            this.relatedComponentCategories = undefined;
            this.relatedComponentCheckedKeys = [];
            this.relatedComponentDropdownSelected = 0;
            this.mainComponentSelectedItem = 0;
            //this.isEnableSaveBtn = false;
        }
    }

    public handleRelatedComponentDropDownChange(item: any) {
        this.relatedComponentCheckedKeys = [];
        if (item > 0) {
            this.relatedComponentDropdownSelected = item;
            this.isEnableRelatedComponentSearchBtn = true;
            this.loadRelatedComponent();
        }
        else {
            this.relatedComponentDropdownSelected = 0;
            this.relatedComponentCategories = undefined;
            //this.isEnableSaveBtn = false;
            this.isEnableRelatedComponentSearchBtn = false;
        }
    }

    public handleMainComponentChecking(itemLookup: TreeItemLookup): void {
        //this.isEnableSaveBtn = false;
        var itemId = itemLookup.item.dataItem.id;
        if (this.mainComponentCheckedKeys.find(f => f == itemId) == itemId) {
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

    public checkById(item: TreeItem) {
        return item.dataItem.id;
    }

    public hasChildren = (item: any) => {
        if (item.childCount > 0) {
            return true;
        }
        return false;
    };

    public fetchMainComponentChildren = (item: any) => {
        var apiUrl = String.Empty;
        switch (this.mainComponentDropdownSelected) {
            case 1: {
                apiUrl = String.Format(AccountApi.AccountChildren, item.id);
                break;
            }
            case 2: {
                apiUrl = String.Format(DetailAccountApi.DetailAccountChildren, item.id);
                break;
            }
            case 3: {
                apiUrl = String.Format(CostCenterApi.CostCenterChildren, item.id);
                break;
            }
            case 4: {
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

    public childrenLoadedHandler = (dataItem: any) => {
        this.fechedRelatedComponentChildren.subscribe(res => {
            for (let item of res) {
                if (item.isSelected && !this.relatedComponentCheckedKeys.find(f => f == item.id)) {
                    this.relatedComponentCheckedKeys.push(item.id);
                }
            }
        })
    }

    loadRelatedComponent() {
        this.isDisableRelatedComponnet = false;
        this.relatedComponentCheckedKeys = [];
        this.relatedComponentExpandedKeys = [];
        if (this.relatedComponentDropdownSelected > 0 && this.mainComponentSelectedItem > 0) {
            switch (this.relatedComponentDropdownSelected) {
                case 1: {
                    if (this.mainComponentDropdownSelected > 0) {
                        switch (this.mainComponentDropdownSelected) {
                            case 2: {
                                this.relatedComponentApiUrl = String.Format(AccountRelationApi.AccountsRelatedToDetailAccount, this.mainComponentSelectedItem);
                                break;
                            }
                            case 3: {
                                this.relatedComponentApiUrl = String.Format(AccountRelationApi.AccountsRelatedToCostCenter, this.mainComponentSelectedItem);
                                break;
                            }
                            case 4: {
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
                case 2: {
                    this.relatedComponentApiUrl = String.Format(AccountRelationApi.DetailAccountsRelatedToAccount, this.mainComponentSelectedItem);
                    break;
                }
                case 3: {
                    this.relatedComponentApiUrl = String.Format(AccountRelationApi.CostCentersRelatedToAccount, this.mainComponentSelectedItem);
                    break;
                }
                case 4: {
                    this.relatedComponentApiUrl = String.Format(AccountRelationApi.ProjectsRelatedToAccount, this.mainComponentSelectedItem);
                    break;
                }
                default: {
                    break;
                }
            }
            this.sppcLoading.show();
            this.accountRelationsService.getRelatedComponentModel(this.relatedComponentApiUrl).subscribe(res => {
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
                this.sppcLoading.hide();
            })
            //this.isEnableSaveBtn = true;
        }
    }

    onCreateRelation() {
        this.isActive = true;
    }

    DeleteRelation() {
        this.errorMessage = String.Empty;
        this.sppcLoading.show();
        var model = new AccountItemRelationsInfo();
        model.id = this.mainComponentSelectedItem;
        model.relatedItemIds = this.relatedComponentCheckedKeys;
        var apiUrl = String.Empty;
        if (this.relatedComponentDropdownSelected > 0) {
            switch (this.relatedComponentDropdownSelected) {
                case 1: {
                    if (this.mainComponentDropdownSelected > 0) {
                        switch (this.mainComponentDropdownSelected) {
                            case 2: {
                                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToDetailAccount, model.id);
                                break;
                            }
                            case 3: {
                                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToCostCenter, model.id);
                                break;
                            }
                            case 4: {
                                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToProject, model.id);
                                break;
                            }
                            default: {
                                break;
                            }
                        }
                    }
                    break;
                }
                case 2: {
                    apiUrl = String.Format(AccountRelationApi.DetailAccountsRelatedToAccount, model.id);
                    break;
                }
                case 3: {
                    apiUrl = String.Format(AccountRelationApi.CostCentersRelatedToAccount, model.id);
                    break;
                }
                case 4: {
                    apiUrl = String.Format(AccountRelationApi.ProjectsRelatedToAccount, model.id);
                    break;
                }
                default: {
                    break;
                }
            }
        }
        this.accountRelationsService.edit<AccountItemRelationsInfo>(apiUrl, model).subscribe(response => {
            this.sppcLoading.hide();
            this.showMessage(this.updateMsg, MessageType.Succes);
        }, (error => {
            this.errorMessage = error;
            this.sppcLoading.hide();
        }));

        this.loadRelatedComponent();
    }

    cancelHandler() {
        this.isActive = false;
    }

    onCancel() {
        this.mainComponentCategories = undefined;
        this.relatedComponentCategories = undefined;
        this.mainComponentCheckedKeys = [];
        this.relatedComponentCheckedKeys = [];
        this.mainComponentSelectedItem = 0;
        //this.isEnableSaveBtn = false;
        this.mainComponentDropdownSelected = 0;
        this.relatedComponentDropdownSelected = 0;
        this.errorMessage = String.Empty;
        this.isDisableRelatedComponnet = true;
        this.isEnableMainComponentSearchBtn = false;
        this.isEnableRelatedComponentSearchBtn = false;

        this.relatedComponentApiUrl = String.Empty;
        this.mainComponentApiUrl = String.Empty;

        this.searchValue = String.Empty;
        this.relatedSearchValue = String.Empty;
        this.noResultMessage = false;
        this.noRelatedResultMessage = false;
    }

    onMainComponentSearch() {
        var filters: Filter[] = [];
        if (this.searchValue) {
            filters.push(new Filter("Name", this.searchValue, ".Contains({0})", "System.String"));
        }

        this.sppcLoading.show();
        this.accountRelationsService.getMainComponentModel(this.mainComponentApiUrl, filters).subscribe(res => {
            this.mainComponentCategories = res.json();
            if (this.mainComponentCategories.length == 0)
                this.noResultMessage = true;
            else
                this.noResultMessage = false;



            this.sppcLoading.hide();
        });
    }

    onKeyMainComponent(e: any) {
        if (KeyCode.Enter == e && this.mainComponentApiUrl) {
            this.onMainComponentSearch();
        }
    }

    onRelatedComponentSearch() {
        if (this.relatedComponentDropdownSelected > 0 && this.mainComponentSelectedItem > 0) {
            this.sppcLoading.show();
            var filters: Filter[] = [];
            if (this.relatedSearchValue) {
                filters.push(new Filter("Name", this.relatedSearchValue, ".Contains({0})", "System.String"));
            }

            this.accountRelationsService.getRelatedComponentModel(this.relatedComponentApiUrl, filters).subscribe(res => {
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

                this.sppcLoading.hide();
            })
        }        
    }

    onKeyRelatedComponent(e: any) {
        if (KeyCode.Enter == e && this.relatedComponentApiUrl) {
            this.onRelatedComponentSearch();
        }
    }
}


