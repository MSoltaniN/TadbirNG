import { Component, OnInit, Renderer2, NgZone, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';
import { FilterColumn, Item, NumberOperatorResource, StringOperatorResource, LoginOperatorResource, FilterRow, Guid, Braces } from '@sppc/shared/models';
import { Layout } from '@sppc/env/environment';
import { RTL } from '@progress/kendo-angular-l10n';
import { BrowserStorageService, GridService, MetaDataService } from '@sppc/shared/services';
import { DefaultComponent, Filter, FilterExpression, FilterExpressionBuilder } from '@sppc/shared/class';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { SettingService } from '@sppc/config/service';
import { forEach } from '@angular/router/src/utils/collection';
import { MessageType } from '@sppc/env/environment.prod';
import { RowArgs } from '@progress/kendo-angular-grid';
import { guid } from '@progress/kendo-angular-dateinputs/dist/es2015/util';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'app-advance-filter',
  templateUrl: './advance-filter.component.html',
  styleUrls: ['./advance-filter.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})
export class AdvanceFilterComponent extends DefaultComponent implements OnInit {  

  public title: string;
  public columnsList: Array<FilterColumn>;
  public operatorsList: Array<Item>;
  public selectedRows: any[] = [];  

  public colsIsDisabled: boolean;
  public opIsDisabled: boolean;
  public lgoIsDisabled: boolean;  

  public selectedLogicalOperator: string = "and";
  public selectedOperator: string = "eq";
  public selectedColumn: string;

  totalFilterExpression: string;
  selectedValue: string;
  formMode: string;
  currentEditIndex: number;

  

  @Output() result: EventEmitter<any> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  logicalOperatorList: Array<Item> = [
    { value: LoginOperatorResource.And, key: "and" },
    { value: LoginOperatorResource.Or, key: "or" }   
  ]

  numberOperators: Array<Item> = [
    { value: NumberOperatorResource.EQ, key: "eq" },
    { value: NumberOperatorResource.GT, key: "gt" },
    { value: NumberOperatorResource.GTE, key: "gte" },
    { value: NumberOperatorResource.LT, key: "lt" },
    { value: NumberOperatorResource.LTE, key: "lte" },
    { value: NumberOperatorResource.NEQ, key: "neq" },
  ]

  stringOperators: Array<Item> = [
    { value: StringOperatorResource.EQ, key: "eq" },
    { value: StringOperatorResource.StartWith, key: "startswith" },
    { value: StringOperatorResource.EndsWith, key: "endswith" },    
    { value: StringOperatorResource.NEQ, key: "neq" },
    { value: StringOperatorResource.Like, key: "contains" },
    { value: StringOperatorResource.NotLike, key: "doesnotcontain" },
  ]  

  public filters: Array<FilterRow>;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService,
    public renderer: Renderer2, public metadataService: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, bStorageService, renderer, metadataService, settingService, '', undefined);
  }

  ngOnInit(): void {
    //throw new Error("Method not implemented.");
    (async () => {
      var columns = await this.getAllMetaDataByViewIdAsync(this.viewId);
      columns.forEach((item) => {
        if (item.name == "RowNo")
          return;

        var fcol = new FilterColumn();
        fcol.dataType = item.dotNetType;
        fcol.scriptType = item.scriptType;
        fcol.name = item.name;
        var setting = JSON.parse(item.settings);
        var title = setting.large.title + (item.groupName != "" ? " (" + item.groupName + ")" : "");        
        fcol.title = title;

        if (setting.large.visibility != "AlwaysHidden") {
          if (!this.columnsList)
            this.columnsList = new Array<FilterColumn>();
          this.columnsList.push(fcol);
        }
      });
    })();

    this.computeTotalExpression();
    this.selectedColumnChange();
    this.formMode = "insert";

    
  }

  selectedColumnChange() {

    if (this.selectedColumn) {
      var selected = this.columnsList.filter(p => p.name === this.selectedColumn)[0];
      switch (selected.scriptType) {
        case "string":
          this.operatorsList = this.stringOperators;
          break;
        case "number":
          this.operatorsList = this.numberOperators;
          break;
        default:
          this.operatorsList = this.stringOperators;
          break;
      }

      //TODO:
      //decision for show releted input box for value ==> dropdown or checkbox or textbox or numberbox
    }

  }

  editFilter() {
    if (this.selectedRows && this.selectedRows.length == 1) {
      this.formMode = 'edit';
      var filter = this.selectedRows[0];
      this.selectedOperator = filter.operator;
      this.selectedColumn = filter.columnName;
      this.selectedLogicalOperator = filter.logicOperator;
      this.selectedValue = filter.value;
      this.currentEditIndex = this.filters.findIndex(f => f === this.selectedRows[0]);
      this.selectedColumnChange();
    }
  }

  revertToDefaultValues() {
    this.selectedValue = "";
    this.selectedOperator = "eq";
    this.selectedLogicalOperator = "or";
    this.selectedColumn = this.columnsList[0].name;
    this.formMode = 'insert';
  }

  saveFilter() {
    if (this.isValidate()) {


      this.filters[this.currentEditIndex].columnName = this.selectedColumn;
      this.filters[this.currentEditIndex].operator = this.selectedOperator;
      this.filters[this.currentEditIndex].value = this.selectedValue;
      this.filters[this.currentEditIndex].logicOperator = this.selectedLogicalOperator;

      var selectedCol = this.columnsList.filter(p => p.name === this.selectedColumn)[0];
      var selectedOp = this.operatorsList.filter(p => p.key === this.selectedOperator)[0];
      var selectedlogOp = this.logicalOperatorList.filter(p => p.key === this.selectedLogicalOperator)[0];

      this.filters[this.currentEditIndex].operatorTitle = this.getText(selectedOp.value);
      this.filters[this.currentEditIndex].columnTitle = selectedCol.title;
      this.filters[this.currentEditIndex].logicalOperatorTitle = this.getText(selectedlogOp.value);
      this.filters[this.currentEditIndex].filterTitle = selectedCol.title + " " + this.filters[this.currentEditIndex].operatorTitle
        + " " + this.selectedValue + " " + this.filters[this.currentEditIndex].logicalOperatorTitle;
            
      this.revertToDefaultValues()
      this.computeTotalExpression();
      this.showMessage(this.getText('AdvanceFilter.FilterEditedSuccess'), MessageType.Succes);
    }
  }

  selectionKey(context: RowArgs): any {
    return context.dataItem;
  }

  editIsDisable() {
    if (this.selectedRows && this.selectedRows.length == 1)
      return false;

    return true;
  }

  removeIsDisable() {
    if (this.selectedRows && this.selectedRows.length > 0)
      return false;

    return true;
  }

  cancelEditMode() {    
    this.revertToDefaultValues()
  }

  getStandardOperator(op: string,type:string):string {
    var operator = "";
    switch (op) {
      case "eq":
        operator = " == {0}";
        break;
      case "neq":
        operator = " != {0}";
        break;
      case "lte":
        operator = " <= {0}";
        break;
      case "gte":
        operator = " >= {0}";
        break;
      case "lt":
        operator = " < {0}";
        break;
      case "gt":
        operator = " > {0}";
        break;
      case "contains":
        operator = ".Contains({0})";
        break;
      case "doesnotcontain":
        operator = ".IndexOf({0}) == -1";
        break;
      case "startswith":
        operator = ".StartsWith({0})";
        break;
      case "endswith":
        operator = ".EndsWith({0})";
        break;
      default:
        operator = " == {0}";
    }

    //if (type.toLowerCase() == "system.string")
      //operator = operator.replace("{0}", "\"{0}\"");

    return operator;
  }



  createFilterExpression():FilterExpression {

    var totalfilter: FilterExpression;
    var lastOperatorUsed = " && ";
    this.filters.forEach((item) => {

      var column = this.columnsList.filter(col => col.name === item.columnName)[0];
      var currentfilter = new Filter(item.columnName, item.value, this.getStandardOperator(item.operator, column.dataType), column.dataType, item.braces, item.id);

      var newFilter = new FilterExpression();
      newFilter.filter = currentfilter;
      newFilter.operator = lastOperatorUsed ? lastOperatorUsed : (item.logicOperator == 'or' ? ' || ' : ' && ')

      if (totalfilter) {
        totalfilter.children.push(newFilter);
        lastOperatorUsed = item.logicOperator == 'or' ? ' || ' : ' && ';
      }
      else {
        //var fbuilder = new FilterExpressionBuilder();
        totalfilter = new FilterExpression();
        totalfilter.filter = currentfilter;
        totalfilter.operator = ' && ';
        lastOperatorUsed = item.logicOperator == 'or' ? ' || ' : ' && ';
        //return filterExpBuilder.New(filter).Build();
      }
    });

    return totalfilter;
  }

  onOk() {

    this.result.emit({ filters: this.createFilterExpression(),filterList: this.filters });
  }

  onCancel(): void {
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }



  braces() {
    if (this.selectedRows && this.selectedRows.length > 1) {

      var orderdSelected = this.selectedRows.sort((a, b) => parseFloat(a.order) - parseFloat(b.order));

      var startIndex = this.filters.findIndex(f => f === orderdSelected[0]);
      var endIndex = this.filters.findIndex(f => f === orderdSelected[orderdSelected.length - 1]);

      if (!this.filters[startIndex].braces)
        this.filters[startIndex].braces = new Array<Braces>();      

      if (!this.filters[endIndex].braces)
        this.filters[endIndex].braces = new Array<Braces>();

      

      if (this.filters[startIndex].braces.findIndex(b => b.outerId === this.filters[endIndex].id && b.brace === "(") >= 0
        && this.filters[endIndex].braces.findIndex(b => b.outerId === this.filters[startIndex].id && b.brace === ")") >= 0) {

        var dStartIndex = this.filters[startIndex].braces.findIndex(b => b.outerId === this.filters[endIndex].id && b.brace === "(");
        var dEndIndex = this.filters[endIndex].braces.findIndex(b => b.outerId === this.filters[startIndex].id && b.brace === ")");

        this.filters[startIndex].braces.splice(dStartIndex);
        this.filters[endIndex].braces.splice(dEndIndex);
        this.computeTotalExpression();

        return;
      }

      /*
      var index = 0;
      var showMsg = false;
      this.filters.forEach((fi) => {
        if (showMsg) return;
        if (fi.braces) {
          fi.braces.forEach((br) => {
            var anotherBraces = this.filters.findIndex(f => f.id === br.outerId);
            if (!((startIndex >= index && endIndex <= anotherBraces)
              || (startIndex < index && endIndex < index)
              || (startIndex > anotherBraces && endIndex > anotherBraces))) {
              showMsg = true;
              return;
            }

          });
        }

        index++;
      });

      if (showMsg) {
        this.showMessage('err');
        return;
      }*/

      var startBrace = new Braces();
      startBrace.brace = "(";
      startBrace.outerId = this.filters[endIndex].id;
    
      var endBrace = new Braces();
      endBrace.brace = ")";
      endBrace.outerId = this.filters[startIndex].id;

      this.filters[startIndex].braces.push(startBrace);
      this.filters[endIndex].braces.push(endBrace);

      this.computeTotalExpression();
    };    
  }

  insertNewFilter()
  {
    if (this.isValidate()) {

      var selectedCol = this.columnsList.filter(p => p.name === this.selectedColumn)[0];
      var selectedOp = this.operatorsList.filter(p => p.key === this.selectedOperator)[0];
      var selectedlogOp = this.logicalOperatorList.filter(p => p.key === this.selectedLogicalOperator)[0];

      var row: FilterRow = new FilterRow();
      row.id = Guid.newGuid();
      row.columnName = this.selectedColumn;
      row.logicOperator = this.selectedLogicalOperator;
      row.operator = this.selectedOperator;
      row.value = this.selectedValue;
      row.operatorTitle = this.getText(selectedOp.value);
      row.columnTitle = selectedCol.title;
      row.logicalOperatorTitle = this.getText(selectedlogOp.value);      
      row.filterTitle = selectedCol.title + " " + row.operatorTitle + " " + this.selectedValue + " " + row.logicalOperatorTitle
      row.order = this.filters ? this.filters.length : 0;

      if (!this.filters) this.filters = new Array<FilterRow>();
      this.filters.push(row);
      this.selectedValue = "";

      this.showMessage(this.getText('AdvanceFilter.FilterInsertedSuccess'), MessageType.Succes)
      this.computeTotalExpression();
    }
  }

 

  computeTotalExpression() {

    if (this.filters) {
      var i = 0;
      var count = this.filters.length;
      this.totalFilterExpression = "";

      this.filters.forEach((item) => {
        i++;

        if (item.braces) {
          item.braces.forEach((br) =>
          {
            if (br.brace == "(")
              this.totalFilterExpression += " " + br.brace;
          });
        }

        this.totalFilterExpression += " " + item.columnTitle + " " + item.operatorTitle + " " + item.value + " ";

        if (item.braces) {
          item.braces.forEach((br) => {
            if (br.brace == ")")
              this.totalFilterExpression += br.brace + " ";
          });
        }

        if (i < count)
          this.totalFilterExpression += item.logicalOperatorTitle + " ";
      });
    }
    
  }
  
  isValidate(): Boolean {
    if (!this.selectedColumn) {
      this.showMessage(this.getText('AdvanceFilter.PlaeseSelectField'), MessageType.Warning);
      return false;
    }


    if (!this.selectedOperator) {
      this.showMessage(this.getText('AdvanceFilter.PlaeseSelectOperator'), MessageType.Warning);
      return false;
    }

    if (!this.selectedValue) {
      this.showMessage(this.getText('AdvanceFilter.PlaeseEnterValue'), MessageType.Warning);
      return false;
    }

    if (!this.selectedLogicalOperator) {
      this.showMessage(this.getText('AdvanceFilter.PlaeseSelectLogicOperator'), MessageType.Warning);
      return false;
    }

    return true;
  }

  removeFilter() {
    if (this.selectedRows) {
      var deleted: Array<FilterRow> = [];

      this.selectedRows.forEach((item) => {
        //this.filters.splice(item,1);
        deleted.push(this.filters[item]);

      });

      var sortDeleted = this.selectedRows.sort((a, b) => parseFloat(a.order) - parseFloat(b.order));

      sortDeleted.forEach((del) => {
        var index = this.filters.findIndex(fi => fi === del);

        //if (this.filters[index].braces && this.filters[index + 1]) {
          var deleteBraces = this.filters[index].braces;
          deleteBraces.forEach((b) => {
            if (this.filters[index + 1]) {
              var nextFilter = this.filters[index + 1];
              if (!nextFilter.braces)
                nextFilter.braces = new Array<Braces>();
              nextFilter.braces.push(b);
            }
            else {
              if (this.filters[index].braces) {
                var deletedBrace = new Array<Braces>();

                this.filters[index].braces.forEach((br) => {
                  var outerBraces = this.filters.filter(f => f.id === br.outerId)[0].braces;

                  outerBraces.forEach((obr) => {
                    if (obr.outerId == this.filters[index].id) {
                      deleteBraces.push(obr);
                    }
                  })

                  deleteBraces.forEach((dbr) => {
                    var dindex = outerBraces.findIndex(br => br == dbr);
                    outerBraces.splice(dindex);
                  });

                })
              }
            }

          });          
        

        this.filters.splice(index, 1);
      });

      this.computeTotalExpression();
      this.selectedRows = [];

      this.revertToDefaultValues();

      this.showMessage(this.getText('AdvanceFilter.FilterDeletedSuccess'), MessageType.Succes)
    }

    
  }

}
