import { Component, Input, Output, EventEmitter, Renderer2, Host } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { String } from '../../class/source';
import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { ViewRowPermissionService, ItemInfo } from '../../service/index';
import { LookupApi } from '../../service/api/index';
import { TreeItemLookup, TreeItem } from '@progress/kendo-angular-treeview';
import { DetailComponent } from '../../class/detail.component';
import { BrowserStorageService } from '../../service/browserStorage.service';



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

export class ViewRowPermissionSingleFormComponent extends DetailComponent {

    public singleFormCategories: any[] = [];
    public singleFormData: any[] = [];

    public singleFormCheckedKey: number[] = [];
    public searchValue: string;

    selectedKey: number = 0;

    //create properties
    @Input() public active: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set model(item: ItemInfo) {

        this.singleFormCheckedKey = [];
        this.selectedKey = 0;
        if (item) {
            this.singleFormCheckedKey.push(item.key);
            this.selectedKey = item.key;
        }
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<ItemInfo> = new EventEmitter();
    //create properties

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        this.searchValue = String.Empty;
        this.onSearch();
        this.save.emit(this.singleFormCategories.find(f => f.key == this.singleFormCheckedKey));
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.active = false;
        this.singleFormCheckedKey = [];
        if (this.selectedKey>0) {
            this.singleFormCheckedKey.push(this.selectedKey);
        }
        this.cancel.emit();
    }
    //Events

  escPress() {
    this.closeForm();
  }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService,
      public viewRowPermissionService: ViewRowPermissionService, public bStorageService: BrowserStorageService) {
      super(toastrService, translate, bStorageService, renderer, metadata, Entities.RowAccess, undefined);

        this.getCategories();
    }

    onSearch() {
        if (this.searchValue)
            this.singleFormData = this.singleFormCategories.filter((s) => s.value.toLowerCase().indexOf(this.searchValue.toLowerCase()) !== -1);
        else
            this.singleFormData = this.singleFormCategories;
    }

    /**
     * حذف فیلتر
     */
    removeFilter() {
        this.searchValue = '';
        this.onSearch();
    }

    onKeyChange() {
        this.onSearch();
    }

    public checkByKey(item: TreeItem) {
        return item.dataItem.key;
    }

    getCategories() {
        this.viewRowPermissionService.getAll(LookupApi.EntityViews).subscribe(res => {
            var data = res.body;
            this.singleFormCategories = data;
            this.singleFormData = data;
        })
    }

}
