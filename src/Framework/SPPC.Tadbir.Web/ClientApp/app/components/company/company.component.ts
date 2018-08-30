import { Component, OnInit, Input, Renderer2, Optional, Host, SkipSelf } from '@angular/core';
import { CompanyService, CompanyInfo, CompanyDbInfo } from '../../service/index';
import { Company, CompanyDb } from '../../model/index';
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

    editDataItem?: CompanyDb = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;
    addToContainer: boolean = false;

    //#endregion

    //#region Constructor

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private companyService: CompanyService, public renderer: Renderer2, public metadata: MetaDataService, ) {
        super(toastrService, translate, renderer, metadata, Entities.Company, Metadatas.Company);
    }

    //#endregion

    //#region Methods

    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.Company, CompanyPermissions.View);
        this.reloadGrid();
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
            //this.sppcLoading.show();
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
            //this.sppcLoading.show();
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }

            if (insertedModel)
                this.goToLastPage();

            //var url = String.Format(CompanyApi.CompanyChildren, this.CompanyId);
            //if (this.parent) {
            //    if (this.parent.childCount > 0)
            //        url = String.Format(CompanyApi.CompanyChildren, this.parent.id);
            //}
            var url = CompanyApi.Companies;
            this.companyService.getAll(url, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {

                var resData = res.body;

                var totalCount = 0;

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

                ////زمانی که تعداد رکورد ها صفر باشد باید کامپوننت پدر رفرش شود
                //if (totalCount == 0) {
                //    if (this.parentCompany && this.parentCompany.Childrens) {
                //        var thisIndex = this.parentCompany.Childrens.findIndex(p => p == this);
                //        if (thisIndex >= 0)
                //            this.parentCompany.Childrens.splice(thisIndex);

                //        this.parentCompany.reloadGrid();

                //    }

                //}

                this.showloadingMessage = !(resData.length == 0);
                this.totalRecords = totalCount;
                ////this.sppcLoading.hide();
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

    goToLastPage() {
        var pageCount: number = 0;
        pageCount = Math.floor(this.totalRecords / this.pageSize);

        if (this.totalRecords % this.pageSize == 0 && this.totalRecords != pageCount * this.pageSize) {
            this.skip = (pageCount * this.pageSize) - this.pageSize;
            return;
        }
        this.skip = (pageCount * this.pageSize)
    }

    deleteModel(confirm: boolean) {
        if (confirm) {
            //this.sppcLoading.show();
            this.companyService.delete(String.Format(CompanyApi.Company, this.deleteModelId)).subscribe(response => {
                this.deleteModelId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
                this.reloadGrid();
            }, (error => {
                ////this.sppcLoading.hide();
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

    //form events
    public editHandler(arg: any) {
        //this.sppcLoading.show();
        this.companyService.getById(String.Format(CompanyApi.Company, arg.dataItem.id)).subscribe(res => {
            this.editDataItem = res;
            ////this.sppcLoading.hide();
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
        this.editDataItem = new CompanyDbInfo();

        this.errorMessage = '';
    }

    public saveHandler(model: Company) {
        //this.sppcLoading.show();
        if (!this.isNew) {
            this.isNew = false;
            this.companyService.edit<CompanyDb>(String.Format(CompanyApi.Company, model.id), model)
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
            this.companyService.insert<CompanyDb>(CompanyApi.Companies, model)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedModel = response;
                    this.reloadGrid();
                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));
        }
        ////this.sppcLoading.hide();
    }

    public showOnlyParent(dataItem: Company, index: number): boolean {
        return dataItem.childCount > 0;
    }

    //#endregion
}


