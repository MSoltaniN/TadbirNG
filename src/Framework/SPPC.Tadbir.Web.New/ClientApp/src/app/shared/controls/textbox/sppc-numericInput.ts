
import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, Validator, AbstractControl, FormControl, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { KeyCode } from '@sppc/shared/enum';




@Component({
  selector: 'sppc-numericInput',
  template: `
<input type="text" [(ngModel)]="showValue" (ngModelChange)="changeValue()" [OnlyNumber] class="k-textbox num-input" [ngClass]="cssClass" (keydown)="keyPress($event.keyCode)"/>
`,
  styles: [`.num-input { width:100% }`],
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

  @Input() cssClass: string = "";

  private parseError: boolean = false;

  showValue: string;
  hiddenValue: string;

  constructor() { }

  ngOnInit() { }


  propagateChange: any = () => { };

  keyPress(keyCode: any) {
    switch (keyCode) {
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
        break;
    }
    this.propagateChange(this.hiddenValue);
  }

  changeValue() {
    this.showValue = this.removeComma(this.showValue);
    this.hiddenValue = this.showValue;
    if (this.showValue) {
      this.showValue = this.setComma(this.showValue);

      this.parseError = false;
    }
    else {
      this.parseError = true;
    }

    this.propagateChange(this.hiddenValue);

  }

  setComma(num: string): string {
    var parts = num.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
  }

  removeComma(num: string): string {
    return num.replace(/\,/g, '');
  }

  writeValue(value: any): void {
    if (value != null && value!=undefined && value >= 0) {
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
