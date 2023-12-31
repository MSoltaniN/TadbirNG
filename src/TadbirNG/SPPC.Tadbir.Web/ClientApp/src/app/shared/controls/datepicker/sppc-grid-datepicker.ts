import { Component, OnInit, Input, forwardRef, OnDestroy, Renderer2, ElementRef, ViewContainerRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator } from '@angular/forms'
import { DatePipe } from '@angular/common'

import * as moment from 'jalali-moment';
import { CalendarType } from '@sppc/shared/enum/metadata';
import { BrowserStorageService } from '@sppc/shared/services';
import { SppcGridFilter } from '..';




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
    ::ng-deep dp-date-picker.dp-material .dp-picker-input { width:100% !important; } 
    dp-date-picker{width:100%; direction:ltr;} 
    ::ng-deep dp-day-calendar{position: fixed;}
::ng-deep dp-time-select{ display:none;}
::ng-deep dp-day-time-calendar { position: fixed; } ::ng-deep dp-day-time-calendar > dp-day-calendar{position:initial}
::ng-deep dp-day-time-calendar >  dp-time-select { display:block;}
::ng-deep sppc-grid-datepicker{ width:100% }
::ng-deep sppc-grid-datepicker input{ border-color: rgba(0, 0, 0, 0.08); }
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
    public displayType: string;

  constructor(private datepipe: DatePipe, private bStorageService: BrowserStorageService) {
    }

  ngOnInit() {
      
      
      var dateFormat = "YYYY/MM/DD"

     
      let lang: string;
      if (this.displayType) {
        if (this.displayType == CalendarType.Jalali)
          lang = "fa";

        if (this.displayType == CalendarType.Gregorian)
          lang = "en";
      }
      else {
        let config: any;
        var calConfig = this.bStorageService.getSystemConfig();
        if (calConfig) {
          config = JSON.parse(calConfig);
          if (config.defaultCalendar == 0)
            lang = "fa";

          if (config.defaultCalendar == 1)
            lang = "en";
        }
      }
        
        //var lang = localStorage.getItem('lang');
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
              var gDate = moment.from(date, this.dateLocale, 'YYYY/MM/DD').format('YYYY/MM/DD')

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
