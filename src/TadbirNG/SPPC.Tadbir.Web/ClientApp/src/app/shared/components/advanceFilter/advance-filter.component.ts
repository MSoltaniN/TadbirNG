import * as moment from 'jalali-moment';
import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  NgZone,
  OnInit,
  Output,
  Renderer2,
  ViewEncapsulation,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { RowArgs } from "@progress/kendo-angular-grid";
import { RTL } from "@progress/kendo-angular-l10n";
import { SettingService } from "@sppc/config/service/settings.service";
import { DefaultComponent } from "@sppc/shared/class/default.component";
import { Filter } from "@sppc/shared/class/filter";
import { FilterExpression } from "@sppc/shared/class/filterExpression";
import { Layout, MessageType } from "@sppc/shared/enum/metadata";
import {
  BooleanOperatorResource,
  Braces,
  FilterColumn,
  FilterRow,
  FilterViewModel,
  GroupFilter,
  Guid,
  Item,
  LoginOperatorResource,
  NumberOperatorResource,
  StringOperatorResource,
} from "@sppc/shared/models";
import { AdvanceFilterService } from "@sppc/shared/services/advance-filter.service";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";
import { GridService } from "@sppc/shared/services/grid.service";
import { MetaDataService } from "@sppc/shared/services/metadata.service";
import { ToastrService } from "ngx-toastr";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "app-advance-filter",
  templateUrl: "./advance-filter.component.html",
  styleUrls: ["./advance-filter.component.css"],
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
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
  public selectedOperator: Item;
  public selectedColumn: FilterColumn;
  selectScriptType: string = "";

  totalFilterExpression: string = "";
  selectedValue: string|any;
  formMode: string;
  currentEditIndex: number;

  currentGFilter: GroupFilter;
  gFilterSelected: number;
  activeGroupFilter: boolean = false;
  activeSaveFilter: boolean = false;
  activeCopyFilter: boolean = false;
  filterUseForOthers: boolean = false;
  isEditMode: boolean = false;
  filterGroupName: string;

  public unSaveFilter: FilterRow[];
  public groupFilters: Array<GroupFilter>;
  public deletedGroupFilters: Array<GroupFilter>;

  public filters: Array<FilterRow>;
  usedColors = new Array<any>();
  maxColorCount: number = 10;

  selectedGroupRows: any[] = [];

  @Output() result: EventEmitter<any> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  logicalOperatorList: Array<Item> = [
    { value: LoginOperatorResource.And, key: "and" },
    { value: LoginOperatorResource.Or, key: "or" },
  ];

  numberDateOperators: Array<Item> = [
    { value: NumberOperatorResource.EQ, key: "eq" },
    { value: NumberOperatorResource.GT, key: "gt" },
    { value: NumberOperatorResource.GTE, key: "gte" },
    { value: NumberOperatorResource.LT, key: "lt" },
    { value: NumberOperatorResource.LTE, key: "lte" },
    { value: NumberOperatorResource.NEQ, key: "neq" },
  ];

  stringOperators: Array<Item> = [
    { value: StringOperatorResource.EQ, key: "eq" },
    { value: StringOperatorResource.StartWith, key: "startswith" },
    { value: StringOperatorResource.EndsWith, key: "endswith" },
    { value: StringOperatorResource.NEQ, key: "neq" },
    { value: StringOperatorResource.Like, key: "contains" },
    { value: StringOperatorResource.NotLike, key: "doesnotcontain" },
  ];

  booleanOperators: Array<Item> = [
    { value: BooleanOperatorResource.EQ, key: "eq" },
    { value: BooleanOperatorResource.NEQ, key: "neq" },
  ]

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public renderer: Renderer2,
    public metadataService: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public ngZone: NgZone,
    public advanceFilterService: AdvanceFilterService,
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadataService,
      settingService,
      "",
      undefined
    );
  }

  ngOnInit(): void {
    //throw new Error("Method not implemented.");
    (async () => {
      var columns = await this.getAllMetaDataByViewIdAsync(this.viewId);
      columns.forEach((item) => {
        if (item.name == "RowNo") return;

        var fcol = new FilterColumn();
        fcol.dataType = item.dotNetType;
        fcol.scriptType = item.scriptType;
        fcol.name = item.name;
        var setting = JSON.parse(item.settings);
        var title =
          setting.large.title +
          (item.groupName != "" ? " (" + item.groupName + ")" : "");
        fcol.title = title;

        if (setting.large.visibility != "AlwaysHidden") {
          if (!this.columnsList) this.columnsList = new Array<FilterColumn>();
          this.columnsList.push(fcol);
        }
      });
    })();

    this.computeTotalExpression();
    this.selectedColumnChange();
    this.formMode = "insert";

    this.firstLoadFilters(undefined);
  }

  firstLoadFilters(groupFilterSelected) {
    //insert first and unsaved items
    this.groupFilters = new Array<GroupFilter>();
    var firstItem = new GroupFilter();
    firstItem.id = -1;
    firstItem.name = "---";
    var init = false;
    if (this.bStorageService.getSession("unSaveFilter" + this.viewId)) {
      var unsavedFilters = <Array<GroupFilter>>(
        JSON.parse(
          this.bStorageService.getSession("unSaveFilter" + this.viewId)
        )
      );

      this.groupFilters = unsavedFilters;
      if (unsavedFilters.length > 1) init = true;
    } else {
      this.groupFilters.push(firstItem);
    }

    //insert first item

    this.advanceFilterService
      .getFilters(this.viewId)
      .subscribe((res: FilterViewModel[]) => {
        if (!init) {
          //insert db item
          res.forEach((fi) => {
            var item = new GroupFilter();
            item.id = fi.id;
            item.name = fi.name;
            item.isPublic = fi.isPublic;
            item.filters = <FilterRow[]>JSON.parse(fi.values);
            this.groupFilters.push(item);
          });
        }

        //insert db item
        if (!groupFilterSelected) {
          if (this.gFilterSelected == undefined) {
            this.gFilterSelected = -1;
            this.currentGFilter = this.groupFilters[0];
            this.filters = firstItem.filters;
          } else {
            this.currentGFilter = this.groupFilters.filter(
              (p) => p.id === this.gFilterSelected
            )[0];
          }
        } else {
          this.currentGFilter = groupFilterSelected;
          var item = this.groupFilters.filter(
            (p) => p.id === groupFilterSelected.id
          )[0];
          this.filters = item.filters;
        }

        let index = this.groupFilters.findIndex(
          (gf) => gf.id === this.gFilterSelected
        );
        this.filters = this.groupFilters[index].filters;

        this.computeTotalExpression();
      });
  }

  selectedColumnChange() {
    if (this.selectedColumn) {
      switch (this.selectedColumn.scriptType) {
        case "string":
          this.operatorsList = this.stringOperators;
          break;
        case "number":
          this.operatorsList = this.numberDateOperators;
          break;
        case "boolean":
          this.operatorsList = this.booleanOperators;
          this.selectedValue = false;
          break;
        case "Date":
          this.operatorsList = this.numberDateOperators;
          break;
        default:
          this.operatorsList = this.stringOperators;
          break;
      }

      this.selectScriptType = this.selectedColumn.scriptType;
      //TODO:
      //decision for show releted input box for value ==> dropdown or checkbox or textbox or numberbox
    }
  }

  editFilter() {
    if (this.selectedRows && this.selectedRows.length == 1) {
      this.formMode = "edit";
      var filter: FilterRow = this.selectedRows[0];
      var selectedCol = this.columnsList.filter(
        (p) => p.name === filter.columnName
      )[0];
      this.selectedColumn = selectedCol;
      this.selectedColumnChange();

      var selectedOp = this.operatorsList.filter(
        (p) => p.key === filter.operator
      )[0];
      this.selectedOperator = selectedOp;

      this.selectedLogicalOperator = filter.logicOperator;

      this.currentEditIndex = this.filters.findIndex(
        (f) => f === this.selectedRows[0]
      );
      this.selectedColumnChange();

      this.selectedValue = filter.value;
    }
  }

  revertToDefaultValues() {
    setTimeout(() => {
      this.selectedValue = "";
      this.selectedOperator = undefined;
      this.selectedLogicalOperator = "and";
      this.selectedColumn = undefined;
      this.selectScriptType = "";
      this.formMode = "insert";
    }, 1);
  }

  saveFilter() {
    if (this.isValidate()) {
      var index = this.groupFilters.findIndex(
        (gf) => gf.id === this.gFilterSelected
      );
      this.filters = this.groupFilters[index].filters;

      this.filters[this.currentEditIndex].columnName = this.selectedColumn.name;
      this.filters[this.currentEditIndex].operator = this.selectedOperator.key;
      this.filters[this.currentEditIndex].value = this.selectedValue;
      this.filters[this.currentEditIndex].logicOperator = this.selectedLogicalOperator;

      var selectedCol = this.columnsList.filter(
        (p) => p.name === this.selectedColumn.name
      )[0];
      var selectedOp = this.operatorsList.filter(
        (p) => p.key === this.selectedOperator.key
      )[0];
      var selectedlogOp = this.logicalOperatorList.filter(
        (p) => p.key === this.selectedLogicalOperator
      )[0];
      
      let displayValue = this.valueToView();

      this.filters[this.currentEditIndex].operatorTitle = this.getText(
        selectedOp.value
      );
      this.filters[this.currentEditIndex].columnTitle = selectedCol.title;
      this.filters[this.currentEditIndex].logicalOperatorTitle = this.getText(
        selectedlogOp.value
      );
      this.filters[this.currentEditIndex].filterTitle =
        selectedCol.title +
        " " +
        this.filters[this.currentEditIndex].operatorTitle +
        " " +
        displayValue +
        " " +
        this.filters[this.currentEditIndex].logicalOperatorTitle;

      this.revertToDefaultValues();
      this.computeTotalExpression();

      this.groupFilters[index].filters = this.filters;
      //this.saveFiltersToDB(false);
      this.showMessage(
        this.getText("AdvanceFilter.FilterEditedSuccess"),
        MessageType.Succes
      );
    }
  }

  selectionKey(context: RowArgs): any {
    return context.dataItem;
  }

  editIsDisable() {
    if (this.selectedRows && this.selectedRows.length == 1) return false;

    return true;
  }

  removeIsDisable() {
    if (this.selectedRows && this.selectedRows.length > 0) return false;

    return true;
  }

  cancelEditMode() {
    this.revertToDefaultValues();
  }

  getStandardOperator(op: string, type: string): string {
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

  createFilterExpression(): FilterExpression {
    var totalfilter: FilterExpression;
    var lastOperatorUsed = " && ";
    if (this.filters) {
      this.filters.forEach((item) => {
        var column = this.columnsList.filter(
          (col) => col.name === item.columnName
        )[0];
        var currentfilter = new Filter(
          item.columnName,
          item.value,
          this.getStandardOperator(item.operator, column.dataType),
          column.dataType,
          item.braces,
          item.id
        );

        var newFilter = new FilterExpression();
        newFilter.filter = currentfilter;
        newFilter.operator = lastOperatorUsed
          ? lastOperatorUsed
          : item.logicOperator == "or"
          ? " || "
          : " && ";

        if (totalfilter) {
          totalfilter.children.push(newFilter);
          lastOperatorUsed = item.logicOperator == "or" ? " || " : " && ";
        } else {
          //var fbuilder = new FilterExpressionBuilder();
          totalfilter = new FilterExpression();
          totalfilter.filter = currentfilter;
          totalfilter.operator = " && ";
          lastOperatorUsed = item.logicOperator == "or" ? " || " : " && ";
          //return filterExpBuilder.New(filter).Build();
        }
      });
    } else {
      return new FilterExpression();
    }

    return totalfilter;
  }

  copyGroupFilter() {
    this.filterGroupName = "";
    this.activeCopyFilter = true;
  }

  saveAllFilter() {
    this.groupFilters.forEach((gf) => {
      if (gf.filters && gf.filters.length > 0) {
        if (gf.id == -1) {
          this.filterGroupName = "";
          this.activeSaveFilter = true;
          return;
        }
      }
    });

    if (!this.activeSaveFilter) {
      this.saveFiltersToDB();
    }
  }

  copyFiltersToDB(groupFilter) {
    var gf = groupFilter;
    var filterModel = new FilterViewModel();
    filterModel.id = 0;
    filterModel.isPublic = gf.isPublic;
    filterModel.name = gf.name;
    filterModel.viewId = this.viewId;
    filterModel.userId = this.UserId;
    filterModel.values = JSON.stringify(this.currentGFilter.filters);
    gf.filters = this.currentGFilter.filters;
    if (filterModel.id == 0) {
      this.advanceFilterService
        .insertFilter(filterModel)
        .subscribe((res: any) => {
          this.gFilterSelected = res.id;
          var fil = this.groupFilters.filter((f) => f.id === gf.id);
          if (fil.length > 0) {
            fil[0].id = res.id;
          } else this.groupFilters.push(res);

          gf.id = res.id;
          this.currentGFilter = gf;

          this.firstLoadFilters(gf);

          //this.gFilterSelectChange(res);
          this.showMessage(
            this.getText("AdvanceFilter.FilterCopiedSuccess"),
            MessageType.Succes
          );
        });
    }
  }

  saveFiltersToDB(showMessage: boolean = true) {
    var unsavedFilters = <Array<GroupFilter>>(
      JSON.parse(this.bStorageService.getSession("unSaveFilter" + this.viewId))
    );

    this.groupFilters.forEach((gf) => {
      //if (gf.filters) {
      if (gf.id > -1) {
        var filterModel = new FilterViewModel();
        filterModel.id = gf.isNew ? 0 : gf.id;
        filterModel.isPublic = gf.isPublic;
        filterModel.name = gf.name;
        filterModel.viewId = this.viewId;
        filterModel.userId = this.UserId;
        if (!gf.filters) gf.filters = new Array<FilterRow>();
        filterModel.values = JSON.stringify(gf.filters);

        if (filterModel.id == 0)
          this.advanceFilterService
            .insertFilter(filterModel)
            .subscribe((res: any) => {
              this.gFilterSelected = res.id;

              var fil = this.groupFilters.filter((f) => f.id === gf.id);
              if (fil.length > 0) {
                fil[0].id = res.id;
                fil[0].isNew = false;
              } else this.groupFilters.push(res);

            });
        else
          this.advanceFilterService
            .saveFilter(filterModel.id, filterModel)
            .subscribe();
      }

    });

    this.bStorageService.removeSessionStorage("unSaveFilter" + this.viewId);

    if (showMessage)
      this.showMessage(
        this.getText("AdvanceFilter.FilterSavedSuccess"),
        MessageType.Succes
      );
  }

  saveFil(gf) {
    var filterModel = new FilterViewModel();
    filterModel.id = 0;
    filterModel.isPublic = gf.isPublic;
    filterModel.name = gf.name;
    filterModel.viewId = this.viewId;
    filterModel.userId = this.UserId;
    filterModel.values = JSON.stringify(gf.filters);
    if (filterModel.id == 0)
      this.advanceFilterService
        .insertFilter(filterModel)
        .subscribe((res: any) => {
          this.gFilterSelected = res.id;

          var fil = this.groupFilters.filter((f) => f.id === gf.id);
          if (fil.length > 0) fil[0].id = res.id;

          gf.id = res.id;
          this.firstLoadFilters(gf);
        });
  }

  onOk() {
    var unSavedGroupFilters = new Array<GroupFilter>();
    this.groupFilters.forEach((gf) => {
      if (gf.id == -1 || gf.isNew) {
        unSavedGroupFilters.push(gf);
      }
    });

    this.bStorageService.setSession(
      "unSaveFilter" + this.viewId,
      JSON.stringify(this.groupFilters)
    );

    this.result.emit({
      filters: this.createFilterExpression(),
      filterList: this.filters,
      groupFilter: this.groupFilters,
      gFilterSelected: this.gFilterSelected,
    });
  }

  onCancel(): void {
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }

  removeAllBraces() {
    var filters = new Array<FilterRow>();
    filters = <Array<FilterRow>>JSON.parse(JSON.stringify(this.filters));

    filters.forEach((f) => {
      f.braces = [];
    });

    var index = this.groupFilters.findIndex(
      (gf) => gf.id === this.gFilterSelected
    );
    this.groupFilters[index].filters = filters;
    this.filters = filters;
    this.selectedRows = [];
    this.computeTotalExpression();
  }

  braces() {
    var filters = new Array<FilterRow>();
    filters = <Array<FilterRow>>JSON.parse(JSON.stringify(this.filters));

    if (this.selectedRows && this.selectedRows.length > 1) {
      var orderdSelected = this.selectedRows.sort(
        (a, b) => parseFloat(a.order) - parseFloat(b.order)
      );

      var startIndex = filters.findIndex((f) => f.id === orderdSelected[0].id);
      var endIndex = filters.findIndex(
        (f) => f.id === orderdSelected[orderdSelected.length - 1].id
      );

      if (!filters[startIndex].braces)
        filters[startIndex].braces = new Array<Braces>();

      if (!filters[endIndex].braces)
        filters[endIndex].braces = new Array<Braces>();

      if (
        filters[startIndex].braces.findIndex(
          (b) => b.outerId === filters[endIndex].id && b.brace === "("
        ) >= 0 &&
        filters[endIndex].braces.findIndex(
          (b) => b.outerId === filters[startIndex].id && b.brace === ")"
        ) >= 0
      ) {
        //remove braces to temp array and check expression
        var dStartIndex = filters[startIndex].braces.findIndex(
          (b) => b.outerId === filters[endIndex].id && b.brace === "("
        );
        var dEndIndex = filters[endIndex].braces.findIndex(
          (b) => b.outerId === filters[startIndex].id && b.brace === ")"
        );

        filters[startIndex].braces.splice(dStartIndex);
        filters[endIndex].braces.splice(dEndIndex);

        if (!this.checkExpression(filters)) {
          this.showMessage(this.getText("AdvanceFilter.RemoveBracesMsg"));
          return;
        }

        //remove braces from real array
        this.filters[startIndex].braces.splice(dStartIndex);
        this.filters[endIndex].braces.splice(dEndIndex);
      } else {
        //add braces to temp array and check expression
        var startBrace = new Braces();
        startBrace.brace = "(";
        startBrace.outerId = filters[endIndex].id;

        var endBrace = new Braces();
        endBrace.brace = ")";
        endBrace.outerId = filters[startIndex].id;

        //filters[startIndex].braces.push(startBrace);
        //filters[endIndex].braces.push(endBrace);
        var bestStartIndex = this.findBestBraceStartIndex(
          filters,
          startIndex,
          endIndex
        );
        var bestEndIndex = this.findBestBraceStartIndex(
          filters,
          startIndex,
          endIndex
        );

        filters[startIndex].braces.splice(bestStartIndex, 0, startBrace);
        filters[endIndex].braces.splice(bestEndIndex, 0, endBrace);

        if (!this.checkExpression(filters)) {
          this.showMessage(this.getText("AdvanceFilter.AddBracesMsg"));
          return;
        }

        //add braces to real array
        if (!this.filters[startIndex].braces)
          this.filters[startIndex].braces = new Array<Braces>();
        if (!this.filters[endIndex].braces)
          this.filters[endIndex].braces = new Array<Braces>();

        //this.filters[startIndex].braces.push(startBrace);
        //this.filters[endIndex].braces.push(endBrace);

        this.filters[startIndex].braces.splice(bestStartIndex, 0, startBrace);
        this.filters[endIndex].braces.splice(bestEndIndex, 0, endBrace);
      }

      var index = this.groupFilters.findIndex(
        (gf) => gf.id === this.gFilterSelected
      );
      this.groupFilters[index].filters = this.filters;

      this.computeTotalExpression();
    }
  }

  findBestBraceStartIndex(filters: FilterRow[], startIndex, endIndex) {
    var counter = 0;
    filters[startIndex].braces.forEach((it) => {
      if (filters.findIndex((f) => f.id === it.outerId) > endIndex)
        return counter;

      counter++;
    });

    return counter;
  }

  findBestBraceEndIndex(filters: FilterRow[], startIndex, endIndex) {
    var counter = 0;
    filters[endIndex].braces.forEach((it) => {
      if (filters.findIndex((f) => f.id === it.outerId) > startIndex)
        return counter;

      counter++;
    });

    return counter;
  }

  checkExpression(filters: Array<FilterRow>): boolean {
    let result = false;
    try {
      result = eval(this.computeExpressionWithBraces(filters)) === eval("true");
    } catch (e) {
      result = false;
    }

    return result;
  }

  insertNewFilter() {
    if (this.isValidate()) {
      var selectedCol = this.columnsList.filter(
        (p) => p.name === this.selectedColumn.name
      )[0];
      var selectedOp = this.operatorsList.filter(
        (p) => p.key === this.selectedOperator.key
      )[0];
      var selectedlogOp = this.logicalOperatorList.filter(
        (p) => p.key === this.selectedLogicalOperator
      )[0];
      
      var index = -1;
      if (this.groupFilters) {
        index = this.groupFilters.findIndex(
          (gf) => gf.id === this.gFilterSelected
        );
        this.filters = this.groupFilters[index].filters;
      } else this.filters = new Array<FilterRow>();

      let value = this.valueToView();

      var row: FilterRow = new FilterRow();
      row.id = Guid.newGuid();
      row.columnName = this.selectedColumn.name;
      row.logicOperator = this.selectedLogicalOperator;
      row.operator = this.selectedOperator.key;
      row.value = this.selectedValue
      row.operatorTitle = this.getText(selectedOp.value);
      row.columnTitle = selectedCol.title;
      row.logicalOperatorTitle = this.getText(selectedlogOp.value);
      row.filterTitle =
        selectedCol.title +
        " " +
        row.operatorTitle +
        " " +
        value +
        " " +
        row.logicalOperatorTitle;
      row.order = this.filters ? this.filters.length : 0;

      if (!this.filters) this.filters = new Array<FilterRow>();

      this.filters.push(row);
      this.selectedValue = "";

      this.groupFilters[index].filters = this.filters;

      this.showMessage(
        this.getText("AdvanceFilter.FilterInsertedSuccess"),
        MessageType.Succes
      );
      this.computeTotalExpression();
      this.revertToDefaultValues();
      //this.saveFiltersToDB(false);
    }
  }

  toJalaliDate(value:any) {
    let format: string = "YYYY/MM/DD";
    moment.locale('en');
    let MomentDate = moment(value).locale('fa').format(format);    
    return MomentDate;
  }

  valueToView() {
    let value;
    switch (this.selectScriptType) {
      case 'Date':
        value = this.CurrentLanguage == 'fa'? this.toJalaliDate(this.selectedValue): this.selectedValue;
        break;

      case 'boolean':
        value = this.selectedValue == true? this.getText("Form.Active"): this.getText("Form.Inactive");
        break;
    
      default:
        value = this.selectedValue;
        break;
    }
    return value;
  }

  computeExpressionWithBraces(filters: Array<FilterRow>) {
    var expression = "";
    if (filters) {
      var i = 0;
      var count = filters.length;

      filters.forEach((item) => {
        i++;

        if (item.braces) {
          item.braces.forEach((br) => {
            if (br.brace == "(") expression += " " + br.brace;
          });
        }

        expression += " true ";

        if (item.braces) {
          item.braces.forEach((br) => {
            if (br.brace == ")") expression += br.brace + " ";
          });
        }

        if (i < count)
          expression += item.logicOperator == "or" ? " || " : " && ";
      });
    } else {
      expression = "true";
    }

    return expression;
  }

  getColor(filtersId) {
    var i = 0;
    if (this.usedColors && this.usedColors.length > 0) {
      if (this.usedColors.findIndex((p) => p.id === filtersId) > -1) {
        return this.usedColors.findIndex((p) => p.id === filtersId);
      }
    }

    for (i = 0; i <= this.maxColorCount; i++) {
      if (this.usedColors[i] == undefined) {
        this.usedColors[i] = { id: filtersId };
        return i;
      }
    }
  }

  computeTotalExpression() {
    this.totalFilterExpression = "";
    if (!this.groupFilters) return;
    var usedId = new Array<string>();

    var index = this.groupFilters.findIndex(
      (gf) => gf.id === this.gFilterSelected
    );
    if (index == -1) return;
    var filters = this.groupFilters[index].filters;

    if (filters) {
      var counter = 0;
      var count = filters.length;

      filters.forEach((item) => {
        counter++;

        if (item.braces) {
          for (var i = item.braces.length; i >= 0; i--) {
            var br = item.braces[i];
            if (br && br.brace == "(") {
              var html = "";
              if (usedId.findIndex((f) => f === br.outerId + item.id) == -1) {
                var color = this.getColor(br.outerId + item.id);
                html = '<span class="color' + color + '">';
                usedId.push(br.outerId + item.id);
                //colorCount++;
              } else {
                //var colorindex = usedId.findIndex(f => f === br.outerId + item.id)
                //html = '<span class="color' + colorindex + '">';
                html =
                  '<span class="color' +
                  this.getColor(br.outerId + item.id) +
                  '">';
              }

              this.totalFilterExpression += " " + html + br.brace + "</span>";
            }
          }
        }
        
        let value = item.columnName.includes('Date') &&
         this.CurrentLanguage == 'fa' &&
         +item.value.split('/')[0] > 1600?
            this.toJalaliDate(item.value) : item.value;

        this.totalFilterExpression +=
          " " +
          "<span class='column-name'>" +
          item.columnTitle +
          "</span>" +
          " " +
          " " +
          "<span class='operator'>" +
          item.operatorTitle +
          "</span>" +
          " " +
          "<span class='value'>" +
          value +
          "</span>" +
          " ";

        if (item.braces) {
          //for (var i = item.braces.length; i >= 0; i--) {
          for (var i = 0; i < item.braces.length; i++) {
            var br = item.braces[i];
            if (br && br.brace == ")") {
              var html = "";
              if (usedId.findIndex((f) => f === item.id + br.outerId) == -1) {
                usedId.push(item.id + br.outerId);
                var color = this.getColor(br.outerId + item.id);
                html = '<span class="color' + color + '">';
                //colorCount++;
              } else {
                //var colorindex = usedId.findIndex(f => f === item.id + br.outerId)
                html =
                  '<span class="color' +
                  this.getColor(item.id + br.outerId) +
                  '">';
              }

              this.totalFilterExpression += " " + html + br.brace + "</span>";
            }
          }
        }

        if (counter < count)
          this.totalFilterExpression +=
            " <span class='logic-operator'>" +
            item.logicalOperatorTitle +
            "</span> ";
      });
    } else {
      this.totalFilterExpression = "";
    }
  }

  isValidate(): Boolean {
    if (!this.selectedColumn) {
      this.showMessage(
        this.getText("AdvanceFilter.PleaseSelectField"),
        MessageType.Warning
      );
      return false;
    }

    if (!this.selectedOperator) {
      this.showMessage(
        this.getText("AdvanceFilter.PleaseSelectOperator"),
        MessageType.Warning
      );
      return false;
    }

    if (!this.selectedValue && this.selectedValue !== false) {
      console.log(this.selectedValue);
      
      this.showMessage(
        this.getText("AdvanceFilter.PleaseEnterValue"),
        MessageType.Warning
      );
      return false;
    }

    if (!this.selectedLogicalOperator) {
      this.showMessage(
        this.getText("AdvanceFilter.PleaseSelectLogicOperator"),
        MessageType.Warning
      );
      return false;
    }

    return true;
  }

  removeFilter() {
    if (this.selectedRows) {
      var deleted: Array<FilterRow> = [];

      var index = this.groupFilters.findIndex(
        (gf) => gf.id === this.gFilterSelected
      );
      var filters = this.groupFilters[index].filters;

      this.selectedRows.forEach((item) => {
        //filters.splice(item,1);
        deleted.push(filters[item]);
      });

      var sortDeleted = this.selectedRows.sort(
        (a, b) => parseFloat(a.order) - parseFloat(b.order)
      );

      sortDeleted.forEach((del) => {
        var index = filters.findIndex((fi) => fi.id === del.id);
        if (index > -1) {
          if (filters[index].braces) {
            var deleteBraces = filters[index].braces;
            if (deleteBraces) {
              deleteBraces.forEach((b) => {
                if (filters[index + 1]) {
                  var nextFilter = filters[index + 1];
                  if (!nextFilter.braces)
                    nextFilter.braces = new Array<Braces>();
                  nextFilter.braces.push(b);
                } else {
                  if (filters[index].braces) {
                    filters[index].braces.forEach((br) => {
                      if (filters.findIndex((f) => f.id === br.outerId) >= 0) {
                        var outerBraces = filters.filter(
                          (f) => f.id === br.outerId
                        )[0].braces;

                        outerBraces.forEach((obr) => {
                          if (obr.outerId == filters[index].id) {
                            deleteBraces.push(obr);
                          }
                        });

                        deleteBraces.forEach((dbr) => {
                          var dindex = outerBraces.findIndex((br) => br == dbr);
                          outerBraces.splice(dindex);
                        });
                      }
                    });
                  }
                }
              });
            }
          }
        }

        filters.splice(index, 1);
      });

      this.computeTotalExpression();
      this.selectedRows = [];

      this.revertToDefaultValues();

      this.groupFilters[index].filters = filters;
      this.filters = filters;
      this.revertToDefaultValues();
      //this.saveFiltersToDB(false);
      this.showMessage(
        this.getText("AdvanceFilter.FilterDeletedSuccess"),
        MessageType.Succes
      );
    }
  }

  addGroupFilter() {
    this.isEditMode = false;
    this.filterGroupName = "";
    this.filterUseForOthers = false;
    this.activeGroupFilter = true;
  }

  checkForDuplicateName(name: string) {
    var findIndex = this.groupFilters.findIndex((f) => f.name == name);
    if (findIndex >= 0) {
      this.showMessage(this.getText("AdvanceFilter.FilterNameIsDuplicated"));
      return false;
    }

    return true;
  }

  onGroupFilterOk() {
    if (!this.filterGroupName) {
      this.showMessage(this.getText("AdvanceFilter.FilterNameIsRequired"));
      return;
    }

    if (this.activeSaveFilter) {
      this.isEditMode = false;
    }

    if (!this.checkForDuplicateName(this.filterGroupName) && !this.isEditMode)
      return;

    if (!this.isEditMode) {
      var gf = new GroupFilter();

      var max = 0;
      this.groupFilters.forEach((it) => {
        if (it.id > max) {
          max = it.id;
        }
      });

      gf.id = max + 1;
      gf.isNew = true;
      gf.name = this.filterGroupName;
      gf.isPublic = this.filterUseForOthers;

      if (this.activeSaveFilter) {
        gf.filters = this.groupFilters[0].filters;
      }

      if (this.activeSaveFilter) {
        this.saveFil(gf);
        this.activeSaveFilter = false;
        this.groupFilters[0].filters = undefined;
        return;
      }

      if (this.activeCopyFilter) {
        this.copyFiltersToDB(gf);
        this.activeCopyFilter = false;
        return;
      }

      this.groupFilters.push(gf);
      this.currentGFilter = gf;
      this.gFilterSelected = gf.id;
      this.gFilterSelectChange(gf);

      this.activeGroupFilter = false;
    } else {
      var index = this.groupFilters.findIndex(
        (gf) => gf.id === this.gFilterSelected
      );
      this.groupFilters[index].name = this.filterGroupName;
      this.groupFilters[index].isPublic = this.filterUseForOthers;
      this.activeGroupFilter = false;
      this.gFilterSelected = this.groupFilters[index].id;
      this.activeSaveFilter = false;
      this.activeCopyFilter = false;

      this.saveGroupFilterToDB(this.groupFilters[index]);
    }
  }

  saveGroupFilterToDB(gf) {
    var filterModel = new FilterViewModel();
    filterModel.id = gf.id;
    filterModel.isPublic = gf.isPublic;
    filterModel.name = gf.name;
    filterModel.viewId = this.viewId;
    filterModel.userId = this.UserId;
    filterModel.values = JSON.stringify(gf.filters);

    this.advanceFilterService
      .saveFilter(gf.id, filterModel)
      .subscribe((res) => {});
  }

  onGroupFilterCancel() {
    this.activeGroupFilter = false;
    this.activeSaveFilter = false;
    this.activeCopyFilter = false;
  }

  gDeleteIsDisable() {
    if (this.gFilterSelected == -1) return true;
    return false;
  }

  gEditIsDisable() {
    if (this.gFilterSelected == -1) return true;
    return false;
  }

  public gFilterSelectChange(groupFilter: GroupFilter) {
    var index = this.groupFilters.findIndex((gf) => gf.id === groupFilter.id);
    this.filters = this.groupFilters[index].filters;
    this.selectedRows = [];
    this.gFilterSelected = groupFilter.id;

    this.computeTotalExpression();
  }

  editGroupFilter() {
    var index = this.groupFilters.findIndex(
      (gf) => gf.id === this.gFilterSelected
    );
    this.filterGroupName = this.groupFilters[index].name;
    this.filterUseForOthers = this.groupFilters[index].isPublic;
    this.isEditMode = true;
    this.activeGroupFilter = true;
  }

  removeGroupFilter() {
    var index = this.groupFilters.findIndex(
      (gf) => gf.id === this.gFilterSelected
    );
    if (!this.deletedGroupFilters)
      this.deletedGroupFilters = new Array<GroupFilter>();

    this.deletedGroupFilters.push(this.groupFilters[index]);
    this.groupFilters.splice(index, 1);
    this.gFilterSelected = -1;
    this.currentGFilter = this.groupFilters[this.groupFilters.length - 1];
    this.gFilterSelectChange(this.currentGFilter);
    this.computeTotalExpression();

    if (this.deletedGroupFilters) {
      this.deletedGroupFilters.forEach((filterModel) => {
        if (!filterModel.isNew)
          this.advanceFilterService.deleteFilter(filterModel.id).subscribe();
      });
    }

    var unsavedFilters = <Array<GroupFilter>>(
      JSON.parse(this.bStorageService.getSession("unSaveFilter" + this.viewId))
    );
    var unsaveIndex = unsavedFilters.findIndex(
      (f) => f.name == this.groupFilters[index].name
    );
    if (unsaveIndex >= 0) {
      unsavedFilters.splice(unsaveIndex, 1);
      this.bStorageService.setSession(
        "unSaveFilter" + this.viewId,
        JSON.stringify(unsavedFilters)
      );
    }

    this.showMessage(
      this.getText("AdvanceFilter.FilterRemovedSuccess"),
      MessageType.Succes
    );
  }
}
