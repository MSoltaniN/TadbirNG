
import { ToastrService, ToastConfig } from 'toastr-ng2'; /** add this component for message in client side */

import { TranslateService } from "ng2-translate";

import { String } from './source';
import { State } from "@progress/kendo-data-query/dist/es/state";
import { BaseComponent } from "./base.component"

import { Filter } from './filter';


export class DefaultComponent extends BaseComponent {

    public translateService: TranslateService
    public updateMsg: string;
    public insertMsg: string;
    public deleteMsg: string;

    public deleteConfirmMsg: string;
    

    public rtlClass: string = "ui-rtl";
    public rtlUse: string = "rtl";
    

    constructor(public toastrService: ToastrService, public translate: TranslateService, public entityName: string) 
    {
        
        super(toastrService);

        translate.addLangs(["en", "fa"]);
        translate.setDefaultLang('fa');

        var browserLang = 'fa';
        translate.use(browserLang);

        this.translateService = translate;

        this.localizeMsg();        
    }

    pageSize: number = 10;
    skip: number = 0;

    
    get pageIndex(): number {
        if (this.skip == 0)
            return 1;
        else
            return (this.skip / this.pageSize) + 1;        
    }
    

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

                    var dataType = "";
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
                    }


                    filters.push(new Filter(filter.filters[i].field, filter.filters[i].value, operator, dataType))

                }
            }

        }

        return filters;
    }

    localizeMsg() {
        // read message format for crud operations
        var entityType = '';
        this.translateService.get("Entity." + this.entityName).subscribe((msg: string) => {
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

       
    }

    public prepareDeleteConfirm(name : string)
    {
        this.translateService.get("Messages.DeleteConfirm").subscribe((msg: string) => {
            this.deleteConfirmMsg = String.Format(msg, name);
        });
    }

    languageChange(value: string) {
        this.translateService.use(value);
        this.localizeMsg();
        switch (value) {
            case "fa":
                {
                    this.rtlUse = "rtl";
                    this.rtlClass = "ui-rtl"
                    break;
                }
            case "en":
                {
                    this.rtlUse = "ltr";
                    this.rtlClass = ""
                    break;
                }
        }


    }

}