import { Component, Input, Output, EventEmitter, Renderer2, Host } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';

import { Property } from "../../class/metadata/property"
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';
import { String } from '../../class/source';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { AccountItemBriefInfo, AccountRelationsService, AccountItemRelationsInfo } from '../../service/index';
import { AccountRelationApi } from '../../service/api/index';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { TreeItem, TreeItemLookup } from '@progress/kendo-angular-treeview';
import { KeyCode } from '../../enum/KeyCode';
import { Filter } from '../../class/filter';
import { AccountRelationsType } from '../../enum/accountRelationType';
import { FilterExpression } from '../../class/filterExpression';
import { FilterExpressionBuilder } from '../../class/filterExpressionBuilder';
import { DetailComponent } from '../../class/detail.component';



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

export class AccountRelationsFormComponent extends DetailComponent {

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

    //create properties
    @Input() public active: boolean = false;
    @Input() public errorMessage: string = '';

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
    //Events

    constructor(public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService,
        public accountRelationsService: AccountRelationsService, @Host() defaultComponent: DefaultComponent) {
        super(toastrService, translate, renderer, metadata, Entities.AccountRelations, '');
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
        if (this.relatedComponentCheckedKeys.find(f => f == item.id)) {
            var index = this.resultCategories.findIndex(f => f.id == item.id);
            if (index > -1) {
                this.resultCategories.splice(index, 1);
            }
        }
        else {
            this.resultCategories.push(item);
            this.resultCheckedKeys.push(item.id);
        }
    }

    public handleResultCheckedChange(itemLookup: TreeItemLookup): void {
        var item = itemLookup.item.dataItem;
        if (this.relatedComponentCheckedKeys.find(f => f == item.id)) {
            var index = this.relatedComponentCheckedKeys.findIndex(f => f == item.id);
            if (index > -1) {
                this.relatedComponentCheckedKeys.splice(index, 1);
            }
        }

        var index = this.resultCategories.findIndex(f => f.id == item.id);
        if (index > -1) {
            this.resultCategories.splice(index, 1);
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
                .Or(new Filter("Code", this.searchValue, ".Contains({0})", "System.String"))
                .Build();
        }

        //this.sppcLoading.show();
        this.getApiUrl();
        this.accountRelationsService.getRelatedComponentModel(this.apiUrl, filterExp).subscribe(res => {
            this.relatedComponentCategories = res;
            if(this.relatedComponentCategories.length == 0)
                this.resultMessage = true;
            ////this.sppcLoading.hide();
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