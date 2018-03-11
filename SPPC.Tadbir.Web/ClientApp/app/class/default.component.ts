﻿
import { ToastrService, ToastConfig } from 'toastr-ng2'; /** add this component for message in client side */

import { TranslateService } from "ng2-translate";

import { String } from './source';
import { State } from "@progress/kendo-data-query/dist/es/state";
import { BaseComponent } from "./base.component"
import { Property } from "./metadata/property"

import { Filter } from './filter';
import { Renderer2, Injectable, Inject, Injector, forwardRef, Optional } from "@angular/core";
import { MetaDataService } from '../service/metadata/metadata.service';
import { Http } from '@angular/http';
import { AppModule } from '../app.module.server';

@Injectable()
export class DefaultComponent extends BaseComponent {

    public translateService: TranslateService

    /** this message show after update command */
    public updateMsg: string;

    /** this message show after insert command */
    public insertMsg: string;

    /** this message show after delete command */
    public deleteMsg: string;

    /** this message show in confirm messagebox */
    public deleteConfirmMsg: string;

    /** array of property.this variable is a container for metadata */
    public properties: { [id: string]: Array<Property>; } = {}

    

    constructor(public toastrService: ToastrService, public translate: TranslateService
        , public renderer: Renderer2, private metadataService: MetaDataService,@Optional() @Inject('empty') private entityType : string) {


        super(toastrService);
        
        //use lang
        translate.addLangs(["en", "fa"]);

        var lang = localStorage.getItem('lang');
        if (lang) {
            this.currentlang = lang;
        }
        else {
            this.currentlang = "fa";
        }

        translate.setDefaultLang(this.currentlang);

        translate.use(this.currentlang);
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

        this.translateService = translate;

        this.localizeMsg();
    }


    /**
    * this function return metadata of column
    * @param name is a name of column like 'id' , 'name' , 'fiscalperiod' , ... .    
    */    
    public getMeta(name: string):Property | undefined {      
        
        if (localStorage.getItem(this.entityType) == undefined) {
            this.metadataService.getMetaData(this.entityType).subscribe(res1 => {

                this.properties[this.entityType] = res1.properties;

                localStorage.setItem(this.entityType, JSON.stringify(this.properties[this.entityType]))
                var result = this.properties[this.entityType].find(p => p.name.toLowerCase() == name.toLowerCase());

                return result;
            });
        }
        else {
            var item: string | null;
            item = localStorage.getItem(this.entityType);
            this.properties[this.entityType] = JSON.parse(item != null ? item.toString() : "");
        }

        if (this.properties[this.entityType] == undefined || this.properties[this.entityType].length == 0) return undefined;
        var result = this.properties[this.entityType].find(p => p.name.toLowerCase() == name.toLowerCase());

        return result;
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

    getFilters(filter: any): Filter[] {
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

                    
                    //var dataType = "";
                    /*
                    switch (filter.filters[i].field) {
                        case "fiscalPeriodId":
                        case "level":
                            dataType = "System.Int16";
                            break;
                        case "code":
                        case "description":
                        case "name":
                            dataType = "System.String";
                            break;
                        default:
                            dataType = "System.String";
                    }*/

                    var metadata = this.getMeta(filter.filters[i].field);
                    var dataType = '';
                    if (metadata != undefined)
                        dataType = metadata.dotNetType;

                    filters.push(new Filter(filter.filters[i].field, filter.filters[i].value, operator, dataType));

                }
            }

        }

        return filters;
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
    public getText(key: string) : string
    {
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
    public prepareDeleteConfirm(text : string)
    {
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
        localStorage.setItem('lang',value);
        
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