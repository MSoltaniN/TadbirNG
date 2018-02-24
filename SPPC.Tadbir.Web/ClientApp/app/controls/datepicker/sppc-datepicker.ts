import { Component, OnInit, Input, forwardRef, OnChanges } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms'
import { DatePipe } from '@angular/common'

import * as moment from 'jalali-moment';
import { DatePickerDirective, DatePickerComponent } from 'ng2-jalali-date-picker';
import { FiscalPeriodService } from '../../service/index';

@Component({
    selector: 'sppc-datepicker',
    template: `<dp-date-picker 
    class="k-textbox"
    [(ngModel)]="dateObject"
    mode="daytime"
    [config]='dateConfig'
    theme="dp-material"
    (ngModelChange)="DateChange()">
  </dp-date-picker>`,
    styles: ['/deep/ dp-date-picker.dp-material .dp-picker-input { height: 26px !important; width:100% !important; } dp-date-picker{width:100%} /deep/ dp-day-time-calendar{position: fixed;}'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SppcDatepicker),
            multi: true
        }
    ]
})
export class SppcDatepicker implements OnInit, ControlValueAccessor {

    private maxDate: string;
    private minDate: string;
    private fiscalPeriodId: string;

    private dateConfig: any;
    
    constructor(private fiscalPeriodService: FiscalPeriodService, private datepipe: DatePipe) {


        //this section written in base class

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            this.fiscalPeriodId = currentContext ? currentContext.fpId.toString() : '';

        }

        //this section written in base class

        //todo: get fiscal period record for set minDate and maxDate
        //this.minDate = "01/20/2018";
        //this.maxDate = "02/15/2019";

        
    }

    ngOnInit() {
        var dateLocale = 'fa';
        var dateFormat = "YYYY/MM/DD HH:mm"
        var lang = localStorage.getItem('lang');
        if (lang) {
            dateLocale = lang;
            if (lang == "en")
                dateFormat = "MM/DD/YYYY HH:mm";
        }

        this.dateConfig = {
            format: dateFormat,
            //min: this.minDate,
            //max: this.maxDate,
            locale: dateLocale
        };
    }

    @Input() date: any;
    private dateObject = moment();
    propagateChange: any = () => { };

    DateChange() {
        this.propagateChange(this.datepipe.transform(this.dateObject, 'yyyy-MM-dd hh:mm'));
    }

    writeValue(value: any): void {
         if (value) {
             this.date = this.datepipe.transform(value, 'yyyy-MM-dd hh:mm');
             this.dateObject = moment(this.date);
        }
    }

    registerOnChange(fn: any): void {
        this.propagateChange = fn;
    }

    registerOnTouched(fn: any): void { }

}
