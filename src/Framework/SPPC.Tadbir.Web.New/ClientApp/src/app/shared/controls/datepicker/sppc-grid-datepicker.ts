import { Component, OnInit, Input, forwardRef, OnDestroy, Renderer2 } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator } from '@angular/forms'
import { DatePipe } from '@angular/common'

import * as moment from 'jalali-moment';




@Component({
    selector: 'sppc-grid-datepicker',
    template: `<dp-date-picker mode="{{mode}}"
    class="k-textbox" 
    [(ngModel)]="dateObject"
    (onChange)="DateChange($event)" 
    [config]='dateConfig'
    theme="dp-material">
  </dp-date-picker>`,
    styles: [`
    /deep/ dp-date-picker.dp-material .dp-picker-input { width:100% !important; } 
    dp-date-picker{width:100%; direction:ltr;} 
    /deep/ dp-day-calendar{position: fixed;}
/deep/ dp-time-select{ display:none;}
/deep/ dp-day-time-calendar { position: fixed; } /deep/ dp-day-time-calendar > dp-day-calendar{position:initial}
/deep/ dp-day-time-calendar >  dp-time-select { display:block;}
/deep/ sppc-grid-datepicker{ width:100% }
/deep/ sppc-grid-datepicker input{ border-color: rgba(0, 0, 0, 0.08); }
       `],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SppcGridDatepicker),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => SppcGridDatepicker),
            multi: true,
        }
    ]
})
export class SppcGridDatepicker implements OnInit, OnDestroy, ControlValueAccessor, Validator {

    public dateConfig: any;
    public dateLocale: string = 'fa';
    private parseError: boolean = false;
    public inputDateFormat: string = 'yyyy/M/d hh:mm';

    public isDate : boolean = false;
    public isDateTime: boolean = false;

    public mode: string = 'daytime';

    constructor(private datepipe: DatePipe, private render: Renderer2) {
    }

    ngOnInit() {
        var dateFormat = "YYYY/MM/DD"
        var lang = localStorage.getItem('lang');
        if (lang) {
            this.dateLocale = lang;
            if (lang == "en")
                dateFormat = "MM/DD/YYYY";
        }
        if (this.mode == 'daytime') {
            dateFormat += " hh:mm";
            this.isDateTime = true;
        }
        else {
            this.isDate = true;
        }
        

        this.dateConfig = {            
            format: dateFormat,
            locale: this.dateLocale,
            showMultipleYearsNavigation: true
        };

        
    }

    ngOnDestroy() {
        moment.locale('en');
    }

    @Input() date: any;
    public dateObject : any;
    propagateChange: any = () => { };

    public destinationElementId: string;
    
    DateChange(event : any) {

        var hiddenElement = document.getElementById(this.destinationElementId) as any;
        
        if (this.dateObject) {
            var date = this.dateObject.format('YYYY/MM/DD');

            var hiddenElement = document.getElementById(this.destinationElementId) as any;
            if (hiddenElement) {

                //hiddenElement.value = date;
                var gDate = moment.from(date, 'fa', 'YYYY/MM/DD').format('YYYY/MM/DD')

                hiddenElement.setAttribute('ng-reflect-model', gDate);
                    
                hiddenElement.value = gDate;
                var eve = new Event('input');
                hiddenElement.dispatchEvent(eve);
            }
            
            
        }
        else {

            var hiddenElement = document.getElementById(this.destinationElementId) as any;

            hiddenElement.setAttribute('ng-reflect-model', '');

            hiddenElement.value = '';
            var eve = new Event('input');
            hiddenElement.dispatchEvent(eve);
        }



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

    public validate(c: FormControl) {
        return (!this.parseError) ? null : {
            jsonParseError: {
                valid: false,
            },
        };
    }

}
