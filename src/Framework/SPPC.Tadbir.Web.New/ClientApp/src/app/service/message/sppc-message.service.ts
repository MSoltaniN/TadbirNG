
import { Observable } from 'rxjs/Observable';
import { TranslateService } from "ng2-translate";


var messages : { [id: string]: string; } = {
    "filterEqOperator": "مساوی",
    "filterNotEqOperator" : "نامساوی",
    "filterIsNullOperator": "مساوی با تهی",
    "filterIsNotNullOperator": "مخالف با تهی",
    "filterIsEmptyOperator": "مساوی با خالی",
    "filterIsNotEmptyOperator": "مخالف با خالی",
    "filterStartsWithOperator": "شروع با",
    "filterContainsOperator": "شامل شود",
    "filterNotContainsOperator": "شامل نشود",
    "filterEndsWithOperator": "پایان با",
    "filterGteOperator": "بزرگتر مساوی با",
    "filterGtOperator": "بزرگتر از",
    "filterLteOperator": "کوچکتر مساوی با",
    "filterLtOperator": "کوچکتر از",
    "filterIsTrue": "True",
    "filterIsFalse": "False",
    "filterBooleanAll": "شامل همه"};

var messagesEn: { [id: string]: string; } = {
    "filterEqOperator": "Equal",
    "filterNotEqOperator": "Not Equal",
    "filterIsNullOperator": "Is Null",
    "filterIsNotNullOperator": "Is Not Null",
    "filterIsEmptyOperator": "Is Empty",
    "filterIsNotEmptyOperator": "Is Not Empty",
    "filterStartsWithOperator": "Starts With",
    "filterContainsOperator": "Contains",
    "filterNotContainsOperator": "Not Contains",
    "filterEndsWithOperator": "Ends With",
    "filterGteOperator": "Greater Than Equal",
    "filterGtOperator": "Greater Than",
    "filterLteOperator": "Less Than Equal",
    "filterLtOperator": "Less Than",
    "filterIsTrue": "Is True",
    "filterIsFalse": "Is False",
    "filterBooleanAll": "All"
};



export class SppcMessageService {


    private translateService: TranslateService
    constructor(private translate: TranslateService) {

        this.translateService = translate;
    }




    getText(key: string): string {

        var value: string = "";

        if (this.translateService.currentLang == 'fa')
            return messages[key];

        if (this.translateService.currentLang == 'en')
            return messagesEn[key];

        return value;
    }


}