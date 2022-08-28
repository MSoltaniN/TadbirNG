
import { Component, OnInit, Input, forwardRef, ElementRef, ViewChild } from '@angular/core';
import { ControlValueAccessor, Validator, AbstractControl, FormControl, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { KeyCode } from '@sppc/shared/enum';




@Component({
  selector: 'sppc-numericInput',
  template: `
<input type="text" #numinput [(ngModel)]="showValue" (ngModelChange)="changeValue()" [OnlyNumber] class="k-textbox num-input" [ngClass]="cssClass" (keyup)="keyPress($event)"/>
`,
  styles: [`.num-input { width:100%; background: #fff !important; border: 1px solid #ccc !important; }`],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SppcNumericInput),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => SppcNumericInput),
      multi: true,
    }
  ]
})

export class SppcNumericInput implements OnInit, ControlValueAccessor, Validator {

  @ViewChild('numinput') numInput: ElementRef;
  
  @Input() cssClass: string = "";
  @Input('naturalNumbers') naturalNumbers = false
  @Input() set decimalCount(decCount: number) {
    this.deciCount = 0
    if (decCount)
      this.deciCount = decCount;
  }


  private parseError: boolean = false;

  showValue: string;
  hiddenValue: string;
  deciCount: number;

  constructor() { }

  ngOnInit() { }


  propagateChange: any = () => { };

  keyPress(event: any) {
    switch (event.keyCode) {
      case KeyCode.Plus: {
        var show = this.removeComma(this.showValue);
        this.showValue = (parseInt(show) * 100).toString()
        this.hiddenValue = this.showValue;

        if (this.showValue) {
          this.showValue = this.setComma(this.showValue);
          this.parseError = false;
        }
        else {
          this.parseError = true;
        }
        break;
      }
      case KeyCode.Minus: {
        var show = this.removeComma(this.showValue);
        this.showValue = (parseInt(show) * 1000).toString()
        this.hiddenValue = this.showValue;

        if (this.showValue) {
          this.showValue = this.setComma(this.showValue);
          this.parseError = false;
        }
        else {
          this.parseError = true;
        }
        break;
      }
      default:
        {
          this.showValue = this.removeComma(this.showValue);
          this.hiddenValue = this.showValue;
          if (this.showValue) {
            this.showValue = this.setComma(this.showValue, event);

            this.parseError = false;
          }
          else {
            this.parseError = true;
          }

          this.hiddenValue = this.removeComma(this.showValue);

          break;
        }
    }

    this.propagateChange(this.hiddenValue);
  }

  changeValue() {}

  setFocus() {
    setTimeout(() => {
      this.numInput.nativeElement.focus();
    }, 0);  
  }

  select() {
    setTimeout(() => {
      this.numInput.nativeElement.select();
    }, 0);
  }

  setComma(num: string, event?: any): string {
    var parts = num.toString().split(".");
    // حذف صفر از اول اعداد
    if (parts[0].charAt(0) == '0' && parts[0].length > 1) {
      parts[0] = parts[0].slice(1);
    }
    // برای جلوگیری از ممیزهای اضافی
    if (parts.length>2) {
      let integer = parts[0];
      parts.shift();
      parts = [integer,parts.join('')];
    }

    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    if (parts.length > 1 && this.deciCount) {
      if (parts[1].length > this.deciCount && event) {
        event.preventDefault();
      }
      parts[1] = parts[1].substr(0, this.deciCount);
    }
    return parts.join(".");
  }

  removeComma(num: string): string {
    return num.replace(/\,/g, '');
  }

  writeValue(value: any): void {
    if (value != null && value != undefined && value >= 0) {
      this.showValue = this.setComma(value);
      this.hiddenValue = this.showValue;
    }
    else {
      this.showValue = undefined;
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
