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
import { AccountRelationsService } from '../../service/index';
import { TreeItemLookup, TreeItem, CheckableSettings } from '@progress/kendo-angular-treeview';
import { AccountItemRelationsInfo } from '../../service/accountRelations.service';


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

    //permission flag
    viewAccess: boolean;
    manageAccess: boolean;


    public mainComponent: Array<Item>;
    public relatedComponent: Array<Item>;
    public selectedMainComponentValue: number | null;
    public selectedRelatedComponentValue: number | null;

    public isDisableRelatedComponnet: boolean = true;
    public isSelectedMainComponent: boolean = false;
    public isEnableSaveBtn: boolean = false;

    public mainComponentCategories: any;
    public mainComponentCheckedKeys: any[] = [];
    public mainComponentSelectedItem: number = 0;
    public mainComponentDropdownSelected: number = 0;

    public relatedComponentCategories: any;
    public relatedComponentCheckedKeys: any[] = [];
    public relatedComponentDropdownSelected: number = 0;

    public errorMessage = String.Empty;

    public ngOnInit(): void {
        this.viewAccess = this.isAccess(SecureEntity.AccountRelations, AccountRelationPermissions.ViewRelationships);
        this.manageAccess = this.isAccess(SecureEntity.AccountRelations, AccountRelationPermissions.ManageRelationships);
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
        this.mainComponentCheckedKeys = [];
        this.relatedComponentCheckedKeys = [];
        this.mainComponentDropdownSelected = 0;
        this.mainComponentSelectedItem = 0;
        this.relatedComponentCategories = undefined;

        if (item > 0) {
            var apiUrl = String.Empty;
            this.isDisableRelatedComponnet = false;
            this.mainComponentDropdownSelected = item;
            this.sppcLoading.show();
            this.relatedComponent = [
                { value: "AccountRelations.Account", key: 1 }
            ];
            switch (item) {
                case 1: {
                    apiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchAccounts, this.FiscalPeriodId, this.BranchId);
                    this.relatedComponent = [
                        { value: "AccountRelations.DetailAccount", key: 2 },
                        { value: "AccountRelations.CostCenter", key: 3 },
                        { value: "AccountRelations.Project", key: 4 }
                    ];
                    break;
                }
                case 2: {
                    apiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchDetailAccounts, this.FiscalPeriodId, this.BranchId);
                    break
                }
                case 3: {
                    apiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchCostCenters, this.FiscalPeriodId, this.BranchId);
                    break
                }
                case 4: {
                    apiUrl = String.Format(AccountRelationApi.FiscalPeriodBranchProjects, this.FiscalPeriodId, this.BranchId);
                    break
                }
                default:
                    {
                        this.sppcLoading.hide();
                        break;
                    }
            }
            this.accountRelationsService.getAccountCategories(apiUrl).subscribe(res => {
                this.mainComponentCategories = res.json();
                this.sppcLoading.hide();
            });
        }
        else {
            this.mainComponentDropdownSelected = 0;
            this.selectedRelatedComponentValue = null;
            this.isDisableRelatedComponnet = true;
            this.mainComponentCategories = undefined;
            this.relatedComponentCategories = undefined;
            this.relatedComponentCheckedKeys = [];
            this.relatedComponentDropdownSelected = 0;
            this.mainComponentSelectedItem = 0;
            this.isEnableSaveBtn = false;
        }
    }

    public handleRelatedComponentDropDownChange(item: any) {
        this.relatedComponentCheckedKeys = [];
        if (item > 0) {
            this.relatedComponentDropdownSelected = item;
            this.loadRelatedComponent();
        }
        else {
            this.relatedComponentDropdownSelected = 0;
            this.relatedComponentCategories = undefined;
            this.isEnableSaveBtn = false;
        }
    }

    public handleMainComponentChecking(itemLookup: TreeItemLookup): void {
        this.isEnableSaveBtn = false;
        var itemId = itemLookup.item.dataItem.id;
        if (this.mainComponentCheckedKeys.find(f => f == itemId) == itemId) {
            this.mainComponentCheckedKeys = [];
            this.relatedComponentCheckedKeys = [];
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

    public fetchRelatedComponentChildren = (item: any) => {
        var apiUrl = String.Empty;
        switch (this.relatedComponentDropdownSelected) {
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

    loadRelatedComponent() {
        this.sppcLoading.show();
        this.isDisableRelatedComponnet = false;
        this.relatedComponentCheckedKeys = [];
        var apiUrl = String.Empty;
        if (this.relatedComponentDropdownSelected > 0 && this.mainComponentSelectedItem > 0) {
            switch (this.relatedComponentDropdownSelected) {
                case 1: {
                    if (this.mainComponentDropdownSelected > 0) {
                        switch (this.mainComponentDropdownSelected) {
                            case 2: {
                                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToDetailAccount, this.mainComponentSelectedItem);
                                break;
                            }
                            case 3: {
                                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToCostCenter, this.mainComponentSelectedItem);
                                break;
                            }
                            case 4: {
                                apiUrl = String.Format(AccountRelationApi.AccountsRelatedToProject, this.mainComponentSelectedItem);
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
                    apiUrl = String.Format(AccountRelationApi.DetailAccountsRelatedToAccount, this.mainComponentSelectedItem);
                    break;
                }
                case 3: {
                    apiUrl = String.Format(AccountRelationApi.CostCentersRelatedToAccount, this.mainComponentSelectedItem);
                    break;
                }
                case 4: {
                    apiUrl = String.Format(AccountRelationApi.ProjectsRelatedToAccount, this.mainComponentSelectedItem);
                    break;
                }
                default: {
                    break;
                }
            }

            this.accountRelationsService.getRelatedComponentModel(apiUrl).subscribe(res => {
                this.relatedComponentCategories = res;

                for (let item of res) {
                    if (item.isSelected) {
                        this.relatedComponentCheckedKeys.push(item.id)
                    }
                }
                this.sppcLoading.hide();
            })
            this.isEnableSaveBtn = true;
        }
        this.sppcLoading.hide();
    }

    onSave() {
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
    }

    onCancel() {
        this.mainComponentCategories = undefined;
        this.relatedComponentCategories = undefined;
        this.mainComponentCheckedKeys = [];
        this.relatedComponentCheckedKeys = [];
        this.mainComponentSelectedItem = 0;
        this.isEnableSaveBtn = false;
        this.mainComponentDropdownSelected = 0;
        this.relatedComponentDropdownSelected = 0;
        this.errorMessage = String.Empty;
    }

}

