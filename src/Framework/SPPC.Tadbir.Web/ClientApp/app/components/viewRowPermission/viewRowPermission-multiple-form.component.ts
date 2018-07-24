import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';

import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';
import { String } from '../../class/source';

import { Observable } from 'rxjs/Observable';
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas, Environment } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { ViewRowPermissionService, ItemInfo, RowPermissionsForRoleInfo, ViewRowPermissionInfo } from '../../service/index';
import { TreeItem, TreeItemLookup } from '@progress/kendo-angular-treeview';
import { FilterExpression } from '../../class/filterExpression';
import { FilterExpressionBuilder } from '../../class/filterExpressionBuilder';
import { Filter } from '../../class/filter';



export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'viewRowPermission-multiple-form-component',
    templateUrl: './viewRowPermission-multiple-form.component.html',
    styles: [`
/deep/ #multipleForm > .k-dialog { width: 800px; }
@media screen and (max-width:800px) {
    /deep/ #multipleForm > .k-dialog { width: 90%; min-width:250px; }
}
#main-section { border: solid 1px #337ab7; border-radius: 3px; margin:0;}
    #main-section > .col-sm-6 { padding: 0; }
    @media screen and (max-width:768px){ #main-section > .col-sm-6 { float:unset !important; }}
.section-header { border: solid 1px #337ab7; padding: 7px 10px;}
.section-body { padding: 10px; border-right: solid 1px #337ab7; border-left: solid 1px #337ab7; height: 450px; overflow-y: scroll; }
.header-label { margin: 6px 0 5px; display: block; } #frm-btn{ margin-top:15px; }
`],
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class ViewRowPermissionMultipleFormComponent extends DefaultComponent {

    roleItem: ItemInfo;

    noData: boolean = false;
    searchValue: string;
    rowPermission: ViewRowPermissionInfo;

    entityName: string = '';
    viewId: number;

    public rowList: ItemInfo[] = [];
    public rowCheckedKeys: number[] = [];

    public selectedRowList: ItemInfo[] = [];
    public selectedRowKeys: number[] = [];

    //create properties
    @Input() public active: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set entity(item: ItemInfo) {
        if (item) {
            this.entityName = item.value;
        }
    }

    @Input() public set dataItem(item: ViewRowPermissionInfo) {
        if (item) {
            this.rowPermission = item;
            this.getFetchUrl();
        }
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<number[]> = new EventEmitter();
    //create properties

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        this.save.emit(this.selectedRowKeys);
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.active = false;
        this.rowList = [];
        this.rowCheckedKeys = [];
        this.selectedRowList = [];
        this.selectedRowKeys = [];
        this.cancel.emit();
    }
    //Events

    constructor(public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService,
        public viewRowPermissionService: ViewRowPermissionService, public sppcLoading: SppcLoadingService) {
        super(toastrService, translate, renderer, metadata, Entities.ViewRowPermission, '');
        //this.getFetchUrl();
    }

    getFetchUrl() {
        this.metadata.getMetaDataById(this.rowPermission.viewId).subscribe(res => {
            var fetchUrl = res.fetchUrl;
            if (fetchUrl) {
                this.loadRowList(fetchUrl);
            }
            else {
                this.noData = true;
            }
        })
    }

    public checkByKey(item: TreeItem) {
        return item.dataItem.key;
    }

    public handleCheckedChange(itemLookup: TreeItemLookup): void {
        var item = itemLookup.item.dataItem;
        if (this.rowCheckedKeys.find(f => f == item.key)) {
            var index = this.selectedRowList.findIndex(f => f.key == item.key);
            if (index > -1) {
                this.selectedRowList.splice(index, 1);
            }
        }
        else {
            this.selectedRowList.push(item);
            this.selectedRowKeys.push(item.key);
        }
    }

    public handleSelectedRowCheckedChange(itemLookup: TreeItemLookup): void {
        var item = itemLookup.item.dataItem;
        if (this.rowCheckedKeys.find(f => f == item.key)) {
            var index = this.rowCheckedKeys.findIndex(f => f == item.key);
            if (index > -1) {
                this.rowCheckedKeys.splice(index, 1);
            }
        }

        var index = this.selectedRowList.findIndex(f => f.key == item.key);
        if (index > -1) {
            this.selectedRowList.splice(index, 1);
        }
    }

    loadRowList(fetchUrl: string) {
        this.viewRowPermissionService.getRowList(Environment.BaseUrl + String.Format(fetchUrl, this.FiscalPeriodId, this.BranchId)).subscribe(res => {
            this.rowList = res;

            this.rowCheckedKeys = [];
            this.selectedRowList = [];
            this.selectedRowKeys = [];

            for (let item of this.rowList) {
                if (this.rowPermission.items.find(f => f == item.key)) {
                    this.rowCheckedKeys.push(item.key);
                    this.selectedRowList.push(item);
                    this.selectedRowKeys.push(item.key);
                }

            }
        })
    }

    removeAllSelected() {
        this.rowCheckedKeys = [];
        this.selectedRowKeys = [];
        this.selectedRowList = [];
    }

    selectAllRow() {
        this.removeAllSelected();
        for (let item of this.rowList) {
            this.rowCheckedKeys.push(item.key);
            this.selectedRowList.push(item);
            this.selectedRowKeys.push(item.key);
        }
    }

    getTitleText(text: string) {
        return String.Format(text, this.entityName);
    }
}