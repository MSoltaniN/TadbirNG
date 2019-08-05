import { Pipe, PipeTransform } from "@angular/core";
import { SettingService } from "@sppc/config/service";
import { NumberConfig } from "@sppc/config/models";


@Pipe({
  name: 'SppcNumConfig'
})
export class SppcNumConfigPipe implements PipeTransform {

  constructor(public settingService: SettingService) { }

  async transform(value: any, withDecimal: boolean) {

    if (value == null || value == undefined)
      return "";

    let result: string = value;
    let config: NumberConfig;
    let hasDecimal: boolean = true;

    if (withDecimal != null) {
      hasDecimal = withDecimal;
    }

    config = await this.settingService.getNumberConfigBySettingIdAsync();

    if (config) {
      if (config.useSeparator) {
        result = this.setSeperator(value, config.separatorSymbol);
      }
    }
    if (parseInt(value) > 0 && hasDecimal) {
      result = this.setDecimalPrecision(result, config);
    }

    return result;
  }


  setSeperator(num: string, char: string): string {
    var parts = num.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, char);
    return parts.join(".");
  }

  setDecimalPrecision(num: string, config: NumberConfig): string {
    if (config && config.decimalPrecision > 0) {

      var parts = num.toString().split(".");
      var number = num;

      var decimalPrecisionDigit = config.maxPrecision < config.decimalPrecision ? config.maxPrecision : config.decimalPrecision;

      if (parts.length > 1) {
        number = parts[1];
        if (number.length != decimalPrecisionDigit) {
          if (number.length < decimalPrecisionDigit) {
            for (var i = 0; i <= decimalPrecisionDigit - number.length; i++) {
              number += "0";
            }
          }
          else {
            number = number.substring(0, decimalPrecisionDigit);
          }
        }

        parts[1] = number;
      }
      else {
        var decimalNum = "";
        for (var i = 0; i < decimalPrecisionDigit; i++) {
          decimalNum += "0";
        }
        parts.push(decimalNum);
      }

      return parts.join(".");
    }

    return num;
  }

  //removeComma(num: string): string {
  //  return num.replace(/\,/g, '');
  //}

}
