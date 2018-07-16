﻿import { Component, OnInit, Input, Renderer2 } from '@angular/core';
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
import { AccountRelationsService, AccountItemBriefInfo } from '../../service/index';
import { TreeItemLookup, TreeItem, CheckableSettings } from '@progress/kendo-angular-treeview';
import { AccountItemRelationsInfo } from '../../service/accountRelations.service';
import { Filter } from '../../class/filter';
import { KeyCode } from '../../enum/KeyCode';
import { AccountRelationsType } from '../../enum/accountRelationType';
import { FilterExpressionBuilder } from '../../class/filterExpressionBuilder';
import { FilterExpression } from '../../class/filterExpression';


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

    public errorMessage = String.Empty;

    public ngOnInit(): void {
        this.viewAccess = this.isAccess(SecureEntity.AccountRelations, AccountRelationPermissions.ViewRelationships);
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private accountRelationsService: AccountRelationsService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.AccountRelations, '');

        this.mainComponent = [
            { value: "AccountRelations.Account", key: AccountRelationsType.Account },
            { value: "AccountRelations.DetailAccount", key: AccountRelationsType.DetailAccount },
            { value: "AccountRelations.CostCenter", key: AccountRelationsType.CostCenter },
            { value: "AccountRelations.Project", key: AccountRelationsType.Project }
        ];
    }


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

        if (item > 0) {
            this.isDisableRelatedComponnet = false;
            this.isEnableMainComponentSearchBtn = true;
            this.mainComponentDropdownSelected = item;
            this.relatedComponent = [
                { value: "AccountRelations.Account", key: AccountRelationsType.Account }
            ];
            switch (item) {
                case AccountRelationsType.Account: {
                    this.mainComponentApiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchAccounts, this.FiscalPeriodId, this.BranchId);
                    this.relatedComponent = [
                        { value: "AccountRelations.DetailAccount", key: AccountRelationsType.DetailAccount },
                        { value: "AccountRelations.CostCenter", key: AccountRelationsType.CostCenter },
                        { value: "AccountRelations.Project", key: AccountRelationsType.Project }
                    ];
                    break;
                }
                case AccountRelationsType.DetailAccount: {
                    this.mainComponentApiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchDetailAccounts, this.FiscalPeriodId, this.BranchId);
                    break
                }
                case AccountRelationsType.CostCenter: {
                    this.mainComponentApiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchCostCenters, this.FiscalPeriodId, this.BranchId);
                    break
                }
                case AccountRelationsType.Project: {
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
            this.selectedRelatedComponentValue = null;
            this.isDisableRelatedComponnet = true;
            this.isEnableMainComponentSearchBtn = false;
        }
    }

    public handleRelatedComponentDropDownChange(item: any) {
        this.relatedComponentCheckedKeys = [];
        this.deleteKey = [];
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

    public handleMainComponentChecking(itemLookup: TreeItemLookup): void {

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
            this.sppcLoading.show();
            this.accountRelationsService.getRelatedComponentModel(this.relatedComponentApiUrl).subscribe(res => {
                this.relatedComponentCategories = res;
                for (let item of res) {
                    if (item.isSelected) {
                        this.relatedComponentCheckedKeys.push(item.id)
                    }
                }
                this.deleteKey = [];
                if (this.relatedComponentCategories.length == 0)
                    this.noRelatedResultMessage = true;
                else
                    this.noRelatedResultMessage = false;
                this.sppcLoading.hide();
            })
        }
    }

    onCreateRelation() {
        if (this.relatedComponentDropdownSelected > 0 && this.mainComponentSelectedItem > 0) {
            this.mainComponentModel = this.mainComponentCategories.find(f => f.id == this.mainComponentSelectedItem);
            this.isActive = true;
        }
    }

    DeleteRelation() {
        var model = new AccountItemRelationsInfo();
        model.id = this.mainComponentSelectedItem;
        model.relatedItemIds = this.relatedComponentCheckedKeys;
        this.saveRelations(model);
    }

    cancelHandler() {
        this.isActive = false;
        this.mainComponentModel = undefined;
    }

    saveHandler(relationModel: AccountItemRelationsInfo) {

        if (relationModel) {
            var keyArray = this.relatedComponentCheckedKeys.concat(relationModel.relatedItemIds, this.deleteKey);
            relationModel.relatedItemIds = keyArray;

            this.saveRelations(relationModel);
            this.isActive = false;
        }
    }

    saveRelations(relationsModel: AccountItemRelationsInfo) {
        this.errorMessage = String.Empty;
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
        this.sppcLoading.show();
        this.accountRelationsService.edit<AccountItemRelationsInfo>(apiUrl, relationsModel).subscribe(response => {
            this.sppcLoading.hide();
            this.showMessage(this.updateMsg, MessageType.Succes);

            this.mainComponentModel = undefined;
            this.loadRelatedComponent();

        }, (error => {
            this.errorMessage = error;
            this.sppcLoading.hide();
        }));


    }

    onCancel() {
        this.mainComponentCategories = [];
        this.relatedComponentCategories = undefined;
        this.mainComponentCheckedKeys = [];
        this.relatedComponentCheckedKeys = [];
        this.mainComponentSelectedItem = 0;
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
        this.deleteKey = [];
    }

    onMainComponentSearch() {
        let filterExp: FilterExpression | undefined;

        if (this.searchValue) {
            var filterExpBuilder = new FilterExpressionBuilder();
            filterExp = filterExpBuilder.New(new Filter("Name", this.searchValue, ".Contains({0})", "System.String"))
                .Or(new Filter("Code", this.searchValue, ".Contains({0})", "System.String"))
                .Build();
        }

        this.sppcLoading.show();
        this.accountRelationsService.getMainComponentModel(this.mainComponentApiUrl, filterExp).subscribe(res => {
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

            let filterExp: FilterExpression | undefined;

            if (this.relatedSearchValue) {
                var filterExpBuilder = new FilterExpressionBuilder();
                filterExp = filterExpBuilder.New(new Filter("Name", this.searchValue, ".Contains({0})", "System.String"))
                    .Or(new Filter("Code", this.searchValue, ".Contains({0})", "System.String"))
                    .Build();
            }

            this.sppcLoading.show();
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


