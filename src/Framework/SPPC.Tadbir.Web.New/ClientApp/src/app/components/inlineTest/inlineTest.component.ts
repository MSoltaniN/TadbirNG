//import { Component, OnInit, Input, Renderer2, ViewChild, SkipSelf, Host, Optional, OnDestroy } from '@angular/core';
//import { ProjectService, ProjectInfo, SettingService } from '../../service/index';
//import { Project } from '../../model/index';
//import { ToastrService } from 'ngx-toastr';
//import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
//import { Observable } from 'rxjs/Observable';
//import "rxjs/Rx";
//import { TranslateService } from '@ngx-translate/core';
//import { String } from '../../class/source';
//import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
//import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
//import { DefaultComponent } from "../../class/default.component";
//import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
//import { Filter } from "../../class/filter";
//import { RTL } from '@progress/kendo-angular-l10n';
//import { MetaDataService } from '../../service/metadata/metadata.service';
//import { Response } from '@angular/http';
//import { SppcLoadingService } from '../../controls/sppcLoading/index';
//import { ProjectApi } from '../../service/api/index';
//import { SecureEntity } from '../../security/secureEntity';
//import { ProjectPermissions } from '../../security/permissions';
//import { FilterExpression } from '../../class/filterExpression';
//import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
//import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
////import { InlineGridComponent } from '../../class/inlineGrid.component';


//export function getLayoutModule(layout: Layout) {
//  return layout.getLayout();
//}


//const matches = (el, selector) => (el.matches || el.msMatchesSelector).call(el, selector);



//@Component({
//  selector: 'inlineTest',
//  templateUrl: './inlineTest.component.html',
//  providers: [{
//    provide: RTL,
//    useFactory: getLayoutModule,
//    deps: [Layout]
//  }]
//})

//export class InlineTestComponent extends DefaultComponent implements OnInit, OnDestroy {

//  public rowData: GridDataResult;


//  @ViewChild(GridComponent)
//  private grid: GridComponent;

//  public selectedRows: string[] = [];
//  groupDelete: boolean = false;
//  errorMessage: string = '';
//  currentFilter: FilterExpression;
//  currentOrder: string = "";
//  totalRecords: number;

//  public formGroup: FormGroup;

//  private editedRowIndex: number;
//  private docClickSubscription: any;
//  private isNew: boolean;




//  constructor(public toastrService: ToastrService, public translate: TranslateService, private projectService: ProjectService, public renderer: Renderer2, public formBuilder: FormBuilder,
//    public metadata: MetaDataService, public settingService: SettingService, @SkipSelf() @Host() @Optional() private parentComponent: InlineTestComponent) {
//    super(toastrService, translate, renderer, formBuilder, metadata, settingService, Entities.Project, Metadatas.Project);
//  }

//  ngOnInit() {

//    this.formGroup = this.editForm;
//    this.closeEditor();

//    this.reloadGrid();

//    this.docClickSubscription = this.renderer.listen('document', 'click', this.onDocumentClick.bind(this));    
//  }

//  public ngOnDestroy(): void {
//    this.docClickSubscription();
//  }

//  public reloadGrid() {

//    var filter = this.currentFilter;
//    var order = this.currentOrder;
//    if (this.totalRecords == this.skip && this.totalRecords != 0) {
//      this.skip = this.skip - this.pageSize;
//    }

//    this.grid.loading = true;
//    this.projectService.getAll(ProjectApi.EnvironmentProjects, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
//      var resData = res.body;
//      var totalCount = 0;
//      if (res.headers != null) {
//        var headers = res.headers != undefined ? res.headers : null;
//        if (headers != null) {
//          var retheader = headers.get('X-Total-Count');
//          if (retheader != null)
//            this.totalRecords = totalCount = parseInt(retheader.toString());
//        }
//      }
//      this.rowData = {
//        data: resData,
//        total: totalCount
//      }
//      this.grid.loading = false;
//    });
//  }

//  public onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
//    if (this.selectedRows.length > 1)
//      this.groupDelete = true;
//    else
//      this.groupDelete = false;
//  }

//  pageChange(event: PageChangeEvent): void {
//    this.skip = event.skip;
//    this.reloadGrid();
//  }

//  filterChange(filter: CompositeFilterDescriptor): void {
//    var isReload: boolean = false;
//    if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
//      isReload = true;

//    this.currentFilter = this.getFilters(filter);
//    if (isReload) {
//      this.reloadGrid();
//    }
//  }

//  /////////////////////////////////
//  //public createFormGroup(dataItem: any): FormGroup {
//  //  return this.formBuilder.group({
//  //    'name': [dataItem.code, Validators.required],
//  //    'code': [dataItem.code, Validators.required],
//  //    'fullCode': [dataItem.fullCode, Validators.required],
//  //    'description': dataItem.description
//  //  });
//  //}

//  public cellClickHandler({ isEdited, dataItem, rowIndex }): void {

//    if (isEdited || (this.formGroup && !this.formGroup.valid)) {
//      return;
//    }

//    this.saveCurrent();

//    //this.formGroup = this.createFormGroup(dataItem);
//    this.formGroup = this.editForm;
//    this.formGroup.reset(dataItem);

//    this.editedRowIndex = rowIndex;

//    this.grid.editRow(rowIndex, this.formGroup);
//  }

//  public addHandler(): void {

//    this.errorMessage = '';
//    this.closeEditor();

//    //this.formGroup = this.createFormGroup({
//    //  'name': '',
//    //  'code': '',
//    //  'fullCode': '',
//    //  'description': ''
//    //});
//    this.formGroup = this.editForm;
//    this.formGroup.reset();
//    this.isNew = true;

//    this.grid.addRow(this.formGroup);
//  }

//  public cancelHandler(): void {
//    this.closeEditor();
//  }

//  private closeEditor(): void {

//    this.grid.closeRow(this.editedRowIndex);
//    this.isNew = false;
//    this.editedRowIndex = undefined;
//    this.formGroup = undefined;

//  }

//  private onDocumentClick(e: any): void {

//    if (this.formGroup && this.formGroup.valid &&
//      !matches(e.target, '#inlineGride tbody *, #inlineGride .k-grid-toolbar .k-button')) {
//      this.saveCurrent();
//    }
//  }

//  private saveCurrent(): void {
//    if (this.formGroup) {
//      if (this.isNew) {
//        this.addNew();
//      }

//      this.closeEditor();
//    }
//  }

//  addNew() {
//    var model: Project = new ProjectInfo();

//    model.branchId = this.BranchId;
//    model.fiscalPeriodId = this.FiscalPeriodId;
//    model.companyId = this.CompanyId;
//    model.name = this.formGroup.value.name;
//    model.code = this.formGroup.value.code;
//    model.fullCode = this.formGroup.value.fullCode;
//    model.description = this.formGroup.value.description;
//    model.branchScope = 1;
//    model.level = 0;

//    this.projectService.insert<Project>(ProjectApi.EnvironmentProjects, model).subscribe((response: any) => {
//      this.isNew = false;
//      this.showMessage(this.insertMsg, MessageType.Succes);

//      this.reloadGrid();
//    }, (error => {
//      this.isNew = true;
//      this.errorMessage = error;
//    }));
//  }



//}
