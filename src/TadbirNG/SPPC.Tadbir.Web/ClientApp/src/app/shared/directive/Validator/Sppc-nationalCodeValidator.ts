
import { AbstractControl, FormControl, ValidatorFn, Validators } from '@angular/forms';


export class SppcNationalCode {

  static validNationalCode(control: AbstractControl) {
    var nationalCode = control.value;

    if (nationalCode) {
      if (nationalCode.length != 10) {
        return { validNationalCode: true };
      }

      var allDigitEqual: Array<string> = ["0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999"];
      if (allDigitEqual.indexOf(nationalCode) > -1) {
        return { validNationalCode: true };
      }

      var chArray = nationalCode.split('');
      var num0 = parseInt(chArray[0]) * 10;
      var num2 = parseInt(chArray[1]) * 9;
      var num3 = parseInt(chArray[2]) * 8;
      var num4 = parseInt(chArray[3]) * 7;
      var num5 = parseInt(chArray[4]) * 6;
      var num6 = parseInt(chArray[5]) * 5;
      var num7 = parseInt(chArray[6]) * 4;
      var num8 = parseInt(chArray[7]) * 3;
      var num9 = parseInt(chArray[8]) * 2;
      var a = parseInt(chArray[9]);
      var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;

      var c = b % 11;
      if (!(((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)))) {
        return { validNationalCode: true }
      }
    }

    return null;
  }


  static validNationalCodeWithForeigners(isForeigners?: boolean): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {

      if (isForeigners)
        return null;

      var nationalCode = control.value;

      if (nationalCode) {
        if (nationalCode.length != 10) {
          return { validNationalCode: true };
        }

        var allDigitEqual: Array<string> = ["0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999"];
        if (allDigitEqual.indexOf(nationalCode) > -1) {
          return { validNationalCode: true };
        }

        var chArray = nationalCode.split('');
        var num0 = parseInt(chArray[0]) * 10;
        var num2 = parseInt(chArray[1]) * 9;
        var num3 = parseInt(chArray[2]) * 8;
        var num4 = parseInt(chArray[3]) * 7;
        var num5 = parseInt(chArray[4]) * 6;
        var num6 = parseInt(chArray[5]) * 5;
        var num7 = parseInt(chArray[6]) * 4;
        var num8 = parseInt(chArray[7]) * 3;
        var num9 = parseInt(chArray[8]) * 2;
        var a = parseInt(chArray[9]);
        var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;

        var c = b % 11;
        if (!(((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)))) {
          return { 'validNationalCode': true }
        }
      }

      return null;
    };
  }
}

export class NumberValidators {

  static minMax(prms:any): ValidatorFn {
    return (control: FormControl): {[key: string]: any} => {
      if(Validators.required(control)) {
        return null;
      }
      
      let val: number = control.value;

      if(isNaN(val) || /\D/.test(val.toString())) {
        return {"number": true};
      } else if(!isNaN(prms.min) && !isNaN(prms.max)) {
        if (val < prms.min)
          return {"min": true};
        else if(val > prms.max)
          return {"max": true};
        else
          return null;
      } else if(!isNaN(prms.min)) {
        
        return val < prms.min ? {"min": true} : null;
      } else if(!isNaN(prms.max)) {
        
        return val > prms.max ? {"max": true} : null;
      } else {
        
        return null;
      }
    };
  }
} 

