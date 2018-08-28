import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { FiscalPeriodService, FiscalPeriodInfo, RelatedItemsInfo } from '../../service/index';
import { FiscalPeriod, RelatedItems } from '../../model/index';
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
import { Filter } from "../../class/filter";

import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';

import { SecureEntity } from '../../security/secureEntity';
import { FiscalPeriodPermissions } from '../../security/permissions';
import { FiscalPeriodApi } from '../../service/api/index';
import { FilterExpression } from '../../class/filterExpression';



export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}

@Component({
    selector: 'fiscalPeriod',
    templateUrl: './fiscalPeriod.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})


export class FiscalPeriodComponent extends DefaultComponent implements OnInit {

    public rowData: GridDataResult;
    public selectedRows: string[] = [];
    public totalRecords: number;

    //permission flag
    viewAccess: boolean;

    //for add in delete messageText
    deleteConfirm: boolean;
    deleteModelId: number;

    currentFilter: FilterExpression;
    currentOrder: string = "";
    public sort: SortDescriptor[] = [];

    showloadingMessage: boolean = true;
    rolesList: boolean = false;

    editDataItem?: FiscalPeriod = undefined;
    fiscalPeriodRolesData: RelatedItemsInfo;

    isNew: boolean;
    errorMessage: string;
    groupDelete: boolean = false;

    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.FiscalPeriod, FiscalPeriodPermissions.View);
        this.reloadGrid();
    }

    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.FiscalPeriod, Metadatas.FiscalPeriod);
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    deleteModels() {
        //this.sppcLoading.show();
        //this.voucherService.groupDelete(VoucherApi.Vouchers, this.selectedRows).subscribe(res => {
        //    this.showMessage(this.deleteMsg, MessageType.Info);
        //    this.selectedRows = [];
        //    this.reloadGrid();
        //}, (error => {
        //    this.sppcLoading.hide();
        //    this.showMessage(error, MessageType.Warning);
        //}));
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    reloadGrid(insertedModel?: FiscalPeriod) {
        if (this.viewAccess) {
            this.sppcLoading.show();
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }


            if (insertedModel)
                this.goToLastPage();

            this.fiscalPeriodService.getAll(String.Format(FiscalPeriodApi.CompanyFiscalPeriods, this.CompanyId), this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
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
                this.showloadingMessage = !(resData.length == 0);
                this.totalRecords = totalCount;
                this.sppcLoading.hide();
            })
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

        if (this.totalRecords % this.pageSize == 0) {
            this.skip = (pageCount * this.pageSize) - this.pageSize;
            return;
        }
        this.skip = (pageCount * this.pageSize)
    }

    deleteModel(confirm: boolean) {
        if (confirm) {
            this.sppcLoading.show();
            this.fiscalPeriodService.delete(String.Format(FiscalPeriodApi.FiscalPeriod, this.deleteModelId)).subscribe(response => {
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

    public editHandler(arg: any) {
        this.sppcLoading.show();
        this.fiscalPeriodService.getById(String.Format(FiscalPeriodApi.FiscalPeriod, arg.dataItem.id)).subscribe(res => {
            this.editDataItem = res;
            this.sppcLoading.hide();
        })
        this.isNew = false;
        this.errorMessage = '';
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.isNew = false;
        this.errorMessage = '';
    }

    public addNew() {
        this.isNew = true;
        this.editDataItem = new FiscalPeriodInfo();
        this.errorMessage = '';
    }

    public rolesHandler(fPeriodId: number) {
        this.rolesList = true;
        this.sppcLoading.show();
        this.fiscalPeriodService.getFiscalPeriodRoles(fPeriodId).subscribe(res => {
            this.fiscalPeriodRolesData = res;
            this.sppcLoading.hide();
        });

        this.errorMessage = '';
    }

    public cancelFiscalPeriodRolesHandler() {
        this.rolesList = false;
        this.errorMessage = '';
    }

    public saveFiscalPeriodRolesHandler(fPeriodRoles: RelatedItems) {
        debugger;
        this.sppcLoading.show();
        this.fiscalPeriodService.modifiedFiscalPeriodRoles(fPeriodRoles)
            .subscribe(response => {
                this.rolesList = false;
                this.showMessage(this.getText("FiscalPeriod.UpdateRoles"), MessageType.Succes);
                this.sppcLoading.hide();
            }, (error => {
                this.sppcLoading.hide();
                this.errorMessage = error;
            }));
    }

    public saveHandler(model: FiscalPeriod) {
        model.companyId = this.CompanyId;    
        this.sppcLoading.show();
        if (!this.isNew) {
            this.fiscalPeriodService.edit<FiscalPeriod>(String.Format(FiscalPeriodApi.FiscalPeriod, model.id), model)
                .subscribe(response => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.errorMessage = error;

                }));
        }
        else {
            this.fiscalPeriodService.insert<FiscalPeriod>(FiscalPeriodApi.FiscalPeriods, model)
                .subscribe((response: any) => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.insertMsg, MessageType.Succes);
                    var insertedModel = response;
                    this.reloadGrid(insertedModel);
                }, (error => {
                    this.isNew = true;
                    this.errorMessage = error;
                }));
        }
        this.sppcLoading.hide();
    }

}


