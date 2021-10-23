import { Pipe, PipeTransform } from "@angular/core";
import { DatePipe } from "@angular/common";
import * as moment from 'jalali-moment';


@Pipe({
    name: 'SppcDate'
})
export class SppcDatePipe implements PipeTransform {

  
  private gregorianDateFormat: string = "MM/DD/YYYY";
    private jalaliDateFormat: string = "YYYY/MM/DD";
    public date: any;

    constructor(private datepipe: DatePipe) {      
    }

    transform(value: any,dateType:string, withTime: string) {
      
      if (value == null || value == undefined)
            return "";

        let hasTime: boolean = false;
        hasTime = (withTime != null && withTime != undefined && withTime.toLowerCase() == "datetime") ? true : false;
        

      if (dateType.toLowerCase() == 'gregorian') {        
        this.date = this.datepipe.transform(value, 'yyyy-M-d HH:mm');
        let format: string = hasTime ? this.gregorianDateFormat + " HH:mm" : this.gregorianDateFormat;

        let MomentDate = moment(this.date).locale('en').format(format);
        return MomentDate;
      }
      else if (dateType.toLowerCase() == 'jalali') {
        this.date = this.datepipe.transform(value, 'yyyy-M-d HH:mm');
        let format: string = hasTime ? this.jalaliDateFormat + " HH:mm" : this.jalaliDateFormat;
        moment.locale('en');
        let MomentDate = moment(this.date).locale('fa').format(format);
        return MomentDate;
      }

    }
}
