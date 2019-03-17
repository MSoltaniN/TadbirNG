import { Pipe, PipeTransform } from "@angular/core";
import { SettingService } from "../service/index";
import { NumberConfig } from "../model/index";
import { SettingKey } from "../enum/settingsKey";
import { SessionKeys } from "../../environments/environment";


@Pipe({
  name: 'SppcNumConfig'
})
export class SppcNumConfigPipe implements PipeTransform {

  constructor(public settingService: SettingService) { }

  transform(value: any) {
    if (value == null || value == undefined)
      return "";

    let result: string = value;
    let config: NumberConfig;


    config = this.settingService.getNumberConfigBySettingId();
    if (config) {
      if (config.useSeparator) {
        result = this.setSeperator(value, config.separatorSymbol);
      }
    }

    return result;
  }


  setSeperator(num: string, char: string): string {
    var parts = num.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, char);
    return parts.join(".");
  }

  //removeComma(num: string): string {
  //  return num.replace(/\,/g, '');
  //}

  //setDecimalPrecision(num: string, precision: number): string {
  //  var parts = num.toString().split(".");
  //  if (parts.length == 1) {
  //    return 
  //  }
  //}

}
