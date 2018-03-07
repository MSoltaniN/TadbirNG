import { Component, OnInit, Input, forwardRef, OnChanges, OnDestroy } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms'
import { DatePipe } from '@angular/common'

import * as moment from 'jalali-moment';
import { DatePickerDirective, DatePickerComponent } from 'ng2-jalali-date-picker';
import { KeyCode } from '../../enum/KeyCode';

@Component({
    selector: 'sppc-datepicker',
    template: `<dp-date-picker 
    class="k-textbox"
    [(ngModel)]="dateObject"
    (keydown)="ChangeDateKey($event.keyCode)"
    (ngModelChange)="DateChange()"  
    [config]='dateConfig'
    theme="dp-material"
    >
  </dp-date-picker>`,
    styles: ['/deep/ dp-date-picker.dp-material .dp-picker-input { height: 26px !important; width:100% !important; } dp-date-picker{width:100%; direction:ltr;} /deep/ dp-day-calendar{position: fixed;}'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SppcDatepicker),
            multi: true
        }
    ]
})
export class SppcDatepicker implements OnInit, OnDestroy, ControlValueAccessor {

    public dateConfig: any;
    public dateLocale: string = 'fa';


    constructor(private datepipe: DatePipe) {
    }

    public inputDateFormat: string = 'yyyy/M/d hh:mm';


    ngOnInit() {
        //var dateLocale = 'fa';
        var dateFormat = "YYYY/M/D"
        var lang = localStorage.getItem('lang');
        if (lang) {
            this.dateLocale = lang;
            if (lang == "en")
                dateFormat = "M/D/YYYY";
        }

        this.dateConfig = {
            mode: "day",
            format: dateFormat,
            locale: this.dateLocale,
            showGoToCurrent: false
        };
    }

    ngOnDestroy() {
        moment.locale('en');
    }

    @Input() date: any;
    public dateObject = moment();
    propagateChange: any = () => { };

    public ChangeDateKey(event: any) {

        var allowKey = false;

        switch (event) {
            case KeyCode.Space: {
                var currentDate = this.dateObject.toDate();
                this.date = this.datepipe.transform(new Date().toString(), this.inputDateFormat);
                this.dateObject = moment(this.date);
                break;
            }
            case KeyCode.Page_Up: {
                var currentDate = this.dateObject.toDate();
                this.date = this.datepipe.transform(currentDate.setFullYear(currentDate.getFullYear() + 1), this.inputDateFormat);
                this.dateObject = moment(this.date);
                break;
            }
            case KeyCode.Page_Down: {
                var currentDate = this.dateObject.toDate();
                this.date = this.datepipe.transform(currentDate.setFullYear(currentDate.getFullYear() - 1), this.inputDateFormat);
                this.dateObject = moment(this.date);
                break;
            }
            case KeyCode.Down_Arrow: {
                var currentDate = this.dateObject.toDate();
                this.date = this.datepipe.transform(currentDate.setMonth(currentDate.getMonth() - 1), this.inputDateFormat);
                this.dateObject = moment(this.date);
                break;
            }
            case KeyCode.Up_Arrow: {
                var currentDate = this.dateObject.toDate();
                this.date = this.datepipe.transform(currentDate.setMonth(currentDate.getMonth() + 1), this.inputDateFormat);
                this.dateObject = moment(this.date);
                break;
            }
            case KeyCode.Left_Arrow: {
                var currentDate = this.dateObject.toDate();
                this.date = this.datepipe.transform(currentDate.setDate(currentDate.getDate() - 1), this.inputDateFormat);
                this.dateObject = moment(this.date);
                break;
            }
            case KeyCode.Right_Arrow: {
                var currentDate = this.dateObject.toDate();
                this.date = this.datepipe.transform(currentDate.setDate(currentDate.getDate() + 1), this.inputDateFormat);
                this.dateObject = moment(this.date);
                break;
            }
            default: {
                if ((event >= 48 && event <= 57) || (event >= 96 && event <= 105) || (event == 191) || (event == 111) || (event == 8)) {
                    allowKey = true;

                    if (this.dateLocale == "fa") {
                        moment.locale('fa');
                    }
                }
                else {
                    allowKey = false;
                }
                break;
            }
        }

        return allowKey;

    }

    DateChange() {
        debugger;
        var test = this.datepipe.transform(this.dateObject, this.inputDateFormat);
        this.propagateChange(test);
        moment.locale('en');
    }

    writeValue(value: any): void {
        if (value) {
            this.date = this.datepipe.transform(value, this.inputDateFormat);
            this.dateObject = moment(this.date);
        }
    }

    registerOnChange(fn: any): void {
        this.propagateChange = fn;
    }

    registerOnTouched(fn: any): void { }


}
