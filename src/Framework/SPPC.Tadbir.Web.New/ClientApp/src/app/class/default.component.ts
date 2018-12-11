
import { ToastrService } from 'ngx-toastr';

import { TranslateService } from '@ngx-translate/core';

import { String } from './source';
import { State, CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { BaseComponent } from "./base.component"
import { Property } from "./metadata/property"

import { Filter } from './filter';
import { Renderer2, Injectable, Inject, Injector, forwardRef, Optional } from "@angular/core";
import { MetaDataService } from '../service/metadata/metadata.service';
import { Http } from '@angular/http';
import { FormGroup, FormControl, Validators, ValidatorFn } from '@angular/forms';

import * as moment from 'jalali-moment';
import { FilterExpression } from './filterExpression';
import { FilterExpressionBuilder } from './filterExpressionBuilder';
import { HttpResponse } from '@angular/common/http';
import { Account, ViewTreeConfig } from '../model/index';
import { IEntity } from '../model/IEntity';
import { SettingService } from '../service/index';


@Injectable()
export class DefaultComponent extends BaseComponent {


  public translateService: TranslateService



  /** array of property.this variable is a container for metadata */
  public properties: { [id: string]: Array<Property>; } = {}

  /*
  private form: FormGroup;

  public get editForm(): FormGroup {

      if (this.form == undefined) {

          this.form = new FormGroup({ id: new FormControl() });


          if (this.properties[this.metaDataName] == undefined) {

              this.metadataService.getMetaData(this.metaDataName).finally(() => {
                  this.fillFormValidators();

                  return this.form;

              }).subscribe((res1: any) => {

                  this.properties[this.metaDataName] = res1.properties;

                  localStorage.setItem(this.metaDataName, JSON.stringify(this.properties[this.metaDataName]))

                  return
              });

          }

      }
      else {
          this.fillFormValidators();
      }

      return this.form;

  }

  */



  constructor(public toastrService: ToastrService, public translate: TranslateService
    , public renderer: Renderer2, public metadataService: MetaDataService, public settingService: SettingService,
    @Optional() @Inject('empty') public entityType: string, @Optional() @Inject('empty') public metaDataName: string) {


    super(toastrService);

    this.setLanguageSetting();

    this.localizeMsg();

        //if(metaDataName != null && metaDataName != "")
        //  this.saveMetadataInCache(metaDataName);
        //if (createForm)
        //this.initializeFrom();
    }

  //abstract filterChange(filter: CompositeFilterDescriptor): void 

  //abstract reloadGrid(insertedModel?: IEntity | undefined): void 

  /*
  private fillFormValidators() {

      var p: Property | undefined = undefined;

      if (this.properties[this.metaDataName] == undefined) return;

      for (let entry of this.properties[this.metaDataName]) {

          var name: string = entry.name.toLowerCase().substring(0, 1) + entry.name.substring(1);

          var validators: ValidatorFn[] = [];

          if (entry.length > 0) validators.push(Validators.maxLength(entry.length));

          if (entry.minLength > 0) validators.push(Validators.minLength(entry.minLength));

          if (!entry.isNullable) validators.push(Validators.required);

          if (!this.form.contains(name)) {
              this.form.addControl(name, new FormControl("", validators));
          }
      }

  }

  private initializeFrom() {

      if (this.properties[this.metaDataName] == undefined) {

          this.metadataService.getMetaData(this.metaDataName).finally(() => {
              this.fillFormValidators();

          }).subscribe((res1: any) => {

              this.properties[this.metaDataName] = res1.properties;

              localStorage.setItem(this.metaDataName, JSON.stringify(this.properties[this.metaDataName]))

              return
          });

      }
      else {
          this.fillFormValidators();
      }

  }
  */

  /**
   * تنظیمات مربوط به زبان سیستم را انجام میدهد
   */
  private setLanguageSetting() {
    //use lang
    this.translate.addLangs(["en", "fa"]);

    var lang = localStorage.getItem('lang');
    if (lang) {
      this.currentlang = lang;
    }
    else {
      this.currentlang = "fa";
    }

    this.translate.setDefaultLang(this.currentlang);

    this.translate.use(this.currentlang);
    //use lang


    //rtl or ltr body

    if (this.currentlang == 'fa') {
      this.renderer.addClass(document.body, 'tRtl');
      this.renderer.removeClass(document.body, 'tLtr');

      this.renderer.addClass(document.getElementById('mainContent'), 'pull-left')
      this.renderer.removeClass(document.getElementById('mainContent'), 'pull-right')
    }

    if (this.currentlang == 'en') {
      this.renderer.addClass(document.body, 'tLtr');
      this.renderer.removeClass(document.body, 'tRtl');

      this.renderer.addClass(document.getElementById('mainContent'), 'pull-right')
      this.renderer.removeClass(document.getElementById('mainContent'), 'pull-left')
    }
    //rtl or ltr body

    this.translateService = this.translate;
  }



  /**
  * این تابع متادیتا مربوط به یک انتیتی را در قالب انتیتی به نام Property برمیگرداند.
  * @param name is a name of column like 'id' , 'name' , 'fiscalperiod' , ... .    
  */
  public getMeta(name: string): Property | undefined {

    if (this.metaDataName) {

      if (localStorage.getItem(this.metaDataName) == "undefined" || localStorage.getItem(this.metaDataName) == null) {
        this.metadataService.getMetaData(this.metaDataName).finally(() => {

          if (this.properties[this.metaDataName] == undefined || this.properties[this.metaDataName].length == 0) return undefined;

          var result = this.properties[this.metaDataName].find(p => p.name.toLowerCase() == name.toLowerCase());

          return result;

        }).subscribe((res1: any) => {

          this.properties[this.metaDataName] = res1.columns;

          localStorage.setItem(this.metaDataName, JSON.stringify(this.properties[this.metaDataName]))

          var result = this.properties[this.metaDataName].find(p => p.name.toLowerCase() == name.toLowerCase());

          return result;
        });
      }
      else {


        var item: string | null;
        item = localStorage.getItem(this.metaDataName);

        if (this.properties == undefined) this.properties = {};

        this.properties[this.metaDataName] = JSON.parse(item != null ? item.toString() : "");

        if (this.properties[this.metaDataName] == undefined || this.properties[this.metaDataName].length == 0) return undefined;

        var result = this.properties[this.metaDataName].find(p => p.name.toLowerCase() == name.toLowerCase());

        return result;

      }

    }
  }

  //public treeConfig: Array<{ name: string, viewTree: any }> = [];

  public getViewTreeSettings(viewId: number): ViewTreeConfig {

    let treeConfig: Array<{ name: string, viewTree: any }> = [];

    treeConfig = JSON.parse(sessionStorage.getItem("viewTreeConfig"));
    var viewName = "view-" + viewId;

    if (treeConfig == undefined || treeConfig.length == 0) {
      treeConfig = [];
      this.settingService.getViewTreeSettings(viewId).subscribe(res => {

        let result: any = res;

        treeConfig.push({ name: viewName, viewTree: result.current });
        sessionStorage.setItem("viewTreeConfig", JSON.stringify(treeConfig));

        return JSON.parse(JSON.stringify(result.current));
      })
    }
    else {
      var config = treeConfig.find(f => f.name == viewName);
      if (config) {
        return config.viewTree;
      }
      else {
        this.settingService.getViewTreeSettings(viewId).subscribe(res => {

          let result: any = res;

          treeConfig.push({ name: viewName, viewTree: result.current });

          sessionStorage.setItem("viewTreeConfig", JSON.stringify(treeConfig));

          return JSON.parse(JSON.stringify(result.current));

        })
      }
    }
  }


    /** return the current language */
    public currentlang: string = "";


  /** the default value of grid paging size  */
  pageSize: number = 10;


  public skip: number = 0;


  /** set number value for grid current page
  * @param value is page number.
  */
  public set pageIndex(value: number) {
    this.skip = value;
  }

  /** set number value for grid current page */
  public get pageIndex(): number {
    if (this.skip == 0)
      return 1;
    else
      return (this.skip / this.pageSize) + 1;
  }

  /** the current state of filtering and paging */
  public state: State = {
    skip: 0,
    take: 10,
    // Initial filter descriptor
    filter: {
      logic: "and",
      filters: [{ field: "code", operator: "contains", value: "" }]
    }
  };

  /**
   * بعد از افزودن یک رکورد، رکورد جدید به صفحه آخر اضافه میشود و صفحه آخر صفحه بندی فعال میشود 
   * @param totalRecords تعداد کل رکوردها
   */
  goToLastPage(totalRecords: number) {
    var pageCount: number = 0;
    pageCount = Math.floor(totalRecords / this.pageSize);

    if (totalRecords % this.pageSize == 0 && totalRecords != pageCount * this.pageSize) {
      this.skip = (pageCount * this.pageSize) - this.pageSize;
      return;
    }
    this.skip = (pageCount * this.pageSize)
  }

  getFilters(filter: any): FilterExpression {
    let filters: Filter[] = [];

    if (filter.filters.length) {
      for (let i = 0; i < filter.filters.length; i++) {
        if (filter.filters[i].value != "") {
          var operator = "";
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

          var metadata = this.getMeta(filter.filters[i].field);
          var dataType = '';
          var field = filter.filters[i].field;

          if (metadata != undefined) {
            dataType = metadata.dotNetType;
            if (metadata.expression)
              field = metadata.expression;
          }

          var filterValue = filter.filters[i].value;

          filters.push(new Filter(field, filterValue, operator, dataType));

        }
      }

    }

    var filterExpBuilder = new FilterExpressionBuilder();
    var filterExp = filterExpBuilder.And(filters)
      .Build();

    return filterExp;
  }

  addFilterToFilterExpression(filterExp: FilterExpression, filter: Filter, operator: string): FilterExpression {
    var newFilter = new FilterExpression();
    newFilter.filter = filter;
    newFilter.operator = operator;

    if (filterExp != null) {
      filterExp.children.push(newFilter);
      return filterExp;
    }
    else {
      var filterExpBuilder = new FilterExpressionBuilder();
      return filterExpBuilder.New(filter).Build();
    }

  }

  /**
   * this method localize CRUD messages 
   */
  private localizeMsg() {
    // read message format for crud operations
    var entityType = '';
    this.translateService.get("Entity." + this.entityType).subscribe((msg: string) => {
      entityType = msg;
    });

    this.translateService.get("Messages.Inserted").subscribe((msg: string) => {
      this.insertMsg = String.Format(msg, entityType);
    });

    this.translateService.get("Messages.Updated").subscribe((msg: string) => {
      this.updateMsg = String.Format(msg, entityType);;
    });

    this.translateService.get("Messages.Deleted").subscribe((msg: string) => {
      this.deleteMsg = String.Format(msg, entityType);;
    });

    this.translateService.get("Messages.DeleteConfirm").subscribe((msg: string) => {
      this.deleteConfirmMsg = String.Format(msg, entityType);
    });


  }

  /**
   * return message or translate key from resource file (from fa.json or en.json)
   * @param key is key of message like 'Buttons.Ok'
   */
  public getText(key: string): string {
    var msgText = '';
    this.translateService.get(key).subscribe((msg: string) => {
      msgText = msg;
    });
    return msgText;
  }

  /**
   * prepare confim message for delete operation
   * @param text is a part of message that use for delete confirm message
  */
  public prepareDeleteConfirm(text: string) {
    this.translateService.get("Messages.DeleteConfirm").subscribe((msg: string) => {
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
    localStorage.setItem('lang', value);

    this.localizeMsg();

    //switch (value) {
    //    case "fa":
    //        {
    //            this.rtlUse = "rtl";
    //            this.rtlClass = "ui-rtl"
    //            break;
    //        }
    //    case "en":
    //        {
    //            this.rtlUse = "ltr";
    //            this.rtlClass = ""
    //            break;
    //        }
    //}


  }

}
