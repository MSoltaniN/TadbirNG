import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
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
import { AccountItemBriefInfo, AccountRelationsService } from '../../service/index';
import { AccountRelationApi } from '../../service/api/index';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { TreeItem, TreeItemLookup } from '@progress/kendo-angular-treeview';
import { KeyCode } from '../../enum/KeyCode';
import { Filter } from '../../class/filter';



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

export class AccountRelationsFormComponent extends DefaultComponent {

    public mainComponentModel: AccountItemBriefInfo;
    public relatedComponentCategories: any;
    public relatedComponentCheckedKeys: any[] = [];

    public resualtCategories: any[] = [];
    public resualtCheckedKeys: any[] = [];

    public searchValue: string;
    public apiUrl: string;

    //create properties
    @Input() public active: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set model(item: AccountItemBriefInfo) {
        if (item) {
            this.mainComponentModel = item;
            this.loadRelatedList();
        }
    }
    @Input() public mainComponentSelected: number = 0;
    @Input() public relatedComponentSelected: number = 0;

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    //@Output() save: EventEmitter<Branch> = new EventEmitter();
    //create properties

    //Events
    public onSave(e: any): void {
        //e.preventDefault();
        //this.save.emit(this.editForm.value);
        //this.active = true;
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        //this.isNew = false;
        this.active = false;

        this.relatedComponentCategories = undefined;
        this.relatedComponentCheckedKeys = [];
        this.resualtCategories = [];
        this.resualtCheckedKeys = [];


        this.cancel.emit();
    }
    //Events

    constructor(public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService,
        public accountRelationsService: AccountRelationsService, public sppcLoading: SppcLoadingService) {
        super(toastrService, translate, renderer, metadata, Entities.AccountRelations, '');

    }

    loadRelatedList() {
        if (this.mainComponentSelected > 0 && this.mainComponentModel.id > 0) {
            switch (this.relatedComponentSelected) {
                case 1: {
                    if (this.mainComponentSelected > 0) {
                        switch (this.mainComponentSelected) {
                            case 2: {
                                this.apiUrl = String.Format(AccountRelationApi.AccountsNotRelatedToDetailAccount, this.mainComponentModel.id);
                                break;
                            }
                            case 3: {
                                this.apiUrl = String.Format(AccountRelationApi.AccountsNotRelatedToCostCenter, this.mainComponentModel.id);
                                break;
                            }
                            case 4: {
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
                case 2: {
                    this.apiUrl = String.Format(AccountRelationApi.DetailAccountsNotRelatedToAccount, this.mainComponentModel.id);
                    break;
                }
                case 3: {
                    this.apiUrl = String.Format(AccountRelationApi.CostCentersNotRelatedToAccount, this.mainComponentModel.id);
                    break;
                }
                case 4: {
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
            var index = this.resualtCategories.findIndex(f => f.id == item.id);
            if (index > -1) {
                this.resualtCategories.splice(index, 1);
            }
        }
        else {
            this.resualtCategories.push(item);
            this.resualtCheckedKeys.push(item.id);
        }      
    }

    public handleResualtCheckedChange(itemLookup: TreeItemLookup): void {
        var item = itemLookup.item.dataItem;
        if (this.relatedComponentCheckedKeys.find(f => f == item.id)) {
            var index = this.relatedComponentCheckedKeys.findIndex(f => f == item.id);
            if (index > -1) {
                this.relatedComponentCheckedKeys.splice(index, 1);
            }
        }

        var index = this.resualtCategories.findIndex(f => f.id == item.id);
        if (index > -1) {
            this.resualtCategories.splice(index, 1);
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
        var filters: Filter[] = [];
        if (this.searchValue) {
            filters.push(new Filter("Name", this.searchValue, ".Contains({0})", "System.String"));
        }

        this.sppcLoading.show();
        this.apiUrl = "http://localhost:8801/relations/account/1/faccounts";
        this.accountRelationsService.getRelatedComponentModel(this.apiUrl, filters).subscribe(res => {
            this.relatedComponentCategories = res;
            this.sppcLoading.hide();
        })
    }
}