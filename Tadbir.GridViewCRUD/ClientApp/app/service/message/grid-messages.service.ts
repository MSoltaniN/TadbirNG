import { MessageService } from '@progress/kendo-angular-l10n';
import { TranslateService } from "ng2-translate";
import { Injectable } from "@angular/core";


//string messages = {
//    'kendo.grid.noRecords': 'No hay datos disponibles.'
//};

@Injectable()
export class GridMessageService extends MessageService {

    private translateService: TranslateService;
    
    get(key: string): string | undefined {

        var result: string = "";

        this.translateService.addLangs(["en", "fa"]);
        this.translateService.setDefaultLang('fa');

        var browserLang = 'fa';//translate.getBrowserLang();
        this.translateService.use(browserLang);

        this.translateService.get("GridMessages." + key.split('.')[2]).subscribe((msg: string) => {
            result = msg;
        });
       
        return result;
    }
   
}