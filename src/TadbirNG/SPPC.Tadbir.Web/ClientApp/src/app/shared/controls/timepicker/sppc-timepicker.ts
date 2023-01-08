import { Component, OnInit, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator } from '@angular/forms'
import { DatePipe } from '@angular/common'


@Component({
  selector: 'sppc-timepicker',
  template: `<dp-date-picker
                           [(ngModel)]="selectedTime"
                           (onChange)=onChangeTime()
                           (keyUp.space)="onSpaceKey()"
                           mode="time"
                           dir="ltr"
                           theme="dp-material"
                           [config]="timeConfig">
             </dp-date-picker>`,
  styles: [`


    ::ng-deep dp-date-picker.dp-material .dp-picker-input { width:100% !important; } 
    dp-date-picker{width:100%; direction:ltr;} 
    ::ng-deep dp-day-calendar{position: fixed;}
    ::ng-deep sppc-timepicker input{
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
      useExisting: forwardRef(() => SppcTimepicker),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => SppcTimepicker),
      multi: true,
    }
  ]
})
export class SppcTimepicker implements OnInit, ControlValueAccessor, Validator {

  public timeConfig: any;
  public timeLocale: string = 'fa';
  private parseError: boolean = false;
  @Input('setTimeWithSpaceKey') setTimeWithSpaceKey = false
  selectedTime: any;

  propagateChange: any = () => { };

  constructor(private datepipe: DatePipe) { }

  ngOnInit() {

    var lang = localStorage.getItem('lang');
    if (lang)
      this.timeLocale = lang;

    this.timeConfig = {
      locale: this.timeLocale
    };
  }

  onSpaceKey() {
    let time = this.datepipe.transform(new Date(), 'HH:mm:ss')
    if (this.setTimeWithSpaceKey) {
      this.selectedTime = time
      this.propagateChange(time);
    }
  }

  onChangeTime() {
    if (typeof this.selectedTime == "string") {
      this.propagateChange(this.selectedTime);
    }
    else {
      var time = this.datepipe.transform(this.selectedTime, 'HH:mm:ss');
      this.propagateChange(time);
    }    
  }

  writeValue(value: any): void {
    if (value) {
      this.selectedTime = value;
    }
  }

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  registerOnTouched(fn: any): void {
    //this.propagateChange = fn;
  }

  public validate(control: FormControl) {
    return (!this.parseError) ? null : {
      jsonParseError: {
        valid: false,
      },
    };
  }


}
