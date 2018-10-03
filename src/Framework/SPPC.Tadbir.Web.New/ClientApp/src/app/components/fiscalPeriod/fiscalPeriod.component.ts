import { Component, OnInit, Input, Renderer2, ViewChild } from '@angular/core';
import { FiscalPeriodService, FiscalPeriodInfo, RelatedItemsInfo } from '../../service/index';
import { FiscalPeriod, RelatedItems } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { TranslateService } from 'ng2-translate';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
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

    //#region Fields
    @ViewChild(GridComponent) grid: GridComponent;

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
    //#endregion

    //#region Events
    ngOnInit() {
        this.viewAccess = this.isAccess(SecureEntity.FiscalPeriod, FiscalPeriodPermissions.View);
        this.reloadGrid();
    }

    selectionKey(context: RowArgs): string {
        if (context.dataItem == undefined) return "";
        return context.dataItem.id + " " + context.index;
    }

    onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
        if (this.selectedRows.length > 1)
            this.groupDelete = true;
        else
            this.groupDelete = false;
    }

    filterChange(filter: CompositeFilterDescriptor): void {
        var isReload: boolean = false;
        if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
            isReload = true;

        this.currentFilter = this.getFilters(filter);
        if (isReload) {
            this.reloadGrid();
        }
    }

    //dataStateChange(state: DataStateChangeEvent): void {
    //    this.currentFilter = this.getFilters(state.filter);
    //    if (state.sort)
    //        if (state.sort.length > 0)
    //            this.currentOrder = state.sort[0].field + " " + state.sort[0].dir;
    //    this.state = state;
    //    this.skip = state.skip;
    //    this.reloadGrid();
    //}

    public sortChange(sort: SortDescriptor[]): void {
        if (sort)
            this.currentOrder = sort[0].field + " " + sort[0].dir;

        this.reloadGrid();
    }

    removeHandler(arg: any) {
        this.prepareDeleteConfirm(arg.dataItem.name);
        this.deleteModelId = arg.dataItem.id;
        this.deleteConfirm = true;
    }

    pageChange(event: PageChangeEvent): void {
        this.skip = event.skip;
        this.reloadGrid();
    }

    public editHandler(arg: any) {
        this.grid.loading = true;
        this.fiscalPeriodService.getById(String.Format(FiscalPeriodApi.FiscalPeriod, arg.dataItem.id)).subscribe(res => {
            this.editDataItem = res;
            this.grid.loading = false;
        })
        this.isNew = false;
        this.errorMessage = '';
    }

    public cancelHandler() {
        this.editDataItem = undefined;
        this.isNew = false;
        this.errorMessage = '';
    }

    public saveFiscalPeriodRolesHandler(fPeriodRoles: RelatedItems) {
        debugger;
        this.grid.loading = true;
        this.fiscalPeriodService.modifiedFiscalPeriodRoles(fPeriodRoles)
            .subscribe(response => {
                this.rolesList = false;
                this.showMessage(this.getText("FiscalPeriod.UpdateRoles"), MessageType.Succes);
                this.grid.loading = false;
            }, (error => {
                this.grid.loading = false;
                this.errorMessage = error;
            }));
    }

    public saveHandler(model: FiscalPeriod) {
        model.companyId = this.CompanyId;
        this.grid.loading = true;
        if (!this.isNew) {
            this.fiscalPeriodService.edit<FiscalPeriod>(String.Format(FiscalPeriodApi.FiscalPeriod, model.id), model)
                .subscribe(response => {
                    this.isNew = false;
                    this.editDataItem = undefined;
                    this.showMessage(this.updateMsg, MessageType.Succes);
                    this.reloadGrid();
                }, (error => {
                    this.grid.loading = false;
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
                    this.grid.loading = false;
                }));
        }
    }

    public rolesHandler(fPeriodId: number) {
        this.rolesList = true;
        this.grid.loading = true;
        this.fiscalPeriodService.getFiscalPeriodRoles(fPeriodId).subscribe(res => {
            this.fiscalPeriodRolesData = res;
            this.grid.loading = false;
        });

        this.errorMessage = '';
    }

    public cancelFiscalPeriodRolesHandler() {
        this.rolesList = false;
        this.errorMessage = '';
    }

    //#endregion

    //#region Constructor
    constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
        private fiscalPeriodService: FiscalPeriodService, public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata, Entities.FiscalPeriod, Metadatas.FiscalPeriod);
    }
    //#endregion

    //#region Methods
    deleteModels() {
        ////this.sppcLoading.show();
        //this.voucherService.groupDelete(VoucherApi.Vouchers, this.selectedRows).subscribe(res => {
        //    this.showMessage(this.deleteMsg, MessageType.Info);
        //    this.selectedRows = [];
        //    this.reloadGrid();
        //}, (error => {
        //    ////this.sppcLoading.hide();
        //    this.showMessage(error, MessageType.Warning);
        //}));
    }

    reloadGridEvent() {
        this.reloadGrid();
    }


    reloadGrid(insertedModel?: FiscalPeriod) {
        if (this.viewAccess) {
            this.grid.loading = true;
            var filter = this.currentFilter;
            var order = this.currentOrder;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }


            if (insertedModel)
                this.goToLastPage(this.totalRecords);

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

                this.grid.loading = false;
            })
        }
        else {
            this.rowData = {
                data: [],
                total: 0
            }
        }
    }

    deleteModel(confirm: boolean) {
        if (confirm) {
            this.grid.loading = true;
            this.fiscalPeriodService.delete(String.Format(FiscalPeriodApi.FiscalPeriod, this.deleteModelId)).subscribe(response => {
                this.deleteModelId = 0;
                this.showMessage(this.deleteMsg, MessageType.Info);
                if (this.rowData.data.length == 1 && this.pageIndex > 1)
                    this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

                this.reloadGrid();
            }, (error => {
                this.grid.loading = false;
                var message = error.message ? error.message : error;
                this.showMessage(message, MessageType.Warning);
            }));
        }

        //hide confirm dialog
        this.deleteConfirm = false;
    }

    public addNew() {
        this.isNew = true;
        this.editDataItem = new FiscalPeriodInfo();
        this.errorMessage = '';
    }

    //#endregion

}
