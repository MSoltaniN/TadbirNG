import { Injectable } from "@angular/core";
import { BaseService } from "../class";
import { WidgetSetting } from "../models/widgetSetting";

@Injectable()
export class ChartService extends BaseService {
  applyChartSetting(setting: WidgetSetting, data: any) {
    setting.series.forEach((item) => {
      const index = data.datasets.findIndex((ds) => ds.label == item.name);
      if (index >= 0) {
        data.datasets[index].type = this.getChartType(parseInt(item.type));

        data.datasets[index].backgroundColor = item.backgroundColor;
        data.datasets[index].fill = false;
      }
    });

    return data;
  }

  getAdjustedChartType(settings: WidgetSetting) {
    if (
      settings.series &&
      settings.series.filter((p) => p.type == "1").length ==
        settings.series.length
    ) {
      return this.getChartType(1);
    } else if (
      settings.series &&
      settings.series.filter((p) => p.type == "2").length ==
        settings.series.length
    ) {
      return this.getChartType(2);
    }
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
