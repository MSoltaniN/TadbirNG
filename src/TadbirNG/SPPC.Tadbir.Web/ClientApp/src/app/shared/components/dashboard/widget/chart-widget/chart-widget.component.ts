import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { WidgetSetting } from "@sppc/shared/models/widgetSetting";
import { DashboardService } from "@sppc/shared/services";
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

  @ViewChild("chart") chart: UIChart;

  constructor(private dashboardService: DashboardService) {}

  ngOnInit() {}

  changeSettings(settings: WidgetSetting) {
    settings.series.forEach((item) => {
      const index = this.data.datasets.findIndex((ds) => ds.label == item.name);
      if (index >= 0) {
        this.data.datasets[index].type = this.dashboardService.getChartType(
          parseInt(item.type)
        );

        this.type = "";

        this.data.datasets[index].backgroundColor = item.backgroundColor;
      }
    });

    if (
      settings.series.filter((p) => p.type == "1").length ==
      settings.series.length
    ) {
      this.type = this.dashboardService.getChartType(1);
    }

    if (
      settings.series.filter((p) => p.type == "2").length ==
      settings.series.length
    ) {
      this.type = this.dashboardService.getChartType(2);
    }

    setTimeout(() => {
      this.chart.reinit();
    }, 10);
  }
}
