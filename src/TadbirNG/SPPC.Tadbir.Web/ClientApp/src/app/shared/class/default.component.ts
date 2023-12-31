import { Inject, Injectable, Optional, Renderer2 } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import {
  CompositeFilterDescriptor,
  SortDescriptor,
  State,
} from "@progress/kendo-data-query";
import { ViewTreeConfig } from "@sppc/config/models";
import { SettingService } from "@sppc/config/service/settings.service";
import {
  BrowserStorageService,
  SessionKeys,
} from "@sppc/shared/services/browserStorage.service";
import { MetaDataService } from "@sppc/shared/services/metadata.service";
import * as moment from "jalali-moment";
import { ToastrService } from "ngx-toastr";
import { Braces } from "../models";
import { BaseComponent } from "./base.component";
import { Filter } from "./filter";
import { FilterExpression } from "./filterExpression";
import { FilterExpressionBuilder } from "./filterExpressionBuilder";
import { Property } from "./metadata/property";
import { String } from "./source";
import { shareReplay } from "rxjs";

@Injectable()
export class DefaultComponent extends BaseComponent {
  public translateService: TranslateService;

  public metadataKey: string;

  /** array of property.this variable is a container for metadata */
  public properties: Map<string, any>;
  public errorMessages: string[];

  public baseEntityName: string;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public renderer: Renderer2,
    public metadataService: MetaDataService,
    public settingService: SettingService,
    @Optional() @Inject("empty") public entityType: string,
    @Optional() @Inject("empty") public viewId: number
  ) {
    super(toastrService, bStorageService);

    this.setLanguageSetting();

    this.metadataKey = String.Format(
      SessionKeys.MetadataKey,
      this.viewId ? this.viewId.toString() : "",
      this.currentlang
    );

    this.localizeMsg(this.entityType);

    var propertiesValue = this.bStorageService.getMetadata(this.metadataKey);
    this.properties = new Map<string, Array<Property>>();
    if (propertiesValue && propertiesValue != null) {
      var result = JSON.parse(propertiesValue);
      this.baseEntityName = result.entityName;
      this.properties.set(this.metadataKey, result.columns);
    }
  }

  /**
   * تنظیمات مربوط به زبان سیستم را انجام میدهد
   */
  private setLanguageSetting() {
    //use lang
    this.translate.addLangs(["en", "fa"]);

    var lang = this.bStorageService.getLanguage();
    if (lang) {
      this.currentlang = lang;
    } else {
      this.currentlang = "fa";
    }

    this.translate.setDefaultLang(this.currentlang);

    this.translate.use(this.currentlang);
    //use lang

    //rtl or ltr body

    if (this.currentlang == "fa") {
      this.renderer.addClass(document.body, "tRtl");
      this.renderer.removeClass(document.body, "tLtr");

      let mainContent = document.getElementById("mainContent");
      if (mainContent) {
        this.renderer.addClass(
          document.getElementById("mainContent"),
          "pull-left"
        );
        this.renderer.removeClass(
          document.getElementById("mainContent"),
          "pull-right"
        );
      }
    }

    if (this.currentlang == "en") {
      this.renderer.addClass(document.body, "tLtr");
      this.renderer.removeClass(document.body, "tRtl");
      let mainContent = document.getElementById("mainContent");
      if (mainContent) {
        this.renderer.addClass(
          document.getElementById("mainContent"),
          "pull-right"
        );
        this.renderer.removeClass(
          document.getElementById("mainContent"),
          "pull-left"
        );
      }
    }
    //rtl or ltr body

    this.translateService = this.translate;
  }

  /**
   * این تابع متادیتا مربوط به یک انتیتی را در قالب انتیتی به نام Property برمیگرداند.
   * @param name is a name of column like 'id' , 'name' , 'fiscalperiod' , ... .
   */
  public getMeta(name: string): Property | undefined {
    if (this.viewId) {
      var item: string | null;

      var metadataKey = String.Format(
        SessionKeys.MetadataKey,
        this.viewId ? this.viewId.toString() : "",
        this.currentlang
      );

      item = this.bStorageService.getMetadata(metadataKey);

      if (!this.properties)
        this.properties = new Map<string, Array<Property>>();

      var arr = JSON.parse(item != null ? item.toString() : "");
      this.properties.set(metadataKey, arr.columns);

      if (!this.properties.get(metadataKey)) return undefined;

      var result = this.properties
        .get(metadataKey)
        .find((p) => p.name.toLowerCase() == name.toLowerCase());

      return result;
    }
  }

  public getAllMetaData(viewId: number): Array<Property> | undefined {
    var metaDataName = String.Format(
      SessionKeys.MetadataKey,
      viewId ? viewId.toString() : "",
      this.currentlang
    );
    if (viewId) {
      var item: string | null;
      item = this.bStorageService.getMetadata(metaDataName);

      if (!this.properties) this.properties = new Map<string, any>();

      var arr = JSON.parse(item != null ? item.toString() : "");
      this.properties.set(metaDataName, arr.columns);

      if (!this.properties.get(metaDataName)) return undefined;

      this.baseEntityName = arr.entityName;

      return arr.columns;
    }
  }

  async getEntityName(viewId: number): Promise<string> {
    var metaDataName = String.Format(
      SessionKeys.MetadataKey,
      viewId ? viewId.toString() : "",
      this.currentlang
    );

    if (viewId) {
      var item: string | null;
      item = this.bStorageService.getMetadata(metaDataName);

      if (!this.properties) this.properties = new Map<string, any>();
      var result = JSON.parse(item != null ? item.toString() : "");
      return result.entityName;
    }
  }

  getLocalizedViewName(viewId: number): string {
    var metaDataName = String.Format(
      SessionKeys.MetadataKey,
      viewId ? viewId.toString() : "",
      this.currentlang
    );

    if (viewId) {
      var item: string | null;
      item = this.bStorageService.getMetadata(metaDataName);
      if (item) {
        if (!this.properties) this.properties = new Map<string, any>();
        var result = JSON.parse(item != null ? item.toString() : "");
        return result.name;
      }
    }

    return "";
  }

  async getAllMetaDataByViewIdAsync(viewId: number): Promise<Array<Property>> {
    var metaDataName = String.Format(
      SessionKeys.MetadataKey,
      viewId ? viewId.toString() : "",
      this.currentlang
    );

    if (viewId) {
      var item: string | null;
      item = this.bStorageService.getMetadata(metaDataName);

      if (!this.properties) this.properties = new Map<string, any>();

      var result = JSON.parse(item != null ? item.toString() : "");
      this.properties.set(metaDataName, result.columns);

      if (!this.properties.get(metaDataName)) return undefined;

      this.baseEntityName = result.entityName;
      return result.columns;
    }
  }

  public getViewTreeSettings(viewId: number): ViewTreeConfig {
    let treeConfig: Array<{ name: string; viewTree: any }> = [];
    treeConfig = JSON.parse(this.bStorageService.getViewTreeConfig());
    var viewName = "view-" + viewId;

    if (treeConfig == undefined || treeConfig.length == 0) {
      treeConfig = [];
      this.settingService.getViewTreeSettings(viewId)
      .pipe(
        shareReplay()
      )
      .subscribe((res) => {
        let result: any = res;
        let viewTree = result.current;
        if (this.currentlang == 'en')
          viewTree.levels.map((i,index) => { i.name = result.default.levels[index].name });

        treeConfig.push({ name: viewName, viewTree: viewTree });
        this.bStorageService.setViewTreeConfig(treeConfig);
        return JSON.parse(JSON.stringify(viewTree));
      });
    } else {
      var config = treeConfig.find((f) => f.name == viewName);
      if (config) {
        return config.viewTree;
      } else {
        this.settingService.getViewTreeSettings(viewId).subscribe((res) => {
          let result: any = res;
          let viewTree = result.current;
          if (this.currentlang == 'en')
            viewTree.levels.map((i,index) => { i.name = result.default.levels[index].name });

          treeConfig.push({ name: viewName, viewTree: viewTree });
          this.bStorageService.setViewTreeConfig(treeConfig);
          return JSON.parse(JSON.stringify(viewTree));
        });
      }
    }
  }

  /** return the current language */
  public currentlang: string = "";

  /** the default value of grid paging size  */
  pageSize: number = 10;

  public pageSizeArray: number[] = [5, 10, 20, 50, 100];

  public skip: number = 0;

  /** set number value for grid current page
   * @param value is page number.
   */
  public set pageIndex(value: number) {
    this.skip = value;
  }

  /** get number value for grid current page */
  public get pageIndex(): number {
    if (this.skip == 0) return 1;
    else return this.skip / this.pageSize + 1;
  }

  /** the current state of filtering and paging */
  public state: State = {
    skip: 0,
    take: 10,
    // Initial filter descriptor
    filter: {
      logic: "and",
      filters: [],
    },
  };

  allFiltersExpression: CompositeFilterDescriptor;

  public sort: SortDescriptor[] = [];

  /**
   * بعد از افزودن یک رکورد، رکورد جدید به صفحه آخر اضافه میشود و صفحه آخر صفحه بندی فعال میشود
   * @param totalRecords تعداد کل رکوردها
   */
  goToLastPage(totalRecords: number) {
    var pageCount: number = 0;
    pageCount = Math.floor(totalRecords / this.pageSize);

    if (
      totalRecords % this.pageSize == 0 &&
      totalRecords != pageCount * this.pageSize
    ) {
      this.skip = pageCount * this.pageSize - this.pageSize;
      return;
    }
    this.skip = pageCount * this.pageSize;
  }

  getFilters(filter: any): FilterExpression {
    let filters: Filter[] = [];

    if (filter.filters.length) {
      for (let i = 0; i < filter.filters.length; i++) {
        if (filter.filters[i].value !== "") {
          var operator = "";

          var dataType = "";
          var field = filter.filters[i].field;
          //var metadata = this.getMeta(filter.filters[i].field);

          var properties = this.getAllMetaData(this.viewId);
          var property = properties.filter(
            (p) => p.name.toLowerCase() === field.toLowerCase()
          );
          if (property && property.length > 0) {
            dataType = property[0].dotNetType;
          }

          //if (metadata != undefined) {
          //  dataType = metadata.dotNetType;
          //  if (metadata.expression)
          //    field = metadata.expression;
          //}

          switch (filter.filters[i].operator) {
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

          var filterValue = filter.filters[i].value;

          filters.push(new Filter(field, filterValue, operator, dataType));
        }
      }
    }

    var filterExpBuilder = new FilterExpressionBuilder();
    var filterExp = filterExpBuilder.And(filters).Build();

    return filterExp;
  }

  addFilterToFilterExpression(
    filterExp: FilterExpression,
    filter: Filter,
    operator: string
  ): FilterExpression {
    if (filter) {
      var newFilter = new FilterExpression();
      newFilter.filter = filter;
      newFilter.operator = operator;

      if (filterExp != null) {
        filterExp.children.push(newFilter);
        return filterExp;
      } else {
        if (filter) {
          var filterExpBuilder = new FilterExpressionBuilder();
          return filterExpBuilder.New(filter).Build();
        } else {
          return undefined;
        }
      }
    } else return filterExp;
  }

  andFilterToFilterExpression(
    filterExp: FilterExpression,
    advanceFilter: FilterExpression
  ): FilterExpression {
    if (filterExp != null) {
      filterExp.children.push(advanceFilter);
      return filterExp;
    } else {
      return advanceFilter;
    }
  }

  andTwoFilterExpression(
    filterExp1: FilterExpression,
    filterExp2: FilterExpression
  ): FilterExpression {
    if (filterExp1 != null) {
      if (filterExp2.filter) {
        var filter = new Filter(
          filterExp2.filter.FieldName,
          filterExp2.filter.Value,
          filterExp2.filter.Operator,
          filterExp2.filter.fieldTypeName
        );
        filterExp1.filter = filter;
      }

      if (filterExp2.children && filterExp2.children.length > 0) {
        var newFilter = new FilterExpression();
        newFilter.children = filterExp2.children;
        filterExp1.children.push(newFilter);
      }
      return filterExp1;
    } else {
      return filterExp2;
    }
  }

  addFilterExpressionWithBrace(
    sourceExpression: FilterExpression,
    filter: Filter,
    end: boolean = false,
    makeBraces: boolean = true
  ): FilterExpression {
    var firstExpressionId = "1";
    var lastExpressionId = "2";

    if (sourceExpression == null) {
      var startBrace = new Array<Braces>();

      var brace1: Braces = { brace: "(", outerId: lastExpressionId };
      startBrace.push(brace1);

      var firstFilter = new FilterExpression();
      filter.braces = makeBraces == true ? startBrace : undefined;
      filter.id = firstExpressionId;
      firstFilter.filter = filter;
      firstFilter.operator = " && ";

      sourceExpression = new FilterExpression();
      sourceExpression.operator = " && ";
      sourceExpression.children.push(firstFilter);
    } else {
      var nextFilter = new FilterExpression();

      if (end) {
        var endBrace = new Array<Braces>();
        var brace2: Braces = { brace: ")", outerId: firstExpressionId };
        endBrace.push(brace2);
        filter.id = lastExpressionId;
        filter.braces = makeBraces == true ? endBrace : undefined;
      }

      nextFilter.filter = filter;
      nextFilter.operator = " || ";

      sourceExpression.children.push(nextFilter);
    }

    return sourceExpression;
  }
  /**
   * this method localize CRUD messages
   */
  public localizeMsg(entityName: string) {
    // read message format for crud operations

    if (entityName) {
      var entityType = "";
      this.translateService
        .get("Entity." + entityName)
        .subscribe((msg: string) => {
          entityType = msg;
        });

      this.translateService
        .get("Messages.Inserted")
        .subscribe((msg: string) => {
          this.insertMsg = String.Format(msg, entityType);
        });

      this.translateService.get("Messages.Updated").subscribe((msg: string) => {
        this.updateMsg = String.Format(msg, entityType);
      });

      this.translateService.get("Messages.Deleted").subscribe((msg: string) => {
        this.deleteMsg = String.Format(msg, entityType);
      });

      this.translateService
        .get("Messages.DeleteConfirm")
        .subscribe((msg: string) => {
          this.deleteConfirmMsg = String.Format(msg, entityType);
        });
    }
  }

  /**
   * return message or translate key from resource file (from fa.json or en.json)
   * @param key is key of message like 'Buttons.Ok'
   */
  public getText(key: string): string {
    var msgText = this.translateService.instant(key);
    return msgText;
  }

  /**
   * prepare confim message for delete operation
   * @param text is a part of message that use for delete confirm message
   */
  public prepareDeleteConfirm(text: string) {
    this.translateService
      .get("Messages.DeleteConfirm")
      .subscribe((msg: string) => {
        this.deleteConfirmMsg = String.Format(msg, text);
      });
  }

  /**
   * change current language
   * @param value is string like 'fa' , 'en' , 'de' , ....
   */
  changeLanguage(value: string) {
    this.translateService.use(value);

    this.currentlang = value;
    this.bStorageService.setLanguage(value);

    this.localizeMsg(this.entityType);
  }

  compareDate(dateA: Date, dateB: Date): number {
    var dateValueA = moment(dateA).format("YYYY/MM/DD");
    var dateValueB = moment(dateB).format("YYYY/MM/DD");

    let dA = new Date(dateValueA + " " + "00:00:00");
    let dB = new Date(dateValueB + " " + "00:00:00");

    var diff = dA.getTime() - dB.getTime();
    var diffDays = Math.ceil(diff / (1000 * 3600 * 24));

    //اگر دو تاریخ مساوی باشد
    if (diffDays == 0) return 0;

    //اگر تاریخ اول از تاریخ دوم بزرگتر باشد
    if (diffDays > 0) return 1;

    //اگر تاریخ اول از تاریخ دوم کوچکتر باشد
    if (diffDays < 0) return -1;
  }

  async metadataResolver(viewId: number) {
    var lang = this.currentlang;

    var metadataKey = String.Format(
      SessionKeys.MetadataKey,
      viewId ? viewId.toString() : "",
      lang ? lang : "fa"
    );
    var metadata = this.bStorageService.getMetadata(metadataKey);
    if (metadata == null) {
      const response = await this.metadataService
        .getMetaDataById(viewId)
        .toPromise();
      this.bStorageService.setMetadata(metadataKey, (<any>response).columns);
    }
  }
}
