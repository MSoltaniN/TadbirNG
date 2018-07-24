import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';

import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';
import { String } from '../../class/source';

import { Observable } from 'rxjs/Observable';
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { ViewRowPermissionService, ItemInfo } from '../../service/index';
import { LookupApi } from '../../service/api/index';
import { TreeItemLookup } from '@progress/kendo-angular-treeview';



export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'viewRowPermission-single-form-component',
    templateUrl: './viewRowPermission-single-form.component.html',
    styles: [`
.section-header {border: solid 1px #337ab7; padding: 7px 10px;}
.section-body {padding: 10px; border: solid 1px #337ab7; border-radius: 3px; height: 400px; overflow-y: scroll;}
`],
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class ViewRowPermissionSingleFormComponent extends DefaultComponent {

    public singleFormCategories: any[] = [];
    public singleFormData: any[] = [];

    public singleFormCheckedKey: number = 0;
    public searchValue: string;

    //create properties
    @Input() public active: boolean = false;
    @Input() public errorMessage: string = '';

    //@Input() public set model(item: AccountItemBriefInfo) {
    //    if (item) {
    //        this.mainComponentModel = item;
    //    }
    //    else {
    //        this.relatedComponentCategories = undefined;
    //        this.relatedComponentCheckedKeys = [];
    //        this.resultCategories = [];
    //        this.resultCheckedKeys = [];
    //    }
    //}

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<ItemInfo> = new EventEmitter();
    //create properties

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        this.save.emit(this.singleFormCategories.find(f => f.key == this.singleFormCheckedKey));
        this.singleFormCheckedKey = 0;
        //this.active = true;
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.active = false;

        this.cancel.emit();
    }
    //Events

    constructor(public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService,
        public viewRowPermissionService: ViewRowPermissionService, public sppcLoading: SppcLoadingService) {
        super(toastrService, translate, renderer, metadata, Entities.ViewRowPermission, '');

        this.getCategories();
    }

    onSearch() {
        if (this.searchValue)
            this.singleFormData = this.singleFormCategories.filter((s) => s.value.toLowerCase().indexOf(this.searchValue.toLowerCase()) !== -1);
        else
            this.singleFormData = this.singleFormCategories;
    }

    onKeyChange() {
        this.onSearch();
    }

    public handleSingleFormChecking(itemLookup: TreeItemLookup): void {
        var itemId = itemLookup.item.dataItem.key;
        if (this.singleFormCheckedKey == itemId)
            this.singleFormCheckedKey = 0;
        else
            this.singleFormCheckedKey = itemId
    }

    getCategories() {
        this.viewRowPermissionService.getAll(LookupApi.EntityViews).subscribe(res => {
            var data = res.json();
            this.singleFormCategories = data;
            this.singleFormData = data;
        })
    }

}