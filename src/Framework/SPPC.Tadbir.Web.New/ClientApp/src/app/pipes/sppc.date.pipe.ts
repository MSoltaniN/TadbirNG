import { Pipe, PipeTransform } from "@angular/core";
import { DatePipe } from "@angular/common";
import * as moment from 'jalali-moment';


@Pipe({
    name: 'SppcDate'
})
export class SppcDatePipe implements PipeTransform {

    private dateLocale: string = 'fa';
    private dateFormat: string = "YYYY/MM/DD";
    public date: any;

    constructor(private datepipe: DatePipe) {

        //TODO از متغیرهای محیطی گرفته شود

        var lang = localStorage.getItem('lang');
        if (lang) {
            this.dateLocale = lang;
            if (lang == "en")
                this.dateFormat = "MM/dd/yyyy";
        }
    }

    transform(value: any, withTime: string) {
        if (value == null || value == undefined)
            return "";

        let hasTime: boolean = false;
        hasTime = (withTime != null && withTime != undefined && withTime.toLowerCase() == "datetime") ? true : false;
        let format: string = hasTime ? this.dateFormat + " HH:mm" : this.dateFormat;

        if (this.dateLocale == 'en')
            return this.datepipe.transform(value, format);
        else {
            this.date = this.datepipe.transform(value, 'yyyy-M-d HH:mm');
            moment.locale('en');
            let MomentDate = moment(this.date).locale('fa').format(format);
            return MomentDate;
        }

    }
}