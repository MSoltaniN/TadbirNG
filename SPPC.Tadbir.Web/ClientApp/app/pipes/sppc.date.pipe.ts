import { Pipe, PipeTransform } from "@angular/core";
import { DatePipe } from "@angular/common";
import * as moment from 'jalali-moment';

@Pipe({
    name: 'SppcDate'
})
export class SppcDatePipe implements PipeTransform {

    private dateLocale: string;
    private dateFormat: string;

    constructor(private datepipe: DatePipe) {

        //TODO از متغیرهای محیطی گرفته شود
         this.dateLocale = 'fa';
        this.dateFormat = "yyyy-MM-dd hh:mm"
        var lang = localStorage.getItem('lang');
        if (lang) {
            this.dateLocale = lang;
            if (lang == "en")
                this.dateFormat = "MM/DD/YYYY HH:mm";
        }
    }

    transform(value: any) {
        if (this.dateLocale == 'en')
            return this.datepipe.transform(value, 'MM/dd/yyyy hh:mm');
        else {
            var d = this.datepipe.transform(value, 'yyyy-MM-dd hh:mm');
            let MomentDate = moment(value);
            return MomentDate.locale('fa').format("YYYY/M/D hh:mm");
        }
    }
}