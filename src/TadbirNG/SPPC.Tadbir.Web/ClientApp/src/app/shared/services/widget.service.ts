import { Injectable } from "@angular/core";
import { isNumber } from "util";
import { BaseService } from "../class";
import { WidgetSetting } from "../models/widgetSetting";

@Injectable()
export class ChartService extends BaseService {
  applyChartSetting(setting: WidgetSetting, data: any) {
    setting.series.forEach((item, index) => {
      if (index >= 0) {
        if (isNaN(parseInt(item.type))) data.datasets[index].type = item.type;
        else data.datasets[index].type = this.getChartType(parseInt(item.type));

        data.datasets[index].backgroundColor = item.backgroundColor;
        data.datasets[index].fill = false;
        data.datasets[index].label = item.name;
      }
    });

    return data;
  }

  getAdjustedChartType(settings: WidgetSetting) {
    let type = "bar";
    if (
      settings.series &&
      settings.series.filter((p) => p.type == "1").length ==
        settings.series.length
    ) {
      type = this.getChartType(1);
    } else if (
      settings.series &&
      settings.series.filter((p) => p.type == "2").length ==
        settings.series.length
    ) {
      type = this.getChartType(2);
    }

    return type;
  }

  getChartType(type: number) {
    let chartType = "";

    switch (type) {
      case 1: //column
        chartType = "bar";
        break;
      case 2: //bar
        chartType = "horizontalBar";
        break;
      case 3: //line
        chartType = "line";
        break;
      default:
        break;
    }

    return chartType;
  }
}
