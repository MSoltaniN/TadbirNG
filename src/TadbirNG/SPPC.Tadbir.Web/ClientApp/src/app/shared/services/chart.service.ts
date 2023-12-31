import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { BaseService } from "../class";
import { WidgetSetting } from "../models/widgetSetting";

@Injectable()
export class ChartService extends BaseService {
  widgetToRefreshSubject = new Subject();
  widgetToRefresh$ = this.widgetToRefreshSubject.asObservable();

  refreshDashboard() {
    this.widgetToRefreshSubject.next("");
  }

  applyChartSetting(setting: WidgetSetting, data: any) {
    setting.series.forEach((item, index) => {
      if (index >= 0) {
        if (isNaN(parseInt(item.type))) data.datasets[index].type = item.type;
        else
          data.datasets[index].type = this.getChartTypeAlias(
            parseInt(item.type)
          );

        // data.datasets[index].backgroundColor = item.backgroundColor;
        // data.datasets[index].fill = false;
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
      type = this.getChartTypeAlias(1);
    } else if (
      settings.series &&
      settings.series.filter((p) => p.type == "2").length ==
        settings.series.length
    ) {
      type = this.getChartTypeAlias(2);
    } else if (
      settings.series &&
      settings.series.filter((p) => p.type == "4").length > 0
    ) {
      type = this.getChartTypeAlias(4);
    } else if (
      settings.series &&
      settings.series.filter((p) => p.type == "10").length > 0
    ) {
      type = this.getChartTypeAlias(10);
    } else if (
      settings.series &&
      settings.series.filter((p) => p.type == "11").length > 0
    ) {
      type = this.getChartTypeAlias(11);
    } else if (
      settings.series &&
      settings.series.filter((p) => p.type == "12").length > 0
    ) {
      type = this.getChartTypeAlias(12);
    }

    return type;
  }

  getColumnChartOptions(data) {
    if (data) {
      let option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            type: "shadow",
          },
        },
        grid: {
          left: "5%",
          bottom: "3%",
          top: "10%",
          containLabel: true,
        },
        legend: { show: true },
        xAxis: {
          type: "category",
          data: data.labels,
          axisTick: {
            alignWithLabel: true,
            inside: true,
          },
          axisLabel: {
            interval: 0,
            rotate: 30,
            alignWithLabel: true,
          },
          nameGap: 0,
        },
        yAxis: {
          type: "value",
        },
        series: this.getBarSeries(data.datasets),
      };

      return option;
    }
  }

  getOptions(type, data) {
    let options;

    switch (type) {
      case "bar": //column
        options = this.getColumnChartOptions(data);
        break;
      case "horizontalBar": //bar
        options = this.getBarChartOptions(data);
        break;
      case "line": //line
        options = this.getLineChartOptions(data);
        break;
      case "pie": //pie
        options = this.getPieChartOptions(data);
        break;
      case "Gauge_Circular": //pie
        options = this.getGaugeChartOptions(data);
        break;
      default:
        break;
    }

    return options;
  }

  getBarChartOptions(data) {
    if (data) {
      let option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            type: "shadow",
          },
        },
        grid: {
          left: "5%",
          bottom: "3%",
          top: "10%",
          containLabel: true,
        },
        legend: { show: true },
        yAxis: {
          data: data.labels,
          type: "category",
        },
        xAxis: {
          type: "value",
          axisLabel: {
            interval: 0,
            rotate: 30,
            alignWithLabel: true,
          },
        },
        series: this.getBarSeries(data.datasets),
      };

      return option;
    }
  }

  getLineChartOptions(data) {
    if (data) {
      let option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            type: "shadow",
          },
        },
        grid: {
          left: "5%",
          bottom: "3%",
          top: "10%",
          containLabel: true,
        },
        legend: { show: true },
        yAxis: {
          data: data.labels,
          type: "category",
        },
        xAxis: {
          type: "value",
          axisLabel: {
            interval: 0,
            rotate: 30,
            alignWithLabel: true,
          },
        },
        series: this.getBarSeries(data.datasets),
      };

      return option;
    }
  }

  getPieChartOptions(data) {
    if (data) {
      let option = {
        tooltip: {
          trigger: "item",
        },
        showEmptyCircle: true,
        grid: {
          left: "5%",
          bottom: "3%",
          top: "10%",
          containLabel: true,
        },
        legend: { show: true },
        series: this.getPieSeries(data.datasets),
      };

      return option;
    }
  }

  getGaugeChartOptions(data) {
    if (data) {
      let option = {
        tooltip: {
          formatter: "{a} <br/>{b} : {c}%",
        },
        series: [
          {
            name: "Pressure",
            type: "gauge",
            min: data.minValue,
            max: data.maxValue,
            progress: {
              show: true,
            },
            detail: {
              valueAnimation: true,
              formatter: "{value}",
            },
            data: [
              {
                value: data.value,
                name: "",
              },
            ],
          },
        ],
      };

      return option;
    }
  }

  getBarSeries(dataset: any[]) {
    const series: any[] = [];
    if (dataset) {
      dataset.forEach((d) => {
        let type = d.type;
        if (d.type == "horizontalBar") type = "bar";

        series.push({
          name: d.label,
          data: d.data,
          type: type,
        });
      });
    }
    return series;
  }

  getPieSeries(dataset: any[]) {
    const series: any[] = [];
    dataset.forEach((d) => {
      let type = d.type;
      series.push({
        name: d.label,
        data: d.data,
        type: type,
        label: { show: false },
      });
    });
    return series;
  }

  getChartTypeAlias(type: number) {
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
      case 4: //pie
        chartType = "pie";
        break;
      case 10: //gauge 10
        chartType = "Gauge_Circular";
        break;
      default:
        break;
    }

    return chartType;
  }

  getChartTypeName(type: number) {
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
      case 4: //pie
        chartType = "pie";
        break;
      default:
        break;
    }

    return chartType;
  }

  getChartTypeId(typeName: string) {
    let chartType = 0;

    switch (typeName) {
      case "bar": //column
        chartType = 1;
        break;
      case "horizontalBar": //bar
        chartType = 1;
        break;
      case "line": //line
        chartType = 3;
        break;
      case "pie": //pie
        chartType = 4;
        break;
      default:
        break;
    }

    return chartType;
  }
}
