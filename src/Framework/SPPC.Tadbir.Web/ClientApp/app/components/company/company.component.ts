import { Component, OnInit, Input, Renderer2 } from '@angular/core';
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

    @Input() public parent: Company;
    @Input() public isChild: boolean = false;

    public parentId?: number = undefined;

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    //permission flag
    viewAccess: boolean;
    insertAccess: boolean;
    editAccess: boolean;
    deleteAccess: boolean;

    ////for add in delete messageText
    deleteConfirm: boolean;
    deleteCompaniesConfirm: boolean;
    deleteCompanyId: number;

    currentFilter: Filter[] = [];
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;

    newCompany: boolean;
    company: Company = new CompanyInfo;


    editDataItem?: Company = undefined;
    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.Company, CompanyPermissions.View);
        this.insertAccess = this.isAccess(SecureEntity.Company, CompanyPermissions.Create);
        this.editAccess = this.isAccess(SecureEntity.Company, CompanyPermissions.Edit);
        this.deleteAccess = this.isAccess(SecureEntity.Company, CompanyPermissions.Delete);

        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private companyService: CompanyService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.Company, Metadatas.Company);
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    showConfirm() {
        this.deleteCompaniesConfirm = true;
    }

    deleteCompanies(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            //this.accountService.deleteAccounts(this.selectedRows).subscribe(res => {
            //    this.showMessage(this.deleteMsg, MessageType.Info);
            //    this.selectedRows = [];
            //    this.reloadGrid();
            //    this.groupDelete = false;
            //}, (error => {
            //    this.sppcLoading.hide();
            //    this.showMessage(error, MessageType.Warning);
            //}));
        }

        this.groupDelete = false;
        this.deleteCompaniesConfirm = false;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid(insertedCompany?: Company) {
        if (this.viewAccess) {
            this.sppcLoading.show();
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip) {
                this.skip = this.skip - this.pageSize;
            }
            if (this.totalRecords == this.skip) {
                this.skip = this.skip - this.pageSize;
            }
            var url = String.Format(CompanyApi.CompanyChildren, this.CompanyId);
            if (this.parent) {
                if (this.parent.childCount > 0)
                    url = String.Format(CompanyApi.CompanyChildren, this.parent.id);
            }
            this.companyService.getAll(url, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
                var resData = res.json();
                var totalCount = 0;
                if (insertedCompany) {
                    var rows = (resData as Array<Company>);
                    var index = rows.findIndex(p => p.id == insertedCompany.id);
                    if (index >= 0) {
                        resData.splice(index, 1);
                        rows.splice(0, 0, insertedCompany);
                    }
                    else {
                        if (rows.length == this.pageSize) {
                            resData.splice(this.pageSize - 1, 1);
                        }

                        rows.splice(0, 0, insertedCompany);
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

    deleteCompany(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.companyService.delete(String.Format(CompanyApi.Company, this.deleteCompanyId)).subscribe(response => {
                this.deleteCompanyId = 0;
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
        this.deleteCompanyId = arg.dataItem.id;
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

    public addNew(parentCompanyId?: number) {
        this.isNew = true;
        this.editDataItem = new CompanyInfo();
        if (parentCompanyId == undefined)
            this.parentId = this.CompanyId;
        else
            if (parentCompanyId)
                this.parentId = parentCompanyId;
        this.errorMessage = '';
    }

    public saveHandler(company: Company) {
        this.sppcLoading.show();
        if (!this.isNew) {
            this.isNew = false;
            this.companyService.edit<Company>(String.Format(CompanyApi.Company, company.id), company)
                .subscribe(response => {
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.editDataItem = company;
                    this.errorMessage = error;
                }));
        }
        else {
            //set parentid for childs accounts
            if (this.parentId) {
                company.parentId = this.parentId;
                this.parentId = undefined;
            }
            else if (this.parent)
                company.parentId = this.parent.id;
            //set parentid for childs accounts

            this.companyService.insert<Company>(CompanyApi.Companies, company)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedCompany = JSON.parse(response._body);
                    this.reloadGrid(insertedCompany);
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

}


