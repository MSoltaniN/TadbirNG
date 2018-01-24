
import { Observable } from 'rxjs/Observable';
import { TranslateService } from "ng2-translate";


var messages : { [id: string]: string; } = {
    "filterEqOperator": "مساوی",
    "filterNotEqOperator" : "نا مساوی",
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





export class SppcMessageService {


    private translateService: TranslateService
    constructor(private translate: TranslateService) {

        this.translateService = translate;
    }




    getText(key: string): string {

        var value: string = "";

        if (this.translateService.currentLang == 'fa')
            return messages[key];

        return value;
    }


}