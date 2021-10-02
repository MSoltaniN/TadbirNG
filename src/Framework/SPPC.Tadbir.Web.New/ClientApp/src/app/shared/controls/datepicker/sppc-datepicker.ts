import { Component, OnInit, Input, forwardRef, OnDestroy, Optional, Host, SkipSelf } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator, ControlContainer, AbstractControl } from '@angular/forms'
import { DatePipe } from '@angular/common'

import * as moment from 'jalali-moment';
import { KeyCode } from '@sppc/shared/enum';
import { CalendarType } from '@sppc/shared/enum/metadata';
import { BrowserStorageService } from '@sppc/shared/services';

@Component({
  selector: 'sppc-datepicker',
  template: `<dp-date-picker
    class="k-textbox"
    [(ngModel)]="dateObject"
    (keydown)="onChangeDateKey($event.keyCode)"
    (onChange)="onDateChange()" 
    (onGoToCurrent)="onGoToCurrentDate()"
    [config]='dateConfig'
    theme="dp-material"
    [disabled]="isReadOnly"
    (focusout)="onDateFocusOut()">
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
      useExisting: forwardRef(() => SppcDatepicker),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => SppcDatepicker),
      multi: true,
    }
  ]
})
export class SppcDatepicker implements OnInit, OnDestroy, ControlValueAccessor, Validator {
  public dateConfig: any;
  public dateLocale: string = 'fa';
  private parseError: boolean = false;
  public inputDateFormat: string = 'yyyy/MM/dd hh:mm';
  public dateFormat: string = "YYYY/MM/DD";
  public spliterChar: string = "/";

  startDate: Date | null;
  endDate: Date | null;

  @Input() date: any;
  @Input() isDisplayDate: boolean = true;
  @Input() displayDate: any;

  @Input() minDate: any;
  @Input() maxDate: any;

  public dateObject: moment.Moment | null;
  editDateValue: any;
  i: number = 0;
  _isReadOnly: boolean = false;  

  propagateChange: any = () => { };

  @Input() formControlName: string;
  @Input() displayType: string; //Jalali | Gregorian

  private control: AbstractControl | null;
  constructor(private datepipe: DatePipe, @Optional() @Host() @SkipSelf() private controlContainer: ControlContainer, private bStorageService: BrowserStorageService) {    
  }  

  ngOnInit() {
    if (this.controlContainer) {
      if (this.formControlName && this.controlContainer.control != null) {
        this.control = this.controlContainer.control.get(this.formControlName);
      }
    }

    if (this.control != null) {
      this.control.clearValidators();
    }

    //var startDate;
    //var endDate;
    var nowDate = new Date();
    var endDiff;
    var startDiff;
    var endDiffDays = 0;
    var startDiffDays = 0;

    if (this.minDate) {
      this.minDate = this.datepipe.transform(this.minDate, this.inputDateFormat);
      this.startDate = new Date(this.minDate.split(' ')[0]);
    }

    if (this.maxDate) {
      this.maxDate = this.datepipe.transform(this.maxDate, this.inputDateFormat);
      this.endDate = new Date(this.maxDate.split(' ')[0]);
    }

    this.dateObject = moment();

    if (this.endDate != null) {
      endDiff = nowDate.getTime() - this.endDate.getTime();
      endDiffDays = endDiff / (1000 * 3600 * 24);
      if (endDiffDays > 1) {
        this.dateObject = moment(this.endDate);
      }
    }

    if (this.startDate != null) {
      startDiff = this.startDate.getTime() - nowDate.getTime();
      startDiffDays = startDiff / (1000 * 3600 * 24);
      if (startDiffDays > 1) {
        this.dateObject = moment(this.startDate);
      }
    }

    if (this.startDate != null && this.endDate != null) {
      if (endDiffDays < 1 && startDiffDays < 1) {
        this.dateObject = moment();
      }
    }

    
    var lang : string;
    debugger;
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


    if (lang) {
      this.dateLocale = lang;
      if (lang == "en")
        this.dateFormat = "MM/DD/YYYY";
    }

    if (this.displayDate) {
      this.displayDate = this.datepipe.transform(this.displayDate, this.inputDateFormat);
      this.dateObject = moment(this.displayDate);
    }

    //if (!this.isDisplayDate) {
    //    if (this.editDateValue) {
    //        this.dateObject = this.editDateValue;
    //    }
    //    else {
    //        this.dateObject = null;
    //    }

    //    this.onDateFocusOut();
    //}

    
    this.initDateConfig();

  }

  initDateConfig() {
    this.dateConfig = {
      mode: "day",
      format: this.dateFormat,
      locale: this.dateLocale,
      min: this.minDate,
      max: this.maxDate,
      showGoToCurrent: true,
      showMultipleYearsNavigation: true
    };
  }

  get isReadOnly(): boolean {
    return this._isReadOnly;
  }

  @Input()
  set isReadOnly(value: boolean) {        
    this._isReadOnly = value;
  }

  ngOnDestroy() {
    moment.locale('en');
  }

  LimitationDate(toDate: any, operationIncrese?: boolean) {

    var endDiff;
    var startDiff;
    var endDiffDays = 0;
    var startDiffDays = 0;

    this.dateObject = moment(toDate);
    var strDate = this.datepipe.transform(toDate, this.inputDateFormat);
    if (strDate != null) {

      var date = new Date(strDate);

      if (this.endDate != null) {
        endDiff = date.getTime() - this.endDate.getTime();
        endDiffDays = endDiff / (1000 * 3600 * 24);
      }

      if (this.startDate != null) {
        startDiff = this.startDate.getTime() - date.getTime();
        startDiffDays = startDiff / (1000 * 3600 * 24);
      }

      if (operationIncrese && this.endDate != null && endDiffDays > 1) {

        this.dateObject = moment(this.endDate);
      }
      else
        if (!operationIncrese && this.startDate != null && startDiffDays > 1) {
          this.dateObject = moment(this.startDate);
        }

      if (operationIncrese == null) {

        if (this.endDate != null && endDiffDays > 1) {
          this.dateObject = moment(this.endDate);
        }

        if (this.startDate != null && startDiffDays > 1) {
          this.dateObject = moment(this.startDate);
        }

        if (this.startDate != null && this.endDate != null && endDiffDays < 1 && startDiffDays < 1) {
          this.dateObject = moment();
        }
      }
    }
  }

  onChangeDateKey(event: any) {
    var allowKey = false;

    switch (event) {
      case KeyCode.Tab: {
        allowKey = true;
        break;
      }
      case KeyCode.Space: {
        this.dateObject = moment();
        this.LimitationDate(new Date());
        break;
      }
      case KeyCode.Page_Up: {
        var newDate = this.dateObject != null ? this.dateObject.add(1, 'months') : moment();
        this.LimitationDate(newDate, true);
        break;
      }
      case KeyCode.Page_Down: {
        var newDate = this.dateObject != null ? this.dateObject.add(-1, 'months') : moment();
        this.LimitationDate(newDate, false);
        break;
      }
      case KeyCode.Down_Arrow: {
        var newDate = this.dateObject != null ? this.dateObject.add(-1, 'days') : moment();
        this.LimitationDate(newDate, false);
        break;
      }
      case KeyCode.Up_Arrow: {
        var newDate = this.dateObject != null ? this.dateObject.add(1, 'days') : moment();
        this.LimitationDate(newDate, true);
        break;
      }
      default: {
        if ((event >= 48 && event <= 57) || (event >= 96 && event <= 105) || (event == 191) || (event == 111) || (event == 8)) {
          allowKey = true;
        }
        else {
          allowKey = false;
        }
        break;
      }
    }

    this.onDateFocusOut();

    return allowKey;

  }

  onDateChange() {
    this.i++;
    if (!this.isDisplayDate && this.i <= 2) {
      this.dateObject = null;

      if (this.editDateValue) {
        this.dateObject = this.editDateValue;
      }
    }
    this.parseError = typeof this.dateObject === "object" && this.dateObject != null ? false : true;
    if (this.dateObject == undefined) {
      setTimeout(() => {
        this.propagateChange("");
      }, 1);
    }
    else {
      this.onDateFocusOut();
    }
  }

  onDateFocusOut() {
    this.parseError = false;

    if (this.dateObject != null) {
      if (typeof this.dateObject === "object") {
        this.parseError = false;
        setTimeout(() => {
          this.propagateChange(this.datepipe.transform(this.dateObject, this.inputDateFormat));
        }, 1);
      }
      else {
        //this.parseError = false;
        let strDate: string = this.dateObject;
        let dateArray: any;

        if (strDate === undefined) {
          this.parseError = true;
        }
        else {

          let yearDate: number = 0;
          let monthDate: number = 0;
          let dayDate: number = 0;

          var formatArray = this.dateFormat.split(this.spliterChar);
          dateArray = strDate.split(this.spliterChar);
          if (dateArray.length == 3) {
            for (var i = 0; i < formatArray.length; i++) {

              switch (formatArray[i]) {
                case "YYYY": {
                  yearDate = +dateArray[i];
                  break;
                }
                case "YY": {
                  yearDate = +dateArray[i];
                  break;
                }
                case "MM": {
                  monthDate = +dateArray[i];
                  break;
                }
                case "M": {
                  monthDate = +dateArray[i];
                  break;
                }
                case "DD": {
                  dayDate = +dateArray[i];
                  break;
                }
                case "D": {
                  dayDate = +dateArray[i];
                  break;
                }
                default: {
                  this.parseError = true;
                  break;
                }
              }

            }

            for (var i = 0; i < formatArray.length; i++) {

              switch (formatArray[i]) {
                case "YYYY": {
                  if (dateArray[i].length < 4) {
                    this.parseError = true;
                  }
                  break;
                }
                case "YY": {
                  if (dateArray[i].length < 2) {
                    this.parseError = true;
                  }
                  break;
                }
                case "MM": {
                  var month = +dateArray[i];
                  if (month == 0 || month > 12) {
                    this.parseError = true;
                  }
                  else {
                    if (month < 10) {
                      dateArray[i] = "0" + month.toString();
                    }
                    else {
                      dateArray[i] = month.toString();
                    }
                  }

                  break;
                }
                case "M": {
                  var month = +dateArray[i];
                  if (month == 0 || month > 12) {
                    this.parseError = true;
                  }
                  else {
                    dateArray[i] = month.toString();
                  }
                  break;
                }
                case "DD": {
                  var day = +dateArray[i];
                  if (day == 0 || day > 31 || (monthDate > 6 && day > 30)) {
                    this.parseError = true;
                  }
                  else {
                    if (day < 10) {
                      dateArray[i] = "0" + day.toString();
                    }
                    else {
                      dateArray[i] = day.toString();
                    }
                  }
                  break;
                }
                case "D": {
                  var day = +dateArray[i];
                  if (day == 0 || day > 31 || (monthDate > 6 && day > 30)) {
                    this.parseError = true;
                  }
                  else {
                    dateArray[i] = day.toString();
                  }
                  break;
                }
                default: {
                  this.parseError = true;
                  break;
                }
              }

            }
          }
          else {
            this.parseError = true;
          }
        }


        if (this.parseError) {
          setTimeout(() => {
            this.propagateChange("");
          }, 1);
        }
        else {
          this.dateObject = this.dateLocale == 'fa' ? moment(dateArray.join(this.spliterChar), 'jYYYY/jM/jD') : moment(dateArray.join(this.spliterChar).toString()).locale('en');

          setTimeout(() => {
            this.propagateChange(this.datepipe.transform(this.dateObject, this.inputDateFormat));
          }, 1);
        }

      }
    }
    else {
      //this.parseError = true;
    }
  }

  onGoToCurrentDate() {
    this.dateObject = moment();
  }

  writeValue(value: any): void {
    if (value) {
      this.date = this.datepipe.transform(value, this.inputDateFormat);
      this.editDateValue = moment(this.date);
      if (this.isDisplayDate) {
        this.dateObject = moment(this.date);
      }
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
