import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { WidgetSetting } from "@sppc/shared/models/widgetSetting";
import { ChartService } from "@sppc/shared/services/widget.service";
import { UIChart } from "primeng/chart";


@Component({
  selector: "chart-widget",
  templateUrl: "./chart-widget.component.html",
  styleUrls: ["./chart-widget.component.css"],
})
export class ChartWidgetComponent implements OnInit {
  @Input() type: string = "bar";
  @Input() data;
  @Input() options;
  @Input() title;

  @ViewChild("chart") chart: UIChart;

  constructor(private chartService:ChartService) {}

  ngOnInit() {}

  changeSettings(settings: WidgetSetting) {
    debugger;
    //this.type = "bar";      

    this.data = this.chartService.applyChartSetting(settings,this.data)
    // settings.series.forEach((item) => {
    //   const index = this.data.datasets.findIndex((ds) => ds.label == item.name);
    //   if (index >= 0) {
    //     this.data.datasets[index].type = this.chartService.getChartType(
    //       parseInt(item.type)
    //     );

    //     this.data.datasets[index].backgroundColor = item.backgroundColor;
    //     this.data.datasets[index].fill = false;
    //   }
    // });

    // if (
    //   settings.series.filter((p) => p.type == "1").length ==
    //   settings.series.length
    // ) {
    //   this.type = this.chartService.getChartType(1);
    // } else if (
    //   settings.series.filter((p) => p.type == "2").length ==
    //   settings.series.length
    // ) {
    //   this.type = this.chartService.getChartType(2);
    // }

    this.type = this.chartService.getAdjustedChartType(settings);

    setTimeout(() => {
      this.chart.reinit();
    }, 10);
    
  }
}
