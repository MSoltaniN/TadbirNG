import { Component, OnInit, Input, forwardRef, OnChanges, OnDestroy, ViewChild, Renderer2, Host, ElementRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator } from '@angular/forms'
import { DatePipe } from '@angular/common'

import * as moment from 'jalali-moment';
import { DatePickerDirective, DatePickerComponent } from 'ng2-jalali-date-picker';
import { KeyCode } from '../../enum/KeyCode';



@Component({
    selector: 'sppc-grid-datepicker',
    template: `<dp-date-picker
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
    /deep/ sppc-datepicker input{
    border-color: rgba(0, 0, 0, 0.15);
    height: calc(1.42857em + (4px * 2) + (1px * 2)) !important;
    /* border-style: solid; */
    border-radius: 2px;
    padding: 4px 8px;
    width: 12.4em;
    box-sizing: border-box;
    border-width: 1px;
    border-style: solid;
    outline: 0;
    font: inherit;
    font-size: 14px;
    line-height: 1.42857em;
    display: inline-flex;
    vertical-align: middle;
    position: relative;
    -webkit-appearance: none;}
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

    public mode: string = 'day';

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
