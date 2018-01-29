
import { ToastrService, ToastConfig } from 'toastr-ng2'; /** add this component for message in client side */

import { TranslateService } from "ng2-translate";

import { String } from './source';


export class DefaultComponent  {

    public translateService: TranslateService
    public updateMsg: string;
    public insertMsg: string;
    public deleteMsg: string;

    public deleteConfirmMsg: string;
    

    public rtlClass: string = "ui-rtl";
    public rtlUse: string = "rtl";


    constructor(public toastrService: ToastrService, public translate: TranslateService)
    {
        translate.addLangs(["en", "fa"]);
        translate.setDefaultLang('fa');

        var browserLang = 'fa';
        translate.use(browserLang);

        this.translateService = translate;

        this.localizeMsg();
    }

    localizeMsg() {
        // read message format for crud operations
        var entityType = '';
        this.translateService.get("Entity.Account").subscribe((msg: string) => {
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
            this.deleteConfirmMsg = String.Format(msg, arg.dataItem.name);
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