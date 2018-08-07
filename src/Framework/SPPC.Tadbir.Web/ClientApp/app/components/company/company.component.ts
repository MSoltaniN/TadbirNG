import { Component, OnInit, Input, Renderer2, Optional, Host, SkipSelf } from '@angular/core';
import { CompanyService, CompanyInfo } from '../../service/index';
import { Company } from '../../model/index';
import { ToastrService } from 'ngx-toastr'; /** add this component for message in client side */

import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState } from '@progress/kendo-angular-grid';

import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';

import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";

import { MessageType, Layout, Entities, Metadatas } from "../../enviroment";
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { CompanyApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { CompanyPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'company',
    templateUrl: './company.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class CompanyComponent extends DefaultComponent implements OnInit {


    //#region Fields
    public Childrens: Array<CompanyComponent>;

    @Input() public parent: Company;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    //permission flag
    viewAccess: boolean;

    ////for add in delete messageText
    deleteConfirm: boolean;
    deleteModelsConfirm: boolean;
    deleteModelId: number;

    currentFilter: FilterExpression;
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    editDataItem?: Company = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;
    addToContainer: boolean = false;

    //#endregion

    //#region Constructor

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private companyService: CompanyService, public renderer: Renderer2, public metadata: MetaDataService,
        @SkipSelf() @Host() @Optional() private parentCompany: CompanyComponent) 
    {
        super(toastrService, translate, renderer, metadata, Entities.Company, Metadatas.Company);
    }  

    //#endregion

    //#region Methods

    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.Company, CompanyPermissions.View);
        this.reloadGrid();
        if (this.parentCompany)
            this.parentCompany.addChildCompany(this);
    }

    /**
     * کامپوننت های فرزند را در متغیری اضافه میکند
     * @param companyComponent کامپوننت شرکت
     */
    public addChildCompany(companyComponent: CompanyComponent) {

        if (this.Childrens == undefined) this.Childrens = new Array<CompanyComponent>();
        if (this.Childrens.findIndex(p => p.parent.id === companyComponent.parent.id) == -1)            
            this.Childrens.push(companyComponent);
        
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteModelsConfirm = true;
    }

    deleteModels(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();           
        }

        this.groupDelete = false;
        this.deleteModelsConfirm = false;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid(insertedModel?: Company) {
        if (this.viewAccess) {
            this.sppcLoading.show();
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }
            var url = String.Format(CompanyApi.CompanyChildren, this.CompanyId);
            if (this.parent) {
                if (this.parent.childCount > 0)
                    url = String.Format(CompanyApi.CompanyChildren, this.parent.id);
            }
            this.companyService.getAll(url, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
                /*
                var resData = res.json();
                var totalCount = 0;
                if (insertedModel) {
                    var rows = (resData as Array<Company>);
                    var index = rows.findIndex(p => p.id == insertedModel.id);
                    if (index >= 0) {
                        resData.splice(index, 1);
                        rows.splice(0, 0, insertedModel);
                    }
                    else {
                        if (rows.length == this.pageSize) {
                            resData.splice(this.pageSize - 1, 1);
                        }

                        rows.splice(0, 0, insertedModel);
                    }
                }
                if (res.headers != null) {
                    var headers = res.headers != undefined ? res.headers : null;
                    if (headers != null) {
                        var retheader = headers.get('X-Total-Count');
                        if (retheader != null)
                            totalCount = parseInt(retheader.toString());
                    }
                }
                this.rowData = {
                    data: resData,
                    total: totalCount
                }
                */

                var resData = res.json();
                
                var totalCount = 0;
                if (insertedModel) {
                    var rows = (resData as Array<Company>);
                    var index = rows.findIndex(p => p.id == insertedModel.id);
                    if (index >= 0) {
                        rows.splice(index, 1);
                        rows.splice(0, 0, insertedModel);
                    }
                    else {
                        if (rows.length == this.pageSize) {
                            rows.splice(this.pageSize - 1, 1);
                        }
                        rows.splice(0, 0, insertedModel);
                    }

                    resData = rows;
                }
                if (res.headers != null) {
                    var headers = res.headers != undefined ? res.headers : null;
                    if (headers != null) {
                        var retheader = headers.get('X-Total-Count');
                        if (retheader != null)
                            totalCount = parseInt(retheader.toString());
                    }
                }

                this.rowData = {
                    data: resData,
                    total: totalCount
                }
                
                //زمانی که تعداد رکورد ها صفر باشد باید کامپوننت پدر رفرش شود
                if (totalCount == 0) {
                    if (this.parentCompany && this.parentCompany.Childrens) {
                        var thisIndex = this.parentCompany.Childrens.findIndex(p => p == this);
                        if (thisIndex >= 0)
                            this.parentCompany.Childrens.splice(thisIndex);
                        
                        this.parentCompany.reloadGrid();

                    }
                    
                }
                

                this.showloadingMessage = !(resData.length == 0);
                this.totalRecords = totalCount;
                this.sppcLoading.hide();
            }, (error => { console.log(error) }))
        }
        else {
            this.rowData = {
                data: [],
                total: 0
            }
        }
    }

    dataStateChange(state: DataStateChangeEvent): void {
        this.currentFilter = this.getFilters(state.filter);
        if (state.sort)
            if (state.sort.length > 0)
                this.currentOrder = state.sort[0].field + " " + state.sort[0].dir;
        this.state = state;
        this.skip = state.skip;
        this.reloadGrid();
    }

    public sortChange(sort: SortDescriptor[]): void {
        if (sort)
            this.currentOrder = sort[0].field + " " + sort[0].dir;
        this.reloadGrid();
    }

    pageChange(event: PageChangeEvent): void {
        this.skip = event.skip;
        this.reloadGrid();
    }

    deleteModel(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.companyService.delete(String.Format(CompanyApi.Company, this.deleteModelId)).subscribe(response => {
                this.deleteModelId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
                this.reloadGrid();
            }, (error => {
                this.sppcLoading.hide();
                this.showMessage(error, MessageType.Warning);
            }));
        }

        //hide confirm dialog
        this.deleteConfirm = false;
    }

    removeHandler(arg: any) {
        this.prepareDeleteConfirm(arg.dataItem.name);
        this.deleteModelId = arg.dataItem.id;
        this.deleteConfirm = true;
    }

    //detail account form events
    public editHandler(arg: any) {
        this.sppcLoading.show();
        this.companyService.getById(String.Format(CompanyApi.Company, arg.dataItem.id)).subscribe(res => {
            this.editDataItem = res;
            this.sppcLoading.hide();
        })
        this.isNew = false;
        this.errorMessage = '';
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.errorMessage = '';
    }

    public addNew(parentModelId?: number, addToThis?: boolean) {
        this.isNew = true;
        this.editDataItem = new CompanyInfo();

        if (parentModelId)
            this.parentId = parentModelId;
        else
            this.parentId = this.CompanyId;

        if (addToThis)
            this.addToContainer = addToThis;

        this.errorMessage = '';
    }

    public saveHandler(model: Company) {
        this.sppcLoading.show();
        if (!this.isNew) {
            this.isNew = false;
            this.companyService.edit<Company>(String.Format(CompanyApi.Company, model.id), model)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = model;
                    this.errorMessage = error;
                }));
        }
        else {
            //set parentid for childs branch
            if (this.parentId) {
                model.parentId = this.parentId;

                var findIndex = this.rowData.data.findIndex(acc => acc.id == this.parentId);
                var parentRow = this.rowData.data[findIndex];
                
                this.parentId = undefined;
            }
            else if (this.parent) {
                model.parentId = this.parent.id;                
            }

            //set parentid for childs branch

            this.companyService.insert<Company>(CompanyApi.Companies, model)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedModel = JSON.parse(response._body);

                    if (this.Childrens) {
                        var childFiltered = this.Childrens.filter(f => f.parent.id == model.parentId);
                        if (childFiltered.length > 0) {
                            childFiltered[0].reloadGrid(insertedModel);
                            return;
                        }
                    }
                    if (model.parentId == undefined || this.addToContainer) {
                        this.addToContainer = false;
                        this.reloadGrid(insertedModel);
                    }
                    else if (model.parentId != undefined) {
                        this.reloadGrid();
                    }
                    
                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));
        }
        this.sppcLoading.hide();
    }

    public showOnlyParent(dataItem: Company, index: number): boolean {
        return dataItem.childCount > 0;
    }

    public checkShow(dataItem: Company) {
        return dataItem != undefined && dataItem.childCount != undefined && dataItem.childCount > 0;
    }


    //#endregion
}


